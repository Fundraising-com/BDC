USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_languages]    Script Date: 02/14/2014 13:05:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Language
CREATE PROCEDURE [dbo].[efrstore_get_languages] AS
begin

select Language_code, Name from Language

end
GO
