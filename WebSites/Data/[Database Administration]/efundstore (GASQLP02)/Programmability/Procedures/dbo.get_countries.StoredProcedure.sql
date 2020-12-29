USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_countries]    Script Date: 02/14/2014 13:06:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	February 18, 2004
Description:	This stored procedure returns all the states or provinces for the passed country code.
*/
CREATE PROCEDURE [dbo].[get_countries]
	@strCultureName VARCHAR(10)
AS
SELECT 
	cn.country_code
	, cn.country_name 
FROM 	countries c
	INNER JOIN country_names cn
		ON c.country_code = cn.country_code
	INNER JOIN languages l 
		ON cn.language_id = l.language_id 
	INNER JOIN cultures cu
		ON l.language_id = cu.language_id 
WHERE
	LOWER( cu.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )
ORDER BY 
	cn.country_name ASC
GO
