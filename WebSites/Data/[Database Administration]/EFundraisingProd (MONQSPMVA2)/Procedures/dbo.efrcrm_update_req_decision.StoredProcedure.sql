USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_req_decision]    Script Date: 02/14/2014 13:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Req_Decision
CREATE PROCEDURE [dbo].[efrcrm_update_req_decision] @Decision_Id int, @Language_Id int, @Description varchar(100) AS
begin

update Req_Decision set Language_Id=@Language_Id, Description=@Description where Decision_Id=@Decision_Id

end
GO
