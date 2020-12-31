USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetAllOrderTrackingFiles]    Script Date: 06/07/2017 09:19:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetAllOrderTrackingFiles] 	@AccountId Int, 
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

CREATE TABLE #ALLCAs (ProgramList Varchar(500),
		      	    GroupId     Int,
		      	    GroupName   Varchar(50),
		     	    CampaignId  Int,
			    CampaignType Varchar(50),
			    Stagedate    Varchar(12),
		     	    FMName 	Varchar(50),
		      	    HasOnlineOrder Varchar(1),
			    CAStart     Varchar(10),
		                 CAEnd       Varchar(10)
			)



DECLARE 	@SelectString	Varchar(2000)

SET @SelectString =	'INSERT INTO #ALLCAs
			SELECT Distinct 
			QSPCanadaOrderManagement.dbo.UDF_ProgramsbyCampaign(c.id) ProgramList,
			GroupId,
			UPPER(GroupName) GroupName,
			ot.CampaignId,
			Case IsNull(C.IsStaffOrder,0) When 1 Then ''Staff'' Else ''Non-Staff'' End CampaignType,
			Null StageDate,
			(SELECT fm.Firstname + '' '' + fm.Lastname FROM QSPCanadaCommon..FieldManager fm JOIN QSPCanadaCommon..Campaign camp on camp.FMID = fm.FMID WHERE camp.ID = c.ID) AS FMName,
			--MAX(UPPER(FMName) FMName,
			CASE WHEN (SELECT COUNT(*)
				          FROM QSPCanadaOrderManagement..Batch B
				          WHERE B.CampaignId = OT.CampaignId 
				          AND OrderQualifierId=39009) > 0 THEN ''Y''
		             ELSE ''N''
			END HasOnlineOrder,
			Convert(varchar(10),c.StartDate,101) CAStart,
			Convert(varchar(10), c.Enddate,101) CAEnd	
			FROM		QSPCanadaOrderManagement.dbo.OrderStageTracking ot
			JOIN		QSPcanadaCommon.dbo.Campaign c
							ON	c.ID = ot.CampaignID
			LEFT JOIN	Batch b
							ON	b.OrderID = ot.OrderID
			WHERE		Convert(dateTime,Convert(Varchar(10),StageDate,101) ,101) >= '''+Convert(varchar(10),@DateFrom,101) +''''

IF ISNULL(@DateTo,'01/01/1995') <> '01/01/1995'
BEGIN
	SET @SelectString =  @SelectString + ' AND Convert(dateTime,Convert(Varchar(10),StageDate,101) ,101)  <= '''+convert(varchar(10),@DateTo,101)+''''
END


IF ISNULL(@AccountId,0) > 0
BEGIN

	SET @SelectString =  @SelectString + ' AND GroupId ='+ CAST(@AccountId AS VARCHAR)
END

IF ISNULL(@AccountName,'')  <> ''
BEGIN

	SET @SelectString =  @SelectString + ' AND GroupName  LIKE '''+  LTRIM(RTRIM(@AccountName))+'%'''
END

IF ISNULL(@CampaignId,0) > 0
BEGIN

	SET @SelectString =  @SelectString + ' AND ot.CampaignId ='+ CAST( @CampaignId AS VARCHAR)
END
IF ISNULL(@FMId,'')  <> ''
BEGIN

	SET @SelectString =  @SelectString + ' AND c.FMID  LIKE '''+  LTRIM(RTRIM(@FMId))+'%'''
END

IF ISNULL(@OrderId,0) > 0
BEGIN

	SET @SelectString =  @SelectString + ' AND ot.OrderId ='+ CAST(@OrderId AS VARCHAR)
END

IF ISNULL(@OrderQualifierId,0) > 0
BEGIN

	SET @SelectString =  @SelectString + ' AND b.OrderQualifierID ='+ CAST(@OrderQualifierID AS VARCHAR)
END

IF  ISNULL(@OrderStatus,  'ALL') <> 'ALL'
BEGIN
	IF @ShowOrdersPastStage = 0
	BEGIN
		IF ISNULL(@OrderId,0) > 0
		BEGIN

			SET @SelectString =  @SelectString + ' AND EXISTS (Select CampaignId, Max(Stage),OrderId From QSPCanadaOrderManagement.dbo.OrderStageTracking t
	    								   Where t.campaignId=ot.campaignid
									   and t.orderid=	ot.orderid	
	    								   Group By CampaignId,OrderId
	    								   Having Max(Stage) = ' + @OrderStatus +' )'
		END
		ELSE
		BEGIN
			SET @SelectString =  @SelectString + ' AND EXISTS (Select CampaignId, Max(Stage) From QSPCanadaOrderManagement.dbo.OrderStageTracking t
	    								   Where t.campaignId=ot.campaignid
									   Group By CampaignId
	    								   Having Max(Stage) = ' + @OrderStatus +' )'
		END
	END
	ELSE
	BEGIN
		IF ISNULL(@OrderId,0) > 0
		BEGIN
			SET @SelectString =  @SelectString + ' AND EXISTS (Select CampaignId, Max(Stage),OrderId From QSPCanadaOrderManagement.dbo.OrderStageTracking t
	    								   Where t.campaignId=ot.campaignid
									   and t.orderid=	ot.orderid	
	    								   Group By CampaignId,OrderId
	    								   Having Max(Stage) >= ' + @OrderStatus +' )'
		END
		ELSE
		BEGIN
			SET @SelectString =  @SelectString + ' AND EXISTS (Select CampaignId, Max(Stage) From QSPCanadaOrderManagement.dbo.OrderStageTracking t
	    								   Where t.campaignId=ot.campaignid
									   Group By CampaignId
	    								   Having Max(Stage) >= ' + @OrderStatus +' )'
		END
	END
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

Select ProgramList, GroupId,GroupName,	CampaignId,  CampaignType , StageDate,FMName,HasOnlineOrder,CAStart,CAEnd  from #ALLCAs
	
DROP TABLE #ALLCAs
GO
