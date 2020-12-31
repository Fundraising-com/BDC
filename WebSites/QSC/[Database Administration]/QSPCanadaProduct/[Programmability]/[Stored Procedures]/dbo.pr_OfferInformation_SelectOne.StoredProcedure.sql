USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_OfferInformation_SelectOne]    Script Date: 06/07/2017 09:17:56 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_OfferInformation_SelectOne] 

@zUMC varchar(4),
@zSeason varchar(1),
@iYear int,
@iNumberOfIssues int

AS


select 	coalesce(pd.Remit_Rate, 0) * 100 as Remit_Rate,
	coalesce(pd.AdInCatalog, 0) as AdInCatalog,
	coalesce(pd.AdPageSizeID, 0) as AdSize,
	coalesce(pd.AdCost, 0) as AdCost,
	coalesce(pd.AdCostCurrency, 0) as AdCostCurrency,
	coalesce(pd.ListingLevelID, 0) as ListingLevel,
	coalesce(pd.ListingCopyText, '') as ListingCopyText,
	coalesce(pd.EffortKeyRequired, 0) as EffortKeyRequired,
	coalesce(pd.Effort_Key, '') as Effort_Key,
	coalesce(pd.NewsStand_Price_Yr, 0) as NewsStandPrice,
	pd.Nbr_Of_Issues,
	coalesce(pd.Basic_Price_Yr, 0) as BasePrice,
	pd.InternetApproval
from	pricing_details pd
where	pd.Pricing_Season = @zSeason
	and pd.Pricing_Year = @iYear
	and pd.Product_Code = @zUMC
	and pd.Nbr_of_Issues = @iNumberOfIssues
order by pd.magprice_instance desc
GO
