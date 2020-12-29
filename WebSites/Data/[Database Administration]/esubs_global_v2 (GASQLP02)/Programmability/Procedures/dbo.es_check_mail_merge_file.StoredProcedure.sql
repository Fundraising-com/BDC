USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_check_mail_merge_file]    Script Date: 02/14/2014 13:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
created by: JF Lavigne and some help by F blais
date: nov 1 2004
description: this returns profit check information and campaign info for a specific partner
             (to be used with the check system)
*/

CREATE PROCEDURE [dbo].[es_check_mail_merge_file] 
            @intPartnerID INT = NULL
AS
IF @intPartnerID IS NULL
BEGIN
	--THE ORDER OF THE FIELDS CANNOT CHANGE (at least #10 and 11)
	SELECT
		e.event_id 
		, e.event_name
		, pa.address_1
		, pa.city
		, pa.subdivision_code
		, pa.zip_code
		, isnull(m.[first_name],'') + ' ' +isnull(m.last_name, '') as sponsor_name
		, pn.phone_number phone
		, g.external_group_id
		, diff.new_check_profit Check_Amount
		, clt.[name]
		, clt.phone_extension          
		, m.email_address
		, m.[password]
		, diff.old_check_profit Check_Cumulative
		, diff.total_check_profit Cumulative_Profit
		, g.partner_id
		, SUM( 1 ) credits_earned
		, 0 AS credits_left
		, 0 AS credits_used
		, py.payment_name as check_to_name
		, MAX( py.on_behalf_of_name ) check_on_behalf_of
		, partner_name
		, 'none' as Partner_url
		, l.lead_id
		, g.group_id
	FROM	dbo.cc_check_diff_2 diff 		
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
		on g.group_id = py.group_id
		and py.active = 1
		inner join postal_address pa
		on pa.postal_address_id = py.postal_address_id
                left outer join subdivision s
                on s.subdivision_code = pa.subdivision_code
		LEFT OUTER JOIN  eFundraisingProd.dbo.lead l 
		ON l.lead_id = g.lead_id
		LEFT OUTER JOIN eFundraisingProd.dbo.consultant clt 
		ON clt.consultant_id = l.consultant_id		
		left outer join phone_number pn
		on pn.phone_number_id = py.phone_number_id
	WHERE
		( e.event_ID <> 40 )
	GROUP BY 
		e.event_id
		, e.event_name
		, pa.address_1
		, pa.city
		, pa.subdivision_code
		, pa.zip_code
		, m.[first_name]
		, m.last_name
		, pn.phone_number
		, g.external_group_id
		, diff.new_check_profit
		, diff.old_check_profit 
		, diff.total_check_profit 
		, clt.[name]
		, clt.phone_extension          
		, m.email_address
		, m.[password]
		, clt.[name]
		, clt.phone_extension
		, p.partner_name
                , g.group_id
                , l.lead_id
		,py.payment_name
		,g.partner_id
	ORDER BY
		p.partner_name
		, e.event_ID

END
ELSE
BEGIN
	--THE ORDER OF THE FIELDS CANNOT CHANGE (at least #10 and 11)
	SELECT
		e.event_id 
		, e.event_name
		, pa.address_1
		, pa.city
		, pa.subdivision_code
		, pa.zip_code
		, isnull(m.[first_name],'') + ' ' +isnull(m.last_name, '') as sponsor_name
		, pn.phone_number phone
		, g.external_group_id
		, diff.new_check_profit Check_Amount
		, clt.[name]
		, clt.phone_extension          
		, m.email_address
		, m.[password]
		, diff.old_check_profit Check_Cumulative
		, diff.total_check_profit Cumulative_Profit
		, g.partner_id
		, SUM( 1 ) credits_earned
		, 0 AS credits_left
		, 0 AS credits_used
		, py.payment_name as check_to_name
		, MAX( py.on_behalf_of_name ) check_on_behalf_of
		, partner_name
		, 'none' as Partner_url
		, l.lead_id
		, g.group_id
	FROM	dbo.cc_check_diff_2 diff 		
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
		on g.group_id = py.group_id
		and py.active = 1
		inner join postal_address pa
		on pa.postal_address_id = py.postal_address_id
                left outer join subdivision s
                on s.subdivision_code = pa.subdivision_code
		LEFT OUTER JOIN  eFundraisingProd.dbo.lead l 
		ON l.lead_id = g.lead_id
		LEFT OUTER JOIN eFundraisingProd.dbo.consultant clt 
		ON clt.consultant_id = l.consultant_id		
		left outer join phone_number pn
		on pn.phone_number_id = py.phone_number_id
	WHERE
		( e.event_ID <> 40 )
		and g.partner_id = @intPartnerID
	GROUP BY 
		e.event_id
		, e.event_name
		, pa.address_1
		, pa.city
		, pa.subdivision_code
		, pa.zip_code
		, m.[first_name]
		, m.last_name
		, pn.phone_number
		, g.external_group_id
		, diff.new_check_profit
		, diff.old_check_profit 
		, diff.total_check_profit 
		, clt.[name]
		, clt.phone_extension          
		, m.email_address
		, m.[password]
		, clt.[name]
		, clt.phone_extension
		, p.partner_name
              	  , g.group_id
              	  , l.lead_id
		,py.payment_name
		,g.partner_id
	ORDER BY
		p.partner_name
		, e.event_ID

END
GO
