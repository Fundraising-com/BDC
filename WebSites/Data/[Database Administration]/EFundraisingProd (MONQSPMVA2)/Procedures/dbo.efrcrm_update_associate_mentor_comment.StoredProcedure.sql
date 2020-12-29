USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_associate_mentor_comment]    Script Date: 02/14/2014 13:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Associate_Mentor_Comment
CREATE PROCEDURE [dbo].[efrcrm_update_associate_mentor_comment] @Ass_Mentor_Comment_ID int, @Associate_ID int, @Mentor_ID int, @Comment_Date smalldatetime, @Comments varchar(255) AS
begin

update Associate_Mentor_Comment set Associate_ID=@Associate_ID, Mentor_ID=@Mentor_ID, Comment_Date=@Comment_Date, Comments=@Comments where Ass_Mentor_Comment_ID=@Ass_Mentor_Comment_ID

end
GO
