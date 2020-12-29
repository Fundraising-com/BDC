USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_subdivisions]    Script Date: 02/14/2014 13:05:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Subdivision
CREATE PROCEDURE [dbo].[efrstore_get_subdivisions] AS
begin

select Subdivision_code, Country_code, Subdivision_name_1, Subdivision_name_2, Subdivision_name_3, Regional_division, Subdivision_category, Display from Subdivision

end
GO
