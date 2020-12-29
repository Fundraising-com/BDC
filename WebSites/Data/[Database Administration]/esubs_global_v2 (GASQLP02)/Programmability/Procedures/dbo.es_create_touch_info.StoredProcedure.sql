USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_create_touch_info]    Script Date: 02/14/2014 13:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_create_touch_info]
    @visitor_log_id int = NULL
	,@subject varchar(100) = NULL
	,@body_txt varchar(8000) = NULL
	,@body_html varchar(8000) = NULL
	,@launch_date datetime = NULL
   	,@business_rule_id int = NULL
	,@reminder_interval_day int = 0
	,@touch_info_id int OUTPUT
AS
BEGIN
    DECLARE @errorCode int
    
    BEGIN TRANSACTION
    
    INSERT INTO touch_info
	(
		visitor_log_id
   		, business_rule_id
		, launch_date
		, create_date
		,reminder_interval_day 
	) VALUES (
		@visitor_log_id
   		, @business_rule_id
		, @launch_date
		, GETDATE()
		,@reminder_interval_day
	)

	SET @errorCode = @@error
	
	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRANSACTION
		RETURN -1
	END
	
	
	SELECT @touch_info_id = SCOPE_IDENTITY()
	
	SET @errorCode = @@error
	
	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRANSACTION
		RETURN -2
	END

	INSERT INTO custom_email_template
	(
		touch_info_id
		, subject
		, body_txt
		, body_html
	) VALUES (
		@touch_info_id
		, @subject
		, @body_txt
		, @body_html
	)
	
	SET @errorCode = @@error
	
	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRANSACTION
		RETURN -3
	END
       
    -- we don't care about the custom_email_template_id so we don't fetch it

    COMMIT TRANSACTION
    RETURN 0
END
GO
