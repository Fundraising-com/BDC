USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_general_comment]    Script Date: 02/14/2014 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for General_Comment
CREATE PROCEDURE [dbo].[efrcrm_insert_general_comment] @General_Comment_Id int OUTPUT, @Lead_Id int, @Sales_Id int, @Entry_Date smalldatetime, @General_Comment text, @User_Name varchar(50), @Department_ID int AS
begin

insert into General_Comment(Lead_Id, Sales_Id, Entry_Date, General_Comment, User_Name, Department_ID) values(@Lead_Id, @Sales_Id, @Entry_Date, @General_Comment, @User_Name, @Department_ID)

select @General_Comment_Id = SCOPE_IDENTITY()

end
GO
