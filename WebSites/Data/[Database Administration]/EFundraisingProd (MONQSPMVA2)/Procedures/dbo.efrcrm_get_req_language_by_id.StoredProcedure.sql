USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_req_language_by_id]    Script Date: 02/14/2014 13:05:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Req_Language
CREATE PROCEDURE [dbo].[efrcrm_get_req_language_by_id] @Language_Id int AS
begin

select Language_Id, Language from Req_Language where Language_Id=@Language_Id

end
GO
