USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_culture_countrys]    Script Date: 02/14/2014 13:05:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Culture_country
CREATE PROCEDURE [dbo].[efrstore_get_culture_countrys] AS
begin

select Culture_code, Country_code, Name from Culture_country

end
GO
