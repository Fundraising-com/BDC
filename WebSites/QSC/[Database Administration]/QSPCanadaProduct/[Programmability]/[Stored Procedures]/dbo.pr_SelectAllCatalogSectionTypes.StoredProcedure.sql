USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllCatalogSectionTypes]    Script Date: 06/07/2017 09:17:59 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllCatalogSectionTypes] AS
SELECT	ID, Description
FROM		ProgramSectionType
WHERE	ID <> 5
ORDER BY	ID
GO
