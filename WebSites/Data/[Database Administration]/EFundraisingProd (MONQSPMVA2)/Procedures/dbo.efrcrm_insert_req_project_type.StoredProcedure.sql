USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_req_project_type]    Script Date: 02/14/2014 13:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Req_Project_Type
CREATE PROCEDURE [dbo].[efrcrm_insert_req_project_type] @Project_Type_ID int OUTPUT, @Language_Id int, @Description varchar(200) AS
begin

insert into Req_Project_Type(Language_Id, Description) values(@Language_Id, @Description)

select @Project_Type_ID = SCOPE_IDENTITY()

end
GO
