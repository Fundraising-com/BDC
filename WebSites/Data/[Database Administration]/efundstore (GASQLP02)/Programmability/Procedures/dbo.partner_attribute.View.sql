USE [eFundstore]
GO
/****** Object:  View [dbo].[partner_attribute]    Script Date: 02/14/2014 13:04:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[partner_attribute]
AS
	select partner_attribute_id, partner_attribute_name as name, create_date
	from efrcommon..partner_attribute
GO
