USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_partner_by_host]    Script Date: 02/14/2014 13:06:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Created by:
	Date:
*/
CREATE PROCEDURE [dbo].[es_get_partner_by_host]
	@host varchar(256)
AS
BEGIN
SELECT     
	p.partner_id
	, p.partner_type_id
	, p.partner_name
	, p.has_collection_site
	, p.guid
	, pt.partner_type_name
FROM partner p
	INNER JOIN program_partner pp
		ON p.partner_id = pp.partner_id
	INNER JOIN partner_type pt
		ON pt.partner_type_id = p.partner_type_id
WHERE     
	pp.program_url = @host
END
GO
