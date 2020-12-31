USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ShipmentReceipt_SelectError]    Script Date: 06/07/2017 09:20:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShipmentReceipt_SelectError]

AS

SELECT		sr.ShipmentReceiptID,
			sr.BatchOrderID,
			sr.CustomerOrderHeaderInstance,
			sr.TransID,
			srb.ShipmentReceiptBatchID,
			srb.FileName,
			sr.CreateDate AS ShipmentReceiptDate,
			srs.Description AS Error
FROM		ShipmentReceiptBatch srb
JOIN		ShipmentReceipt sr
				ON	sr.ShipmentReceiptBatchID = srb.ShipmentReceiptBatchID
JOIN		ShipmentReceiptStatus srs
				ON	srs.ShipmentReceiptStatusID = sr.StatusID
LEFT JOIN	ShipmentReceipt srNew
				ON	srNew.CustomerOrderHeaderInstance = sr.CustomerOrderHeaderInstance
				AND	srNew.TransID = sr.TransID
				AND	srNew.CreateDate > sr.CreateDate
WHERE		sr.StatusID >= 4
AND			srNew.ShipmentReceiptID IS NULL --No new shipment receipt for this sub has been received
GO
