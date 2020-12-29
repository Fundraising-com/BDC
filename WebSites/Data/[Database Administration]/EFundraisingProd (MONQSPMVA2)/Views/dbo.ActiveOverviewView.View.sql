USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[ActiveOverviewView]    Script Date: 02/14/2014 13:01:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/****** Object:  View dbo.ActiveOverviewView    Script Date: 2003-02-22 20:34:16 ******/
CREATE view [dbo].[ActiveOverviewView] /* creator. */ /* view column name,... */
  as 
	select 	ActiveSaleIDView.Partner_ID,
		ActiveSaleIDView.Promotion_ID,
		ActiveSaleIDView.Description,
		ActiveLeadByPromotion.CountOfLead_ID,
    		Count(ActiveSalesProductBreakdown.Sales_ID) as CountOfSales_ID,
    		Sum(ActiveSalesProductBreakdown.Brochure) as SumOfBrochure,
    		Sum(ActiveSalesProductBreakdown.Candies) as SumOfCandies,
   		Sum(ActiveSalesProductBreakdown.Scratchcard) as SumOfScratchcard 
	from	(ActiveSaleIDView 
		join ActiveLeadByPromotion on ActiveSaleIDView.Promotion_ID = ActiveLeadByPromotion.Promotion_ID) 
		join ActiveSalesProductBreakdown on ActiveSaleIDView.Sales_ID = ActiveSalesProductBreakdown.Sales_ID
--fA-41,B-15    
	group by ActiveSaleIDView.Partner_ID,
		ActiveSaleIDView.Promotion_ID,
		ActiveSaleIDView.Description,
		ActiveLeadByPromotion.CountOfLead_ID
GO
