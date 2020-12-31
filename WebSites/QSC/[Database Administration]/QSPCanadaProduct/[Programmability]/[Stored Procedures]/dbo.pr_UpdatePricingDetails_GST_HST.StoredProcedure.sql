USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdatePricingDetails_GST_HST]    Script Date: 06/07/2017 09:18:05 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[pr_UpdatePricingDetails_GST_HST]

	@iMagPriceInstanceGST	int,
	@iMagPriceInstanceHST	int,
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
	@fAdCost			numeric(10, 2),
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
	@fBaseRemitRate  numeric(10, 2),
	@zListAgentCode		varchar(5),
	@zQSPAgencyCode 	varchar(20)	

AS

	DECLARE @fConvertedPrice		numeric(10,2)
	DECLARE @fConvertedNewsStandPrice	numeric(10,2)
	DECLARE @iOfferCode			int
	DECLARE @iProductType		int
	

	SELECT	@iProductType = p.Type
	FROM		Product p,
			Pricing_Details pd
	WHERE	pd.Product_Instance = p.Product_Instance
	AND		pd.MagPrice_Instance = @iMagPriceInstanceGST


	IF(@iProductType = 46001)
	BEGIN
		SELECT	@fConvertedPrice = @fBasePrice * @fConversionRate / 100

		SELECT	@fConvertedNewsStandPrice = @fNewsStandPrice / CASE @fConversionRate WHEN 0 THEN 1 ELSE @fConversionRate END * 100


		IF(@iNumberOfIssues <> (SELECT pd.Nbr_of_Issues FROM Pricing_Details pd WHERE MagPrice_Instance = @iMagPriceInstanceGST))
		BEGIN
			-- Check if an offer code already exists
			SELECT	DISTINCT 
					@iOfferCode = pdTo.Offer_Code
			FROM		Pricing_Details pdFrom,
					Product pFrom,
					Product pTo,
					Pricing_Details pdTo
			WHERE	pdFrom.MagPrice_Instance = @iMagPriceInstanceGST
			AND		pFrom.Product_Instance = pdFrom.Product_Instance
			AND		pTo.Product_Code = pFrom.Product_Code
			AND		pTo.Product_Year = pFrom.Product_Year
			AND		pTo.Product_Season = pFrom.Product_Season
			AND		pdTo.Product_Instance = pTo.Product_Instance
			AND		pdTo.Nbr_of_Issues = @iNumberOfIssues
		
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
			SELECT @iOfferCode = pd.Offer_Code FROM Pricing_Details pd WHERE pd.MagPrice_Instance = @iMagPriceInstanceGST
		END
	END
	ELSE
	BEGIN
		SET @fConvertedPrice = 0
		SET @fConvertedNewsStandPrice = 0
		SET @iOfferCode = 0
	END

	-- GST
	UPDATE	Pricing_Details
	SET		Status = @iStatus,
			Remit_Rate = @fRemitRate / 100,
			Nbr_of_Issues = @iNumberOfIssues,
			NewsStand_Price_Yr = @fNewsStandPrice,
			Basic_Price_Yr = @fBasePrice,
			QSP_Price = @fQSPPriceGST,
			EffortKeyRequired = convert(bit, @iEffortKeyRequired),
			Effort_Key = @zEffortKey,
			EffectiveDate = @dEffectiveDate,
			EndDate = @dEndDate,
			NewsStandPriceOriginalCurrency = @fConvertedNewsStandPrice,
			ConversionRate = @fConversionRate / 100,
			Comment = @zComment,
			DateSubmitted = @dDateSubmitted,
			BasePriceOriginalCurrency = @fConvertedPrice,
			FSExtra_Limit_Rate = CONVERT(numeric(18, 0), @iFSExtraLimitRate),
			FSIsBrochure = @bFSIsBrochure,
			FSApplicabilityId = @iFSApplicabilityId,
			FSDistributionLevelID = @iFSDistributionLevelID,
			FSCatalog_Product_Code = @zFSCatalogProductCode,
			FSContent_Catalog_Code = @zFSContentCatalogCode,
			FSProgram_ID = @iFSProgramID,
			OracleCode = @zOracleCode,
			InternetApproval = convert(bit, @iInternetApproval),
			ABCCode = CASE @zABCCode WHEN '' THEN null ELSE @zABCCode END,
			AdInCatalog = convert(bit, @iAdInCatalog),
			AdPageSizeID = @iAdPageSizeID,
			AdCost = @fAdCost,
			AdCostCurrency = @iAdPaymentCurrency,
			ListingLevelID = @iListingLevelID,
			ListingCopyText = @zListingCopyText,
			QSPPremiumID = @iQSPPremiumID,
			Offer_Code = @iOfferCode,
			prdPremiumInd = @zPremiumInd,
			prdPremiumCode = @zPremiumCode,
			prdPremiumCopy = @zPremiumCopy,
			FSProvinceCode = @zFSProvinceCode,
			ContractFormReceived=@iContractFormReceived,
			MagazineCoverFilename=@zMagazineCoverFilename,
			CatalogAdFilename=@zCatalogAdFilename,
			CatalogPageNumber =@iCatalogPageNumber, 
			PlacementLevel = @iPlacementLevel,
			ContractComment = @zContractComments,
			PrinterComment = @zPrinterComments,
			QSPCAListingCopyText	= @zQspcaListingCopyText,
			BasePriceSansPostage=@fBasePriceSansPostage,
			PostageRemitRate=@fPostageRemitRate /100,
			PostageAmount=@fPostageAmount ,
			BaseRemitRate=@fBaseRemitRate /100,
			ListAgentCode = NULLIF(@zListAgentCode , ''),
			QSPAgencyCode =  NULLIF(@zQSPAgencyCode , ''),
         Log_Dt = GETDATE()
	WHERE	MagPrice_Instance = @iMagPriceInstanceGST

	-- HST
	UPDATE	Pricing_Details
	SET		Status = @iStatus,
			Remit_Rate = @fRemitRate / 100,
			Nbr_of_Issues = @iNumberOfIssues,
			NewsStand_Price_Yr = @fNewsStandPrice,
			Basic_Price_Yr = @fBasePrice,
			QSP_Price = @fQSPPriceHST,
			EffortKeyRequired = convert(bit, @iEffortKeyRequired),
			Effort_Key = @zEffortKey,
			EffectiveDate = @dEffectiveDate,
			EndDate = @dEndDate,
			NewsStandPriceOriginalCurrency = @fConvertedNewsStandPrice,
			ConversionRate = @fConversionRate / 100,
			Comment = @zComment,
			DateSubmitted = @dDateSubmitted,
			BasePriceOriginalCurrency = @fConvertedPrice,
			FSExtra_Limit_Rate = CONVERT(numeric(18, 0), @iFSExtraLimitRate),
			FSIsBrochure = @bFSIsBrochure,
			FSApplicabilityId = @iFSApplicabilityId,
			FSDistributionLevelID = @iFSDistributionLevelID,
			FSCatalog_Product_Code = @zFSCatalogProductCode,
			FSContent_Catalog_Code = @zFSContentCatalogCode,
			FSProgram_ID = @iFSProgramID,
			OracleCode = @zOracleCode,
			InternetApproval = convert(bit, @iInternetApproval),
			ABCCode = CASE @zABCCode WHEN '' THEN null ELSE @zABCCode END,
			AdInCatalog = convert(bit, @iAdInCatalog),
			AdPageSizeID = @iAdPageSizeID,
			AdCost = @fAdCost,
			AdCostCurrency = @iAdPaymentCurrency,
			ListingLevelID = @iListingLevelID,
			ListingCopyText = @zListingCopyText,
			QSPPremiumID = @iQSPPremiumID,
			Offer_Code = @iOfferCode,
			prdPremiumInd = @zPremiumInd,
			prdPremiumCode = @zPremiumCode,
			prdPremiumCopy = @zPremiumCopy,
			FSProvinceCode = @zFSProvinceCode,
			ContractFormReceived = @iContractFormReceived,
			MagazineCoverFilename =@zMagazineCoverFilename,
			CatalogAdFilename =@zCatalogAdFilename,
			CatalogPageNumber =@iCatalogPageNumber, 
			PlacementLevel = @iPlacementLevel,
			ContractComment = @zContractComments,
			PrinterComment = @zPrinterComments,
			QSPCAListingCopyText	= @zQspcaListingCopyText,
			BasePriceSansPostage=@fBasePriceSansPostage,
			PostageRemitRate=@fPostageRemitRate /100,
			PostageAmount=@fPostageAmount ,
			BaseRemitRate=@fBaseRemitRate /100,
			ListAgentCode = NULLIF(@zListAgentCode , ''),
			QSPAgencyCode = NULLIF(@zQSPAgencyCode , ''),
         Log_Dt = GETDATE()		
	WHERE	MagPrice_Instance = @iMagPriceInstanceHST
GO
