USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_kit_form_options]    Script Date: 02/14/2014 13:06:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	May 28, 2004
Description:	This stored procedure returns all the menu options 
		for the free kit request form.
*/
CREATE PROCEDURE [dbo].[get_kit_form_options]
	@strCultureName VARCHAR(10)
AS
-- Position title menu options
SELECT 
	td.title_id
	, td.[description] AS title_desc
FROM 	title_desc td
	INNER JOIN title t
		ON td.title_id = t.title_id 
	INNER JOIN languages l
		ON td.language_id = l.language_id
	INNER JOIN cultures c
		ON l.language_id = c.language_id 
WHERE
	LOWER( c.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )

-- Hear about us menu options
SELECT 
	hd.hear_id
	, hd.[description] AS hear_desc
FROM 	hear_about_us_desc hd
	INNER JOIN hear_about_us h
		ON hd.hear_id = h.hear_id
	INNER JOIN languages l
		ON hd.language_id = l.language_id
	INNER JOIN cultures c
		ON l.language_id = c.language_id 
WHERE
	LOWER( c.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )

-- Organization type menu options
SELECT 
	otd.organization_type_id
	, otd.[description] AS org_type_desc
FROM 	organization_type_desc otd
	INNER JOIN organization_type ot
		ON otd.organization_type_id = ot.organization_type_id
	INNER JOIN languages l
		ON otd.language_id = l.language_id
	INNER JOIN cultures c
		ON l.language_id = c.language_id 
WHERE
	LOWER( c.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )

-- Campaign reason menu options
SELECT 
	crd.campaign_reason_id
	, crd.[description] AS campaign_reason_desc
FROM 	campaign_reason_desc crd
	INNER JOIN campaign_reason cr
		ON crd.campaign_reason_id = cr.campaign_reason_id
	INNER JOIN languages l
		ON crd.language_id = l.language_id
	INNER JOIN cultures c
		ON l.language_id = c.language_id 
WHERE
	LOWER( c.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )
GO
