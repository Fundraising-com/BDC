USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_email_activity_by_touch_info_id]    Script Date: 02/14/2014 13:05:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===========================================================================
-- Author:		Jiro Hidaka
-- Create date: April 10, 2010
-- Description:	Get email activity by touch info id, project id, and action id
-- ===========================================================================
CREATE PROCEDURE [dbo].[es_get_email_activity_by_touch_info_id] 
	@touch_info_id int,
	@project_id int = 2,
    @action_id int = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT ea.[email_activity_id]
      ,ea.[touch_id]
      ,ea.[project_id]
      ,ea.[email_template_id]
      ,ea.[email_activity_date]
      ,ea.[action_id]
      ,ea.[action_desc]
      ,ea.[batch_id]
      ,ea.[create_date]
	FROM [RD_Mailer].[dbo].[email_activity] ea INNER JOIN
		 [esubs_global_v2].[dbo].[touch] t ON ea.[touch_id] = t.[touch_id]
	WHERE t.[touch_info_id] = @touch_info_id AND ea.[project_id] = @project_id AND
         (@action_id = NULL OR ea.[action_id] = @action_id)
END
GO
