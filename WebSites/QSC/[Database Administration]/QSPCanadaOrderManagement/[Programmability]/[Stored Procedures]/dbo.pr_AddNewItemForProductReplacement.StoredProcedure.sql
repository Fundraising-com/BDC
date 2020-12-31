USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_AddNewItemForProductReplacement]    Script Date: 06/07/2017 09:19:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_AddNewItemForProductReplacement]

	@iCustomerOrderHeaderInstance	int,
	@iMagPriceInstance		int,
	@iQuantity			int,
	@fPrice				float = 0,
	@iOverrideCode			int,
	@iReasonId				int

AS

	SET NOCOUNT ON

	DECLARE @zProductCode	varchar(20)
	DECLARE @iProgramSectionID	int
	DECLARE @fCatalogPrice	float
	DECLARE @iProductType	int
	DECLARE @dToday		smalldatetime
	DECLARE @zFirstName		varchar(50)
	DECLARE @zLastName		varchar(50)
	DECLARE @iOrderID		int
	DECLARE @fTotalPrice		float


	SELECT		@zProductCode = p.Product_Code,
				@iProgramSectionID = pd.ProgramSectionID,
				@fCatalogPrice = pd.QSP_Price,
				@iProductType = p.Type
	FROM			QSPCanadaProduct..Pricing_Details pd,
				QSPCanadaProduct..Product p
	WHERE		p.Product_Instance = pd.Product_Instance
	AND			pd.MagPrice_Instance = @iMagPriceInstance

	SELECT @dToday = GETDATE()

	SET @zFirstName = 'Item'
	SET @zLastName = 'Replacement'

	SELECT		@iOrderID = b.OrderID
	FROM			Batch b,
				CustomerOrderHeader coh
	WHERE		coh.OrderBatchID = b.ID
	AND			coh.OrderBatchDate = b.Date
	AND			coh.Instance = @iCustomerOrderHeaderInstance

	SET		@fTotalPrice = @fPrice * @iQuantity

	EXEC	CreateDetailItem
		@dToday,
		@iCustomerOrderHeaderInstance,
		@zProductCode ,
		@zFirstName,
		@zLastName,
		@iQuantity ,
		@fTotalPrice,
		@iProgramSectionID,
		@fCatalogPrice,
		0,
		@iProductType,
		@iMagPriceInstance,
		502,
		0

	UPDATE	Batch 
	SET		EnterredCount = EnterredCount + @iQuantity,
			EnterredAmount = EnterredAmount + @fTotalPrice,
			CalculatedAmount = CalculatedAmount + @fTotalPrice,
			OrderDetailCount = OrderDetailCount + 1
	WHERE	OrderID = @iOrderID

	UPDATE	CustomerOrderDetail
	SET		Renewal = 'N',
			PriceOverrideID = @iOverrideCode,
			ProductReplacementReasonID = @iReasonId
	WHERE	CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	AND		PricingDetailsID = @iMagPriceInstance

	SET NOCOUNT OFF
GO
