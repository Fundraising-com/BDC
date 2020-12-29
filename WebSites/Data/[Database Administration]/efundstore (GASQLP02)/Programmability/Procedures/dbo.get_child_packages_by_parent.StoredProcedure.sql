USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_child_packages_by_parent]    Script Date: 02/14/2014 13:06:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	June 3, 2004
Description:	This stored procedure returns all the child packages 
		that belong to parent packages.
*/
CREATE PROCEDURE [dbo].[get_child_packages_by_parent]
	@intParentPackageID TINYINT
	, @strCultureName VARCHAR(10)
	, @bitOnlinePackages BIT = NULL
AS
IF @bitOnlinePackages IS NULL
	SELECT
		p.package_id
		, pd.package_name
		, pd.package_short_desc
		, pd.package_long_desc
		, pd.package_extra_desc
		, pd.package_small_img
		, pd.package_large_img
		, pd.page_url
		, p.contains_products
		, p.accounting_class_id
	FROM	packages p
		INNER JOIN package_desc pd
			ON p.package_id = pd.package_id
		INNER JOIN partner_packages pp
			ON pp.package_id = pd.package_id
		INNER JOIN web_tracking..websites w
			ON pp.partner_id = w.partner_id 
		INNER JOIN web_tracking..websites_cultures wc
			ON w.website_id = wc.website_id
		INNER JOIN languages l 
			ON pd.language_id = l.language_id 
		INNER JOIN cultures cu
			ON l.language_id = cu.language_id 
			 AND wc.culture_id = cu.culture_id 
	WHERE
		LOWER( cu.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )
	 AND	p.parent_package_id = @intParentPackageID
	 order by p.display_order asc
ELSE
	SELECT
		p.package_id
		, pd.package_name
		, pd.package_short_desc
		, pd.package_long_desc
		, pd.package_extra_desc
		, pd.package_small_img
		, pd.package_large_img
		, pd.page_url
		, p.contains_products
		, p.accounting_class_id
	FROM	packages p
		INNER JOIN package_desc pd
			ON p.package_id = pd.package_id
		INNER JOIN partner_packages pp
			ON pp.package_id = pd.package_id
		INNER JOIN web_tracking..websites w
			ON pp.partner_id = w.partner_id 
		INNER JOIN web_tracking..websites_cultures wc
			ON w.[website_id] = wc.[website_id]
		INNER JOIN languages l 
			ON pd.language_id = l.language_id 
		INNER JOIN cultures cu
			ON l.language_id = cu.language_id 
			 AND wc.culture_id = cu.culture_id 
	WHERE
		LOWER( cu.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )
	 AND	p.parent_package_id = @intParentPackageID
	 AND	p.package_enabled = @bitOnlinePackages
	 order by p.display_order asc
GO
