USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_DeletePricingDetailsByCatalogSectionID]    Script Date: 06/07/2017 09:17:51 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_DeletePricingDetailsByCatalogSectionID]

	@iCatalogSectionID	int

AS
	DELETE FROM Pricing_Details WHERE ProgramSectionID = @iCatalogSectionID
GO
