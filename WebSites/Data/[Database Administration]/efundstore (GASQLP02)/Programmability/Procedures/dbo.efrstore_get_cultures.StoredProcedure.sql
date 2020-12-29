USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_cultures]    Script Date: 02/14/2014 13:05:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Culture
CREATE PROCEDURE [dbo].[efrstore_get_cultures] AS
begin

select Culture_code, Country_code, Language_code, Culture_name from Culture

end
GO
