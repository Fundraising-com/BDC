USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CancelSub]    Script Date: 06/07/2017 09:19:46 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_CancelSub]

	@iCustomerOrderHeaderInstance int,
	@iTransID			int,	
	@sUserID varchar(15) ,
	@dDate datetime = ''

 AS

DECLARE @ProductType INT
SELECT	@ProductType = ProductType
FROM	CustomerOrderDetail
WHERE	CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
AND		TransID = @iTransID

DECLARE @OrderID INT,
		@OrderQualifierID INT,
		@IsShippedToAccount BIT,
		@CampaignID INT
		
SELECT	@OrderID = b.OrderID,
		@OrderQualifierID = b.OrderQualifierID,
		@IsShippedToAccount = cod.IsShippedToAccount,
		@CampaignID = b.CampaignID
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE	coh.Instance = @iCustomerOrderHeaderInstance
AND		cod.TransID = @iTransID

IF @ProductType <> 46001
BEGIN
	UPDATE	CustomerOrderDetail
	SET		StatusInstance = 506
	FROM	CustomerOrderDetail
	WHERE	CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	AND		TransID = @iTransID

	declare @count int
	select	@count=count(*)
	from	QSPCanadaOrderManagement..CustomerOrderDetail ,
			QSPCanadaOrderManagement..CustomerOrderHeader,
			QSPCanadaOrderManagement..Batch
	where  QSPCanadaOrderManagement..CustomerOrderDetail.CustomerOrderHeaderInstance=
				QSPCanadaOrderManagement..CustomerOrderHeader.Instance
			and OrderBatchDate=Date
			and OrderBatchID=id
			and (OrderID = @orderid
					OR (OrderID IN (SELECT DISTINCT OnlineOrderID  
					FROM OnlineOrderMappingTable  
					WHERE LandedOrderID = @OrderID)
					AND IsShippedToAccount = 1))
			and (producttype not in  (46001	,46017, 46012, 46021, 46023, 46024) OR (ProductType = 46024 AND dbo.UDF_Entertainment_IsShippedToAccount(CustomerOrderHeader.Instance) = 1))
			and QSPCanadaOrderManagement..CustomerOrderDetail.StatusInstance not in (500, 501, 506, 508, 513)
						
	if(@count = 0)
	begin
		update batch set statusinstance = 40013 where orderid=@orderID 
	end

	DECLARE @COHcount int
	SELECT	@COHcount = COUNT(*)
	FROM	CustomerOrderDetail cod
	WHERE	cod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	AND		(ProductType not in (46001, 46017, 46012, 46021, 46023, 46024) OR (ProductType = 46024 AND dbo.UDF_Entertainment_IsShippedToAccount(cod.CustomerOrderHeaderInstance) = 1))
	AND		StatusInstance IN (508, 513)
						
	if(@COHcount = 0)
	begin
		UPDATE	CustomerOrderDetail
		SET		StatusInstance = 506
		FROM	CustomerOrderDetail
		WHERE	CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		AND		ProductType IN (46017, 46021) --Processing Fee, Shipping Fee
	end

	IF (@OrderQualifierID = 39009 AND @IsShippedToAccount = 1)
	BEGIN
		SET @OrderID = NULL
	
		SELECT	@OrderID = b.OrderID
		FROM	Batch b
		WHERE	OrderQualifierID = 39001
		AND		CampaignID = @CampaignID
	END

	IF (@count > 0)
	BEGIN
		DECLARE @ReportsGenerated BIT
		SELECT	@ReportsGenerated = 1
		FROM	ReportRequestBatch
		WHERE	BatchOrderID = @OrderID
		
		IF @ReportsGenerated = 1
		BEGIN
			DECLARE @ShipmentGroupID INT

			SELECT	@ShipmentGroupID = pl.ShipmentGroupID
			FROM	QSPCanadaCommon..QSPProductLine pl
			WHERE	pl.ID = @ProductType

			IF @ShipmentGroupID > 0
			BEGIN
				exec pr_Ins_Report_Parameters_V2 @OrderId, -1, @ShipmentGroupID
			END
		END
	END
END
ELSE
BEGIN
	DECLARE 	@iRemitBatchID int,
	     		@iLastRemitBatchID int,
			@iFulfillmentHouseNbr int,
			@iInstance int

	if @dDate = ''
		set @dDate = getdate()


	SET @iRemitBatchID = 0

	--get the lastest remit batch id from this cod
	SELECT @iLastRemitBatchID = MAX(RemitBatchID)
	   FROM CustomerOrderDetailRemitHistory
	WHERE CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance AND
 		 TransID = @iTransID

	print @iLastRemitBatchID

	--get the fulf house nbr for the latest remit batch
	SELECT @iFulfillmentHouseNbr=FulfillmentHouseNbr
	   FROM RemitBatch
	WHERE ID = @iLastRemitBatchID

	print @iFulfillmentHouseNbr

	SELECT @iRemitBatchID=coalesce(MAX(ID),0)
	   FROM RemitBatch
	WHERE Status='42000' AND
		  FulfillmentHouseNbr = @iFulfillmentHouseNbr
		--and ID > @iLastRemitBatchID

	print @iRemitBatchID

		declare @maxinstance int
		create table #temp
		(
			NextInstance int
		)
		

	IF @iRemitBatchID <= 0 
	BEGIN

		
		insert into #temp exec qspcanadaordermanagement..InsertNextInstance 17
		select @maxinstance=nextinstance from #temp
		truncate table #temp
			
		SELECT @iRemitBatchID = @maxinstance

		INSERT INTO RemitBatch (ID, Date, Status, FulfillmentHouseNbr,UserIDChanged) VALUES (@iRemitBatchID, @dDate, '42000', @iFulfillmentHouseNbr,@sUserID)

	END 

	print @iRemitBatchID

	set @maxinstance = 1
	delete from #temp

	insert into #temp exec qspcanadaordermanagement..InsertNextInstance 18
	select @maxinstance=nextinstance from #temp
	truncate table #temp
		
	drop table #temp
	SET @iInstance = @maxinstance

	insert into CustomerRemitHistory 
		(RemitBatchID,
			Instance,
			CustomerInstance,
			StatusInstance,
			LastName,
			FirstName,
			Address1,
			Address2,
			City,
			State,
			Zip,
			ZipPlusFour,
			UserIDModified)

		select  @iRemitBatchID,
			@iInstance,
			CustomerInstance,
			'42002',
			LastName,
			FirstName,
			Address1,
			Address2,
			City,
			State,
			Zip,
			ZipPlusFour,
			@sUserID as UserIDModified
		
		 from customerremithistory 
		where --customerinstance = @iCustomerOrderHeaderInstance AND 
			 instance = (select max(crh.instance) from customerremithistory crh, customerorderdetailremithistory codrh
					 where 	crh.instance = codrh.customerremithistoryinstance 
						and codrh.customerorderheaderinstance=@iCustomerOrderHeaderInstance 
						and codrh.transid = @iTransID)



	INSERT INTO CustomerOrderDetailRemitHistory        
		(CustomerOrderHeaderInstance,
		TransID,
		RemitBatchID,
		CountryCode,
		CustomerRemitHistoryInstance,
		Status,
		Quantity,
		RemitRate,
		BasePrice,
		CurrencyID,
		Lang,
		PremiumIndicator,
		PremiumCode,
		PremiumDescription,
		ABCCode,
		Renewal,
		TitleCode,
		MagazineTitle,
		CatalogPrice,
		ItemPriceTotal,
		NumberOfIssues,
		DefaultGrossValue,
		Comment,
		SwitchLetterBatchID,
		GiftOrderType,
		GiftOrderStatus,
		GiftCardDateGenerated,
		SupporterName,
		DateChanged,
		EffortKey,
		UserIdChanged)
		SELECT TOP 1 CustomerOrderHeaderInstance,
			TransID,
			@iRemitBatchID,
			CountryCode,
			@iInstance,
			'42002' AS Status,
			Quantity,
			RemitRate,
			BasePrice,
			CurrencyID,
			Lang,
			PremiumIndicator,
			PremiumCode,
			PremiumDescription,
			ABCCode,
			Renewal,
			TitleCode,
			MagazineTitle,
			CatalogPrice,
			ItemPriceTotal,
			NumberOfIssues,
			DefaultGrossValue,
			Comment,
			SwitchLetterBatchID,
			GiftOrderType,
			GiftOrderStatus,
			GiftCardDateGenerated,
			SupporterName,
			@dDate AS DateChanged,
			EffortKey,
			@sUserID as UserIDChanged
			   FROM CustomerOrderDetailRemitHistory
			WHERE  CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance AND
	 			  TransID = @iTransID AND
				  RemitBatchID = @iLastRemitBatchID
END
GO
