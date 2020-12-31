USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_FiscalYear_SelectAll]    Script Date: 06/07/2017 09:33:18 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_FiscalYear_SelectAll] AS

SELECT	DISTINCT
		0 AS ID,
		s.FiscalYear
FROM		Season s
WHERE	coalesce(s.FiscalYear, 0) <> 0
ORDER BY	s.FiscalYear DESC
GO
