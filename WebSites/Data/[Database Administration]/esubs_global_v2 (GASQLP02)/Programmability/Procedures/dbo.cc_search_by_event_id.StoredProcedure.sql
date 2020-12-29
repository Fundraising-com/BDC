USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_search_by_event_id]    Script Date: 02/14/2014 13:04:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec dbo.cc_search_by_event_id 1002418
CREATE PROCEDURE [dbo].[cc_search_by_event_id] 
	@valeur int
AS
BEGIN

SELECT   g.group_name
        ,e.active
        ,e.event_name
		,e.event_id
        ,CASE WHEN u.first_name COLLATE database_default + ' ' + u.last_name COLLATE database_default IS NULL 
              THEN m.first_name + ' ' + m.last_name 
              ELSE u.first_name COLLATE database_default + ' ' + u.last_name COLLATE database_default 
              END as [name]	
FROM
	[group] g with (nolock) join event_group eg with (nolock)
    on g.group_id = eg.group_id join event e with (nolock)
   	on e.event_id = eg.event_id left join event_participation ep with (nolock)
	on ep.event_id = e.event_id and ep.participation_channel_id = 3 join member_hierarchy mh with (nolock)
   	on mh.member_hierarchy_id = ep.member_hierarchy_id and mh.active = 1 join member m with (nolock)
	on m.member_id = mh.member_id and m.deleted = 0 left join [users] u with (nolock)
	on m.user_id = u.user_id
WHERE e.event_id = @valeur
GROUP BY
         g.group_name
        ,e.active
        ,e.event_name
		,e.event_id
        ,CASE WHEN u.first_name COLLATE database_default + ' ' + u.last_name COLLATE database_default IS NULL 
              THEN m.first_name + ' ' + m.last_name 
              ELSE u.first_name COLLATE database_default + ' ' + u.last_name COLLATE database_default 
              END
END
GO
