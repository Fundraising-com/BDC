USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_subdivisions_by_country_code]    Script Date: 02/14/2014 13:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Subdivision
CREATE PROCEDURE [dbo].[efrcrm_get_subdivisions_by_country_code] @country_code nvarchar(2) AS
begin

select Subdivision_code, Country_code, Subdivision_name_1, Subdivision_name_2, Subdivision_name_3, Regional_division, Subdivision_category from Subdivision
where Country_code = @country_code

end
GO
