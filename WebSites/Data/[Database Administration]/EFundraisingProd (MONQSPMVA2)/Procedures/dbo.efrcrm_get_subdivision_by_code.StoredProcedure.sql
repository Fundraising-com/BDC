USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_subdivision_by_code]    Script Date: 02/14/2014 13:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Subdivision
CREATE PROCEDURE [dbo].[efrcrm_get_subdivision_by_code] @Subdivision_code nvarchar(7) AS
begin

select Subdivision_code, Country_code, Subdivision_name_1, Subdivision_name_2, Subdivision_name_3, Regional_division, Subdivision_category from Subdivision
where Subdivision_code = @Subdivision_code

end
GO
