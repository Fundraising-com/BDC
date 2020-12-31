USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectPricingDetails_Single]    Script Date: 06/07/2017 09:18:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SelectPricingDetails_Single]

	@iMagPriceInstance		int

AS

SELECT	pd.MagPrice_Instance,
		pd.Pricing_Year,
		pd.Pricing_Season,
		pd.Product_Code,
		p.Product_Sort_Name,
		'' As Currency,
		pd.Program_ID,
		pd.Program_Type,
		pd.ProgramSectionID,
		pd.Offer_Code,
		pd.Status,
		coalesce(pd.Remit_Rate, 0) * 100 AS Remit_Rate,
		pd.Nbr_of_Issues,
		coalesce(pd.Duration, 0) AS Duration,
		coalesce(pd.Duration_Measure, '') AS Duration_Measure,
		coalesce(pd.NewsStand_Price_Yr, 0) AS NewsStand_Price_Yr,
		coalesce(pd.Basic_Price_Yr, 0) AS Basic_Price_Yr,
		coalesce(pd.QSP_Price, 0) AS QSP_PriceGST,
		coalesce(pd.QSP_Price, 0) AS QSP_PriceHST,
		coalesce(pd.EffortKeyRequired, 0) AS EffortKeyRequired,
		coalesce(pd.Effort_Key, '') AS Effort_Key,
		coalesce(pd.Logged_By, '') AS Logged_By,
		coalesce(pd.Log_Dt, '1995-01-01') AS Log_Dt,
		coalesce(pd.EffectiveDate, '1995-01-01') AS EffectiveDate,
		coalesce(pd.EndDate, '1995-01-01') AS EndDate,
		coalesce(pd.NewsStandPriceOriginalCurrency, 0) AS NewsStandPriceOriginalCurrency,
		coalesce(pd.ConversionRate, 0) * 100 AS ConversionRate,
		coalesce(pd.Comment, '') AS Comment,
		coalesce(pd.DateSubmitted, '1995-01-01') AS DateSubmitted,
		coalesce(pd.BasePriceOriginalCurrency, 0) AS BasePriceOriginalCurrency,
		pd.TaxRegionID,
		coalesce(pd.DefaultGrossValue, 0) AS DefaultGrossValue,
		coalesce(pd.FSExtra_Limit_Rate, 0) AS FSExtra_Limit_Rate,
		coalesce(pd.FSIsBrochure, 0) AS FSIsBrochure,
		coalesce(pd.FSApplicabilityId, 0) AS FSApplicabilityId,
		coalesce(pd.FSDistributionLevelId, 0) AS FSDistributionLevelId,
		pd.Language_Code,
		coalesce(pd.FSCatalog_Product_Code, '') AS FSCatalog_Product_Code,
		coalesce(pd.FSContent_Catalog_Code, '') AS FSContent_Catalog_Code,
		coalesce(pd.FSProgram_Id, 0) AS FSProgram_Id,
		coalesce(pd.OracleCode, '') AS OracleCode,
		pd.InternetApproval,
		coalesce(pd.ABCCode, '') AS ABCCode,
		coalesce(pd.AdInCatalog, 0) as AdInCatalog,
		coalesce(pd.AdPageSizeID, 0) as AdPageSizeID,
		coalesce(pd.AdCost, 0) as AdCost,
		coalesce(pd.AdCostCurrency, 0) as AdCostCurrency,
		coalesce(pd.ListingLevelID, 0) as ListingLevelID,
		coalesce(pd.ListingCopyText, '') as ListingCopyText,
		coalesce(pd.QSPPremiumID, 0) as QSPPremiumID,
		coalesce(pd.prdPremiumInd, '') as PremiumInd,
		coalesce(pd.prdPremiumCode, '') as PremiumCode,
		coalesce(pd.prdPremiumCopy, '') as PremiumCopy,
		coalesce(pd.FSProvinceCode, '') as FSProvinceCode,
		coalesce(pd.ContractFormReceived,0) as ContractFormReceived,
		coalesce(pd.MagazineCoverFilename,'') as MagazineCoverFilename,
		coalesce(pd.CatalogAdFilename,'') as CatalogAdFilename,
		coalesce(pd.CatalogPageNumber,0) as CatalogPageNumber,
		coalesce(pd.PlacementLevel,0) as PlacementLevel, 
		coalesce(pd.ContractComment,'') as ContractComment,
		coalesce(pd.PrinterComment,'') as PrinterComment,
		coalesce(pd.QSPCAListingCopyText,'') as QSPCAListingCopyText,
		coalesce(BasePriceSansPostage,0) as BasePriceSansPostage,
		coalesce(PostageRemitRate,0) as PostageRemitRate,
		coalesce(PostageAmount,0) as PostageAmount,
		coalesce(BaseRemitRate,0) * 100	 as BaseRemitRate,
		coalesce(p.VendorProductCode, '') AS VendorProductCode,
		coalesce(pd.AddlHandlingFee, 0) as AddlHandlingFee,
		coalesce(pd.ListAgentCode,'') as ListAgentCode,
		coalesce(pd.QSPAgencyCode,'') as QSPAgencyCode
FROM		Pricing_Details pd,
		Product	p
WHERE	pd.MagPrice_Instance = @iMagPriceInstance
AND		p.Product_Instance = pd.Product_Instance
GO
