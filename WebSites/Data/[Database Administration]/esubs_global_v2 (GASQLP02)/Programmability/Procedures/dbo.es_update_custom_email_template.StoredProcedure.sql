USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_custom_email_template]    Script Date: 02/14/2014 13:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*

*/
CREATE PROC [dbo].[es_update_custom_email_template]
	@custom_email_template_id int,
	@touch_info_id int,
	@subject VARCHAR(100),
	@body_txt VARCHAR(8000),
	@body_html VARCHAR(8000),
	@create_date Datetime
AS
BEGIN
	DECLARE @errorCode int
		
	-- Validate partner type

	BEGIN TRAN
	
	UPDATE custom_email_template
	SET touch_info_id = @touch_info_id
		, subject= @subject
		,body_txt= @body_txt
		,body_html= @body_html
		,create_date =@create_date 
	WHERE custom_email_template_id = @custom_email_template_id
	
	SET @errorCode = @@error
	
	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRAN
		RETURN -1
	END
	
	COMMIT TRAN
	RETURN 0
END
GO
