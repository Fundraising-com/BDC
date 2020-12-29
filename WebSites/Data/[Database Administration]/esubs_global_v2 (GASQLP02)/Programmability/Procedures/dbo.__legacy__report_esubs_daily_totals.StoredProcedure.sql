USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[__legacy__report_esubs_daily_totals]    Script Date: 02/14/2014 13:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
    Created by: Philippe Girard
    Created on: 24 august 2005
    
    Description: 

    This report will only work when the suppID 
    field is filled with event_participation_id    
*/

CREATE PROCEDURE [dbo].[__legacy__report_esubs_daily_totals]
AS
BEGIN
    DECLARE @intTotalQuantity SMALLINT
    DECLARE @intOrderTotal DECIMAL(9,2)    
    DECLARE @dteStartingDate DATETIME
    DECLARE @intCampaignCount INT
    DECLARE @intParticipantCount INT
    DECLARE @intSupporterCount INT
    DECLARE @dteToday DATETIME
    DECLARE @now DATETIME


    SET NOCOUNT ON
    SET DATEFIRST 1
    SET @dteToday = GETDATE()
--    SET @dteToday = '2005-09-16'
    SET @dteToday = DATEADD(ms, -DATEPART(ms, @dteToday), @dteToday)
    SET @dteToday = DATEADD(s, -DATEPART(s, @dteToday), @dteToday)
    SET @dteToday = DATEADD(n, -DATEPART(n, @dteToday), @dteToday)
    SET @dteToday = DATEADD(hh, -DATEPART(hh, @dteToday), @dteToday)
    SET @now = GETDATE()

    PRINT 'eSubs Daily Report' 
    PRINT ''
    PRINT '* Temporary numbers. Complete report pending Q&A.'
    PRINT ''
    PRINT ''
    
    -- Monthly sales
    SELECT 	
        @intTotalQuantity = SUM( tps.TotalQuantity )
    	, @intOrderTotal = SUM( tps.OrderTotal ) 
	    , @dteStartingDate = MIN( tps.OrderDate )
    FROM dbo.event AS e
        INNER JOIN dbo.event_participation AS ep
            ON ep.event_id = e.event_id
        INNER JOIN QSPStore..es_totals_per_sale AS tps
            ON tps.suppID = ep.event_participation_id
    WHERE e.event_type_id = 1
      AND MONTH( tps.OrderDate ) = MONTH( @dteToday ) 
      AND YEAR( tps.OrderDate ) = YEAR( @dteToday ) 
    
    
    IF @intTotalQuantity IS NULL AND @intOrderTotal IS NULL
    BEGIN
	    PRINT 'No sales were recorded for the month.'
    	PRINT ''
    END
    ELSE
    BEGIN
	    PRINT 'Monthly sales for ' + DATENAME( MONTH, @dteStartingDate ) + ' ' + CONVERT( CHAR(4), YEAR( @dteStartingDate ) )
    	PRINT 'My Group Page: ' + CONVERT( VARCHAR(20), @intTotalQuantity ) + ' subs, $' + CONVERT( VARCHAR(20), @intOrderTotal ) 
	    PRINT ''
    END
    
    
    -- Weekly sales
    SELECT 
    	@intTotalQuantity = SUM( tps.TotalQuantity ) 
    	, @intOrderTotal = SUM( tps.OrderTotal ) 
    	, @dteStartingDate = MIN( tps.OrderDate ) 
    FROM dbo.event AS e 
        INNER JOIN dbo.event_participation AS ep 
            ON ep.event_id = e.event_id 
        INNER JOIN QSPStore..es_totals_per_sale AS tps 
            ON tps.suppID = ep.event_participation_id 
    WHERE e.event_type_id = 1 
      AND tps.OrderDate BETWEEN DATEADD(day, -DATEPART(dw, @dteToday) + 1, @dteToday) AND @now
      AND YEAR( tps.OrderDate ) = YEAR( @dteToday ) 

        
    IF @intTotalQuantity IS NULL AND @intOrderTotal IS NULL
    BEGIN
    	PRINT 'No sales were recorded for the week.'
    	PRINT ''
    END
    ELSE
    BEGIN
    	PRINT 'Weekly sales as of ' + DATENAME( dw, @dteStartingDate ) + ', ' + DATENAME( MONTH, @dteStartingDate ) + ' ' + CONVERT( VARCHAR(2), DATENAME( DAY, @dteStartingDate ) ) + ', ' + CONVERT( CHAR(4), YEAR( @dteStartingDate ) )
    	PRINT 'My Group Page: ' + CONVERT( VARCHAR(20), @intTotalQuantity ) + ' subs, $' + CONVERT( VARCHAR(20), @intOrderTotal ) 
    	PRINT ''
    END

        
    -- Yesterday's sales
    SELECT 
    	@intTotalQuantity = SUM( tps.TotalQuantity ) 
    	, @intOrderTotal = SUM( tps.OrderTotal ) 
    	, @dteStartingDate = MIN( tps.OrderDate ) 
    FROM dbo.event AS e 
        INNER JOIN dbo.event_participation AS ep 
            ON ep.event_id = e.event_id 
        INNER JOIN QSPStore..es_totals_per_sale AS tps 
            ON tps.suppID = ep.event_participation_id 
    WHERE e.event_type_id = 1 
      AND DATEPART(day, tps.OrderDate) = DATEPART(day, DATEADD(day, -1, @dteToday ))
      AND DATEPART(month, tps.OrderDate) = DATEPART(month, DATEADD(day, -1, @dteToday))
      AND DATEPART(year, tps.OrderDate) = DATEPART(year, DATEADD(day, -1, @dteToday))
    
    IF @intTotalQuantity IS NULL AND @intOrderTotal IS NULL
    BEGIN
    	PRINT 'No sales were recorded yesterday.'
    	PRINT ''
    END
    ELSE
    BEGIN
    	PRINT 'Yesterday''s sales ' + DATENAME( dw, @dteStartingDate ) + ', ' + DATENAME( MONTH, @dteStartingDate ) + ' ' + CONVERT( VARCHAR(2), DATENAME( DAY, @dteStartingDate ) ) + ', ' + CONVERT( CHAR(4), YEAR( @dteStartingDate ) )
    	PRINT 'My Group Page: ' + CONVERT( VARCHAR(20), @intTotalQuantity ) + ' subs, $' + CONVERT( VARCHAR(20), @intOrderTotal )
    	PRINT ''
    END
    
    
    -- Today's sales
    SELECT 
    	@intTotalQuantity = SUM( tps.TotalQuantity ) 
    	, @intOrderTotal = SUM( tps.OrderTotal ) 
    	, @dteStartingDate = MIN( tps.OrderDate ) 
    FROM dbo.event AS e 
        INNER JOIN dbo.event_participation AS ep 
            ON ep.event_id = e.event_id 
        INNER JOIN QSPStore..es_totals_per_sale AS tps 
            ON tps.suppID = ep.event_participation_id 
    WHERE e.event_type_id = 1 
      AND DATEPART(day, tps.OrderDate) = DATEPART(day, @dteToday)
      AND DATEPART(month, tps.OrderDate) = DATEPART(month, @dteToday)
      AND DATEPART(year, tps.OrderDate) = DATEPART(year, @dteToday)


    IF @intTotalQuantity IS NULL AND @intOrderTotal IS NULL
    BEGIN
    	PRINT 'No sales were recorded today.'
    	PRINT ''
    END
    ELSE
    BEGIN
    	PRINT 'Today''s sales'
    	PRINT 'My Group Page: ' + CONVERT( VARCHAR(20), @intTotalQuantity ) + ' subs, $' + CONVERT( VARCHAR(20), @intOrderTotal )
    	PRINT ''
    END    
        
    PRINT ''
    PRINT 'Campaign Activations'
    PRINT ''

    
    -- This month's campaign activations
    SET @intCampaignCount = NULL

    SELECT
	    @intCampaignCount = COUNT( v.event_id ) 
    FROM (
        SELECT 
		    e.event_id
		    , MIN( tps.OrderDate ) AS order_date
	    FROM dbo.event AS e 
            INNER JOIN dbo.event_participation AS ep 
                ON ep.event_id = e.event_id 
            INNER JOIN QSPStore..es_totals_per_sale AS tps 
                ON tps.suppID = ep.event_participation_id 
	    WHERE e.event_type_id = 1
	    GROUP BY e.event_id
        ) v
    WHERE MONTH( v.order_date ) = MONTH( @dteToday ) 
      AND YEAR( v.order_date ) = YEAR( @dteToday ) 

    IF @intCampaignCount IS NULL 
    BEGIN
    	PRINT 'No campaigns were activated this month.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    		PRINT '1 campaign was activated this month.'
    	ELSE
    		PRINT CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were activated this month.'
    END

    
    -- This week's campaign activations
    SET @intCampaignCount = NULL

    SELECT 
        @intCampaignCount = COUNT( v.event_id ) 
    FROM (
    	SELECT 
    		e.event_id 
    		, MIN( tps.OrderDate ) AS order_date 
    	FROM dbo.event AS e 
            INNER JOIN dbo.event_participation AS ep 
                ON ep.event_id = e.event_id 
            INNER JOIN QSPStore..es_totals_per_sale AS tps 
                ON tps.suppID = ep.event_participation_id 
    	WHERE e.event_type_id = 1
    	GROUP BY e.event_id
    ) v
    WHERE v.order_date BETWEEN DATEADD( DAY, -DATEPART( dw, @dteToday ) + 1, @dteToday) AND @now
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	PRINT 'No campaigns were activated this week.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    		PRINT '1 campaign was activated this week.'
    	ELSE
    		PRINT CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were activated this week.'
    END
    
    -- Yesterday's campaign activations
    SET @intCampaignCount = NULL
    SELECT
    	@intCampaignCount = COUNT( v.event_id ) 
    FROM (
    	SELECT 
    		e.event_id
    		, MIN( tps.OrderDate ) AS order_date
    	FROM  
    		dbo.event AS e 
            INNER JOIN dbo.event_participation AS ep 
                ON ep.event_id = e.event_id 
            INNER JOIN QSPStore..es_totals_per_sale AS tps 
                ON tps.suppID = ep.event_participation_id 
    	WHERE 
    		e.event_type_id = 1
    	GROUP BY 
    		e.event_id
    ) v
    WHERE 
      DATEPART(day, v.order_date) = DATEPART(day, DATEADD( DAY, -1, @dteToday ))
      AND DATEPART(month, v.order_date) = DATEPART(month, DATEADD( day, -1, @dteToday))
      AND DATEPART(year, v.order_date) = DATEPART(year, DATEADD(day, -1, @dteToday))
      
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	PRINT 'No campaigns were activated yesterday.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    		PRINT '1 campaign was activated yesterday.'
    	ELSE
    		PRINT CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were activated yesterday.'
    END
    
        
    -- Today's campaign activations
    SET @intCampaignCount = NULL

    SELECT
    	@intCampaignCount = COUNT( v.event_id ) 
    FROM (
    	SELECT 
    		e.event_id
    		, MIN( tps.OrderDate ) AS order_date
    	FROM  
    		dbo.event AS e 
            INNER JOIN dbo.event_participation AS ep 
                ON ep.event_id = e.event_id 
            INNER JOIN QSPStore..es_totals_per_sale AS tps 
                ON tps.suppID = ep.event_participation_id 
    	GROUP BY 
    		e.event_id
    ) v
    WHERE 
      DATEPART(day, v.order_date) = DATEPART(day, @dteToday)
      AND DATEPART(month, v.order_date) = DATEPART(month, @dteToday)
      AND DATEPART(year, v.order_date) = DATEPART(year, @dteToday)
       
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	PRINT 'No campaigns were activated today.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    		PRINT '1 campaign was activated today.'
    	ELSE
    		PRINT CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were activated today.'
    END
    
    PRINT ''
    PRINT ''
    PRINT 'Campaign Kickoffs'
    PRINT ''
    

    -- This month's campaign kick-offs
    SET @intCampaignCount = NULL
    SELECT
    	@intCampaignCount = COUNT( v.event_id ) 
    FROM (        
        SELECT 
            e.event_id
            , COUNT(ep.event_participation_id) as participation_count
            , MIN (ep.create_date) as create_date
        FROM event as e
            INNER JOIN event_participation as ep
                ON ep.event_id = e.event_id
            INNER JOIN member_hierarchy as mh
                ON mh.member_hierarchy_id = ep.member_hierarchy_id
            INNER JOIN member as m
                ON m.member_id = mh.member_id
         WHERE ( m.email_address is not null
                 and m.email_address not like '%@efundraising.com')
           AND mh.creation_channel_id IN (7,20,23)
         GROUP BY e.event_id
    ) v
    WHERE
    	( MONTH( v.create_date ) = MONTH( @dteToday ) )
     AND	( YEAR( v.create_date ) = YEAR( @dteToday ) )
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	PRINT 'No campaigns were kicked-off this month.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    		PRINT CONVERT( CHAR(1), @intCampaignCount ) + '1 campaign was kicked-off this month.'
    	ELSE
    		PRINT CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were kicked-off this month.'
    END

        
    -- This week's campaign kick-offs
    SET @intCampaignCount = NULL
    SELECT
    	@intCampaignCount = COUNT( v.event_id ) 
    FROM (
    	SELECT 
            e.event_id
            , COUNT(ep.event_participation_id) as participation_count
            , MIN (ep.create_date) as create_date
        FROM event as e
            INNER JOIN event_participation as ep
                ON ep.event_id = e.event_id
            INNER JOIN member_hierarchy as mh
                ON mh.member_hierarchy_id = ep.member_hierarchy_id
            INNER JOIN member as m
                ON m.member_id = mh.member_id
         WHERE ( m.email_address is not null
                 and m.email_address not like '%@efundraising.com')
           AND mh.creation_channel_id IN (7,20,23)
         GROUP BY e.event_id
    ) v
    WHERE
        v.create_date BETWEEN DATEADD(day, -DATEPART(dw, @dteToday) + 1, @dteToday) AND @now
        AND YEAR( v.create_date ) = YEAR( @dteToday ) 
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	PRINT 'No campaigns were kicked-off this week.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    		PRINT '1 campaign was kicked-off this week.'
    	ELSE
    		PRINT CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were kicked-off this week.'
    END

        
    -- Yesterday's campaign kick-offs
    SET @intCampaignCount = NULL
    SELECT
    	@intCampaignCount = COUNT( v.event_id ) 
    FROM (
    	SELECT 
            e.event_id
            , COUNT(ep.event_participation_id) as participation_count
            , MIN (ep.create_date) as create_date
        FROM event as e
            INNER JOIN event_participation as ep
                ON ep.event_id = e.event_id
            INNER JOIN member_hierarchy as mh
                ON mh.member_hierarchy_id = ep.member_hierarchy_id
            INNER JOIN member as m
                ON m.member_id = mh.member_id
         WHERE ( m.email_address is not null
                 and m.email_address not like '%@efundraising.com')
           AND mh.creation_channel_id IN (7,20,23)
         GROUP BY e.event_id
    ) v
    WHERE
          DATEPART(year, v.create_date) = DATEPART(year, DATEADD(day, -1, @dteToday))
      AND DATEPART(month, v.create_date) = DATEPART(month, DATEADD(day, -1, @dteToday))
      AND DATEPART(day, v.create_date) = DATEPART(day, DATEADD(day, -1, @dteToday))
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	PRINT 'No campaigns were kicked-off yesterday.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    		PRINT '1 campaign was kicked-off yesterday.'
    	ELSE
    		PRINT CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were kicked-off yesterday.'
    END
    
    -- Today's campaign kick-offs
    SET @intCampaignCount = NULL
    SELECT
    	@intCampaignCount = COUNT( v.event_id ) 
    FROM (
    	SELECT 
            e.event_id
            , COUNT(ep.event_participation_id) as participation_count
            , MIN (ep.create_date) as create_date
        FROM event as e
            INNER JOIN event_participation as ep
                ON ep.event_id = e.event_id
            INNER JOIN member_hierarchy as mh
                ON mh.member_hierarchy_id = ep.member_hierarchy_id
            INNER JOIN member as m
                ON m.member_id = mh.member_id
         WHERE ( m.email_address is not null
                 and m.email_address not like '%@efundraising.com')
           AND mh.creation_channel_id IN (7,20,23)
         GROUP BY e.event_id
    ) v
    WHERE
        DATEPART(year, v.create_date) = DATEPART(year, @dteToday)
    AND DATEPART(month, v.create_date) = DATEPART(month, @dteToday)
    AND DATEPART(day, v.create_date) = DATEPART(day, @dteToday)
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	PRINT 'No campaigns were kicked-off today.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    		PRINT '1 campaign was kicked-off today.'
    	ELSE
    		PRINT CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were kicked-off today.'
    END
    
/*    
    PRINT ''
    PRINT ''
    PRINT 'New Participants'
    PRINT ''
    
    -- This month's new participants
    SET @intParticipantCount = NULL
    SELECT
    	@intParticipantCount = COUNT( v.event_id ) 
    FROM (
        SELECT
            e.event_id
            , COUNT( ep.event_participation_id ) as participant_count
        FROM event e
            INNER JOIN event_participation ep
                ON ep.event_id = e.event_id
            INNER JOIN member_hierarchy mh
                ON mh.member_hierarchy_id = ep.member_hierarchy_id
            INNER JOIN member m
                ON m.member_id = mh.member_id
        WHERE MONTH(ep.create_date) = MONTH(@dteToday)
          AND YEAR(ep.create_date) = YEAR(@dteToday)
          AND m.email_address IS NOT NULL 
          AND m.email_address NOT LIKE '%@efundraising.com'
    	GROUP BY e.event_id
    ) v
    
    IF @intParticipantCount IS NULL 
    BEGIN
    	PRINT 'No new participants were added this month.'
    END
    ELSE
    BEGIN
    	IF @intParticipantCount = 1
    		PRINT '1 new participant was added this month.'
    	ELSE
    		PRINT CONVERT( VARCHAR(5), @intParticipantCount ) + ' new participants were added this month.'
    END
    
    
    -- This week's new participants
    SET @intParticipantCount = NULL
    SELECT
    	@intParticipantCount = COUNT( v.event_id ) 
    FROM (
    	SELECT
            e.event_id
            , COUNT( ep.event_participation_id ) as supporter_count
        FROM event e
            INNER JOIN event_participation ep
                ON ep.event_id = e.event_id
            INNER JOIN member_hierarchy mh
                ON mh.member_hierarchy_id = ep.member_hierarchy_id
            INNER JOIN member m
                ON m.member_id = mh.member_id
        WHERE ep.create_date BETWEEN DATEADD(DAY, -DATEPART(dw, @dteToday), @dteToday) AND @now
          AND m.email_address IS NOT NULL 
          AND m.email_address NOT LIKE '%@efundraising.com'
    	GROUP BY e.event_id
    ) v
    
    IF @intParticipantCount IS NULL 
    BEGIN
    	PRINT 'No new participants were added this week.'
    END
    ELSE
    BEGIN
    	IF @intParticipantCount = 1
    		PRINT '1 new participant was added this week.'
    	ELSE
    		PRINT CONVERT( VARCHAR(5), @intParticipantCount ) + ' new participants were added this week.'
    END
    
    
    -- This yesterday's new participants
    SET @intParticipantCount = NULL
    SELECT @intParticipantCount = COUNT( e.event_id ) 
    FROM event e
        INNER JOIN event_participation ep
            ON ep.event_id = e.event_id
        INNER JOIN member_hierarchy mh
            ON mh.member_hierarchy_id = ep.member_hierarchy_id
        INNER JOIN member m
            ON m.member_id = mh.member_id
    WHERE DATEPART(DAY, ep.create_date) = DATEPART(DAY, DATEADD(DAY, -1, @dteToday))
      AND DATEPART(MONTH, ep.create_date) = DATEPART(MONTH, DATEADD(DAY, -1, @dteToday))
      AND DATEPART(YEAR, ep.create_date) = DATEPART(YEAR, DATEADD(DAY, -1, @dteToday))
      AND m.email_address IS NOT NULL 
      AND m.email_address NOT LIKE '%@efundraising.com'
	GROUP BY e.event_id
    
    IF @intParticipantCount IS NULL 
    BEGIN
    	PRINT 'No new participants were added yesterday.'
    END
    ELSE
    BEGIN
    	IF @intParticipantCount = 1
    		PRINT '1 new participant was added yesterday.'
    	ELSE
    		PRINT CONVERT( VARCHAR(5), @intParticipantCount ) + ' new participants were added yesterday.'
    END
    

    -- This today's new participants
    SET @intParticipantCount = NULL
    SELECT
    	@intParticipantCount = COUNT( e.event_id ) 
    FROM event e
        INNER JOIN event_participation ep
            ON ep.event_id = e.event_id
        INNER JOIN member_hierarchy mh
            ON mh.member_hierarchy_id = ep.member_hierarchy_id
        INNER JOIN member m
            ON m.member_id = mh.member_id
    WHERE DATEPART(DAY, ep.create_date) = DATEPART(DAY, @dteToday)
      AND DATEPART(MONTH, ep.create_date) = DATEPART(MONTH, @dteToday)
      AND DATEPART(YEAR, ep.create_date) = DATEPART(YEAR, @dteToday)
      AND m.email_address IS NOT NULL 
      AND m.email_address NOT LIKE '%@efundraising.com'
	GROUP BY e.event_id
    	
    
    IF @intParticipantCount IS NULL 
    BEGIN
    	PRINT 'No new participants were added today.'
    END
    ELSE
    BEGIN
    	IF @intParticipantCount = 1
    		PRINT '1 new participant was added today.'
    	ELSE
    		PRINT CONVERT( VARCHAR(5), @intParticipantCount ) + ' new participants were added today.'
    END
    
        
    PRINT ''
    PRINT ''
    PRINT 'New Active Participants'
    PRINT ''
    
    -- This month's new active participants
    SET @intParticipantCount = NULL
    SELECT
    	@intParticipantCount = COUNT( v.member_hierarchy_id ) 
    FROM (
        SELECT mh.member_hierarchy_id
        FROM event e
            INNER JOIN event_participation ep
                ON ep.event_id = e.event_id
            INNER JOIN member_hierarchy mh
                ON mh.member_hierarchy_id = ep.member_hierarchy_id
            INNER JOIN member m
                ON m.member_id = mh.member_id
        WHERE MONTH(ep.create_date) = MONTH( @dteToday )
          AND YEAR(ep.create_date) = YEAR( @dteToday )
          AND m.email_address IS NOT NULL 
          AND m.email_address NOT LIKE '%@efundraising.com'
    ) v
    
    IF @intParticipantCount IS NULL 
    BEGIN
    	PRINT 'No new active participants were added this month.'
    END
    ELSE
    BEGIN
    	IF @intParticipantCount = 1
    		PRINT '1 new active participant was added this month.'
    	ELSE
    		PRINT CONVERT( VARCHAR(5), @intParticipantCount ) + ' new participants active were added this month.'
    END
    
    -- This week's new active participants
    SET @intParticipantCount = NULL
    SELECT
    	@intParticipantCount = COUNT( v.member_hierarchy_id ) 
    FROM (
        SELECT mh.member_hierarchy_id
        FROM event e
              INNER JOIN event_participation ep
                  ON ep.event_id = e.event_id
              INNER JOIN member_hierarchy mh
                  ON mh.member_hierarchy_id = ep.member_hierarchy_id
              INNER JOIN member m
                  ON m.member_id = mh.member_id
        WHERE ep.create_date BETWEEN DATEADD(DAY, -DATEPART(dw, @dteToday), @dteToday) AND @now
          AND m.email_address IS NOT NULL 
          AND m.email_address NOT LIKE '%@efundraising.com'
    ) v
    
    IF @intParticipantCount IS NULL 
    BEGIN
    	PRINT 'No new active participants were added this week.'
    END
    ELSE
    BEGIN
    	IF @intParticipantCount = 1
    		PRINT '1 new active participant was added this week.'
    	ELSE
    		PRINT CONVERT( VARCHAR(5), @intParticipantCount ) + ' new active participants were added this week.'
    END
    
    -- This yesterday's new participants
    SET @intParticipantCount = NULL
    SELECT
    	@intParticipantCount = COUNT( v.member_hierarchy_id ) 
    FROM (
        SELECT mh.member_hierarchy_id
        FROM event e
            INNER JOIN event_participation ep
                ON ep.event_id = e.event_id
            INNER JOIN member_hierarchy mh
                ON mh.member_hierarchy_id = ep.member_hierarchy_id
            INNER JOIN member m
                ON m.member_id = mh.member_id
        WHERE DATEPART(DAY, ep.create_date) = DATEPART(DAY, DATEADD(DAY, -1, @dteToday))
          AND DATEPART(MONTH, ep.create_date) = DATEPART(MONTH, DATEADD(DAY, -1, @dteToday))
          AND DATEPART(YEAR, ep.create_date) = DATEPART(YEAR, DATEADD(DAY, -1, @dteToday))
          AND m.email_address IS NOT NULL 
          AND m.email_address NOT LIKE '%@efundraising.com'
    ) v
    
    IF @intParticipantCount IS NULL 
    BEGIN
    	PRINT 'No new active participants were added yesterday.'
    END
    ELSE
    BEGIN
    	IF @intParticipantCount = 1
    		PRINT '1 new active participant was added yesterday.'
    	ELSE
    		PRINT CONVERT( VARCHAR(5), @intParticipantCount ) + ' new active participants were added yesterday.'
    END
    
    -- This today's new participants
    SET @intParticipantCount = NULL
    SELECT
    	@intParticipantCount = COUNT( v.member_hierarchy_id ) 
    FROM (
    	SELECT mh.member_hierarchy_id
        FROM event e
            INNER JOIN event_participation ep
                ON ep.event_id = e.event_id
            INNER JOIN member_hierarchy mh
                ON mh.member_hierarchy_id = ep.member_hierarchy_id
            INNER JOIN member m
                ON m.member_id = mh.member_id
        WHERE DATEPART(DAY, ep.create_date) = DATEPART(DAY, @dteToday)
          AND DATEPART(MONTH, ep.create_date) = DATEPART(MONTH, @dteToday)
          AND DATEPART(YEAR, ep.create_date) = DATEPART(YEAR, @dteToday)
          AND m.email_address IS NOT NULL 
          AND m.email_address NOT LIKE '%@efundraising.com'
    ) v
    
    IF @intParticipantCount IS NULL 
    BEGIN
    	PRINT 'No new active participants were added today.'
    END
    ELSE
    BEGIN
    	IF @intParticipantCount = 1
    		PRINT '1 new active participant was added today.'
    	ELSE
    		PRINT CONVERT( VARCHAR(5), @intParticipantCount ) + ' new active participants were added today.'
    END
    
    PRINT ''
    PRINT ''
    PRINT 'New Supporters'
    PRINT ''
    
    -- This month's new supporters
    SELECT @intSupporterCount = COUNT( mh.member_hierarchy_id )
    FROM event e
        INNER JOIN event_participation ep
            ON ep.event_id = e.event_id
        INNER JOIN member_hierarchy mh
            ON mh.member_hierarchy_id = ep.member_hierarchy_id
        INNER JOIN member m
            ON m.member_id = mh.member_id
    WHERE MONTH(ep.create_date) = MONTH(@dteToday)
          AND YEAR(ep.create_date) = YEAR(@dteToday)
          AND m.email_address IS NOT NULL 
          AND m.email_address NOT LIKE '%@efundraising.com'
    GROUP BY e.event_id
    
    IF @intSupporterCount IS NULL 
    BEGIN
    	PRINT 'No new supporters were added this month.'
    END
    ELSE
    BEGIN
    	IF @intSupporterCount = 1
    		PRINT '1 new supporter was added this month.'
    	ELSE
    		PRINT CONVERT( VARCHAR(5), @intSupporterCount ) + ' new supporters were added this month.'
    END
*/
    
    
END
GO
