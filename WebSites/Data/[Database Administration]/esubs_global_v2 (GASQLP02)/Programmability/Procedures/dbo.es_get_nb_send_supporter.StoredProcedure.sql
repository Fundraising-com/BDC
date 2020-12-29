USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_nb_send_supporter]    Script Date: 02/14/2014 13:05:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_nb_send_supporter]
	@member_hierarchy_id int
AS
BEGIN
	SET NOCOUNT ON;

      select 
            -- 7/11/2011 (JIRO): There is a bug in the system for INDIVIDUAL GROUPS Kick Off emails, the creation_channel_id is set to the default id of 7.
  count(distinct case when e.event_type_id = 3 then case when mh.creation_channel_id in (7,12,14,29,33, 37) then mh.member_id else null end
            else case when mh.creation_channel_id in(12,14,29,33, 37) then mh.member_id else null end end) as nb_supporters
	  from event_participation ep
		-- enfant
	    inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join member m on m.member_id = mh.member_id
		
	    inner join event_group eg on eg.event_id = ep.event_id 
		inner join [event] e on e.event_id = eg.event_id 
	    inner join [group] g on g.group_id = eg.group_id 
	    
	    -- parent
	    left outer join member_hierarchy mhp on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	    left outer join member mp on mp.member_id = mhp.member_id
	
    where mh.parent_member_hierarchy_id =@member_hierarchy_id
      and mh.active = 1
END
GO
