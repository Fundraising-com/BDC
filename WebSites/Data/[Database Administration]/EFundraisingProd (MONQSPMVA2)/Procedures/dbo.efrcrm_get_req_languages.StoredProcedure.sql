USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_req_languages]    Script Date: 02/14/2014 13:05:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Req_Language
CREATE PROCEDURE [dbo].[efrcrm_get_req_languages] AS
begin

select Language_Id, Language from Req_Language

end
GO
