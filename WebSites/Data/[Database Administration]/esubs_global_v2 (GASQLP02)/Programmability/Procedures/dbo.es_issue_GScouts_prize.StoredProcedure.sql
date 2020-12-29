USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_issue_GScouts_prize]    Script Date: 02/14/2014 13:06:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
prize GScouts
*/
CREATE procedure [dbo].[es_issue_GScouts_prize]

as
begin tran
declare @touch_info_id int

insert into touch_info(
business_rule_id, visitor_log_id, launch_date, create_date)
values(85,null,getdate(),getdate())

if @@error <> 0
begin
	rollback transaction
	return-1
end

set @touch_info_id =@@identity


insert into touch (
event_participation_id, member_hierarchy_id, touch_info_id, processed, create_date
)
select 
	event_participation_id
	,null
	,@touch_info_id
	,0
	,getdate()
from (
	select
		ep.event_participation_id
		,count(mh.member_hierarchy_id) as nb_supp
	from 
		event_participation ep
		inner join member_hierarchy mh
		on mh.parent_member_hierarchy_id = ep.member_hierarchy_id
		and mh.creation_channel_id in(12,14)
		inner join member m
		on m.member_id = mh.member_id
		and bounced =0
		AND m.email_address NOT LIKE '%efundraising.com'
		inner join  member_hierarchy mh2
		on mh2.member_hierarchy_id = ep.member_hierarchy_id
		inner join member m2
		on m2.member_id = mh2.member_id
		and m2.bounced =0
		AND m2.email_address NOT LIKE '%efundraising.com'
		inner join event_group eg
		on eg.event_id = ep.event_id
		inner join [group] g
		on g.group_id = eg.group_id
		left outer join (
			select
				t.touch_id
				,t.event_participation_id
			from 
			touch t		
			inner join touch_info ti
			on ti.touch_info_id = t.touch_info_id
			and business_rule_id =85
		) t
		on t.event_participation_id = ep.event_participation_id
	where 
		ep.participation_channel_id <> 3
		and t.touch_id is null
		and g.partner_id =589
	group by 
		ep.event_participation_id
	having 
		count(mh.member_hierarchy_id) >=10
)a

if @@error <> 0
begin
	rollback transaction
	return-2
end

commit transaction
return 0
GO
