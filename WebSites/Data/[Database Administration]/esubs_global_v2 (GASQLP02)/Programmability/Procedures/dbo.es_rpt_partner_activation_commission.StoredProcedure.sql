USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_partner_activation_commission]    Script Date: 02/14/2014 13:06:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
-- =============================================
-- Author: Stephen Lim
-- Create Date: 2009-12-11
-- Description:	Partner activation commission report
-- 
-- Activation is defined as a new group that sold as least one magazine.
-- Fixed amount commission is calculated on pro-rated number of activations for the month.
--
-- e.g. Given a commission table below. Partner who has 70 unique activations this month is paid $49 for the first 1-49 range, 
-- and $42 for the remaining quantity.
--
-- [01-49]: $1
-- [50-99]: $2
-- 
-- =============================================
CREATE PROCEDURE [dbo].[es_rpt_partner_activation_commission]
    @start_date as datetime,
	@end_date as datetime,
	@partner_id as int
AS
BEGIN

    -- Force clock to end of day
	SET @end_date = CONVERT(varchar(30), @end_date, 101) + ' 23:59:59'

	-- Print header
	SELECT 
		'YEAR',
		'MONTH',
		'+ACTIVATIONS',
		'CURRENCY',
		'$TOTAL COMMISSION'

	-- Save into temp table because of a bug in SQL Server that croaks on subquery with aggregate columns
	-- http://support.microsoft.com/kb/290817
	--
	-- Find only *first* sales that occured after @start_date and before @end_date
	--
	-- YEAR	MTH	GROUP_ID
	-- 2008	1	123456
	-- 2008	1	123456
	--
	SELECT 
		YEAR(MIN(o.order_date)) AS 'Year',
		MONTH(MIN(o.order_date)) AS 'Month',
		g.group_id
	INTO #Temp_Commission
	FROM QSPEcommerce.dbo.efundraisingtransaction et
		INNER JOIN event_participation ep ON ep.event_participation_id = et.suppid
		INNER JOIN event_group eg ON eg.event_id = ep.event_id
		INNER JOIN [group] g ON g.group_id = eg.group_id AND 
						-- HACK: Merge Fundsnet (147) and fundsnet (107) in condition until data is merged.
						(g.partner_id = @partner_id OR g.partner_id = CASE WHEN @partner_id = 147 THEN 107 ELSE NULL END)

		-- Must have sold Magazine or Frozen food (cookies) product type only
		INNER JOIN [QSPFulfillment].[dbo].[order] o ON o.order_id = et.orderid AND o.order_date <= @end_date
		INNER JOIN [dbo].es_get_valid_order_status() os ON os.order_status_id = o.order_status_id
		INNER JOIN [QSPFulfillment].[dbo].[order_detail] od ON od.order_id = o.order_id
		INNER JOIN [QSPFulfillment].[dbo].[catalog_item_detail] as cid ON od.catalog_item_detail_id = cid.catalog_item_detail_id
		INNER JOIN [QSPFulfillment].[dbo].[catalog_item] as ci ON cid.catalog_item_id = ci.catalog_item_id
		INNER JOIN [QSPFulfillment].[dbo].[Product] p ON ci.product_id = p.Product_id AND (p.product_type_id = 1 OR p.product_type_id = 6)
		INNER JOIN [QSPFulfillment].[dbo].[Product_Type] pt ON p.product_type_id = pt.product_type_id 
	GROUP BY g.group_id
	HAVING MIN(o.order_date) >= @start_date
	ORDER BY YEAR(MIN(o.order_date)), MONTH(MIN(o.order_date)), g.group_id


	-- Count the number of activations per date range
	SELECT 
		[Year],
		[Month],
		COUNT(*) AS Activations
	INTO #Temp_Commission2
	FROM #Temp_Commission
	GROUP BY [Year], [Month]
	ORDER BY [Year], [Month]


	-- Calculate commission
	--
	-- The following query returns a matrix of Activation X Commission table:
	--
	-- YEAR	MTH	ACTIVATIONS	MIN	MAX	COMMISSION DATE			F_AMT
	-- 2008	1	2			1	1	2008-01-01 00:00:00.000	1.1000
	-- 2008	1	2			2	99	2008-01-01 00:00:00.000	1.2000
	SELECT 
		tc2.[Year], 
		tc2.[Month], 
		tc2.Activations,
		pcr.MinThresholdValue, 
		pcr.MaxThresholdValue, 
		pac.Effective_Date, 
		pac.Fixed_Amount, 
		CASE 
			WHEN tc2.Activations > pcr.MaxThresholdValue 
				THEN (pcr.MaxThresholdValue - pcr.MinThresholdValue + 1) * pac.Fixed_Amount
			WHEN tc2.Activations BETWEEN pcr.MinThresholdValue AND pcr.MaxThresholdValue 
				THEN (tc2.Activations - pcr.MinThresholdValue + 1) * pac.Fixed_Amount
			ELSE 0
		END AS Commission
	INTO #Temp_Commission3
	FROM #Temp_Commission2 tc2

		-- Get earliest matching commission rate. 
		-- Commission Effective_Date should always begin on the first day of the month.
		-- The following join will return duplicate rows, one for each matching range.
	    INNER JOIN Partner_Activation_Commission pac ON
			pac.Partner_ID = @partner_id AND Active = 1 AND 
			pac.Effective_Date = 
				(
					SELECT MAX(Effective_Date) FROM Partner_Activation_Commission 
					WHERE Partner_ID = @partner_id AND YEAR(Effective_Date) <= tc2.[Year] AND MONTH(Effective_Date) <= tc2.[Month] AND Active = 1
				)
		INNER JOIN Partner_Commission_Range pcr ON pcr.Partner_Commission_Range_ID = pac.Partner_Commission_Range_ID		
	GROUP BY 
		tc2.[Year], 
		tc2.[Month], 
		tc2.Activations,
		pcr.MinThresholdValue, 
		pcr.MaxThresholdValue, 
		pac.Effective_Date, 
		pac.Fixed_Amount
	HAVING tc2.Activations > pcr.MaxThresholdValue OR tc2.Activations BETWEEN pcr.MinThresholdValue AND pcr.MaxThresholdValue


	-- Print data. 
	-- Note the quantity should be taken as a singular item because the last join with Partner_Sales_Commission
	-- returns duplicate rows, one for each matching commission range. The group by and min/sum functions are used to
	-- return one entry per year and month from the exploded matrix.
	SELECT 
		[Year], 
		DATENAME(MONTH, '2009-' + CONVERT(VARCHAR, [Month]) + '-01'),
		MIN(Activations) AS 'Activations', 
		'$ US',
		SUM(Commission) AS 'Commission' 
	FROM #Temp_Commission3
	GROUP BY [Year], [Month], DATENAME(MONTH, '2009-' + CONVERT(VARCHAR, [Month]) + '-01')
	ORDER BY [Year], [Month]


	-- Clean up
	DROP TABLE #Temp_Commission
	DROP TABLE #Temp_Commission2
	DROP TABLE #Temp_Commission3

END
GO
