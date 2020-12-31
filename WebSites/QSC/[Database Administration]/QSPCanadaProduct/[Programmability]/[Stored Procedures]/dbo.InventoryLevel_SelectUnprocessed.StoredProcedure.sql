USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[InventoryLevel_SelectUnprocessed]    Script Date: 06/07/2017 09:17:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InventoryLevel_SelectUnprocessed]

	@InventoryLevelBatchID	INT

AS

SELECT		il.InventoryLevelID
FROM		InventoryLevel il
WHERE		il.StatusID IN (1) --1: Unprocessed
AND			il.InventoryLevelBatchID = @InventoryLevelBatchID
AND			NOT EXISTS (SELECT	1
						FROM	InventoryLevel ilError
						WHERE	ilError.InventoryLevelBatchID = il.InventoryLevelBatchID
						AND		ilError.StatusID >= 3) --Another receipt in same file is in Error
ORDER BY	il.InventoryLevelID
GO
