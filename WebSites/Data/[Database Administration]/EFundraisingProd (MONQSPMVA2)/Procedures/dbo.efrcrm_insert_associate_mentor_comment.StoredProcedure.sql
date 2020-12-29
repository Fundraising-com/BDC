USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_associate_mentor_comment]    Script Date: 02/14/2014 13:06:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Associate_Mentor_Comment
CREATE PROCEDURE [dbo].[efrcrm_insert_associate_mentor_comment] @Ass_Mentor_Comment_ID int OUTPUT, @Associate_ID int, @Mentor_ID int, @Comment_Date smalldatetime, @Comments varchar(255) AS
begin

insert into Associate_Mentor_Comment(Associate_ID, Mentor_ID, Comment_Date, Comments) values(@Associate_ID, @Mentor_ID, @Comment_Date, @Comments)

select @Ass_Mentor_Comment_ID = SCOPE_IDENTITY()

end
GO
