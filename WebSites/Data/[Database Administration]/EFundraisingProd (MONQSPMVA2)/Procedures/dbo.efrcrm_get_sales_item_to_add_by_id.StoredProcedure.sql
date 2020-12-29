USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sales_item_to_add_by_id]    Script Date: 02/14/2014 13:05:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Sales_item_to_add
CREATE PROCEDURE [dbo].[efrcrm_get_sales_item_to_add_by_id] @Sales_item_to_add_no int AS
begin

select Sales_item_to_add_no, Sale_to_add_id, Scratch_book_id, Service_type_id, Group_name, Quantity_sold, Unit_price_sold, Quantity_free, Suggested_coupons, Sales_amount, Paid_amount, Adjusted_amount, Discount_amount, Sales_commission_amount, Sponsor_commission_amount, Nb_units_sold, Manual_product_description from Sales_item_to_add where Sales_item_to_add_no=@Sales_item_to_add_no

end
GO
