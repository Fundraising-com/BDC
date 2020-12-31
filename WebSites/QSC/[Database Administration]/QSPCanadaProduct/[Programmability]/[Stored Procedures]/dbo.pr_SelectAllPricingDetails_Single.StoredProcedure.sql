USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllPricingDetails_Single]    Script Date: 06/07/2017 09:18:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllPricingDetails_Single]

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
				coalesce(pd.MagPrice_Instance, 0) AS MagPrice_Instance,
				p.Product_Instance,
				p.Product_Code,
				COALESCE(p.RemitCode, '''') AS RemitCode,
				coalesce(pd.Program_ID, 0) AS Program_ID,
				coalesce(pd.Program_Type, '''') AS Program_Type,
				p.Product_Year AS Year,
				p.Product_Season AS Season,
				p.Product_Sort_Name,
				coalesce(pd.Offer_Code, '''') AS Offer_Code,
				coalesce(pd.Status, p.Status) AS StatusInstance,
				coalesce(cdContractStatus.Description, cdProductStatus.Description) AS Status,
				pd.Remit_Rate,
				coalesce(pd.Nbr_Of_Issues, 0) AS Nbr_Of_Issues,
				pd.Duration,
				pd.Duration_Measure,
				pd.NewsStand_Price_Yr,
				pd.Basic_Price_Yr,
				coalesce(pd.QSP_Price, 0.00) AS QSP_Price,
				pd.EffortKeyRequired,
				pd.Effort_Key,
				pd.NewsStandPriceOriginalCurrency,
				pd.ConversionRate,
				coalesce(pd.Comment, '''') AS Comment,
				pd.BasePriceOriginalCurrency,
				coalesce(pd.TaxRegionID, 0) AS TaxRegionID,
				coalesce(tr.Description, '''') AS TaxRegion,
				pd.DefaultGrossValue,
				pd.FSExtra_Limit_Rate,
				CASE p.Type WHEN 46004 THEN COALESCE(pd.FSIsBrochure, 0) ELSE pd.FSIsBrochure END AS FSIsBrochure,
				pd.FSApplicabilityId,
				coalesce(cdFSApplicability.Description, '''') AS FSApplicability,
				pd.FSDistributionLevelId,
				coalesce(cdFSDistributionLevel.Description, '''') AS FSDistributionLevel,
				CASE coalesce(pd.Language_Code, p.Lang) WHEN '''' THEN p.Lang ELSE coalesce(pd.Language_Code, p.Lang) END as Language,
				coalesce(pd.FSCatalog_Product_Code, p.Product_Code) AS FSCatalog_Product_Code,
				pd.FSContent_Catalog_Code,
				pd.FSProgram_Id,
				coalesce(pd.OracleCode, coalesce(p.OracleCode, '''')) AS OracleCode,
				coalesce(pd.InternetApproval, 0) AS InternetApproval,
				pd.ABCCode,
				pd.AdInCatalog,
				pd.AdPageSizeID,
				pd.AdCost,
				pd.AdCostCurrency,
				pd.ListingLevelID,
				pd.ListingCopyText,
				pd.prdPremiumInd,
				pd.prdPremiumCode,
				pd.prdPremiumCopy,
				pd.FSProvinceCode,
				p.Type AS ProductTypeInstance,
				cdType.Description AS ProductType,
				coalesce(pm.Code, '''') AS CatalogCode,
				coalesce(pm.Program_Type, '''') AS CatalogName,
				p.Pub_Nbr AS PublisherID,
				p.Fulfill_House_Nbr AS FulfillmentHouseID,
				pd.AddlHandlingFee '
				

	SET @sqlStatement2 = '
	FROM			Product p '

	if(coalesce(@bIncludeBlank, 1) = 1)
	begin
		set @sqlStatement2 = @sqlStatement2 + ' LEFT OUTER '
	end

	set @sqlStatement2 = @sqlStatement2 + ' JOIN	Pricing_Details pd
					ON	pd.Product_Instance = p.Product_Instance
	LEFT OUTER JOIN	ProgramSection ps
					ON	ps.ID = pd.ProgramSectionID
	LEFT OUTER JOIN	Program_Master pm
					ON	pm.Program_ID = ps.Program_ID
	LEFT OUTER JOIN	QSPCanadaCommon..CodeDetail cdContractStatus
					ON	cdContractStatus.Instance = pd.Status
	LEFT OUTER JOIN	QSPCanadaCommon..CodeDetail cdFSApplicability
					ON	cdFSApplicability.Instance = pd.FSApplicabilityId
	LEFT OUTER JOIN	QSPCanadaCommon..CodeDetail cdFSDistributionLevel
					ON	cdFSDistributionLevel.Instance = pd.FSDistributionLevelID
	LEFT OUTER JOIN	QSPCanadaCommon..TaxRegion tr
					ON	tr.ID = pd.TaxRegionID,
				QSPCanadaCommon..CodeDetail cdType,
				QSPCanadaCommon..CodeDetail cdProductStatus
	WHERE	cdType.Instance = p.Type
	AND		cdProductStatus.Instance = p.Status '

	if(coalesce(@iProductInstance, 0) <> 0)	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND p.Product_Instance = ' + convert(varchar, @iProductInstance)
	end

	if(coalesce(@zProductCode, '') <> '')
	begin		set @sqlStatement2 = @sqlStatement2 + ' AND p.Product_Code LIKE ''%' + @zProductCode + '%'' '
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
		set @sqlStatement2 = @sqlStatement2 + ' AND pd.Pricing_Year = ' + convert(varchar, @iYearSearch)
	end

	if(coalesce(@zSeasonSearch, '') <> '')
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND pd.Pricing_Season = ''' + @zSeasonSearch + ''' '
	end

	if(coalesce(@iProductStatus, 0) <> 0)
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND pd.Status = ' + convert(varchar, @iProductStatus)
	end

	if(coalesce(@iProductType, 0) <> 0)
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND p.Type = ' + convert(varchar, @iProductType)
	end

	if(coalesce(@zOracleCode, '') <> '')
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND pd.OracleCode LIKE ''%' + @zOracleCode + '%'' '
	end

	if(coalesce(@iNumberOfIssues, 0) <> 0)
	begin		set @sqlStatement2 = @sqlStatement2 + ' AND pd.Nbr_of_Issues = ' + convert(varchar, @iNumberOfIssues)
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
