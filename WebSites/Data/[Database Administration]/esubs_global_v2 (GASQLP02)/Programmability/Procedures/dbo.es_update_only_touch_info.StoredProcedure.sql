USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_only_touch_info]    Script Date: 02/14/2014 13:07:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_update_only_touch_info]
    @visitor_log_id int = NULL
	,@business_rule_id int
	,@launch_date DATETIME
	,@reminder_interval_day int = 0
	,@touch_info_id int
AS
BEGIN
    DECLARE @errorCode int
    
    BEGIN TRANSACTION
    
    UPDATE touch_info SET 
		visitor_log_id= @visitor_log_id
   		, business_rule_id = @business_rule_id
		, launch_date = @launch_date
		,reminder_interval_day = @reminder_interval_day
		WHERE touch_info_id  = @touch_info_id 


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

	
	
	SET @errorCode = @@error
	
	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRANSACTION
		RETURN -3
	END
       
  
    COMMIT TRANSACTION
    RETURN 0
END
GO
