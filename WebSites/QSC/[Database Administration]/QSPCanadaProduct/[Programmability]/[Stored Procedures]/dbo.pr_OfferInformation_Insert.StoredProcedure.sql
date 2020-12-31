USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_OfferInformation_Insert]    Script Date: 06/07/2017 09:17:56 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_OfferInformation_Insert]

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
	@zListingCopyText		varchar(500),
	@zEffortKey			varchar(40),
	@iNumberOfIssues		int,
	@fBasePrice			numeric(10,2),
	@iInternetApproval		int,
	@zUserName			varchar(50)

AS

	DECLARE	@zLang		varchar(10)

	SELECT	top 1
			@zLang = p.Lang
	FROM		Product p
	WHERE	p.Product_Code = @zUMC
	AND		p.Product_Year = @iYear
	AND		p.Product_Season = @zSeason


	INSERT INTO	Pricing_Details
	(Pricing_Season,
	Pricing_Year,
	Product_Code,
	Program_Type,
	ProgramSectionID,
	Offer_Code,
	Status,
	QSP_Price,
	Logged_By,
	Log_Dt,
	TaxRegionID,
	Language_Code,
	Remit_Rate,
	NewsStand_Price_Yr,
	AdInCatalog,
	AdPageSizeID,
	AdCost,
	AdCostCurrency,
	ListingLevelID,
	ListingCopyText,
	Effort_Key,
	Nbr_of_Issues,
	Basic_Price_Yr,
	InternetApproval)
	VALUES
	(@zSeason,
	@iYear,
	@zUMC,
	'',
	0,
	0,
	30602,
	0,
	@zUserName,
	getdate(),
	0,
	@zLang,
	@fRemitRate / 100,
	@fNewsStandPrice,
	convert(bit, @iAdInCatalog),
	@iAdSize,
	@fAdCost,
	@iAdCostCurrency,
	@iListingLevel,
	@zListingCopyText,
	@zEffortKey,
	@iNumberOfIssues,
	@fBasePrice,
	convert(bit, @iInternetApproval))
GO
