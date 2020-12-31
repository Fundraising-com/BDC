USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetTrackingOrderDetail]    Script Date: 06/07/2017 09:19:37 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetTrackingOrderDetail]      @AccountId Int, 
							@AccountName Varchar(50),
							@CampaignId Int, 
							@FMId	Varchar(4),
							@DateFrom datetime,
							@DateTo  datetime,
							@OrderId int,
							@OrderStatus Varchar(10),
							@ShowOrdersPastStage BIT,
							@OrderQualifierID INT,
							@ProductType INT

AS

Declare @SelectString Varchar(4000)


CREATE TABLE [dbo].[#OrderDetail] (
		CampaignId 	int NOT NULL ,
		OrderId		int,
		Stage		int,
		ReceiptDate 	datetime NULL,
		ImageDate 	datetime NULL,
		DataCaptureDate	datetime NULL,
		EditDate 	datetime NULL,
		VerificationDate  datetime NULL,
		TransmitDate 	datetime NULL,
		ToteID		int
) 

CREATE TABLE [dbo].[#OutPutTable] (
		CampaignId 	int NOT NULL ,
		OrderId		int,
		Stage		int,
		ReceiptDate 	datetime NULL,
		ImageDate 	datetime NULL,
		DataCaptureDate	datetime NULL,
		VerificationDate  datetime NULL,
		EditDate 	datetime NULL,
		TransmitDate 	datetime NULL,
		ToteID		int
) 

Set @SelectString='
Insert #OrderDetail
SELECT	t.campaignid,ISNULL(t.orderid,0) OrderID,stage,null,null,null,null,null,null,ISNULL(t.ToteID,0) ToteID
FROM 	OrderStageTracking t
LEFT JOIN	Batch b
				ON	b.OrderID = t.OrderID
JOIN		QSPcanadacommon..codedetail cd
				ON	cd.Instance = t.Stage
WHERE		t.ToteID > 0'

IF ISNULL(@DateFrom,'01/01/1995') <> '01/01/1995'
BEGIN
	SET @SelectString =  @SelectString + ' AND Convert(dateTime,Convert(Varchar(10),StageDate,101) ,101)  >= '''+convert(varchar(10),@DateFrom,101)+''''
END

IF ISNULL(@DateTo,'01/01/1995') <> '01/01/1995'
BEGIN
	SET @SelectString =  @SelectString + ' AND Convert(dateTime,Convert(Varchar(10),StageDate,101) ,101)  <= '''+convert(varchar(10),@DateTo,101)+''''
END

IF ISNULL(@AccountId,0) > 0
BEGIN

	SET @SelectString =  @SelectString + ' AND T.GroupId ='+ CAST(@AccountId AS VARCHAR)
END

IF ISNULL(@AccountName,'')  <> ''
BEGIN

	SET @SelectString =  @SelectString + ' AND T.GroupName  LIKE '''+  LTRIM(RTRIM(@AccountName))+'%'''
END

IF ISNULL(@CampaignId,0) > 0
BEGIN

	SET @SelectString =  @SelectString + ' AND T.CampaignId ='+ CAST( @CampaignId AS VARCHAR)
END

IF ISNULL(@FMId,'')  <> ''
BEGIN

	SET @SelectString =  @SelectString + ' AND T.FMID  LIKE '''+  LTRIM(RTRIM(@FMId))+'%'''
END

IF ISNULL(@OrderId,0) > 0
BEGIN

	SET @SelectString =  @SelectString + ' AND T.OrderId ='+ CAST(@OrderId AS VARCHAR)
END

IF ISNULL(@OrderQualifierID,0) > 0
BEGIN

	SET @SelectString =  @SelectString + ' AND b.OrderQualifierID ='+ CAST(@OrderQualifierID AS VARCHAR)
END

IF ISNULL(@ProductType,0) > 0
BEGIN

	SET @SelectString =  @SelectString + ' AND b.OrderID IN (	SELECT	OrderID
																FROM	Batch b
																JOIN	CustomerOrderHeader coh ON coh.OrderBatchID = b.ID AND coh.OrderBatchDate = b.Date
																JOIN	CustomerOrderDetail cod ON cod.CustomerOrderHeaderInstance = coh.Instance
																WHERE	cod.ProductType = ' + CAST(@ProductType AS VARCHAR) + ')'

END

EXEC (@SelectString)

UPDATE #OrderDetail
SET 	ReceiptDate = OrderStageTracking.ReceiptDate,
	ImageDate = OrderStageTracking.ImageDate,
	DataCaptureDate = OrderStageTracking.DataCaptureDate,
	EditDate = OrderStageTracking.EditDate,
	VerificationDate = OrderStageTracking.VerificationDate,
	TransmitDate = OrderStageTracking.TransmitDate
FROM     OrderStageTracking
WHERE OrderStageTracking.CampaignID = #OrderDetail.CampaignID
AND       ISNULL(OrderStageTracking.OrderID, 0)=ISNULL(#OrderDetail.OrderId, 0)
AND       OrderStageTracking.Stage = #OrderDetail.Stage

--Get Max Stage/Dates
INSERT #OutPutTable
SELECT  CampaignId,
		OrderId,
		Max(stage) Stage, 
		Max(ReceiptDate) ReceiptDate,
		Max(ImageDate) ImageDate,
		MAX(DatacaptureDate) DatacaptureDate,
		Max(VerificationDate) VerificationDate,
		Max(EditDate) EditDate,
		Max(TransmitDate) TransmitDate,
		ToteID
FROM #OrderDetail
GROUP BY CampaignId,OrderId, ToteID

SELECT		CampaignId,
			OrderId,
			Stage,
			Convert(Varchar(10), ReceiptDate, 101) ReceiptDate,
			Convert(Varchar(10), ImageDate, 101) ImageDate,
			Convert(Varchar(10), Datacapturedate, 101) Datacapturedate,
			Convert(Varchar(10), VerificationDate, 101) VerificationDate,
			Convert(Varchar(10), EditDate, 101) EditDate,
			Convert(Varchar(10), TransmitDate, 101) TransmitDate,
			ToteID
FROM		#OutPutTable

/*
--Take Record with Max Stage
INSERT #OutPutTable
SELECT  CampaignId,OrderId,
	Max(stage)Stage, 
	Convert(Varchar(10),'01/01/1900',101)  ReceiptDate ,
	'01/01/1900' ImageDate,
	'01/01/1900' DatacaptureDate,
	'01/01/1900' VerificationDate,
	'01/01/1900' EditDate,
	'01/01/1900' TransmitDate
FROM #OrderDetail
GROUP BY CampaignId,OrderId


UPDATE #OutPutTable
SET 	 ReceiptDate = #OrderDetail.ReceiptDate,
	 ImageDate = #OrderDetail.ImageDate,
	 DataCaptureDate = #OrderDetail.DataCaptureDate,
	 EditDate = #OrderDetail.EditDate,
	 VerificationDate = #OrderDetail.VerificationDate,
	TransmitDate = #OrderDetail.TransmitDate
FROM #OrderDetail
WHERE #OrderDetail.CampaignID = #OutPutTable.CampaignID
AND ISNULL(#OrderDetail.OrderID, 0) = ISNULL(#OutPutTable.OrderId, 0)
AND #OrderDetail.Stage=#OutPutTable.Stage


SELECT  CampaignId,OrderId,Max(stage),
	CASE Convert(Varchar(10),ReceiptDate,101) 
	WHEN '01/01/1900' Then Null
	Else convert(varchar(10),ReceiptDate,101) 
	End ReceiptDate,
	CASE Convert(Varchar(10),ImageDate,101) 
	WHEN '01/01/1900' Then Null
	Else Convert(Varchar(10),ImageDate,101)
	End ImageDate,
	CASE Convert(Varchar(10),DatacaptureDate,101) 
	WHEN '01/01/1900' Then Null
	Else Convert(Varchar(10),DatacaptureDate,101)
	End DatacaptureDate,
	CASE Convert(Varchar(10),VerificationDate,101) 
	WHEN '01/01/1900' Then Null
	Else Convert(Varchar(10),VerificationDate,101)
	End VerificationDate,
	CASE Convert(Varchar(10),EditDate,101) 
	WHEN '01/01/1900' Then Null
	Else Convert(Varchar(10),EditDate,101)
	End EditDate,
	CASE Convert(Varchar(10),TransmitDate,101) 
	WHEN '01/01/1900' Then Null
	Else Convert(Varchar(10),TransmitDate,101)
	End TransmitDate
FROM #OutPutTable
GROUP BY CampaignId,OrderId,ReceiptDate,ImageDate,DatacaptureDate,VerificationDate,EditDate,TransmitDate
*/
DROP TABLE #OrderDetail
DROP TABLE #OutPutTable
GO
