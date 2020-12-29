USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_sale_shipping_ratio]    Script Date: 02/14/2014 13:09:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[sp_sale_shipping_ratio](@sale_id INT = null) AS

-- retourne le sale id, total_amount, shipping fees, shipping fees discount, sale amount without shipping,  
-- shipping ratio, sale item amount, sale item ratio, product class id 
SELECT  s.sales_id as sale_id, 
	s.total_amount, 
	s.shipping_fees, 
	s.shipping_fees_discount, 
	(s.total_amount - s.shipping_fees + s.shipping_fees_discount) as sale_amount_without_shipping,
	(s.shipping_fees + s.shipping_fees_discount)/s.total_amount as shipping_ratio,
	SUM(si.sales_amount) as sale_item_amount,
	sb.product_class_id
	INTO #tb_sale_shipping_ratio
	FROM sale s, sales_item si, scratch_book sb
	WHERE s.sales_id = si.sales_id
	AND si.scratch_book_id = sb.scratch_book_id
	AND s.total_amount <> 0
	AND s.sales_id = @sale_id
	GROUP BY s.sales_id, s.total_amount,  s.shipping_fees, s.shipping_fees_discount, sb.product_class_id



-- retourne le sale id, total_amount, shipping fees, shipping fees discount, sale amount without shipping,  
-- shipping ratio, sale item amount, sale item ratio, product class id 
SELECT 	sale_id, 
	total_amount, 
	shipping_fees, 
	shipping_fees_discount, 
	sale_amount_without_shipping, 
	shipping_ratio, 
	sale_item_amount, 
	((sale_item_amount/1) / sale_amount_without_shipping) as sale_item_ratio, 
	product_class_id
	FROM #tb_sale_shipping_ratio
GO
