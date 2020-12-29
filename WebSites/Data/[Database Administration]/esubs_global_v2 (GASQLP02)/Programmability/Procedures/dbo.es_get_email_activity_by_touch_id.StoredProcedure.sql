USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_email_activity_by_touch_id]    Script Date: 02/14/2014 13:05:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Author:		Jiro Hidaka
-- Create date: April 10, 2011
-- Description:	Get email activity by touch id, project id and action id
-- =====================================================================
CREATE PROCEDURE [dbo].[es_get_email_activity_by_touch_id] 
	@touch_id int,
	@project_id int = 2,
    @action_id int = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT [email_activity_id]
      ,[touch_id]
      ,[project_id]
      ,[email_template_id]
      ,[email_activity_date]
      ,[action_id]
      ,[action_desc]
      ,[batch_id]
      ,[create_date]
	FROM [RD_Mailer].[dbo].[email_activity]
	WHERE [touch_id] = @touch_id AND [project_id] = @project_id AND
          (@action_id = NULL OR [action_id] = @action_id)
END
GO
