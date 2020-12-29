USE [eFundraisingProd]
GO
/****** Object:  UserDefinedFunction [dbo].[fct_commission_rate]    Script Date: 02/14/2014 13:09:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Retourne le shipping ration d'une vente donnee
CREATE   FUNCTION [dbo].[fct_commission_rate] (@partner_id INT = NULL, @product_class_id INT = NULL) RETURNS FLOAT
BEGIN
	DECLARE @commission_rate FLOAT

	-- retourne le sale id, total_amount, shipping fees, shipping fees discount, sale amount without shipping,  
	-- shipping ratio, sale item amount, sale item ratio, product class id 
	SELECT  @commission_rate = pc.commission_rate
		FROM partner_commission pc
		WHERE pc.partner_id = @partner_id 
		AND pc.product_class_id = @product_class_id

	RETURN 	@commission_rate
END
GO
