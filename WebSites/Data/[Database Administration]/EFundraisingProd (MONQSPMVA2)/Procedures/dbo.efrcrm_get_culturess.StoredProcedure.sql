USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_culturess]    Script Date: 02/14/2014 13:04:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Cultures
CREATE PROCEDURE [dbo].[efrcrm_get_culturess] AS
begin

select Culture_id, Language_id, Country_code, Culture_name, Display_name, Culture_code, Iso_code from Cultures

end
GO
