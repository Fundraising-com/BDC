USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[load_stage_fact_order_detail]    Script Date: 02/14/2014 13:08:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[load_stage_fact_order_detail] @load_date datetime as
/*
declare @load_date datetime



select @load_date = getdate() - 1825
set @load_date = dateadd(ms, -datepart(ms, @load_date), @load_date)
set @load_date = dateadd(s, -datepart(s, @load_date), @load_date)
set @load_date = dateadd(n, -datepart(n, @load_date), @load_date)
set @load_date = dateadd(hh, -datepart(hh, @load_date), @load_date)
*/


select	(od.price * od.quantity) as price,
		od.quantity,
		od.order_id,
		mh.creation_channel_id 'channel_key',
		e.event_type_id 'campaign_type_key',
        -1 as 'location_key',
		postal_address.subdivision_code 'subdivision_code',
		p.product_type_id 'product_type_key',
        case when prize_item.prize_id is null then -1 else prize_item.prize_id end 'prize_program_key',
        e.group_type_id 'group_type_key',
        m.partner_id 'partner_key',
        --case when tinfo.business_rule_id is null or tinfo.business_rule_id in (137,145) then -1 else tinfo.business_rule_id end 'email_type_key',
		--case when t.business_rule_id is null then -1 else t.business_rule_id end 'email_type_key',
		case when ti10.business_rule_id is null then -1 else ti10.business_rule_id end 'email_type_key',
        case when personalization.fundraising_goal is null then 0 else personalization.fundraising_goal end 'fundraising_goal',
        null 'fundraising_goal_key',
        (select count(distinct case when mh2.creation_channel_id in(7,20,23,32,35, 38) then mh2.member_hierarchy_id else null end) 
        from esubs_global_v2..event_participation ep3 with(nolock)
        inner join esubs_global_v2..member_hierarchy mh2 with(nolock)
        on ep3.member_hierarchy_id = mh2.member_hierarchy_id
        where e.event_id = ep3.event_id) 'participant_count',
        null as 'participant_count_key',
        (select count(distinct case when mh2.creation_channel_id in(12,14,29,33,37) then mh2.member_id else null end ) 
        from esubs_global_v2..event_participation ep3 with(nolock)
        inner join esubs_global_v2..member_hierarchy mh2 with(nolock)
        on ep3.member_hierarchy_id = mh2.member_hierarchy_id
        where e.event_id = ep3.event_id) 'supporter_count',
        null as 'supporter_count_key',
		o.create_date 'order_create_date',
		null 'order_time_key', 
		e.create_date 'campaign_create_date',
        null 'campaign_time_key',
        (select min(createdate) 
		 from qspecommerce..efundraisingtransaction efundraisingtransaction with(nolock)
         where ep.event_participation_id = efundraisingtransaction.suppid) 'activation_create_date',
         null 'activation_time_key',
		 ep.event_id
from qspfulfillment..order_detail od with(nolock)
inner join qspfulfillment..[order] o with(nolock)
on o.order_id = od.order_id
INNER JOIN dbo.es_get_valid_order_status() os 
ON os.order_status_id = o.order_status_id 
inner join qspecommerce..efundraisingtransaction et with(nolock)
on od.order_id = et.orderid
inner join esubs_global_v2..event_participation ep with(nolock)
on ep.event_participation_id = et.suppid
inner join esubs_global_v2..member_hierarchy mh with(nolock)
on mh.member_hierarchy_id = ep.member_hierarchy_id
inner join esubs_global_v2..event e with(nolock)
on ep.event_id = e.event_id 
--inner join esubs_global_v2..payment_info pay
--on ep.event_id = pay.event_id
left join
(select event_id,max(postal_address_id) postal_address_id
from payment_info with(nolock) 
group by event_id) pay
on ep.event_id = pay.event_id 
left join esubs_global_v2..postal_address with(nolock)
on postal_address.postal_address_id = pay.postal_address_id
inner join qspfulfillment..catalog_item_detail cid with(nolock)
on od.catalog_item_detail_id = cid.catalog_item_detail_id
inner join qspfulfillment..catalog_item ci with(nolock)
on cid.catalog_item_id = ci.catalog_item_id
inner join qspfulfillment..product p with(nolock)
on p.product_id = ci.product_id
left join esubs_global_v2..earned_prize with(nolock) 
on earned_prize.event_participation_id = ep.event_participation_id
left join esubs_global_v2..prize_item with(nolock)
on prize_item.prize_item_id = earned_prize.prize_item_id
inner join esubs_global_v2..member m with(nolock)
on m.member_id = mh.member_id
left join 
(select touch1.event_participation_id,max(tinfo1.touch_info_id) 'maxtouch_info_id'
from esubs_global_v2..touch  touch1 with(nolock)
inner join qspecommerce..efundraisingtransaction efundt1 with(nolock)
on touch1.event_participation_id = efundt1.suppid
inner join esubs_global_v2..touch_info tinfo1 with(nolock)
on tinfo1.touch_info_id = touch1.touch_info_id
where tinfo1.launch_date < efundt1.createdate
group by touch1.event_participation_id
having min(datediff(ss,tinfo1.launch_date,efundt1.createdate)) = 
(select min(datediff(ss,tinfo2.launch_date,efundt2.createdate))
from esubs_global_v2..touch  touch2 with(nolock)
inner join qspecommerce..efundraisingtransaction efundt2 with(nolock)
on touch2.event_participation_id = efundt2.suppid
inner join esubs_global_v2..touch_info tinfo2 with(nolock)
on tinfo2.touch_info_id = touch2.touch_info_id
where tinfo2.launch_date < efundt2.createdate and touch1.event_participation_id = touch2.event_participation_id))email_related_to_sale
on email_related_to_sale.event_participation_id = ep.event_participation_id
left join esubs_global_v2..touch_info ti10 with(nolock)
on ti10.touch_info_id = email_related_to_sale.maxtouch_info_id
left join
(select max(personalization_id) personalization_id,event_participation_id
from esubs_global_v2..personalization  with(nolock)
group by event_participation_id) pers
on pers.event_participation_id = ep.event_participation_id
left join esubs_global_v2..personalization personalization with(nolock)
on personalization.personalization_id = pers.personalization_id
where od.update_date > @load_date and
o.order_status_id IN (101, 110, 112, 120, 201, 301, 302, 304, 401, 501, 601, 701)--and pay.active = 1




--order by order_id

--select * from esubs_global_v2.INFORMATION_SCHEMA.COLUMNS where column_name = 'partner_id'

--select * from esubs_global_v2..business_rule

--select * from esubs_global_v2..participation_channel
--select * from esubs_global_v2..business_rule

--select top 100 * from esubs_global_v2.dbo.member_hierarchy
--select * from esubs_global_v2.dbo.participation_channel
--select * from touch where event_participation_id = 129953583
--select * from qspecommerce..efundraisingtransaction where suppid = 129953583 2009-11-24 23:32:11.050
GO
