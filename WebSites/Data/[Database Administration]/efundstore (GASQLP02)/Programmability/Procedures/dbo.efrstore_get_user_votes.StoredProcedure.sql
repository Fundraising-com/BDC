USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_user_votes]    Script Date: 02/14/2014 13:05:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrstore_get_user_votes]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	

    select session_id, choice_id, survey_id
	from user_vote
	
END
GO
