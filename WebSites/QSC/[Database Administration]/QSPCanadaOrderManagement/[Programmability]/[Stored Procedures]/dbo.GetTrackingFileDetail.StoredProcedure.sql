USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetTrackingFileDetail]    Script Date: 06/07/2017 09:19:37 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetTrackingFileDetail]         
							@AccountId Int, 
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

DECLARE @SelectString Varchar(8000)

CREATE TABLE [dbo].[#OrderInFile] (
		CampaignId 			int NOT NULL,
		OrderQualifierID	int,
		OrderId				int,
		Stage 				int NOT NULL,
		[Description] 		varchar (70) NULL ,
		PaymentSend 		NUMERIC(10,2) NULL ,
		ScanCount			int,
		DateShipped 		datetime NULL,
		DateInvoiced 		datetime NULL
		--ToteID				int
) 

CREATE TABLE [dbo].[#OrderShipmentDate] (
		OrderId int NOT NULL,
		Shipmentdate datetime NULL
) 

SET @SelectString=
	'INSERT INTO DBO.#OrderInFile
SELECT 
			t.CampaignId,
			b.OrderQualifierID,
			ISNULL(t.orderid,0) OrderID,
			MAX(Stage) Stage, 
			NULL Description,
			0 PaymentSend,
			Min(ScanCount) ScanCount,
			t.DateShipped,
			MAX(t.DateInvoiced) DateInvoiced
			--ISNULL(t.ToteID,0) ToteID
FROM		OrderStageTracking t
LEFT JOIN	Batch b
				ON	b.OrderID = t.OrderID
WHERE	Convert(dateTime,Convert(Varchar(10),StageDate,101) ,101) >= '''+Convert(varchar(10),@DateFrom,101) +'''
AND		(t.OrderID > 0 OR NOT EXISTS (SELECT 1 FROM OrderStageTracking t2 WHERE t2.CampaignID = t.CampaignID AND t2.OrderID > 0 AND t2.ReceiptDate = t.ReceiptDate AND t2.StageDate >= '''+Convert(varchar(10),@DateFrom,101)+''' AND t2.StageDate <= '''+Convert(varchar(10),@DateTo,101)+'''))'

IF ISNULL(@DateTo,'01/01/1995') <> '01/01/1995'
BEGIN
	SET @SelectString =  @SelectString + 'AND Convert(dateTime,Convert(Varchar(10),StageDate,101) ,101)  <= '''+convert(varchar(10),@DateTo,101)+''''
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

SET @SelectString =  @SelectString +' GROUP BY t.CampaignId, b.OrderQualifierID, ISNULL(t.orderid,0),  DateShipped'--, ISNULL(t.ToteID,0)' 

IF  ISNULL(@OrderStatus,  'ALL') <> 'ALL'
BEGIN
	IF @ShowOrdersPastStage = 0
	BEGIN
		SET @SelectString =  @SelectString +' Having Max(Stage)= '+@OrderStatus
	END
	ELSE
	BEGIN
		SET @SelectString =  @SelectString +' Having Max(Stage)>= '+@OrderStatus
	END	
END

PRINT @SelectString
EXEC (@SelectString)

UPDATE #OrderInFile
SET Description = QSPCanadaCommon..CodeDetail.Description
From QSPCanadaCommon..CodeDetail
Where QSPCanadaCommon..CodeDetail.Instance = #OrderInFile.Stage

INSERT INTO #OrderShipmentDate
SELECT OrderId,MAX(Shipmentdate) Shipmentdate
FROM   QSPcanadaOrdermanagement..Batch b,
	QSPCanadaOrderManagement..CustomerorderHeader h,
     	QSPCanadaOrderManagement..CustomeroRderDetail d
	LEFT JOIN QSPCanadaOrderManagement.dbo.Shipment S ON D.ShipmentID = S.ID
WHERE   b.id=OrderBatchId
AND	b.date=orderBatchdate
AND	h.instance=d.customerorderheaderinstance 
AND     b.OrderId IN (SELECT DISTINCT OrderId FROM #OrderInFile  WHERE Stage >=59005)
GROUP BY OrderId

UPDATE #OrderInFile
SET #OrderInFile.DateShipped= #OrderShipmentDate.ShipmentDate
FROM #OrderShipmentDate
WHERE #OrderInFile.OrderId=#OrderShipmentDate.OrderId


SELECT * FROM #OrderInFile

DROP TABLE #OrderInFile
DROP TABLE #OrderShipmentDate
GO
