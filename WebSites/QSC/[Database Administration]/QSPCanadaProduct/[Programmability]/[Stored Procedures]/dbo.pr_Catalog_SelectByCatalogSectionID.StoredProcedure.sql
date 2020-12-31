USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_Catalog_SelectByCatalogSectionID]    Script Date: 06/07/2017 09:17:49 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Catalog_SelectByCatalogSectionID]

	@iProgramSectionID	int

AS

SELECT		pm.*
FROM		ProgramSection ps
JOIN		Program_Master pm
				ON	pm.Program_ID = ps.Program_ID
WHERE		ps.ID = @iProgramSectionID
GO
