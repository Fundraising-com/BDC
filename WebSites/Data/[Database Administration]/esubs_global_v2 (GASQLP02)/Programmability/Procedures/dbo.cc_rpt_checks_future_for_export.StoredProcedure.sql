USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_rpt_checks_future_for_export]    Script Date: 02/14/2014 13:04:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[cc_rpt_checks_future_for_export]
AS



SELECT
        'U-422' + cast(g.group_id as varchar(50)) as group_id
	, py.payment_name
	, py.payment_name
        , g.group_name as contact
	, pa.address_1	
	, pa.city + ', ' + right(pa.subdivision_code,2) + ' ' + pa.zip_code as city_zip_state
       , pnu.phone_number
        , m.email_address
        , case e.redirect when null then '' else 'http://magfundraising.com/' + e.redirect end as redirect
        , '1000000'

	
FROM	dbo.event e 
	INNER JOIN dbo.cc_check_diff diff 
	ON e.event_id = diff.event_id
	inner join event_group eg
	on eg.event_id = e.event_id
	inner join [group] g
	on g.group_id = eg.group_id	
	INNER JOIN dbo.partner pn
	ON g.partner_id = pn.partner_id 
	inner join payment_info py
	on py.group_id = g.group_id
	and py.active = 1
	inner join member_hierarchy mh
	on mh.member_hierarchy_id = g.sponsor_id
	inner join member m
	on m.member_id = mh.member_id
	left outer join postal_address pa
	on pa.postal_address_id = py.postal_address_id
	left outer join subdivision s
	on s.subdivision_code = pa.subdivision_code
        inner join order_profit op 
            on e.event_id = op.event_id
      
        left join phone_number pnu
           on pnu.phone_number_id = py.phone_number_id
        

        
WHERE
( pa.address_1 IS NOT NULL )
 AND	( LTRIM( RTRIM( pa.address_1 ) ) != '' )
and pn.partner_id not in (143,57)
and	 diff.old_check_profit <1         
GROUP BY
          g.group_id
        , py.payment_name  
	, g.group_name
	, pa.address_1
	, pa.city
	, pa.zip_code
	, m.first_name 
	, m.last_name
        , pnu.phone_number
        , m.email_address
        , e.redirect 
        ,pa.subdivision_code
     
ORDER BY 
	
	 MAX( op.order_date ) desc
	, g.group_id
GO
