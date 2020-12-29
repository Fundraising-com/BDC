SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: March 7 2015
-- Description:	Order Transfer
-- =============================================
ALTER PROCEDURE [dbo].[cc_order_transfer]
	@event_participation_id int,
	@parent_member_hierarchy_id int
AS
BEGIN
	DECLARE @member_hierarchy_id int, @member_type_id int, @new_event_participation_id int,
	        @new_member_hierarchy_id int
	
	BEGIN TRANSACTION
	
	SELECT @member_hierarchy_id = mh.member_hierarchy_id, @member_type_id = cc.member_type_id
	FROM [esubs_global_v2].[dbo].[event_participation] ep (NOLOCK)
	JOIN [esubs_global_v2].[dbo].[member_hierarchy] mh (NOLOCK) ON ep.member_hierarchy_id = mh.member_hierarchy_id
	JOIN [esubs_global_v2].[dbo].[creation_channel] cc (NOLOCK) ON mh.creation_channel_id = cc.creation_channel_id
	WHERE event_participation_id = @event_participation_id;

    IF @member_type_id = 3 -- SUPPORTERS
    BEGIN
		UPDATE [esubs_global_v2].[dbo].[member_hierarchy]
		SET parent_member_hierarchy_id = @parent_member_hierarchy_id
		WHERE member_hierarchy_id = @member_hierarchy_id;
		
		IF @@error <> 0
		BEGIN
			ROLLBACK TRANSACTION
			RETURN -1
		END
    END
    ELSE -- SPONSORS or PARTICIPANTS
    BEGIN
		-- CLONE EXISTING EVENT PARTICIPATION FIRST
		INSERT INTO [esubs_global_v2].[dbo].[event_participation]
           ([event_id]
           ,[member_hierarchy_id]
           ,[participation_channel_id]
           ,[create_date]
           ,[salutation]
           ,[coppa_month]
           ,[coppa_year]
           ,[agree_term_services]
           ,[holiday_reminders])
		SELECT [event_id], [member_hierarchy_id], [participation_channel_id], GETDATE(), [salutation], [coppa_month],
		       [coppa_year], [agree_term_services], [holiday_reminders]
		FROM   [esubs_global_v2].[dbo].[event_participation]
		WHERE  event_participation_id = @event_participation_id;

		IF @@error <> 0
		BEGIN
			ROLLBACK TRANSACTION
			RETURN -1
		END

		SET @new_event_participation_id = SCOPE_IDENTITY()

		IF @@error <> 0
		BEGIN
			ROLLBACK TRANSACTION
			RETURN -1
		END
		
		-- NEXT UPDATE EXISTING EVENT PARTICIPATION BY FIRST CREATING A NEW MEMBER HIERARCHY
		INSERT INTO [esubs_global_v2].[dbo].[member_hierarchy]
           ([parent_member_hierarchy_id]
           ,[member_id]
           ,[creation_channel_id]
           ,[create_date]
           ,[active]
           ,[unsubscribe]
           ,[unsubscribe_date])
		SELECT @parent_member_hierarchy_id, [member_id], 15, GETDATE(), [active], [unsubscribe], [unsubscribe_date]
		FROM [esubs_global_v2].[dbo].[member_hierarchy]
		WHERE member_hierarchy_id = @member_hierarchy_id;
		
		IF @@error <> 0
		BEGIN
			ROLLBACK TRANSACTION
			RETURN -1
		END

		SET @new_member_hierarchy_id = SCOPE_IDENTITY()

		IF @@error <> 0
		BEGIN
			ROLLBACK TRANSACTION
			RETURN -1
		END

		UPDATE [esubs_global_v2].[dbo].[event_participation]
		   SET [member_hierarchy_id] = @new_member_hierarchy_id
			  ,[participation_channel_id] = 2
		 WHERE event_participation_id = @event_participation_id;
		 
		 IF @@error <> 0
		 BEGIN
			ROLLBACK TRANSACTION
			RETURN -1
		 END
		
		-- FINALLY MAKE SURE TO POINT ALL TOUCHES AND PERSONALIZATION TO THE NEW EVENT PARTICIPATION
		UPDATE [esubs_global_v2].[dbo].[touch]
		   SET [event_participation_id] = @new_event_participation_id
		 WHERE event_participation_id = @event_participation_id;
		 
		 IF @@error <> 0
		 BEGIN
			ROLLBACK TRANSACTION
			RETURN -1
		 END
		 
		 UPDATE [esubs_global_v2].[dbo].[personalization]
		    SET [event_participation_id] = @new_event_participation_id
		  WHERE event_participation_id = @event_participation_id;
		 
		 IF @@error <> 0
		 BEGIN
			ROLLBACK TRANSACTION
			RETURN -1
		 END
    END
    
    COMMIT TRANSACTION
	RETURN 1
END
GO
