USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_DeleteCatalogSection]    Script Date: 06/07/2017 09:17:50 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_DeleteCatalogSection]

	@iCatalogSectionID	int

AS
	DELETE FROM ProgramSection WHERE ID = @iCatalogSectionID
GO
