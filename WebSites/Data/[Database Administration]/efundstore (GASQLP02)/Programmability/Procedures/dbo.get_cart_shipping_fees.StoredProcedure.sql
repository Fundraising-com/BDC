USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_cart_shipping_fees]    Script Date: 02/14/2014 13:06:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:		JF Lavigne
Created On:		June 3, 2004
Last Modified By:	Paolo De Rosa
Last Modified On:	July 28, 2004
Description:		This stored procedure get the shipping amount for each accounting class in a cart
*/
CREATE  PROCEDURE [dbo].[get_cart_shipping_fees] 
	@intCartID INT
	, @strCultureName VARCHAR(10)
AS
SELECT
	sc.shopping_cart_id
	, ac.accounting_class_id
	, SUM( pp.unit_price * sci.quantity ) AS subtotal
	, dbo.udf_calculate_shipping_fees( ac.accounting_class_id, SUM( pp.unit_price * sci.quantity ) ) AS shipping_fees
FROM       
	dbo.shopping_cart sc 
	INNER JOIN  dbo.shopping_cart_items sci 
		ON sc.shopping_cart_id = sci.shopping_cart_id
	INNER JOIN dbo.Scratch_Book sb 
		ON sci.scratch_book_id = sb.Scratch_Book_ID
	INNER JOIN dbo.product_class pc
		ON sb.Product_Class_ID = pc.product_class_id 
	INNER JOIN  dbo.accounting_class ac
		ON pc.accounting_class_id = ac.accounting_class_id
	INNER JOIN (
		SELECT 
			sbpi.Country_Code
			, sbpi.Scratch_Book_ID
			, sbpi.Unit_Price
			, sbpi.Product_Class_ID
		FROM 	dbo.Scratch_Book_Price_Info sbpi
			INNER JOIN (
				SELECT
					Country_Code
					, Scratch_Book_ID
					, MAX( Effective_Date ) AS Effective_Date
				FROM	dbo.Scratch_Book_Price_Info
				GROUP BY
					Country_Code
					, Scratch_Book_ID
			) p
				ON sbpi.Country_Code = p.Country_Code
				 AND sbpi.Scratch_Book_ID = p.Scratch_Book_ID
				 AND sbpi.Effective_Date = p.Effective_Date
			INNER JOIN dbo.cultures c
				ON p.country_code = c.country_code 
		WHERE
			c.culture_name = @strCultureName
	) pp 
		ON sb.Scratch_Book_ID = pp.Scratch_Book_ID 
		 AND pc.product_class_id = pp.Product_Class_ID 
WHERE
	sc.shopping_cart_id = @intCartID
GROUP BY 
	sc.shopping_cart_id
	, ac.accounting_class_id
GO
