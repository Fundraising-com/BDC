USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[rs_sale_items]    Script Date: 02/14/2014 13:08:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[rs_sale_items] @sale_id int AS

SELECT 
	product_code
	, [description]
	, quantity_sold
	, quantity_free
	---nb_units_sold
	, convert(decimal(6,2), unit_price_sold) as unit_price_sold
	, convert(decimal(6,2), isnull(discount_amount,0)) as discount_amount
	, (quantity_sold * unit_price_sold) - isnull(discount_amount,0) as total
	, part.participant_id
	, part.first_name
	, part.last_name
FROM         
	dbo.sale INNER JOIN
	dbo.sales_item ON dbo.sale.sales_id = dbo.sales_item.sales_id INNER JOIN
	dbo.scratch_book ON dbo.sales_item.scratch_book_id = dbo.scratch_book.scratch_book_id
	left join participant part on part.participant_id = sales_item.participant_id
where 
quantity_sold > 0 and
sale.sales_id = @sale_id
--order by product_code

union all

select cs.product_code
	, cs.[description] 
	, 0
	, (quantity_sold + quantity_free) * sics.Sheet_Per_Booklet
	, 0 
	, 0
	, 0
	, NULL
	, NULL
	, NULL
from Sales_Item si 
inner join Sales_Item_Coupon_Sheet sics on si.sales_item_no = sics.Sales_Item_No 
	AND si.sales_id = sics.Sales_ID
inner join Coupon_Sheet cs on cs.Coupon_Sheet_ID = sics.Coupon_Sheet_ID
where si.sales_id = @sale_id
--order by product_code
GO
