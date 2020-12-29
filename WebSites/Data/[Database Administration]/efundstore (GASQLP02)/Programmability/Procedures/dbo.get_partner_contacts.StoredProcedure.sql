USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_partner_contacts]    Script Date: 02/14/2014 13:06:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	JF Buist
Created On:	June 10, 2004
Description:	This stored procedure return the partner's contact information
*/
CREATE PROCEDURE [dbo].[get_partner_contacts]
	@intPartnerID INT
	, @strCultureName VARCHAR(10)
AS
SELECT
	pc.partner_id
	, pc.language_id
	, pc.section_name
	, pc.section_value
	, pc.display_order
FROM	partner_contacts pc
	INNER JOIN languages l 
		ON pc.language_id = l.language_id 
	INNER JOIN cultures cu
		ON l.language_id = cu.language_id 
WHERE
	pc.partner_id = @intPartnerID
 AND	LOWER( cu.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName) ) )
ORDER BY
	 pc.display_order
GO
