USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Ins_Report_Parameters_V2]    Script Date: 06/07/2017 09:20:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_Ins_Report_Parameters_V2]
	  @OrderID int
	, @CreatedBY int
	, @shipmentGroupID int = null
AS

-- this proc is used to pick the order, reserve quantities and supply parameter info in the data driven subscriptions tables.
set nocount on
DECLARE @RRBID int
DECLARE @BatchID int
DECLARE @BatchDATE datetime
DECLARE @CampaignID int
DECLARE @Language varchar(10)
DECLARE @BatchOrderId int
DECLARE @Is_MagExpress char(1)
DECLARE @Is_Combo char(1)
DECLARE @Is_Planetary char(1)
DECLARE @Is_Regular_Mag char(1)
DECLARE @has_bhe char(1)
DECLARE @Is_Magnet int
DECLARE @OrderQualifier int
DECLARE @BHE_WH int, @Prizes_WH int, @Popcorn_WH int, @CookieDough_WH int, @Gift_WH int, @PretzelRods_WH int, @DiscountCardFulfilledByFM int, @IsTPL int
DECLARE @NO_PRINT int  -- contains 1 if order should not be printed at all.
Declare @QSPPrint int
Declare  @IsStaff int, @Error int
DECLARE @CustomerDeliveredOrder BIT
DECLARE @SingleShipmentReportsInOtherShipment BIT
DECLARE @IsBackOrder BIT

Select @QSPPrint = 1

SET @NO_PRINT = 0 
SET @IsTPL   = 0
SET @Error     = 0 


SELECT
	 @BatchID 	= B.[id]
	,@BatchDATE 	= B.[date]
	,@CampaignID 	= B.CampaignID 
	,@Language 	= UPPER(C.Lang) 
	,@OrderQualifier = OrderQualifierID
FROM
	QSPCanadaOrderManagement.dbo.Batch B
	LEFT JOIN QSPCanadaCommon.dbo.CAccount C ON B.AccountId = C.[Id]
WHERE
	OrderId = @OrderID
 

 Select @Error = 1
 from QspCanadaCommon.dbo.SystemErrorLog   
 where Orderid = @OrderId
            and isFixed = 0 


 IF @BatchID is NULL   OR
     @Error = 1
   begin
          if @Error = 1 
             begin
              Select 'Error - Order# '+str(@OrderId)+ ' - pr_Ins_Report_Parameters_V2 - Did not run due to an un-fixed issue in SystemErrorLog table'
	end 
	else  if @BatchID is NULL
	begin
	     Select 'Error - Order# '+str(@OrderId)+ 'pr_Ins_Report_Parameters_V2 - Invalid Order, Order does not exist in batch table'
	end
   end 
ELSE

 BEGIN


Select top 1 @IsStaff = ca.ISSTAFFORDER
         from 	QspcanadaorderManagement.dbo.Batch as batch,
		QSPCANADACOMMON..CAMPAIGN as ca
 	 where 	batch.campaignid = ca.id
		and batch.orderid  = @OrderID 



  Select	top 1 @BHE_WH = bdc.DistributionCenterID
  FROM		BatchDistributionCenter bdc
  JOIN		QSPCanadaCommon..QSPProductLine pl ON pl.ID = bdc.QSPProductLine
  where		bdc.batchID = @BatchID
  and		bdc.BatchDate  = @BatchDATE
  and		bdc.QspProductLine in ( 46006,46007,46012) --bhe items
  and		pl.ShipmentGroupID = @shipmentGroupID

  set @BHE_WH = isnull(@BHE_WH,0)


  Select	top 1 @Prizes_WH = bdc.DistributionCenterID
  FROM		BatchDistributionCenter bdc
  JOIN		QSPCanadaCommon..QSPProductLine pl ON pl.ID = bdc.QSPProductLine
  where		bdc.batchID = @BatchID
  and		bdc.BatchDate  = @BatchDATE
  and		bdc.QspProductLine in ( 46008,46013,46014,46015) --prizes (for teacher box labels)
  and		pl.ShipmentGroupID = @shipmentGroupID

 Set @Prizes_WH  = isnull(@Prizes_WH,0)

  Select	top 1 @Popcorn_WH = bdc.DistributionCenterID
  FROM		BatchDistributionCenter bdc
  JOIN		QSPCanadaCommon..QSPProductLine pl ON pl.ID = bdc.QSPProductLine
  where		bdc.batchID = @BatchID
  and		bdc.BatchDate  = @BatchDATE
  and		bdc.QspProductLine in (46019) --Popcorn
  and		pl.ShipmentGroupID = @shipmentGroupID

  set @Popcorn_WH = isnull(@Popcorn_WH,0)

  Select	top 1 @CookieDough_WH = bdc.DistributionCenterID
  FROM		BatchDistributionCenter bdc
  JOIN		QSPCanadaCommon..QSPProductLine pl ON pl.ID = bdc.QSPProductLine
  where		bdc.batchID = @BatchID
  and		bdc.BatchDate  = @BatchDATE
  and		bdc.QspProductLine in (46018) --Cookie Dough
  and		pl.ShipmentGroupID = @shipmentGroupID

  set @CookieDough_WH = isnull(@CookieDough_WH,0)

  Select	top 1 @Gift_WH = bdc.DistributionCenterID
  FROM		BatchDistributionCenter bdc
  JOIN		QSPCanadaCommon..QSPProductLine pl ON pl.ID = bdc.QSPProductLine
  where		bdc.batchID = @BatchID
  and		bdc.BatchDate  = @BatchDATE
  and		bdc.QspProductLine in (46002, 46007, 46020, 46022, 46024) --Gift, Jewellery, Candles, Discount Cards
  and		pl.ShipmentGroupID = @shipmentGroupID

  set @Gift_WH = isnull(@Gift_WH,0)

  Select	top 1 @PretzelRods_WH = 1
  FROM		Batch b
  JOIN		CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
  JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
  WHERE		cod.ProductType IN (46025) --Pretzel Rods
  AND		b.OrderID = @OrderID

  set @PretzelRods_WH = isnull(@PretzelRods_WH,0)

  Select	top 1 @DiscountCardFulfilledByFM = 1
  FROM		Batch b
  JOIN		CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
  JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
  LEFT JOIN	BatchDistributionCenter bdc
				ON	bdc.BatchID = b.ID AND bdc.BatchDate = b.Date
  WHERE		cod.ProductType IN (46024) --Discount Cards
  AND		b.OrderID = @OrderID
  AND		bdc.BatchID IS NULL

  set @DiscountCardFulfilledByFM = isnull(@DiscountCardFulfilledByFM,0)

  Select	top 1 @SingleShipmentReportsInOtherShipment = CONVERT(BIT, 1)
  FROM		BatchDistributionCenter bdc
  where		bdc.batchID = @BatchID
  and		bdc.BatchDate  = @BatchDATE
  and		bdc.QspProductLine in (46018) --Cookie Dough
  and		@shipmentGroupID NOT IN (2) --Cookie Dough

  set @SingleShipmentReportsInOtherShipment = isnull(@SingleShipmentReportsInOtherShipment,0)

  Select	@IsBackOrder = CONVERT(BIT, Id)
  From		QSPCanadaOrdermanagement.dbo.ReportRequestBatch 
  Where		BatchOrderId = @OrderID
  And		ShipmentGroupID = @ShipmentGroupID

  SET @IsBackOrder = Isnull(@IsBackOrder,0)

----chk if order completly belongs to third party logistics .i.e unigistix
/* IF ((@Prizes_WH = 2 AND @BHE_WH = 2) OR @Chocolate_WH = 3 OR @CookieDough_WH = 2)
  BEGIN
      Set @IsTPL = 1 
  END*/
  
Set @IsTPL = 0

-- check whether order is already queued, if so then no need to pick it again
/*  Select	@BatchOrderId = BatchOrderId
  From		QSPCanadaOrdermanagement.dbo.ReportRequestBatch 
  Where		BatchOrderId = @OrderID
  And		ShipmentGroupID = @ShipmentGroupID

  SET @BatchOrderId = Isnull(@BatchOrderId,0)  -- important dont exclude
*/
 Select top 1  @Is_Magnet = 1
 From qspcanadacommon.dbo.campaignprogram cp, qspcanadaordermanagement.dbo.batch batch
 where batch.campaignid = cp.campaignid
 and  programid  = 3
 and cp.DeletedTF <> 1 
 and batch.OrderQualifierID in (39001,39002)
 and batch.orderid  = @OrderID 

 Select top 1 @Is_MagExpress = 'T'
 From qspcanadacommon..campaignprogram cp1, qspcanadaordermanagement.dbo.batch batch1
 where batch1.campaignid = cp1.campaignid
 and  programid  = 2 
 and cp1.DeletedTF <> 1 
 and batch1.orderid  = @OrderID

 SET @Is_MagExpress = Isnull(@Is_MagExpress,'F')  -- important, dont exclude

 Select top 1  @Is_Combo = 'T'
 From qspcanadacommon..campaignprogram cp, qspcanadaordermanagement.dbo.batch batch
 where batch.campaignid = cp.campaignid
       and batch.orderid  = @OrderID
       and cp.DeletedTF <> 1 
       and  programid in ( 4,19,20, 32, 40, 44, 49, 50, 52, 53, 54, 55, 56, 58, 59, 62, 64, 65, 67, 69, 72) --Gift,  Gift Program Only, Sweet Sensations, All Occasion, Prize Time, Candles, TRT, Entertainment, Embrace, Festival, Bloom, Kitchen Collection, Naturally Good, Life is Sweet, Travel Mug, Gift Cards

 SET @Is_Combo = Isnull(@Is_Combo,'F')  -- important, dont exclude

 Select top 1 @Is_Planetary = 'T'
 From qspcanadacommon..campaignprogram cp1, qspcanadaordermanagement.dbo.batch batch1
 where batch1.campaignid = cp1.campaignid
 and  programid  in ( 11 , 18,22,29,33, 42, 48)  --Prize Safari/zone added Aug 15, 07 MS
 and cp1.DeletedTF <> 1 
 and batch1.orderid  = @OrderID

 SET @Is_Planetary = Isnull(@Is_Planetary,'F')  -- important, dont exclude

 Select top 1  @Is_Regular_Mag = 'T'
 From qspcanadacommon..campaignprogram cp1, qspcanadaordermanagement.dbo.batch batch1
 where batch1.campaignid = cp1.campaignid
 and  programid  = 1 
 and cp1.DeletedTF <> 1 
 and batch1.orderid  = @OrderID
 and not exists (  Select 1
	      From qspcanadacommon..campaignprogram cp2, qspcanadaordermanagement.dbo.batch batch2
	      where batch2.campaignid = cp2.campaignid
              and batch2.orderid  = @OrderID
	 and cp2.DeletedTF <> 1 
	      and  programid  = 2  ) 
 and not exists (  Select 1
	      From qspcanadacommon..campaignprogram cp2, qspcanadaordermanagement.dbo.batch batch2
	      where batch2.campaignid = cp2.campaignid
              and batch2.orderid  = @OrderID
	 and cp2.DeletedTF <> 1 
	      and  programid  = 4  ) 
 and not exists (  Select 1
	      From qspcanadacommon..campaignprogram cp2, qspcanadaordermanagement.dbo.batch batch2
	      where batch2.campaignid = cp2.campaignid
              and batch2.orderid  = @OrderID
	 and cp2.DeletedTF <> 1 
	      and  programid  = 3  ) 

 SET @Is_Regular_Mag = Isnull(@Is_Regular_Mag,'F')  -- important, dont exclude

SELECT	TOP 1 @CustomerDeliveredOrder = CONVERT(BIT, 1)
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE	b.OrderID = @OrderID
AND		cod.IsShippedToAccount = 0
AND		cod.DistributionCenterID = 1

SET @CustomerDeliveredOrder = ISNULL(@CustomerDeliveredOrder, CONVERT(BIT, 0))

---now determine which orders should not be printed at all---

  IF (@Is_Regular_Mag  =  'T'  )
     BEGIN
--	SET @NO_PRINT = 1
	Set @QSPPrint=1

     END

  IF @OrderQualifier in (39016 ) AND ( @BHE_WH = 1  OR  @Prizes_WH  = 1  )  
       BEGIN
	SET @NO_PRINT = 1
       END
 
 -- IF @OrderQualifier in (39004,39009,39010,39011,39012,39013,39014,39015 )  MS Feb 02, 2006 Issue #45
IF @OrderQualifier in (39004,39007,39010,39011,39013,39014) 
       BEGIN
	SET @NO_PRINT = 1
       END

IF (@OrderQualifier IN  (39006,39007, 39017) )
begin
	SET @NO_PRINT = 0

end

IF @Popcorn_WH = 1
BEGIN
	SET @QSPPrint=1
	SET @NO_PRINT = 0
END

IF (@CookieDough_WH = 1)-- AND ISNULL(@Prizes_WH, 0) <> 2 AND ISNULL(@Gift_WH, 0) <> 2) 
BEGIN
	SET @QSPPrint=1
	SET @NO_PRINT = 0
END

IF @OrderQualifier IN (39009)
BEGIN
	IF @CustomerDeliveredOrder = 1
	BEGIN
		SET @NO_PRINT = 0
	END
	ELSE
	BEGIN
		SET @NO_PRINT = 1
	END
END 

IF (@PretzelRods_WH = 1)
BEGIN
	SET @NO_PRINT = 1
END

IF (@DiscountCardFulfilledByFM = 1)
BEGIN
	SET @NO_PRINT = 1
END

---END NO PRINT-------


--IF ( @BatchOrderId <> @OrderID )   -- if order is not already queued  then pick it and insert the data in queueing tables

BEGIN

IF(@NO_PRINT <> 1)
Begin

	Exec dbo.pr_ins_ReportRequestBatch 
	@BatchOrderId 		= @OrderID
	, @TypeId 			= 1 -- 1 = queued thru dds , 2 = one off report 
	, @CreatedBy 		= @CreatedBY 
	, @ShipmentGroupID	= @ShipmentGroupID
	, @ReportRequestBatchID 	= @RRBID OUTPUT ;
End

--Pick list

IF @NO_PRINT <> 1 AND (@OrderQualifier IN  (39006,39007, 39017,39018,39022) OR (@Prizes_WH = 1 OR @CookieDough_WH = 1 OR @Gift_WH = 1 OR @Popcorn_WH = 1 OR (@CustomerDeliveredOrder = 1 AND @OrderQualifier = 39009))) -- Pick list is printed only for  fs or kanata orders AND NON-TPL  cust svc all TPL
BEGIN
	Exec dbo.pr_ins_ReportRequestBatch_PrintPickList 
	  @ReportRequestBatchID 	= @RRBID
	, @pBatchId 			= @BatchID
	, @pBatchDate 		= @BatchDATE
	, @pReportType 		= 1 --1 is PickList, 2 is Packing Slip
	, @pShipDateFrom 		= null
	, @pShipDateTo 		= null
	, @CreatedBy 		= @CreatedBY ;
END


--packing slip

IF (@OrderQualifier IN  (39008) AND @IsTPL <> 1) --packing slip is only printed for customer service
BEGIN
	Exec dbo.pr_ins_ReportRequestBatch_PrintPickList 
	  @ReportRequestBatchID 	= @RRBID
	, @pBatchId 			= @BatchID
	, @pBatchDate 			= @BatchDATE
	, @pReportType 		= 2 --1 is PickList, 2 is Packing Slip
	, @pShipDateFrom 		= null
	, @pShipDateTo 		= null
	, @CreatedBy 			= @CreatedBY ;
END



IF ( @NO_PRINT <> 1  and  @OrderQualifier not in (39006,39007,39009,39012, 39015, 39017,39018,39020, 39022) AND @Popcorn_WH <> 1 ) -- if order is not a regular magazine or not in cs,kanata,wfs, fc etc then continue with the following reports 
BEGIN

	
	--PROBLEM SOLVER
	IF (@Is_Combo = 'T' OR @CookieDough_WH = 1) and @OrderQualifier in (39001,39002, 39003,39005) and @SingleShipmentReportsInOtherShipment = 0 and @IsBackOrder = 0-- problem solver is printed only for main Combo orders
	BEGIN
		 
		Exec dbo.pr_ins_ReportRequestBatch_ProblemSolver
		@ReportRequestBatchID 	= @RRBID
		, @Lang 			= @Language
		, @pCampaignID 		= @CampaignID
		, @CreatedBy 			= @CreatedBY ;

	END
	
	IF (@IsBackOrder = 0)
	BEGIN
		Exec dbo.pr_ins_ReportRequestBatch_ParticipantListing
		@ReportRequestBatchID 	= @RRBID
		, @Lang 			= @Language
		, @CreatedBy 		= @CreatedBY ;
	END

    IF (@OrderQualifier NOT IN  (39008,39019) ) --following docs are not needed for gift prob solver and cust sevrice 

    BEGIN
	
		IF (@IsBackOrder = 0)
		BEGIN
			Exec dbo.pr_ins_ReportRequestBatch_HomeroomSummary
			@ReportRequestBatchID 	= @RRBID
			, @pBatchId 			= @BatchID
			, @pBatchDate 		= @BatchDATE
			, @CreatedBy 		= @CreatedBY ;
		END

		IF (@SingleShipmentReportsInOtherShipment = 0 AND @IsBackOrder = 0)
		BEGIN
			Exec dbo.pr_ins_ReportRequestBatch_GroupSummary		@ReportRequestBatchID 	= @RRBID
			, @pBatchId 			= @BatchID
			, @pBatchDate 		= @BatchDATE
			, @CreatedBy 		= @CreatedBY ;
		
			CREATE TABLE #OEFUCC
			(
				Classroom VARCHAR(255), TeacherName VARCHAR(255), ParticipantName VARCHAR(255), CustomerBillToInstance INT, SubscriberName VARCHAR(255), PurchaserName VARCHAR(255), BillToCustomerPhone VARCHAR(255), CustomerPhone VARCHAR(255), CustomerAddress1 VARCHAR(255), CustomerAddress2 VARCHAR(255), CustomerCity VARCHAR(255), CustomerState VARCHAR(255), CustomerZip VARCHAR(255), TitleCode VARCHAR(255), MagazineTitle VARCHAR(255), Numberofissues INT, CatalogPrice NUMERIC(16,2), Lang VARCHAR(255), StatusInstance INT
			)
			INSERT INTO #OEFUCC
			EXEC QSPCanadaOrderManagement..pr_OrderEntryFollowupReport_CC @OrderID
			DECLARE @CCErrors INT
			SET @CCErrors = @@ROWCOUNT
			
			CREATE TABLE #OEFUMag
			(
				Classroom VARCHAR(255), TeacherName VARCHAR(255), ParticipantName VARCHAR(255), CustomerBillToInstance INT, StatusInstance INT, SubscriberName VARCHAR(255), PurchaserName VARCHAR(255), CustomerPhone VARCHAR(255), CustomerAddress1 VARCHAR(255), CustomerAddress2 VARCHAR(255), CustomerCity VARCHAR(255), CustomerState VARCHAR(255), CustomerZip VARCHAR(255), TitleCode VARCHAR(255), MagazineTitle VARCHAR(255), Numberofissues INT, CatalogPrice NUMERIC(16,2), Lang VARCHAR(255), CustomerOrderHeaderInstance INT, TransID INT, InvoiceNumber INT, ErrorCategory VARCHAR(255), ErrorCategory_FR VARCHAR(255), ErrorType VARCHAR(255), ErrorType_FR VARCHAR(255)
			)
			INSERT INTO #OEFUMag
			EXEC QSPCanadaOrderManagement..pr_OrderEntryFollowupReport_Mag @OrderID
			DECLARE @MagErrors INT
			SELECT	@MagErrors = COUNT(*)
			FROM	#OEFUMag
			CREATE TABLE #OEFUGift
			(
				Classroom VARCHAR(255), TeacherName VARCHAR(255), ParticipantName VARCHAR(255), CustomerBillToInstance INT, PurchaserName VARCHAR(255), CustomerPhone VARCHAR(255), CatalogPrice NUMERIC(16,2), Quantity INT, Price NUMERIC(16,2), OrderID INT, Lang VARCHAR(255)
			)
			INSERT INTO #OEFUGift
			EXEC QSPCanadaOrderManagement..pr_OrderEntryFollowupReport_Gift @OrderID
			DECLARE @GiftErrors INT
			SET @GiftErrors = @@ROWCOUNT
			DECLARE @OEFUNumRows INT
			SET @OEFUNumRows = ISNULL(@CCErrors,0) + ISNULL(@MagErrors,0) + ISNULL(@GiftErrors,0)
			IF (@OEFUNumRows > 0)
			BEGIN	
				Exec dbo.pr_ins_ReportRequestBatch_OrderEntryFollowup
				@ReportRequestBatchID 	= @RRBID
				, @pAccountID 		= null
				, @pCampaignID 		= null
				, @CreatedBy 		= @CreatedBY ;
			END

			IF (@GiftErrors > 0)
			BEGIN
				DECLARE @Subject VARCHAR(100)
				SET @Subject = 'Gift Items in OEFU Report - OrderID' + str(@OrderID)
                exec QSPCanadaCommon..Send_EMail  'QSPFulf@gafundraising.com','fieldsupport@qsp.ca', @Subject, '', 'Debbie.Cyr@qsp.ca;jmiles@gafundraising.com'
			END
		END
		/*DECLARE	TeacherBoxLabels CURSOR FOR
		SELECT	DISTINCT t.Instance TeacherInstance
		FROM	Batch b
		JOIN	CustomerOrderHeader coh ON coh.OrderBatchID = b.ID and coh.OrderBatchDate = b.Date
		JOIN	Student s ON s.Instance = coh.StudentInstance
		JOIN	Teacher t ON t.Instance = s.TeacherInstance
		WHERE	b.OrderID = @BatchOrderID*/
		
    END

	IF (@OrderQualifier NOT IN (39018, 39019, 39022) AND (@Prizes_WH = 1 OR @CookieDough_WH = 1 OR @Gift_WH = 1))
	BEGIN
		Exec dbo.pr_ins_ReportRequestBatch_TeacherBoxLabels
		@ReportRequestBatchID 	= @RRBID
		, @Lang				= @Language
		, @pTeacherID		= NULL
		, @pTotalLabels		= 2
		, @CreatedBy 		= @CreatedBY ;
	END	
	
END --IF @Is_Regular_Mag <> 'T'


             -- now set the flag for orders to be sent to unigistix
	if @IsStaff <> 1 and (@Is_Planetary = 'T' or @Is_Combo = 'T') and @OrderQualifier not in (39006,39007, 39017 )
	begin
		Select @QSPPrint = 1 
	end

	if @IsStaff <> 1 and  @OrderQualifier in (39008) -- any cust service goes to tpl
	begin
		Select @QSPPrint = 1 
	end

	 -- MS Dec14, 2007 Added Gift PSolver Issue #4009
	If @OrderQualifier in (39019)
	begin
		Select @QSPPrint = 1 
	end

 Update ReportRequestBatch set IsQSPPrint = @QSPPrint Where id=@RRBID    

 IF   (@IsStaff = 1 /*or   @Is_Regular_Mag = 'T'*/) and @OrderQualifier not in (39006,39007,39008, 39017,39018,39019, 39022 ) OR  -- MS Dec14, 2007 Added Gift PSolver Issue #4009
      ((@IsStaff = 1 ) and @OrderQualifier  in (39008)) OR
      ( @Is_Magnet = 1) OR
      ( @OrderQualifier  in (39015,39012) AND @Prizes_WH <> 1 AND @CookieDough_WH <> 1 AND @Gift_WH <> 1)	OR  --MS Feb 02, 2006 Issue #45
      (@OrderQualifier  in (39020) AND @Prizes_WH <> 1 AND @CookieDough_WH <> 1 AND @Gift_WH <> 1)
   Begin
     Update ReportRequestBatch
     Set IsPrinted = 1 , IsQSPPrint=1
     Where id=@RRBID
   End


 END 

/*ELSE -- if order is already queued then return the error message
BEGIN
   Select 'Order is already picked and queued'
END
*/
END
GO
