use esubs_global_v2
go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: March 31, 2014
-- Description:	Returns all changes done to Touch
--              email templates through the Mail
--              Template Builder
-- EXEC [dbo].[es_get_touch_change_logs]
-- =============================================
CREATE PROCEDURE [dbo].[es_get_touch_change_logs]
AS
BEGIN
	SELECT tcl.touch_change_log_id, tcl.email_template_id, tcl.culture_code, tcl.create_date as [modified_date],
	       tcl.created_by as [modified_by], etf.email_template_field_id, etf.field_name, tcld.value, tcld.prod_refreshed, 
	       tcld.refreshed_by, tcld.refreshed_date
	from
	dbo.touch_change_log tcl (nolock) LEFT JOIN
	dbo.touch_change_log_details tcld (nolock) 
	ON tcl.touch_change_log_id = tcld.touch_change_log_id LEFT JOIN
	dbo.email_template_field etf (nolock)
	ON tcld.email_template_field_id = etf.email_template_field_id
	ORDER BY tcl.create_date DESC
END
GO

grant exec on [dbo].[es_get_touch_change_logs] to proc_exec
grant exec on [dbo].[es_get_touch_change_logs] to db_stored_proc_exec