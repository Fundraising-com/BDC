USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_products_by_package]    Script Date: 02/14/2014 13:06:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	May 28, 2004
Description:	This stored procedure returns all the products 
		for a package.
*/
CREATE PROCEDURE [dbo].[get_products_by_package] 
	@intPackageID TINYINT
	, @strCultureName VARCHAR(10)
	, @bitOnlineProducts BIT = NULL
AS
IF @bitOnlineProducts IS NULL
	SELECT
		pd.product_id
		, pd.product_name
		, pd.product_short_desc
		, pd.product_long_desc
		, pd.product_small_img
		, pd.product_large_img
		, pad.package_name
	FROM	products_packages pp
		INNER JOIN package_desc pad
			ON pp.package_id = pad.package_id
		INNER JOIN product_desc pd
			ON pp.product_id = pd.product_id
		INNER JOIN languages l 
			ON pd.language_id = l.language_id 
			 AND pad.language_id = l.language_id 
		INNER JOIN cultures cu
			ON l.language_id = cu.language_id 
	WHERE
		pp.package_id = @intPackageID
	 AND	LOWER( cu.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )
	 order by pp.display_order
ELSE
	SELECT
		pd.product_id
		, pd.product_name
		, pd.product_short_desc
		, pd.product_long_desc
		, pd.product_small_img
		, pd.product_large_img
		, pad.package_name
	FROM	products_packages pp
		INNER JOIN package_desc pad
			ON pp.package_id = pad.package_id
		INNER JOIN product_desc pd
			ON pp.product_id = pd.product_id
		INNER JOIN languages l 
			ON pd.language_id = l.language_id 
			 AND pad.language_id = l.language_id 
		INNER JOIN cultures cu
			ON l.language_id = cu.language_id 
	WHERE
		pp.package_id = @intPackageID
	 AND	LOWER( cu.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )
	 AND	pd.available_online = @bitOnlineProducts
	 order by pp.display_order
GO
