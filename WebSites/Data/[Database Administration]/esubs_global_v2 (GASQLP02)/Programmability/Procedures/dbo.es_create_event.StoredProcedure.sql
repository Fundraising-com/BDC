USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_create_event]    Script Date: 02/14/2014 13:05:05 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	es_create_event	
	Date: 13 July 2006	
*/
CREATE PROC [dbo].[es_create_event]
	  @group_id int
    , @event_status_id int = 1 -- Campaign by default	
	, @culture_code nvarchar(5) = 'en-US'
	, @event_name varchar(255) = NULL
	, @start_date datetime = NULL
	, @end_date datetime = NULL
	, @active bit = 1
	, @comments varchar(1024) = NULL
	, @redirect varchar(255)
	, @relaunch bit = null
	, @want_sales_rep_call bit = 0
    , @group_type_id int = 1
	, @profit_group_id int
    , @profit_calculated float
    , @event_type_id int = 1 -- Group Fundraiser with Subpage by default
	, @date_of_event DateTime = NULL
	, @humeur_representative varchar(200) = NULL
	, @event_id int OUTPUT
AS
BEGIN
	DECLARE @errorCode int
	DECLARE @validatorCode int	

	IF @start_date IS NULL
		SET @start_date = GETDATE()

	

--	SELECT @validatorCode = dbo.es_validate_event (@event_id, @redirect)  

SELECT @validatorCode = validate_state,  @event_id = event_id
FROM dbo.es_validate_event(@event_id, @redirect)  

	 IF @validatorCode = 1
	BEGIN
		RETURN 1 -- redirect already exists
	END

	BEGIN TRANSACTION
	
	INSERT INTO event
	(
		  event_status_id
		, culture_code
		, event_name
		, start_date
		, end_date
		, active
		, comments
		, create_date
		, redirect
		, want_sales_rep_call
		,group_type_id
		, profit_group_id
		, profit_calculated
        , event_type_id
		, date_of_event
		, humeur_representative
	)
	VALUES
	(
          @event_status_id		
		, @culture_code
		, @event_name
		, @start_date
		, @end_date
		--, @financial_goal
		--, @image_url
		, @active
		, @comments
		, GETDATE()
		, @redirect
		, @want_sales_rep_call
		, @group_type_id
		, @profit_group_id
		, @profit_calculated
        , @event_type_id
		, @date_of_event 
        , @humeur_representative
	)
	
	SET @errorCode = @@error
	
	IF (@errorCode <> 0)
	BEGIN
		ROLLBACK TRANSACTION
		RETURN -1 -- Insert into event error
	END
	
	
	SELECT @event_id = SCOPE_IDENTITY()
	
	SET @errorCode = @@error
	
	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRANSACTION
		RETURN -2 -- Error to retrieve event_id from SCOPE_IDENTITY()
	END
	
	INSERT INTO event_group
	(
		  event_id
		, group_id
		, create_date
	) VALUES (
		@event_id
		, @group_id
		, getdate()
	)

	SET @errorCode = @@error
	
	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRANSACTION
		RETURN -3 -- Insert into event_group error.
	END

	COMMIT TRANSACTION
	RETURN 0
END
GO
