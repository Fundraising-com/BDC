USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_rpt_check_summary_sheet_mid_year]    Script Date: 02/14/2014 13:04:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE     PROCEDURE [dbo].[cc_rpt_check_summary_sheet_mid_year]-- 0
	@intpartnerid int
	,@intcampaignid int = null
as
declare @conversion_rate float
/*declare @intPartnerId int

set @intPartnerId =0*/




SELECT 
	@conversion_rate=Conversion_Rate
FROM	SQLEROSE_EFUNDRAISINGPROD.eFundraisingProd.dbo.conversion_rate_table
WHERE	Conversion_Date = (
	SELECT 
		MAX( Conversion_Date )
	FROM	SQLEROSE_EFUNDRAISINGPROD.eFundraisingProd.dbo.conversion_rate_table
)

if (@intcampaignid is not null)
BEGIN
	
	SELECT
		  @conversion_rate as conversion_rate
		, p.partner_name
		, g.group_id
		, e.event_id as campaign_id
		, e.event_Name as group_name
		, parent_name as participant_name
		, parent.email_address as  participant_email
		, isnull(m.[first_name],'') + ' ' + isnull(m.[last_name],'') supporter_name	
		, m.email_address as supporter_email
		, op.payment_id as supporter_check_id
		, op.order_id
		, op.order_item_id
		, op.order_date
		, op.item_price as order_total
		, 1 as quantity
		, op.profit
		, op.total_profit
		, pm.paid_amount as check_amount
		, pp.start_date
		, pp.end_date
		, pm.cheque_number as check_number
		, pm.payment_id as check_id
		, cd.new_check_profit as diff
		, cd.total_check_profit as sum_profit 
		, cd.old_check_profit as sum_check
		, si.org_name as [name]
		, si.payment_name as payable_name
		, si.group_name as organization_name
		, si.address_1 as address
		, si.city
		, si.zip_code as zip
		, si.state_code
		, si.country_code
		, si.external_group_id as ext_org_id
                , isnull(year_profit,0) + cd.new_check_profit as year_profit
	FROM         
		[group] g
	
		inner join event_group eg
		on g.group_id = eg.group_id		
		inner join event e
		on eg.event_id = e.event_id
             	inner join (
			select 
				isnull(m.first_name,'') + ' ' + isnull(m.last_name,'') as org_name
				,py.payment_name
				,py.payment_info_id
				,g.group_name
				,pa.address_1
				,pa.city
				,pa.zip_code
				,s.country_code
				,replace (s.subdivision_code, 'US-','') as state_code
				, external_group_id
				,g.group_id
                                ,eg.event_id
			from 
				[group] g
                                inner join event_group eg
                                on g.group_id = eg.group_id
				inner join member_hierarchy mh
				on mh.member_hierarchy_id = g.sponsor_id
				inner join member m
				on m.member_id = mh.member_id
				inner join payment_info py
				on py.event_id = eg.event_id
				and py.active=1
				inner join postal_address pa
				on pa.postal_address_id = py.postal_address_id
				left join subdivision s
				on s.subdivision_code = pa.subdivision_code
                             where eg.event_id = @intCampaignID

		)si
		on si.event_id = e.event_id
		inner join order_profit op
		on op.event_id = e.event_id
		inner join event_participation ep
		on ep.event_participation_id = op.event_participation_id
		inner join member_hierarchy mh
		on mh.member_hierarchy_id = ep.member_hierarchy_id
		inner join member m
		on m.member_id = mh.member_id
		left outer join (
			select 
				mh.member_hierarchy_id
				,isnull(m.first_name,'') + ' ' + isnull(m.last_name,'') as parent_name
				,m.email_address 
			from
				member_hierarchy mh
				inner join member_hierarchy mh2
				on mh.member_hierarchy_id = mh2.parent_member_hierarchy_id
				inner join member m
				on m.member_id = mh.member_id
			group by
				mh.member_hierarchy_id
				, m.first_name 
				, m.last_name 
				,m.email_address 
		)parent
		on parent.member_hierarchy_id = mh.parent_member_hierarchy_id			
		INNER JOIN dbo.partner p
		ON p.partner_id = g.partner_id 
		inner join dbo.cc_check_diff_closed_period_mid_year cd
		ON cd.event_id = e.event_id
		left outer join dbo.[payment] pm
		ON op.payment_id = pm.payment_id
		LEFT OUTER JOIN dbo.payment_period pp
		ON pp.payment_period_id = pm.payment_period_id 
                left join (select group_id, sum(paid_amount) as year_profit
                          from payment p
                          inner join payment_info pi on p.payment_info_id = pi.payment_info_id
                          where cheque_date between '2007-01-16' and '2008-01-16'
                          group by pi.group_id) year_profit_view
                on g.group_id = year_profit_view.group_id 
	
	WHERE
		op.order_date < '07-01-2007'
	and 	e.event_id = @intCampaignID


end
else if (@intpartnerid = -1 )
begin
SELECT
		 @conversion_rate as conversion_rate
		,  p.partner_name
		, g.group_id
		, e.event_id as campaign_id
		, e.event_Name as group_name
		, parent_name as participant_name
		, parent.email_address as  participant_email
		, isnull(m.[first_name],'') + ' ' + isnull(m.[last_name],'') supporter_name	
		, m.email_address as supporter_email
		, op.payment_id as supporter_check_id
		, op.order_id
		, op.order_item_id
		, op.order_date
		, op.item_price as order_total
		, 1 as quantity
		, op.profit
		, op.total_profit
		, pm.paid_amount as check_amount
		, pp.start_date
		, pp.end_date
		, pm.cheque_number as check_number
		, pm.payment_id as check_id
		, cd.new_check_profit as diff
		, cd.total_check_profit as sum_profit 
		, cd.old_check_profit as sum_check
		, si.org_name as [name]
		, si.payment_name as payable_name
		, si.group_name as organization_name
		, si.address_1 as address
		, si.city
		, si.zip_code as zip
		, si.state_code
		, si.country_code
		, si.external_group_id as ext_org_id
                , isnull(year_profit,0) + cd.new_check_profit as year_profit
	FROM         
		[group] g
	
		inner join event_group eg
		on g.group_id = eg.group_id		
		inner join event e
		on eg.event_id = e.event_id
             	inner join (
			select 
				isnull(m.first_name,'') + ' ' + isnull(m.last_name,'') as org_name
				,py.payment_name
				,py.payment_info_id
				,g.group_name
				,pa.address_1
				,pa.city
				,pa.zip_code
				,s.country_code
				,replace (s.subdivision_code, 'US-','') as state_code
				, external_group_id
				,g.group_id
                                ,eg.event_id
			from 
				[group] g
                                inner join event_group eg
                                on g.group_id = eg.group_id
				inner join member_hierarchy mh
				on mh.member_hierarchy_id = g.sponsor_id
				inner join member m
				on m.member_id = mh.member_id
				inner join payment_info py
				on py.event_id = eg.event_id
				and py.active=1
				inner join postal_address pa
				on pa.postal_address_id = py.postal_address_id
				left join subdivision s
				on s.subdivision_code = pa.subdivision_code
           
		)si
		on si.event_id = e.event_id
		inner join order_profit op
		on op.event_id = e.event_id
		inner join event_participation ep
		on ep.event_participation_id = op.event_participation_id
		inner join member_hierarchy mh
		on mh.member_hierarchy_id = ep.member_hierarchy_id
		inner join member m
		on m.member_id = mh.member_id
		left outer join (
			select 
				mh.member_hierarchy_id
				,isnull(m.first_name,'') + ' ' + isnull(m.last_name,'') as parent_name
				,m.email_address 
			from
				member_hierarchy mh
				inner join member_hierarchy mh2
				on mh.member_hierarchy_id = mh2.parent_member_hierarchy_id
				inner join member m
				on m.member_id = mh.member_id
			group by
				mh.member_hierarchy_id
				, m.first_name 
				, m.last_name 
				,m.email_address 
		)parent
		on parent.member_hierarchy_id = mh.parent_member_hierarchy_id			
		INNER JOIN dbo.partner p
		ON p.partner_id = g.partner_id 
		inner join dbo.cc_check_diff_closed_period_mid_year cd
		ON cd.event_id = e.event_id
		left outer join dbo.[payment] pm
		ON op.payment_id = pm.payment_id
		LEFT OUTER JOIN dbo.payment_period pp
		ON pp.payment_period_id = pm.payment_period_id 
                left join (select group_id, sum(paid_amount) as year_profit
                          from payment p
                          inner join payment_info pi on p.payment_info_id = pi.payment_info_id
                          where cheque_date between '2007-01-16' and '2008-01-16'
                          group by pi.group_id) year_profit_view
                on g.group_id = year_profit_view.group_id 
	
	WHERE
		op.order_date < '07-01-2007'
		and ( p.partner_id <> 0 )
	
end
else
BEGIN
		SELECT
		 @conversion_rate as conversion_rate
		,  p.partner_name
		, g.group_id
		, e.event_id as campaign_id
		, e.event_Name as group_name
		, parent_name as participant_name
		, parent.email_address as  participant_email
		, isnull(m.[first_name],'') + ' ' + isnull(m.[last_name],'') supporter_name	
		, m.email_address as supporter_email
		, op.payment_id as supporter_check_id
		, op.order_id
		, op.order_item_id
		, op.order_date
		, op.item_price as order_total
		, 1 as quantity
		, op.profit
		, op.total_profit
		, pm.paid_amount as check_amount
		, pp.start_date
		, pp.end_date
		, pm.cheque_number as check_number
		, pm.payment_id as check_id
		, cd.new_check_profit as diff
		, cd.total_check_profit as sum_profit 
		, cd.old_check_profit as sum_check
		, si.org_name as [name]
		, si.payment_name as payable_name
		, si.group_name as organization_name
		, si.address_1 as address
		, si.city
		, si.zip_code as zip
		, si.state_code
		, si.country_code
		, si.external_group_id as ext_org_id
                , isnull(year_profit,0) + cd.new_check_profit as year_profit
	FROM         
		[group] g
	
		inner join event_group eg
		on g.group_id = eg.group_id		
		inner join event e
		on eg.event_id = e.event_id
             	inner join (
			select 
				isnull(m.first_name,'') + ' ' + isnull(m.last_name,'') as org_name
				,py.payment_name
				,py.payment_info_id
				,g.group_name
				,pa.address_1
				,pa.city
				,pa.zip_code
				,s.country_code
				,replace (s.subdivision_code, 'US-','') as state_code
				, external_group_id
				,g.group_id
                                ,eg.event_id
			from 
				[group] g
                                inner join event_group eg
                                on g.group_id = eg.group_id
				inner join member_hierarchy mh
				on mh.member_hierarchy_id = g.sponsor_id
				inner join member m
				on m.member_id = mh.member_id
				inner join payment_info py
				on py.event_id = eg.event_id
				and py.active=1
				inner join postal_address pa
				on pa.postal_address_id = py.postal_address_id
				left join subdivision s
				on s.subdivision_code = pa.subdivision_code
               
		)si
		on si.event_id = e.event_id
		inner join order_profit op
		on op.event_id = e.event_id
		inner join event_participation ep
		on ep.event_participation_id = op.event_participation_id
		inner join member_hierarchy mh
		on mh.member_hierarchy_id = ep.member_hierarchy_id
		inner join member m
		on m.member_id = mh.member_id
		left outer join (
			select 
				mh.member_hierarchy_id
				,isnull(m.first_name,'') + ' ' + isnull(m.last_name,'') as parent_name
				,m.email_address 
			from
				member_hierarchy mh
				inner join member_hierarchy mh2
				on mh.member_hierarchy_id = mh2.parent_member_hierarchy_id
				inner join member m
				on m.member_id = mh.member_id
			group by
				mh.member_hierarchy_id
				, m.first_name 
				, m.last_name 
				,m.email_address 
		)parent
		on parent.member_hierarchy_id = mh.parent_member_hierarchy_id			
		INNER JOIN dbo.partner p
		ON p.partner_id = g.partner_id 
		inner join dbo.cc_check_diff_closed_period_mid_year cd
		ON cd.event_id = e.event_id
		left outer join dbo.[payment] pm
		ON op.payment_id = pm.payment_id
		LEFT OUTER JOIN dbo.payment_period pp
		ON pp.payment_period_id = pm.payment_period_id 
                left join (select group_id, sum(paid_amount) as year_profit
                          from payment p
                          inner join payment_info pi on p.payment_info_id = pi.payment_info_id
                          where cheque_date between '2007-01-16' and '2008-01-16'
                          group by pi.group_id) year_profit_view
                on g.group_id = year_profit_view.group_id 
	
	WHERE
		op.order_date < '07-01-2007'
		and ( p.partner_id = @intPartnerID )
order by e.event_id

END
GO
