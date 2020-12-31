USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ShippingFee_Insert]    Script Date: 06/07/2017 09:20:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ShippingFee_Insert]

	@OrderID INT

AS

DECLARE @QtyPretzelRods INT

SELECT	@QtyPretzelRods = SUM(cod.Quantity)
FROM	CustomerOrderDetail cod
JOIN	CustomerOrderHeader coh
			ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN	Batch b
			ON	b.ID = coh.OrderBatchID
			AND	b.[Date] = coh.OrderBatchDate
JOIN	QSPCanadaProduct..PRICING_DETAILS pd
			ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN	QSPCanadaProduct..ProgramSection ps
			ON	ps.ID = pd.ProgramSectionID
WHERE	b.OrderID = @OrderID
AND		cod.ProductType = 46025 --Pretzel Rods
AND		ps.Type = 17 --$2 Pretzel Rods

If @QtyPretzelRods > 0
BEGIN

	DECLARE @SFeeInstance INT
	DECLARE @fPrice FLOAT
	
	SET @SFeeInstance = 516715

	SET @fPrice = CASE WHEN @QtyPretzelRods >= 10 THEN 100.00 ELSE @QtyPretzelRods * 10.00 END

	DECLARE @COHInstance INT
	
	SELECT	@COHInstance = coh.Instance
	FROM	CustomerOrderHeader coh
	JOIN	Batch b
				ON	b.ID = coh.OrderBatchID
				AND	b.[Date] = coh.OrderBatchDate
	WHERE	b.OrderID = @OrderID

	EXEC pr_Kanata_OrderItem_Create 
		@iCustomerOrderHeaderInstance = @COHInstance,
		@iMagPriceInstance = @SFeeInstance,
		@iQuantity = 1,
		@fPrice	= @fPrice,
		@iOverrideCode = 45004,
		@iShipToInstance = 0,
		@zShipToFirstName = 'zz',
		@zShipToLastName = 'zz'

END
GO
