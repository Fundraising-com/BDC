USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_partner_by_guid]    Script Date: 02/14/2014 13:06:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Created by:
	Date:
*/
CREATE PROCEDURE [dbo].[es_get_partner_by_guid]
	@guid varchar(64)
AS
BEGIN
SELECT     
	p.partner_id
	, p.partner_type_id
	, p.partner_name
	, p.has_collection_site
	, p.guid
	, pt.partner_type_name
FROM dbo.partner p
	inner join partner_type pt
		on pt.partner_type_id = p.partner_type_id
where 
	cast(guid as varchar(64)) LIKE @guid + '%'
END
GO
