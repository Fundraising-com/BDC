USE [esubs_global_v2]
GO
/****** Object:  View [dbo].[partner]    Script Date: 02/14/2014 13:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[partner]
AS
	select partner_id, partner_type_id, partner_name, has_collection_site, guid, create_date, is_active
	from efrcommon..partner
GO
