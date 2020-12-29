USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_event_participation]    Script Date: 02/14/2014 13:07:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: 10/8/2010
-- Description:	Update event participation
-- =============================================
CREATE PROCEDURE [dbo].[es_update_event_participation]
    @event_participation_id int,
	@event_id int,
	@member_hierarchy_id int,
	@participation_channel_id int,
	@salutation varchar(300),
    @coppa_month int = null,
	@coppa_year int = null,
	@agree_term_services bit = 0,
	@holiday_reminders bit = 0
	
AS
BEGIN
	DECLARE @errorCode int
	DECLARE @validator int

	BEGIN TRANSACTION
	
	UPDATE event_participation 
	SET   event_id = @event_id
		, member_hierarchy_id = @member_hierarchy_id
		, participation_channel_id = @participation_channel_id	
		, salutation = @salutation 
		, coppa_month =  @coppa_month
		, coppa_year = @coppa_year
		, agree_term_services = @agree_term_services
		, holiday_reminders = @holiday_reminders
	WHERE event_participation_id = @event_participation_id
	
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
