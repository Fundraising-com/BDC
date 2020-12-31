USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Season_SelectAllSeasonLetters]    Script Date: 06/07/2017 09:33:29 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Season_SelectAllSeasonLetters] AS
SELECT	DISTINCT
		s.Season
FROM		.Season s
WHERE	coalesce(s.Season, '') <> ''
GO
