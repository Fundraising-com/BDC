USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_culture_country_names]    Script Date: 02/14/2014 13:05:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Culture_country_name
CREATE PROCEDURE [dbo].[efrstore_get_culture_country_names] AS
begin

select Culture_code, Country_code, Country_name from Culture_country_name

end
GO
