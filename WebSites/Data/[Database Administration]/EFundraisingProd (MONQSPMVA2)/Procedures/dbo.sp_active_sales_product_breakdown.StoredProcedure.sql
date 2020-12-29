USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_active_sales_product_breakdown]    Script Date: 02/14/2014 13:08:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_active_sales_product_breakdown](@date_from datetime  = NULL, @date_to datetime = NULL) AS
	SELECT     Sale.Sales_ID, SUM(CASE WHEN Product_Class.Description = 'Scratchcard' THEN Sales_Item.Sales_Amount ELSE 0 END) AS Scratchcard, 
                      SUM(CASE WHEN Product_Class.Description = 'Candies' THEN Sales_Item.Sales_Amount ELSE 0 END) AS Candies, 
                      SUM(CASE WHEN Product_Class.Description = 'Brochure' THEN Sales_Item.Sales_Amount ELSE 0 END) AS Brochure
	FROM         Product_Class JOIN
                      (((ActiveSaleIDView JOIN
                      Sale ON ActiveSaleIDView.Sales_ID = Sale.Sales_ID) JOIN
                      Sales_Item ON Sale.Sales_ID = Sales_Item.Sales_ID) JOIN
                      Scratch_Book ON Sales_Item.Scratch_Book_ID = Scratch_Book.Scratch_Book_ID) ON 
                      Product_Class.Product_Class_ID = Scratch_Book.Product_Class_ID
	WHERE   dbo.Sale.Sales_Date BETWEEN isnull(@date_from, dbo.Sale.Sales_Date) AND isnull(@date_to, dbo.Sale.Sales_Date)	
	GROUP BY Sale.Sales_ID
GO
