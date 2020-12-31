USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_TaxRegion_SelectAll]    Script Date: 06/07/2017 09:33:30 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_TaxRegion_SelectAll] AS

SELECT	tr.ID,
		tr.Description,
		tr.ConsolidatedRate,
		tr.EffectiveDate
FROM		TaxRegion tr
ORDER BY	tr.ID,
		tr.EffectiveDate
GO
