USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_general_comment_by_id]    Script Date: 02/14/2014 13:04:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for General_Comment
CREATE PROCEDURE [dbo].[efrcrm_get_general_comment_by_id] @General_Comment_Id int AS
begin

select General_Comment_Id, Lead_Id, Sales_Id, Entry_Date, General_Comment, User_Name, Department_ID from General_Comment where General_Comment_Id=@General_Comment_Id

end
GO
