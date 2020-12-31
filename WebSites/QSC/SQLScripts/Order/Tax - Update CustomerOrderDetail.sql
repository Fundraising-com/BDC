USE QSPCanadaOrderManagement
GO

SELECT	CustomerOrderHeaderInstance,
		TransID
INTO	#SubsToChange
FROM	CustomerOrderDetail
WHERE	CustomerOrderHeaderInstance = 13897178
AND		TransID = 1

BEGIN TRAN

--Cursor
DECLARE @CustomerOrderHeaderInstance [int],
		@TransID [int]

DECLARE	info CURSOR FOR
SELECT	CustomerOrderHeaderInstance, TransID
FROM	#SubsToChange

OPEN info
FETCH NEXT FROM info  INTO  @CustomerOrderHeaderInstance,
							@TransID

WHILE(@@fetch_status = 0)
BEGIN

	CREATE TABLE #taxes
	(
		Tax1				NUMERIC(14, 6),
		Tax2				NUMERIC(14, 6),
		Gross				NUMERIC(14, 6),
		Net					NUMERIC(14, 6),
		GroupProfitAmount	NUMERIC(14, 6)
	)

	DECLARE @ProgramSectionID	INT,
			@PricingDetailsID	INT,
			@ProductCode		VARCHAR(20)

	SELECT	@ProgramSectionID = pd.ProgramSectionID,
			@PricingDetailsID = pd.MagPrice_Instance,
			@ProductCode = prod.Product_Code
	FROM	QSPCanadaProduct..Pricing_Details pd
	JOIN	QSPCanadaProduct..Product prod
				ON	prod.Product_Instance = pd.Product_Instance
	JOIN	CustomerOrderDetail cod
				ON	cod.PricingDetailsID = pd.MagPrice_Instance
	WHERE	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
	AND		cod.TransID = @TransID

	DECLARE	@OrderDateStr			VARCHAR(20),
			@Tax					NUMERIC(14, 6),
			@TaxA					NUMERIC(14, 6),
			@Net					NUMERIC(14, 6),
			@Gross					NUMERIC(14, 6),
			@GroupProfitAmount		NUMERIC(14, 6),
			@CampaignID				INT,
			@Province				VARCHAR(2),
			@customershiptoinstance INT,
			@PaidPrice				NUMERIC(10, 2)
			

	SELECT	@OrderDateStr			= CONVERT(VARCHAR(20), coh.OrderBatchDate, 101),
			@CampaignID				= camp.ID,
			@customershiptoinstance = cod.CustomerShipToInstance,
			@PaidPrice = cod.Price
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
		
	INSERT INTO #Taxes
	EXEC QSPCanadaCommon..pr_Calc_Order_Item_Amounts
			@OrderDateStr, @PaidPrice, @ProgramSectionID, 
			@ProductCode, 'N', @CampaignID, @PricingDetailsID, @Province, @CustomerOrderHeaderInstance

	SELECT	@Tax = Tax1,
			@TaxA = Tax2,
			@Net = Net,
			@Gross = Gross,
			@GroupProfitAmount = GroupProfitAmount
	FROM	#Taxes
	
	DROP TABLE #Taxes

	UPDATE	CustomerOrderDetail
	SET		Tax = @Tax,
			TaxA = @Tax,
			Tax2 = @TaxA,
			Tax2A = @TaxA,
			Net = @Net,
			Gross = @Gross,
			GroupProfitAmount = @GroupProfitAmount
	WHERE	CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
	AND		TransID = @TransID

	FETCH NEXT FROM info  INTO  @CustomerOrderHeaderInstance,
								@TransID

END
CLOSE info
DEALLOCATE info

COMMIT TRAN
