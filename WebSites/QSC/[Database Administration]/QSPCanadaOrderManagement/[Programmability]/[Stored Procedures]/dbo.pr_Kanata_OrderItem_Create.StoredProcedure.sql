USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Kanata_OrderItem_Create]    Script Date: 06/07/2017 09:20:12 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Kanata_OrderItem_Create]

	@iCustomerOrderHeaderInstance	int,
	@iMagPriceInstance				int,
	@iQuantity						int,
	@fPrice							float = 0,
	@iOverrideCode					int,
	@iShipToInstance				int,
	@zShipToFirstName				varchar(50),
	@zShipToLastName				varchar(50)

AS

	SET NOCOUNT ON

	DECLARE @zProductCode		varchar(20)
	DECLARE @iProgramSectionID	int
	DECLARE @fCatalogPrice		float
	DECLARE @iProductType		int
	DECLARE @dToday				smalldatetime
	DECLARE @iOrderID			int
	DECLARE @fTotalPrice		float

	SELECT		@zProductCode = p.Product_Code,
				@iProgramSectionID = pd.ProgramSectionID,
				@fCatalogPrice = pd.QSP_Price,
				@iProductType = p.Type
	FROM		QSPCanadaProduct..Pricing_Details pd,
				QSPCanadaProduct..Product p
	WHERE		p.Product_Instance = pd.Product_Instance
	AND			pd.MagPrice_Instance = @iMagPriceInstance

	SELECT @dToday = GETDATE()

	SELECT		@iOrderID = b.OrderID
	FROM		Batch b,
				CustomerOrderHeader coh
	WHERE		coh.OrderBatchID = b.ID
	AND			coh.OrderBatchDate = b.Date
	AND			coh.Instance = @iCustomerOrderHeaderInstance

	SET		@fTotalPrice = @fPrice * @iQuantity

	EXEC	CreateDetailItem
			@dToday,
			@iCustomerOrderHeaderInstance,
			@zProductCode ,
			@zShipToFirstName,
			@zShipToLastName,
			@iQuantity ,
			@fTotalPrice,
			@iProgramSectionID,
			@fCatalogPrice,
			0,
			@iProductType,
			@iMagPriceInstance,
			502,
			@iShipToInstance

	UPDATE	Batch 
	SET		EnterredCount = EnterredCount + @iQuantity,
			EnterredAmount = EnterredAmount + @fTotalPrice,
			CalculatedAmount = CalculatedAmount + @fTotalPrice,
			OrderDetailCount = OrderDetailCount + 1
	WHERE	OrderID = @iOrderID

	UPDATE	CustomerOrderDetail
	SET		Renewal = 'N',
			PriceOverrideID = @iOverrideCode
	WHERE	CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	AND		PricingDetailsID = @iMagPriceInstance

	SET NOCOUNT OFF
GO
