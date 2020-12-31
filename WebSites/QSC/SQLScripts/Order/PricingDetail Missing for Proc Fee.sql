USE [QSPCanadaOrderManagement]
GO

DECLARE @CustomerOrderHeaderInstance INT,
		@TransID INT

SET @CustomerOrderHeaderInstance = 11802133
SET @TransID = 2

SELECT	b.IsInvoiced, b.StatusInstance, b.OrderID,cod.*,*
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance

/*
DECLARE @OrderID INT
SELECT @OrderID = b.OrderID
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance

SELECT	b.IsInvoiced, b.StatusInstance, b.OrderID,cod.*,*
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE	b.OrderID = @OrderID
*/

EXEC pr_CustomerOrderDetail_UpdateProduct 
		@ProductPriceInstance	= 379660,
		@CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance,
		@TransID = @TransID,
		@CloseOrder = 0

--Only do if not already invoiced
UPDATE	b
SET		StatusInstance = 40010
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance 