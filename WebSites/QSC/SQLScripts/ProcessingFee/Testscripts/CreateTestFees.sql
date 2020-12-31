DECLARE @iCustomerOrderHeaderInstance int;
DECLARE @zProcessingFeeProductCode varchar(20);
DECLARE @iProductType int;
DECLARE @iProgramSectionID int;
DECLARE @iPricingDetailsID int;
DECLARE @zComment varchar(255)
DECLARE @iProgramSectionTypeID int;

/* 	TODO: Change this value if the product line that is inserted as configuration data is not 46017, 
	or the product code selected is not PFEE. A lot of the script relies on these 2 values to affect 
	the correct data */
SET @iProductType = 46017;
SET @zProcessingFeeProductCode = 'PFEE';
SET @zComment = 'Processing fee for tests';

--	Recover the other values created by CreateProductFee.sql
SET @iPricingDetailsID = (SELECT TOP 1 MagPrice_Instance from QSPCanadaProduct..Pricing_Details WHERE Product_Code = @zProcessingFeeProductCode);
SET @iProgramSectionID = (SELECT TOP 1 ProgramSectionID from QSPCanadaProduct..Pricing_Details WHERE Product_Code = @zProcessingFeeProductCode);
SET @iProgramSectionTypeID = (SELECT PS.Type FROM QSPCanadaProduct..ProgramSection PS WHERE ID = @iProgramSectionID);

--	Read all orders that have been imported since June 1st (not enough data since July 1st)
--	that are either Main or supplement orders - landed orders
DECLARE cOrderHeaderCursor cursor for 
	SELECT Instance
	FROM [QSPCanadaOrderManagement]..[CustomerOrderHeader] AS COH INNER JOIN [QSPCanadaOrderManagement]..Batch AS Batch ON COH.OrderBatchID = Batch.ID AND COH.OrderBatchDate = Batch.Date
	WHERE Batch.Date >= '2011-06-01' AND Batch.OrderQualifierId IN (39001,39002)
	
Open cOrderHeaderCursor;
fetch NEXT from cOrderHeaderCursor into @iCustomerOrderHeaderInstance;

while @@fetch_status = 0
BEGIN
	DECLARE @iTransId int;
	DECLARE @fTax1Amount numeric (14,6)
	DECLARE @fTax2Amount numeric (14,6)
	DECLARE @fNetAmount numeric(14,6)
	DECLARE @fTax1Rate numeric(4,2)
	DECLARE @fTax2rate numeric(4,2)
	DECLARE @fProcessingFeeBase numeric (14,6)
	DECLARE @fProcessingFeeTax1 numeric (14,6)
	DECLARE @fProcessingFeeTax2 numeric (14,6)
	DECLARE @iCampaignID int
	DECLARE @TaxOnTax bit
	DECLARE @TaxID int
	DECLARE @TaxRate numeric(4,2)
	DECLARE @tCustomerOrderDetailsData table
	(
		CustomerShipToInstance int null,
		StatusInstance int null,
		Renewal varchar(1) null,
		Recipient varchar(81) null,
		OverrideProduct bit null,
		CreationDate datetime null,
		CrossedBridgeDate datetime null,
		CouponPage varchar(2) null,
		FDIndicator varchar(1) null,
		MktgIndicator varchar(10) null,
		ToteInstance int null,
		SupporterName varchar(81) null
	);
	DECLARE @TempTax table
	(
		TaxID int,
		TaxRate numeric(10,2)
	);
	
	-- Retrieve the latest transId for the current COH, and increment by one to be sure to have a new transId
	SET @iTransId = (SELECT MAX(transID) from [QSPCanadaOrderManagement]..[CustomerOrderDetail] WHERE CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance);
	SET @iTransId = @iTransId + 1;
	
	/* Retrieve information that is common to all COD 
	   If order did not have any COD, no insertion will be made here, so no insertion in table */
	INSERT INTO @tCustomerOrderDetailsData
	SELECT TOP 1 CustomerShipToInstance, StatusInstance, Renewal, Recipient, OverrideProduct, CreationDate, CrossedBridgeDate, CouponPage,
	FDIndicator, MktgIndicator, ToteInstance, SupporterName
	FROM  [QSPCanadaOrderManagement]..[CustomerOrderDetail]
	WHERE CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	
	/* Calculate taxes */
	SET @fTax1Rate = 0
	SET @fTax2Rate = 0
	SET @fTax1Amount = 0
	SET @fTax2Amount = 0
	SET @fNetAmount = 0
	SET @fTax1Rate = 0
	SET @fTax2rate = 0
	SET @fProcessingFeeBase = 0
	SET @fProcessingFeeTax1 = 0
	SET @fProcessingFeeTax2 = 0
	SET @TaxOnTax = 0
	
	SET @iCampaignID = (SELECT campaignID FROM QSPCanadaOrderManagement..CustomerOrderHEADER Where instance = @iCustomerOrderHeaderInstance);
	

	SET @fProcessingFeeBase = 0
	SET @fProcessingFeeTax1 = 0
	SET @fProcessingFeeTax2 = 0
	
	--Get the tax rate and id for this campaign
	--May not be valid, as there is no tax charged for processing fee, so taxes may not be involved until invoicing / post to GL
	--If taxes are not involved, then this section can be removed, @fProcessingFeeTax1 and @fProcessingFeeTax2 can be set to 0
	--and @fProcessingFeeBase set to 1
	INSERT @TempTax
	EXEC QSPCanadaCommon..GetTaxRateAndIDForCampaign @iCampaignID, @iProgramSectionTypeID
	
		
	WHILE EXISTS (SELECT DISTINCT TOP 1 TaxID, TaxRate FROM @TempTax)
		BEGIN 
			SELECT @TaxID = TaxID, 
					@TaxRate = TaxRate / 100
			FROM @TempTax
			IF (@TaxID = 3 or @TaxID = 7 or @TaxID = 8 or @TaxID = 9 ) 
				BEGIN
					SET @fTax2rate = @TaxRate
					if @TaxID = 3 OR @TaxID = 7 
						SET @TaxOnTax = 1
				END
			ELSE
				SET @fTax1Rate = @TaxRate
			DELETE FROM @TempTax WHERE TaxID = @TaxID						
		END
	DELETE @TempTax
	
	if @TaxOnTax = 1
		BEGIN
			SET @fProcessingFeeBase = round(1/(1+@fTax2rate)/(1+@fTax1Rate),2)
			SET @fProcessingFeeTax1 = round(@fTax1Rate * @fProcessingFeeBase, 2)
			SET @fProcessingFeeTax2 = round(@fTax2Rate * (@fProcessingFeeBase + @fProcessingFeeTax1), 2)
		END
	else
		BEGIN
			SET @fProcessingFeeBase = round(1/(1+@fTax1Rate+@fTax2rate),2)
			SET @fProcessingFeeTax1 = round(@fTax1Rate * @fProcessingFeeBase, 2)
			SET @fProcessingFeeTax2 = round(@fTax2Rate * @fProcessingFeeBase, 2)
		END
		
	/* Recalculate base processing fee to account for rounding. Otherwise, we get cases with 13% tax 
	   where base amount = $0.88 and $0.88 * 13% = $0.11, so total for both is $0.99 rather than $1 */
	SET @fProcessingFeeBase = 1 - @fProcessingFeeTax1 - @fProcessingFeeTax2
	
	INSERT INTO [QSPCanadaOrderManagement]..[CustomerOrderDetail]
	SELECT 
		@iCustomerOrderHeaderInstance,		--CustomerOrderHeaderInstance
		@iTransId,							--TransID
		CustomerShipToInstance,				--CustomerShipToInstance, from Order
		@zProcessingFeeProductCode,			--ProductCode
		'Processing fee',					--ProductName
		1,									--Quantity
		1,									--Price
        0,									--PriceA
        @fProcessingFeeTax1,				--Tax
        0,									--TaxA
        500,								--StatusInstance (Order Detail good)
        0,									--DelFlag
        renewal,							--Renewal, from Order
        Recipient,							--Recipient, from Order
        OverrideProduct,					--OverrideProduct, from Order
        GetDate(),							--CreationDate (Now)
        CrossedBridgeDate,					--CrossedBridgeDate, from Order
        'ADMI',								--ChangeUserID
        GetDate(),							--ChangeDate
		0,									--InvoiceNumber (Not yet invoiced)
        NULL,								--AlphaProductCode
        CouponPage,							--CouponPage, from Order
        FDIndicator,						--FDIndicator, from Order
        MktgIndicator,						--MktgIndicator, from Order
        ToteInstance,						--ToteInstance, from Order
        NULL,								--GiftCD
        0,									--IsGift
        0,									--IsGiftCardSent
        '1995-01-01',						--SendGiftCardBeforeDate
        @iProgramSectionID,					--ProgramSectionID
        1,									--CatalogPrice
        0,									--QuantityReserved
        45004,								--PriceOverrideID (No Price Override)
        @iProductType,						--ProductType
        @iPricingDetailsID,					--PricingDetailsID
        @fProcessingFeeTax2,				--Tax2
        0,									--Tax2A
        @fProcessingFeeBase,				--Net (price without taxes)
        @fProcessingFeeBase + @fProcessingFeeTax1 + @fProcessingFeeTax2,	--Gross (price with taxes)
        SupporterName,						--SupporterName
        0,									--SendGiftCard
        0,									--QuantityShipped
        0,									--ShipmentID
        NULL,								--ReplacedProductCode
        0,									--ReplacedProductQty
        NULL,								--DistributionCenterID
        @zComment,							--Comment
        NULL								--CustomerComment
	FROM @tCustomerOrderDetailsData;

	/* Update CustomerOrderHeader to reset the next cod item trans #, as long as there was an actual insert performed (if the order previously had no CODs, then no insert was performed) */
	if (SELECT Count(*) FROM @tCustomerOrderDetailsData) > 0
	BEGIN
		Update [QSPCanadaOrderManagement]..[CustomerOrderHeader]
		SET NextDetailTransID = @iTransId + 1
		WHERE  Instance = @iCustomerOrderHeaderInstance
	END

	DELETE @tCustomerOrderDetailsData;

	fetch NEXT from cOrderHeaderCursor into @iCustomerOrderHeaderInstance;
END

CLOSE cOrderHeaderCursor
DEALLOCATE cOrderHeaderCursor

/* Select all inserted data to view it and confirm results */
SELECT * FROM [QSPCanadaOrderManagement]..[CustomerOrderDetail] WHERE ProductCode = @zProcessingFeeProductCode;


GO


