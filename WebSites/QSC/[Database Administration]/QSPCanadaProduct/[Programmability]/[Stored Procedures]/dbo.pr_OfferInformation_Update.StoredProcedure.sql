USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_OfferInformation_Update]    Script Date: 06/07/2017 09:17:56 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_OfferInformation_Update]

	@zUMC			varchar(4),
	@zSeason			varchar(1),
	@iYear				int,
	@fRemitRate			numeric(10,2),
	@fNewsStandPrice		numeric(10,2),
	@iAdInCatalog			int,
	@iAdSize			int,
	@fAdCost			numeric(10,2),
	@iAdCostCurrency		int,
	@iListingLevel			int,
	@zListingCopyText		varchar(1000),
	@zEffortKey			varchar(40),
	@iNumberOfIssues		int,
	@fBasePrice			numeric(10,2),
	@iInternetApproval		int

AS

	UPDATE	Pricing_Details
	SET		Remit_Rate = @fRemitRate / 100,
			NewsStand_Price_Yr = @fNewsStandPrice,
			AdInCatalog = convert(bit, @iAdInCatalog),
			AdPageSizeID = @iAdSize,
			AdCost = @fAdCost,
			AdCostCurrency = @iAdCostCurrency,
			ListingLevelID = @iListingLevel,
			ListingCopyText = @zListingCopyText,
			Effort_Key = @zEffortKey,
			Basic_Price_Yr = @fBasePrice,
			InternetApproval = convert(bit, @iInternetApproval)
	WHERE	Product_Code = @zUMC
	AND		Pricing_Season = @zSeason
	AND		Pricing_Year = @iYear
	AND		Nbr_of_Issues = @iNumberOfIssues
GO
