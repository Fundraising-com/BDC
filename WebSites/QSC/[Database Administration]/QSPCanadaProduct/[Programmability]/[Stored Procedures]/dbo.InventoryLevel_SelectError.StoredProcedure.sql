USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[InventoryLevel_SelectError]    Script Date: 06/07/2017 09:17:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InventoryLevel_SelectError]

AS

SELECT		il.InventoryLevelBatchID,
			il.ProductCode,
			ilb.FileName,
			il.CreateDate AS InventoryLevelDate,
			ils.Description AS Error
FROM		InventoryLevelBatch ilb
JOIN		InventoryLevel il
				ON	il.InventoryLevelBatchID = ilb.InventoryLevelBatchID
JOIN		InventoryLevelStatus ils
				ON	ils.InventoryLevelStatusID = il.StatusID
LEFT JOIN	InventoryLevel ilNew
				ON	ilNew.ProductCode = il.ProductCode
				AND	ilNew.CreateDate > il.CreateDate
WHERE	il.StatusID >= 4
AND		ilNew.InventoryLevelID IS NULL --No new inventory level for this product has been received
GO
