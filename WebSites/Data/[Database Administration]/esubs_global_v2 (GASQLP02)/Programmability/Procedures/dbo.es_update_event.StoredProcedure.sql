USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_event]    Script Date: 02/14/2014 13:07:51 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*

Dat Nghiem 2006-07-14
	
*/

CREATE PROC [dbo].[es_update_event]
	@event_id int
	, @event_name varchar(255)
	, @end_date datetime = NULL
	, @active bit = 1
	, @redirect varchar(255) = null
	, @comments varchar(1024) = NULL
	, @want_sales_rep_call bit = 0
    , @group_type_id int = 1
	, @donation bit = 0
	, @date_of_event DateTime
	, @culture_code nvarchar(5) = NULL
	, @profit_calculated float = -1
AS
BEGIN
	DECLARE @errorCode int
	DECLARE @validator int
	
	IF (@event_id IS NULL)
		RETURN -1
	
	--SELECT @validator = dbo.es_validate_event (@event_id, @redirect) 

	SELECT @validator = validate_state
	FROM dbo.es_validate_event(@event_id, @redirect) 
	-- Validate parameter

	IF (@validator = 1)
		RETURN 1	

	BEGIN TRANSACTION
	
	IF @culture_code IS NULL
	  SELECT @culture_code = culture_code FROM event WHERE event_id = @event_id
	  
	IF @profit_calculated = -1 OR @profit_calculated IS NULL
	  SELECT @profit_calculated = profit_calculated FROM event WHERE event_id = @event_id
	
	UPDATE event 
	SET event_name = @event_name
		, end_date = @end_date
		, comments = @comments	
		, active = @active 
		, redirect =  @redirect
		, want_sales_rep_call = @want_sales_rep_call
		, group_type_id = @group_type_id
		, donation = @donation
		, date_of_event = @date_of_event
		, culture_code = @culture_code
		, profit_calculated = @profit_calculated
	WHERE event_id = @event_id
	
	SET @errorCode = @@error
	
	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRANSACTION
		RETURN -1
	END
	
	COMMIT TRANSACTION
	RETURN 0
END
GO
