USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_survey_by_page_name]    Script Date: 02/14/2014 13:05:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[efrstore_get_survey_by_page_name]
	-- Add the parameters for the stored procedure here
	@Page_name varchar(40)
AS
BEGIN
	

    select survey_id, page_name, display
	from survey
	where [page_name] = @Page_name

END
GO
