USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_search_by_member_name]    Script Date: 03/06/2015 22:59:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--dbo.cc_search_by_member_name 'mel'
ALTER     PROCEDURE [dbo].[cc_search_by_member_name]
	@valeur varchar(50)
AS
BEGIN

Select 
         g.group_name
        ,e.active
        ,e.event_name
		,e.event_id
        ,u.first_name + ' ' + u.last_name [name]

From
	[group] g
        join event_group eg
           on g.group_id = eg.group_id
		join event e
   	   on e.event_id = eg.event_id
        join event_participation ep
           on ep.event_id = e.event_id
        join member_hierarchy mh
	   on mh.member_hierarchy_id = ep.member_hierarchy_id and mh.active = 1
		join member m
	   on m.member_id = mh.member_id
	    join users u
	   on m.user_id = u.user_id
         
where (u.first_name like '%'+@valeur+'%' or u.last_name like '%'+@valeur+'%')

group by
         g.group_name
        ,e.active
        ,e.event_name
		,e.event_id
        ,u.first_name + ' ' + u.last_name



end






