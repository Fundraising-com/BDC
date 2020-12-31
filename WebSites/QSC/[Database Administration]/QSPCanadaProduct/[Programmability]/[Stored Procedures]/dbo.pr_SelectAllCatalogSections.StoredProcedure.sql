USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllCatalogSections]    Script Date: 06/07/2017 09:17:59 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllCatalogSections]
	@iCatalogID	int
AS
SELECT	ps.ID,
		pst.ID AS CatalogSectionTypeID,
		pst.Description AS Type,
		ps.Name,
		coalesce(ps.DateCreated, '1955-01-01') AS DateCreated,
		coalesce(ps.UserIDCreated, '') AS UserIDCreated,
		coalesce(ps.DateChanged, '1955-01-01') AS DateChanged,
		coalesce(ps.UserIDChanged, '') AS UserIDChanged,
		coalesce(pfssm.Program_ID, 0) AS FSProgramID,
		coalesce(p.Name, '') AS FSProgramName
FROM		ProgramSection ps
LEFT JOIN	ProgFSSectionMap pfssm
			ON	pfssm.Catalog_Section_ID = ps.ID
LEFT JOIN	QSPCanadaCommon..Program p
			ON	p.ID = pfssm.Program_ID,
		ProgramSectionType pst
WHERE	ps.Program_ID = @iCatalogID
AND		ps.Type = pst.ID
GO
