USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_merge_group]    Script Date: 02/14/2014 13:04:56 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE
	procedure [dbo].[cc_merge_group]
	@good_group_id int
	,@good_event_id int
	,@rip_group_id int
	,@rip_event_id int
	,@userName varchar(50)
	,@mergedComments nvarchar(200)
as

declare @old_sponsor_id int
declare @new_sponsor_id int

begin transaction

/* on prend son nouveau sponsor*/
select 
	@new_sponsor_id = sponsor_id 
from 
	[group]
where 
	group_id = @good_group_id

if @@error <> 0
begin
	rollback transaction 
	return -1
end
/*on prend son ancien sponsor*/
select 
	@old_sponsor_id = sponsor_id 
from 
	[group]
where 
	group_id = @rip_group_id
if @@error <> 0
begin
	rollback transaction 
	return -2
end
/* on change de event tous les gens sous l'ancien sponsor et leurs enfants*/

update event_participation
set event_id = @good_event_id
where event_participation_id in(
	select 
		event_participation_id 
	from 
		event_participation 
	where 
		event_id in(
	
		select 
			event_id 
		from 
			event_group 
		where 
			group_id = @rip_group_id
		)
	and 	(participation_channel_id <> 3 
	or participation_channel_id is null)
)
if @@error <> 0
begin
	rollback transaction 
	return -3
end
/*
on update les membres de l'ancien sponsor
vers le nouveau sponsor pour bouger tout l'arbre dans le nouveau groupe
*/

update member_hierarchy
set parent_member_hierarchy_id = @new_sponsor_id
where parent_member_hierarchy_id =@old_sponsor_id
if @@error <> 0
begin
	rollback transaction 
	return -4
end
/* on rend innactive toutes les anciennes campagnes */

update event
set end_date = getdate() , active = 0
where event_id in(
	select 
		event_id
	from 
		event_group
	where 
		group_id = @rip_group_id 
)

if @@error <> 0
begin
	rollback transaction 
	return -5
end

--insert campaign_merges
--set original_campaign = @good_event_id , old_campaign =@rip_event_id, date_changed=getdate(), user_name=@userName

INSERT INTO campaign_merges values (@good_event_id, @rip_event_id,@userName, GETDATE(), @mergedComments)


if @@error <> 0
begin
	rollback transaction 
	return -5
end

commit transaction
return 0
GO
