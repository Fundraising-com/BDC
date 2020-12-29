USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[search_products]    Script Date: 02/14/2014 13:06:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	May 31, 2004
Description:	This stored procedure returns all the products where 
		the passed keywords are found in the full-text index.
*/
CREATE PROCEDURE [dbo].[search_products]
	@strProductDesc VARCHAR(100)
	, @strCultureName VARCHAR(10)
AS
SELECT 
	pd.product_id
	, pd.product_name
	, pd.product_short_desc
	, pd.product_long_desc
FROM 	product_desc pd
	INNER JOIN languages l 
		ON pd.language_id = l.language_id 
	INNER JOIN cultures cu
		ON l.language_id = cu.language_id 
WHERE 
	CONTAINS( pd.*, @strProductDesc )
 AND	LOWER( cu.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )
 AND	pd.available_online = 1
GO
