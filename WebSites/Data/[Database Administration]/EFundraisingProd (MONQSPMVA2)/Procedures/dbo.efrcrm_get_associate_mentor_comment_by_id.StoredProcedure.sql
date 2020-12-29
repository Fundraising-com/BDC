USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_associate_mentor_comment_by_id]    Script Date: 02/14/2014 13:03:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Associate_Mentor_Comment
CREATE PROCEDURE [dbo].[efrcrm_get_associate_mentor_comment_by_id] @Ass_Mentor_Comment_ID int AS
begin

select Ass_Mentor_Comment_ID, Associate_ID, Mentor_ID, Comment_Date, Comments from Associate_Mentor_Comment where Ass_Mentor_Comment_ID=@Ass_Mentor_Comment_ID

end
GO
