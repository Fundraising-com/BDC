USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_event_sellers]    Script Date: 02/14/2014 13:05:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* retourne un top meilleurs pour 
   mcote 2009-03-27
   
   Updated by Melissa Cote 
   Update date 2010.09.27
   
   need to display the participants name not the supporter 
   
   Updated by Jiro Hidaka 
   Update date 2010.09.28

   Added CASE statement for the sponsor name

   Updated by Jiro Hidaka 
   Update date 2011.01.20
*/

CREATE procedure [dbo].[es_get_event_sellers]
	@event_id int
AS

select
	(case 
      -- sponsor order must be under his name
      when (mp.first_name + ' ' + mp.last_name) is null 
	    then m.first_name + ' ' + m.last_name
      else mp.first_name + ' ' +mp.last_name
      end) as [supp_name]
	--, ep.event_id
	, sum(case 
		  when tps.product_type_id  = 18 and tps.store_id = 1 then 0
		  else tps.quantity end) as quantity
	, sum(case 
			when tps.product_type_id = 18  and tps.store_id = 1 then 0
			else tps.sub_total end) as amount
	, min(m.create_date) as create_date
    , COALESCE(sum(od.quantity * od.price), 0) AS donation_amount
from
	member_hierarchy mh	
	inner join member m	on m.member_id = mh.member_id 
	inner join event_participation ep on ep.member_hierarchy_id=mh.member_hierarchy_id
	inner join [es_get_valid_orders_items_by_event_id] (@event_id) tps on tps.supp_id = ep.event_participation_id
	 -- parent
	left outer join member_hierarchy mhp on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	left outer join member mp on mp.member_id = mhp.member_id

	-- Donation orders from EFRECommerce db:
    left join EFRECommerce.dbo.[order] o WITH (NOLOCK) ON ep.event_participation_id = o.ext_participation_id AND o.deleted = 0 AND o.source_id = 1
    left join EFRECommerce.dbo.order_detail od WITH (NOLOCK) ON o.order_id = od.order_id AND od.deleted = 0 AND od.product_id = 1
    left join dbo.es_get_valid_efrecommerce_order_status() efreos ON o.status_id = efreos.status_id
where
	(mh.creation_channel_id in(12,14,29) or tps.supp_id is not null) -- on prend tous les gens qui ont des subs et les supporters invited
	and ep.event_id = @event_id
group by 
     (case 
      -- sponsor order must be under his name
      when (mp.first_name + ' ' + mp.last_name) is null 
	    then m.first_name + ' ' + m.last_name
      else mp.first_name + ' ' +mp.last_name
      end)
	--, ep.event_id
order by 3 desc
GO
