USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_sales_item]    Script Date: 02/14/2014 13:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Sales_item
-- Generate insert stored proc for Sales_item
CREATE PROCEDURE [dbo].[efrcrm_insert_sales_item] @Sales_id int, @Sales_item_no smallint output , @Scratch_book_id int, @Service_type_id tinyint, @Product_class_id tinyint, 
@Group_name text, @Quantity_sold smallint, @Unit_price_sold decimal(15,4), @Quantity_free smallint, @Suggested_coupons text, 
@Sales_amount decimal(15,4), @Paid_amount decimal(15,4), @Adjusted_amount decimal(15,4), @Discount_amount decimal(15,4), 
@Sales_commission_amount decimal(15,4), @Sponsor_commission_amount decimal(15,4), @Nb_units_sold decimal(15,4), @Manual_product_description varchar(255),
@Participant_id int = null 
AS
begin

declare @sales_item_Id smallint
declare @errorCode int 

select @sales_item_Id = Isnull(max(Sales_item_no),0) + 1 from sales_item where sales_id = @Sales_id

insert into Sales_item(
	Sales_id,
	Sales_item_no, 
	Scratch_book_id, 
	Service_type_id, 
	Product_class_id, 
	Group_name, 
	Quantity_sold, 
	Unit_price_sold, 
	Quantity_free, 
	Suggested_coupons, 
	Sales_amount, 
	Paid_amount, 
	Adjusted_amount, 
	Discount_amount, 
	Sales_commission_amount, 
	Sponsor_commission_amount, 
	Nb_units_sold, 
	Manual_product_description,
	Participant_id
	) 
values(
	@Sales_id,
	@sales_item_Id, 
	@Scratch_book_id, 
	@Service_type_id, 
	@Product_class_id, 
	@Group_name, 
	@Quantity_sold, 
	@Unit_price_sold, 
	@Quantity_free, 
	@Suggested_coupons, 
	@Sales_amount, 
	@Paid_amount, 
	@Adjusted_amount, 
	@Discount_amount, 
	@Sales_commission_amount, 
	@Sponsor_commission_amount, 
	@Nb_units_sold, 
	@Manual_product_description,
	@Participant_id
	)

-- select @Sales_id = SCOPE_IDENTITY()

SET @errorCode = @@error
IF (@errorCode <> 0)
	Set @Sales_item_no = -1
ELSE
	Set @Sales_item_no = @sales_item_Id

end
GO
