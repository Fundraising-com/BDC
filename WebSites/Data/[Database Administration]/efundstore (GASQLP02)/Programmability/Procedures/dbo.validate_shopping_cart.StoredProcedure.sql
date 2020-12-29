USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[validate_shopping_cart]    Script Date: 02/14/2014 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	June 9, 2004
Description:	This stored procedure validates the user's order quantity per product with 
		the minimum order quantity allowed for a product class.  If the minimum order
		is not met, an message is returned to the user.
*/
CREATE  PROCEDURE [dbo].[validate_shopping_cart]
	@intCartID INT
	, @strCultureName VARCHAR(10)
AS
SELECT
	r.min_requirements
FROM (
	SELECT
		CASE 
			WHEN SUM( sci.quantity ) < pc.minimum_order_qty THEN pcd.min_requirements 
		END AS min_requirements 
		, pc.product_class_id 
	FROM	shopping_cart_items sci
		INNER JOIN scratch_book p
			ON sci.scratch_book_id = p.scratch_book_id
		INNER JOIN product_class pc
			ON p.product_class_id = pc.product_class_id 
		INNER JOIN product_class_desc pcd
			ON pc.product_class_id = pcd.product_class_id 
		INNER JOIN languages l 
			ON pcd.language_id = l.language_id 
		INNER JOIN cultures cu
			ON l.language_id = cu.language_id 
	WHERE
		shopping_cart_id = @intCartID
	 AND	LOWER( cu.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )
	GROUP BY
		pc.product_class_id 
		, pcd.min_requirements 
		, pc.minimum_order_qty 
) r
WHERE
	r.min_requirements IS NOT NULL
GO
