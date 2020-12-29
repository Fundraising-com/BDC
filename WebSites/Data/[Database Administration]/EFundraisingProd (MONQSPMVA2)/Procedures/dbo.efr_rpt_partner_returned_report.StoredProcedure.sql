USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efr_rpt_partner_returned_report]    Script Date: 02/14/2014 13:03:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec efr_rpt_partner_payment_report '2006-06-01', '2006-06-30 23:59:59'

CREATE               procedure [dbo].[efr_rpt_partner_returned_report]
		@start_date as datetime 
		, @end_date as datetime
as


declare @end_date2 datetime
set @end_date2 = dateadd(hh, 24, @end_date)
set @end_date = dateadd(ss, -1, @end_date2)

select 'Year', 'Month', 'Partner_name', 'Product Class', 'Currency', '$Total Sales', '$Total Shipping', '$Total Returned'

select 	year(s.box_return_date) AS shipped_year
	, month(s.box_return_date) AS shipped_month
	, pa.partner_name
	, pc.description
	, countries.currency_code 
	, sum(s.total_amount - (s.shipping_fees - s.shipping_fees_discount)) as total_shipped
	, sum(s.shipping_fees - s.shipping_fees_discount) as shipping
	, sum(s.total_amount) AS sum_payment
FROM    lead l 
	INNER JOIN client c ON l.lead_id = c.lead_id 
	INNER JOIN sale s ON c.client_sequence_code = s.client_sequence_code AND c.client_id = s.client_id 
	INNER JOIN promotion p ON l.promotion_id = p.promotion_id 
	INNER JOIN partner pa ON p.partner_id = pa.partner_id 
	INNER JOIN (select distinct sales_id, min(sales_item_no) as sales_item_no from sales_item group by sales_id) si1 on s.sales_id = si1.sales_id 
	inner join sales_item si ON si1.sales_id = si.sales_id and si1.sales_item_no = si.sales_item_no
	INNER JOIN scratch_book sb ON si.scratch_book_id = sb.scratch_book_id 
	INNER JOIN product_class pc ON sb.product_class_id = pc.product_class_id
	INNER JOIN client_address ca ON ca.client_id = c.client_id and ca.client_sequence_code= c.client_sequence_code and ca.address_type = 'BT'
	INNER JOIN countries on countries.country_code = ca.country_code
where s.box_return_date between @start_date and @end_date
	--and (si.sales_item_no = 1)
GROUP BY 
	MONTH(s.box_return_date)
	, YEAR(s.box_return_date)
	, pa.partner_name
	, pc.description
	, countries.currency_code 
	, si.sales_item_no
ORDER BY YEAR(s.box_return_date), MONTH(s.box_return_date)
GO
