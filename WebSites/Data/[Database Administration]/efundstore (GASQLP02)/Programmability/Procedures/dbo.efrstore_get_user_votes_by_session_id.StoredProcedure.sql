USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_user_votes_by_session_id]    Script Date: 02/14/2014 13:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrstore_get_user_votes_by_session_id]
	-- Add the parameters for the stored procedure here
	@Session_id varchar(20)	
AS
BEGIN
	

    select session_id, choice_id, survey_id
	from user_vote
	where [session_id] = @Session_id
	
END
GO
