USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_country_namess]    Script Date: 02/14/2014 13:04:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Country_names
CREATE PROCEDURE [dbo].[efrcrm_get_country_namess] AS
begin

select Country_code, Language_id, Country_name from Country_names

end
GO
