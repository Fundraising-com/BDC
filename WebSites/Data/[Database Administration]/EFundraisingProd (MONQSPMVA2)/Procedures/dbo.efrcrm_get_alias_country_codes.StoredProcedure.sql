USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_alias_country_codes]    Script Date: 02/14/2014 13:03:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Alias_Country_Code
CREATE PROCEDURE [dbo].[efrcrm_get_alias_country_codes] AS
begin

select Input_Country_Code, Country_Code from Alias_Country_Code

end
GO
