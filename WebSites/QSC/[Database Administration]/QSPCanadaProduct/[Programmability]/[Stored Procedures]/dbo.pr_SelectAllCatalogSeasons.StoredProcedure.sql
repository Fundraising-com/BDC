USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllCatalogSeasons]    Script Date: 06/07/2017 09:17:59 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllCatalogSeasons] AS

SELECT	DISTINCT
		s.Season
FROM		QSPCanadaCommon..Season s
WHERE	coalesce(s.Season, '') <> ''
GO
