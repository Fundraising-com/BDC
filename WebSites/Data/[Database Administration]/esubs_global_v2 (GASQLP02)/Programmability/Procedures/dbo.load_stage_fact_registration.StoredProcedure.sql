USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[load_stage_fact_registration]    Script Date: 02/14/2014 13:08:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[load_stage_fact_registration] @load_date datetime as

select e.event_id,
		e.event_type_id 'campaign_type_key',
		e.create_date 'registration_create_date',--,null 'activation_create_date'
        e.group_type_id
from [event] e with(nolock)  where create_date > @load_date

/*
select e.event_id,e.event_type_id 'campaign_type_key',e.create_date 'registration_create_date',null 'activation_create_date'
from [event] e
where not exists
(select 1 from qspecommerce..efundraisingtransaction et
 inner join event_participation ep
 on et.suppid = ep.event_participation_id
 where ep.event_id = e.event_id)

union all

select e.event_id,e.event_type_id 'campaign_type_key',e.create_date 'registration_create_date',min(et.createdate) 'activation_create_date'
from esubs_global_v2..event e
inner join esubs_global_v2..event_participation ep
on ep.event_id = e.event_id
inner join qspecommerce..efundraisingtransaction et
on et.suppid = ep.event_participation_id
group by e.event_id,e.event_type_id,e.create_date

*/
GO
