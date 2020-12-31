USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectPricingDetailsForNewSubToInvoice]    Script Date: 06/07/2017 09:20:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SelectPricingDetailsForNewSubToInvoice]

	@sProductCode		varchar(20),
	@iNumberOfIssues	int,
	@iProgramSectionID	int,
	@fCatalogPrice		numeric(10,2)

AS

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- This proc is used for New Sub To Invoice and should not be used elsewhere since the goal is only
-- to get a MagPrice_Instance that will retrieve the right ProductCode, NumberOfIssues, ProgramSectionID
-- and CatalogPrice in pr_AddNewItemForCustomerService.
-- 
-- Ben
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

SELECT	top 1
		MagPrice_Instance,
		Pricing_Year,
		Pricing_Season,
		Product_Code,
		Program_ID,
		Program_Type,
		ProgramSectionID,
		Offer_Code,
		Remit_Rate,
		Nbr_of_Issues,
		Duration,
		Duration_Measure,
		NewsStand_Price_Yr,
		Basic_Price_Yr,
		QSP_Price,
		Effort_Key,
		Logged_By,
		Log_Dt,
		EffectiveDate,
		EndDate,
		NewsStandPriceOriginalCurrency,
		ConversionRate,
		Comment,
		DateSubmitted,
		BasePriceOriginalCurrency,
		TaxRegionID,
		DefaultGrossValue,
		FSExtra_Limit_Rate,
		FSIsBrochure,
		FSApplicabilityId,
		FSDistributionLevelId,
		Language_Code,
		FSCatalog_Product_Code,
		FSContent_Catalog_Code,
		FSProgram_Id,
		OracleCode,
		InternetApproval,
		ABCCode
FROM		QSPCanadaProduct..Pricing_Details
WHERE	Product_code = @sProductCode
AND		Nbr_of_Issues = @iNumberOfIssues
AND		ProgramSectionID = @iProgramSectionID
AND		qsp_price = @fCatalogPrice
GO
