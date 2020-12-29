USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efr_rpt_partner_payment_report]    Script Date: 02/14/2014 13:03:29 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
--exec efr_rpt_partner_payment_report '2006-06-01', '2006-06-30 23:59:59'

CREATE             procedure [dbo].[efr_rpt_partner_payment_report]
		@start_date as datetime 
		, @end_date as datetime
as


declare @end_date2 varchar(30)
set @end_date2 = convert(varchar(30), @end_date, 101) + ' 23:59:59'
set @end_date = convert(datetime, @end_date2)


select 'Year', 'Month', 'Partner_name', 'Product Class', 'Currency', '$Total Payment'

select 	year(pay.payment_entry_date) AS payment_year
	, month(pay.payment_entry_date) AS payment_month
	, pa.partner_name
	, pc.description
	, countries.currency_code 
	, sum(pay.payment_amount) AS sum_payment
FROM    lead l 
	INNER JOIN client c ON l.lead_id = c.lead_id 
	INNER JOIN sale s ON c.client_sequence_code = s.client_sequence_code AND c.client_id = s.client_id 
	INNER JOIN payment pay ON s.sales_id = pay.sales_id 
	INNER JOIN promotion p ON l.promotion_id = p.promotion_id 
	INNER JOIN partner pa ON p.partner_id = pa.partner_id 
	INNER JOIN (select distinct sales_id, min(sales_item_no) as sales_item_no from sales_item group by sales_id) si1 on s.sales_id = si1.sales_id 
	inner join sales_item si ON si1.sales_id = si.sales_id and si1.sales_item_no = si.sales_item_no
	INNER JOIN scratch_book sb ON si.scratch_book_id = sb.scratch_book_id 
	INNER JOIN product_class pc ON sb.product_class_id = pc.product_class_id
	INNER JOIN client_address ca ON ca.client_id = c.client_id and ca.client_sequence_code= c.client_sequence_code and ca.address_type = 'BT'
	INNER JOIN countries on countries.country_code = ca.country_code
where pay.payment_entry_date between @start_date and @end_date
	--and (si.sales_item_no = 1)
GROUP BY 
	MONTH(pay.payment_entry_date)
	, YEAR(pay.payment_entry_date)
	--, pay.payment_entry_date
	, pa.partner_name
	, pc.description
	, countries.currency_code 
	, si.sales_item_no
ORDER BY YEAR(pay.payment_entry_date), MONTH(pay.payment_entry_date)
GO
