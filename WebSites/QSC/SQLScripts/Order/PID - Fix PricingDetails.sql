--Get List of Subs
SELECT	coh.CampaignID, b.OrderQualifierID, b.OrderID, cod.CustomerOrderHeaderInstance, cod.TransID, cod.ProductCode, cod.Quantity
INTO	#SubsToChange
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE	b.OrderID IN (919170)
AND		ISNULL(cod.PricingDetailsID,0) = 0

BEGIN TRAN

--Cursor
DECLARE @CustomerOrderHeaderInstance [int],
		@TransID [int],
		@CampaignID [int],
		@ProductCode [varchar](20),
		@Quantity [int],
		@OrderQualifierID [int]

DECLARE	info CURSOR FOR
SELECT	CustomerOrderHeaderInstance, TransID, CampaignID, ProductCode, Quantity, OrderQualifierID
FROM	#SubsToChange

OPEN info
FETCH NEXT FROM info  INTO  @CustomerOrderHeaderInstance,
							@TransID,
							@CampaignID,
							@ProductCode,
							@Quantity,
							@OrderQualifierID

WHILE(@@fetch_status = 0)
BEGIN

	IF (@OrderQualifierID = 39009)
	BEGIN
		print 'needtofix'
		--Todo fix this
		/*DECLARE @NewProductCode [varchar](20)
		EXEC	spGetProductCodeFromRemitCodeAndTerm
				@zRemitCode = @ProductCode,
				@iTerm = @Quantity,
				@iCampaignID = @CampaignID,
				@ProductCode = @NewProductCode OUTPUT*/
	END
	
	--Get tax region
	DECLARE @TaxRegionId	Int
	SELECT	@TaxRegionId = TaxRegionId
	FROM	CustomerOrderHeader coh
	JOIN	QSPCanadaCommon..Campaign c
				ON	c.ID = coh.CampaignID
				AND	c.ID = @CampaignID
	JOIN	QSPCanadaCommon..CAccount a
				ON	a.ID = c.ShipToAccountID
	JOIN	QSPCanadaCommon..Address [add]
				ON [add].AddressListId = a.AddressListID
				AND	[add].Address_Type = 54001 --ShipTo
	JOIN	QSPCanadaCommon..TaxRegionProvince t
				ON	t.Province = [add].StateProvince

	DECLARE @PricingDetailsID INT
	
	SELECT	@PricingDetailsID = pd.MagPrice_Instance
	FROM	CustomerOrderDetail cod
	JOIN	QSPCanadaProduct..Pricing_Details pd
				ON	pd.Product_Code = cod.ProductCode
				AND	pd.QSP_Price = cod.Price
				AND	pd.TaxRegionID = @TaxRegionId
				AND	pd.Pricing_Year = 2014
				AND	pd.Pricing_Season = 'S'
	JOIN	QSPCanadaProduct..Product p
				ON	p.Product_Instance = pd.Product_Instance
	WHERE	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
	AND		cod.TransID = @TransID
	
	EXEC pr_CustomerOrderDetail_UpdateProduct
		@ProductPriceInstance = @PricingDetailsID,
		@CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance,
		@TransID = @TransID,
		@CloseOrder = 0

	--Get next record
	FETCH NEXT FROM info  INTO  @CustomerOrderHeaderInstance,
								@TransID,
								@CampaignID,
								@ProductCode,
								@Quantity,
								@OrderQualifierID
END
CLOSE info
DEALLOCATE info

COMMIT TRAN

/*
SELECT	cod.*, pd.*,p.*
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN	QSPCanadaProduct..Pricing_Details pd
			ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN	QSPCanadaProduct..Product p
			ON	p.Product_Instance = pd.Product_Instance
WHERE	b.OrderID = 919170
order by cod.statusinstance
*/

EXEC pr_CloseOrder 919170

DECLARE @OrderID INT
SET	@OrderID = 919170

EXEC pr_cleanprintqueue @OrderID
EXEC pr_Ins_Report_Parameters_V2 @OrderID, -1

DROP TABLE #SubsToChange
