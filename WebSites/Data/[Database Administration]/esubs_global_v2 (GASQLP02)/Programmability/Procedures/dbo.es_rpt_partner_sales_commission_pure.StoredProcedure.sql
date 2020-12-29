USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_partner_sales_commission_pure]    Script Date: 02/14/2014 13:06:57 ******/
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
--
-- exec [es_rpt_partner_sales_commission_pure] '2011-09-01','2011-09-30',58  -- 'Leaguelineup'
-- =============================================
CREATE PROCEDURE [dbo].[es_rpt_partner_sales_commission_pure]
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
	    sales.product_type_desc AS 'Product_Type_Name',
	    SUM(sales.quantity) AS 'Quantity',
	    SUM(sales.sub_total * sales.quantity) AS 'Sales_Amount',
		pcr.MinThresholdValue, 
		pcr.MaxThresholdValue, 
		psc.Effective_Date, 
		psc.Fixed_Amount, 
		psc.Variable_Rate,
		psc.Pure_Variable_Rate,
		(case 
			when fc.consultant_id is null then 1
			when fc.consultant_id  in (-1, 0, 936, 3481) then 1 
			when fc.is_fm = -1 then 0
			--when fc.is_active = -1 then 'OTHER' 
			when fc.is_agent = -1 then 0
			--when fc.phone_extension is null then 'Pure Online' 
			else 0 /*fc.name*/ end) as isPure

		, CASE 
			WHEN SUM(sales.quantity) > pcr.MaxThresholdValue 
				THEN (pcr.MaxThresholdValue - pcr.MinThresholdValue + 1) * psc.Fixed_Amount
			WHEN SUM(sales.quantity) BETWEEN pcr.MinThresholdValue AND pcr.MaxThresholdValue 
				THEN ((SUM(sales.quantity) - pcr.MinThresholdValue + 1) * psc.Fixed_Amount) + (SUM(sales.sub_total * sales.quantity) * psc.Variable_Rate)
			ELSE 0
		END AS Commission

		, CASE 
			WHEN SUM(sales.quantity) > pcr.MaxThresholdValue 
				THEN (pcr.MaxThresholdValue - pcr.MinThresholdValue + 1)  * psc.Fixed_Amount
			WHEN SUM(sales.quantity) BETWEEN pcr.MinThresholdValue AND pcr.MaxThresholdValue 
				THEN ((SUM(sales.quantity) - pcr.MinThresholdValue + 1) * psc.Fixed_Amount) --+ (SUM(od.price * od.quantity) * psc.Variable_Rate)
			ELSE 0
		END AS Commission_Fix_Amount
		, CASE 
			WHEN SUM(sales.quantity) > pcr.MaxThresholdValue 
				THEN 0 --(pcr.MaxThresholdValue - pcr.MinThresholdValue + 1) -- * psc.Fixed_Amount
			WHEN SUM(sales.quantity) BETWEEN pcr.MinThresholdValue AND pcr.MaxThresholdValue 
				THEN (SUM(sales.sub_total * sales.quantity)) --* psc.Variable_Rate)
			ELSE 0
		END AS Commissonable_Var_Amount

	INTO #Temp_Commission
	FROM dbo.es_get_valid_orders_items() sales
		INNER JOIN event_participation ep ON ep.event_participation_id = sales.supp_id
        INNER JOIN event_group eg ON eg.event_id = ep.event_id
        INNER JOIN [group] g ON g.group_id = eg.group_id AND 
						-- HACK: Merge Fundsnet (147) and fundsnet (107) in condition until data is merged.
						(g.partner_id = @partner_id OR g.partner_id = CASE WHEN @partner_id = 147 THEN 107 ELSE NULL END)
		-- Self reg get commissions only
		LEFT JOIN efundraisingprod.dbo.lead l on g.lead_id = l.lead_id 
		LEFT JOIN efundraisingprod.dbo.consultant fc on fc.consultant_id = l.consultant_id
		-- Get earliest matching commission rate. 
		-- Commission Effective_Date should always begin on the first day of the month.
		-- The following join will return duplicate rows, one for each matching range.
	    LEFT JOIN Partner_Sales_Commission psc 
	      ON psc.Partner_ID = @partner_id 
	     AND psc.Product_Type_ID = sales.product_type_id AND Active = 1  
	     AND psc.Store_ID = sales.Store_ID
		/*AND psc.Effective_Date = 
				(
					SELECT MAX(Effective_Date) FROM Partner_Sales_Commission 
					WHERE Partner_ID = @partner_id AND psc.Product_Type_ID = p.product_type_id AND Effective_Date <= o.order_date AND Active = 1
				)*/
		LEFT JOIN Partner_Commission_Range pcr ON pcr.Partner_Commission_Range_ID = psc.Partner_Commission_Range_ID		
	WHERE  sales.create_date BETWEEN @start_date AND @end_date
	GROUP BY YEAR(sales.create_date), MONTH(sales.create_date), sales.product_type_desc, psc.Effective_Date, psc.Fixed_Amount, psc.Variable_Rate,psc.Pure_Variable_Rate, pcr.MinThresholdValue, pcr.MaxThresholdValue
		, (case 
			when fc.consultant_id is null then 1
			when fc.consultant_id  in (-1, 0, 936, 3481) then 1 
			when fc.is_fm = -1 then 0
			--when fc.is_active = -1 then 'OTHER' 
			when fc.is_agent = -1 then 0
			--when fc.phone_extension is null then 'Pure Online' 
			else 0 /*fc.name*/ end)

	HAVING (SUM(sales.quantity) > pcr.MaxThresholdValue) OR (SUM(sales.quantity) BETWEEN pcr.MinThresholdValue AND pcr.MaxThresholdValue)



--select * from #Temp_Commission

	-- Print data. 
	-- Note the quantity should be taken as a singular item because the last join with Partner_Sales_Commission
	-- returns duplicate rows, one for each matching commission range. The group by and min/sum functions are used to
	-- return one entry per year, month and product type from the exploded matrix.
	SELECT 
		[Year], 
		DATENAME(MONTH, '2009-' + CONVERT(VARCHAR, [Month]) + '-01'), 
		product_type_name AS 'Product', 
		SUM(quantity) AS 'Quantity', 
		SUM(sales_amount) AS 'Sales_Amount', 
		'$ US',
		SUM(case when isPure = 1 AND Pure_variable_rate IS NOT NULL then (Commissonable_Var_Amount * Pure_variable_rate) + Commission_Fix_Amount
			when isPure = 0 AND Pure_variable_rate IS NOT NULL then (Commissonable_Var_Amount * variable_rate) + Commission_Fix_Amount
			else (Commissonable_Var_Amount * variable_rate) + Commission_Fix_Amount end ) AS 'Commission' 
	FROM #Temp_Commission
	GROUP BY [Year], [Month], DATENAME(MONTH, '2009-' + CONVERT(VARCHAR, [Month]) + '-01'), [Product_Type_Name]
	ORDER BY [Year], [Month], [Product_Type_Name]

	-- Clean up
	DROP TABLE #Temp_Commission

END
GO
