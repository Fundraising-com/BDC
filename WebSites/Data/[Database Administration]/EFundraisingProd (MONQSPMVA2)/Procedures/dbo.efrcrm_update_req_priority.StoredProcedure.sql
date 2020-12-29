USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_req_priority]    Script Date: 02/14/2014 13:08:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Req_Priority
CREATE PROCEDURE [dbo].[efrcrm_update_req_priority] @Priority_Id int, @Language_Id int, @Description varchar(50), @Is_Default bit AS
begin

update Req_Priority set Language_Id=@Language_Id, Description=@Description, Is_Default=@Is_Default where Priority_Id=@Priority_Id

end
GO
