USE [eFundraisingProd]
GO
/****** Object:  UserDefinedFunction [dbo].[fct_get_commission_rate]    Script Date: 02/14/2014 13:09:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Return the commission rate for the product
CREATE   FUNCTION [dbo].[fct_get_commission_rate] (@sales_id INT, @country_code VARCHAR = 'US', @quantity_sold INT = 0) RETURNS FLOAT
BEGIN

	DECLARE @commission_rate FLOAT

	SELECT    @commission_rate = case 
                 --sample
                 WHEN sb.scratch_book_id in (SELECT sb.scratch_book_id FROM   dbo.scratch_book sb inner join scratch_book_commission sbc on sb.scratch_book_id = sbc.scratch_book_id where (sb.description LIKE 'Sample%')) THEN 0.0
           
                 --agent sale
                 WHEN sb.scratch_book_id = 43 THEN 0.3

                   --scratchcards not prepaid 
                 WHEN sb.product_class_id = 1 THEN 0.17 --was 0.065

                --Herbert/lamontagne  - base
                WHEN sb.product_class_id = 27 THEN 0.03 --was 0.03

                --frozen food us --Pine Valley -- Student Pack FF
                WHEN sb.product_class_id = 29 or sb.product_class_id = 37 or sb.product_class_id = 42  THEN 0.07  --was 0.075 
				
                --T-Shirt - base
                WHEN sb.product_class_id = 36 THEN 0.07

                --frozen food ca chippery     
                WHEN sb.product_class_id = 33 THEN 0.07

                --lollipops us
                     --stAND
                     WHEN sb.scratch_book_id = 3339 THEN 0.02
                     --others               
                     WHEN sb.product_class_id = 28 or sb.product_class_id = 7 THEN 0.02

                --lollipops ca
                WHEN sb.product_class_id = 32 THEN 0.03

                --beef jerky us
                      --x-stick
                      WHEN sb.package_id = 36 AND @quantity_sold <= 2 THEN .02
                      --original beef stick
                      WHEN sb.package_id = 129 THEN .02
                      --other 
                      WHEN sb.product_class_id = 30 THEN 0.02
                
                --beef jerky ca
                WHEN sb.product_class_id = 34 THEN 0.03
                
                --Restaurant.com cards - base 
                WHEN sb.product_class_id = 10 THEN 0.07

                --Hershey m&m - base
                WHEN sb.product_class_id = 5 THEN 0.02 --was 0.03
               
                --caramilk
                WHEN sb.product_class_id = 31 THEN 0.02
 
               --Emblems - base
                WHEN sb.product_class_id = 38 THEN 0.07
             
                --pop corn
                WHEN sb.product_class_id = 16 THEN 0.07

                --efund cards
                WHEN sb.product_class_id = 39 THEN 0.07

                --van wyk
                      WHEN sb.package_id = 125 THEN 0.02
                      --chocolatiers base
                      WHEN sb.package_id = 124 THEN 0.03
                      --2$ Ultimate
                      WHEN sb.package_id = 149 THEN 0.03
                      --VW Pretzels
                       WHEN sb.package_id = 165 THEN 0.02
                      --free cards
                      WHEN sb.product_class_id = 72 THEN 0
 
                 --pucks base
                WHEN sb.product_class_id = 14 THEN 0.02

                --Niagara naturals
                WHEN sb.product_class_id = 40 THEN 0.07
         
                --Mediator Earings - base
                WHEN sb.product_class_id = 41 THEN 0.03

                --gift
                WHEN sb.product_class_id = 23 THEN 0.06

				-- Otis Spunkmeyer FF 
				WHEN sb.product_class_id = 43 THEN 0.055
				
				-- Entertainment books / National Discounts cards 
				WHEN sb.product_class_id in ( 46, 47)  THEN 0.07
				
				-- Kathryn Beich 
				WHEN (sb.product_class_id = 45 AND @country_code = 'CA')   or sb.product_class_id = 48 THEN 0.04
				
				-- Kathryn Beich
				WHEN sb.product_class_id = 45  THEN 0.03
				
				-- Nestle
				WHEN sb.product_class_id = 44  THEN 0.03
				
				-- TB Frozen Food
				WHEN sb.product_class_id IN ( 49, 52)  THEN 0.07
				
				-- Nestle Big pack
				WHEN sb.product_class_id = 50  THEN 0.03
				
				-- Nestle Big pack
				WHEN sb.product_class_id = 51  THEN 0.07
				
				-- Smencils
				WHEN sb.product_class_id = 53  THEN 0.02
				
				-- Golden Treasure
				WHEN sb.product_class_id = 54  THEN 0.07
								
				-- Kathryn Beich/Nestle
				WHEN sb.product_class_id = 55  THEN 0.03
				
				-- Smanimals
				WHEN sb.product_class_id = 57  THEN 0.02

				  -- Heritage Candles
				WHEN sb.product_class_id = 58 THEN 0.07
				
				-- GAO Trash Bags
				WHEN sb.product_class_id = 59  THEN 0.07
				
				-- Cadbury Canada
				  WHEN sb.package_id = 173 THEN 0.03
				  WHEN sb.package_id = 172 THEN 0.03
				  WHEN sb.package_id = 171 THEN 0.03
				  
				  WHEN sb.package_id = 170 THEN 0.05
				  WHEN sb.package_id = 169 THEN 0.05
				  
				  WHEN sb.package_id = 168 THEN 0.07
				  WHEN sb.package_id = 167 THEN 0.07

				
	
	
                WHEN @country_code = 'CA' THEN isnull(sbc.commission_rate_ca,0)

                 else isnull(sbc.commission_rate,0) end
	 FROM  sale s
	INNER JOIN sales_item si on si.sales_id = s.sales_id 
	INNER JOIN scratch_book sb on si.scratch_book_id = sb.scratch_book_id
	 LEFT OUTER JOIN scratch_book_commission sbc on sb.scratch_book_id = sbc.scratch_book_id
	 LEFT OUTER JOIN package pa on sb.package_id = pa.package_Id 
	WHERE sb.product_class_id not in (25,26)
	  AND s.sales_id = @sales_id

	RETURN 	@commission_rate
	
END
GO
