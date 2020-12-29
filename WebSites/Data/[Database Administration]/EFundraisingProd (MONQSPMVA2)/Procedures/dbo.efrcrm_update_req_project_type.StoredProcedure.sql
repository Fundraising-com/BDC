USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_req_project_type]    Script Date: 02/14/2014 13:08:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Req_Project_Type
CREATE PROCEDURE [dbo].[efrcrm_update_req_project_type] @Project_Type_ID int, @Language_Id int, @Description varchar(200) AS
begin

update Req_Project_Type set Language_Id=@Language_Id, Description=@Description where Project_Type_ID=@Project_Type_ID

end
GO
