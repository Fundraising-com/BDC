USE [eFundraisingProd]
GO
/****** Object:  UserDefinedFunction [dbo].[fct_sale_item_rate]    Script Date: 02/14/2014 13:09:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
-- Retourne le shipping ratio d'un item de vente donnee
CREATE   FUNCTION [dbo].[fct_sale_item_rate] (@sale_id INT = NULL, @sale_item_no INT = null, @product_class_id INT = NULL) RETURNS FLOAT
BEGIN
	DECLARE @sale_item_ratio FLOAT

	-- retourne le sale id, total_amount, shipping fees, shipping fees discount, sale amount without shipping,  
	-- shipping ratio, sale item amount, sale item ratio, product class id 
	SELECT  @sale_item_ratio = (si.sales_amount) / (s.total_amount) -- // - s.shipping_fees + s.shipping_fees_discount)
		FROM sale s, sales_item si, scratch_book sb
		WHERE s.sales_id = si.sales_id
		AND si.scratch_book_id = sb.scratch_book_id
		AND s.total_amount <> 0
		AND s.sales_id = @sale_id
		AND si.sales_item_no = @sale_item_no
		AND sb.product_class_id = @product_class_id
		GROUP BY s.sales_id, si.sales_amount, s.total_amount, sb.product_class_id
	
	RETURN 	@sale_item_ratio		
END
GO
