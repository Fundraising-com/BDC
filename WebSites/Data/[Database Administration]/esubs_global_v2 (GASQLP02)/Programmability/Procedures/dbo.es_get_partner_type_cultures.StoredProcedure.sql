USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_partner_type_cultures]    Script Date: 02/14/2014 13:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Partner_type_culture
CREATE PROCEDURE [dbo].[es_get_partner_type_cultures] AS
begin

select Partner_type_id, Culture_code, Partner_type_name, Create_date from Partner_type_culture

end
GO
