USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[ActiveSalesProductBreakdown]    Script Date: 02/14/2014 13:01:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE view [dbo].[ActiveSalesProductBreakdown] /* creator. */ /* view column name,... */
  as 
	select Sale.Sales_ID,
    		sum(case when Product_Class.Description = 'Scratchcard' then Sales_Item.Sales_Amount else 0 end) as Scratchcard,
    		sum(case when Product_Class.Description = 'Candies' then Sales_Item.Sales_Amount else 0 end) as Candies,
    		sum(case when Product_Class.Description = 'Brochure' then Sales_Item.Sales_Amount else 0 end) as Brochure 
	from    Product_Class 
		join(((ActiveSaleIDView join Sale on ActiveSaleIDView.Sales_ID = Sale.Sales_ID) 
		join Sales_Item on Sale.Sales_ID = Sales_Item.Sales_ID) 
		join Scratch_Book on Sales_Item.Scratch_Book_ID = Scratch_Book.Scratch_Book_ID) on  
			Product_Class.Product_Class_ID = Scratch_Book.Product_Class_ID
--fA-41,B-15
    	group by Sale.Sales_ID
GO
