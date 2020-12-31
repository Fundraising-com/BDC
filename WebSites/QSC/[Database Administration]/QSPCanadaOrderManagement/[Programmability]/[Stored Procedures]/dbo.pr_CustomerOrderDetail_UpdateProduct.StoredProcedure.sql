USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CustomerOrderDetail_UpdateProduct]    Script Date: 06/07/2017 09:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_CustomerOrderDetail_UpdateProduct]

	@ProductPriceInstance			INT,
	@CustomerOrderHeaderInstance	INT,
	@TransID						INT,
	@CloseOrder						BIT = 1

AS

DECLARE @ProductName		VARCHAR(50),
		@Quantity			INT,
		@CatalogPrice		NUMERIC(10, 2),
		@ProgramSectionID	INT,
		@PricingDetailsID	INT,
		@ProductType		INT,
		@ProductCode		VARCHAR(20),
		@OrderQualifierID	INT

SELECT	@ProductName = prod.Product_Sort_Name,
		@Quantity = ISNULL(CASE pd.Nbr_Of_Issues WHEN 0 THEN 1 ELSE pd.Nbr_of_Issues END, 1),
		@CatalogPrice = pd.QSP_Price,
		@ProgramSectionID = pd.ProgramSectionID,
		@PricingDetailsID = pd.MagPrice_Instance,
		@ProductType = prod.Type,
		@ProductCode = prod.Product_Code
FROM	QSPCanadaProduct..Pricing_Details pd
JOIN	QSPCanadaProduct..Product prod
			ON	prod.Product_Instance = pd.Product_Instance
WHERE	pd.MagPrice_Instance = @ProductPriceInstance

UPDATE	cod
SET		cod.ProductName = @ProductName,
		cod.Quantity = @Quantity,
		cod.CatalogPrice = @CatalogPrice,
		cod.ProgramSectionID = @ProgramSectionID,
		cod.PriceOverrideID = 45002, --45002: Invalid Price
		cod.PricingDetailsID = @PricingDetailsID,
		cod.ProductType = @ProductType,
		cod.ProductCode = @ProductCode,
		cod.Price = @CatalogPrice --COD hasn't been invoiced yet, so update to correct price
FROM	CustomerOrderDetail cod
WHERE	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
AND		cod.TransID = @TransID

CREATE TABLE #taxes
(
	Tax1	NUMERIC(14, 6),
	Tax2	NUMERIC(14, 6),
	Gross	NUMERIC(14, 6),
	Net		NUMERIC(14, 6),
	groupprofitamount money
)

DECLARE	@OrderDateStr			VARCHAR(20),
		@Tax					NUMERIC(14, 6),
		@TaxA					NUMERIC(14, 6),
		@Net					NUMERIC(14, 6),
		@Gross					NUMERIC(14, 6),
		@AccountID				INT,
		@CampaignID				INT,
		@Province				VARCHAR(2),
		@OrderID				INT,
		@customershiptoinstance INT,
		@IsShippedToAccount		BIT

SELECT	@OrderDateStr			= CONVERT(VARCHAR(20), coh.OrderBatchDate, 101),
		@AccountID				= camp.ShipToAccountID,
		@CampaignID				= camp.ID,
		@OrderID				= b.OrderID,
		@OrderQualifierID		= b.OrderqualifierID,
		@customershiptoinstance = cod.CustomerShipToInstance,
		@IsShippedToAccount		= cod.IsShippedToAccount
FROM	CustomerOrderDetail cod
JOIN	CustomerOrderHeader coh
			ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN	Batch b
			ON	b.ID = coh.OrderBatchID
			AND	b.Date = coh.OrderBatchDate
JOIN	QSPCanadaCommon..Campaign camp
			ON	camp.ID = b.CampaignID
WHERE	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
AND		cod.TransID = @TransID

/*SELECT	@Province = addr.StateProvince
FROM	QSPCanadacommon..CAccount acc
JOIN	QSPCanadacommon..Address addr
			ON	addr.AddressListID = acc.AddressListID
WHERE	addr.Address_Type = 54002 --54002: Bill To
AND		acc.ID = @AccountID*/

IF (@customershiptoinstance > 0)
BEGIN
	SELECT	@Province = State
	FROM	Customer
	WHERE	Instance = @customershiptoinstance
END

IF (ISNULL(@Province, '') = '')
BEGIN
	SELECT	@Province = State
	FROM	CustomerOrderHeader coh
	JOIN	Customer cust
				ON	cust.Instance = coh.CustomerBillToInstance
	WHERE	coh.Instance = @CustomerOrderHeaderInstance
END

IF (ISNULL(@Province, '') = '' OR @IsShippedToAccount = 1)
BEGIN
	select	@Province = addr.StateProvince
	from	CustomerOrderHeader coh,
			QSPCanadaCommon..CAccount a,
			QSPCanadaCommon..Address addr
	where	a.ID = coh.AccountID
	and	addr.AddressListID = a.AddressListID
	and	addr.Address_Type = 54002
	and	coh.Instance = @CustomerOrderHeaderInstance
END

INSERT INTO #Taxes
EXEC QSPCanadaCommon..pr_Calc_Order_Item_Amounts
		@OrderDateStr, @CatalogPrice, @ProgramSectionID, 
		@ProductCode, 'N', @CampaignID, @PricingDetailsID, @Province, @CustomerOrderHeaderInstance

SELECT	@Tax = Tax1,
		@TaxA = Tax2,
		@Net = Net,
		@Gross = Gross
FROM	#Taxes
	
DROP TABLE #Taxes

DECLARE	@CCBad BIT
SELECT	@CCBad = CONVERT(BIT, COUNT(cp.CustomerPaymentHeaderInstance))
FROM	CreditCardPayment cp
JOIN	CustomerPaymentHeader ph
			ON	ph.Instance = cp.CustomerPaymentHeaderInstance
JOIN	CustomerOrderHeader coh
			ON	coh.Instance = ph.CustomerOrderHeaderInstance
JOIN	Batch batch
			ON	batch.ID = coh.OrderBatchID
			AND	batch.Date = coh.OrderBatchDate
WHERE	coh.Instance = @CustomerOrderHeaderInstance
AND		coh.PaymentMethodInstance IN (50003, 50004, 50005)
AND		cp.StatusInstance IN (19001, 19002, 19005)
AND		batch.OrderQualifierID NOT IN (39009) --Assume Internet CC's are good

IF @CCBad = NULL
	SET @CCBad = 0

UPDATE	CustomerOrderDetail
SET		Tax = @Tax,
		TaxA = @Tax,
		Tax2 = @TaxA,
		Tax2A = @TaxA,
		Net = @Net,
		Gross = @Gross
		--StatusInstance = CASE @CCBad WHEN 1 THEN StatusInstance ELSE 502 END
WHERE	CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
AND		TransID = @TransID

/*IF (@CloseOrder = 0 OR @CCBad = 1)
BEGIN
	UPDATE	ReportRequestBatch_OrderEntryFollowupReport
	SET		CreateDate = GETDATE(),
			QUEUEDATE = NULL,
			RUNDATESTART = NULL,
			[FILENAME] = NULL 
	WHERE	ReportRequestBatchID = (SELECT	ID
									FROM	ReportRequestBatch
									WHERE	BatchOrderID = @OrderID)
END

IF (@CloseOrder = 1 AND @CCBad = 0)
BEGIN
	
	DECLARE @IsOrderOpen BIT

	SELECT	@IsOrderOpen = 1
	FROM	CustomerOrderDetail cod
	JOIN	CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
	JOIN	Batch b
				ON	b.ID = coh.OrderBatchID
				AND	b.Date = coh.OrderBatchDate
	WHERE	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
	AND		cod.TransID = @TransID
	AND		(b.StatusInstance IN (40002, 40003))
	
	IF @IsOrderOpen = 1
		EXEC pr_CloseOrder @OrderID
	ELSE
	BEGIN
		DECLARE	@remitstatus int
		EXEC	spRemitIndividualItem @customerorderheaderinstance = @CustomerOrderHeaderInstance, @transid = @TransID, @remitstatus = @remitstatus OUTPUT
		
		UPDATE	ReportRequestBatch_OrderEntryFollowupReport
		SET		CreateDate = GETDATE(),
				QUEUEDATE = NULL,
				RUNDATESTART = NULL,
				[FILENAME] = NULL 
		WHERE	ReportRequestBatchID = (SELECT	ID
										FROM	ReportRequestBatch
										WHERE	BatchOrderID = @OrderID)
	END
END
*/
GO
