USE [eFundraisingProd]
GO
/****** Object:  UserDefinedFunction [dbo].[fct_sale_item_ratio]    Script Date: 02/14/2014 13:09:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Retourne le shipping ration d'une vente donnee
CREATE   FUNCTION [dbo].[fct_sale_item_ratio] (@sale_id INT = null, @product_class_id INT = NULL) RETURNS FLOAT
BEGIN
	DECLARE @sale_item_ratio FLOAT

	-- retourne le sale id, total_amount, shipping fees, shipping fees discount, sale amount without shipping,  
	-- shipping ratio, sale item amount, sale item ratio, product class id 
	SELECT  @sale_item_ratio = SUM(si.sales_amount) / (s.total_amount) -- // - s.shipping_fees + s.shipping_fees_discount)
		FROM sale s, sales_item si, scratch_book sb
		WHERE s.sales_id = si.sales_id
		AND si.scratch_book_id = sb.scratch_book_id
		AND s.total_amount <> 0
		AND s.sales_id = @sale_id
		AND sb.product_class_id = @product_class_id
		GROUP BY s.sales_id, s.total_amount,  s.shipping_fees, s.shipping_fees_discount, sb.product_class_id
	
	RETURN 	@sale_item_ratio		
END
GO
