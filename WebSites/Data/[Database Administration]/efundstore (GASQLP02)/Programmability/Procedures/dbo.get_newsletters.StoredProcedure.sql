USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_newsletters]    Script Date: 02/14/2014 13:06:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	JF Buist
Created On:	June 10, 2004
Description:	This stored procedure return the partner's contact information
*/
CREATE PROCEDURE [dbo].[get_newsletters]
	@intPartnerID INT
	, @strCultureName VARCHAR(10)
AS
SELECT
	nl.newsletter_id
	, nl.language_id
	, nl.partner_id
	, nl.news_month
	, nl.url
	, nl.display_order
FROM	newsletters nl
	INNER JOIN languages l 
		ON nl.language_id = l.language_id 
	INNER JOIN cultures cu
		ON l.language_id = cu.language_id 
WHERE
	nl.partner_id = @intPartnerID
 AND	LOWER( cu.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName) ) )
 AND	nl.newsletter_enabled = 1
ORDER BY
	 nl.display_order
GO
