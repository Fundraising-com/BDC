USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_email_templates]    Script Date: 04/14/2014 12:34:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =============================================
-- Author:		Jiro Hidaka
-- Create date: 04/14/2014
-- Description:	Searches through all email templates
--              that satisfy a certain search term
-- =============================================
CREATE PROCEDURE [dbo].[es_get_email_templates_by_search_term]
	@email_template_id int,
	@culture_code nvarchar(5),
	@email_template_name varchar(50),
	@description varchar(255)	
AS
BEGIN
	SELECT et.email_template_id,email_template_name, description,culture_code 
	FROM 
		email_template et (nolock)
		join email_template_culture etc (nolock)
		on et.email_template_id = etc.email_template_id
	WHERE
		(et.email_template_id = @email_template_id or @email_template_id IS NULL) AND
		(etc.culture_code LIKE '%'+@culture_code+'%' or @culture_code IS NULL) AND
		(et.email_template_name LIKE '%'+@email_template_name+'%' or @email_template_name IS NULL) AND
		(et.description LIKE '%'+@description+'%' or @description IS NULL)
	ORDER BY et.email_template_id
END
GO

grant exec on [dbo].[es_get_email_templates_by_search_term] to db_stored_proc_exec
grant exec on [dbo].[es_get_email_templates_by_search_term] to proc_exec