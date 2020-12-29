USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efr_rpt_workable_lead_summary]    Script Date: 02/14/2014 13:03:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec efr_rpt_workable_lead_summary '07-01-2006' , '07-31-2006'

-- Create by MEL
CREATE  PROCEDURE [dbo].[efr_rpt_workable_lead_summary] 
	@start_date datetime,
	@end_date datetime
as

select 'COUNTRY', 'STATUS', 'COUNT LEAD'
select (case when l.country_code = 'CA' then 'CA'
		when l.country_code = 'US' then 'US' else 'INTERNATIONAL' end) as country

	, (case when p3s.qsp_account_matching_code is not null then '1) FLAGPOLE'
		when c.is_agent = 1 or c.is_fm = 1 then '2) FSM/Agents'
		when l.day_phone is null and l.evening_phone is null then '3) NO PHONE NUMBER / TEST'
		when l.consultant_id = 936 then '3) NO PHONE NUMBER / TEST'
		when p.promotion_type_code = 'OUT' then '4) OUTBOUND'		
		when l.country_code <> 'CA' and l.country_code <> 'US' then '5) INTERNATIONAL'
		when l.promotion_id = 3624 or l.promotion_id = 3852 then '6) INDIRECT'
		when p.promotion_type_code = 'AF' or p.promotion_type_code = 'IM' then '6) INDIRECT'
		else '7) WORKABLE' end) as type
	, count(*) from lead l
inner join consultant c on c.consultant_id = l.consultant_id
inner join promotion p on p.promotion_id = l.promotion_id
left outer join (select distinct qsp_account_matching_code from dbo.crm_static_past3seasons_new) p3s on p3s.qsp_account_matching_code = l.matching_code
where l.lead_entry_date between @start_date and @end_date
group by (case when l.country_code = 'CA' then 'CA'
		when l.country_code = 'US' then 'US' else 'INTERNATIONAL' end)

	, (case when p3s.qsp_account_matching_code is not null then '1) FLAGPOLE'
		when c.is_agent = 1 or c.is_fm = 1 then '2) FSM/Agents'
		when l.day_phone is null and l.evening_phone is null then '3) NO PHONE NUMBER / TEST'
		when l.consultant_id = 936 then '3) NO PHONE NUMBER / TEST'
		when p.promotion_type_code = 'OUT' then '4) OUTBOUND'		
		when l.country_code <> 'CA' and l.country_code <> 'US' then '5) INTERNATIONAL'
		when l.promotion_id = 3624 or l.promotion_id = 3852 then '6) INDIRECT'
		when p.promotion_type_code = 'AF' or p.promotion_type_code = 'IM' then '6) INDIRECT'
		else '7) WORKABLE' end)
order by 1, 2
GO
