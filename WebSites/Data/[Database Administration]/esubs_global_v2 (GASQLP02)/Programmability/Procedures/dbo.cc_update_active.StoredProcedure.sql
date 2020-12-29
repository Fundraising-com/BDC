USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_update_active]    Script Date: 02/14/2014 13:04:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cc_update_active]
	@event_participation_id int
as

declare @member_hierarchy_id int

select
	@member_hierarchy_id = member_hierarchy_id
from 
	event_participation
where 
	event_participation_id = @event_participation_id

if exists(select * from member_hierarchy where parent_member_hierarchy_id = @member_hierarchy_id)
return -1

--if exists( select * from qspstore.dbo.totalspersaletable where suppid= @event_participation_id)
--return -2

begin transaction

update member_hierarchy
set unsubscribe=1
where member_hierarchy_id = @member_hierarchy_id

if @@error <> 0
begin
	rollback transaction
	return -3
end

update member_hierarchy
set active=0
where member_hierarchy_id = @member_hierarchy_id

if @@error <> 0
begin
	rollback transaction
	return -3
end

--on va annuler les touch
update touch 
set processed =1 
where event_participation_id =  @event_participation_id

if @@error <> 0
begin
	rollback transaction
	return -3
end

commit transaction
return 0
GO
