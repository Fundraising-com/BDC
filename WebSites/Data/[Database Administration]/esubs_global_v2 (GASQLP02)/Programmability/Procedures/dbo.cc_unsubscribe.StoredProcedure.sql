USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_unsubscribe]    Script Date: 02/14/2014 13:04:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cc_unsubscribe]
	@event_participation_id int,
    @is_unsubscribe bit --1 = unsubscribe, 0 = resubsribe
as

declare @member_id int, @user_id int
declare @member_hierarchy_id int

select 	@member_id = mh.member_id, @member_hierarchy_id = mh.member_hierarchy_id, @user_id = m.user_id
from	event_participation ep with (nolock) join
        member_hierarchy mh with (nolock) on ep.member_hierarchy_id = mh.member_hierarchy_id join
        member m with (nolock) on mh.member_id = m.member_id
where	event_participation_id = @event_participation_id

if @is_unsubscribe = 1
begin
   update member set opt_status_id = 2
   where member_id = @member_id
   
   if @user_id is not null
   begin
	  update users set opt_status_id = 0, unsubscribe = 1, unsubscribe_date=GETDATE()
      where user_id = @user_id
   end

   update member_hierarchy
   set unsubscribe=1
   where member_hierarchy_id = @member_hierarchy_id

   --on va annuler les touch
   update touch 
   set processed = 8 
   where event_participation_id = @event_participation_id
end
else
begin
   update member set opt_status_id = 1
   where member_id = @member_id
   
   if @user_id is not null
   begin
	  update users set opt_status_id = 1, unsubscribe = 0, unsubscribe_date = NULL
      where user_id = @user_id
   end

   update member_hierarchy
   set unsubscribe=0
   where member_hierarchy_id = @member_hierarchy_id
end
GO
