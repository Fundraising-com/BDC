USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_search_by_email]    Script Date: 02/14/2014 13:04:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[cc_search_by_email] 
	@valeur varchar(50)
AS
BEGIN
Select 
    g.group_name
    ,e.active
    ,e.event_name
	,e.event_id
    ,m2.first_name + ' ' + m2.last_name [name]
From
	[group] g
    inner join event_group eg
        on g.group_id = eg.group_id
	inner join event e
   	    on e.event_id = eg.event_id
    inner join event_participation ep
        on ep.event_id = e.event_id
    inner join member_hierarchy mh
	    on mh.member_hierarchy_id = ep.member_hierarchy_id and mh.active = 1
	inner join member m
	    on m.member_id = mh.member_id
    inner JOIN dbo.member_hierarchy mh2
        ON g.sponsor_id = mh2.member_hierarchy_id 
    inner JOIN dbo.member m2 ON mh2.member_id = m2.member_id
where m.email_address = @valeur
group by
    g.group_name
    ,e.active
    ,e.event_name
	,e.event_id
    ,m2.first_name + ' ' + m2.last_name
end
GO
