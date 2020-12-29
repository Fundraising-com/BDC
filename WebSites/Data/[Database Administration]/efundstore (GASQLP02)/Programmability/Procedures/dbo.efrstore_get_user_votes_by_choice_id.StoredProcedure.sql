USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_user_votes_by_choice_id]    Script Date: 02/14/2014 13:05:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[efrstore_get_user_votes_by_choice_id]
	-- Add the parameters for the stored procedure here
	@Choice_id int
AS
BEGIN
	
	SELECT session_id, choice_id, survey_id
	FROM user_vote
	WHERE choice_id = @Choice_id

END
GO
