USE [eFundstore]
GO
/****** Object:  View [dbo].[partner_type_culture]    Script Date: 02/14/2014 13:04:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[partner_type_culture]
AS 
	select partner_type_id, culture_code,partner_type_name as name, create_date
	from efrcommon..partner_type_culture
GO
