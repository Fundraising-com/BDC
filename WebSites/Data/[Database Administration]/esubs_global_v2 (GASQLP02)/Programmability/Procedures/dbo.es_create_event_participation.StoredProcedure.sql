USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_create_event_participation]    Script Date: 02/14/2014 13:05:06 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
	es_create_event_participation
	
	Projects: Esubs v2
	Date: 13 Jul 2006
	
	Description: Add a member hierarchy to an event
	
*/
CREATE PROC [dbo].[es_create_event_participation]
	@event_id int,
	@member_hierarchy_id int,
	@participation_channel_id int,
	@salutation varchar(300),
    @coppa_month int = null,
	@coppa_year int = null,
	@agree_term_services bit = 0,
	@event_participation_id int OUTPUT,
    @holiday_reminders bit = 0
AS
BEGIN
	DECLARE @errorCode int
	DECLARE @validatorCode int

	
	--SELECT @validatorCode = dbo.es_validate_event_participation (@event_participation_id, @event_id, @member_hierarchy_id)  
	SELECT @validatorCode = validate_state,  @event_participation_id = event_participant_id
	FROM dbo.es_validate_event_participation(@event_participation_id, @event_id, @member_hierarchy_id)  

	IF @validatorCode = 1
		RETURN 1 -- @eventID, @memberHierarchyID already exists
	ELSE IF @validatorCode = 0
	BEGIN
		--check pour savoir s'il n'est pas déjà présent

	    	BEGIN TRANSACTION
	    
	    	INSERT INTO event_participation
	    	(
	    		event_id
	    		, member_hierarchy_id
	    		, participation_channel_id
				, salutation
				, coppa_month
				, coppa_year
                , agree_term_services
	    		, create_date
                , holiday_reminders
	    	)
	    	VALUES
	    	(
	    		@event_id
	    		, @member_hierarchy_id
	    		, @participation_channel_id
				, @salutation
			    , @coppa_month
				, @coppa_year
                , @agree_term_services
	    		, GETDATE()
                , @holiday_reminders
	    	)
	    	
	    	SET @errorCode = @@error	    	
	    	IF (@errorCode <> 0)
	    	BEGIN
	      		ROLLBACK TRANSACTION
	    		RETURN @errorCode
	    	END	    
	    
	    	SELECT @event_participation_id = SCOPE_IDENTITY()
	    
	    	SET @errorCode = @@error	    	
	    	IF (@errorCode <> 0)

	    	BEGIN
	      		ROLLBACK TRANSACTION
	    		RETURN @errorCode
	    	END
			COMMIT TRANSACTION

	END
    	RETURN 0
END
GO
