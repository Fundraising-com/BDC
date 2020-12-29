USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efr_rpt_aging_report]    Script Date: 02/14/2014 13:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE       PROCEDURE [dbo].[efr_rpt_aging_report] (
		@end_date datetime
	)
AS 
--62
declare @today as datetime 
set @today = getdate()


declare @end_date2 varchar(30)
set @end_date2 = convert(varchar(30), @end_date, 101) + ' 23:59:59'
set @end_date = convert(datetime, @end_date2)



select 'US$'
select ''

select '+SALE ID'
	,'$0-30' 
	, '$31-60' 
	, '$61-90' 
	, '$91-120' 
	, '$121-150' 
	, '$151-180'
	, '$181 +'
	, 'Coll'

select 	sales_id
	, (case when collection_status_id <> 6 and datdiff between -30 and 0 then diff end)
	, (case when collection_status_id <> 6 and datdiff between -60 and -31 then diff end)
	, (case when collection_status_id <> 6 and datdiff between -90 and -61 then diff end)
	, (case when collection_status_id <> 6 and datdiff between -120 and -91 then diff end)
	, (case when collection_status_id <> 6 and datdiff between -150 and -121 then diff end)
	, (case when collection_status_id <> 6 and datdiff between -180 and -151 then diff end)
	, (case when collection_status_id <> 6 and datdiff < -180 then diff end)
	, (case when collection_status_id = 6 then diff end)
from(

SELECT --top 1 
	datediff(d, @today,  s.actual_ship_date) as datdiff
	, s.actual_ship_date
	, s.collection_status_id
	, s.total_amount
	, (coalesce(p.payment_amount, 0)) as pay
	, (coalesce(a.adjustment_amount, 0)) as adj
	, ((coalesce(p.payment_amount, 0)) + (coalesce(a.adjustment_amount, 0))) as pay_adj
	, s.total_amount - (coalesce(p.payment_amount, 0) + coalesce(a.adjustment_amount, 0)) as diff
	,  s.sales_id 
from sale s
inner join client c on s.client_sequence_code = c.client_sequence_code and s.client_id = c.client_id 
inner join billing_company bc on bc.billing_company_id = s.billing_company_id 
inner join client_address ca on c.client_sequence_code = ca.client_sequence_code and c.client_id = ca.client_id and address_type = 'BT'
inner join country ctry on ca.country_code = ctry.country_code and currency_code like '%US%'
left outer join collection_status cs on s.collection_status_id = cs.collection_status_id 
left outer join (select sales_id, sum(payment_amount) as payment_amount from payment where payment_entry_date <= @end_date group by sales_id) p on s.sales_id = p.sales_id 
left outer join (select sales_id, sum(adjustment_amount) as adjustment_amount from adjustment where adjustment_date <= @end_date group by sales_id) a on s.sales_id = a.sales_id 
where  ((s.actual_ship_date is not null and s.box_return_date is null) 
	or (s.actual_ship_date is not null and  s.box_return_date is not null and s.reship_date is not null))
	and s.actual_ship_date <= @end_date
)  rec
--where rec.diff > 0
order by 9 desc


select 'CA$'
select ''

select '+SALE ID'
	,'$0-30' 
	, '$31-60' 
	, '$61-90' 
	, '$91-120' 
	, '$121-150' 
	, '$151-180'
	, '$181 +'
	, 'Coll'

select 	sales_id
	, (case when collection_status_id <> 6 and datdiff between -30 and 0 then diff end)
	, (case when collection_status_id <> 6 and datdiff between -60 and -31 then diff end)
	, (case when collection_status_id <> 6 and datdiff between -90 and -61 then diff end)
	, (case when collection_status_id <> 6 and datdiff between -120 and -91 then diff end)
	, (case when collection_status_id <> 6 and datdiff between -150 and -121 then diff end)
	, (case when collection_status_id <> 6 and datdiff between -180 and -151 then diff end)
	, (case when collection_status_id <> 6 and datdiff < -180 then diff end)
	, (case when collection_status_id = 6 then diff end)
from(

SELECT --top 1 
	datediff(d, @today,  s.actual_ship_date) as datdiff
	, s.actual_ship_date
	, s.collection_status_id
	, s.total_amount
	, (coalesce(p.payment_amount, 0)) as pay
	, (coalesce(a.adjustment_amount, 0)) as adj
	, ((coalesce(p.payment_amount, 0)) + (coalesce(a.adjustment_amount, 0))) as pay_adj
	, s.total_amount - (coalesce(p.payment_amount, 0) + coalesce(a.adjustment_amount, 0)) as diff
	,  s.sales_id 
from sale s
inner join client c on s.client_sequence_code = c.client_sequence_code and s.client_id = c.client_id 
inner join billing_company bc on bc.billing_company_id = s.billing_company_id 
inner join client_address ca on c.client_sequence_code = ca.client_sequence_code and c.client_id = ca.client_id and address_type = 'BT'
inner join country ctry on ca.country_code = ctry.country_code and currency_code like '%CA%'
left outer join collection_status cs on s.collection_status_id = cs.collection_status_id 
left outer join (select sales_id, sum(payment_amount) as payment_amount from payment where payment_entry_date <= @end_date group by sales_id) p on s.sales_id = p.sales_id 
left outer join (select sales_id, sum(adjustment_amount) as adjustment_amount from adjustment where adjustment_date <= @end_date group by sales_id) a on s.sales_id = a.sales_id 
where  ((s.actual_ship_date is not null and s.box_return_date is null) 
	or (s.actual_ship_date is not null and  s.box_return_date is not null and s.reship_date is not null))
	and s.actual_ship_date <= @end_date
)  rec
--where rec.diff > 0
order by 9 desc
GO
