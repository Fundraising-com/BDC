SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: 2017-02-08
-- Description:	Returns consolidated information regarding a Group
-- =============================================
ALTER PROCEDURE sp_group_cardbox
	@groupId INT
AS
BEGIN
	DECLARE @amountRaised float = 0;
	DECLARE @totalMembers int;
	DECLARE @goal float;
	DECLARE @eventId INT;
	CREATE TABLE #TempGroupsReport (event_id INT, nb_group_members INT, nb_part INT, nb_active INT, nb_supporters INT, nb_subs INT, amount_sold FLOAT, donation_amount_sold FLOAT, profit FLOAT, last_activity DATETIME);

	SELECT
	@goal = SUM(P.fundraising_goal)
	FROM
	event_group EG (NOLOCK)
	JOIN event E (NOLOCK) ON EG.event_id = E.event_id
	JOIN event_participation EP (NOLOCK) ON E.event_id = EP.event_id AND EP.participation_channel_id = 3
	JOIN personalization P (NOLOCK) ON EP.event_participation_id = P.event_participation_id
	WHERE EG.group_id = @groupId
	GROUP BY EG.group_id;

	DECLARE event_id_cursor CURSOR FOR
		SELECT
		EG.event_id
		FROM
		event_group EG (NOLOCK)
		WHERE EG.group_id = @groupId

	OPEN event_id_cursor
	FETCH NEXT FROM event_id_cursor INTO @eventId

	WHILE @@FETCH_STATUS = 0   
	BEGIN   
		INSERT INTO #TempGroupsReport
		EXEC [dbo].[es_rpt_campaign_summary_report] @eventId
		SET @amountRaised = (SELECT @amountRaised + ISNULL(amount_sold, 0) FROM #TempGroupsReport)
		DELETE #TempGroupsReport
		FETCH NEXT FROM event_id_cursor INTO @eventId
	END   

	CLOSE event_id_cursor   
	DEALLOCATE event_id_cursor

	SELECT
	@totalMembers = ISNULL(COUNT(M.member_id), 0)
	FROM
	event_group EG (NOLOCK)
	JOIN event E (NOLOCK) ON EG.event_id = E.event_id
	JOIN event_participation EP (NOLOCK) ON E.event_id = EP.event_id AND EP.participation_channel_id <> 3
	JOIN member_hierarchy MH (NOLOCK) ON EP.member_hierarchy_id = MH.member_hierarchy_id
	JOIN member M (NOLOCK) ON MH.member_id = M.member_id
	WHERE EG.group_id = @groupId
	GROUP BY EG.group_id;

	DROP TABLE #TempGroupsReport

	SELECT ISNULL(@goal,0) AS [goal], ISNULL(@amountRaised,0) AS [amount_raised], ISNULL(@totalMembers,0) AS [total_members]
END
GO