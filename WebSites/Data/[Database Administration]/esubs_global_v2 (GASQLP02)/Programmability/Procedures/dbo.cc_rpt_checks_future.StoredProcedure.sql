USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_rpt_checks_future]    Script Date: 02/14/2014 13:04:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE      PROCEDURE [dbo].[cc_rpt_checks_future]
AS
SELECT
        g.group_id
	, e.event_name
	, e.event_id
	, pn.partner_name
	, MAX( op.order_date ) last_sale
	, pa.address_1
	, pa.city
	, pa.zip_code
	, pa.subdivision_code
	, s.country_code
	, m.first_name + ' ' + m.last_name
	,  payment_name
   
	
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
              -- inner join (select max(order_date) last_sale from order_profit) op on e.event_id = op.event_id
        inner join order_profit op on e.event_id = op.event_id
WHERE
( pa.address_1 IS NOT NULL )
 AND	( LTRIM( RTRIM( pa.address_1 ) ) != '' )
and pn.partner_id not in (143,57)
GROUP BY
          g.group_id
	, e.event_name
	, e.event_id
	, pn.partner_name
	, pa.address_1
	, pa.city
	, pa.zip_code
	, pa.subdivision_code
	, s.country_code
	, m.first_name 
	, m.last_name
	,  payment_name
        , diff.old_check_profit

HAVING 
	( e.event_id <> 40 ) and --and max(op.order_date) > '2005-09-11'
         diff.old_check_profit <1         
ORDER BY 
	pn.partner_name
	, MAX( op.order_date ) desc
	, e.event_id
GO
