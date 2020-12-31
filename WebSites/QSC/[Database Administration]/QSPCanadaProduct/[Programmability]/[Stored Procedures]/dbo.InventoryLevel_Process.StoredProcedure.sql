USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[InventoryLevel_Process]    Script Date: 06/07/2017 09:17:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InventoryLevel_Process]

	@InventoryLevelID		INT,
	@IsInventoryLevelValid	BIT OUTPUT

AS

SET @IsInventoryLevelValid = CONVERT(BIT, 1)

DECLARE	@Error			BIT,
		@ErrorMessage	VARCHAR(1000),
		@RecExist		BIT,
		@ProductCode	VARCHAR(20),
		@Date			DATETIME

SET	@Date = GETDATE()

DECLARE	@ProductExists BIT

SELECT		@ProductExists = 1,
			@ProductCode = prod.Product_Code
FROM		InventoryLevel il
JOIN		Product prod
				ON	prod.Product_Code = il.ProductCode
JOIN		QSPCanadaCommon..Season seas
				ON	seas.Season = prod.Product_Season
				AND	seas.FiscalYear = prod.Product_Year
				AND	seas.StartDate <= @Date
				AND	seas.EndDate > @Date
WHERE		il.InventoryLevelID = @InventoryLevelID

IF ISNULL(@ProductExists, 0) <> 1
BEGIN
	UPDATE	InventoryLevel
	SET		[StatusID] = 4 --4: No Current Active Product Code in Product Table
	WHERE	InventoryLevelID = @InventoryLevelID
	
	SET @Error = CONVERT(BIT, 0)
	SET @IsInventoryLevelValid = CONVERT(BIT, 0)
END

SELECT		TOP 1
			@Error = CONVERT(BIT, 1)
FROM		InventoryLevel ilCurrent
JOIN		InventoryLevelBatch ilbCurrent
				ON	ilbCurrent.InventoryLevelBatchID = ilCurrent.InventoryLevelBatchID
JOIN		InventoryLevelBatch ilbExisting
				ON	ilbCurrent.Filename = ilbCurrent.Filename
JOIN		InventoryLevel ilExisting
				ON	ilExisting.InventoryLevelBatchID = ilbExisting.InventoryLevelBatchID
WHERE		ilCurrent.InventoryLevelID = @InventoryLevelID
AND			ilExisting.StatusID IN (2) --2: Processed

IF ISNULL(@Error, 0) = 1
BEGIN
	UPDATE	InventoryLevel
	SET		[StatusID] = 5 --5: InventoryLevel File was already processed
	WHERE	InventoryLevelID = @InventoryLevelID
	
	SET @Error = CONVERT(BIT, 0)
	SET @IsInventoryLevelValid = CONVERT(BIT, 0)
END
GO
