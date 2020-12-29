USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_checks_all_info_mid_year]    Script Date: 02/14/2014 13:04:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[cc_checks_all_info_mid_year] --36
    @period_id int
AS
BEGIN
    
	SELECT
		e.event_id 
		, e.event_name
		, diff.new_check_profit Check_Amount
		, diff.old_check_profit Check_Cumulative
		, diff.total_check_profit Cumulative_Profit
		, g.partner_id
		, p.partner_name
        , pt.cheque_number
        , (case when pt.paid_amount > 0 then pt.paid_amount else diff.new_check_profit end) as amount 
        , isnull(pt.payment_id,0) as Paid
       FROM	dbo.cc_check_diff_closed_period_mid_year diff
		INNER JOIN dbo.event e
		ON diff.event_id = e.event_id
		inner join event_group eg
		on eg.event_id = e.event_id
		inner join [group] g
		on g.group_id = eg.group_id
		INNER JOIN dbo.partner p
		ON g.partner_id = p.partner_id
		inner join member_hierarchy mh
		on mh.member_hierarchy_id = g.sponsor_id
		inner join member m
		on m.member_id = mh.member_id
		inner join payment_info py
		on e.event_id = py.event_id
		and py.active = 1
	    left join payment pt 
        on py.payment_info_id = pt.payment_info_id and pt.payment_period_id = @period_id
        --juste eux avec addresses
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
				inner join subdivision s
				on s.subdivision_code = pa.subdivision_code
		       )si
		       on si.event_id = e.event_id
        GROUP BY e.event_id 
		, e.event_name
		, diff.new_check_profit
		, diff.old_check_profit
		, diff.total_check_profit 
		, g.partner_id
		, partner_name
        , pt.cheque_number
        , pt.paid_amount
        , pt.payment_id
	ORDER BY
		p.partner_name
		, e.event_ID

END
GO
