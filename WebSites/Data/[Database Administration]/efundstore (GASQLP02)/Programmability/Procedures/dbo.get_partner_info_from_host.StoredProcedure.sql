USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_partner_info_from_host]    Script Date: 02/14/2014 13:06:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_partner_info_from_host]
	@strHostName VARCHAR(50)
AS
-- This query returns all the partner information
SELECT 
	pa.partner_id
	, pgt.partner_group_type_desc
	, pgt2.partner_group_type_desc AS partner_subgroup_type_desc 
	, pa.partner_name
	, pa.partner_path
	, pa.free_kit_url
	, pa.phone_number
	, pa.guid
	, c.culture_name
	, w.website_id
	, pwd.top_menu 
	, pwd.left_menu
	, pwd.right_menu
	, pwd.images_path
	, pwd.default_color
	, pwd.short_cut_menu
	, pwd.product_image_map
	, h.host_id
FROM
	dbo.partner pa
	INNER JOIN partner_group_types pgt
		ON pgt.partner_group_type_id = pa.partner_group_type_id 
	INNER JOIN partner_group_types pgt2
		ON pgt2.partner_group_type_id = pa.partner_subgroup_type_id 
	INNER JOIN partner_web_details pwd
		ON pa.partner_id = pwd.partner_id 
	INNER JOIN web_tracking..websites w
		ON pa.partner_id = w.partner_id 
	INNER JOIN web_tracking..websites_cultures wc
		ON w.website_id = wc.website_id
	INNER JOIN web_tracking..hosts h
		ON w.website_id = h.website_id 
	INNER JOIN cultures c
		ON c.culture_id = wc.culture_id 
WHERE
	h.[host_name] = @strHostName
 AND	wc.default_culture = 1


-- This query returns all the available cultures for this host_name
SELECT
	c.culture_id
	, c.culture_name
	, cn.country_name
	, ld.language_name
	, wc.default_culture 
FROM	web_tracking..websites_cultures wc
	INNER JOIN web_tracking..hosts h
		ON wc.website_id = h.website_id 
	INNER JOIN cultures c
		ON wc.culture_id = c.culture_id 
	INNER JOIN country_names cn
		ON c.country_code = cn.country_code
		 AND c.language_id = cn.language_id 
	INNER JOIN language_desc ld
		ON c.language_id = ld.language_id 
		 AND c.language_id = ld.display_language_id 
WHERE
	h.[host_name] = @strHostName
GO
