USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_esubs_hourly_total]    Script Date: 02/14/2014 13:06:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_rpt_esubs_hourly_total]
AS
BEGIN
	DECLARE @varProduct varchar(30)
    DECLARE @intTotalQuantity SMALLINT
    DECLARE @intOrderTotal DECIMAL(9,2)    
    DECLARE @intCumulativeQty SMALLINT    
    DECLARE @decCumulativeTotal DECIMAL(9,2)    
    DECLARE @dteStartingDate DATETIME
    DECLARE @intCampaignCount INT
    DECLARE @intParticipantCount INT
    DECLARE @intSupporterCount INT
    DECLARE @dteToday DATETIME
    DECLARE @now DATETIME

	DECLARE @report TABLE (
		line VARCHAR(1024)
	)


    SET NOCOUNT ON
    SET DATEFIRST 1
    SET @dteToday = GETDATE()
    --SET @dteToday = '2006-01-31'
    SET @dteToday = DATEADD(ms, -DATEPART(ms, @dteToday), @dteToday)
    SET @dteToday = DATEADD(s, -DATEPART(s, @dteToday), @dteToday)
    SET @dteToday = DATEADD(n, -DATEPART(n, @dteToday), @dteToday)
    SET @dteToday = DATEADD(hh, -DATEPART(hh, @dteToday), @dteToday)
    SET @now = GETDATE()

	-- header
	INSERT INTO @report
	SELECT 'eSubs Daily Report' 
	UNION ALL
	SELECT ''
	UNION ALL
	SELECT '==========================================================='
	UNION ALL
    SELECT '       Today''s sales '
    UNION ALL
	SELECT '==========================================================='

	-- Today's sales
	INSERT INTO @report
    SELECT 'Product Line: ' + pt.Product_Type_Name + char(13) + char(10) + 'Qty Sold: ' + CAST(SUM(od.quantity) AS VARCHAR(20)) + ', Revenue: $' + CAST(SUM(od.price*od.quantity) AS VARCHAR(20))
    /*
	  pt.Product_Type_Name
	, SUM(od.quantity) AS quantity
	, SUM(od.price*od.quantity)  AS total
	*/
    FROM QSPEcommerce.dbo.efundraisingtransaction as et
	INNER JOIN dbo.event_participation AS ep
		ON ep.event_participation_id = et.suppID
	INNER JOIN dbo.event AS e
	    ON e.event_id = ep.event_id
	INNER JOIN QSPFulfillment.dbo.[order] as o
		ON o.order_id = et.OrderID
    INNER JOIN QSPFulfillment.dbo.[order_detail] as od
        ON od.order_id = o.order_id
    INNER JOIN [QSPFulfillment].[dbo].[catalog_item_detail] as cid
        ON od.catalog_item_detail_id = cid.catalog_item_detail_id
    INNER JOIN [QSPFulfillment].[dbo].[catalog_item] as ci
        ON cid.catalog_item_id = ci.catalog_item_id
    INNER JOIN [QSPFulfillment].[dbo].[Product] p
        ON ci.product_id = p.Product_id
    INNER JOIN [QSPFulfillment].[dbo].[Product_Type] pt
        ON p.Product_Type_id = pt.Product_Type_id
    WHERE e.event_type_id in (1,3) 
      AND DATEPART(day, et.CreateDate) = DATEPART(day, @dteToday)
      AND DATEPART(month, et.CreateDate) = DATEPART(month, @dteToday)
      AND DATEPART(year, et.CREATEDate) = DATEPART(year, @dteToday)
    GROUP BY pt.Product_Type_Name
    HAVING SUM(od.price) IS NOT NULL

	
	SELECT line
	FROM @report
END
GO
