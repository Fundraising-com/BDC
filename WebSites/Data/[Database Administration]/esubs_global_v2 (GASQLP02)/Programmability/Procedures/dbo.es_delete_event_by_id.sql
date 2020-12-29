USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_delete_event_by_id]    Script Date: 12/12/2017 9:59:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: 2017-12-07
-- Description:	Deletes an Event and all its cascaded tables
-- =============================================
ALTER PROCEDURE [dbo].[es_delete_event_by_id]
	@eventID INT
AS
BEGIN	
	SET NOCOUNT ON;
	DECLARE @numberOfEventGroups INT;
	DECLARE @groupID INT;
	DECLARE @numberOfMH INT;
	DECLARE @numberOfMH2 INT;
	DECLARE @numberOfPayments INT;
	DECLARE @MHID INT;
	CREATE TABLE #membersToBeRemoved (member_id INT);
	CREATE TABLE #memberHierarchiesToBeRemoved (member_hierarchy_id INT);
	CREATE TABLE #groupsToBeRemoved (group_id INT);
	PRINT('START DELETE. Event ID: ' + CONVERT(VARCHAR(10), @eventID));
	PRINT('DELETE touch_action');
	DELETE
		touch_action
	FROM 
		touch_action
		JOIN touch ON touch_action.touch_id = touch.touch_id
		JOIN event_participation ON touch.event_participation_id = event_participation.event_participation_id
	WHERE
		event_participation.event_id = @eventID;

	PRINT('DELETE touch_archive');
	DELETE
		touch_archive
	FROM 
		touch_archive
		JOIN touch ON touch_archive.touch_id = touch.touch_id
		JOIN event_participation ON touch.event_participation_id = event_participation.event_participation_id
	WHERE
		event_participation.event_id = @eventID;

	PRINT('DELETE custom_email_template');
	DELETE 
		cet
	FROM 
		event_participation ep
		JOIN touch t on ep.event_participation_id = t.event_participation_id
		JOIN touch_info ti on t.touch_info_id = ti.touch_info_id
		JOIN custom_email_template cet on cet.touch_info_id = ti.touch_info_id
	WHERE
		ep.event_id = @eventID;
	PRINT('DELETE touch');
	DELETE
		touch
	FROM
		touch t
		JOIN event_participation ep ON t.event_participation_id = ep.event_participation_id
	WHERE
		ep.event_id = @eventID;

	PRINT('DELETE users');
	DELETE
		U
	FROM
		users u
		JOIN member m on u.user_id = m.user_id
		JOIN member_hierarchy mh on m.member_id = mh.member_id
		JOIN event_participation ep on mh.member_hierarchy_id = ep.member_hierarchy_id
	WHERE
		ep.event_id = @eventID;

	INSERT INTO #membersToBeRemoved
	SELECT
	M.member_id
	FROM	 
		event_participation EP
		JOIN member_hierarchy MH ON EP.member_hierarchy_id = MH.member_hierarchy_id
		JOIN member M ON MH.member_id = M.member_id
	WHERE
		EP.event_id = @eventID;

	INSERT INTO #memberHierarchiesToBeRemoved
	SELECT
	MH.member_hierarchy_id
	FROM event_participation EP
	JOIN member_hierarchy MH ON EP.member_hierarchy_id = MH.member_hierarchy_id
	WHERE EP.event_id = @eventID;

	PRINT('DELETE personalization_image');
	DELETE
		[pi]
	FROM
		personalization_image [pi]
		JOIN personalization p ON [pi].personalization_id = p.personalization_id
		JOIN event_participation ep on p.event_participation_id = ep.event_participation_id
	WHERE
		ep.event_id = @eventID;

	PRINT('DELETE personalization');
	DELETE
		p
	FROM
		personalization p
		JOIN event_participation ep on p.event_participation_id = ep.event_participation_id
	WHERE
		ep.event_id = @eventID;

	PRINT('DELETE event_participation');
	DELETE
		ep
	FROM
		event_participation ep
	WHERE
		ep.event_id = @eventID;

	SELECT @numberOfPayments = COUNT(*) 
		FROM payment P 
		JOIN payment_info [PI] ON P.payment_info_id = [PI].payment_info_id 
		JOIN [group] g ON [pi].group_id = g.group_id
		JOIN event_group eg ON g.group_id = eg.group_id
	WHERE
		eg.event_id = @eventID; 
	IF @numberOfPayments = 0
	BEGIN
		PRINT('DELETE payment_info');
		DELETE [pi]
		FROM
			payment_info [pi]
			JOIN [group] g ON [pi].group_id = g.group_id
			JOIN event_group eg ON g.group_id = eg.group_id
		WHERE
			eg.event_id = @eventID;
	END

	INSERT INTO #groupsToBeRemoved
	SELECT
		g.group_id
	FROM
		[group] g
		JOIN event_group eg ON g.group_id = eg.group_id
	WHERE
		eg.event_id = @eventID;

	-- Check if Group has no more events
	SELECT @groupID = group_id FROM event_group WHERE event_id = @eventID;
	SELECT @numberOfEventGroups = COUNT(event_id) FROM event_group WHERE group_id = @groupID;

	PRINT('DELETE event_group');
	DELETE event_group where event_id = @eventID;


	IF @numberOfEventGroups = 1
	BEGIN
		PRINT('DELETE group_group_status');
		DELETE GGS
		FROM
			group_group_status GGS
			JOIN #groupsToBeRemoved T1 ON GGS.group_id = T1.group_id;
		IF @numberOfPayments = 0
			BEGIN
				PRINT('DELETE group');
				DELETE g
				FROM
					[group] g
					JOIN #groupsToBeRemoved T1 ON g.group_id = T1.group_id;
			END
	END

	PRINT('DELETE event');
	DELETE event where event_id = @eventID;

	SELECT TOP 1 @MHID = member_hierarchy_id FROM #memberHierarchiesToBeRemoved;
	SELECT @numberOfMH = COUNT(event_participation_id) FROM event_participation WHERE member_hierarchy_id = @MHID;
	
	IF @numberOfMH = 0
	BEGIN
		PRINT('DELETE member_hierarchy');
		DELETE
			MH
		FROM
			member_hierarchy MH
			JOIN #memberHierarchiesToBeRemoved T1 ON T1.member_hierarchy_id = MH.member_hierarchy_id;

		PRINT('DELETE member_postal_address');
		DELETE
			M
		FROM
			member_postal_address M
			JOIN #membersToBeRemoved T1 ON T1.member_id = M.member_id;

		PRINT('DELETE member_phone_number');
		DELETE
			M
		FROM
			member_phone_number M
			JOIN #membersToBeRemoved T1 ON T1.member_id = M.member_id;
		SELECT
			@numberOfMH2 = COUNT(MH.member_hierarchy_id)
		FROM
			member_hierarchy MH
			JOIN #membersToBeRemoved T1 ON MH.member_id = T1.member_id
		IF @numberOfMH2 = 0
		BEGIN
			PRINT('DELETE member');
			DELETE
				M
			FROM
				member M
				JOIN #membersToBeRemoved T1 ON T1.member_id = M.member_id;
		END;
	END

	DROP TABLE #membersToBeRemoved;
	DROP TABLE #memberHierarchiesToBeRemoved;
	DROP TABLE #groupsToBeRemoved;
END
