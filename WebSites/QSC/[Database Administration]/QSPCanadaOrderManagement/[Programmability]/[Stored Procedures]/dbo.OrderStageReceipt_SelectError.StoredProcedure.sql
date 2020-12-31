USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[OrderStageReceipt_SelectError]    Script Date: 06/07/2017 09:19:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[OrderStageReceipt_SelectError]

AS

SELECT		osr.OrderStageReceiptID,
			osr.OrderID,
			osrb.FileName,
			osr.CreateDate AS OrderStageReceiptDate,
			osrs.Description AS Error
FROM		OrderStageReceiptBatch osrb
JOIN		OrderStageReceipt osr
				ON	osr.OrderStageReceiptBatchID = osrb.OrderStageReceiptBatchID
JOIN		OrderStageReceiptStatus osrs
				ON	osrs.OrderStageReceiptStatusID = osr.StatusID
LEFT JOIN	OrderStageReceipt osrNew
				ON	ISNULL(osrNew.OrderID, 0) = ISNULL(osr.OrderID, 0)
				AND	osrNew.CampaignID = osr.CampaignID
				AND	osrNew.CreateDate > osr.CreateDate
WHERE		osr.StatusID >= 4
AND			osrNew.OrderStageReceiptID IS NULL --No new order stage receipt for this order has been received
GO
