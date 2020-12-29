USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_partner_sales_commission]    Script Date: 02/14/2014 13:06:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Stephen Lim
-- Create Date: 2009-12-11
-- Description:	Partner sales commission report
-- 
-- Fixed amount commission is calculated on pro-rated quantity.
-- Variable rate commission is calculated on total sales of the month multiplied by the range sold.
--
-- e.g. Given a commission table below. Partner who sold 70 magazines (total sales of $70) this month is paid $49 for the first 1-49 range, 
-- and $42 for the remaining quantity. In addition, partner receives 11% on the total sales (or $7.70) for the month having sold 70 magazines.
--
-- [01-49]: $1 and 10%
-- [50-99]: $2 and 11%
-- exec [es_rpt_partner_sales_commission] '2011-06-01','2011-06-30',665  -- 'Fundraiseralley'
-- =============================================
CREATE PROCEDURE [dbo].[es_rpt_partner_sales_commission]
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
		'PRODUCT',
		'+UNITS',
		'$SALES AMOUNT',
		'CURRENCY',
		'$TOTAL COMMISSION'

	-- Save into temp table because of a bug in SQL Server that croaks on subquery with aggregate columns
	-- http://support.microsoft.com/kb/290817
	--
	-- The following query returns a matrix of Sales X Commission table:
	--
	-- YEAR	MTH	PROD		QTY SALES	MIN	MAX	COMMISSION DATE			F_AMT	V_RATE	COMMISSION
	-- 2008	1	Magazine	3	70.00	1	1	2008-01-01 00:00:00.000	1.1000	0.1000	1.10000000
	-- 2008	1	Magazine	3	70.00	2	99	2008-01-01 00:00:00.000	1.2000	0.1000	9.40000000
	--
	SELECT 
		YEAR(sales.create_date) AS 'Year', 
		MONTH(sales.create_date) AS 'Month',
		sales.create_date AS order_date,
	    sales.product_type_desc AS 'Product_Type_Name',
	    SUM(sales.quantity) AS 'Quantity',
	    SUM(sales.sub_total) AS 'Sales_Amount',
		pcr.MinThresholdValue, 
		pcr.MaxThresholdValue, 
		psc.Effective_Date, 
		psc.Fixed_Amount, 
		psc.Variable_Rate,
		CASE 
			WHEN SUM(sales.quantity) > pcr.MaxThresholdValue 
				THEN (pcr.MaxThresholdValue - pcr.MinThresholdValue + 1) * psc.Fixed_Amount
			WHEN SUM(sales.quantity) BETWEEN pcr.MinThresholdValue AND pcr.MaxThresholdValue 
				THEN ((SUM(sales.quantity) - pcr.MinThresholdValue + 1) * psc.Fixed_Amount) + (SUM(sales.price * sales.quantity) * psc.Variable_Rate)
			ELSE 0
		END AS Commission
	INTO #Temp_Commission
	FROM es_get_valid_order_items() sales
		INNER JOIN event_participation ep ON ep.event_participation_id = sales.supp_id
        INNER JOIN event_group eg ON eg.event_id = ep.event_id
        INNER JOIN [group] g ON g.group_id = eg.group_id AND 
						-- HACK: Merge Fundsnet (147) and fundsnet (107) in condition until data is merged.
						(g.partner_id = @partner_id OR g.partner_id = CASE WHEN @partner_id = 147 THEN 107 ELSE NULL END)
		-- Get earliest matching commission rate. 
		-- Commission Effective_Date should always begin on the first day of the month.
		-- The following join will return duplicate rows, one for each matching range.
	    INNER JOIN Partner_Sales_Commission psc ON
			psc.Partner_ID = @partner_id AND psc.Product_Type_ID = p.product_type_id AND Active = 1  AND psc.store_id = sales.store_id
		INNER JOIN Partner_Commission_Range pcr ON pcr.Partner_Commission_Range_ID = psc.Partner_Commission_Range_ID		
	GROUP BY YEAR(o.order_date), MONTH(o.order_date),  o.order_date, pt.product_type_name, psc.Effective_Date, psc.Fixed_Amount, psc.Variable_Rate, pcr.MinThresholdValue, pcr.MaxThresholdValue
	HAVING (SUM(od.quantity) > pcr.MaxThresholdValue) OR (SUM(od.quantity) BETWEEN pcr.MinThresholdValue AND pcr.MaxThresholdValue)
	and psc.Effective_Date <= o.order_date

/*			SELECT MAX(Effective_Date)as Effective_Date, psc.Product_Type_ID FROM Partner_Sales_Commission 
				WHERE Partner_ID = @partner_id AND  AND Active = 1
		) edate on edate.Effective_Date = psc.Effective_Date and edate.Product_Type_ID = psc.Product_Type_ID*/

	-- Print data. 
	-- Note the quantity should be taken as a singular item because the last join with Partner_Sales_Commission
	-- returns duplicate rows, one for each matching commission range. The group by and min/sum functions are used to
	-- return one entry per year, month and product type from the exploded matrix.
	SELECT 
		 year(order_date) as year, 
		DATENAME(MONTH, '2009-' + CONVERT(VARCHAR, Month(order_date)) + '-01'), 
		product_type_name AS 'Product', 
		MIN(quantity) AS 'Quantity', 
		MIN(sales_amount) AS 'Sales_Amount', 
		'$ US',
		SUM(commission) AS 'Commission' 
	FROM #Temp_Commission
	GROUP BY year(order_date), month(order_date), DATENAME(MONTH, '2009-' + CONVERT(VARCHAR, month(order_date)) + '-01'), [Product_Type_Name]
	ORDER BY year(order_date), month(order_date), [Product_Type_Name]

	-- Clean up
	DROP TABLE #Temp_Commission

END
GO
