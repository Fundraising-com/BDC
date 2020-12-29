USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_general_comment]    Script Date: 02/14/2014 13:08:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for General_Comment
CREATE PROCEDURE [dbo].[efrcrm_update_general_comment] @General_Comment_Id int, @Lead_Id int, @Sales_Id int, @Entry_Date smalldatetime, @General_Comment text, @User_Name varchar(50), @Department_ID int AS
begin

update General_Comment set Lead_Id=@Lead_Id, Sales_Id=@Sales_Id, Entry_Date=@Entry_Date, General_Comment=@General_Comment, User_Name=@User_Name, Department_ID=@Department_ID where General_Comment_Id=@General_Comment_Id

end
GO
