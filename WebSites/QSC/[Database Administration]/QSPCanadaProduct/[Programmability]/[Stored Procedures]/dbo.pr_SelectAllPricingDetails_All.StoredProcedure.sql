USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllPricingDetails_All]    Script Date: 06/07/2017 09:18:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllPricingDetails_All]

	@iProductInstance	int = 0,
	@zProductCode		varchar(20) = '',
	@zProductName	varchar(55) = '',
	@iYearSearch		int = 0,
	@zSeasonSearch	varchar(1) = '',
	@iProductStatus	int = 0,
	@zProductTypeList	varchar(150) = '',
	@zCatalogCode		varchar(10) = '',
	@iProgramSectionID	int = 0,
	@bIncludeBlank		bit = 1

AS
DECLARE	@sqlStatement	varchar(4000)
	DECLARE	@sqlStatement2	varchar(4000)

	set @sqlStatement = 'SELECT			DISTINCT
				coalesce(pd.MagPrice_Instance, 0) AS MagPrice_Instance,
				coalesce(pdHST.MagPrice_Instance, 0) AS MagPrice_InstanceHST,
				p.Product_Instance,
				p.Product_Code,
				p.Product_Year AS Year,
				p.Product_Season AS Season,
				p.Product_Sort_Name,
				coalesce(pd.Status, p.Status) AS StatusInstance,
				coalesce(cdContractStatus.Description, cdProductStatus.Description) AS Status,
				coalesce(pd.Comment, '''') AS Comment,
				pd.QSP_Price,pd.OracleCode,
				pd.FSApplicabilityId FSApplicability,
				pd.FSDistributionLevelId FSDistributionLevel,
				pd.FSExtra_Limit_Rate,
				pd.FSIsBrochure,
				pd.TaxRegionID TaxRegion,
				pd.FSProvinceCode,
				pd.FSContent_Catalog_Code,
				CASE coalesce(pd.Language_Code, p.Lang) WHEN '''' THEN p.Lang ELSE coalesce(pd.Language_Code, p.Lang) END as Language,
				p.Type AS ProductTypeInstance,
				cdType.Description AS ProductType,
				coalesce(pm.Code, '''') AS CatalogCode,
				coalesce(pm.Program_Type, '''') AS CatalogName '

	SET @sqlStatement2 = '
	FROM			Product p '

	if(coalesce(@bIncludeBlank, 1) = 1)
	begin
		set @sqlStatement2 = @sqlStatement2 + ' LEFT OUTER '
	end

	set @sqlStatement2 = @sqlStatement2 + ' JOIN	Pricing_Details pd
					ON	pd.Product_Instance = p.Product_Instance
					AND	pd.TaxRegionID = CASE WHEN p.Type IN (46001, 46006, 46007, 46012) THEN 1 ELSE pd.TaxRegionID END
	LEFT OUTER JOIN	Pricing_Details pdHST
					ON	p.Type IN (46001, 46006, 46007, 46012)
					AND	pdHST.Product_Instance = p.Product_Instance
					AND	pdHST.Nbr_of_Issues = pd.Nbr_of_Issues
					AND	pdHST.ProgramSectionID = pd.ProgramSectionID
					AND	pdHST.Offer_Code = pd.Offer_Code
					AND	pdHST.TaxRegionID = 2
	LEFT OUTER JOIN	ProgramSection ps
					ON	ps.ID = pd.ProgramSectionID
	LEFT OUTER JOIN	Program_Master pm
					ON	pm.Program_ID = ps.Program_ID
	LEFT OUTER JOIN	QSPCanadaCommon..CodeDetail cdContractStatus
					ON	cdContractStatus.Instance = pd.Status,
				QSPCanadaCommon..CodeDetail cdType,
				QSPCanadaCommon..CodeDetail cdProductStatus
	WHERE	cdType.Instance = p.Type
	AND		cdProductStatus.Instance = p.Status '

	if(coalesce(@iProductInstance, 0) <> 0)
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND p.Product_Instance = ' + convert(varchar, @iProductInstance)
	end

	if(coalesce(@zProductCode, '') <> '')
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND p.Product_Code LIKE ''%' + @zProductCode + '%'' '
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

	if(coalesce(@zProductTypeList, '') <> '')
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND p.Type IN (' + @zProductTypeList + ') '
	end

	if(coalesce(@zCatalogCode, '') <> '')
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND pm.Code LIKE ''%' + @zCatalogCode + '%'' '
	end

	if(coalesce(@iProgramSectionID, 0) <> 0)
	begin
		set @sqlStatement2 = @sqlStatement2 + ' AND ps.ID = ' + convert(varchar, @iProgramSectionID)
	end

	set @sqlStatement2 = @sqlStatement2 + ' ORDER BY	p.Product_Year DESC, p.Product_Season DESC, p.Product_Sort_Name '

	exec(@sqlStatement + @sqlStatement2)
GO
