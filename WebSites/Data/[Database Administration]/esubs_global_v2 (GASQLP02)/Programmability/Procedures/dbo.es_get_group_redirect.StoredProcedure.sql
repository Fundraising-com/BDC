USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_group_redirect]    Script Date: 02/14/2014 13:05:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	retourne un group id pour savoir quel event loader
	mod fblais 2005-11-29
	exec [dbo].[es_get_group_redirect] 'kappa'
*/
CREATE procedure [dbo].[es_get_group_redirect]
	@redirect varchar(255)
as
select top 1
	g.group_id
	, e.event_id
from
	[group] as g
	inner join event_group eg
		on eg.group_id = g.group_id
	inner join event e
		on e.event_id = eg.event_id
		and e.active=1
	inner join event_participation ep
		on ep.event_id = e.event_id and ep.participation_channel_id = 3
	inner join personalization p
		on ep.event_participation_id = p.event_participation_id
where
	p.redirect=@redirect
order by e.start_date desc
GO
