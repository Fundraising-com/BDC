USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_culture_subdivisions]    Script Date: 02/14/2014 13:05:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Culture_subdivision
CREATE PROCEDURE [dbo].[efrstore_get_culture_subdivisions] AS
begin

select Culture_code, Subdivision_code, Name from Culture_subdivision

end
GO
