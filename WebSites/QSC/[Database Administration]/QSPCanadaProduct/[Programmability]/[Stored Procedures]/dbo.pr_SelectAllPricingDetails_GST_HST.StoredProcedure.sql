USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllPricingDetails_GST_HST]    Script Date: 06/07/2017 09:18:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllPricingDetails_GST_HST]

	@iProductInstance	int = 0,
	@zProductCode		varchar(20) = '',
	@zRemitCode		varchar(20) = '',
	@zProductName	varchar(55) = '',
	@iYearSearch		int = 0,
	@zSeasonSearch	varchar(1) = '',
	@iProductStatus	int = 0,
	@iProductType		int = 0,
	@iNumberOfIssues	int = 0,
	@zOracleCode		varchar(50) = '',
	@zCatalogCode		varchar(10) = '',
	@iPublisherID		int = 0,
	@iFulfillmentHouseID	int = 0,
	@iProgramSectionID	int = 0,
	@bIncludeBlank		bit = 1

AS

	DECLARE	@sqlStatement	varchar(4000)
	DECLARE	@sqlStatement2	varchar(4000)

	set @sqlStatement = 'SELECT			DISTINCT
				coalesce(pdGST.MagPrice_Instance, 0) AS MagPrice_InstanceGST,
				coalesce(pdHST.MagPrice_Instance, 0) AS MagPrice_InstanceHST,
				p.Product_Instance,
				p.Product_Code,
				COALESCE(p.RemitCode, '''') AS RemitCode,
				coalesce(pdGST.Program_ID, 0) AS Program_ID,
				coalesce(pdGST.Program_Type, '''') AS Program_Type,
				p.Product_Year AS Year,
				p.Product_Season AS Season,
				p.Product_Sort_Name,
				coalesce(pdGST.Offer_Code, '''') AS Offer_Code,
				coalesce(pdGST.Status, p.Status) AS StatusInstance,
				coalesce(cdContractStatus.Description, cdProductStatus.Description) AS Status,
				coalesce(pdGST.Remit_Rate, 0) AS Remit_Rate,
				coalesce(pdGST.Nbr_Of_Issues, 0) AS Nbr_Of_Issues,
				coalesce(pdGST.Duration, 0) AS Duration,
				coalesce(pdGST.Duration_Measure, '''') AS Duration_Measure,
				coalesce(pdGST.NewsStand_Price_Yr, 0) AS NewsStand_Price_Yr,
				coalesce(pdGST.Basic_Price_Yr, 0) AS Basic_Price_Yr,
				coalesce(pdGST.QSP_Price, 0.00) AS GSTPrice,
				coalesce(pdHST.QSP_Price, 0.00) AS HSTPrice,
				coalesce(pdGST.EffortKeyRequired, 0) AS EffortKeyRequired,
				coalesce(pdGST.Effort_Key, '''') AS Effort_Key,
				coalesce(pdGST.NewsStandPriceOriginalCurrency, 0) AS NewsStandPriceOriginalCurrency,
				coalesce(pdGST.ConversionRate, CASE p.Currency WHEN 802 THEN COALESCE(s.DefaultConversionRate, 1.00) ELSE 1.00 END) AS ConversionRate,
				coalesce(pdGST.Comment, '''') AS Comment,
				coalesce(pdGST.BasePriceOriginalCurrency, 0) AS BasePriceOriginalCurrency,
				pdGST.DefaultGrossValue,
				coalesce(pdGST.FSExtra_Limit_Rate, 0) AS FSExtra_Limit_Rate,
				coalesce(pdGST.FSIsBrochure, 0) AS FSIsBrochure,
				coalesce(pdGST.FSApplicabilityId, 0) AS FSApplicabilityId,
				coalesce(cdFSApplicability.Description, '''') AS FSApplicability,
				coalesce(pdGST.FSDistributionLevelId, 0) AS FSDistributionLevelId,
				coalesce(cdFSDistributionLevel.Description, '''') AS FSDistributionLevel,
				CASE coalesce(pdGST.Language_Code, p.Lang) WHEN '''' THEN p.Lang ELSE coalesce(pdGST.Language_Code, p.Lang) END as Language,
				coalesce(pdGST.FSCatalog_Product_Code, '''') AS FSCatalog_Product_Code,
				coalesce(pdGST.FSContent_Catalog_Code, '''') AS FSContent_Catalog_Code,
				coalesce(pdGST.FSProgram_Id, 0) AS FSProgram_Id,
				coalesce(pdGST.OracleCode, coalesce(p.OracleCode, '''')) AS OracleCode,
				coalesce(pdGST.InternetApproval, 1) AS InternetApproval,
				pdGST.ABCCode,
				coalesce(pdGST.AdInCatalog, 0) AS AdInCatalog,
				coalesce(pdGST.AdPageSizeID, 0) AS AdPageSizeID,
				coalesce(pdGST.AdCost, 0) AS AdCost,
				coalesce(pdGST.AdCostCurrency, 0) AS AdCostCurrency,
				coalesce(pdGST.ListingLevelID, 0) AS ListingLevelID,
				coalesce(pdGST.ListingCopyText, '''') AS ListingCopyText,
				coalesce(pdGST.prdPremiumInd, '''') AS PremiumInd,
				coalesce(pdGST.prdPremiumCode, '''') AS PremiumCode,
				coalesce(pdGST.prdPremiumCopy, '''') AS PremiumCopy,
				coalesce(pdGST.FSProvinceCode, '''') AS FSProvinceCode,
				p.Type AS ProductTypeInstance,
				cdType.Description AS ProductType,
				coalesce(pm.Code, '''') AS CatalogCode,
				coalesce(pm.Program_Type, '''') AS CatalogName,
				p.Pub_Nbr AS PublisherID,
				p.Fulfill_House_Nbr AS FulfillmentHouseID '

	SET @sqlStatement2 = '
	FROM			Product p '

	if(coalesce(@bIncludeBlank, 1) = 1)
	begin
		set @sqlStatement2 = @sqlStatement2 + ' LEFT OUTER '
	end

	set @sqlStatement2 = @sqlStatement2 + ' JOIN	Pricing_Details pdGST
					ON	pdGST.Product_Instance = p.Product_Instance
					AND	pdGST.TaxRegionID = 1
	LEFT OUTER JOIN	Pricing_Details pdHST
					ON	pdHST.Product_Instance = p.Product_Instance
					AND	pdHST.Nbr_of_Issues = pdGST.Nbr_of_Issues
					AND	pdHST.ProgramSectionID = pdGST.ProgramSectionID
					AND	pdHST.Offer_Code = pdGST.Offer_Code
					AND	pdHST.TaxRegionID = 2
	LEFT OUTER JOIN	ProgramSection ps
					ON	ps.ID = pdGST.ProgramSectionID
	LEFT OUTER JOIN	Program_Master pm
					ON	pm.Program_ID = ps.Program_ID
	LEFT OUTER JOIN	QSPCanadaCommon..CodeDetail cdContractStatus
					ON	cdContractStatus.Instance = pdGST.Status
	LEFT OUTER JOIN	QSPCanadaCommon..CodeDetail cdFSApplicability
					ON	cdFSApplicability.Instance = pdGST.FSApplicabilityId
	LEFT OUTER JOIN	QSPCanadaCommon..CodeDetail cdFSDistributionLevel
					ON	cdFSDistributionLevel.Instance = pdGST.FSDistributionLevelID,
				QSPCanadaCommon..CodeDetail cdType,
				QSPCanadaCommon..CodeDetail cdProductStatus,
				QSPCanadaCommon..Season s
	WHERE	cdType.Instance = p.Type
	AND		cdProductStatus.Instance = p.Status
	AND		s.FiscalYear = p.Product_Year
	AND		s.Season = p.Product_Season '

	if(coalesce(@iProductInstance, 0) <> 0)
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND p.Product_Instance = ' + convert(varchar, @iProductInstance)
	end

	if(coalesce(@zProductCode, '') <> '')
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND p.Product_Code LIKE ''%' + @zProductCode + '%'' '
	end

	if(coalesce(@zRemitCode, '') <> '')
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND p.RemitCode LIKE ''%' + @zRemitCode + '%'' '
	end

	if(coalesce(@zProductName, '') <> '')
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND QSPCanadaOrderManagement.dbo.UDF_ReplaceAccents(p.Product_Sort_Name) LIKE ''%' + QSPCanadaOrderManagement.dbo.UDF_ReplaceAccents(@zProductName) + '%'' '
	end

	if(coalesce(@iYearSearch, 0) <> 0)
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND pdGST.Pricing_Year = ' + convert(varchar, @iYearSearch)
	end

	if(coalesce(@zSeasonSearch, '') <> '')
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND pdGST.Pricing_Season = ''' + @zSeasonSearch + ''' '
	end

	if(coalesce(@iProductStatus, 0) <> 0)
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND pdGST.Status = ' + convert(varchar, @iProductStatus)
	end

	if(coalesce(@iProductType, 0) <> 0)
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND p.Type = ' + convert(varchar, @iProductType)
	end

	if(coalesce(@iNumberOfIssues, 0) <> 0)
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND pdGST.Nbr_of_Issues = ' + convert(varchar, @iNumberOfIssues)
	end

	if(coalesce(@zOracleCode, '') <> '')
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND pd.OracleCode LIKE ''%' + @zOracleCode + '%'' '
	end

	if(coalesce(@zCatalogCode, '') <> '')
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND pm.Code LIKE ''%' + @zCatalogCode + '%'' '
	end

	if(coalesce(@iPublisherID, 0) <> 0)
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND p.Pub_Nbr = ' + convert(varchar, @iPublisherID)
	end

	if(coalesce(@iFulfillmentHouseID, 0) <> 0)
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND p.Fulfill_House_Nbr = ' + convert(varchar, @iFulfillmentHouseID)
	end

	if(coalesce(@iProgramSectionID, 0) <> 0)
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND ps.ID = ' + convert(varchar, @iProgramSectionID)
	end

	set @sqlStatement2 = @sqlStatement2 + ' ORDER BY	p.Product_Year DESC, p.Product_Season DESC, p.Product_Sort_Name '

	exec(@sqlStatement + @sqlStatement2)
GO
