USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_survey_choices]    Script Date: 02/14/2014 13:05:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrstore_get_survey_choices]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	

    select survey_id, choice_id
	from survey_choice	

END
GO
