USE [esubs_global_v2]
GO
/****** Object:  View [dbo].[partner_attribute_value]    Script Date: 02/14/2014 13:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[partner_attribute_value]
AS 
	select partner_id, partner_attribute_id, culture_code  COLLATE Latin1_General_CI_AS  as culture_code, value, create_date
	from efrcommon..partner_attribute_value
GO
