USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_languagess]    Script Date: 02/14/2014 13:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Languages
CREATE PROCEDURE [dbo].[efrcrm_get_languagess] AS
begin

select Language_id, Language_name, Long_language_code, Short_language_code from Languages

end
GO
