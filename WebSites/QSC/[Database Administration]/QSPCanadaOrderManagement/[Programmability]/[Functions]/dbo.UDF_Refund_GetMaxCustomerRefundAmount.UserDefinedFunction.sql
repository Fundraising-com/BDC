USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_Refund_GetMaxCustomerRefundAmount]    Script Date: 06/07/2017 09:21:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_Refund_GetMaxCustomerRefundAmount]  
(	
	@CustomerOrderHeaderInstance	INT,
	@TransID						INT
)

RETURNS NUMERIC(12, 2)

AS

BEGIN 

	DECLARE @ReturnValue			NUMERIC(12, 2),
			@ProcessingFeeAmount	NUMERIC(12, 2),
			@PriceEntered			NUMERIC(12, 2),
			@ShippingFeeAmount		NUMERIC(12, 2),
			@TRTShippingFeeAmount	NUMERIC(12, 2),
			@RecipientName			VARCHAR(81)

	SET @ProcessingFeeAmount = CAST('0.00' AS NUMERIC(12,2))
	SET @ShippingFeeAmount = CAST('0.00' AS NUMERIC(12,2))
	SET @TRTShippingFeeAmount = CAST('0.00' AS NUMERIC(12,2))
	
	DECLARE @ExistsOtherCODNotCancelled BIT
	
	SELECT		@ExistsOtherCODNotCancelled = CONVERT(BIT, COUNT(*))
	FROM		CustomerOrderDetail cod
	LEFT JOIN	CustomerOrderDetailRemitHistory codrh ON codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance AND codrh.TransID = cod.TransID
	WHERE		cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
	AND			cod.TransID <> @TransID
	AND			cod.ProductType NOT IN (46017, 46021) --46017: Processing Fee, 46021: S&H
	AND			cod.StatusInstance NOT IN (506) --506: Cancelled
	AND			ISNULL(codrh.Status, 0) NOT IN (42002, 42003, 42004)
	AND			cod.DelFlag = 0
	
	IF (@ExistsOtherCODNotCancelled = 0)
		SET @ProcessingFeeAmount = ISNULL(dbo.UDF_CustomerOrderHeader_GetProcFeeInfo(@CustomerOrderHeaderInstance),0.00)

	SELECT	@RecipientName = cod.Recipient
	FROM	CustomerOrderDetail cod
	WHERE	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
	AND		cod.TransID = @TransID

	DECLARE @ExistsOtherCODInShipmentNotCancelled BIT
	
	SELECT		@ExistsOtherCODInShipmentNotCancelled = CONVERT(BIT, COUNT(*))
	FROM		CustomerOrderDetail cod
	WHERE		cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
	AND			cod.TransID <> @TransID
	AND			cod.ProductType NOT IN (46017, 46021) --46017: Processing Fee, 46021: S&H
	AND			cod.StatusInstance NOT IN (506) --506: Cancelled
	AND			cod.DelFlag = 0
	AND			cod.Recipient = @RecipientName
	AND			cod.DistributionCenterID = 1
	AND			cod.IsShippedToAccount = 0

	IF (@ExistsOtherCODInShipmentNotCancelled = 0)
	(
		SELECT	@ShippingFeeAmount = ISNULL(SUM(cod.Price), 0.00)
		FROM	CustomerOrderDetail cod
		WHERE	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
		AND		cod.ProductType IN (46021) --46017: Processing Fee
		AND		cod.DelFlag = 0
		AND		cod.Recipient = @RecipientName
		AND		cod.ProductCode = 'SHIPFEE'
	)

	SELECT	@PriceEntered = ISNULL(vw.Price, 0.00)
	FROM	vw_GetSubAndProductsInfo vw
	WHERE	vw.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance AND vw.TransID = @TransID
	AND		vw.producttypeinstance NOT IN (46017, 46021)  --46017: Processing Fee, 46021: S&H

	SELECT	@TRTShippingFeeAmount = CASE WHEN (cod.Price / ISNULL(cod.Quantity, 1)) > cod.CatalogPrice THEN 0.00 ELSE ISNULL(pd.AddlHandlingFee * ISNULL(cod.Quantity, 1), 0.00) END
	FROM	CustomerOrderDetail cod
	JOIN	QSPCanadaProduct..Pricing_Details pd ON pd.MagPrice_Instance = cod.PricingDetailsID
	JOIN	QSPCanadaProduct..Product p ON p.Product_Instance = pd.Product_Instance
	WHERE	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
	AND		cod.TransID = @TransID

	SET @ReturnValue = @ProcessingFeeAmount + @PriceEntered + @ShippingFeeAmount + @TRTShippingFeeAmount

	RETURN @ReturnValue
END
GO
