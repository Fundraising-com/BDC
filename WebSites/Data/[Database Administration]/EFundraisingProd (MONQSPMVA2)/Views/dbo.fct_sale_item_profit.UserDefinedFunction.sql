USE [eFundraisingProd]
GO
/****** Object:  UserDefinedFunction [dbo].[fct_sale_item_profit]    Script Date: 02/14/2014 13:09:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
-- Retourne le shipping ratio d'un item de vente donnee
CREATE FUNCTION [dbo].[fct_sale_item_profit] (@sale_id INT, @scratch_book_id INT) RETURNS FLOAT
BEGIN
	DECLARE @sale_item_profit FLOAT
	
	SET @sale_item_profit = null
	
	      SELECT  TOP 1 @sale_item_profit = pr.profit_percentage
		FROM        Sale s
		JOIN        Sales_Item si   ON    si.Sales_ID = s.Sales_ID
		JOIN        Scratch_book sb ON    sb.Scratch_book_ID = si.Scratch_book_ID
		JOIN        Client c          ON    c.Client_ID = s.Client_ID  AND   c.Client_Sequence_Code = s.Client_Sequence_Code 
		JOIN        product_class pc ON pc.product_class_id = si.product_class_id
		JOIN        product_business_rule pbr ON pbr.product_class_id = pc.product_class_id AND ISNULL(pbr.package_id,0) = sb.package_id 
		JOIN        product_business_rule_profit_range  pbrpr ON pbrpr.product_business_rule_id = pbr.product_business_rule_id 
		JOIN        dbo.profit_range pr ON pbrpr.profit_range_id = pr.profit_range_id
		WHERE s.sales_id = @sale_id
		GROUP BY pc.description, pr.profit_percentage,pr.item_nbr_min, pr.item_nbr_max
	       HAVING SUM(si.quantity_sold) BETWEEN pr.item_nbr_min AND pr.item_nbr_max
		ORDER BY pr.profit_percentage DESC
		
	IF @sale_item_profit IS NULL -- Need to check without looking at product class
	BEGIN
	
  		SELECT  TOP 1  @sale_item_profit = pr.profit_percentage
		FROM        Sale s
		JOIN        Sales_Item si   ON    si.Sales_ID = s.Sales_ID
		JOIN        Scratch_book sb ON    sb.Scratch_book_ID = si.Scratch_book_ID
		JOIN        Client c          ON    c.Client_ID = s.Client_ID  AND   c.Client_Sequence_Code = s.Client_Sequence_Code 
		JOIN        product_class pc ON pc.product_class_id = si.product_class_id
		JOIN        product_business_rule pbr ON pbr.product_class_id = pc.product_class_id 
		JOIN        product_business_rule_profit_range  pbrpr ON pbrpr.product_business_rule_id = pbr.product_business_rule_id 
		JOIN        dbo.profit_range pr ON pbrpr.profit_range_id = pr.profit_range_id
		WHERE s.sales_id = @sale_id
		GROUP BY pc.description, pr.profit_percentage,pr.item_nbr_min, pr.item_nbr_max
	       HAVING SUM(si.quantity_sold) BETWEEN pr.item_nbr_min AND pr.item_nbr_max
		ORDER BY pr.profit_percentage DESC	
	END
	
	RETURN 	@sale_item_profit		
END
GO
