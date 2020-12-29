USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_event_participation_by_event_participation_id]    Script Date: 02/14/2014 13:05:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	mod fblais : 2005-12-01 ajout du active pour gérer les unsubscribes
*/
CREATE PROCEDURE [dbo].[es_get_event_participation_by_event_participation_id]
	@eventParticipationID int
AS
BEGIN
SELECT     
	ep.event_participation_id
	, ep.event_id
	, ep.member_hierarchy_id
	, ep.participation_channel_id
	, ep.create_date
	, ep.salutation
	, pc.participation_channel_name
	, e.active
    	, ep.coppa_month
    	, ep.coppa_year
    	, ep.agree_term_services
    	, p.personalization_id
    	, p.redirect
	,ep.holiday_reminders
FROM         
	event_participation ep
inner join member_hierarchy mh
	on mh.member_hierarchy_id = ep.member_hierarchy_id
left join personalization p
	on p.event_participation_id = ep.event_participation_id
inner join event e
	on e.event_id = ep.event_id
left outer JOIN participation_channel pc
	ON ep.participation_channel_id = pc.participation_channel_id 
WHERE     
	(ep.event_participation_id = @eventParticipationID)
and 	mh.active = 1
END
GO
