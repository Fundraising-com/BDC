USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_relink_efundscard_order]    Script Date: 02/14/2014 13:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_relink_efundscard_order] 
	@groupID int,
    @suppID int
AS
BEGIN
    DECLARE @event_id int
    DECLARE @errorCode int
	DECLARE @sponsor_id int -- member hierarchy id of Sponsor
	DECLARE @member_hierarchy_id int -- member hierarchy id of the supporter

    /* 1) Event ID of the group who bought and redeemed eFunds Card */
    SELECT top 1 @event_id = event_id -- Get the latest campaign ID
	FROM   dbo.event_group
	WHERE  group_id = @groupID
	ORDER BY create_date desc

	/* 2) MemberHierarchyID of this group */
	SELECT @sponsor_id = sponsor_id
	FROM   dbo.[group]
	WHERE  group_id = @groupID
    
    /* 3) Get the memberHierarchyID of the actual person who redeemed it. It should be under the default efunds group (efundscard) */
    SELECT @member_hierarchy_id = member_hierarchy_id
	FROM   dbo.event_participation
	WHERE  event_participation_id = @suppID

	BEGIN TRANSACTION

	-- First update the campaign ID
	UPDATE dbo.event_participation 
	SET    event_id = @event_id
	WHERE  event_participation_id = @suppID

	SET @errorCode = @@error

	IF (@errorCode <> 0)
	BEGIN
		ROLLBACK TRANSACTION
		RETURN -1
	END
    	
	IF EXISTS(SELECT * FROM member_hierarchy
			  WHERE member_hierarchy_id = @member_hierarchy_id AND 
			  		parent_member_hierarchy_id IS NULL)
	BEGIN
		-- SPONSOR
		UPDATE dbo.event_participation 
		SET    member_hierarchy_id = @sponsor_id
		WHERE  event_participation_id = @suppID

		SET @errorCode = @@error
	
		IF (@errorCode <> 0)
		BEGIN
  			ROLLBACK TRANSACTION
			RETURN -2
		END
	END
	ELSE
	BEGIN
		-- SUPPORTER
		UPDATE member_hierarchy 
		SET    parent_member_hierarchy_id = @sponsor_id
		WHERE  member_hierarchy_id = @member_hierarchy_id
	
		SET @errorCode = @@error
	
		IF (@errorCode <> 0)
		BEGIN
  			ROLLBACK TRANSACTION
			RETURN -3
		END
	END

    COMMIT TRANSACTION
	RETURN 0
END
GO
