USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_sales_item_to_add]    Script Date: 02/14/2014 13:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Sales_item_to_add
CREATE PROCEDURE [dbo].[efrcrm_insert_sales_item_to_add] @Sales_item_to_add_no int OUTPUT, @Sale_to_add_id int, @Scratch_book_id int, @Service_type_id tinyint, @Group_name text, @Quantity_sold smallint, @Unit_price_sold decimal, @Quantity_free smallint, @Suggested_coupons text, @Sales_amount decimal, @Paid_amount decimal, @Adjusted_amount decimal, @Discount_amount decimal, @Sales_commission_amount decimal, @Sponsor_commission_amount decimal, @Nb_units_sold decimal, @Manual_product_description varchar(255) AS
begin

insert into Sales_item_to_add(Sale_to_add_id, Scratch_book_id, Service_type_id, Group_name, Quantity_sold, Unit_price_sold, Quantity_free, Suggested_coupons, Sales_amount, Paid_amount, Adjusted_amount, Discount_amount, Sales_commission_amount, Sponsor_commission_amount, Nb_units_sold, Manual_product_description) values(@Sale_to_add_id, @Scratch_book_id, @Service_type_id, @Group_name, @Quantity_sold, @Unit_price_sold, @Quantity_free, @Suggested_coupons, @Sales_amount, @Paid_amount, @Adjusted_amount, @Discount_amount, @Sales_commission_amount, @Sponsor_commission_amount, @Nb_units_sold, @Manual_product_description)

select @Sales_item_to_add_no = SCOPE_IDENTITY()

end
GO
