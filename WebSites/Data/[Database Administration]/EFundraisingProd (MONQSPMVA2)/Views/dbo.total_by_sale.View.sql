USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[total_by_sale]    Script Date: 02/14/2014 13:02:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View dbo.total_by_sale    Script Date: 2003-02-22 20:34:18 ******/

create view [dbo].[total_by_sale]
  as 
	select Sales_Item.Sales_ID,
		Sum(Sales_Item.Quantity_Sold) as Quantity_Sold,
		Sum(sales_item.quantity_sold*sales_item.unit_price_sold) as Original_Amount,
		Sum(Sales_Item.Discount_Amount) as Discount_Amount,
		Sum(Sales_Item.Sales_Amount) as sale_amount,
		Count(Sales_Item.Sales_Item_No) as nb_items 
	from Sales_Item
--fA-41,B-15
--fB-15
    	group by Sales_Item.Sales_ID having Sum(Sales_Item.Sales_Amount) <> 0
GO
