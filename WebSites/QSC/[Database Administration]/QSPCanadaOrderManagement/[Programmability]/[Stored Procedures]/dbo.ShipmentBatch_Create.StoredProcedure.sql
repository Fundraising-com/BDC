USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ShipmentBatch_Create]    Script Date: 06/07/2017 09:20:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShipmentBatch_Create]

	@ShipmentBatchID	INT OUTPUT

AS

DECLARE @ShipmentsAvailable BIT

SELECT	@ShipmentsAvailable = CONVERT(BIT, COUNT(*))
FROM	ShipmentOrder
WHERE	ISNULL(ShipmentBatchID, 0) = 0

--If no shipments available then don't create a batch
IF @ShipmentsAvailable = CONVERT(BIT, 0)
BEGIN
	SET @ShipmentBatchID = 0
	RETURN
END

BEGIN TRANSACTION

INSERT INTO ShipmentBatch
(
	CreationDate
)
SELECT	GETDATE()

SET @ShipmentBatchID = SCOPE_IDENTITY()

UPDATE	ShipmentOrder
SET		ShipmentBatchID = @ShipmentBatchID
WHERE	ISNULL(ShipmentBatchID, 0) = 0

COMMIT TRANSACTION
GO
