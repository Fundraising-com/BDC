USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sales_items_by_sales_id]    Script Date: 02/14/2014 13:05:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrcrm_get_sales_items_by_sales_id] @Sales_id int as
begin

SELECT 
   Sales_id, Sales_item_no, Scratch_book_id, Service_type_id, Product_class_id, Group_name, Quantity_sold,       
Unit_price_sold, Quantity_free, Suggested_coupons, Sales_amount, Paid_amount, Adjusted_amount, Discount_amount, 
   Sales_commission_amount, Sponsor_commission_amount, Nb_units_sold, Manual_product_description, participant_id 
FROM Sales_item
WHERE Sales_id=@Sales_id

end
GO
