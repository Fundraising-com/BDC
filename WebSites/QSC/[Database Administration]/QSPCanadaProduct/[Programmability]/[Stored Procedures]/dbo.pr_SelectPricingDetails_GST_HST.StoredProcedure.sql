USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectPricingDetails_GST_HST]    Script Date: 06/07/2017 09:18:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[pr_SelectPricingDetails_GST_HST]

	@iMagPriceInstanceGST	int,
	@iMagPriceInstanceHST	int

AS

SELECT	pdGST.MagPrice_Instance,
		pdGST.Pricing_Year,
		pdGST.Pricing_Season,
		pdGST.Product_Code,
		p.Product_Sort_Name,
		c.Description as Currency,
		pdGST.Program_ID,
		pdGST.Program_Type,
		pdGST.ProgramSectionID,
		pdGST.Offer_Code,
		pdGST.Status,
		coalesce(pdGST.Remit_Rate, 0) * 100 AS Remit_Rate,
		pdGST.Nbr_of_Issues,
		coalesce(pdGST.Duration, 0) AS Duration,
		coalesce(pdGST.Duration_Measure, '') AS Duration_Measure,
		coalesce(pdGST.NewsStand_Price_Yr, 0) AS NewsStand_Price_Yr,
		coalesce(pdGST.Basic_Price_Yr, 0) AS Basic_Price_Yr,
		coalesce(pdGST.QSP_Price, 0) AS QSP_PriceGST,
		coalesce(pdHST.QSP_Price, 0) AS QSP_PriceHST,
		coalesce(pdGST.EffortKeyRequired, 0) AS EffortKeyRequired,
		coalesce(pdGST.Effort_Key, '') AS Effort_Key,
		coalesce(pdGST.Logged_By, '') AS Logged_By,
		coalesce(pdGST.Log_Dt, '1995-01-01') AS Log_Dt,
		coalesce(pdGST.EffectiveDate, '1995-01-01') AS EffectiveDate,
		coalesce(pdGST.EndDate, '1995-01-01') AS EndDate,
		coalesce(pdGST.NewsStandPriceOriginalCurrency, 0) AS NewsStandPriceOriginalCurrency,
		coalesce(pdGST.ConversionRate, 0) * 100 AS ConversionRate,
		coalesce(pdGST.Comment, '') AS Comment,
		coalesce(pdGST.DateSubmitted, '1995-01-01') AS DateSubmitted,
		coalesce(pdGST.BasePriceOriginalCurrency, 0) AS BasePriceOriginalCurrency,
		coalesce(pdGST.TaxRegionID, 0) AS TaxRegionID,
		coalesce(pdGST.DefaultGrossValue, 0) AS DefaultGrossValue,
		coalesce(pdGST.FSExtra_Limit_Rate, 0) AS FSExtra_Limit_Rate,
		coalesce(pdGST.FSIsBrochure, 0) AS FSIsBrochure,
		coalesce(pdGST.FSApplicabilityId, 0) AS FSApplicabilityId,
		coalesce(pdGST.FSDistributionLevelId, 0) AS FSDistributionLevelId,
		pdGST.Language_Code,
		coalesce(pdGST.FSCatalog_Product_Code, '') AS FSCatalog_Product_Code,
		coalesce(pdGST.FSContent_Catalog_Code, '') AS FSContent_Catalog_Code,
		coalesce(pdGST.FSProgram_Id, 0) AS FSProgram_Id,
		coalesce(pdGST.OracleCode, '') AS OracleCode,
		pdGST.InternetApproval,
		coalesce(pdGST.ABCCode, '') AS ABCCode,
		coalesce(pdGST.AdInCatalog, 0) as AdInCatalog,
		coalesce(pdGST.AdPageSizeID, 0) as AdPageSizeID,
		coalesce(pdGST.AdCost, 0) as AdCost,
		coalesce(pdGST.AdCostCurrency, 0) as AdCostCurrency,
		coalesce(pdGST.ListingLevelID, 0) as ListingLevelID,
		coalesce(pdGST.ListingCopyText, '') as ListingCopyText,
		coalesce(pdGST.QSPPremiumID, 0) as QSPPremiumID,
		coalesce(pdGST.prdPremiumInd, '') as PremiumInd,
		coalesce(pdGST.prdPremiumCode, '') as PremiumCode,
		coalesce(pdGST.prdPremiumCopy, '') as PremiumCopy,
		coalesce(pdGST.FSProvinceCode, '') as FSProvinceCode,
		coalesce(pdGST.ContractFormReceived,0) as ContractFormReceived,
		coalesce(pdGST.MagazineCoverFilename,'') as MagazineCoverFilename,
		coalesce(pdGST.CatalogAdFilename,'') as CatalogAdFilename,
		coalesce(pdGST.CatalogPageNumber,0) as CatalogPageNumber,
		coalesce(pdGST.PlacementLevel,0) as PlacementLevel, 
		coalesce(pdGST.ContractComment,'') as ContractComment,
		coalesce(pdGST.PrinterComment,'') as PrinterComment,
		coalesce(pdGST.QSPCAListingCopyText,'') as QSPCAListingCopyText,
		coalesce(pdGST.BasePriceSansPostage,0) as BasePriceSansPostage,
		coalesce(pdGST.PostageRemitRate,0) * 100 as PostageRemitRate,
		coalesce(pdGST.PostageAmount,0)	 as PostageAmount	,
		coalesce(pdGST.BaseRemitRate,0) * 100	 as BaseRemitRate,	
		coalesce(p.VendorProductCode, '') AS VendorProductCode,
		coalesce(pdGST.AddlHandlingFee,0) as AddlHandlingFee,
		coalesce(pdGST.ListAgentCode,'') as ListAgentCode,
		coalesce(pdGST.QSPAgencyCode,'') as QSPAgencyCode

FROM		Pricing_Details pdGST,
		Pricing_Details pdHST,
		QSPCanadaCommon..CodeDetail c,
		Product	p
WHERE	pdGST.MagPrice_Instance = @iMagPriceInstanceGST
AND		pdHST.MagPrice_Instance = @iMagPriceInstanceHST
AND		p.Product_Instance = pdGST.Product_Instance
and             c.Instance = Currency
GO
