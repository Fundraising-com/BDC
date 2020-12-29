USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_sales_item]    Script Date: 02/14/2014 13:08:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Sales_item
CREATE  PROCEDURE [dbo].[efrcrm_update_sales_item] @Sales_id int, 
@Sales_item_no smallint, 
@Scratch_book_id int, 
@Service_type_id tinyint, 
@Product_class_id tinyint, 
@Group_name text, 
@Quantity_sold smallint, 
@Unit_price_sold decimal(15, 4), 
@Quantity_free smallint, 
@Suggested_coupons text, 
@Sales_amount decimal(15, 4), 
@Paid_amount decimal(15, 4), 
@Adjusted_amount decimal(15, 4), 
@Discount_amount decimal(15, 4), 
@Sales_commission_amount decimal(15, 4), 
@Sponsor_commission_amount decimal(15, 4), 
@Nb_units_sold decimal(15, 4), 
@Manual_product_description varchar(255),
@participant_id int AS
begin


declare @errorCode int	

	
	update Sales_item 
	set 
	
	Scratch_book_id=@Scratch_book_id, 
	Service_type_id=@Service_type_id, 
	Product_class_id=@Product_class_id, 
	Group_name=@Group_name, 
	Quantity_sold=@Quantity_sold, 
	Unit_price_sold=@Unit_price_sold,
	Quantity_free=@Quantity_free, 
	--Suggested_coupons=@Suggested_coupons, 
	Sales_amount=@Sales_amount,
	Paid_amount=@Paid_amount, 
	Adjusted_amount=@Adjusted_amount, 
	Discount_amount=@Discount_amount, 
	Sales_commission_amount=@Sales_commission_amount, 
	Sponsor_commission_amount=@Sponsor_commission_amount, 
	Nb_units_sold=@Nb_units_sold, 
	Manual_product_description=@Manual_product_description,
	participant_id =@participant_id 
	where Sales_id=@Sales_id and Sales_item_no=@Sales_item_no



end
GO
