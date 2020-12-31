USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllCatalogFinancialYears]    Script Date: 06/07/2017 09:17:59 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllCatalogFinancialYears] AS

SELECT	DISTINCT
		s.FiscalYear
FROM		QSPCanadaCommon..Season s
WHERE	coalesce(s.FiscalYear, 0) <> 0
ORDER BY	s.FiscalYear DESC
GO
