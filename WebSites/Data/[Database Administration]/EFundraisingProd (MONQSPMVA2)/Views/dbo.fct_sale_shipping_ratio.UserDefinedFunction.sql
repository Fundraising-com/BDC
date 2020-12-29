USE [eFundraisingProd]
GO
/****** Object:  UserDefinedFunction [dbo].[fct_sale_shipping_ratio]    Script Date: 02/14/2014 13:09:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Retourne le shipping ration d'une vente donnee
CREATE  FUNCTION [dbo].[fct_sale_shipping_ratio] (@sale_id INT = null) RETURNS FLOAT
BEGIN
	DECLARE @shipping_ratio FLOAT

	SELECT  @shipping_ratio = (s.shipping_fees + s.shipping_fees_discount)/s.total_amount
		FROM sale s, sales_item si, scratch_book sb
		WHERE s.sales_id = si.sales_id
		AND si.scratch_book_id = sb.scratch_book_id
		AND s.total_amount <> 0
		AND s.sales_id = @sale_id
	
	RETURN 	@shipping_ratio

END
GO
