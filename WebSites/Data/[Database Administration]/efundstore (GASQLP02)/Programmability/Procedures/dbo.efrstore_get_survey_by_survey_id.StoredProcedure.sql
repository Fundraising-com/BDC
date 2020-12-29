USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_survey_by_survey_id]    Script Date: 02/14/2014 13:05:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrstore_get_survey_by_survey_id]
	-- Add the parameters for the stored procedure here
	@Survey_id int
AS
BEGIN
	

    select survey_id, page_name, display
	from survey
	where [survey_id] = @Survey_id

END
GO
