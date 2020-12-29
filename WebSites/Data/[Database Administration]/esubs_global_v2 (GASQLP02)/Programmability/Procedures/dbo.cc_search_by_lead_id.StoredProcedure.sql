USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_search_by_lead_id]    Script Date: 03/06/2015 16:25:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--dbo.cc_search_by_group_name 'mel'
ALTER     PROCEDURE [dbo].[cc_search_by_lead_id]
	@valeur int
AS
BEGIN

Select 
         g.group_name
        ,e.active
        ,e.event_name
	,e.event_id
        ,m.first_name + ' ' + m.last_name as [name]
	
From
	[group] g
        inner join event_group eg
           on g.group_id = eg.group_id
		join event e
   	   on e.event_id = eg.event_id
        join event_participation ep
	   on ep.event_id = e.event_id
           and ep.participation_channel_id = 3
		join member_hierarchy mh
   	   on mh.member_hierarchy_id = ep.member_hierarchy_id and mh.active = 1
		join member m
	   on m.member_id = mh.member_id
       
where g.lead_id = @valeur

group by
         g.group_name
        ,e.active
        ,e.event_name
	,e.event_id
        ,m.first_name + ' ' + m.last_name



end






