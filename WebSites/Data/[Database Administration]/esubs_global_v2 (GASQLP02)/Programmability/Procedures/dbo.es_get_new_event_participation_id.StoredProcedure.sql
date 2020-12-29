USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_new_event_participation_id]    Script Date: 02/14/2014 13:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  procedure [dbo].[es_get_new_event_participation_id]
	@event_participation_id int
	,@member_hierarchy_id int
as

declare @nb_campaign int
declare @new_event_participation_id int
declare @new_event_id  int
declare @sponsor_id int
declare @new_member_hierarchy_id int
declare @group_id int

if exists(
	select * 
	from 
		member_hierarchy mh
		inner join creation_channel cc
		on cc.creation_channel_id = mh.creation_channel_id
	where
		member_type_id = 1
	and	mh.member_hierarchy_id = @member_hierarchy_id
)
return -1 -- c'est un sponsor

begin transaction 
-- on regarde s'il est actif ailleur sous son inviteur
select 
	top 1 @new_event_participation_id = event_participation_id
from 
	event_participation ep
	inner join event e
	on e.event_id = ep.event_id
where
	e.active = 1
and 	ep.member_hierarchy_id = @member_hierarchy_id
order by
	ep.create_date

if @new_event_participation_id is not null
begin
	commit transaction
	return @new_event_participation_id
end

-- est-ce que son inviteur est actif dans une autre campagne du groupe

select  top 1
	@new_event_id = ep.event_id
from 
	event_participation ep
	inner join event e
	on e.event_id=ep.event_id
where
	member_hierarchy_id = (select parent_member_hierarchy_id from member_hierarchy where member_hierarchy_id = @member_hierarchy_id )
	and e.active =1
order by 
	e.create_date
-- on l'insère donc dans la campagne sous le même inviteur
if @new_event_id  is not null
begin
	insert into event_participation(event_id, member_hierarchy_id,participation_channel_id,create_date)
	select @new_event_id,member_hierarchy_id,participation_channel_id,getdate()
	from event_participation
	where event_participation_id = @event_participation_id

	if @@error <> 0
	begin 
		rollback transaction
		return -2
	end
	else
	begin
		set @new_event_participation_id = @@identity
		commit transaction
		return @new_event_participation_id 		
	end
end
-- si le groupe a une campagne d'active, on l'insère 
-- sous le sponsor avec le bon creation_channel
select 
	@group_id = group_id 
from 
	event_participation ep
	inner join event_group eg
	on eg.event_id = ep.event_id
where ep.event_participation_id = @event_participation_id

select
	top 1
	 @new_event_id = e.event_id
	, @sponsor_id =g.sponsor_id
from 
	[group] g
	inner join event_group eg
	on eg.group_id = g.group_id
	inner join event e
	on eg.event_id = e.event_id
where g.group_id = @group_id
and e.active = 1
order by e.create_date

if @new_event_id is not null
begin
	-- on doit lui créer un nouveau member_hierarchy
	insert into member_hierarchy(
		parent_member_hierarchy_id
		, member_id
		, creation_channel_id
		, create_date
	)
	select
		@sponsor_id
		, member_id
		-- si on déplace un supporter sous un sponsor
		, case when cc.member_type_id = 3 then 29 else mh.creation_channel_id end
		, getdate()
	from 
		member_hierarchy mh
		left outer join creation_channel cc
		on cc.creation_channel_id =  mh.creation_channel_id
	where 
		mh.member_hierarchy_id = @member_hierarchy_id

	set @new_member_hierarchy_id = @@identity
	if @@error <> 0
	begin 
		rollback transaction
		return -3
	end
	else
	begin
		set @new_member_hierarchy_id = @@identity
	end

	insert into event_participation(
		event_id
		, member_hierarchy_id
		, participation_channel_id
		, create_date
	)
		select
			@new_event_id
			,@new_member_hierarchy_id
			, participation_channel_id
			, getdate()
		from 
			event_participation ep
		where 
			ep.event_participation_id = @event_participation_id
	if @@error <> 0
	begin 
		rollback transaction
		return -4
	end
	else
	begin
		set @new_event_participation_id = @@identity
		commit transaction
		return @new_event_participation_id 		
	end

end

commit transaction
-- aucun n'a été trouvé
return 0
GO
