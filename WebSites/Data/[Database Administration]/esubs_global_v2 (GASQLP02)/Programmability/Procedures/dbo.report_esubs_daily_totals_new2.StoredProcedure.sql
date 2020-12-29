USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[report_esubs_daily_totals_new2]    Script Date: 02/14/2014 13:08:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
    Created by: Drew Pettit
    Created on: 1 Sept 2009
    
	Updated by: Melissa Cote
	Updated on: 1 Feb 2011
	Update description : Add relaunched campaign and last year result to the activations section
	Updated by: Melissa Cote
	Updated on: 9 Nov 2011
	Update description : add import manual roster in Ko results

    Description: 

    This report will only work when the suppID 
    field is filled with event_participation_id    

	Modified by: Philippe Girard
	
	For performance reason.
	declare @report varchar(8000) 
	exec [report_esubs_daily_totals_new2] @report OUTPUT
	print @report



*/

CREATE PROCEDURE [dbo].[report_esubs_daily_totals_new2]
	@report varchar(8000) OUTPUT
AS
BEGIN
    DECLARE @varProduct varchar(30)
    DECLARE @intTotalQuantity INT
    DECLARE @intOrderTotal DECIMAL(15,2)    
    DECLARE @intLYTotalQuantity INT
    DECLARE @intLYOrderTotal DECIMAL(9,2)    
    DECLARE @intCumulativeQty INT    
    DECLARE @decCumulativeTotal DECIMAL(15,2)    
    DECLARE @decProcessingFees DECIMAL(9,2)    
    DECLARE @decProcessingFeesTotal DECIMAL(9,2)    
    DECLARE @dteStartingDate DATETIME
    DECLARE @intCampaignCount INT
    DECLARE @intImportCount INT
    DECLARE @intManualCount INT 
    DECLARE @intRosterCount INT 
    DECLARE @intParticipantCount INT
    DECLARE @intSupporterCount INT
    DECLARE @dteToday DATETIME
    DECLARE @dteLastYear DATETIME
    DECLARE @now DATETIME
	
    DECLARE @partner_name VARCHAR(100)
    DECLARE @event_id int
    DECLARE @event_name VARCHAR(100)
    DECLARE @ProcessingFee int
    DECLARE @create_date datetime
    DECLARE @Orders int
    DECLARE @OrderTotal MONEY
    DECLARE @TotalQuantity int

	DECLARE @tmp_sales TABLE (
		product_type_name VARCHAR(100)
		, create_date datetime
		, quantity INT
		, total MONEY
	)
	
	DECLARE @tmp TABLE (
		product_type_name VARCHAR(100)
		, quantity INT
		, total MONEY
	)

	DECLARE @top10 TABLE (
	 partner_name VARCHAR(100)
	, event_id int
	, event_name VARCHAR(100)
	, ProcessingFee int
	, create_date datetime
	, Orders int
	, OrderTotal MONEY
	, TotalQuantity int
	)

	DECLARE @activations TABLE (
		event_id int
		, event_status_id INT
		, create_date datetime
	)

	DECLARE @activationsPY TABLE (
		event_id int
		, event_status_id INT
		, create_date datetime
	)


    SET @report = ''

    SET NOCOUNT ON
    SET DATEFIRST 1
    SET @dteToday = GETDATE()
    SET @dteLastYear = DATEADD(yy, -1, GETDATE()) + 1
    SET @dteToday = DATEADD(ms, -DATEPART(ms, @dteToday), @dteToday)
    SET @dteToday = DATEADD(s, -DATEPART(s, @dteToday), @dteToday)
    SET @dteToday = DATEADD(n, -DATEPART(n, @dteToday), @dteToday)
    SET @dteToday = DATEADD(hh, -DATEPART(hh, @dteToday), @dteToday)
    SET @now = GETDATE()

    SET @report = @report + char(13) + char(10) +  'EFUNDRAISING ONLINE Report' 
    SET @report = @report + char(13) + char(10) +  ''
    SET @report = @report + char(13) + char(10) +  ''
    
/*************************************/
Print '-- Load sales data for report '
Print getDate()
/************************************/
    INSERT INTO @tmp_sales
    SELECT 
	  product_type_desc	 AS product_type_desc
	, create_date		 AS create_date
	, quantity 		 AS quantity
	, sub_total  AS total
     FROM [esubs_global_v2].[dbo].[es_get_valid_orders_items] () es
    WHERE DATEPART(year,create_date) = DATEPART(year, @dteToday)

--/*************************************/
Print '---- Last Years Sales - same day of week'
Print getDate()
--/************************************/
  
	SELECT @intLYTotalQuantity 	= SUM(quantity) 
	      ,@intLYOrderTotal 	= SUM(sub_total)
	FROM [esubs_global_v2].[dbo].[es_get_valid_orders_items] () es
	WHERE DATEPART(day, create_date) = DATEPART(day, @dteLastYear)
	AND DATEPART(month, create_date) = DATEPART(month, @dteLastYear)
	AND DATEPART(year, create_date) = DATEPART(year, @dteLastYear)

/*************************************/
Print '-- Load the top 5 sales groups'
Print getDate()
/************************************/

INSERT INTO @top10
SELECT top 10 prt.partner_name
	, ev.event_id
	, ev.event_name
	, 0 as ProcessingFee
	, ev.create_date
	, count(distinct es.order_id) as Orders
	, SUM(sub_total) as OrderTotal
	, SUM(quantity) as TotalQuantity
 FROM [esubs_global_v2].[dbo].[es_get_valid_orders_items] () es
    INNER JOIN [esubs_global_v2].[dbo].event_participation ep  with(nolock) ON es.supp_id  = ep.event_participation_id
	INNER JOIN [esubs_global_v2].[dbo].event ev  with(nolock) ON ev.event_id = ep.event_id
	INNER JOIN [esubs_global_v2].[dbo].event_group eg  with(nolock) ON ev.event_id = eg.event_id
	INNER JOIN [esubs_global_v2].[dbo].[group] g  with(nolock) ON g.group_id = eg.group_id
	INNER JOIN EFRCommon.dbo.partner prt  with(nolock) ON prt.partner_id = g.partner_id
WHERE DATEPART(day, es.create_date ) = DATEPART(day, @dteToday)
AND DATEPART(month, es.create_date ) = DATEPART(month, @dteToday)
AND DATEPART(year, es.create_date ) = DATEPART(year, @dteToday)
AND prt.partner_id != 857
GROUP BY prt.partner_id
	  , prt.partner_name
	  , ev.event_id	  
	  , ev.event_name
	  , ev.processing_fee
	  , ev.create_date
ORDER BY SUM(sub_total) DESC

INSERT INTO @top10
SELECT prt.partner_name
	, ev.event_id
	, ev.event_name
	, 0 as ProcessingFee
	, ev.create_date
	, count(distinct es.order_id) as Orders
	, SUM(sub_total) as OrderTotal
	, SUM(quantity) as TotalQuantity
 FROM [esubs_global_v2].[dbo].[es_get_valid_orders_items] () es
    INNER JOIN [esubs_global_v2].[dbo].event_participation ep  with(nolock) ON es.supp_id  = ep.event_participation_id
	INNER JOIN [esubs_global_v2].[dbo].event ev  with(nolock) ON ev.event_id = ep.event_id
	INNER JOIN [esubs_global_v2].[dbo].event_group eg  with(nolock) ON ev.event_id = eg.event_id
	INNER JOIN [esubs_global_v2].[dbo].[group] g  with(nolock) ON g.group_id = eg.group_id
	INNER JOIN EFRCommon.dbo.partner prt  with(nolock) ON prt.partner_id = g.partner_id
WHERE DATEPART(day, es.create_date ) = DATEPART(day, @dteToday)
AND DATEPART(month, es.create_date ) = DATEPART(month, @dteToday)
AND DATEPART(year, es.create_date ) = DATEPART(year, @dteToday)
AND prt.partner_id = 857
GROUP BY prt.partner_id
	  , prt.partner_name
	  , ev.event_id	  
	  , ev.event_name
	  , ev.processing_fee
	  , ev.create_date
ORDER BY SUM(sub_total) DESC


/************************************/
Print '-- Todays sales'
Print getDate()
/************************************/
    
    INSERT INTO @tmp (product_type_name,quantity,total)
    SELECT product_type_name,SUM(quantity),SUM(total)
      FROM @tmp_sales
    WHERE DATEPART(day, create_date) = DATEPART(day, @dteToday)
      AND DATEPART(month, create_date) = DATEPART(month, @dteToday)
      AND DATEPART(year, create_date) = DATEPART(year, @dteToday)
	GROUP BY product_type_name
	
    SET @decCumulativeTotal = 0
    SET @intCumulativeQty = 0
    SET @decProcessingFeesTotal = 0
        
    SET @report = @report + char(13) + char(10) +  '==============================='
    SET @report = @report + char(13) + char(10) +  '       Today''s sales '
    SET @report = @report + char(13) + char(10) +  '==============================='
        
    IF NOT EXISTS(SELECT product_type_name FROM @tmp)
	BEGIN
		SET @report = @report + char(13) + char(10) +  'No sales were recorded for Today.'
		SET @report = @report + char(13) + char(10) +  ''
	END
    ELSE
	BEGIN
	    WHILE EXISTS(SELECT product_type_name FROM @tmp)
	    BEGIN 
			SELECT TOP 1 @varProduct = product_type_name
					, @intTotalQuantity = quantity
					, @intOrderTotal = total
			FROM @tmp
			
			DELETE FROM @tmp WHERE product_type_name = @varProduct
  
			SET @report = @report + char(13) + char(10) +  'Product Line: ' + @varProduct
			SET @report = @report + char(13) + char(10) +  'Qty Sold: ' + CONVERT( VARCHAR(20), @intTotalQuantity ) + ', Revenue: $' + CONVERT( VARCHAR(20), @intOrderTotal ) 
			SET @report = @report + char(13) + char(10) +  ''
		    
			SET @decCumulativeTotal = @decCumulativeTotal + @intOrderTotal
			SET @intCumulativeQty = @intCumulativeQty + @intTotalQuantity
		  	
			--FETCH NEXT FROM db_cursor INTO @varProduct,@intTotalQuantity, @intOrderTotal, @dteStartingDate
	    END   
--		SET @decCumulativeTotal = @decCumulativeTotal + @decProcessingFees
--		SET @decProcessingFeesTotal = @decProcessingFeesTotal + @decProcessingFees
--		SET @report = @report + char(13) + char(10) + 'Processing Fees: $' + CONVERT( VARCHAR(20),@decProcessingFees)
	    	SET @report = @report + char(13) + char(10) +  ' '
		SET @report = @report + char(13) + char(10) +  '----------------------'
    		SET @report = @report + char(13) + char(10) +  'Total: Qty Sold: ' + CONVERT( VARCHAR(20), @intCumulativeQty ) + ', Revenue: $' + CONVERT( VARCHAR(20), @decCumulativeTotal ) 
	    	SET @report = @report + char(13) + char(10) +  ' '
        	SET @report = @report + char(13) + char(10) +  '==============================='
        	SET @report = @report + char(13) + char(10) +  '       Last Years sales on this day '
        	SET @report = @report + char(13) + char(10) +  '==============================='
        	SET @report = @report + char(13) + char(10) +  'Total: Qty Sold: ' + CONVERT( VARCHAR(20), @intLYTotalQuantity ) + ', Revenue: $' + CONVERT( VARCHAR(20), @intLYOrderTotal ) 
	    SET @report = @report + char(13) + char(10) +  '   ' 
	    SET @report = @report + char(13) + char(10) +  '   ' 
	END	


/*************************************/
Print '-- Display the top 10 sales groups'
Print getDate()
/************************************/

SET @report = @report + char(13) + char(10) +  '==============================='
SET @report = @report + char(13) + char(10) +  '       TOP 5 Groups Today '
SET @report = @report + char(13) + char(10) +  '==============================='
SET @report = @report + char(13) + char(10) +  ''
			
	    WHILE EXISTS(SELECT partner_name FROM @top10)
	    BEGIN 
			SELECT 
			    @partner_name = partner_name 
			    ,@event_id = event_id
			    ,@event_name = event_name 
			    ,@ProcessingFee = ProcessingFee 
			    ,@create_date = create_date 
			    ,@Orders = coalesce(Orders, 0)
			    ,@OrderTotal = coalesce(OrderTotal , 0)
			    ,@TotalQuantity = coalesce(TotalQuantity, 0)
			FROM @top10
			
			DELETE FROM @top10 WHERE event_id = @event_id
			SET @report = @report + char(13) + char(10) +  ' Partner Name: ' + @partner_name 
			SET @report = @report + char(13) + char(10) +  ' Event Name: ' + @event_name 
			--SET @report = @report + char(13) + char(10) +  ' Processing Fee: ' + CONVERT( VARCHAR(20), @ProcessingFee ) 
			--SET @report = @report + char(13) + char(10) +  ' Event Created: ' + CONVERT( VARCHAR(20), @create_date ) 
			SET @report = @report + char(13) + char(10) +  ' Orders: ' + CONVERT( VARCHAR(20), @Orders )
			SET @report = @report + char(13) + char(10) +  ' Sales: ' + CONVERT( VARCHAR(20), @OrderTotal ) 
			SET @report = @report + char(13) + char(10) +  ' Qty: ' + CONVERT( VARCHAR(20), @TotalQuantity )
			SET @report = @report + char(13) + char(10) +  ''
	    END 
	SET @report = @report + char(13) + char(10) +  ''
	SET @report = @report + char(13) + char(10) +  ''

/************************************/
Print '-- Yesterdays Sales'
Print getDate()
/************************************/

 DELETE FROM @tmp

Print '-- Deleted from tmp'
Print getDate()

 INSERT INTO @tmp (product_type_name,quantity,total)
SELECT product_type_name,SUM(quantity),SUM(total)
  FROM @tmp_sales
  WHERE DATEPART(day, create_date) = DATEPART(day, DATEADD(day, -1, @dteToday ))
    AND DATEPART(month, create_date) = DATEPART(month, DATEADD(day, -1, @dteToday))
    AND DATEPART(year, create_date) = DATEPART(year, DATEADD(day, -1, @dteToday))
GROUP BY product_type_name
	
    SET @dteStartingDate = DATEADD(day, -1, @dteToday )
    SET @decCumulativeTotal = 0
    SET @intCumulativeQty = 0

    SET @report = @report + char(13) + char(10) +  '==============================='
    SET @report = @report + char(13) + char(10) +  '       Yesterday''s sales ' + DATENAME( dw, @dteStartingDate ) + ', ' + DATENAME( MONTH, @dteStartingDate ) + ' ' + CONVERT( VARCHAR(2), DATENAME( DAY, @dteStartingDate ) ) + ', ' + CONVERT( CHAR(4), YEAR( @dteStartingDate ) ) + ' '
    SET @report = @report + char(13) + char(10) +  '==============================='


    IF NOT EXISTS(SELECT product_type_name FROM @tmp)
   	BEGIN
		SET @report = @report + char(13) + char(10) +  'No sales were recorded Yesterday.'
		SET @report = @report + char(13) + char(10) +  ''
   	END
    ELSE
	BEGIN
	    WHILE EXISTS(SELECT product_type_name FROM @tmp)
	    BEGIN

			SELECT TOP 1 @varProduct = coalesce(product_type_name, ' ')
					, @intTotalQuantity = coalesce(quantity, 0)
					, @intOrderTotal = coalesce(total, 0)
			FROM @tmp
			
			DELETE FROM @tmp WHERE product_type_name = @varProduct
	    
			SET @report = @report + char(13) + char(10) +  'Product Line: ' + @varProduct
			SET @report = @report + char(13) + char(10) +  'Qty Sold: ' + CONVERT( VARCHAR(20), @intTotalQuantity ) + ', Revenue: $' + CONVERT( VARCHAR(20), @intOrderTotal ) 
			SET @report = @report + char(13) + char(10) +  ''
		    
			SET @decCumulativeTotal = @decCumulativeTotal + @intOrderTotal
			SET @intCumulativeQty = @intCumulativeQty + @intTotalQuantity
    		
	    END
	    
--	    SET @decCumulativeTotal = @decCumulativeTotal + @decProcessingFees
--	    SET @decProcessingFeesTotal = @decProcessingFeesTotal + @decProcessingFees
--	    SET @report = @report + char(13) + char(10) + 'Processing Fees: $' + CONVERT( VARCHAR(20),@decProcessingFees)
--	    SET @report = @report + char(13) + char(10) +  ' '
	    SET @report = @report + char(13) + char(10) +  '--------------------'
	    SET @report = @report + char(13) + char(10) +  'Total: Qty Sold: ' + CONVERT( VARCHAR(20), @intCumulativeQty ) + ', Revenue: $' + CONVERT( VARCHAR(20), @decCumulativeTotal ) 
	    SET @report = @report + char(13) + char(10) +  '   ' 
	    SET @report = @report + char(13) + char(10) +  '   ' 
	END

/************************************/
Print '-- Weekly Sales'
Print getDate()
/************************************/

DELETE FROM @tmp

INSERT INTO @tmp (product_type_name,quantity,total)
SELECT product_type_name,SUM(quantity),SUM(total)
  FROM @tmp_sales
 WHERE create_date BETWEEN DATEADD(day, -DATEPART(dw, @dteToday) + 1, @dteToday) AND @now
   AND YEAR( create_date ) = YEAR( @dteToday ) 
GROUP BY product_type_name
	
    SET @dteStartingDate = DATEADD(day, -DATEPART(dw, @dteToday) + 1, @dteToday)
    SET @decCumulativeTotal = 0
    SET @intCumulativeQty = 0
		
    SET @report = @report + char(13) + char(10) +  '==============================='
    SET @report = @report + char(13) + char(10) +  '       Weekly sales as of ' + DATENAME( dw, @dteStartingDate ) + ', ' + DATENAME( MONTH, @dteStartingDate ) + ' ' + CONVERT( VARCHAR(2), DATENAME( DAY, @dteStartingDate ) ) + ', ' + CONVERT( CHAR(4), YEAR( @dteStartingDate ) ) + ' '
    SET @report = @report + char(13) + char(10) +  '==============================='
 
    IF NOT EXISTS(SELECT product_type_name FROM @tmp)
 	BEGIN
		SET @report = @report + char(13) + char(10) +  'No sales were recorded for the week.'
		SET @report = @report + char(13) + char(10) +  ''
 	END
    ELSE
	BEGIN
	    WHILE EXISTS(SELECT product_type_name FROM @tmp)   
	    BEGIN   

		    SELECT TOP 1 @varProduct = coalesce(product_type_name, ' ')
					, @intTotalQuantity = coalesce(quantity, 0)
					, @intOrderTotal = coalesce(total, 0)
			FROM @tmp
			
			DELETE FROM @tmp WHERE product_type_name = @varProduct
	
			SET @report = @report + char(13) + char(10) +  'Product Line: ' + @varProduct
			SET @report = @report + char(13) + char(10) +  'Qty Sold: ' + CONVERT( VARCHAR(20), @intTotalQuantity ) + ', Revenue: $' + CONVERT( VARCHAR(20), @intOrderTotal ) 
			SET @report = @report + char(13) + char(10) +  ''
	    	SET @decCumulativeTotal = @decCumulativeTotal + @intOrderTotal
			SET @intCumulativeQty = @intCumulativeQty + @intTotalQuantity

	    END
   
--   	    SET @decCumulativeTotal = @decCumulativeTotal + @decProcessingFees
--   	    SET @decProcessingFeesTotal = @decProcessingFeesTotal + @decProcessingFees
--   	    SET @report = @report + char(13) + char(10) + 'Processing Fees: $' + CONVERT( VARCHAR(20),@decProcessingFees)

  	    SET @report = @report + char(13) + char(10) +  ' '
 	    SET @report = @report + char(13) + char(10) +  '-----------------------'
	    SET @report = @report + char(13) + char(10) +  'Total: Qty Sold: ' + CONVERT( VARCHAR(20), @intCumulativeQty ) + ', Revenue: $' + CONVERT( VARCHAR(20), @decCumulativeTotal ) 
  	    SET @report = @report + char(13) + char(10) +  '   ' 
	    SET @report = @report + char(13) + char(10) +  '   ' 
	END  



/************************************/
Print '-- Monthly Sales'
Print getDate()
/************************************/

DELETE FROM @tmp

INSERT INTO @tmp (product_type_name,quantity,total)
SELECT product_type_name,SUM(quantity),SUM(total)
  FROM @tmp_sales
 WHERE MONTH(create_date) = MONTH( @dteToday ) 
   AND YEAR(create_date) = YEAR( @dteToday ) 
GROUP BY product_type_name

    SET @decCumulativeTotal = 0
    SET @intCumulativeQty = 0

    SET @report = @report + char(13) + char(10) +  '==============================='
    SET @report = @report + char(13) + char(10) +  '       Monthly sales for ' + DATENAME( MONTH, @dteToday ) + ' ' + CONVERT( CHAR(4), YEAR( @dteToday ) ) + ' '
    SET @report = @report + char(13) + char(10) +  '==============================='
    
    IF NOT EXISTS(SELECT product_type_name FROM @tmp)
	BEGIN
		SET @report = @report + char(13) + char(10) +  'No sales were recorded for the month.'
		SET @report = @report + char(13) + char(10) +  ''
	    END
    ELSE
    BEGIN
	    WHILE EXISTS(SELECT product_type_name FROM @tmp)
	    BEGIN   
		
			SELECT TOP 1 @varProduct = product_type_name
					, @intTotalQuantity = coalesce(quantity, 0)
					, @intOrderTotal = coalesce(total, 0)
			FROM @tmp
			
			DELETE FROM @tmp WHERE product_type_name = @varProduct

			SET @report = @report + char(13) + char(10) +  'Product Line: ' + @varProduct
			SET @report = @report + char(13) + char(10) +  'Qty Sold: ' + CONVERT( VARCHAR(20), @intTotalQuantity ) + ', Revenue: $' + CONVERT( VARCHAR(20), @intOrderTotal ) 
			SET @report = @report + char(13) + char(10) +  ''
	    	SET @decCumulativeTotal = @decCumulativeTotal + @intOrderTotal
			SET @intCumulativeQty = @intCumulativeQty + @intTotalQuantity

	    END   

--       	    SET @decCumulativeTotal = @decCumulativeTotal + @decProcessingFees
--      	    SET @decProcessingFeesTotal = @decProcessingFeesTotal + @decProcessingFees
--       	    SET @report = @report + char(13) + char(10) + 'Processing Fees: $' + CONVERT( VARCHAR(20),@decProcessingFees)
	    SET @report = @report + char(13) + char(10) +  ' '
	    SET @report = @report + char(13) + char(10) +  '------------------------'
	    SET @report = @report + char(13) + char(10) +  'Total: Qty Sold: ' + CONVERT( VARCHAR(20), @intCumulativeQty ) + ', Revenue: $' + CONVERT( VARCHAR(20), @decCumulativeTotal ) 
	    SET @report = @report + char(13) + char(10) +  '   ' 
	    SET @report = @report + char(13) + char(10) +  '   ' 
	END
    
/************************************/
Print '-- Annual Sales'
Print getDate()
/************************************/

DELETE FROM @tmp

 INSERT INTO @tmp (product_type_name,quantity,total)
SELECT product_type_name,SUM(quantity),SUM(total)
  FROM @tmp_sales
  WHERE DATEPART(year, create_date) = DATEPART(year, DATEADD(day, -1, @dteToday))
GROUP BY product_type_name
	
    SET @decCumulativeTotal = 0
    SET @intCumulativeQty = 0

    SET @report = @report + char(13) + char(10) +  '==============================='
    SET @report = @report + char(13) + char(10) +  '       Annual sales for ' + DATENAME( YEAR, @dteToday ) + ' '
    SET @report = @report + char(13) + char(10) +  '==============================='
    
    WHILE EXISTS(SELECT product_type_name FROM @tmp)
    BEGIN   

		SELECT TOP 1 @varProduct = product_type_name
				, @intTotalQuantity = coalesce(quantity,0)
				, @intOrderTotal = coalesce(total,0)
		FROM @tmp

		DELETE FROM @tmp WHERE product_type_name = @varProduct

		SET @report = @report + char(13) + char(10) +  'Product Line: ' + @varProduct
		SET @report = @report + char(13) + char(10) +  'Qty Sold: ' + CONVERT( VARCHAR(20), @intTotalQuantity ) + ', Revenue: $' + CONVERT( VARCHAR(20), @intOrderTotal ) 
		SET @report = @report + char(13) + char(10) +  ''
		SET @decCumulativeTotal = @decCumulativeTotal + @intOrderTotal
		SET @intCumulativeQty = @intCumulativeQty + @intTotalQuantity

    END   

--    SET @decCumulativeTotal = @decCumulativeTotal + @decProcessingFees
--    SET @decProcessingFeesTotal = @decProcessingFeesTotal + @decProcessingFees
--    SET @report = @report + char(13) + char(10) + 'Processing Fees: $' + CONVERT( VARCHAR(20),@decProcessingFees)
    SET @report = @report + char(13) + char(10) +  ' '
    SET @report = @report + char(13) + char(10) +  '-----------------------------'
    SET @report = @report + char(13) + char(10) +  'Total: Qty Sold: ' + CONVERT( VARCHAR(20), @intCumulativeQty ) + ', Revenue: $' + CONVERT( VARCHAR(20), @decCumulativeTotal ) 
    SET @report = @report + char(13) + char(10) +  '   ' 
    SET @report = @report + char(13) + char(10) +  '   ' 



/************************************/
Print '-- Activations'
Print getDate()
/************************************/

	SET @report = @report + char(13) + char(10) +  '==============================='
	SET @report = @report + char(13) + char(10) +  '       Campaign Activations'
	SET @report = @report + char(13) + char(10) +  '==============================='


-- Prime the data
INSERT INTO @activations 
	(event_id 
	, event_status_id 
	, create_date )
SELECT ev.event_id, ev.event_status_id, orders.create_date
  FROM dbo.es_get_valid_orders_items() orders
  JOIN dbo.event ev ON ev.event_id = orders.event_id
WHERE orders.act = 1
  AND MONTH(orders.create_date) = MONTH(GETDATE())
  AND YEAR(orders.create_date) = YEAR(GETDATE())


INSERT INTO @activationsPY 
	(event_id 
	, event_status_id 
	, create_date )
SELECT ev.event_id, ev.event_status_id, orders.create_date
  FROM dbo.es_get_valid_orders_items() orders
  JOIN dbo.event ev ON ev.event_id = orders.event_id
WHERE orders.act = 1
  AND MONTH(orders.create_date) = MONTH(@dteLastYear)
  AND YEAR(orders.create_date) = YEAR(@dteLastYear)
  
	
	DECLARE @intCampaignCountNew int 
	DECLARE @intCampaignCountRelaunched int 

    -- This month's campaign activations
        SET @intCampaignCount = NULL
	SET @intCampaignCountNew = NULL
	SET @intCampaignCountRelaunched = NULL

	SELECT @intCampaignCount = COUNT(event_id)
	  FROM @activations
	
	SELECT @intCampaignCountNew  = COUNT(event_id)
	  FROM @activations
	 WHERE event_status_id = 1
	
	SELECT @intCampaignCountRelaunched = COUNT(event_id)
	  FROM @activations
	 WHERE event_status_id = 3


    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) +  'No campaign were activated this month.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
		begin 
			IF @intCampaignCountNew = 1
    			SET @report = @report + char(13) + char(10) +  '1 new campaign was activated this month.'
			ELSE 
				SET @report = @report + char(13) + char(10) +  '1 relaunched campaign was activated this month.'
		end 
    	ELSE
    		SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns (' + CONVERT( VARCHAR(5), @intCampaignCountNew )+ ' new, ' + CONVERT( VARCHAR(5), @intCampaignCountRelaunched ) + ' relaunched) were activated this month.'
    END

    -- This week's campaign activations
	SET @intCampaignCount = NULL
	SET @intCampaignCountNew = NULL
	SET @intCampaignCountRelaunched = NULL

	SELECT @intCampaignCount = COUNT(event_id)
	  FROM @activations
	 WHERE create_date BETWEEN DATEADD( DAY, -DATEPART( dw, @dteToday ) + 1, @dteToday) AND @now


	SELECT @intCampaignCountNew = COUNT(event_id)
	  FROM @activations
	 WHERE event_status_id = 1
	   AND create_date BETWEEN DATEADD( DAY, -DATEPART( dw, @dteToday ) + 1, @dteToday) AND @now

	SELECT @intCampaignCountRelaunched = COUNT(event_id)
	  FROM @activations
	 WHERE event_status_id = 3
	   AND create_date BETWEEN DATEADD( DAY, -DATEPART( dw, @dteToday ) + 1, @dteToday) AND @now



    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) +  'No campaigns were activated this week.'
    END
    ELSE
    BEGIN
    	
		IF @intCampaignCount = 1
		begin 
			IF @intCampaignCountNew = 1
    			SET @report = @report + char(13) + char(10) +  '1 new campaign was activated this week.'
			ELSE 
				SET @report = @report + char(13) + char(10) +  '1 relaunched campaign was activated this week.'
		end 
    	ELSE
    		SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns (' + CONVERT( VARCHAR(5), @intCampaignCountNew )+ ' new, ' + CONVERT( VARCHAR(5), @intCampaignCountRelaunched ) + ' relaunched) were activated this week.'
    END
    
    -- Yesterday's campaign activations
    SET @intCampaignCount = NULL
    SET @intCampaignCountNew = NULL
    SET @intCampaignCountRelaunched = NULL

	SELECT @intCampaignCount = COUNT(event_id)
	  FROM @activations
	 WHERE DATEPART(DAY, create_date) = DATEPART(day, DATEADD( DAY, -1, @dteToday ))
 	   AND DATEPART(MONTH, create_date) = DATEPART(month, DATEADD( day, -1, @dteToday))
           AND DATEPART(year, create_date) = DATEPART(year, DATEADD(day, -1, @dteToday))

	SELECT @intCampaignCountNew = COUNT(event_id)
	  FROM @activations
	 WHERE event_status_id = 1
	   AND DATEPART(DAY, create_date) = DATEPART(day, DATEADD( DAY, -1, @dteToday ))
 	   AND DATEPART(MONTH, create_date) = DATEPART(month, DATEADD( day, -1, @dteToday))
           AND DATEPART(year, create_date) = DATEPART(year, DATEADD(day, -1, @dteToday))

    
	SELECT @intCampaignCountRelaunched = COUNT(event_id)
	  FROM @activations
	 WHERE event_status_id = 3
	   AND DATEPART(DAY, create_date) = DATEPART(day, DATEADD( DAY, -1, @dteToday ))
 	   AND DATEPART(MONTH, create_date) = DATEPART(month, DATEADD( day, -1, @dteToday))
           AND DATEPART(year, create_date) = DATEPART(year, DATEADD(day, -1, @dteToday))


    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) +  'No campaigns were activated yesterday.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
		begin 
			IF @intCampaignCountNew = 1
    			SET @report = @report + char(13) + char(10) +  '1 new campaign was activated yesterday.'
			ELSE 
				SET @report = @report + char(13) + char(10) +  '1 relaunched campaign was activated yesterday.'
		end 
    	ELSE
    		SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns (' + CONVERT( VARCHAR(5), @intCampaignCountNew )+ ' new, ' + CONVERT( VARCHAR(5), @intCampaignCountRelaunched ) + ' relaunched) were activated yesterday.'
	END
        
    -- Today's campaign activations
    SET @intCampaignCount = NULL
    SET @intCampaignCountNew = NULL
    SET @intCampaignCountRelaunched = NULL

	SELECT @intCampaignCount = COUNT(event_id)
	  FROM @activations
	 WHERE DATEPART(DAY, create_date) = DATEPART(day, @dteToday)
 	   AND DATEPART(MONTH, create_date) = DATEPART(month, @dteToday)
           AND DATEPART(year, create_date) = DATEPART(year, @dteToday)


	SELECT @intCampaignCountNew = COUNT(event_id)
	  FROM @activations
	 WHERE event_status_id = 1
	   AND DATEPART(DAY, create_date) = DATEPART(day, @dteToday)
 	   AND DATEPART(MONTH, create_date) = DATEPART(month, @dteToday)
           AND DATEPART(year, create_date) = DATEPART(year, @dteToday)


	SELECT @intCampaignCountRelaunched = COUNT(event_id)
	  FROM @activations
	 WHERE event_status_id = 3
	   AND DATEPART(DAY, create_date) = DATEPART(day, @dteToday)
 	   AND DATEPART(MONTH, create_date) = DATEPART(month, @dteToday)
           AND DATEPART(year, create_date) = DATEPART(year, @dteToday)


    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) +  'No campaigns were activated today.'
    END
    ELSE
    BEGIN
		IF @intCampaignCount = 1
		begin 
			IF @intCampaignCountNew = 1
    			SET @report = @report + char(13) + char(10) +  '1 new campaign was activated today.'
			ELSE 
				SET @report = @report + char(13) + char(10) +  '1 relaunched campaign was activated today.'
		end 
    	ELSE
    		SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns (' + CONVERT( VARCHAR(5), @intCampaignCountNew )+ ' new, ' + CONVERT( VARCHAR(5), @intCampaignCountRelaunched ) + ' relaunched) were activated today.'
    END
    

/************************************/
Print '-- Activations Last Year'
/************************************/
    
	SET @report = @report + char(13) + char(10) +  '==============================='
	SET @report = @report + char(13) + char(10) +  ' Last Year Campaign Activations '
	SET @report = @report + char(13) + char(10) +  '==============================='

    -- This month's campaign activations
        SET @intCampaignCount = NULL
	SET @intCampaignCountNew = NULL
	SET @intCampaignCountRelaunched = NULL

	SELECT @intCampaignCount = COUNT(event_id)
	  FROM @activationsPY
	 WHERE MONTH(create_date) = MONTH(@dteLastYear)
	   AND YEAR(create_date) = YEAR(@dteLastYear)
	  
	SELECT @intCampaignCountNew  = COUNT(event_id)
	  FROM @activationsPY
	 WHERE event_status_id = 1
	   AND MONTH(create_date) = MONTH(@dteLastYear)
	   AND YEAR(create_date) = YEAR(@dteLastYear)
	  
	
	SELECT @intCampaignCountRelaunched = COUNT(event_id)
	  FROM @activationsPY
	 WHERE event_status_id = 3
	   AND MONTH(create_date) = MONTH(@dteLastYear)
	   AND YEAR(create_date) = YEAR(@dteLastYear)
	

    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) +  'No campaign were activated this month.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
		begin 
			IF @intCampaignCountNew = 1
    			SET @report = @report + char(13) + char(10) +  '1 new campaign was activated this month.'
			ELSE 
				SET @report = @report + char(13) + char(10) +  '1 relaunched campaign was activated this month.'
		end 
    	ELSE
    		SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns (' + CONVERT( VARCHAR(5), @intCampaignCountNew )+ ' new, ' + CONVERT( VARCHAR(5), @intCampaignCountRelaunched ) + ' relaunched) were activated this month.'
    END

    -- This month to date campaign activations
	SET @intCampaignCount = NULL
	SET @intCampaignCountNew = NULL
	SET @intCampaignCountRelaunched = NULL

	SELECT @intCampaignCount = COUNT(event_id)
	  FROM @activationsPY
	 WHERE MONTH(create_date) = MONTH(@dteLastYear)
	   AND YEAR(create_date) = YEAR(@dteLastYear)
	   AND create_date <= @dteLastYear

	SELECT @intCampaignCountNew = COUNT(event_id)
	  FROM @activationsPY
	 WHERE event_status_id = 1
	   AND MONTH(create_date) = MONTH(@dteLastYear)
	   AND YEAR(create_date) = YEAR(@dteLastYear)
	   AND create_date <= @dteLastYear


	SELECT @intCampaignCountRelaunched = COUNT(event_id)
	  FROM @activationsPY
	 WHERE event_status_id = 3
	   AND MONTH(create_date) = MONTH(@dteLastYear)
	   AND YEAR(create_date) = YEAR(@dteLastYear)
	   AND create_date <= @dteLastYear


    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) +  'No campaigns were activated this month to date.'
    END
    ELSE
    BEGIN
    	
		IF @intCampaignCount = 1
		begin 
			IF @intCampaignCountNew = 1
    			SET @report = @report + char(13) + char(10) +  '1 new campaign was activated this month to date.'
			ELSE 
				SET @report = @report + char(13) + char(10) +  '1 relaunched campaign was activated this month to date.'
		end 
    	ELSE
    		SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns (' + CONVERT( VARCHAR(5), @intCampaignCountNew )+ ' new, ' + CONVERT( VARCHAR(5), @intCampaignCountRelaunched ) + ' relaunched) were activated this month to date.'
    END
    
    -- Yesterday's campaign activations
    SET @intCampaignCount = NULL
    SET @intCampaignCountNew = NULL
    SET @intCampaignCountRelaunched = NULL

	SELECT @intCampaignCount = COUNT(event_id)
	  FROM @activationsPY
	 WHERE DATEPART(DAY, create_date) = DATEPART(day, DATEADD( DAY, -1, @dteLastYear ))
	   AND DATEPART(MONTH, create_date) = DATEPART(month, DATEADD( day, -1, @dteLastYear))
	   AND DATEPART(year, create_date) = DATEPART(year, DATEADD(day, -1, @dteLastYear))


	SELECT @intCampaignCountNew = COUNT(event_id)
	  FROM @activationsPY
	 WHERE event_status_id = 1
	   AND DATEPART(DAY, create_date) = DATEPART(day, DATEADD( DAY, -1, @dteLastYear ))
	   AND DATEPART(MONTH, create_date) = DATEPART(month, DATEADD( day, -1, @dteLastYear))
	   AND DATEPART(year, create_date) = DATEPART(year, DATEADD(day, -1, @dteLastYear))

    
	SELECT @intCampaignCountRelaunched = COUNT(event_id)
	  FROM @activationsPY
	 WHERE event_status_id = 3
	   AND DATEPART(DAY, create_date) = DATEPART(day, DATEADD( DAY, -1, @dteLastYear ))
	   AND DATEPART(MONTH, create_date) = DATEPART(month, DATEADD( day, -1, @dteLastYear))
	   AND DATEPART(year, create_date) = DATEPART(year, DATEADD(day, -1, @dteLastYear))


    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) +  'No campaigns were activated yesterday.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
		begin 
			IF @intCampaignCountNew = 1
    			SET @report = @report + char(13) + char(10) +  '1 new campaign was activated yesterday.'
			ELSE 
				SET @report = @report + char(13) + char(10) +  '1 relaunched campaign was activated yesterday.'
		end 
    	ELSE
    		SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns (' + CONVERT( VARCHAR(5), @intCampaignCountNew )+ ' new, ' + CONVERT( VARCHAR(5), @intCampaignCountRelaunched ) + ' relaunched) were activated yesterday.'
	END
        
    -- Today's campaign activations
    SET @intCampaignCount = NULL
    SET @intCampaignCountNew = NULL
    SET @intCampaignCountRelaunched = NULL

	SELECT @intCampaignCount = COUNT(event_id)
	  FROM @activationsPY
	 WHERE DATEPART(DAY, create_date) = DATEPART(day, @dteLastYear)
	   AND DATEPART(MONTH, create_date) = DATEPART(month, @dteLastYear)
	   AND DATEPART(year, create_date) = DATEPART(year, @dteLastYear)


	SELECT @intCampaignCountNew = COUNT(event_id)
	  FROM @activationsPY
	 WHERE event_status_id = 1
	   AND DATEPART(DAY, create_date) = DATEPART(day, @dteLastYear)
	   AND DATEPART(MONTH, create_date) = DATEPART(month, @dteLastYear)
	   AND DATEPART(year, create_date) = DATEPART(year, @dteLastYear)

	SELECT @intCampaignCountRelaunched = COUNT(event_id)
	  FROM @activationsPY
	 WHERE event_status_id = 3
	   AND DATEPART(DAY, create_date) = DATEPART(day, @dteLastYear)
	   AND DATEPART(MONTH, create_date) = DATEPART(month, @dteLastYear)
	   AND DATEPART(year, create_date) = DATEPART(year, @dteLastYear)


    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) +  'No campaigns were activated today.'
    END
    ELSE
    BEGIN
		IF @intCampaignCount = 1
		begin 
			IF @intCampaignCountNew = 1
    			SET @report = @report + char(13) + char(10) +  '1 new campaign was activated today.'
			ELSE 
				SET @report = @report + char(13) + char(10) +  '1 relaunched campaign was activated today.'
		end 
    	ELSE
    		SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns (' + CONVERT( VARCHAR(5), @intCampaignCountNew )+ ' new, ' + CONVERT( VARCHAR(5), @intCampaignCountRelaunched ) + ' relaunched) were activated today.'
    END
    

-- ============================
Print '-- Campaign Kickoffs'
-- ============================

    SET @report = @report + char(13) + char(10) +  ''
    SET @report = @report + char(13) + char(10) +  '==============================='
    SET @report = @report + char(13) + char(10) +  '       Campaign Kickoffs'
    SET @report = @report + char(13) + char(10) +  '==============================='
    SET @report = @report + char(13) + char(10) +  ''
    

    -- This month's campaign kick-offs
    SET @intCampaignCount = NULL
    SET @intImportCount = NULL 
    SET @intManualCount = NULL 
    SET @intRosterCount = NULL 

    SELECT
    	@intCampaignCount = COUNT(v.event_id ) 
    	, @intImportCount = SUM(KOImport)
    	, @intManualCount = SUM(KOManual)
    	, @intRosterCount = SUM(KORoster)
    FROM (        
        SELECT 
            e.event_id
            , COUNT(ep.event_participation_id) as participation_count
			, SUM(case when mh.creation_channel_id IN(38, 23,46) then 1 else 0 end) as KOImport
			, SUM(case when mh.creation_channel_id IN(29, 7) then 1 else 0 end) as KOManual
			, SUM(case when mh.creation_channel_id IN(20) then 1 else 0 end) as KORoster
            , MIN (ep.create_date) as create_date
        FROM event as e with(nolock)
            INNER JOIN event_participation as ep with(nolock)
                ON ep.event_id = e.event_id
            INNER JOIN member_hierarchy as mh with(nolock)
                ON mh.member_hierarchy_id = ep.member_hierarchy_id
            INNER JOIN member as m with(nolock)
                ON m.member_id = mh.member_id
         WHERE ( m.email_address is not null
                 and m.email_address not like '%@efundraising.com')
           AND mh.creation_channel_id IN (7,20,23,38,29,23,46)
         GROUP BY e.event_id
    ) v
    WHERE
    	( MONTH( v.create_date ) = MONTH( @dteToday ) )
     AND	( YEAR( v.create_date ) = YEAR( @dteToday ) )
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) +  'No campaigns were kicked-off this month.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    	BEGIN
    		SET @report = @report + char(13) + char(10) +  CONVERT( CHAR(1), @intCampaignCount ) + '1 campaign was kicked-off this month.'
    		IF @intImportCount > 0 
    			SET @report = @report  + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount )
    		IF @intManualCount > 0 
    			SET @report = @report  + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intManualCount )
    		IF @intRosterCount > 0   
    			SET @report = @report  + char(13) + char(10) +  '     ' +  CONVERT( VARCHAR(5), @intRosterCount )
    	END
    	ELSE
    	BEGIN
    		SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were kicked-off this month.'
    	
    		IF @intImportCount > 0 and @intManualCount > 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual and ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
    		IF @intImportCount > 0 and @intManualCount > 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import and ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
     		IF @intImportCount > 0 and @intManualCount <= 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import emails.'
    		IF @intImportCount <= 0 and @intManualCount > 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) + '     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
    		IF @intImportCount <= 0 and @intManualCount > 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) + '     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual and ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
	  		IF @intImportCount <= 0 and @intManualCount <= 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
		END 
   END

        
    -- This week's campaign kick-offs
    SET @intCampaignCount = NULL
    SET @intImportCount = NULL 
    SET @intManualCount = NULL 
    SET @intRosterCount = NULL 

    SELECT
    	@intCampaignCount = COUNT(v.event_id ) 
    	, @intImportCount = SUM(KOImport)
    	, @intManualCount = SUM(KOManual)
    	, @intRosterCount = SUM(KORoster)
	FROM (
    	SELECT 
            e.event_id
            , COUNT(ep.event_participation_id) as participation_count
			, SUM(case when mh.creation_channel_id IN(38, 23,46) then 1 else 0 end) as KOImport
			, SUM(case when mh.creation_channel_id IN(29, 7) then 1 else 0 end) as KOManual
			, SUM(case when mh.creation_channel_id IN(20) then 1 else 0 end) as KORoster
            , MIN (ep.create_date) as create_date
        FROM event as e with(nolock)
            INNER JOIN event_participation as ep with(nolock)
                ON ep.event_id = e.event_id
            INNER JOIN member_hierarchy as mh with(nolock)
                ON mh.member_hierarchy_id = ep.member_hierarchy_id
            INNER JOIN member as m with(nolock)
                ON m.member_id = mh.member_id
         WHERE ( m.email_address is not null
                 and m.email_address not like '%@efundraising.com')
           AND mh.creation_channel_id IN (7,20,23,38,29,23,46)
         GROUP BY e.event_id
    ) v
    WHERE
        v.create_date BETWEEN DATEADD(day, -DATEPART(dw, @dteToday) + 1, @dteToday) AND @now
        AND YEAR( v.create_date ) = YEAR( @dteToday ) 
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) +  'No campaigns were kicked-off this week.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    	BEGIN
    		SET @report = @report + char(13) + char(10) +  '1 campaign was kicked-off this week.'
    		IF @intImportCount > 0 
    			SET @report = @report  + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount )
    		IF @intManualCount > 0 
    			SET @report = @report  + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intManualCount )
    		IF @intRosterCount > 0   
    			SET @report = @report  + char(13) + char(10) +  '     ' +  CONVERT( VARCHAR(5), @intRosterCount )
    	END
    	ELSE
    	BEGIN
    		SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were kicked-off this week.'
    		IF @intImportCount > 0 and @intManualCount > 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual and ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
    		IF @intImportCount > 0 and @intManualCount > 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import and ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
     		IF @intImportCount > 0 and @intManualCount <= 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import emails.'
    		IF @intImportCount <= 0 and @intManualCount > 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) + '     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
    		IF @intImportCount <= 0 and @intManualCount > 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) + '     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual and ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
	  		IF @intImportCount <= 0 and @intManualCount <= 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
		END
   END

        
    -- Yesterday's campaign kick-offs
    SET @intCampaignCount = NULL
    SET @intImportCount = NULL 
    SET @intManualCount = NULL 
    SET @intRosterCount = NULL 
    SELECT
    	@intCampaignCount = COUNT(v.event_id ) 
    	, @intImportCount = SUM(KOImport)
    	, @intManualCount = SUM(KOManual)
    	, @intRosterCount = SUM(KORoster) 
    FROM (
    	SELECT 
            e.event_id
            , COUNT(ep.event_participation_id) as participation_count
			, SUM(case when mh.creation_channel_id IN(38, 23,46) then 1 else 0 end) as KOImport
			, SUM(case when mh.creation_channel_id IN(29, 7) then 1 else 0 end) as KOManual
			, SUM(case when mh.creation_channel_id IN(20) then 1 else 0 end) as KORoster
            , MIN (ep.create_date) as create_date
        FROM event as e with(nolock)
            INNER JOIN event_participation as ep with(nolock)
                ON ep.event_id = e.event_id
            INNER JOIN member_hierarchy as mh with(nolock)
                ON mh.member_hierarchy_id = ep.member_hierarchy_id
            INNER JOIN member as m with(nolock)
                ON m.member_id = mh.member_id
         WHERE ( m.email_address is not null
                 and m.email_address not like '%@efundraising.com')
           AND mh.creation_channel_id IN (7,20,23,38,29,23,46)
         GROUP BY e.event_id
    ) v
    WHERE
          DATEPART(year, v.create_date) = DATEPART(year, DATEADD(day, -1, @dteToday))
      AND DATEPART(month, v.create_date) = DATEPART(month, DATEADD(day, -1, @dteToday))
      AND DATEPART(day, v.create_date) = DATEPART(day, DATEADD(day, -1, @dteToday))
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) +  'No campaigns were kicked-off yesterday.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    	BEGIN
    		SET @report = @report + char(13) + char(10) +  '1 campaign was kicked-off yesterday.'
    		IF @intImportCount > 0 
    			SET @report = @report  + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount )
    		IF @intManualCount > 0 
    			SET @report = @report  + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intManualCount )
    		IF @intRosterCount > 0   
    			SET @report = @report  + char(13) + char(10) +  '     ' +  CONVERT( VARCHAR(5), @intRosterCount )
    	END
    	ELSE
    	BEGIN
    		SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were kicked-off yesterday.'
    	
     		IF @intImportCount > 0 and @intManualCount > 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual and ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
    		IF @intImportCount > 0 and @intManualCount > 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import and ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
     		IF @intImportCount > 0 and @intManualCount <= 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import emails.'
    		IF @intImportCount <= 0 and @intManualCount > 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) + '     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
    		IF @intImportCount <= 0 and @intManualCount > 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) + '     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual and ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
	  		IF @intImportCount <= 0 and @intManualCount <= 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
		END
  END
    
   -- Today's campaign kick-offs per channel
    SET @intCampaignCount = NULL
    SET @intImportCount = NULL 
    SET @intManualCount = NULL 
    SET @intRosterCount = NULL 
    
    SELECT
    	@intCampaignCount = COUNT(v.event_id ) 
    	, @intImportCount = SUM(KOImport)
    	, @intManualCount = SUM(KOManual)
    	, @intRosterCount = SUM(KORoster)
    FROM (
    	SELECT 
            e.event_id
            , COUNT(ep.event_participation_id) as participation_count
			, SUM(case when mh.creation_channel_id IN(38, 23,46) then 1 else 0 end) as KOImport
			, SUM(case when mh.creation_channel_id IN(29, 7) then 1 else 0 end) as KOManual
			, SUM(case when mh.creation_channel_id IN(20) then 1 else 0 end) as KORoster
			, MIN (ep.create_date) as create_date
        FROM event as e with(nolock)
            INNER JOIN event_participation as ep with(nolock)
                ON ep.event_id = e.event_id
            INNER JOIN member_hierarchy as mh with(nolock)
                ON mh.member_hierarchy_id = ep.member_hierarchy_id
            INNER JOIN member as m with(nolock)
                ON m.member_id = mh.member_id
         WHERE ( m.email_address is not null
                 and m.email_address not like '%@efundraising.com')
           AND mh.creation_channel_id IN (7,20,23,38,29,23,46)
         GROUP BY e.event_id
    ) v
    WHERE
        DATEPART(year, v.create_date) = DATEPART(year, @dteToday)
    AND DATEPART(month, v.create_date) = DATEPART(month, @dteToday)
    AND DATEPART(day, v.create_date) = DATEPART(day, @dteToday)
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) +  'No campaigns were kicked-off today.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    	BEGIN
    		SET @report = @report + char(13) + char(10) +  '1 campaign was kicked-off today.'
     		IF @intImportCount > 0 
    			SET @report = @report  + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount )
    		IF @intManualCount > 0 
    			SET @report = @report  + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intManualCount )
    		IF @intRosterCount > 0   
    			SET @report = @report  + char(13) + char(10) +  '     ' +  CONVERT( VARCHAR(5), @intRosterCount )
    	END
   	ELSE
    	BEGIN
    		SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were kicked-off today.'
      		IF @intImportCount > 0 and @intManualCount > 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual and ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
    		IF @intImportCount > 0 and @intManualCount > 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import and ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
     		IF @intImportCount > 0 and @intManualCount <= 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import emails.'
    		IF @intImportCount <= 0 and @intManualCount > 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) + '     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
    		IF @intImportCount <= 0 and @intManualCount > 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) + '     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual and ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
	  		IF @intImportCount <= 0 and @intManualCount <= 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
		END
 		
    END
    
    



    SET @report = @report + char(13) + char(10) +  ''
    SET @report = @report + char(13) + char(10) +  '==============================='
    SET @report = @report + char(13) + char(10) +  '  Last Year Campaign Kickoffs'
    SET @report = @report + char(13) + char(10) +  '==============================='
    SET @report = @report + char(13) + char(10) +  ''
    

    -- This month's campaign kick-offs
    SET @intCampaignCount = NULL
    SET @intImportCount = NULL 
    SET @intManualCount = NULL 
    SET @intRosterCount = NULL 

    SELECT
    	@intCampaignCount = COUNT(v.event_id ) 
    	, @intImportCount = SUM(KOImport)
    	, @intManualCount = SUM(KOManual)
    	, @intRosterCount = SUM(KORoster)
    FROM (        
        SELECT 
            e.event_id
            , COUNT(ep.event_participation_id) as participation_count
			, SUM(case when mh.creation_channel_id IN(38, 23,46) then 1 else 0 end) as KOImport
			, SUM(case when mh.creation_channel_id IN(29, 7) then 1 else 0 end) as KOManual
			, SUM(case when mh.creation_channel_id IN(20) then 1 else 0 end) as KORoster
            , MIN (ep.create_date) as create_date
        FROM event as e with(nolock)
            INNER JOIN event_participation as ep with(nolock)
                ON ep.event_id = e.event_id
            INNER JOIN member_hierarchy as mh with(nolock)
                ON mh.member_hierarchy_id = ep.member_hierarchy_id
            INNER JOIN member as m with(nolock)
                ON m.member_id = mh.member_id
         WHERE ( m.email_address is not null
                 and m.email_address not like '%@efundraising.com')
           AND mh.creation_channel_id IN (7,20,23,38,29,23,46)
         GROUP BY e.event_id
    ) v
    WHERE
    	( MONTH( v.create_date ) = MONTH(DATEADD(year, -1,  @dteToday )) )
     AND	( YEAR( v.create_date ) = YEAR( DATEADD(year, -1, @dteToday) ) )
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) +  'No campaigns were kicked-off this month.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    	BEGIN
    		SET @report = @report + char(13) + char(10) +  CONVERT( CHAR(1), @intCampaignCount ) + '1 campaign was kicked-off this month.'
     		IF @intImportCount > 0 
    			SET @report = @report  + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount )
    		IF @intManualCount > 0 
    			SET @report = @report  + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intManualCount )
    		IF @intRosterCount > 0   
    			SET @report = @report  + char(13) + char(10) +  '     ' +  CONVERT( VARCHAR(5), @intRosterCount )
    	END
   	ELSE
    	BEGIN
    		SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were kicked-off this month.'
    		
    		IF @intImportCount > 0 and @intManualCount > 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual and ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
    		IF @intImportCount > 0 and @intManualCount > 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import and ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
     		IF @intImportCount > 0 and @intManualCount <= 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import emails.'
    		IF @intImportCount <= 0 and @intManualCount > 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) + '     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
    		IF @intImportCount <= 0 and @intManualCount > 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) + '     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual and ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
	  		IF @intImportCount <= 0 and @intManualCount <= 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
		END

    END

        
    -- This week's campaign kick-offs
    SET @intCampaignCount = NULL
    SET @intImportCount = NULL 
    SET @intManualCount = NULL 
    SET @intRosterCount = NULL 

    SELECT
    	@intCampaignCount = COUNT(v.event_id ) 
    	, @intImportCount = SUM(KOImport)
    	, @intManualCount = SUM(KOManual)
    	, @intRosterCount = SUM(KORoster)
    FROM (
    	SELECT 
            e.event_id
            , COUNT(ep.event_participation_id) as participation_count
			, SUM(case when mh.creation_channel_id IN(38, 23,46) then 1 else 0 end) as KOImport
			, SUM(case when mh.creation_channel_id IN(29, 7) then 1 else 0 end) as KOManual
			, SUM(case when mh.creation_channel_id IN(20) then 1 else 0 end) as KORoster
            , MIN (ep.create_date) as create_date
        FROM event as e with(nolock)
            INNER JOIN event_participation as ep with(nolock)
                ON ep.event_id = e.event_id
            INNER JOIN member_hierarchy as mh with(nolock)
                ON mh.member_hierarchy_id = ep.member_hierarchy_id
            INNER JOIN member as m with(nolock)
                ON m.member_id = mh.member_id
         WHERE ( m.email_address is not null
                 and m.email_address not like '%@efundraising.com')
           AND mh.creation_channel_id IN (7,20,23,38,29,23,46)
         GROUP BY e.event_id
    ) v
    WHERE
        v.create_date BETWEEN DATEADD(day, -DATEPART(dw, DATEADD(year, -1, @dteToday)) + 1, DATEADD(year, -1, @dteToday)) AND DATEADD(year, -1, @now)
        AND YEAR( v.create_date ) = YEAR( DATEADD(year, -1, @dteToday )) 
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) +  'No campaigns were kicked-off this week.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    	BEGIN
    		SET @report = @report + char(13) + char(10) +  '1 campaign was kicked-off this week.'
     		IF @intImportCount > 0 
    			SET @report = @report  + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount )
    		IF @intManualCount > 0 
    			SET @report = @report  + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intManualCount )
    		IF @intRosterCount > 0   
    			SET @report = @report  + char(13) + char(10) +  '     ' +  CONVERT( VARCHAR(5), @intRosterCount )
    	END
   	ELSE
    	BEGIN
    		SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were kicked-off this week.'
      		IF @intImportCount > 0 and @intManualCount > 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual and ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
    		IF @intImportCount > 0 and @intManualCount > 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import and ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
     		IF @intImportCount > 0 and @intManualCount <= 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import emails.'
    		IF @intImportCount <= 0 and @intManualCount > 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) + '     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
    		IF @intImportCount <= 0 and @intManualCount > 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) + '     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual and ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
	  		IF @intImportCount <= 0 and @intManualCount <= 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
		END

   END

        
    -- Yesterday's campaign kick-offs
    SET @intCampaignCount = NULL
    SET @intImportCount = NULL 
    SET @intManualCount = NULL 
    SET @intRosterCount = NULL 

    SELECT
    	@intCampaignCount = COUNT(v.event_id ) 
    	, @intImportCount = SUM(KOImport)
    	, @intManualCount = SUM(KOManual)
    	, @intRosterCount = SUM(KORoster)
    FROM (
    	SELECT 
            e.event_id
            , COUNT(ep.event_participation_id) as participation_count
			, SUM(case when mh.creation_channel_id IN(38, 23,46) then 1 else 0 end) as KOImport
			, SUM(case when mh.creation_channel_id IN(29, 7) then 1 else 0 end) as KOManual
			, SUM(case when mh.creation_channel_id IN(20) then 1 else 0 end) as KORoster
            , MIN (ep.create_date) as create_date
        FROM event as e with(nolock)
            INNER JOIN event_participation as ep with(nolock)
                ON ep.event_id = e.event_id
            INNER JOIN member_hierarchy as mh with(nolock)
                ON mh.member_hierarchy_id = ep.member_hierarchy_id
            INNER JOIN member as m with(nolock)
                ON m.member_id = mh.member_id
         WHERE ( m.email_address is not null
                 and m.email_address not like '%@efundraising.com')
           AND mh.creation_channel_id IN (7,20,23,38,29,23,46)
         GROUP BY e.event_id
    ) v
    WHERE
          DATEPART(year, v.create_date) = DATEPART(year, DATEADD(year, -1, DATEADD(day, -1, @dteToday)))
      AND DATEPART(month, v.create_date) = DATEPART(month, DATEADD(year, -1, DATEADD(day, -1, @dteToday)))
      AND DATEPART(day, v.create_date) = DATEPART(day, DATEADD(year, -1, DATEADD(day, -1, @dteToday)))
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) +  'No campaigns were kicked-off yesterday.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    	BEGIN
    		SET @report = @report + char(13) + char(10) +  '1 campaign was kicked-off yesterday.'
     		IF @intImportCount > 0 
    			SET @report = @report  + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount )
    		IF @intManualCount > 0 
    			SET @report = @report  + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intManualCount )
    		IF @intRosterCount > 0   
    			SET @report = @report  + char(13) + char(10) +  '     ' +  CONVERT( VARCHAR(5), @intRosterCount )
    	END
   	ELSE
    	BEGIN
    		SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were kicked-off yesterday.'
    		
    		IF @intImportCount > 0 and @intManualCount > 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual and ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
    		IF @intImportCount > 0 and @intManualCount > 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import and ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
     		IF @intImportCount > 0 and @intManualCount <= 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import emails.'
    		IF @intImportCount <= 0 and @intManualCount > 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) + '     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
    		IF @intImportCount <= 0 and @intManualCount > 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) + '     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual and ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
	  		IF @intImportCount <= 0 and @intManualCount <= 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
		END

    END
    
    -- Today's campaign kick-offs
    SET @intCampaignCount = NULL
    SET @intImportCount = NULL 
    SET @intManualCount = NULL 
    SET @intRosterCount = NULL 

    SELECT
    	@intCampaignCount = COUNT(v.event_id ) 
    	, @intImportCount = SUM(KOImport)
    	, @intManualCount = SUM(KOManual)
    	, @intRosterCount = SUM(KORoster)
    FROM (
    	SELECT 
            e.event_id
            , COUNT(ep.event_participation_id) as participation_count
			, SUM(case when mh.creation_channel_id IN(38, 23,46) then 1 else 0 end) as KOImport
			, SUM(case when mh.creation_channel_id IN(29, 7) then 1 else 0 end) as KOManual
			, SUM(case when mh.creation_channel_id IN(20) then 1 else 0 end) as KORoster
            , MIN (ep.create_date) as create_date
        FROM event as e with(nolock)
            INNER JOIN event_participation as ep with(nolock)
                ON ep.event_id = e.event_id
            INNER JOIN member_hierarchy as mh with(nolock)
                ON mh.member_hierarchy_id = ep.member_hierarchy_id
            INNER JOIN member as m with(nolock)
                ON m.member_id = mh.member_id
         WHERE ( m.email_address is not null
                 and m.email_address not like '%@efundraising.com')
           AND mh.creation_channel_id IN (7,20,23,38,29,23,46)
         GROUP BY e.event_id
    ) v
    WHERE
        DATEPART(year, v.create_date) = DATEPART(year, DATEADD(year, -1, @dteToday))
    AND DATEPART(month, v.create_date) = DATEPART(month, DATEADD(year, -1, @dteToday))
    AND DATEPART(day, v.create_date) = DATEPART(day, DATEADD(year, -1, @dteToday))
    
    IF @intCampaignCount IS NULL 
    BEGIN
    	SET @report = @report + char(13) + char(10) +  'No campaigns were kicked-off today.'
    END
    ELSE
    BEGIN
    	IF @intCampaignCount = 1
    	BEGIN
    		SET @report = @report + char(13) + char(10) +  '1 campaign was kicked-off today.'
     		IF @intImportCount > 0 
    			SET @report = @report  + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount )
    		IF @intManualCount > 0 
    			SET @report = @report  + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intManualCount )
    		IF @intRosterCount > 0   
    			SET @report = @report  + char(13) + char(10) +  '     ' +  CONVERT( VARCHAR(5), @intRosterCount )
    	END
   	ELSE
    	BEGIN
    		SET @report = @report + char(13) + char(10) +  CONVERT( VARCHAR(5), @intCampaignCount ) + ' campaigns were kicked-off today.'
     		IF @intImportCount > 0 and @intManualCount > 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual and ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
    		IF @intImportCount > 0 and @intManualCount > 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import and ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
     		IF @intImportCount > 0 and @intManualCount <= 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import emails.'
    		IF @intImportCount <= 0 and @intManualCount > 0 and @intRosterCount <= 0   
    			SET @report = @report + char(13) + char(10) + '     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
    		IF @intImportCount <= 0 and @intManualCount > 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) + '     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual and ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
	  		IF @intImportCount <= 0 and @intManualCount <= 0 and @intRosterCount > 0   
    			SET @report = @report + char(13) + char(10) +  '     ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
		END
   END
   
   
   
    SET @report = @report + char(13) + char(10) +  ''
    SET @report = @report + char(13) + char(10) +  '==============================='
    SET @report = @report + char(13) + char(10) +  '  Emails/Channel '
    SET @report = @report + char(13) + char(10) +  '==============================='
    SET @report = @report + char(13) + char(10) +  ''

    -- Today's sponsor emails 
    SET @intImportCount = NULL 
    SET @intManualCount = NULL 
    SET @intRosterCount = NULL 

     
	SELECT 
        @intImportCount = SUM(case when mh.creation_channel_id IN(38, 23,46)  then 1 else 0 end) -- KOImport
		, @intManualCount =  SUM(case when mh.creation_channel_id IN(29, 7) then 1 else 0 end) -- KOManual
        , @intRosterCount = SUM(case when mh.creation_channel_id IN(20) then 1 else 0 end)-- as KORoster
    FROM event_participation as ep 
        INNER JOIN member_hierarchy as mh with(nolock)
            ON mh.member_hierarchy_id = ep.member_hierarchy_id
        INNER JOIN member as m with(nolock)
            ON m.member_id = mh.member_id
			
    WHERE mh.creation_channel_id IN (7,20,23,38,29,23,46)
    AND DATEPART(year, ep.create_date) = DATEPART(year, @dteToday)
    AND DATEPART(month, ep.create_date) = DATEPART(month, @dteToday)
    AND DATEPART(day, ep.create_date) = DATEPART(day, @dteToday)
   
  
	SET @report = @report + char(13) + char(10) +  'Sponsor'
	IF @intImportCount > 0 and @intManualCount > 0 and @intRosterCount > 0   
		SET @report = @report + char(13) + char(10) +  'Today     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual and ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
	IF @intImportCount > 0 and @intManualCount > 0 and @intRosterCount <= 0   
		SET @report = @report + char(13) + char(10) +  'Today     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import and ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
	IF @intImportCount > 0 and @intManualCount <= 0 and @intRosterCount <= 0   
		SET @report = @report + char(13) + char(10) +  'Today     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import emails.'
	IF @intImportCount <= 0 and @intManualCount > 0 and @intRosterCount <= 0   
		SET @report = @report + char(13) + char(10) + 'Today     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
	IF @intImportCount <= 0 and @intManualCount > 0 and @intRosterCount > 0   
		SET @report = @report + char(13) + char(10) + 'Today     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual and ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
	IF @intImportCount <= 0 and @intManualCount <= 0 and @intRosterCount > 0   
		SET @report = @report + char(13) + char(10) +  'Today     ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'

       -- yesterday's sponsor emails 
    SET @intImportCount = NULL 
    SET @intManualCount = NULL 
    SET @intRosterCount = NULL 

     
	SELECT 
        @intImportCount = SUM(case when mh.creation_channel_id IN(38, 23,46)  then 1 else 0 end) -- KOImport
		, @intManualCount =  SUM(case when mh.creation_channel_id IN(29, 7) then 1 else 0 end) -- KOManual
        , @intRosterCount = SUM(case when mh.creation_channel_id IN(20) then 1 else 0 end)-- as KORoster
    FROM event_participation as ep 
        INNER JOIN member_hierarchy as mh with(nolock)
            ON mh.member_hierarchy_id = ep.member_hierarchy_id
        INNER JOIN member as m with(nolock)
            ON m.member_id = mh.member_id
			
    WHERE mh.creation_channel_id IN (7,20,23,38,29,23,46)
    AND DATEPART(year, ep.create_date) = DATEPART(year, DATEADD(dd, -1,@dteToday))
    AND DATEPART(month, ep.create_date) = DATEPART(month, DATEADD(dd,-1,@dteToday))
    AND DATEPART(day, ep.create_date) = DATEPART(day, DATEADD(dd,-1,@dteToday))
   
  
	--SET @report = @report + char(13) + char(10) +  'Sponsor'
	IF @intImportCount > 0 and @intManualCount > 0 and @intRosterCount > 0   
		SET @report = @report + char(13) + char(10) +  'Yesterday     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual and ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
	IF @intImportCount > 0 and @intManualCount > 0 and @intRosterCount <= 0   
		SET @report = @report + char(13) + char(10) +  'Yesterday     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import and ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
	IF @intImportCount > 0 and @intManualCount <= 0 and @intRosterCount <= 0   
		SET @report = @report + char(13) + char(10) +  'Yesterday     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import emails.'
	IF @intImportCount <= 0 and @intManualCount > 0 and @intRosterCount <= 0   
		SET @report = @report + char(13) + char(10) + 'Yesterday     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
	IF @intImportCount <= 0 and @intManualCount > 0 and @intRosterCount > 0   
		SET @report = @report + char(13) + char(10) + 'Yesterday     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual and ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'
	IF @intImportCount <= 0 and @intManualCount <= 0 and @intRosterCount > 0   
		SET @report = @report + char(13) + char(10) +  'Yesterday     ' + CONVERT( VARCHAR(5), @intRosterCount ) + ' roster emails.'

    
    
 
    
    -- Today's part emails 
    SET @intImportCount = NULL 
    SET @intManualCount = NULL 
   

    
	SELECT 
        @intImportCount = SUM(case when mh.creation_channel_id IN(14) then 1 else 0 end) -- KOImport
		, @intManualCount =  SUM(case when mh.creation_channel_id IN(12) then 1 else 0 end) -- KOManual
      
    FROM event_participation as ep 
        INNER JOIN member_hierarchy as mh with(nolock)
            ON mh.member_hierarchy_id = ep.member_hierarchy_id
        INNER JOIN member as m with(nolock)
            ON m.member_id = mh.member_id
			
    WHERE mh.creation_channel_id IN (14, 12)
    AND DATEPART(year, ep.create_date) = DATEPART(year, @dteToday)
    AND DATEPART(month, ep.create_date) = DATEPART(month, @dteToday)
    AND DATEPART(day, ep.create_date) = DATEPART(day, @dteToday)
   
  
    SET @report = @report + char(13) + char(10) +  ''
	SET @report = @report + char(13) + char(10) +  'Participant '
	IF @intImportCount > 0 and @intManualCount > 0   
		SET @report = @report + char(13) + char(10) +  'Today     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import and ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
	IF @intImportCount > 0 and @intManualCount <= 0   
		SET @report = @report + char(13) + char(10) +  'Today     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import emails.'
	IF @intImportCount <= 0 and @intManualCount > 0  
		SET @report = @report + char(13) + char(10) + 'Today     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 



   -- yesterday's part emails 
    SET @intImportCount = NULL 
    SET @intManualCount = NULL 
   

    
	SELECT 
        @intImportCount = SUM(case when mh.creation_channel_id IN(14) then 1 else 0 end) -- KOImport
		, @intManualCount =  SUM(case when mh.creation_channel_id IN(12) then 1 else 0 end) -- KOManual
      
    FROM event_participation as ep 
        INNER JOIN member_hierarchy as mh with(nolock)
            ON mh.member_hierarchy_id = ep.member_hierarchy_id
        INNER JOIN member as m with(nolock)
            ON m.member_id = mh.member_id
			
    WHERE mh.creation_channel_id IN (14, 12)
    AND DATEPART(year, ep.create_date) = DATEPART(year, DATEADD(dd,-1,@dteToday))
    AND DATEPART(month, ep.create_date) = DATEPART(month, DATEADD(dd,-1,@dteToday))
    AND DATEPART(day, ep.create_date) = DATEPART(day, DATEADD(dd,-1,@dteToday))
   
  
	--SET @report = @report + char(13) + char(10) +  'Participant '
	IF @intImportCount > 0 and @intManualCount > 0   
		SET @report = @report + char(13) + char(10) +  'Yesterday     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import and ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 
	IF @intImportCount > 0 and @intManualCount <= 0   
		SET @report = @report + char(13) + char(10) +  'Yesterday     ' + CONVERT( VARCHAR(5), @intImportCount ) + ' import emails.'
	IF @intImportCount <= 0 and @intManualCount > 0  
		SET @report = @report + char(13) + char(10) + 'Yesterday     ' + CONVERT( VARCHAR(5), @intManualCount ) + ' manual emails.' 

    
END
GO
