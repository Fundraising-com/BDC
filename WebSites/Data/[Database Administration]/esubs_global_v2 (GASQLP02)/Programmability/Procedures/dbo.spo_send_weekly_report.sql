USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[sp_send_weekly_report]    Script Date: 02/01/2016 14:52:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: January 12, 2016
-- Description:	Sends an email with all the data from MGP
-- =============================================
ALTER PROCEDURE [dbo].[sp_send_weekly_report]
	@partner INT
AS
BEGIN
	DECLARE @breakLine VARCHAR(10) = '<br>';
	DECLARE @message NVARCHAR(MAX) = '';
	DECLARE @subject NVARCHAR(400) = '[EFUNDRAISING] [INFO] MGP Weekly Report';
	DECLARE @currentMTD VARCHAR(30) = '<strong>MTD' + CAST(DATEPART(YEAR, GETDATE()) AS VARCHAR(4)) + ': </strong>';
	DECLARE @previousMTD VARCHAR(30) = '<strong>MTD' + CAST(DATEPART(YEAR, DATEADD(YEAR, -1, GETDATE())) AS VARCHAR(4)) + ': </strong>';
	DECLARE @currentYTD VARCHAR(30) = '<strong>YTD' + CAST(DATEPART(YEAR, GETDATE()) AS VARCHAR(4)) + ': </strong>';
	DECLARE @previousYTD VARCHAR(30) = '<strong>YTD' + CAST(DATEPART(YEAR, DATEADD(YEAR, -1, GETDATE())) AS VARCHAR(4)) + ': </strong>';
	DECLARE @startDateCurrentYear DATETIME = DATEADD(yy, DATEDIFF(yy,0,getdate()), 0);
	DECLARE @startDatePreviousYear DATETIME = DATEADD(YEAR, -1, DATEADD(yy, DATEDIFF(yy,0,getdate()), 0));
	DECLARE @today DATETIME = GETDATE();
	DECLARE @todayPreviousYear DATETIME = DATEADD(YEAR, -1, GETDATE());
	DECLARE @startDateCurrentMonth DATETIME = DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0));
	DECLARE @startDatePreviousMonth DATETIME = DATEADD(YEAR, -1, DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0)));

	-- FINAL VALUES
	DECLARE @sponsorRegistrationsCurrentMTD INT = -1;
	DECLARE @sponsorRegistrationsCurrentYTD INT = -1;
	DECLARE @sponsorRegistrationsPreviousMTD INT = -1;
	DECLARE @sponsorRegistrationsPreviousYTD INT = -1;
	DECLARE @sponsorKickoffsCurrentMTD INT = -1;
	DECLARE @sponsorKickoffsCurrentYTD INT = -1;
	DECLARE @sponsorKickoffsPreviousMTD INT = -1;
	DECLARE @sponsorKickoffsPreviousYTD INT = -1;
	DECLARE @sponsorEmailsSentCurrentMTD INT = -1;
	DECLARE @sponsorEmailsSentCurrentYTD INT = -1;
	DECLARE @sponsorEmailsSentPreviousMTD INT = -1;
	DECLARE @sponsorEmailsSentPreviousYTD INT = -1;
	DECLARE @sponsorEmailsOpeningRatioCurrentMTD FLOAT = -1;
	DECLARE @sponsorEmailsOpeningRatioCurrentYTD FLOAT = -1;
	DECLARE @sponsorEmailsOpeningRatioPreviousMTD FLOAT = -1;
	DECLARE @sponsorEmailsOpeningRatioPreviousYTD FLOAT = -1;
	DECLARE @sponsorSalesCurrentMTD FLOAT = -1;
	DECLARE @sponsorSalesCurrentYTD FLOAT = -1;
	DECLARE @sponsorSalesPreviousMTD FLOAT = -1;
	DECLARE @sponsorSalesPreviousYTD FLOAT = -1;	
	DECLARE @memberRegistrationsCurrentMTD INT = -1;
	DECLARE @memberRegistrationsCurrentYTD INT = -1;
	DECLARE @memberRegistrationsPreviousMTD INT = -1;
	DECLARE @memberRegistrationsPreviousYTD INT = -1;
	DECLARE @memberKickoffsCurrentMTD INT = -1;
	DECLARE @memberKickoffsCurrentYTD INT = -1;
	DECLARE @memberKickoffsPreviousMTD INT = -1;
	DECLARE @memberKickoffsPreviousYTD INT = -1;
	DECLARE @memberEmailsSentCurrentMTD INT = -1;
	DECLARE @memberEmailsSentCurrentYTD INT = -1;
	DECLARE @memberEmailsSentPreviousMTD INT = -1;
	DECLARE @memberEmailsSentPreviousYTD INT = -1;
	DECLARE @memberSalesCurrentMTD FLOAT = -1;
	DECLARE @memberSalesCurrentYTD FLOAT = -1;
	DECLARE @memberSalesPreviousMTD FLOAT = -1;
	DECLARE @memberSalesPreviousYTD FLOAT = -1;
	DECLARE @memberEmailsOpeningRatioCurrentMTD FLOAT = -1;
	DECLARE @memberEmailsOpeningRatioCurrentYTD FLOAT = -1;
	DECLARE @memberEmailsOpeningRatioPreviousMTD FLOAT = -1;
	DECLARE @memberEmailsOpeningRatioPreviousYTD FLOAT = -1;
	-- SPONSOR REGISTRATIONS
	SELECT
		@sponsorRegistrationsCurrentMTD = COUNT(distinct e.event_id)
	FROM event_group eg (NOLOCK) 
	JOIN [group] g (NOLOCK) on eg.group_id=g.group_id
	JOIN event e (NOLOCK) on eg.event_id = e.event_id
	WHERE 
	e.create_date BETWEEN @startDateCurrentMonth AND @today
	AND g.partner_id = @partner;
	SELECT
		@sponsorRegistrationsCurrentYTD = COUNT(distinct e.event_id)
	FROM event_group eg (NOLOCK) 
	JOIN [group] g (NOLOCK) on eg.group_id=g.group_id
	JOIN event e (NOLOCK) on eg.event_id = e.event_id
	WHERE 
	e.create_date BETWEEN @startDateCurrentYear AND @today
	AND g.partner_id = @partner;
	SELECT
		@sponsorRegistrationsPreviousMTD = COUNT(distinct e.event_id)
	FROM event_group eg (NOLOCK) 
	JOIN [group] g (NOLOCK) on eg.group_id=g.group_id
	JOIN event e (NOLOCK) on eg.event_id = e.event_id
	WHERE 
	e.create_date BETWEEN @startDatePreviousMonth AND @todayPreviousYear
	AND g.partner_id = @partner;
	SELECT
		@sponsorRegistrationsPreviousYTD = COUNT(distinct e.event_id)
	FROM event_group eg (NOLOCK) 
	JOIN [group] g (NOLOCK) on eg.group_id=g.group_id
	JOIN event e (NOLOCK) on eg.event_id = e.event_id
	WHERE 
	e.create_date BETWEEN @startDatePreviousYear AND @todayPreviousYear
	AND g.partner_id = @partner;
	-- SPONSOR KICKOFFS AND EMAILS
	SELECT  
	 @sponsorKickoffsCurrentMTD = COUNT(v.event_id )
	,@sponsorEmailsSentCurrentMTD = SUM(KOImport) + SUM(KOManual)
	FROM (
	SELECT
	e.event_id
	, COUNT(ep.event_participation_id) as participation_count
	, SUM(case when mh.creation_channel_id IN(38, 23,46) then 1 else 0 end) as KOImport
	, SUM(case when mh.creation_channel_id IN(29, 7) then 1 else 0 end) as KOManual
	, SUM(case when mh.creation_channel_id IN(20) then 1 else 0 end) as KORoster
	, MIN (ep.create_date) as create_date
	FROM event as e with(nolock)
	JOIN event_participation as ep with(nolock)
	ON ep.event_id = e.event_id
	JOIN member_hierarchy as mh with(nolock)
	ON mh.member_hierarchy_id = ep.member_hierarchy_id
	JOIN member as m with(nolock)
	ON m.member_id = mh.member_id
	WHERE ( m.email_address is not null
	and m.email_address not like '%@efundraising.com')
	AND mh.creation_channel_id IN (7,20,23,38,29,23,46)
	and m.partner_id = @partner
	GROUP BY e.event_id
	) v
	WHERE v.create_date BETWEEN @startDateCurrentMonth and @today;
	SELECT  
	 @sponsorKickoffsCurrentYTD = COUNT(v.event_id )
	,@sponsorEmailsSentCurrentYTD = SUM(KOImport) + SUM(KOManual)
	FROM (
	SELECT
	e.event_id
	, COUNT(ep.event_participation_id) as participation_count
	, SUM(case when mh.creation_channel_id IN(38, 23,46) then 1 else 0 end) as KOImport
	, SUM(case when mh.creation_channel_id IN(29, 7) then 1 else 0 end) as KOManual
	, SUM(case when mh.creation_channel_id IN(20) then 1 else 0 end) as KORoster
	, MIN (ep.create_date) as create_date
	FROM event as e with(nolock)
	JOIN event_participation as ep with(nolock)
	ON ep.event_id = e.event_id
	JOIN member_hierarchy as mh with(nolock)
	ON mh.member_hierarchy_id = ep.member_hierarchy_id
	JOIN member as m with(nolock)
	ON m.member_id = mh.member_id
	WHERE ( m.email_address is not null
	and m.email_address not like '%@efundraising.com')
	AND mh.creation_channel_id IN (7,20,23,38,29,23,46)
	and m.partner_id = @partner
	GROUP BY e.event_id
	) v
	WHERE v.create_date BETWEEN @startDateCurrentYear and @today;
	SELECT  
	 @sponsorKickoffsPreviousMTD = COUNT(v.event_id )
	,@sponsorEmailsSentPreviousMTD = SUM(KOImport) + SUM(KOManual)
	FROM (
	SELECT
	e.event_id
	, COUNT(ep.event_participation_id) as participation_count
	, SUM(case when mh.creation_channel_id IN(38, 23,46) then 1 else 0 end) as KOImport
	, SUM(case when mh.creation_channel_id IN(29, 7) then 1 else 0 end) as KOManual
	, SUM(case when mh.creation_channel_id IN(20) then 1 else 0 end) as KORoster
	, MIN (ep.create_date) as create_date
	FROM event as e with(nolock)
	JOIN event_participation as ep with(nolock)
	ON ep.event_id = e.event_id
	JOIN member_hierarchy as mh with(nolock)
	ON mh.member_hierarchy_id = ep.member_hierarchy_id
	JOIN member as m with(nolock)
	ON m.member_id = mh.member_id
	WHERE ( m.email_address is not null
	and m.email_address not like '%@efundraising.com')
	AND mh.creation_channel_id IN (7,20,23,38,29,23,46)
	and m.partner_id = @partner
	GROUP BY e.event_id
	) v
	WHERE v.create_date BETWEEN @startDatePreviousMonth and @todayPreviousYear;
	SELECT  
	 @sponsorKickoffsPreviousYTD = COUNT(v.event_id )
	,@sponsorEmailsSentPreviousYTD = SUM(KOImport) + SUM(KOManual)
	FROM (
	SELECT
	e.event_id
	, COUNT(ep.event_participation_id) as participation_count
	, SUM(case when mh.creation_channel_id IN(38, 23,46) then 1 else 0 end) as KOImport
	, SUM(case when mh.creation_channel_id IN(29, 7) then 1 else 0 end) as KOManual
	, SUM(case when mh.creation_channel_id IN(20) then 1 else 0 end) as KORoster
	, MIN (ep.create_date) as create_date
	FROM event as e with(nolock)
	JOIN event_participation as ep with(nolock)
	ON ep.event_id = e.event_id
	JOIN member_hierarchy as mh with(nolock)
	ON mh.member_hierarchy_id = ep.member_hierarchy_id
	JOIN member as m with(nolock)
	ON m.member_id = mh.member_id
	WHERE ( m.email_address is not null
	and m.email_address not like '%@efundraising.com')
	AND mh.creation_channel_id IN (7,20,23,38,29,23,46)
	and m.partner_id = @partner
	GROUP BY e.event_id
	) v
	WHERE v.create_date BETWEEN @startDatePreviousYear and @todayPreviousYear;

-- SPONSOR EMAILS OPENING RATIO
	DECLARE @totalEmailsSent INT = 0;
	DECLARE @totalEmailsOpened INT = 0;
	
	SELECT @totalEmailsSent= SUM (totalEmailsSent) FROM
		(SELECT
		COUNT(DISTINCT t.touch_id) AS totalEmailsSent
		FROM event as e with(nolock)
		JOIN event_participation as ep with(nolock)	ON ep.event_id = e.event_id
		JOIN member_hierarchy as mh with(nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
		JOIN member as m with(nolock) ON m.member_id = mh.member_id
		JOIN touch as t with (NOLOCK) ON ep.event_participation_id = t.event_participation_id
		WHERE ( m.email_address is not null
		and m.email_address not like '%@efundraising.com')
		AND mh.creation_channel_id IN (7,20,23,38,29,23,46)
		and m.partner_id = @partner
		and ep.create_date BETWEEN @startDateCurrentMonth and @today
		GROUP BY t.touch_id) AS A
	SELECT @totalEmailsOpened= SUM (totalEmailsOpened) FROM
		(SELECT
		COUNT(DISTINCT t.touch_id) AS totalEmailsOpened
		FROM event as e with(nolock)
		JOIN event_participation as ep with(nolock)	ON ep.event_id = e.event_id
		JOIN member_hierarchy as mh with(nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
		JOIN member as m with(nolock) ON m.member_id = mh.member_id
		JOIN touch as t with (NOLOCK) ON ep.event_participation_id = t.event_participation_id
		JOIN touch_action as ta WITH (NOLOCK) ON t.touch_id = ta.touch_id AND ta.action_id = 108
		WHERE ( m.email_address is not null
		AND m.email_address not like '%@efundraising.com')
		AND mh.creation_channel_id IN (7,20,23,38,29,23,46)
		AND m.partner_id = @partner
		AND ep.create_date BETWEEN @startDateCurrentMonth and @today
		AND ta.touch_id is not null
		GROUP BY t.touch_id) AS B
	SET @sponsorEmailsOpeningRatioCurrentMTD = COALESCE(NULLIF(@totalEmailsOpened,0) * 1.0 / @totalEmailsSent * 100,0);
	
	SELECT @totalEmailsSent= SUM (totalEmailsSent) FROM
		(SELECT
		COUNT(DISTINCT t.touch_id) AS totalEmailsSent
		FROM event as e with(nolock)
		JOIN event_participation as ep with(nolock)	ON ep.event_id = e.event_id
		JOIN member_hierarchy as mh with(nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
		JOIN member as m with(nolock) ON m.member_id = mh.member_id
		JOIN touch as t with (NOLOCK) ON ep.event_participation_id = t.event_participation_id
		WHERE ( m.email_address is not null
		and m.email_address not like '%@efundraising.com')
		AND mh.creation_channel_id IN (7,20,23,38,29,23,46)
		and m.partner_id = @partner
		and ep.create_date BETWEEN @startDateCurrentYear and @today
		GROUP BY t.touch_id) AS A
	SELECT @totalEmailsOpened= SUM (totalEmailsOpened) FROM
		(SELECT
		COUNT(DISTINCT t.touch_id) AS totalEmailsOpened
		FROM event as e with(nolock)
		JOIN event_participation as ep with(nolock)	ON ep.event_id = e.event_id
		JOIN member_hierarchy as mh with(nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
		JOIN member as m with(nolock) ON m.member_id = mh.member_id
		JOIN touch as t with (NOLOCK) ON ep.event_participation_id = t.event_participation_id
		JOIN touch_action as ta WITH (NOLOCK) ON t.touch_id = ta.touch_id AND ta.action_id = 108
		WHERE ( m.email_address is not null
		AND m.email_address not like '%@efundraising.com')
		AND mh.creation_channel_id IN (7,20,23,38,29,23,46)
		AND m.partner_id = @partner
		AND ep.create_date BETWEEN @startDateCurrentYear and @today
		AND ta.touch_id is not null
		GROUP BY t.touch_id) AS B
	SET @sponsorEmailsOpeningRatioCurrentYTD = COALESCE(NULLIF(@totalEmailsOpened,0) * 1.0 / @totalEmailsSent * 100,0);
	SELECT @totalEmailsSent= SUM (totalEmailsSent) FROM
		(SELECT
		COUNT(DISTINCT t.touch_id) AS totalEmailsSent
		FROM event as e with(nolock)
		JOIN event_participation as ep with(nolock)	ON ep.event_id = e.event_id
		JOIN member_hierarchy as mh with(nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
		JOIN member as m with(nolock) ON m.member_id = mh.member_id
		JOIN touch as t with (NOLOCK) ON ep.event_participation_id = t.event_participation_id
		WHERE ( m.email_address is not null
		and m.email_address not like '%@efundraising.com')
		AND mh.creation_channel_id IN (7,20,23,38,29,23,46)
		and m.partner_id = @partner
		and ep.create_date BETWEEN @startDatePreviousMonth and @todayPreviousYear
		GROUP BY t.touch_id) AS A
	SELECT @totalEmailsOpened= SUM (totalEmailsOpened) FROM
		(SELECT
		COUNT(DISTINCT t.touch_id) AS totalEmailsOpened
		FROM event as e with(nolock)
		JOIN event_participation as ep with(nolock)	ON ep.event_id = e.event_id
		JOIN member_hierarchy as mh with(nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
		JOIN member as m with(nolock) ON m.member_id = mh.member_id
		JOIN touch as t with (NOLOCK) ON ep.event_participation_id = t.event_participation_id
		JOIN touch_action as ta WITH (NOLOCK) ON t.touch_id = ta.touch_id AND ta.action_id = 108
		WHERE ( m.email_address is not null
		AND m.email_address not like '%@efundraising.com')
		AND mh.creation_channel_id IN (7,20,23,38,29,23,46)
		AND m.partner_id = @partner
		AND ep.create_date BETWEEN @startDatePreviousMonth and @todayPreviousYear
		AND ta.touch_id is not null
		GROUP BY t.touch_id) AS B
	SET @sponsorEmailsOpeningRatioPreviousMTD = COALESCE(NULLIF(@totalEmailsOpened,0) * 1.0 / @totalEmailsSent * 100,0);
	SELECT @totalEmailsSent= SUM (totalEmailsSent) FROM
		(SELECT
		COUNT(DISTINCT t.touch_id) AS totalEmailsSent
		FROM event as e with(nolock)
		JOIN event_participation as ep with(nolock)	ON ep.event_id = e.event_id
		JOIN member_hierarchy as mh with(nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
		JOIN member as m with(nolock) ON m.member_id = mh.member_id
		JOIN touch as t with (NOLOCK) ON ep.event_participation_id = t.event_participation_id
		WHERE ( m.email_address is not null
		and m.email_address not like '%@efundraising.com')
		AND mh.creation_channel_id IN (7,20,23,38,29,23,46)
		and m.partner_id = @partner
		and ep.create_date BETWEEN @startDatePreviousYear and @todayPreviousYear
		GROUP BY t.touch_id) AS A
	SELECT @totalEmailsOpened= SUM (totalEmailsOpened) FROM
		(SELECT
		COUNT(DISTINCT t.touch_id) AS totalEmailsOpened
		FROM event as e with(nolock)
		JOIN event_participation as ep with(nolock)	ON ep.event_id = e.event_id
		JOIN member_hierarchy as mh with(nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
		JOIN member as m with(nolock) ON m.member_id = mh.member_id
		JOIN touch as t with (NOLOCK) ON ep.event_participation_id = t.event_participation_id
		JOIN touch_action as ta WITH (NOLOCK) ON t.touch_id = ta.touch_id AND ta.action_id = 108
		WHERE ( m.email_address is not null
		AND m.email_address not like '%@efundraising.com')
		AND mh.creation_channel_id IN (7,20,23,38,29,23,46)
		AND m.partner_id = @partner
		AND ep.create_date BETWEEN @startDatePreviousYear and @todayPreviousYear
		AND ta.touch_id is not null
		GROUP BY t.touch_id) AS B
	SET @sponsorEmailsOpeningRatioPreviousYTD = COALESCE(NULLIF(@totalEmailsOpened,0) * 1.0 / @totalEmailsSent * 100,0);	
	-- SPONSOR SALES
	SELECT
	@sponsorSalesCurrentMTD = SUM(x.TotalAmount)
	FROM
		(SELECT 
			ISNULL(SUM(tps.quantity), 0) as [NoProducts],
			ISNULL(SUM(quantity*price), 0) as [TotalAmount]
		FROM 
			DW.es_valid_orders_items tps (NOLOCK)
			join event_participation ep (nolock) on tps.supp_id = ep.event_participation_id
			join event_group eg (nolock) on ep.event_id = eg.event_id
			join [group] g (nolock) on eg.group_id = g.group_id
		WHERE
			ep.create_date BETWEEN @startDateCurrentMonth and @today 
			and ep.participation_channel_id = 3
			and g.partner_id = @partner
		UNION
		SELECT 
			ISNULL(SUM(tps.quantity), 0) as [NoProducts],
			ISNULL(SUM(quantity*price), 0) as [TotalAmount]
		FROM 
			DW.es_valid_orders_items tps (NOLOCK)
			join event_participation ep (nolock) on tps.supp_id = ep.event_participation_id
			join event_group eg (nolock) on ep.event_id = eg.event_id
			join [group] g (nolock) on eg.group_id = g.group_id
			join member_hierarchy mh (NOLOCK) ON ep.member_hierarchy_id = mh.member_hierarchy_id
			JOIN member_hierarchy mh2 (NOLOCK) ON mh.parent_member_hierarchy_id = mh2.member_hierarchy_id
			JOIN event_participation ep2 (NOLOCK) ON mh2.member_hierarchy_id = ep2.member_hierarchy_id AND ep2.participation_channel_id = 3
		WHERE
			ep.create_date BETWEEN @startDateCurrentMonth and @today 
			and ep.participation_channel_id = 1
			and g.partner_id = @partner	) AS x;
	SELECT
	@sponsorSalesCurrentYTD = SUM(x.TotalAmount)
	FROM
		(SELECT 
			ISNULL(SUM(tps.quantity), 0) as [NoProducts],
			ISNULL(SUM(quantity*price), 0) as [TotalAmount]
		FROM 
			DW.es_valid_orders_items tps (NOLOCK)
			join event_participation ep (nolock) on tps.supp_id = ep.event_participation_id
			join event_group eg (nolock) on ep.event_id = eg.event_id
			join [group] g (nolock) on eg.group_id = g.group_id
		WHERE
			ep.create_date BETWEEN @startDateCurrentYear and @today 
			and ep.participation_channel_id = 3
			and g.partner_id = @partner
		UNION
		SELECT 
			ISNULL(SUM(tps.quantity), 0) as [NoProducts],
			ISNULL(SUM(quantity*price), 0) as [TotalAmount]
		FROM 
			DW.es_valid_orders_items tps (NOLOCK)
			join event_participation ep (nolock) on tps.supp_id = ep.event_participation_id
			join event_group eg (nolock) on ep.event_id = eg.event_id
			join [group] g (nolock) on eg.group_id = g.group_id
			join member_hierarchy mh (NOLOCK) ON ep.member_hierarchy_id = mh.member_hierarchy_id
			JOIN member_hierarchy mh2 (NOLOCK) ON mh.parent_member_hierarchy_id = mh2.member_hierarchy_id
			JOIN event_participation ep2 (NOLOCK) ON mh2.member_hierarchy_id = ep2.member_hierarchy_id AND ep2.participation_channel_id = 3
		WHERE
			ep.create_date BETWEEN @startDateCurrentYear and @today 
			and ep.participation_channel_id = 1
			and g.partner_id = @partner	) AS x;
	SELECT
	@sponsorSalesPreviousMTD = SUM(x.TotalAmount)
	FROM
		(SELECT 
			ISNULL(SUM(tps.quantity), 0) as [NoProducts],
			ISNULL(SUM(quantity*price), 0) as [TotalAmount]
		FROM 
			DW.es_valid_orders_items tps (NOLOCK)
			join event_participation ep (nolock) on tps.supp_id = ep.event_participation_id
			join event_group eg (nolock) on ep.event_id = eg.event_id
			join [group] g (nolock) on eg.group_id = g.group_id
		WHERE
			ep.create_date BETWEEN @startDatePreviousMonth and @todayPreviousYear
			and ep.participation_channel_id = 3
			and g.partner_id = @partner
		UNION
		SELECT 
			ISNULL(SUM(tps.quantity), 0) as [NoProducts],
			ISNULL(SUM(quantity*price), 0) as [TotalAmount]
		FROM 
			DW.es_valid_orders_items tps (NOLOCK)
			join event_participation ep (nolock) on tps.supp_id = ep.event_participation_id
			join event_group eg (nolock) on ep.event_id = eg.event_id
			join [group] g (nolock) on eg.group_id = g.group_id
			join member_hierarchy mh (NOLOCK) ON ep.member_hierarchy_id = mh.member_hierarchy_id
			JOIN member_hierarchy mh2 (NOLOCK) ON mh.parent_member_hierarchy_id = mh2.member_hierarchy_id
			JOIN event_participation ep2 (NOLOCK) ON mh2.member_hierarchy_id = ep2.member_hierarchy_id AND ep2.participation_channel_id = 3
		WHERE
			ep.create_date BETWEEN @startDatePreviousMonth and @todayPreviousYear
			and ep.participation_channel_id = 1
			and g.partner_id = @partner	) AS x;	
	SELECT
	@sponsorSalesPreviousYTD = SUM(x.TotalAmount)
	FROM
		(SELECT 
			ISNULL(SUM(tps.quantity), 0) as [NoProducts],
			ISNULL(SUM(quantity*price), 0) as [TotalAmount]
		FROM 
			DW.es_valid_orders_items tps (NOLOCK)
			join event_participation ep (nolock) on tps.supp_id = ep.event_participation_id
			join event_group eg (nolock) on ep.event_id = eg.event_id
			join [group] g (nolock) on eg.group_id = g.group_id
		WHERE
			ep.create_date BETWEEN @startDatePreviousYear and @todayPreviousYear
			and ep.participation_channel_id = 3
			and g.partner_id = @partner
		UNION
		SELECT 
			ISNULL(SUM(tps.quantity), 0) as [NoProducts],
			ISNULL(SUM(quantity*price), 0) as [TotalAmount]
		FROM 
			DW.es_valid_orders_items tps (NOLOCK)
			join event_participation ep (nolock) on tps.supp_id = ep.event_participation_id
			join event_group eg (nolock) on ep.event_id = eg.event_id
			join [group] g (nolock) on eg.group_id = g.group_id
			join member_hierarchy mh (NOLOCK) ON ep.member_hierarchy_id = mh.member_hierarchy_id
			JOIN member_hierarchy mh2 (NOLOCK) ON mh.parent_member_hierarchy_id = mh2.member_hierarchy_id
			JOIN event_participation ep2 (NOLOCK) ON mh2.member_hierarchy_id = ep2.member_hierarchy_id AND ep2.participation_channel_id = 3
		WHERE
			ep.create_date BETWEEN @startDatePreviousYear and @todayPreviousYear
			and ep.participation_channel_id = 1
			and g.partner_id = @partner	) AS x;	
		
	-- MEMBER REGISTRATIONS	
	SELECT 
	@memberRegistrationsCurrentMTD = count(distinct u.user_id)
	from event_participation ep (nolock)
	join event e (nolock) on ep.event_id = e.event_id
	join member_hierarchy mh (nolock) on ep.member_hierarchy_id = mh.member_hierarchy_id
	join member m (nolock) on mh.member_id = m.member_id
	join [users] u (NOLOCK) on m.user_id = u.user_id
	where 
	mh.active = 1 and ep.participation_channel_id <> 3
	and ( m.email_address is not null
	and m.email_address not like '%@efundraising.com')
	and u.create_date BETWEEN @startDateCurrentMonth and @today 
	and m.partner_id = @partner;
	SELECT 
	@memberRegistrationsCurrentyTD = count(distinct u.user_id)
	from event_participation ep (nolock)
	join event e (nolock) on ep.event_id = e.event_id
	join member_hierarchy mh (nolock) on ep.member_hierarchy_id = mh.member_hierarchy_id
	join member m (nolock) on mh.member_id = m.member_id
	join [users] u (NOLOCK) on m.user_id = u.user_id
	where 
	mh.active = 1 and ep.participation_channel_id <> 3
	and ( m.email_address is not null
	and m.email_address not like '%@efundraising.com')
	and u.create_date BETWEEN @startDateCurrentYear and @today 
	and m.partner_id = @partner;
	SELECT 
	@memberRegistrationsPreviousMTD = count(distinct u.user_id)
	from event_participation ep (nolock)
	join event e (nolock) on ep.event_id = e.event_id
	join member_hierarchy mh (nolock) on ep.member_hierarchy_id = mh.member_hierarchy_id
	join member m (nolock) on mh.member_id = m.member_id
	join [users] u (NOLOCK) on m.user_id = u.user_id
	where 
	mh.active = 1 and ep.participation_channel_id <> 3
	and ( m.email_address is not null
	and m.email_address not like '%@efundraising.com')
	and u.create_date BETWEEN @startDatePreviousMonth and @todayPreviousYear
	and m.partner_id = @partner;
	SELECT 
	@memberRegistrationsPreviousYTD = count(distinct u.user_id)
	from event_participation ep (nolock)
	join event e (nolock) on ep.event_id = e.event_id
	join member_hierarchy mh (nolock) on ep.member_hierarchy_id = mh.member_hierarchy_id
	join member m (nolock) on mh.member_id = m.member_id
	join [users] u (NOLOCK) on m.user_id = u.user_id
	where 
	mh.active = 1 and ep.participation_channel_id <> 3
	and ( m.email_address is not null
	and m.email_address not like '%@efundraising.com')
	and u.create_date BETWEEN @startDatePreviousYear and @todayPreviousYear 
	and m.partner_id = @partner;

	---- MEMBER KICKOFFS AND EMAILS
	SELECT 
	@memberKickoffsCurrentMTD = COUNT(v.event_id ),
	@memberEmailsSentCurrentMTD = ISNULL(SUM(ISNULL(KOImport,0)) + SUM(ISNULL(KOManual,0)),0)
	FROM (
	SELECT
	e.event_id
	, COUNT(ep.event_participation_id) as participation_count
	, SUM(case when mh.creation_channel_id IN(14) then 1 else 0 end) as KOImport
	, SUM(case when mh.creation_channel_id IN(12) then 1 else 0 end) as KOManual
	, MIN (ep.create_date) as create_date
	FROM event as e with(nolock)
	JOIN event_participation as ep with(nolock)
	ON ep.event_id = e.event_id
	JOIN member_hierarchy as mh with(nolock)
	ON mh.member_hierarchy_id = ep.member_hierarchy_id
	JOIN member as m with(nolock)
	ON m.member_id = mh.member_id
	WHERE ( m.email_address is not null
	and m.email_address not like '%@efundraising.com')
	AND mh.creation_channel_id IN (12,14)
	AND M.partner_id = @partner
	GROUP BY e.event_id
	) v
	WHERE v.create_date BETWEEN @startDateCurrentMonth and @today;
	SELECT 
	@memberKickoffsCurrentYTD = COUNT(v.event_id ),
	@memberEmailsSentCurrentYTD = SUM(KOImport) + SUM(KOManual)
	FROM (
	SELECT
	e.event_id
	, COUNT(ep.event_participation_id) as participation_count
	, SUM(case when mh.creation_channel_id IN(14) then 1 else 0 end) as KOImport
	, SUM(case when mh.creation_channel_id IN(12) then 1 else 0 end) as KOManual
	, MIN (ep.create_date) as create_date
	FROM event as e with(nolock)
	JOIN event_participation as ep with(nolock)
	ON ep.event_id = e.event_id
	JOIN member_hierarchy as mh with(nolock)
	ON mh.member_hierarchy_id = ep.member_hierarchy_id
	JOIN member as m with(nolock)
	ON m.member_id = mh.member_id
	WHERE ( m.email_address is not null
	and m.email_address not like '%@efundraising.com')
	AND mh.creation_channel_id IN (12,14)
	AND M.partner_id = @partner
	GROUP BY e.event_id
	) v
	WHERE v.create_date BETWEEN @startDateCurrentYear and @today;
	SELECT 
	@memberKickoffsPreviousMTD = COUNT(v.event_id ),
	@memberEmailsSentPreviousMTD = SUM(KOImport) + SUM(KOManual)
	FROM (
	SELECT
	e.event_id
	, COUNT(ep.event_participation_id) as participation_count
	, SUM(case when mh.creation_channel_id IN(14) then 1 else 0 end) as KOImport
	, SUM(case when mh.creation_channel_id IN(12) then 1 else 0 end) as KOManual
	, MIN (ep.create_date) as create_date
	FROM event as e with(nolock)
	JOIN event_participation as ep with(nolock)
	ON ep.event_id = e.event_id
	JOIN member_hierarchy as mh with(nolock)
	ON mh.member_hierarchy_id = ep.member_hierarchy_id
	JOIN member as m with(nolock)
	ON m.member_id = mh.member_id
	WHERE ( m.email_address is not null
	and m.email_address not like '%@efundraising.com')
	AND mh.creation_channel_id IN (12,14)
	AND M.partner_id = @partner
	GROUP BY e.event_id
	) v
	WHERE v.create_date BETWEEN @startDatePreviousMonth and @todayPreviousYear;
	SELECT 
	@memberKickoffsPreviousYTD = COUNT(v.event_id ),
	@memberEmailsSentPreviousYTD = SUM(KOImport) + SUM(KOManual)
	FROM (
	SELECT
	e.event_id
	, COUNT(ep.event_participation_id) as participation_count
	, SUM(case when mh.creation_channel_id IN(14) then 1 else 0 end) as KOImport
	, SUM(case when mh.creation_channel_id IN(12) then 1 else 0 end) as KOManual
	, MIN (ep.create_date) as create_date
	FROM event as e with(nolock)
	JOIN event_participation as ep with(nolock)
	ON ep.event_id = e.event_id
	JOIN member_hierarchy as mh with(nolock)
	ON mh.member_hierarchy_id = ep.member_hierarchy_id
	JOIN member as m with(nolock)
	ON m.member_id = mh.member_id
	WHERE ( m.email_address is not null
	and m.email_address not like '%@efundraising.com')
	AND mh.creation_channel_id IN (12,14)
	AND M.partner_id = @partner
	GROUP BY e.event_id
	) v
	WHERE v.create_date BETWEEN @startDatePreviousYear and @todayPreviousYear;

	---- MEMBER EMAILS OPENING RATIO
	SELECT @totalEmailsSent= SUM (totalEmailsSent) FROM
		(SELECT
		COUNT(DISTINCT t.touch_id) AS totalEmailsSent
		FROM event as e with(nolock)
		JOIN event_participation as ep with(nolock)	ON ep.event_id = e.event_id
		JOIN member_hierarchy as mh with(nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
		JOIN member as m with(nolock) ON m.member_id = mh.member_id
		JOIN touch as t with (NOLOCK) ON ep.event_participation_id = t.event_participation_id
		WHERE ( m.email_address is not null
		and m.email_address not like '%@efundraising.com')
		AND mh.creation_channel_id IN (12,14,19)
		and m.partner_id = @partner
		and ep.create_date BETWEEN @startDateCurrentMonth and @today
		GROUP BY t.touch_id) AS A
	SELECT @totalEmailsOpened= SUM (totalEmailsOpened) FROM
		(SELECT
		COUNT(DISTINCT t.touch_id) AS totalEmailsOpened
		FROM event as e with(nolock)
		JOIN event_participation as ep with(nolock)	ON ep.event_id = e.event_id
		JOIN member_hierarchy as mh with(nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
		JOIN member as m with(nolock) ON m.member_id = mh.member_id
		JOIN touch as t with (NOLOCK) ON ep.event_participation_id = t.event_participation_id
		JOIN touch_action as ta WITH (NOLOCK) ON t.touch_id = ta.touch_id AND ta.action_id = 108
		WHERE ( m.email_address is not null
		AND m.email_address not like '%@efundraising.com')
		AND mh.creation_channel_id IN (12,14,19)
		AND m.partner_id = @partner
		AND ep.create_date BETWEEN @startDateCurrentMonth and @today
		AND ta.touch_id is not null
		GROUP BY t.touch_id) AS B
	SET @memberEmailsOpeningRatioCurrentMTD = COALESCE(NULLIF(@totalEmailsOpened,0) * 1.0 / @totalEmailsSent * 100,0);	
	SELECT @totalEmailsSent= SUM (totalEmailsSent) FROM
		(SELECT
		COUNT(DISTINCT t.touch_id) AS totalEmailsSent
		FROM event as e with(nolock)
		JOIN event_participation as ep with(nolock)	ON ep.event_id = e.event_id
		JOIN member_hierarchy as mh with(nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
		JOIN member as m with(nolock) ON m.member_id = mh.member_id
		JOIN touch as t with (NOLOCK) ON ep.event_participation_id = t.event_participation_id
		WHERE ( m.email_address is not null
		and m.email_address not like '%@efundraising.com')
		AND mh.creation_channel_id IN (12,14,19)
		and m.partner_id = @partner
		and ep.create_date BETWEEN @startDateCurrentYear and @today
		GROUP BY t.touch_id) AS A
	SELECT @totalEmailsOpened= SUM (totalEmailsOpened) FROM
		(SELECT
		COUNT(DISTINCT t.touch_id) AS totalEmailsOpened
		FROM event as e with(nolock)
		JOIN event_participation as ep with(nolock)	ON ep.event_id = e.event_id
		JOIN member_hierarchy as mh with(nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
		JOIN member as m with(nolock) ON m.member_id = mh.member_id
		JOIN touch as t with (NOLOCK) ON ep.event_participation_id = t.event_participation_id
		JOIN touch_action as ta WITH (NOLOCK) ON t.touch_id = ta.touch_id AND ta.action_id = 108
		WHERE ( m.email_address is not null
		AND m.email_address not like '%@efundraising.com')
		AND mh.creation_channel_id IN (12,14,19)
		AND m.partner_id = @partner
		AND ep.create_date BETWEEN @startDateCurrentYear and @today
		AND ta.touch_id is not null
		GROUP BY t.touch_id) AS B
	SET @memberEmailsOpeningRatioCurrentYTD = COALESCE(NULLIF(@totalEmailsOpened,0) * 1.0 / @totalEmailsSent * 100,0);
	SELECT @totalEmailsSent= SUM (totalEmailsSent) FROM
		(SELECT
		COUNT(DISTINCT t.touch_id) AS totalEmailsSent
		FROM event as e with(nolock)
		JOIN event_participation as ep with(nolock)	ON ep.event_id = e.event_id
		JOIN member_hierarchy as mh with(nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
		JOIN member as m with(nolock) ON m.member_id = mh.member_id
		JOIN touch as t with (NOLOCK) ON ep.event_participation_id = t.event_participation_id
		WHERE ( m.email_address is not null
		and m.email_address not like '%@efundraising.com')
		AND mh.creation_channel_id IN (12,14,19)
		and m.partner_id = @partner
		and ep.create_date BETWEEN @startDatePreviousMonth and @todayPreviousYear
		GROUP BY t.touch_id) AS A
	SELECT @totalEmailsOpened= SUM (totalEmailsOpened) FROM
		(SELECT
		COUNT(DISTINCT t.touch_id) AS totalEmailsOpened
		FROM event as e with(nolock)
		JOIN event_participation as ep with(nolock)	ON ep.event_id = e.event_id
		JOIN member_hierarchy as mh with(nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
		JOIN member as m with(nolock) ON m.member_id = mh.member_id
		JOIN touch as t with (NOLOCK) ON ep.event_participation_id = t.event_participation_id
		JOIN touch_action as ta WITH (NOLOCK) ON t.touch_id = ta.touch_id AND ta.action_id = 108
		WHERE ( m.email_address is not null
		AND m.email_address not like '%@efundraising.com')
		AND mh.creation_channel_id IN (12,14,19)
		AND m.partner_id = @partner
		AND ep.create_date BETWEEN @startDatePreviousMonth and @todayPreviousYear
		AND ta.touch_id is not null
		GROUP BY t.touch_id) AS B
	SET @memberEmailsOpeningRatioPreviousMTD = COALESCE(NULLIF(@totalEmailsOpened,0) * 1.0 / @totalEmailsSent * 100,0);
	SELECT @totalEmailsSent= SUM (totalEmailsSent) FROM
		(SELECT
		COUNT(DISTINCT t.touch_id) AS totalEmailsSent
		FROM event as e with(nolock)
		JOIN event_participation as ep with(nolock)	ON ep.event_id = e.event_id
		JOIN member_hierarchy as mh with(nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
		JOIN member as m with(nolock) ON m.member_id = mh.member_id
		JOIN touch as t with (NOLOCK) ON ep.event_participation_id = t.event_participation_id
		WHERE ( m.email_address is not null
		and m.email_address not like '%@efundraising.com')
		AND mh.creation_channel_id IN (12,14,19)
		and m.partner_id = @partner
		and ep.create_date BETWEEN @startDatePreviousYear and @todayPreviousYear
		GROUP BY t.touch_id) AS A
	SELECT @totalEmailsOpened= SUM (totalEmailsOpened) FROM
		(SELECT
		COUNT(DISTINCT t.touch_id) AS totalEmailsOpened
		FROM event as e with(nolock)
		JOIN event_participation as ep with(nolock)	ON ep.event_id = e.event_id
		JOIN member_hierarchy as mh with(nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
		JOIN member as m with(nolock) ON m.member_id = mh.member_id
		JOIN touch as t with (NOLOCK) ON ep.event_participation_id = t.event_participation_id
		JOIN touch_action as ta WITH (NOLOCK) ON t.touch_id = ta.touch_id AND ta.action_id = 108
		WHERE ( m.email_address is not null
		AND m.email_address not like '%@efundraising.com')
		AND mh.creation_channel_id IN (12,14,19)
		AND m.partner_id = @partner
		AND ep.create_date BETWEEN @startDatePreviousYear and @todayPreviousYear
		AND ta.touch_id is not null
		GROUP BY t.touch_id) AS B
	SET @memberEmailsOpeningRatioPreviousYTD = COALESCE(NULLIF(@totalEmailsOpened,0) * 1.0 / @totalEmailsSent * 100,0);	

	-- MEMBER SALES
	SELECT
	@memberSalesCurrentMTD = SUM(x.TotalAmount)
	FROM
		(SELECT 
			ISNULL(SUM(tps.quantity), 0) as [NoProducts],
			ISNULL(SUM(quantity*price), 0) as [TotalAmount]
		FROM 
			DW.es_valid_orders_items tps (NOLOCK)
			join event_participation ep (nolock) on tps.supp_id = ep.event_participation_id
			join event_group eg (nolock) on ep.event_id = eg.event_id
			join [group] g (nolock) on eg.group_id = g.group_id
		WHERE
			ep.create_date BETWEEN @startDateCurrentMonth and @today 
			and ep.participation_channel_id = 2
			and g.partner_id = @partner
		UNION
		SELECT 
			ISNULL(SUM(tps.quantity), 0) as [NoProducts],
			ISNULL(SUM(quantity*price), 0) as [TotalAmount]
		FROM 
			DW.es_valid_orders_items tps (NOLOCK)
			join event_participation ep (nolock) on tps.supp_id = ep.event_participation_id
			join event_group eg (nolock) on ep.event_id = eg.event_id
			join [group] g (nolock) on eg.group_id = g.group_id
			join member_hierarchy mh (NOLOCK) ON ep.member_hierarchy_id = mh.member_hierarchy_id
			JOIN member_hierarchy mh2 (NOLOCK) ON mh.parent_member_hierarchy_id = mh2.member_hierarchy_id
			JOIN event_participation ep2 (NOLOCK) ON mh2.member_hierarchy_id = ep2.member_hierarchy_id AND ep2.participation_channel_id = 2
		WHERE
			ep.create_date BETWEEN @startDateCurrentMonth and @today 
			and ep.participation_channel_id = 1
			and g.partner_id = @partner	) AS x;
	SELECT
	@memberSalesCurrentYTD = SUM(x.TotalAmount)
	FROM
		(SELECT 
			ISNULL(SUM(tps.quantity), 0) as [NoProducts],
			ISNULL(SUM(quantity*price), 0) as [TotalAmount]
		FROM 
			DW.es_valid_orders_items tps (NOLOCK)
			join event_participation ep (nolock) on tps.supp_id = ep.event_participation_id
			join event_group eg (nolock) on ep.event_id = eg.event_id
			join [group] g (nolock) on eg.group_id = g.group_id
		WHERE
			ep.create_date BETWEEN @startDateCurrentYear and @today 
			and ep.participation_channel_id = 2
			and g.partner_id = @partner
		UNION
		SELECT 
			ISNULL(SUM(tps.quantity), 0) as [NoProducts],
			ISNULL(SUM(quantity*price), 0) as [TotalAmount]
		FROM 
			DW.es_valid_orders_items tps (NOLOCK)
			join event_participation ep (nolock) on tps.supp_id = ep.event_participation_id
			join event_group eg (nolock) on ep.event_id = eg.event_id
			join [group] g (nolock) on eg.group_id = g.group_id
			join member_hierarchy mh (NOLOCK) ON ep.member_hierarchy_id = mh.member_hierarchy_id
			JOIN member_hierarchy mh2 (NOLOCK) ON mh.parent_member_hierarchy_id = mh2.member_hierarchy_id
			JOIN event_participation ep2 (NOLOCK) ON mh2.member_hierarchy_id = ep2.member_hierarchy_id AND ep2.participation_channel_id = 2
		WHERE
			ep.create_date BETWEEN @startDateCurrentYear and @today 
			and ep.participation_channel_id = 1
			and g.partner_id = @partner	) AS x;
	SELECT
	@memberSalesPreviousMTD = SUM(x.TotalAmount)
	FROM
		(SELECT 
			ISNULL(SUM(tps.quantity), 0) as [NoProducts],
			ISNULL(SUM(quantity*price), 0) as [TotalAmount]
		FROM 
			DW.es_valid_orders_items tps (NOLOCK)
			join event_participation ep (nolock) on tps.supp_id = ep.event_participation_id
			join event_group eg (nolock) on ep.event_id = eg.event_id
			join [group] g (nolock) on eg.group_id = g.group_id
		WHERE
			ep.create_date BETWEEN @startDatePreviousMonth and @todayPreviousYear
			and ep.participation_channel_id = 2
			and g.partner_id = @partner
		UNION
		SELECT 
			ISNULL(SUM(tps.quantity), 0) as [NoProducts],
			ISNULL(SUM(quantity*price), 0) as [TotalAmount]
		FROM 
			DW.es_valid_orders_items tps (NOLOCK)
			join event_participation ep (nolock) on tps.supp_id = ep.event_participation_id
			join event_group eg (nolock) on ep.event_id = eg.event_id
			join [group] g (nolock) on eg.group_id = g.group_id
			join member_hierarchy mh (NOLOCK) ON ep.member_hierarchy_id = mh.member_hierarchy_id
			JOIN member_hierarchy mh2 (NOLOCK) ON mh.parent_member_hierarchy_id = mh2.member_hierarchy_id
			JOIN event_participation ep2 (NOLOCK) ON mh2.member_hierarchy_id = ep2.member_hierarchy_id AND ep2.participation_channel_id = 2
		WHERE
			ep.create_date BETWEEN @startDatePreviousMonth and @todayPreviousYear
			and ep.participation_channel_id = 1
			and g.partner_id = @partner	) AS x;	
	SELECT
	@memberSalesPreviousYTD = SUM(x.TotalAmount)
	FROM
		(SELECT 
			ISNULL(SUM(tps.quantity), 0) as [NoProducts],
			ISNULL(SUM(quantity*price), 0) as [TotalAmount]
		FROM 
			DW.es_valid_orders_items tps (NOLOCK)
			join event_participation ep (nolock) on tps.supp_id = ep.event_participation_id
			join event_group eg (nolock) on ep.event_id = eg.event_id
			join [group] g (nolock) on eg.group_id = g.group_id
		WHERE
			ep.create_date BETWEEN @startDatePreviousYear and @todayPreviousYear
			and ep.participation_channel_id = 2
			and g.partner_id = @partner
		UNION
		SELECT 
			ISNULL(SUM(tps.quantity), 0) as [NoProducts],
			ISNULL(SUM(quantity*price), 0) as [TotalAmount]
		FROM 
			DW.es_valid_orders_items tps (NOLOCK)
			join event_participation ep (nolock) on tps.supp_id = ep.event_participation_id
			join event_group eg (nolock) on ep.event_id = eg.event_id
			join [group] g (nolock) on eg.group_id = g.group_id
			join member_hierarchy mh (NOLOCK) ON ep.member_hierarchy_id = mh.member_hierarchy_id
			JOIN member_hierarchy mh2 (NOLOCK) ON mh.parent_member_hierarchy_id = mh2.member_hierarchy_id
			JOIN event_participation ep2 (NOLOCK) ON mh2.member_hierarchy_id = ep2.member_hierarchy_id AND ep2.participation_channel_id = 2
		WHERE
			ep.create_date BETWEEN @startDatePreviousYear and @todayPreviousYear
			and ep.participation_channel_id = 1
			and g.partner_id = @partner	) AS x;	

	SET @message = @message + '<body style=''font-family: Verdana; font-size: 11px''>';
	SET @message = @message + '<h2>===== MGP Weekly Report =====</h2>' + @breakLine;
	SET @message = @message + '<i>Created on: ' + CAST(GETDATE() AS VARCHAR(100)) + '</i>' + @breakLine ;
	SET @message = @message + '<i>Partner: ' + CAST(@partner AS VARCHAR(5)) + '</i>' + @breakLine;
	SET @message = @message + '<h4>Statistics:</h4>';
	IF @sponsorKickoffsCurrentMTD > 0 AND @sponsorRegistrationsCurrentMTD > 0
		SET @message = @message + @currentMTD + 'Out of 100 Sponsors who registered, <u>' + CAST(CAST((@sponsorKickoffsCurrentMTD * 100.0 / @sponsorRegistrationsCurrentMTD) AS NUMERIC(36,2)) AS VARCHAR(10)) + '</u> kicked off' + @breakLine;
	IF @sponsorKickoffsPreviousMTD > 0 AND @sponsorRegistrationsPreviousMTD > 0
		SET @message = @message + @previousMTD + 'Out of 100 Sponsors who registered, <u>' + CAST(CAST((@sponsorKickoffsPreviousMTD * 100.0 / @sponsorRegistrationsPreviousMTD) AS NUMERIC(36,2)) AS VARCHAR(10)) + '</u> kicked off' + @breakLine;
	IF @sponsorKickoffsCurrentYTD > 0 AND @sponsorRegistrationsCurrentYTD > 0
		SET @message = @message + @currentYTD + 'Out of 100 Sponsors who registered, <u>' + CAST(CAST((@sponsorKickoffsCurrentYTD * 100.0 / @sponsorRegistrationsCurrentYTD) AS NUMERIC(36,2)) AS VARCHAR(10)) + '</u> kicked off' + @breakLine;	
	IF @sponsorKickoffsPreviousYTD > 0 AND @sponsorRegistrationsPreviousYTD > 0
		SET @message = @message + @previousYTD + 'Out of 100 Sponsors who registered, <u>' + CAST(CAST((@sponsorKickoffsPreviousYTD * 100.0 / @sponsorRegistrationsPreviousYTD) AS NUMERIC(36,2)) AS VARCHAR(10)) + '</u> kicked off' + @breakLine;
	SET @message = @message + @breakLine;	
	IF @memberKickoffsCurrentMTD > 0 AND @memberRegistrationsCurrentMTD > 0
		SET @message = @message + @currentMTD + 'Out of 100 Members who registered, <u>' + CAST(CAST((@memberKickoffsCurrentMTD * 100.0 / @memberRegistrationsCurrentMTD) AS NUMERIC(36,2)) AS VARCHAR(10)) + '</u> kicked off' + @breakLine;
	IF @memberKickoffsPreviousMTD > 0 AND @memberRegistrationsPreviousMTD > 0
		SET @message = @message + @previousMTD + 'Out of 100 Members who registered, <u>' + CAST(CAST((@memberKickoffsPreviousMTD * 100.0 / @memberRegistrationsPreviousMTD) AS NUMERIC(36,2)) AS VARCHAR(10)) + '</u> kicked off' + @breakLine;	
	IF @memberKickoffsCurrentYTD > 0 AND @memberRegistrationsCurrentYTD > 0
		SET @message = @message + @currentYTD + 'Out of 100 Members who registered, <u>' + CAST(CAST((@memberKickoffsCurrentYTD * 100.0 / @memberRegistrationsCurrentYTD) AS NUMERIC(36,2)) AS VARCHAR(10)) + '</u> kicked off' + @breakLine;	
	IF @memberKickoffsPreviousYTD > 0 AND @memberRegistrationsPreviousYTD > 0
		SET @message = @message + @previousYTD + 'Out of 100 Members who registered, <u>' + CAST(CAST((@memberKickoffsPreviousYTD * 100.0 / @memberRegistrationsPreviousYTD) AS NUMERIC(36,2)) AS VARCHAR(10)) + '</u> kicked off' + @breakLine;
	SET @message = @message + @breakLine;
	SET @message = @message + '<h4>Details:</h4>';
	SET @message = @message + '<table style=''font-family: Verdana; font-size: 11px; border: solid 1px black;'' border=''1''><thead style=''background-color: #006699; color: white; border: solid 1px black;''><tr><th>Sponsor</th><th>Registrations</th><th>Kickoffs</th><th>Kickoffs per Registration</th><th>Emails Sent</th><th>Emails per Kickoff</th><th>Emails Opening Ratio</th><th>Sales</th></tr></thead>';
	SET @message = @message + '<tbody>';
	SET @message = @message + '<tr><td>' + @currentMTD + '</td><td style=''text-align: right''>' + CAST(@sponsorRegistrationsCurrentMTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>' + CAST(@sponsorKickoffsCurrentMTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>';
	IF @sponsorKickoffsCurrentMTD > 0 AND @sponsorRegistrationsCurrentMTD > 0
		SET @message = @message + CAST(CAST(@sponsorKickoffsCurrentMTD * 1.0  / @sponsorRegistrationsCurrentMTD AS NUMERIC(36,2)) AS VARCHAR(20));
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>' + CAST(@sponsorEmailsSentCurrentMTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>';
	IF @sponsorKickoffsCurrentMTD > 0 AND @sponsorEmailsSentCurrentMTD > 0
		SET @message = @message + CAST(CAST(@sponsorEmailsSentCurrentMTD * 1.0  / @sponsorKickoffsCurrentMTD AS NUMERIC(36,1)) AS VARCHAR(20));
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '<td style=''text-align: right''>' + CAST(CAST(@sponsorEmailsOpeningRatioCurrentMTD AS NUMERIC(36,1)) AS VARCHAR(10)) + '%</td>';
	SET @message = @message + '<td style=''text-align: right''>$' + CAST(@sponsorSalesCurrentMTD AS VARCHAR(10)) + '</td></tr>';
	SET @message = @message + '<tr><td>' + @previousMTD + '</td><td style=''text-align: right''>' + CAST(@sponsorRegistrationsPreviousMTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>' + CAST(@sponsorKickoffsPreviousMTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>';
	IF @sponsorKickoffsPreviousMTD > 0 AND @sponsorRegistrationsPreviousMTD > 0
		SET @message = @message + CAST(CAST(@sponsorKickoffsPreviousMTD * 1.0  / @sponsorRegistrationsPreviousMTD AS NUMERIC(36,2)) AS VARCHAR(20));
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>' + CAST(@sponsorEmailsSentPreviousMTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>';
	IF @sponsorKickoffsPreviousMTD > 0 AND @sponsorEmailsSentPreviousMTD > 0
		SET @message = @message + CAST(CAST(@sponsorEmailsSentPreviousMTD * 1.0  / @sponsorKickoffsPreviousMTD AS NUMERIC(36,1)) AS VARCHAR(20));
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '<td style=''text-align: right''>' + CAST(CAST(@sponsorEmailsOpeningRatioPreviousMTD AS NUMERIC(36,1)) AS VARCHAR(10)) + '%</td>';
	SET @message = @message + '<td style=''text-align: right''>$' + CAST(@sponsorSalesPreviousMTD AS VARCHAR(10)) + '</td></tr>';
	SET @message = @message + '<tr><td><strong>VAR:</strong></td><td style=''text-align: right''>';
	IF @sponsorRegistrationsPreviousMTD > 0 AND @sponsorRegistrationsCurrentMTD > 0 AND (@sponsorRegistrationsPreviousMTD - @sponsorRegistrationsCurrentMTD) <> 0
		SET @message = @message + CAST(CAST((@sponsorRegistrationsCurrentMTD - @sponsorRegistrationsPreviousMTD) * 100.0 / @sponsorRegistrationsCurrentMTD AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @sponsorKickoffsPreviousMTD > 0 AND @sponsorKickoffsCurrentMTD > 0 AND (@sponsorKickoffsPreviousMTD - @sponsorKickoffsCurrentMTD) <> 0
		SET @message = @message + CAST(CAST((@sponsorKickoffsCurrentMTD - @sponsorKickoffsPreviousMTD) * 100.0 / @sponsorKickoffsCurrentMTD AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';	
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @sponsorRegistrationsCurrentMTD > 0 AND @sponsorRegistrationsPreviousMTD > 0 AND @sponsorKickoffsCurrentMTD > 0 AND @sponsorKickoffsPreviousMTD > 0
		SET @message = @message + CAST(CAST(((@sponsorKickoffsCurrentMTD * 1.0 / @sponsorRegistrationsCurrentMTD) - (@sponsorKickoffsPreviousMTD * 1.0 / @sponsorRegistrationsPreviousMTD)) * 100.0 / (@sponsorKickoffsCurrentMTD * 1.0 / @sponsorRegistrationsCurrentMTD) AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';		
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @sponsorEmailsSentPreviousMTD > 0 AND @sponsorEmailsSentCurrentMTD > 0 AND (@sponsorEmailsSentPreviousMTD - @sponsorEmailsSentCurrentMTD) <> 0
		SET @message = @message + CAST(CAST((@sponsorEmailsSentCurrentMTD - @sponsorEmailsSentPreviousMTD) * 100.0 / @sponsorEmailsSentCurrentMTD AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @sponsorKickoffsCurrentMTD > 0 AND @sponsorEmailsSentCurrentMTD > 0 AND @sponsorKickoffsPreviousMTD > 0 AND @sponsorEmailsSentPreviousMTD > 0
		SET @message = @message + CAST(CAST(((@sponsorEmailsSentCurrentMTD * 1.0 / @sponsorKickoffsCurrentMTD) - (@sponsorEmailsSentPreviousMTD / @sponsorKickoffsPreviousMTD)) * 100.0 / (@sponsorEmailsSentCurrentMTD / @sponsorKickoffsCurrentMTD) AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @sponsorEmailsOpeningRatioPreviousMTD > 0 AND @sponsorEmailsOpeningRatioCurrentMTD > 0 AND (@sponsorEmailsOpeningRatioPreviousMTD - @sponsorEmailsOpeningRatioCurrentMTD) <> 0
		SET @message = @message + CAST(CAST((@sponsorEmailsOpeningRatioCurrentMTD - @sponsorEmailsOpeningRatioPreviousMTD) * 100.0 / @sponsorEmailsOpeningRatioCurrentMTD AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';	
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @sponsorSalesPreviousMTD > 0 AND @sponsorSalesCurrentMTD > 0 AND (@sponsorSalesPreviousMTD - @sponsorSalesCurrentMTD) <> 0
		SET @message = @message + CAST(CAST((@sponsorSalesCurrentMTD - @sponsorSalesPreviousMTD) * 100.0 / @sponsorSalesCurrentMTD AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td></tr>';
	SET @message = @message + '<tr><td colspan=''8''>&nbsp;</td></tr>'
	SET @message = @message + '<tr><td>' + @currentYTD + '</td><td style=''text-align: right''>' + CAST(@sponsorRegistrationsCurrentYTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>' + CAST(@sponsorKickoffsCurrentYTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>';
	IF @sponsorKickoffsCurrentYTD > 0 AND @sponsorRegistrationsCurrentYTD > 0
		SET @message = @message + CAST(CAST(@sponsorKickoffsCurrentYTD * 1.0  / @sponsorRegistrationsCurrentYTD AS NUMERIC(36,2)) AS VARCHAR(20));
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>' + CAST(@sponsorEmailsSentCurrentYTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>';	
	IF @sponsorKickoffsCurrentYTD > 0 AND @sponsorEmailsSentCurrentYTD > 0
		SET @message = @message + CAST(CAST(@sponsorEmailsSentCurrentYTD * 1.0  / @sponsorKickoffsCurrentYTD AS NUMERIC(36,1)) AS VARCHAR(20));
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '<td style=''text-align: right''>' + CAST(CAST(@sponsorEmailsOpeningRatioCurrentYTD AS NUMERIC(36,1)) AS VARCHAR(10)) + '%</td>';
	SET @message = @message + '<td style=''text-align: right''>' + CAST(@sponsorSalesCurrentYTD AS VARCHAR(10)) + '</td></tr>';
	SET @message = @message + '<tr><td>' + @previousYTD + '</td><td style=''text-align: right''>' + CAST(@sponsorRegistrationsPreviousYTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>' + CAST(@sponsorKickoffsPreviousYTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>';
	IF @sponsorKickoffsPreviousYTD > 0 AND @sponsorRegistrationsPreviousYTD > 0
		SET @message = @message + CAST(CAST(@sponsorKickoffsPreviousYTD * 1.0  / @sponsorRegistrationsPreviousYTD AS NUMERIC(36,2)) AS VARCHAR(20));
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>' + CAST(@sponsorEmailsSentPreviousYTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>';
	IF @sponsorKickoffsPreviousYTD > 0 AND @sponsorEmailsSentPreviousYTD > 0
		SET @message = @message + CAST(CAST(@sponsorEmailsSentPreviousYTD * 1.0  / @sponsorKickoffsPreviousYTD AS NUMERIC(36,1)) AS VARCHAR(20));
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '<td style=''text-align: right''>' + CAST(CAST(@sponsorEmailsOpeningRatioPreviousYTD AS NUMERIC(36,1)) AS VARCHAR(10)) + '%</td>';
	SET @message = @message + '<td style=''text-align: right''>' + CAST(@sponsorSalesPreviousYTD AS VARCHAR(10)) + '</td></tr>';
	SET @message = @message + '<tr><td><strong>VAR:</strong></td><td style=''text-align: right''>';
	IF @sponsorRegistrationsPreviousYTD > 0 AND @sponsorRegistrationsCurrentYTD > 0 AND (@sponsorRegistrationsPreviousYTD - @sponsorRegistrationsCurrentYTD) <> 0
		SET @message = @message + CAST(CAST((@sponsorRegistrationsCurrentYTD - @sponsorRegistrationsPreviousYTD) * 100.0 / @sponsorRegistrationsCurrentYTD AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @sponsorKickoffsPreviousYTD > 0 AND @sponsorKickoffsCurrentYTD > 0 AND (@sponsorKickoffsPreviousYTD - @sponsorKickoffsCurrentYTD) <> 0
		SET @message = @message + CAST(CAST((@sponsorKickoffsCurrentYTD - @sponsorKickoffsPreviousYTD) * 100.0 / @sponsorKickoffsCurrentYTD AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @sponsorRegistrationsCurrentYTD > 0 AND @sponsorRegistrationsPreviousYTD > 0 AND @sponsorKickoffsCurrentYTD > 0 AND @sponsorKickoffsPreviousYTD > 0
		SET @message = @message + CAST(CAST(((@sponsorKickoffsCurrentYTD * 1.0 / @sponsorRegistrationsCurrentYTD) - (@sponsorKickoffsPreviousYTD * 1.0 / @sponsorRegistrationsPreviousYTD)) * 100.0 / (@sponsorKickoffsCurrentYTD * 1.0 / @sponsorRegistrationsCurrentYTD) AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';	
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @sponsorEmailsSentPreviousYTD > 0 AND @sponsorEmailsSentCurrentYTD > 0 AND (@sponsorEmailsSentPreviousYTD - @sponsorEmailsSentCurrentYTD) <> 0
		SET @message = @message + CAST(CAST((@sponsorEmailsSentCurrentYTD - @sponsorEmailsSentPreviousYTD) * 100.0 / @sponsorEmailsSentCurrentYTD AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>';
	
	IF @sponsorKickoffsCurrentYTD > 0 AND @sponsorEmailsSentCurrentYTD > 0 AND @sponsorKickoffsPreviousYTD > 0 AND @sponsorEmailsSentPreviousYTD > 0
		SET @message = @message + CAST(CAST(((@sponsorEmailsSentCurrentYTD * 1.0 / @sponsorKickoffsCurrentYTD) - (@sponsorEmailsSentPreviousYTD / @sponsorKickoffsPreviousYTD)) * 100.0 / (@sponsorEmailsSentCurrentYTD / @sponsorKickoffsCurrentYTD) AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>';
	
	IF @sponsorEmailsOpeningRatioPreviousYTD > 0 AND @sponsorEmailsOpeningRatioCurrentYTD > 0 AND (@sponsorEmailsOpeningRatioPreviousYTD - @sponsorEmailsOpeningRatioCurrentYTD) <> 0
		SET @message = @message + CAST(CAST((@sponsorEmailsOpeningRatioCurrentYTD - @sponsorEmailsOpeningRatioPreviousYTD) * 100.0 / @sponsorEmailsOpeningRatioCurrentYTD AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @sponsorSalesPreviousYTD > 0 AND @sponsorSalesCurrentYTD > 0 AND (@sponsorSalesPreviousYTD - @sponsorSalesCurrentYTD) <> 0
		SET @message = @message + CAST(CAST((@sponsorSalesCurrentYTD - @sponsorSalesPreviousYTD) * 100.0 / @sponsorSalesCurrentYTD AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td></tr>';	
	SET @message = @message + '</tbody>';
	SET @message = @message + '</table>';
	SET @message = @message + @breakLine;
	SET @message = @message + '<table style=''font-family: Verdana; font-size: 11px; border: solid 1px black;'' border=''1''><thead style=''background-color: #009900; color: white; border: solid 1px black;''><tr><th>Member</th><th>Registrations</th><th>Kickoffs</th><th>Kickoffs per Registration</th><th>Emails Sent</th><th>Emails per Kickoff</th><th>Emails Opening Ratio</th><th>Sales</th></tr></thead>';
	SET @message = @message + '<tbody>';	
	SET @message = @message + '<tr><td>' + @currentMTD + '</td><td style=''text-align: right''>' + CAST(@memberRegistrationsCurrentMTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>' + CAST(@memberKickoffsCurrentMTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>';
	IF @memberKickoffsCurrentMTD > 0 AND @memberRegistrationsCurrentMTD > 0
		SET @message = @message + CAST(CAST(@memberKickoffsCurrentMTD * 1.0  / @memberRegistrationsCurrentMTD AS NUMERIC(36,2)) AS VARCHAR(20));
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message +'</td><td style=''text-align: right''>' + CAST(@memberEmailsSentCurrentMTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>';
	IF @memberKickoffsCurrentMTD > 0 AND @memberEmailsSentCurrentMTD > 0
		SET @message = @message + CAST(CAST(@memberEmailsSentCurrentMTD * 1.0  / @memberKickoffsCurrentMTD AS NUMERIC(36,1)) AS VARCHAR(20));
	ELSE
		SET @message = @message + 'N/A';	
	SET @message = @message + '<td style=''text-align: right''>' + CAST(CAST(@memberEmailsOpeningRatioCurrentMTD AS NUMERIC(36,1)) AS VARCHAR(10)) + '%</td>';
	SET @message = @message + '<td style=''text-align: right''>$' + CAST(@memberSalesCurrentMTD AS VARCHAR(10)) + '</td></tr>';
	SET @message = @message + '<tr><td>' + @previousMTD + '</td><td style=''text-align: right''>' + CAST(@memberRegistrationsPreviousMTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>' + CAST(@memberKickoffsPreviousMTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>';
	IF @memberKickoffsPreviousMTD > 0 AND @memberRegistrationsPreviousMTD > 0
		SET @message = @message + CAST(CAST(@memberKickoffsPreviousMTD * 1.0  / @memberRegistrationsPreviousMTD AS NUMERIC(36,2)) AS VARCHAR(20));
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>' + CAST(@memberEmailsSentPreviousMTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>';
	
	IF @memberKickoffsPreviousMTD > 0 AND @memberEmailsSentPreviousMTD > 0
		SET @message = @message + CAST(CAST(@memberEmailsSentPreviousMTD * 1.0  / @memberKickoffsPreviousMTD AS NUMERIC(36,1)) AS VARCHAR(20));
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '<td style=''text-align: right''>' + CAST(CAST(@memberEmailsOpeningRatioPreviousMTD AS NUMERIC(36,1)) AS VARCHAR(10)) + '%</td>';
	SET @message = @message + '<td style=''text-align: right''>$' + CAST(@memberSalesPreviousMTD AS VARCHAR(10)) + '</td></tr>';
	SET @message = @message + '<tr><td><strong>VAR:</strong></td><td style=''text-align: right''>';	
	IF @memberRegistrationsPreviousMTD > 0 AND @memberRegistrationsCurrentMTD > 0 AND (@memberRegistrationsPreviousMTD - @memberRegistrationsCurrentMTD) <> 0
		SET @message = @message + CAST(CAST((@memberRegistrationsCurrentMTD - @memberRegistrationsPreviousMTD) * 100.0 / @memberRegistrationsCurrentMTD AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @memberKickoffsPreviousMTD > 0 AND @memberKickoffsCurrentMTD > 0 AND (@memberKickoffsPreviousMTD - @memberKickoffsCurrentMTD) <> 0
		SET @message = @message + CAST(CAST((@memberKickoffsCurrentMTD - @memberKickoffsPreviousMTD) * 100.0 / @memberKickoffsCurrentMTD AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @memberRegistrationsCurrentMTD > 0 AND @memberRegistrationsPreviousMTD > 0 AND @memberKickoffsCurrentMTD > 0 AND @memberKickoffsPreviousMTD > 0
		SET @message = @message + CAST(CAST(((@memberKickoffsCurrentMTD * 1.0 / @memberRegistrationsCurrentMTD) - (@memberKickoffsPreviousMTD * 1.0 / @memberRegistrationsPreviousMTD)) * 100.0 / (@memberKickoffsCurrentMTD * 1.0 / @memberRegistrationsCurrentMTD) AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @memberEmailsSentPreviousMTD > 0 AND @memberEmailsSentCurrentMTD > 0 AND (@memberEmailsSentPreviousMTD - @memberEmailsSentCurrentMTD) <> 0
		SET @message = @message + CAST(CAST((@memberEmailsSentCurrentMTD - @memberEmailsSentPreviousMTD) * 100.0 / @memberEmailsSentCurrentMTD AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @memberKickoffsCurrentMTD > 0 AND @memberEmailsSentCurrentMTD > 0 AND @memberKickoffsPreviousMTD > 0 AND @memberEmailsSentPreviousMTD > 0
		SET @message = @message + CAST(CAST(((@memberEmailsSentCurrentMTD * 1.0 / @memberKickoffsCurrentMTD) - (@memberEmailsSentPreviousMTD / @memberKickoffsPreviousMTD)) * 100.0 / (@memberEmailsSentCurrentMTD / @memberKickoffsCurrentMTD) AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @memberEmailsOpeningRatioPreviousMTD > 0 AND @memberEmailsOpeningRatioCurrentMTD > 0 AND (@memberEmailsOpeningRatioPreviousMTD - @memberEmailsOpeningRatioCurrentMTD) <> 0
		SET @message = @message + CAST(CAST((@memberEmailsOpeningRatioCurrentMTD - @memberEmailsOpeningRatioPreviousMTD) * 100.0 / @memberEmailsOpeningRatioCurrentMTD AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @memberSalesPreviousMTD > 0 AND @memberSalesCurrentMTD > 0 AND (@memberSalesPreviousMTD - @memberSalesCurrentMTD) <> 0
		SET @message = @message + CAST(CAST((@memberSalesCurrentMTD - @memberSalesPreviousMTD) * 100.0 / @memberSalesCurrentMTD AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td></tr>';
	SET @message = @message + '<tr><td colspan=''8''>&nbsp;</td></tr>'
	SET @message = @message + '<tr><td>' + @currentYTD + '</td><td style=''text-align: right''>' + CAST(@memberRegistrationsCurrentYTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>' + CAST(@memberKickoffsCurrentYTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>';
	IF @memberKickoffsCurrentYTD > 0 AND @memberRegistrationsCurrentYTD > 0
		SET @message = @message + CAST(CAST(@memberKickoffsCurrentYTD * 1.0  / @memberRegistrationsCurrentYTD AS NUMERIC(36,2)) AS VARCHAR(20));
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>' + CAST(@memberEmailsSentCurrentYTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>';	
	IF @memberKickoffsCurrentYTD > 0 AND @memberEmailsSentCurrentYTD > 0
		SET @message = @message + CAST(CAST(@memberEmailsSentCurrentYTD * 1.0  / @memberKickoffsCurrentYTD AS NUMERIC(36,1)) AS VARCHAR(20));
	ELSE
		SET @message = @message + 'N/A';	
	SET @message = @message + '<td style=''text-align: right''>' + CAST(CAST(@memberEmailsOpeningRatioCurrentYTD AS NUMERIC(36,1)) AS VARCHAR(10)) + '%</td>';
	SET @message = @message + '<td style=''text-align: right''>$' + CAST(@memberSalesCurrentYTD AS VARCHAR(10)) + '</td></tr>';
	SET @message = @message + '<tr><td>' + @previousYTD + '</td><td style=''text-align: right''>' + CAST(@memberRegistrationsPreviousYTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>' + CAST(@memberKickoffsPreviousYTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>';
	IF @memberKickoffsPreviousYTD > 0 AND @memberRegistrationsPreviousYTD > 0
		SET @message = @message + CAST(CAST(@memberKickoffsPreviousYTD * 1.0  / @memberRegistrationsPreviousYTD AS NUMERIC(36,2)) AS VARCHAR(20));
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>' + CAST(@memberEmailsSentPreviousYTD AS VARCHAR(10)) + '</td><td style=''text-align: right''>';
	
	IF @memberKickoffsPreviousYTD > 0 AND @memberEmailsSentPreviousYTD > 0
		SET @message = @message + CAST(CAST(@memberEmailsSentPreviousYTD * 1.0  / @memberKickoffsPreviousYTD AS NUMERIC(36,1)) AS VARCHAR(20));
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '<td style=''text-align: right''>' + CAST(CAST(@memberEmailsOpeningRatioPreviousYTD AS NUMERIC(36,1)) AS VARCHAR(10)) + '%</td>';
	SET @message = @message + '<td style=''text-align: right''>$' + CAST(@memberSalesPreviousYTD AS VARCHAR(10)) + '</td></tr>';
	SET @message = @message + '<tr><td><strong>VAR:</strong></td><td style=''text-align: right''>';
	IF @memberRegistrationsPreviousYTD > 0 AND @memberRegistrationsCurrentYTD > 0 AND (@memberRegistrationsPreviousYTD - @memberRegistrationsCurrentYTD) <> 0
		SET @message = @message + CAST(CAST((@memberRegistrationsCurrentYTD - @memberRegistrationsPreviousYTD) * 100.0 / @memberRegistrationsCurrentYTD AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @memberKickoffsPreviousYTD > 0 AND @memberKickoffsCurrentYTD > 0 AND (@memberKickoffsPreviousYTD - @memberKickoffsCurrentYTD) <> 0
		SET @message = @message + CAST(CAST((@memberKickoffsCurrentYTD - @memberKickoffsPreviousYTD) * 100.0 / @memberKickoffsCurrentYTD AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @memberRegistrationsCurrentYTD > 0 AND @memberRegistrationsPreviousYTD > 0 AND @memberKickoffsCurrentYTD > 0 AND @memberKickoffsPreviousYTD > 0
		SET @message = @message + CAST(CAST(((@memberKickoffsCurrentYTD * 1.0 / @memberRegistrationsCurrentYTD) - (@memberKickoffsPreviousYTD * 1.0 / @memberRegistrationsPreviousYTD)) * 100.0 / (@memberKickoffsCurrentYTD * 1.0 / @memberRegistrationsCurrentYTD) AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @memberEmailsSentPreviousYTD > 0 AND @memberEmailsSentCurrentYTD > 0 AND (@memberEmailsSentPreviousYTD - @memberEmailsSentCurrentYTD) <> 0
		SET @message = @message + CAST(CAST((@memberEmailsSentCurrentYTD - @memberEmailsSentPreviousYTD) * 100.0 / @memberEmailsSentCurrentYTD AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @memberKickoffsCurrentYTD > 0 AND @memberEmailsSentCurrentYTD > 0 AND @memberKickoffsPreviousYTD > 0 AND @memberEmailsSentPreviousYTD > 0
		SET @message = @message + CAST(CAST(((@memberEmailsSentCurrentYTD * 1.0 / @memberKickoffsCurrentYTD) - (@memberEmailsSentPreviousYTD / @memberKickoffsPreviousYTD)) * 100.0 / (@memberEmailsSentCurrentYTD / @memberKickoffsCurrentYTD) AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @memberEmailsOpeningRatioPreviousYTD > 0 AND @memberEmailsOpeningRatioCurrentYTD > 0 AND (@memberEmailsOpeningRatioPreviousYTD - @memberEmailsOpeningRatioCurrentYTD) <> 0
		SET @message = @message + CAST(CAST((@memberEmailsOpeningRatioCurrentYTD - @memberEmailsOpeningRatioPreviousYTD) * 100.0 / @memberEmailsOpeningRatioCurrentYTD AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td><td style=''text-align: right''>';
	IF @memberSalesPreviousYTD > 0 AND @memberSalesCurrentYTD > 0 AND (@memberSalesPreviousYTD - @memberSalesCurrentYTD) <> 0
		SET @message = @message + CAST(CAST((@memberSalesCurrentYTD - @memberSalesPreviousYTD) * 100.0 / @memberSalesCurrentYTD AS NUMERIC(36,2)) AS VARCHAR(20)) + '%';
	ELSE
		SET @message = @message + 'N/A';
	SET @message = @message + '</td></tr>';	
	SET @message = @message + '</tbody>';
	SET @message = @message + '</table>';
	SET @message = @message + '</body>';
	EXEC msdb.dbo.sp_send_dbmail @profile_name = NULL,
                                   @recipients = 'BDCDevelopers@fundraising.com;xavier.desaunettes@fundraising.com;marc.alcindor@fundraising.com;sadday.Zivec@fundraising.com',
                                   --@recipients = 'javier.arellano@fundraising.com',
                                   @subject = @subject,
                                   @body = @message,
                                   @body_format = 'HTML',
                                   @from_address = 'efrreporting@fundraising.com',
                                   @reply_to = 'BDCDevelopers@fundraising.com'
 --PRINT @message
END
