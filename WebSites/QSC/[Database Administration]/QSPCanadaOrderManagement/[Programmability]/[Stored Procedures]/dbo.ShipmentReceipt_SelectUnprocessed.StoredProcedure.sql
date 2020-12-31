USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ShipmentReceipt_SelectUnprocessed]    Script Date: 06/07/2017 09:20:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShipmentReceipt_SelectUnprocessed]

	@ShipmentReceiptBatchID	INT

AS

SELECT		sr.ShipmentReceiptID
FROM		ShipmentReceipt sr
WHERE		sr.StatusID IN (1) --1: Unprocessed
AND			sr.ShipmentReceiptBatchID = @ShipmentReceiptBatchID
AND			NOT EXISTS (SELECT	1
						FROM	ShipmentReceipt srError
						WHERE	srError.ShipmentReceiptBatchID = sr.ShipmentReceiptBatchID
						AND		srError.StatusID >= 3) --Another receipt in same file is in Error
ORDER BY	sr.ShipmentReceiptID
GO
