USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_user_votes_by_survey_id]    Script Date: 02/14/2014 13:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[efrstore_get_user_votes_by_survey_id]
	@Survey_id int
	
AS
BEGIN
	select session_id, choice_id, survey_id
	from user_vote
	where [survey_id] = @Survey_id
END
GO
