USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[get_states_provinces_by_country]    Script Date: 02/14/2014 13:04:36 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
Created By:	Paolo De Rosa
Created On:	February 18, 2004
Description:	This stored procedure returns all the states or provinces for the passed country code.
*/
create  PROCEDURE [dbo].[get_states_provinces_by_country]
	@strCountryCode VARCHAR(10)
AS
SELECT 
	State_Code
	, State_Name 
FROM 	efundraisingprod.dbo.State 
WHERE 
	State_Code IS NOT NULL
 AND	LTRIM( RTRIM( State_Code ) ) <> ''
 AND 	LOWER( Country_Code ) = LOWER( @strCountryCode )
ORDER BY 
	State_Name ASC
GO
