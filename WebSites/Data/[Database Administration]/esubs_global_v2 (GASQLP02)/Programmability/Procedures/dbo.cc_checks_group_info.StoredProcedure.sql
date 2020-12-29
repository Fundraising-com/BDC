USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_checks_group_info]    Script Date: 02/14/2014 13:04:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Created by: JF Lavigne
	Created on:

	Put in production on 2005-12-19

	Description:

*/


CREATE       PROCEDURE [dbo].[cc_checks_group_info] --1008345
            @intEventID INT = NULL
AS
BEGIN

if @intEventID is null
begin
	--THE ORDER OF THE FIELDS CANNOT CHANGE (at least #10 and 11)
	SELECT
		e.event_id 
		, e.event_name
		, pa.address_1 as address
		, pa.city
		, pa.subdivision_code as state_code
		, pa.zip_code as zip
		, isnull(m.[first_name],'') + ' ' +isnull(m.last_name, '') as organizer
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
                           , 'www.' + pav.value as partner_url
		, g.group_id
                ,clt.csr_consultant
	FROM	dbo.cc_check_diff_closed_period diff 		
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
                inner join partner_attribute_value pav
                on p.partner_id = pav.partner_id and pav.partner_attribute_id = 2
	--WHERE
		--( e.event_ID = @intEventID)
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
                ,pav.value   
		,g.partner_id
                 ,clt.csr_consultant
	ORDER BY
		p.partner_name
		, e.event_ID

end
else
begin

SELECT
		e.event_id 
		, e.event_name
		, pa.address_1 as address
		, pa.city
		, pa.subdivision_code as state_code
		, pa.zip_code as zip
		, isnull(m.[first_name],'') + ' ' +isnull(m.last_name, '') as organizer
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
                , 'www.' + pav.value as partner_url
		, g.group_id
                ,clt.csr_consultant
	FROM	dbo.cc_check_diff_closed_period diff 		
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
                inner join partner_attribute_value pav
                on p.partner_id = pav.partner_id and pav.partner_attribute_id = 2
	WHERE
		( e.event_ID = @intEventID)
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
                ,pav.value  
		,g.partner_id
                ,clt.csr_consultant
	ORDER BY
		p.partner_name
		, e.event_ID

end


END
GO
