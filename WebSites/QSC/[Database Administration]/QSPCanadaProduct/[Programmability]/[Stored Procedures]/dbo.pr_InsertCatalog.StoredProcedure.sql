USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_InsertCatalog]    Script Date: 06/07/2017 09:17:54 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_InsertCatalog]
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
INSERT INTO	Program_Master
		(Program_Type,
		SubType,
		Season,
		Code,
		Status,
		Country,
		Lang,
		IsReplacement,
		IsNational,
		DateCreated,
		UserIDCreated,
		DateChanged,
		UserIDChanged)
VALUES	(@zCatalogName,
		@iCatalogType,
		coalesce(@iSeasonID, 0),
		@zCatalogCode,
		@iCatalogStatus,
		'CA',
		@zLanguage,
		@zIsReplacement,
		'N',
		getdate(),
		@zUserID,
		getdate(),
		@zUserID)
SELECT SCOPE_IDENTITY()
GO
