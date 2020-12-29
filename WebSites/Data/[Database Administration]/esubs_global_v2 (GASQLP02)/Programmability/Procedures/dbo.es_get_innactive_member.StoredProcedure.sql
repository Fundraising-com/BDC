USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_innactive_member]    Script Date: 02/14/2014 13:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	mod fblais : 2005-12-01 ajout du active pour gérer les unsubscribes
*/
CREATE procedure [dbo].[es_get_innactive_member]
	@event_participation_id int
	, @member_type_id tinyint
as

declare @event_id int
declare @member_hierarchy_id int

select
	@member_hierarchy_id = member_hierarchy_id
	,@event_id = event_id
from 
	event_participation 
where 
	event_participation_id = @event_participation_id

select
	mh.member_hierarchy_id
from 
	member_hierarchy mh
	left outer join event_participation ep
	on ep.member_hierarchy_id = mh.member_hierarchy_id
	and ep.event_id = @event_id
	left outer join creation_channel cc
	on cc.creation_channel_id = mh.creation_channel_id
where
	mh.parent_member_hierarchy_id = @member_hierarchy_id
and	ep.event_id is null
and 	cc.member_type_id = @member_type_id
and 	mh.active =1
GO
