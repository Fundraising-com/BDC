USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_get_partners]    Script Date: 02/14/2014 13:05:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrc_get_partners]
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
	INNER JOIN partner_type pt
		ON pt.partner_type_id = p.partner_type_id
END
GO
