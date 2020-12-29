USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_all_packages_with_products]    Script Date: 02/14/2014 13:06:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	Paolo De Rosa
Description:	This stored procedure returns all the child packages and their associated products.
*/
CREATE PROCEDURE [dbo].[get_all_packages_with_products]
	@intGrandPackageID INT
	, @strCultureName VARCHAR(10)
AS
IF (
	SELECT 
		SUM( ccp.package_id )
	FROM 	packages p
		INNER JOIN packages cp
			ON p.package_id = cp.parent_package_id 
		LEFT OUTER JOIN packages ccp
			ON cp.package_id = ccp.parent_package_id 
	WHERE
		p.package_id = @intGrandPackageID
) IS NULL
	SELECT 
		p.package_id AS parent_package_id
		, p.package_name AS parent_package_name
		, cpd.package_short_desc AS parent_package_short_desc
		, cpd.package_long_desc AS parent_package_long_desc
		, cpd.package_extra_desc AS parent_package_extra_desc
		, cpd.package_small_img AS parent_package_small_img
		, cpd.package_large_img AS parent_package_large_img
		, ccp.package_id AS child_package_id
		, ccp.package_template_id AS child_package_template_id
		, ccp.package_name AS child_package_name
		, ccpd.package_short_desc AS child_package_short_desc
		, ccpd.package_long_desc AS child_package_long_desc
		, ccpd.package_extra_desc AS child_package_extra_desc
		, ccpd.package_small_img AS child_package_small_img
		, ccpd.package_large_img AS child_package_large_img
		, pc.minimum_order_qty
		, ppd.product_id
		, ppd.product_name
		, ppd.product_short_desc
		, ppd.product_long_desc
		, ppd.product_small_img
		, ppd.product_large_img
	FROM 	packages p
		INNER JOIN package_desc cpd
			ON p.package_id = cpd.package_id 
		INNER JOIN packages ccp
			ON p.package_id = ccp.parent_package_id 
		INNER JOIN package_desc ccpd
			ON ccp.package_id = ccpd.package_id 
		INNER JOIN products_packages pp
			ON ccpd.package_id = pp.package_id
		INNER JOIN product_desc ppd
			ON pp.product_id = ppd.product_id
		INNER JOIN scratch_book sb
			ON ppd.product_id = sb.scratch_book_id
		INNER JOIN product_class pc
			ON sb.product_class_id = pc.product_class_id
		INNER JOIN languages l
			ON cpd.language_id = l.language_id 
			 AND ccpd.language_id = l.language_id 
			 AND ppd.language_id = l.language_id 
		INNER JOIN cultures c
			ON l.language_id = c.language_id 
	WHERE
		p.package_id = @intGrandPackageID
	 AND	LOWER( c.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )
	 AND	pp.displayable = 1
	ORDER BY
		p.display_order
		, ccp.display_order
--		, ppd.product_id
		, pp.display_order
ELSE
	SELECT 
		p.package_id AS parent_package_id
		, p.package_name AS parent_package_name
		, pd.package_short_desc AS parent_package_short_desc
		, pd.package_long_desc AS parent_package_long_desc
		, pd.package_extra_desc AS parent_package_extra_desc
		, pd.package_small_img AS parent_package_small_img
		, pd.package_large_img AS parent_package_large_img
		, cp.package_id AS child_package_id
		, cp.package_name AS child_package_name
		, cpd.package_short_desc AS child_package_short_desc
		, cpd.package_long_desc AS child_package_long_desc
		, cpd.package_extra_desc AS child_package_extra_desc
		, cpd.package_small_img AS child_package_small_img
		, cpd.package_large_img AS child_package_large_img
		, ccp.package_id AS child_child_package_id
		, ccp.package_template_id AS child_child_package_template_id
		, ccp.package_name AS child_child_package_name
		, ccpd.package_short_desc AS child_child_package_short_desc
		, ccpd.package_long_desc AS child_child_package_long_desc
		, ccpd.package_extra_desc AS child_child_package_extra_desc
		, ccpd.package_small_img AS child_child_package_small_img
		, ccpd.package_large_img AS child_child_package_large_img
		, pc.minimum_order_qty
		, ppd.product_id
		, ppd.product_name
		, ppd.product_short_desc
		, ppd.product_long_desc
		, ppd.product_small_img
		, ppd.product_large_img
	FROM 	packages p
		INNER JOIN package_desc pd
			ON p.package_id = pd.package_id 
		INNER JOIN packages cp
			ON p.package_id = cp.parent_package_id 
		INNER JOIN package_desc cpd
			ON cp.package_id = cpd.package_id 
		INNER JOIN packages ccp
			ON cp.package_id = ccp.parent_package_id 
		INNER JOIN package_desc ccpd
			ON ccp.package_id = ccpd.package_id 		INNER JOIN products_packages pp
			ON ccpd.package_id = pp.package_id
		INNER JOIN product_desc ppd
			ON pp.product_id = ppd.product_id
		INNER JOIN scratch_book sb
			ON ppd.product_id = sb.scratch_book_id
		INNER JOIN product_class pc
			ON sb.product_class_id = pc.product_class_id
		INNER JOIN languages l
			ON pd.language_id = l.language_id 
			 AND cpd.language_id = l.language_id 
			 AND ccpd.language_id = l.language_id 
			 AND ppd.language_id = l.language_id 
		INNER JOIN cultures c
			ON l.language_id = c.language_id 
	WHERE
		p.package_id = @intGrandPackageID
	 AND	LOWER( c.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )
	 AND	pp.displayable = 1
	ORDER BY
		p.display_order
		, cp.display_order
		, ccp.display_order
--		, ppd.product_id
		, pp.display_order
GO
