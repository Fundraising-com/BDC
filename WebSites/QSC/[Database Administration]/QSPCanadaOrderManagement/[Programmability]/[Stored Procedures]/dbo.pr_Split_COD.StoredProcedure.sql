USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Split_COD]    Script Date: 06/07/2017 09:20:37 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Split_COD]

@CustomerOrderHeaderInstance int
, @TransId int
, @SplitQuantity int
, @ModifiedBy varchar(50)

AS

BEGIN TRAN T1

---- CALCULATE DIFFERENCE
DECLARE @RevisedQty int, @OriginalQty int
DECLARE @NewTransId int
DECLARE @HighestTransId int
DECLARE @OriginalQuantityReserved int, @NewQuantityReserved int, @RevisedQuantityReserved int
DECLARE @OriginalTax money, @NewTax money, @RevisedTax money
DECLARE @OriginalTaxA money, @NewTaxA money, @RevisedTaxA money
DECLARE @OriginalTax2 money, @NewTax2 money, @RevisedTax2 money
DECLARE @OriginalTax2A money, @NewTax2A money, @RevisedTax2A money
DECLARE @OriginalNet money, @NewNet money, @RevisedNet money
DECLARE @OriginalGross money, @NewGross money, @RevisedGross money



SELECT 
	@OriginalQty = Quantity
	, @OriginalTax = Tax
	, @OriginalTaxA = TaxA
	, @OriginalTax2 = Tax2
	, @OriginalTax2A = Tax2A 
	, @OriginalNet = Net
	, @OriginalGross = Gross
	, @OriginalQuantityReserved = QuantityReserved
FROM 
	CustomerOrderDetail 
WHERE 
	CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance 
	AND TransId = @TransId


SET @RevisedQty = @OriginalQty - @SplitQuantity

IF @OriginalTax > 0 
	BEGIN
		SELECT @NewTax = (@OriginalTax/@OriginalQty) * @SplitQuantity
		SELECT @RevisedTax = (@OriginalTax/@OriginalQty) * @RevisedQty
	END
ELSE
	BEGIN
		SELECT @OriginalTax = 0
		SELECT @NewTax = 0
		SELECT @RevisedTax = 0
	END


IF @OriginalTaxA > 0 
	BEGIN
		SELECT @NewTaxA = (@OriginalTaxA/@OriginalQty) * @SplitQuantity
		SELECT @RevisedTaxA = (@OriginalTaxA/@OriginalQty) * @RevisedQty
	END
ELSE
	BEGIN
		SELECT @OriginalTaxA = 0
		SELECT @NewTaxA = 0
		SELECT @RevisedTaxA = 0
	END


IF @OriginalTax2 > 0 
	BEGIN
		SELECT @NewTax2 = (@OriginalTax2/@OriginalQty) * @SplitQuantity
		SELECT @RevisedTax2 = (@OriginalTax2/@OriginalQty) * @RevisedQty
	END
ELSE
	BEGIN
		SELECT @OriginalTax2 = 0
		SELECT @NewTax2 = 0
		SELECT @RevisedTax2 = 0
	END


IF @OriginalTax2A > 0 
	BEGIN
		SELECT @NewTax2A = (@OriginalTax2A/@OriginalQty) * @SplitQuantity
		SELECT @RevisedTax2A = (@OriginalTax2A/@OriginalQty) * @RevisedQty
	END
ELSE
	BEGIN
		SELECT @OriginalTax2A = 0
		SELECT @NewTax2A = 0
		SELECT @RevisedTax2A = 0
	END


IF @OriginalNet > 0 
	BEGIN
		SELECT @NewNet = (@OriginalNet/@OriginalQty) * @SplitQuantity
		SELECT @RevisedNet = (@OriginalNet/@OriginalQty) * @RevisedQty
	END
ELSE
	BEGIN
		SELECT @OriginalNet = 0
		SELECT @NewNet = 0
		SELECT @RevisedNet = 0
	END


IF @OriginalGross > 0 
	BEGIN
		SELECT @NewGross = (@OriginalGross/@OriginalQty) * @SplitQuantity
		SELECT @RevisedGross = (@OriginalGross/@OriginalQty) * @RevisedQty
	END
ELSE
	BEGIN
		SELECT @OriginalGross = 0
		SELECT @NewGross = 0
		SELECT @RevisedGross = 0
	END


IF @OriginalQuantityReserved > 0 
	BEGIN
		SELECT @NewQuantityReserved = @SplitQuantity
		SELECT @RevisedQuantityReserved = @RevisedQty
	END
ELSE
	BEGIN
		SELECT @OriginalQuantityReserved = 0
		SELECT @NewQuantityReserved = 0
		SELECT @RevisedQuantityReserved = 0
	END



SELECT @HighestTransId = TransId FROM CustomerOrderDetail WHERE CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance

SELECT @NewTransId = @HighestTransId + 1


---- INSERT A NEW COD
INSERT INTO
	CustomerOrderDetail
SELECT
	CustomerOrderHeaderInstance
	, @NewTransId
	, CustomerShipToInstance
	, ProductCode
	, ProductName
	, @SplitQuantity
	, Price
	, PriceA
	, @NewTax
	, @NewTaxA
	, StatusInstance
	, DelFlag
	, Renewal
	, Recipient
	, OverrideProduct
	, CreationDate
	, CrossedBridgeDate
	, Left(@ModifiedBy,4)
	, getdate()
	, InvoiceNumber
	, AlphaProductCode
	, CouponPage
	, FDIndicator
	, MktgIndicator
	, ToteInstance
	, GiftCD
	, IsGift
	, IsGiftCardSent
	, SendGiftCardBeforeDate
	, ProgramSectionId
	, CatalogPrice
	, @NewQuantityReserved
	, PriceOverrideId
	, ProductType
	, PricingDetailsId
	, @NewTax2
	, @NewTax2A
	, @NewNet
	, @NewGross
	, SupporterName
	, SendGiftCard
	, QuantityShipped
	, ShipmentId
	, ReplacedProductCode
	, ReplacedProductQty
	, DistributionCenterId
	, Comment
	, CustomerComment
FROM
	 CustomerOrderDetail 
WHERE 
	CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance 
	AND TransId = @TransId



---- UPDATE THE OLD COD WITH NEW QUANTITIES
UPDATE
	CustomerOrderDetail
SET
	Tax = @RevisedTax
	, TaxA = @RevisedTaxA
	, Tax2 = @RevisedTax2
	, Tax2A = @RevisedTax2A
	, Net = @RevisedNet
	, Gross = @RevisedGross
	, QuantityReserved = @RevisedQuantityReserved
	, Quantity = @RevisedQty
	, ChangeUserId = Left(@ModifiedBy,4)
	, ChangeDate = getdate()
WHERE
	CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
	AND TransId = @TransId


---- BUMP COH to NextTransId
UPDATE
	CustomerOrderHeader
SET
	NextDetailTransId = @NewTransId + 1
WHERE
	Instance = @CustomerOrderHeaderInstance


COMMIT TRAN T1
GO
