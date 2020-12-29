USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_partner_programs]    Script Date: 02/14/2014 13:06:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
	
*/
CREATE PROCEDURE [dbo].[es_get_partner_programs]
	@partnerID int
AS
BEGIN
SELECT     
	pp.program_id
	, pp.partner_id
	, pp.program_url
	, p.program_name
FROM         
	program_partner pp
	INNER JOIN program p
	ON pp.program_id = p.program_id
where 
	pp.partner_id = @partnerID
END
GO
