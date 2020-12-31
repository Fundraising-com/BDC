USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdateCatalog]    Script Date: 06/07/2017 09:18:04 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_UpdateCatalog]
	@iCatalogID		int,
	@zCatalogCode		varchar(50),
	@zCatalogName	varchar(50),
	@iCatalogType		int,
	@zLanguage		varchar(10),
	@iYear			int,
	@zSeason		varchar(1),
	@iCatalogStatus		int,
	@zIsReplacement	varchar(1),
	@zUserID		varchar(30)
AS
DECLARE	@iSeasonID	int
SELECT	top 1
		@iSeasonID = s.ID
FROM		QSPCanadaCommon..Season s
WHERE	s.FiscalYear = @iYear
AND		s.Season = @zSeason
UPDATE	Program_Master
SET		Program_Type = @zCatalogName,
		SubType = @iCatalogType,
		Season = coalesce(@iSeasonID, 0),
		Code = @zCatalogCode,
		Status =	 @iCatalogStatus,
		Lang = @zLanguage,
		IsReplacement = @zIsReplacement,
		DateChanged = getdate(),
		UserIDChanged = @zUserID
WHERE	Program_ID = @iCatalogID
GO
