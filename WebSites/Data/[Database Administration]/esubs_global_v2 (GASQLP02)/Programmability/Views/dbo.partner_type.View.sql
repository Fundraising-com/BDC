USE [esubs_global_v2]
GO
/****** Object:  View [dbo].[partner_type]    Script Date: 02/14/2014 13:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[partner_type]
AS 
	select partner_type_id, partner_type_name, create_date
	from efrcommon..partner_type
GO
