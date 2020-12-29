USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_req_project_type_by_id]    Script Date: 02/14/2014 13:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Req_Project_Type
CREATE PROCEDURE [dbo].[efrcrm_get_req_project_type_by_id] @Project_Type_ID int AS
begin

select Project_Type_ID, Language_Id, Description from Req_Project_Type where Project_Type_ID=@Project_Type_ID

end
GO
