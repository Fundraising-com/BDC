USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[ActiveSaleIDView]    Script Date: 02/14/2014 13:01:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE view [dbo].[ActiveSaleIDView] /* creator. */ /* view column name,... */
  as 
	select 	Partner.Partner_ID,
		Promotion.Description,
		Sale.Sales_ID,
		Lead.Promotion_ID,
		Sale.Box_Return_Date,
		Sale.Reship_Date,
		Sale.Actual_Ship_Date 
	from	(Sale join Lead on Sale.Lead_Id = Lead.Lead_ID) 
		join((Client join Promotion on Client.Promotion_ID = Promotion.Promotion_ID) 
		join Partner on Promotion.Partner_ID = Partner.Partner_ID) on Lead.Lead_ID = Client.Lead_ID 
	where	((Partner.Partner_ID = 8 and Sale.Actual_Ship_Date is not null) and /* only sales with 'real ship dates' */
    		((Sale.Reship_Date is null and Sale.Box_Return_Date is null) or /* all sales with no box return and reship */
    		(Sale.Reship_Date is not null and Sale.Box_Return_Date is not null))) /* include only box returned that were reshipped */
--fA-41,B-15    
	group by Partner.Partner_ID,Promotion.Description,Sale.Sales_ID,Lead.Promotion_ID,Sale.Box_Return_Date,Sale.Reship_Date,Sale.Actual_Ship_Date
GO
