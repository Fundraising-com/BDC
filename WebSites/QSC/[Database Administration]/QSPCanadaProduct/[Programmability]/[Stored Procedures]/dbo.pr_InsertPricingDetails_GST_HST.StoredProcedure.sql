USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_InsertPricingDetails_GST_HST]    Script Date: 06/07/2017 09:17:55 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[pr_InsertPricingDetails_GST_HST]

	@iProductInstance		int,
	@zProductCode			varchar(20),
	@iYear				int,
	@zSeason			varchar(1),
	@iCatalogSectionID		int,
	@iStatus			int,
	@dEffectiveDate		datetime,
	@dEndDate			datetime,
	@dDateSubmitted		datetime,
	@fRemitRate			numeric(10, 2),
	@fConversionRate		numeric(10, 2),
	@fNewsStandPrice		numeric(10, 2),
	@iListingLevelID		int,
	@zListingCopyText		varchar(500),
	@iAdInCatalog			int,
	@iAdPageSizeID		int,
	@iAdPaymentCurrency		int,
	@fAdCost			numeric(10,2),
	@zComment			varchar(200),
	@iEffortKeyRequired		int,
	@zEffortKey			varchar(40),
	@iNumberOfIssues		int,
	@fBasePrice			numeric(10, 2),
	@fQSPPriceGST		numeric(10, 2),
	@fQSPPriceHST		numeric(10, 2),
	@iInternetApproval		int,
	@zABCCode			varchar(20),
	@iQSPPremiumID		int,
	@zOracleCode			varchar(50),
	@iFSApplicabilityId		int,
	@iFSDistributionLevelID		int,
	@iFSExtraLimitRate		int,
	@bFSIsBrochure		bit,
	@zFSCatalogProductCode	varchar(50),
	@zFSContentCatalogCode	varchar(50),
	@iFSProgramID			int,
	@iTaxRegionID			int,
	@zPremiumInd			varchar(1),
	@zPremiumCode		varchar(50),
	@zPremiumCopy		varchar(500),
	@zFSProvinceCode		varchar(10),
	@zUserID			varchar(30),
	@iContractFormReceived int,
	@zMagazineCoverFilename varchar( 100),
	@zCatalogAdFilename  varchar( 100),
	@iCatalogPageNumber int,
	@iPlacementLevel int,
	@zContractComments varchar(500),
	@zPrinterComments varchar(500),
	@zQspcaListingCopyText varchar(500),
	@fBasePriceSansPostage numeric(10, 2),
	@fPostageRemitRate numeric(10, 2),
	@fPostageAmount numeric(10, 2),
	@fBaseRemitRate numeric(10, 2),
	@zListAgentCode		varchar(5),
	@zQSPAgencyCode 	varchar(20)

AS

	DECLARE @iOfferCode			int
	DECLARE @fConvertedPrice		numeric(10,2)
	DECLARE @fConvertedNewsStandPrice	numeric(10,2)
	DECLARE @iProductType		int


	SELECT	@iProductType = p.Type
	FROM		Product p
	WHERE	p.Product_Instance = @iProductInstance


	IF(@iProductType = 46001)
	BEGIN	
		SELECT	@fConvertedPrice = @fBasePrice * @fConversionRate / 100

		SELECT	@fConvertedNewsStandPrice = @fNewsStandPrice / CASE @fConversionRate WHEN 0 THEN 1 ELSE @fConversionRate END * 100

		-- Check if an offer code already exists
		SELECT	DISTINCT 
				@iOfferCode = pd.Offer_Code
		FROM		Product p,
				Pricing_Details pd
		WHERE	p.Product_Instance = @iProductInstance
		AND		pd.Product_Instance = p.Product_Instance
		AND		pd.Nbr_of_Issues = @iNumberOfIssues
	
		if(coalesce(@iOfferCode, 0) = 0)
		begin
			-- Create a new offer code
			create table #temp
			(
				 NextInstance int
			)
		
			insert into #temp exec qspcanadaordermanagement..InsertNextInstance 19 -- OfferCodeNext
			select @iOfferCode = nextinstance from #temp
			truncate table #temp
				
			drop table #temp
		end
	END
	ELSE
	BEGIN
		SET @fConvertedPrice = 0
		SET @fConvertedNewsStandPrice = 0
		SET @iOfferCode = 0
	END

	-- GST
	INSERT INTO	Pricing_Details
			(Product_Instance,
			Pricing_Year,
			Pricing_Season,
			Product_Code,
			Program_ID,
			Program_Type,
			ProgramSectionID,
			Offer_Code,
			Status,
			Remit_Rate,
			Nbr_of_Issues,
			Duration,
			Duration_Measure,
			NewsStand_Price_Yr,
			Basic_Price_Yr,
			QSP_Price,
			EffortKeyRequired,
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
			ABCCode,
			AdInCatalog,
			AdPageSizeID,
			AdCost,
			AdCostCurrency,
			ListingLevelID,
			ListingCopyText,
			QSPPremiumID,
			prdPremiumInd,
			prdPremiumCode,
			prdPremiumCopy,
			FSProvinceCode,
			ContractFormReceived,
			MagazineCoverFilename,
			CatalogAdFilename,
			CatalogPageNumber, 
			PlacementLevel,
			ContractComment,
			PrinterComment,
			QSPCAListingCopyText	,
			BasePriceSansPostage,
			PostageRemitRate,
			PostageAmount,
			BaseRemitRate,
			ListAgentCode,
			QSPAgencyCode
			)
	SELECT	@iProductInstance,
			@iYear,
			@zSeason,
			@zProductCode,
			0,
			'',
			@iCatalogSectionID,
			@iOfferCode,
			@iStatus,
			@fRemitRate / 100,
			@iNumberOfIssues,
			0,
			'',
			@fNewsStandPrice,
			@fBasePrice,
			@fQSPPriceGST,
			convert(bit, @iEffortKeyRequired),
			@zEffortKey,
			@zUserID,
			getdate(),
			@dEffectiveDate,
			@dEndDate,
			@fConvertedNewsStandPrice,
			@fConversionRate / 100,
			@zComment,
			@dDateSubmitted,
			@fConvertedPrice,
			1,
			null,
			CONVERT(numeric(18, 0), @iFSExtraLimitRate),
			@bFSIsBrochure,
			@iFSApplicabilityId,
			@iFSDistributionLevelID,
			p.Lang,
			@zFSCatalogProductCode,
			@zFSContentCatalogCode,
			@iFSProgramID,
			p.OracleCode,
			convert(bit, @iInternetApproval),
			CASE @zABCCode WHEN '' THEN null ELSE @zABCCode END,
			convert(bit, @iAdInCatalog),
			@iAdPageSizeID,
			@fAdCost,
			@iAdPaymentCurrency,
			@iListingLevelID,
			@zListingCopyText,
			@iQSPPremiumID,
			@zPremiumInd,
			@zPremiumCode,
			@zPremiumCopy,
			@zFSProvinceCode,
			@iContractFormReceived ,
			@zMagazineCoverFilename ,
			@zCatalogAdFilename  ,
			@iCatalogPageNumber ,
			@iPlacementLevel ,
			@zContractComments ,
			@zPrinterComments,
			@zQspcaListingCopyText	,
			@fBasePriceSansPostage,
			@fPostageRemitRate/100,
			@fPostageAmount,
			@fBaseRemitRate/100,
			NULLIF(@zListAgentCode , ''),
			NULLIF(@zQSPAgencyCode , '')
	FROM		Product p
	WHERE	p.Product_Instance = @iProductInstance


	-- HST
	INSERT INTO	Pricing_Details
			(Product_Instance,
			Pricing_Year,
			Pricing_Season,
			Product_Code,
			Program_ID,
			Program_Type,
			ProgramSectionID,
			Offer_Code,
			Status,
			Remit_Rate,
			Nbr_of_Issues,
			Duration,
			Duration_Measure,
			NewsStand_Price_Yr,
			Basic_Price_Yr,
			QSP_Price,
			EffortKeyRequired,
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
			ABCCode,
			AdInCatalog,
			AdPageSizeID,
			AdCost,
			AdCostCurrency,
			ListingLevelID,
			ListingCopyText,
			QSPPremiumID,
			prdPremiumInd,
			prdPremiumCode,
			prdPremiumCopy,
			FSProvinceCode,
			ContractFormReceived,
			MagazineCoverFilename,
			CatalogAdFilename,
			CatalogPageNumber, 
			PlacementLevel,
			ContractComment,
			PrinterComment,
			QSPCAListingCopyText	,
			BasePriceSansPostage,
			PostageRemitRate,
			PostageAmount,
			BaseRemitRate,
			ListAgentCode,
			QSPAgencyCode
			)
	SELECT	@iProductInstance,
			@iYear,
			@zSeason,
			@zProductCode,
			0,
			'',
			@iCatalogSectionID,
			@iOfferCode,
			@iStatus,
			@fRemitRate / 100,
			@iNumberOfIssues,
			0,
			'',
			@fNewsStandPrice,
			@fBasePrice,
			@fQSPPriceHST,
			convert(bit, @iEffortKeyRequired),
			@zEffortKey,
			@zUserID,
			getdate(),
			@dEffectiveDate,
			@dEndDate,
			@fConvertedNewsStandPrice,
			@fConversionRate / 100,
			@zComment,
			@dDateSubmitted,
			@fConvertedPrice,
			2,
			null,
			CONVERT(numeric(18, 0), @iFSExtraLimitRate),
			@bFSIsBrochure,
			@iFSApplicabilityId,
			@iFSDistributionLevelID,
			p.Lang,
			@zFSCatalogProductCode,
			@zFSContentCatalogCode,
			@iFSProgramID,
			p.OracleCode,
			convert(bit, @iInternetApproval),
			CASE @zABCCode WHEN '' THEN null ELSE @zABCCode END,
			convert(bit, @iAdInCatalog),
			@iAdPageSizeID,
			@fAdCost,
			@iAdPaymentCurrency,
			@iListingLevelID,
			@zListingCopyText,
			@iQSPPremiumID,
			@zPremiumInd,
			@zPremiumCode,
			@zPremiumCopy,
			@zFSProvinceCode,
			@iContractFormReceived ,
			@zMagazineCoverFilename ,
			@zCatalogAdFilename  ,
			@iCatalogPageNumber ,
			@iPlacementLevel ,
			@zContractComments ,
			@zPrinterComments,
			@zQspcaListingCopyText	,
			@fBasePriceSansPostage,
			@fPostageRemitRate/100,
			@fPostageAmount,
			@fBaseRemitRate/100,
			NULLIF(@zListAgentCode , ''),
			NULLIF(@zQSPAgencyCode , '')
			
	FROM		Product p
	WHERE	p.Product_Instance = @iProductInstance
GO
