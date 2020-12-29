USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[report_esubs_daily_totals2]    Script Date: 02/14/2014 13:08:06 ******/
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

CREATE PROCEDURE [dbo].[report_esubs_daily_totals2]
	@report varchar(8000) OUTPUT
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
	--DECLARE @report VARCHAR(8000)
	SET @report = ''

    SET NOCOUNT ON
    SET DATEFIRST 1
    SET @dteToday = GETDATE()
    SET @dteToday = DATEADD(ms, -DATEPART(ms, @dteToday), @dteToday)
    SET @dteToday = DATEADD(s, -DATEPART(s, @dteToday), @dteToday)
    SET @dteToday = DATEADD(n, -DATEPART(n, @dteToday), @dteToday)
    SET @dteToday = DATEADD(hh, -DATEPART(hh, @dteToday), @dteToday)
    SET @now = GETDATE()

   SET @report = @report + char(13) + char(10) + 'eSubs Daily Report' 
    SET @report = @report + char(13) + char(10)  + ''
    SET @report = @report + char(13) + char(10) + '* Temporary numbers. Complete report pending Q&A.'
    SET @report = @report + char(13) + char(10) + ''
    SET @report = @report + char(13) + char(10) + ''
    
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
    WHERE e.event_status_id IN (1,3)
      AND MONTH( tps.OrderDate ) = MONTH( @dteToday ) 
      AND YEAR( tps.OrderDate ) = YEAR( @dteToday ) 
    
    
    IF @intTotalQuantity IS NULL AND @intOrderTotal IS NULL
    BEGIN
	    SET @report = @report + char(13) + char(10) + 'No sales were recorded for the month.'
    	SET @report = @report + char(13) + char(10) + ''
    END
    ELSE
    BEGIN
	    SET @report = @report + char(13) + char(10) + 'Monthly sales for ' + DATENAME( MONTH, @dteStartingDate ) + ' ' + CONVERT( CHAR(4), YEAR( @dteStartingDate ) )
    	SET @report = @report + char(13) + char(10) + 'My Group Page: ' + CONVERT( VARCHAR(20), @intTotalQuantity ) + ' subs, $' + CONVERT( VARCHAR(20), @intOrderTotal ) 
	    SET @report = @report + char(13) + char(10) + ''
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
    WHERE e.event_status_id IN (1,3)
      AND tps.OrderDate BETWEEN DATEADD(day, -DATEPART(dw, @dteToday) + 1, @dteToday) AND @now
      AND YEAR( tps.OrderDate ) = YEAR( @dteToday ) 

        
    IF @intTotalQuantity IS NULL AND @intOrderTotal IS NULL
    BEGIN
    	SET @report = @report + char(13) + char(10) + 'No sales were recorded for the week.'
    	SET @report = @report + char(13) + char(10) + ''
    END
    ELSE
    BEGIN
    	SET @report = @report + char(13) + char(10) + 'Weekly sales as of ' + DATENAME( dw, @dteStartingDate ) + ', ' + DATENAME( MONTH, @dteStartingDate ) + ' ' + CONVERT( VARCHAR(2), DATENAME( DAY, @dteStartingDate ) ) + ', ' + CONVERT( CHAR(4), YEAR( @dteStartingDate ) )
    	SET @report = @report + char(13) + char(10) + 'My Group Page: ' + CONVERT( VARCHAR(20), @intTotalQuantity ) + ' subs, $' + CONVERT( VARCHAR(20), @intOrderTotal ) 
    	SET @report = @report + char(13) + char(10) + ''
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
    WHERE e.event_status_id IN (1,3)
      AND DATEPART(day, tps.OrderDate) = DATEPART(day, DATEADD(day, -1, @dteToday ))
      AND DATEPART(month, tps.OrderDate) = DATEPART(month, DATEADD(day, -1, @dteToday))
      AND DATEPART(year, tps.OrderDate) = DATEPART(year, DATEADD(day, -1, @dteToday))
    
    IF @intTotalQuantity IS NULL AND @intOrderTotal IS NULL
    BEGIN
    	SET @report = @report + char(13) + char(10) + 'No sales were recorded yesterday.'
    	SET @report = @report + char(13) + char(10) + ''
    END
    ELSE
    BEGIN
    	SET @report = @report + char(13) + char(10) + 'Yesterday''s sales ' + DATENAME( dw, @dteStartingDate ) + ', ' + DATENAME( MONTH, @dteStartingDate ) + ' ' + CONVERT( VARCHAR(2), DATENAME( DAY, @dteStartingDate ) ) + ', ' + CONVERT( CHAR(4), YEAR( @dteStartingDate ) )
    	SET @report = @report + char(13) + char(10) + 'My Group Page: ' + CONVERT( VARCHAR(20), @intTotalQuantity ) + ' subs, $' + CONVERT( VARCHAR(20), @intOrderTotal )
    	SET @report = @report + char(13) + char(10) + ''
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
    WHERE e.event_status_id IN (1,3)
      AND DATEPART(day, tps.OrderDate) = DATEPART(day, @dteToday)
      AND DATEPART(month, tps.OrderDate) = DATEPART(month, @dteToday)
      AND DATEPART(year, tps.OrderDate) = DATEPART(year, @dteToday)


    IF @intTotalQuantity IS NULL AND @intOrderTotal IS NULL
    BEGIN
    	SET @report = @report + char(13) + char(10) + 'No sales were recorded today.'
    	SET @report = @report + char(13) + char(10) + ''
    END
    ELSE
    BEGIN
    	SET @report = @report + char(13) + char(10) + 'Today''s sales'
    	SET @report = @report + char(13) + char(10) + 'My Group Page: ' + CONVERT( VARCHAR(20), @intTotalQuantity ) + ' subs, $' + CONVERT( VARCHAR(20), @intOrderTotal )
    	SET @report = @report + char(13) + char(10) + ''
    END    
        
    SET @report = @report + char(13) + char(10) + ''
    SET @report = @report + char(13) + char(10) + 'Campaign Activations'
    SET @report = @report + char(13) + char(10) + ''

    
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
	    WHERE e.event_status_id IN (1,3)
	    GROUP BY e.event_id
        ) v
    WHERE MONTH( v.order_date ) = MONTH( @dteToday ) 
      AND YEAR( v.order_date ) = YEAR( @dteToday ) 

    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) + 'No campaigns were activated this month.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    		SET @report = @report + char(13) + char(10) + '1 campaign was activated this month.'
    	ELSE
    		SET @report = @report + char(13) + char(10) + CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were activated this month.'
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
    	WHERE e.event_status_id IN (1,3)
    	GROUP BY e.event_id
    ) v
    WHERE v.order_date BETWEEN DATEADD( DAY, -DATEPART( dw, @dteToday ) + 1, @dteToday) AND @now
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) + 'No campaigns were activated this week.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    		SET @report = @report + char(13) + char(10) + '1 campaign was activated this week.'
    	ELSE
    		SET @report = @report + char(13) + char(10) + CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were activated this week.'
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
    		e.event_status_id IN (1,3)
    	GROUP BY 
    		e.event_id
    ) v
    WHERE 
      DATEPART(day, v.order_date) = DATEPART(day, DATEADD( DAY, -1, @dteToday ))
      AND DATEPART(month, v.order_date) = DATEPART(month, DATEADD( day, -1, @dteToday))
      AND DATEPART(year, v.order_date) = DATEPART(year, DATEADD(day, -1, @dteToday))
      
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) + 'No campaigns were activated yesterday.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    		SET @report = @report + char(13) + char(10) + '1 campaign was activated yesterday.'
    	ELSE
    		SET @report = @report + char(13) + char(10) + CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were activated yesterday.'
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
    	SET @report = @report + char(13) + char(10) + 'No campaigns were activated today.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    		SET @report = @report + char(13) + char(10) + '1 campaign was activated today.'
    	ELSE
    		SET @report = @report + char(13) + char(10) + CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were activated today.'
    END
    
    SET @report = @report + char(13) + char(10) + ''
    SET @report = @report + char(13) + char(10) + ''
    SET @report = @report + char(13) + char(10) + 'Campaign Kickoffs'
    SET @report = @report + char(13) + char(10) + ''
    

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
			INNER JOIN creation_channel cc 
				ON cc.creation_channel_id = mh.creation_channel_id 
         WHERE ( m.email_address is not null
                 and m.email_address not like '%@efundraising.com')
           AND cc.member_type_id = 2 and cc.is_contact = 1
         GROUP BY e.event_id
    ) v
    WHERE
    	( MONTH( v.create_date ) = MONTH( @dteToday ) )
     AND	( YEAR( v.create_date ) = YEAR( @dteToday ) )
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) + 'No campaigns were kicked-off this month.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    		SET @report = @report + char(13) + char(10) + CONVERT( CHAR(1), @intCampaignCount ) + '1 campaign was kicked-off this month.'
    	ELSE
    		SET @report = @report + char(13) + char(10) + CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were kicked-off this month.'
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
			INNER JOIN creation_channel cc 
				ON cc.creation_channel_id = mh.creation_channel_id 
         WHERE ( m.email_address is not null
                 and m.email_address not like '%@efundraising.com')
         AND cc.member_type_id = 2 and cc.is_contact = 1
         GROUP BY e.event_id
    ) v
    WHERE
        v.create_date BETWEEN DATEADD(day, -DATEPART(dw, @dteToday) + 1, @dteToday) AND @now
        AND YEAR( v.create_date ) = YEAR( @dteToday ) 
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) + 'No campaigns were kicked-off this week.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    		SET @report = @report + char(13) + char(10) + '1 campaign was kicked-off this week.'
    	ELSE
    		SET @report = @report + char(13) + char(10) + CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were kicked-off this week.'
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
			INNER JOIN creation_channel cc 
				ON cc.creation_channel_id = mh.creation_channel_id 
         WHERE ( m.email_address is not null
                 and m.email_address not like '%@efundraising.com')
           AND cc.member_type_id = 2 and cc.is_contact = 1
         GROUP BY e.event_id
    ) v
    WHERE
          DATEPART(year, v.create_date) = DATEPART(year, DATEADD(day, -1, @dteToday))
      AND DATEPART(month, v.create_date) = DATEPART(month, DATEADD(day, -1, @dteToday))
      AND DATEPART(day, v.create_date) = DATEPART(day, DATEADD(day, -1, @dteToday))
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) + 'No campaigns were kicked-off yesterday.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    		SET @report = @report + char(13) + char(10) + '1 campaign was kicked-off yesterday.'
    	ELSE
    		SET @report = @report + char(13) + char(10) + CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were kicked-off yesterday.'
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
			INNER JOIN creation_channel cc 
				ON cc.creation_channel_id = mh.creation_channel_id	
         WHERE ( m.email_address is not null
                 and m.email_address not like '%@efundraising.com')
           AND cc.member_type_id = 2 and cc.is_contact = 1
         GROUP BY e.event_id
    ) v
    WHERE
        DATEPART(year, v.create_date) = DATEPART(year, @dteToday)
    AND DATEPART(month, v.create_date) = DATEPART(month, @dteToday)
    AND DATEPART(day, v.create_date) = DATEPART(day, @dteToday)
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) + 'No campaigns were kicked-off today.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    		SET @report = @report + char(13) + char(10) + '1 campaign was kicked-off today.'
    	ELSE
    		SET @report = @report + char(13) + char(10) + CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were kicked-off today.'
    END

END
GO
