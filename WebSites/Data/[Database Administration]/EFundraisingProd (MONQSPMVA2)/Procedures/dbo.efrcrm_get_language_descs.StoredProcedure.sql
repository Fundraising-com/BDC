USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_language_descs]    Script Date: 02/14/2014 13:04:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Language_desc
CREATE PROCEDURE [dbo].[efrcrm_get_language_descs] AS
begin

select Language_id, Display_language_id, Language_name from Language_desc

end
GO
