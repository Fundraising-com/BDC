USE [report_center_v2]
GO
/****** Object:  StoredProcedure [dbo].[efr_rpt_partner_confirmed_report]    Script Date: 02/14/2014 13:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE             procedure [dbo].[efr_rpt_partner_confirmed_report]
		@start_date as datetime 
		, @end_date as datetime
as


declare @end_date2 datetime
set @end_date2 = dateadd(hh, 24, @end_date)
set @end_date = dateadd(ss, -1, @end_date2)

select 'Year', 'Month', 'Partner_name', 'Product Class', 'Currency', '$Total Confirmed'

select 	year(s.confirmed_date) AS shipped_year
	, month(s.confirmed_date) AS shipped_month
	, pa.partner_name
	, pc.description
	, countries.currency_code 
	, sum(s.total_amount) AS sum_payment
FROM    lead l 
	INNER JOIN client c ON l.lead_id = c.lead_id 
	INNER JOIN sale s ON c.client_sequence_code = s.client_sequence_code AND c.client_id = s.client_id 
	INNER JOIN promotion p ON l.promotion_id = p.promotion_id 
	INNER JOIN partner pa ON p.partner_id = pa.partner_id 
	INNER JOIN sales_item si ON s.sales_id = si.sales_id 
	INNER JOIN scratch_book sb ON si.scratch_book_id = sb.scratch_book_id 
	INNER JOIN product_class pc ON sb.product_class_id = pc.product_class_id
	INNER JOIN client_address ca ON ca.client_id = c.client_id and ca.client_sequence_code= c.client_sequence_code and ca.address_type = 'BT'
	INNER JOIN countries on countries.country_code = ca.country_code
where s.confirmed_date between @start_date and @end_date
	and (si.sales_item_no = 1)
GROUP BY 
	MONTH(s.confirmed_date)
	, YEAR(s.confirmed_date)
	, pa.partner_name
	, pc.description
	, countries.currency_code 
	, si.sales_item_no
ORDER BY YEAR(s.confirmed_date), MONTH(s.confirmed_date)
GO
