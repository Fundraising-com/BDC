USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efr_rpt_leads_dashbord]    Script Date: 02/14/2014 13:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec efr_rpt_workable_lead_summary '07-01-2006' , '07-31-2006', '08-30-2006'

-- ALTER  by MEL
CREATE        PROCEDURE [dbo].[efr_rpt_leads_dashbord] 
	@start_date datetime,
	@end_date datetime, 
	@sale_end_date datetime
as


declare @end_date2 varchar(30)
set @end_date2 = dateadd(hh, 24, @end_date)
set @end_date = dateadd(ss, -1, @end_date2)

declare @sale_end_date2 datetime
set @sale_end_date2 = dateadd(hh, 24, @end_date)
set @sale_end_date = dateadd(ss, -1, @end_date2)


select distinct qsp_account_matching_code into #p3s
from dbo.crm_static_past3seasons_new
create index ix_matching_code on #p3s (qsp_account_matching_code)

select cl.lead_id, sum(s.total_amount) as total_amount into #sale
from client cl 
inner join sale s on s.client_id = cl.client_id 
	and s.client_sequence_code = cl.client_sequence_code and s.actual_ship_date  between @start_date and @sale_end_date 
group by cl.lead_id
create index ix_lead_id on #sale (lead_id)


select 'COUNTRY', 'STATUS', '+COUNT LEAD', '$TOTAL SALES'
select (case when l.country_code = 'CA' then 'CA'
		when l.country_code = 'US' then 'US' else 'INTERNATIONAL' end) as country

	, (case when p3s.qsp_account_matching_code is not null then '1) FLAGPOLE'
		when c.is_fm = 1 then '2) HARMONY'
		when xcon.is_fm = 1 then '2) HARMONY'
		when c.is_agent = 1 then '3) Agents'
		when l.day_phone is null and l.evening_phone is null then '4) NO PHONE NUMBER / TEST'
		when l.consultant_id = 936 then '4) NO PHONE NUMBER / TEST'
		when p.promotion_type_code = 'OUT' then '5) OUTBOUND'		
		when l.country_code <> 'CA' and l.country_code <> 'US' then '6) INTERNATIONAL'
		when l.promotion_id = 3624 or l.promotion_id = 3852 then '7) INDIRECT'
		when p.promotion_type_code = 'AF' or p.promotion_type_code = 'IM' then '7) INDIRECT'
		else '8) WORKABLE' end) as type
	, count(*) 
	, (case when sum(s.total_amount) is null then 0 else sum(s.total_amount) end) as totamount
	from lead l
inner join consultant c on c.consultant_id = l.consultant_id
left outer join consultant xcon on xcon.consultant_id = l.ext_consultant_id
inner join promotion p on p.promotion_id = l.promotion_id
left outer join #p3s p3s on p3s.qsp_account_matching_code = l.matching_code
-- sales
left outer join #sale s on s.lead_id = l.lead_id 
where l.lead_entry_date between @start_date and @end_date
group by (case when l.country_code = 'CA' then 'CA'
		when l.country_code = 'US' then 'US' else 'INTERNATIONAL' end)

	, (case when p3s.qsp_account_matching_code is not null then '1) FLAGPOLE'
		when c.is_fm = 1 then '2) HARMONY'
		when xcon.is_fm = 1 then '2) HARMONY'
		when c.is_agent = 1 then '3) Agents'
		when l.day_phone is null and l.evening_phone is null then '4) NO PHONE NUMBER / TEST'
		when l.consultant_id = 936 then '4) NO PHONE NUMBER / TEST'
		when p.promotion_type_code = 'OUT' then '5) OUTBOUND'		
		when l.country_code <> 'CA' and l.country_code <> 'US' then '6) INTERNATIONAL'
		when l.promotion_id = 3624 or l.promotion_id = 3852 then '7) INDIRECT'
		when p.promotion_type_code = 'AF' or p.promotion_type_code = 'IM' then '7) INDIRECT'
		else '8) WORKABLE' end)
order by 1, 2
GO
