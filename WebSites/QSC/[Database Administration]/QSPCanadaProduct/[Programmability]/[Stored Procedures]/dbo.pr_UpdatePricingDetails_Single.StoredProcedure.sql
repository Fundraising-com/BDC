USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdatePricingDetails_Single]    Script Date: 06/07/2017 09:18:05 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_UpdatePricingDetails_Single]

	@iMagPriceInstance		int,
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
	@fQSPPrice			numeric(10, 2),
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
	@zQspcaListingCopyText varchar(500),
	@fAddlHandlingFee money

AS

	UPDATE	pd
	SET		Status = @iStatus,
			Nbr_of_Issues = @iNumberOfIssues,
			QSP_Price = @fQSPPrice,
			EffectiveDate = @dEffectiveDate,
			EndDate = @dEndDate,
			Comment = @zComment,
			DateSubmitted = @dDateSubmitted,
			TaxRegionID = @iTaxRegionID,
			FSExtra_Limit_Rate = CASE p.Type WHEN 46004 THEN CONVERT(numeric(18, 0), @iFSExtraLimitRate) ELSE NULL END,
			FSIsBrochure = CASE p.Type WHEN 46004 THEN @bFSIsBrochure ELSE NULL END,
			FSApplicabilityId = CASE p.Type WHEN 46004 THEN @iFSApplicabilityId ELSE NULL END,
			FSDistributionLevelID = CASE p.Type WHEN 46004 THEN @iFSDistributionLevelID ELSE NULL END,
			FSCatalog_Product_Code = @zFSCatalogProductCode,
			FSContent_Catalog_Code = CASE p.Type WHEN 46004 THEN @zFSContentCatalogCode ELSE NULL END,
			FSProgram_ID = CASE p.Type WHEN 46004 THEN @iFSProgramID ELSE NULL END,
			OracleCode = @zOracleCode,
			InternetApproval = convert(bit, @iInternetApproval),
			FSProvinceCode = CASE p.Type WHEN 46004 THEN @zFSProvinceCode ELSE NULL END,
			QSPCAListingCopyText = @zQspcaListingCopyText,
			AddlHandlingFee = @fAddlHandlingFee,
         Log_Dt = GETDATE()
	FROM		Pricing_Details pd,
			Product p
	WHERE	p.Product_Instance = pd.Product_Instance
	AND		pd.MagPrice_Instance = @iMagPriceInstance
GO
