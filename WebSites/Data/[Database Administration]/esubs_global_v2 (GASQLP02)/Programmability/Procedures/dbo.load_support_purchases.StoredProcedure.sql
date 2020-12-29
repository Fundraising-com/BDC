USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[load_support_purchases]    Script Date: 02/14/2014 13:08:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[load_support_purchases] as

select distinct ep.event_id,
	   mh.member_id member_id,
	   min(ep.create_date) support_create_date,
	   g.partner_id partner_key,
	   min(mh.creation_channel_id) channel_key,
       min(et.createdate) support_purchase_create_date,
       e.group_type_id group_type_key
from event_participation ep with(nolock) 
inner join member_hierarchy mh with(nolock) 
on ep.member_hierarchy_id = mh.member_hierarchy_id
inner join creation_channel cc with(nolock) 
on cc.creation_channel_id = mh.creation_channel_id
inner join [event] e with(nolock) 
on e.event_id = ep.event_id
inner join event_group eg with(nolock) 
on eg.event_id = e.event_id
inner join [group] g with(nolock) 
on g.group_id = eg.group_id
inner join qspecommerce..efundraisingtransaction et with(nolock) 
on et.suppid = ep.event_participation_id
--inner join [partner] p
--on p.partner_id = g.partner_id
where --mh.creation_channel_id in (12,14,29,33, 37)
      cc.member_type_id = 3
group by ep.event_id,mh.member_id,g.partner_id,e.group_type_id
GO
