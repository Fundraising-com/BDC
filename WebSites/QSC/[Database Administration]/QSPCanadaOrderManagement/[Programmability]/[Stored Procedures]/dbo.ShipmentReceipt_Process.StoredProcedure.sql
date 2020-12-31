USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ShipmentReceipt_Process]    Script Date: 06/07/2017 09:20:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShipmentReceipt_Process]

	@ShipmentReceiptID		INT,
	@IsShipmentReceiptValid	BIT OUTPUT

AS

SET @IsShipmentReceiptValid = CONVERT(BIT, 1)

DECLARE	@Error							BIT,
		@ErrorMessage					VARCHAR(1000),
		@RecExist						BIT,
		@OrderID						INT,
		@CustomerOrderHeaderInstance	INT,
		@TransID						INT

DECLARE @CODExists BIT

SELECT		@CODExists = 1,
			@CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance,
			@TransID = cod.TransID
FROM		CustomerOrderDetail cod
JOIN		ShipmentReceipt sr
				ON	sr.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	sr.TransID = cod.TransID
WHERE		sr.ShipmentReceiptID = @ShipmentReceiptID
AND			cod.Delflag <> 1

IF ISNULL(@CODExists, 0) <> 1
BEGIN
	UPDATE	ShipmentReceipt
	SET		[StatusID] = 4 --4: CustomerOrderDetail does not exist
	WHERE	ShipmentReceiptID = @ShipmentReceiptID
	
	SET @IsShipmentReceiptValid = CONVERT(BIT, 0)
END

SELECT		TOP 1
			@Error = CONVERT(BIT, 1)
FROM		CustomerOrderDetail cod
JOIN		CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		Batch b
				ON	b.Date = coh.OrderBatchDate
				AND	b.ID = coh.OrderBatchID
JOIN		ShipmentReceipt sr
				ON	sr.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	sr.TransID = cod.TransID
WHERE		sr.ShipmentReceiptID = @ShipmentReceiptID
AND			cod.Delflag <> 1
AND			sr.QtyShipped > 0 --Don't fail quantity=0 in case files come in out of order
AND			cod.StatusInstance <> 509 --509: Order Detail Pending to TPL

IF ISNULL(@Error, 0) = 1
BEGIN
	UPDATE	ShipmentReceipt
	SET		[StatusID] = 5 --5: CustomerOrderDetail was not in a pending shipment state
	WHERE	ShipmentReceiptID = @ShipmentReceiptID
	
	SET @Error = CONVERT(BIT, 0)
	SET @IsShipmentReceiptValid = CONVERT(BIT, 0)
END

SELECT		TOP 1
			@Error = CONVERT(BIT, 1)
FROM		ShipmentReceipt sr
JOIN		ShipmentReceiptBatch srb
				ON	srb.ShipmentReceiptBatchID = sr.ShipmentReceiptBatchID
JOIN		ShipmentReceiptBatch srbExist
				ON	srbExist.Filename = srb.Filename
JOIN		ShipmentReceipt srExist
				ON	srExist.ShipmentReceiptBatchID = srbExist.ShipmentReceiptBatchID
WHERE		sr.ShipmentReceiptID = @ShipmentReceiptID
AND			srExist.StatusID IN (2) --2: Processed

IF ISNULL(@Error, 0) = 1
BEGIN
	UPDATE	ShipmentReceipt
	SET		[StatusID] = 6 --6: Shipment Receipt File for CustomerOrderDetail was already processed
	WHERE	ShipmentReceiptID = @ShipmentReceiptID
	
	SET @Error = CONVERT(BIT, 0)
	SET @IsShipmentReceiptValid = CONVERT(BIT, 0)
END
GO
