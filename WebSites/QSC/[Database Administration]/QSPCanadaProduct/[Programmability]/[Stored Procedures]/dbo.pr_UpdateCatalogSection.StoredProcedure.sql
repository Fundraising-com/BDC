USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdateCatalogSection]    Script Date: 06/07/2017 09:18:04 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_UpdateCatalogSection]
	@iCatalogSectionID	int,
	@zCatalogCode		varchar(50),
	@iType			int,
	@zName		varchar(50),
	@iFSProgramID		int,
	@zUserID		varchar(30)
AS

DECLARE	@iFSProgramCount	int
DECLARE	@iOriginalFSProgramID	int

UPDATE	ProgramSection
SET		Type = @iType,
		CatalogCode = @zCatalogCode,
		Name = @zName,
		DateChanged = getdate(),
		UserIDChanged = @zUserID
WHERE	ID = @iCatalogSectionID

IF(@iType = 3 AND @iFSProgramID <> 0)
BEGIN
	SELECT	TOP 1
			@iOriginalFSProgramID = Program_ID
	FROM		ProgFSSectionMap
	WHERE	Catalog_Section_ID = @iCatalogSectionID

	SELECT	@iFSProgramCount = COUNT(*)
	FROM		ProgFSSectionMap
	WHERE	Catalog_Section_ID = @iCatalogSectionID

	IF(@iFSProgramCount > 0)
	BEGIN
		UPDATE	ProgFSSectionMap
		SET		Program_ID = @iFSProgramID
		WHERE	Catalog_Section_ID = @iCatalogSectionID
	END
	ELSE
	BEGIN
		INSERT INTO	ProgFSSectionMap
				(Program_ID,
				Catalog_Section_ID)
		VALUES	(@iFSProgramID,
				@iCatalogSectionID)
	END

	IF(COALESCE(@iFSProgramID, 0) <> COALESCE(@iOriginalFSProgramID, 0))
	BEGIN
		UPDATE	Pricing_Details
		SET		FSProgram_ID = @iFSProgramID
		WHERE	ProgramSectionID = @iCatalogSectionID
	END
END
ELSE
BEGIN
	DELETE FROM	ProgFSSectionMap
	WHERE	Catalog_Section_ID = @iCatalogSectionID
END
GO
