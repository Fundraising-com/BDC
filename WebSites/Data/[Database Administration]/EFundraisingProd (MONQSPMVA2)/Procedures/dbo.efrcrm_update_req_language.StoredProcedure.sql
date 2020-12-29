USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_req_language]    Script Date: 02/14/2014 13:08:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Req_Language
CREATE PROCEDURE [dbo].[efrcrm_update_req_language] @Language_Id int, @Language varchar(30) AS
begin

update Req_Language set Language=@Language where Language_Id=@Language_Id

end
GO
