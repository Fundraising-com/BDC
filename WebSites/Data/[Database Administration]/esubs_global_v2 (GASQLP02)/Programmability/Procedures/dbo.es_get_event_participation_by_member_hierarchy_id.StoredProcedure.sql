USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_event_participation_by_member_hierarchy_id]    Script Date: 02/14/2014 13:05:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	mod fblais : 2005-12-01 ajout du active pour gérer les unsubscribes
*/
CREATE PROCEDURE [dbo].[es_get_event_participation_by_member_hierarchy_id]
	@member_hierarchy_id int
AS

SELECT     
	ep.event_participation_id
	, ep.salutation
	, e.active
    	, ep.coppa_month
    	, ep.coppa_year
    	, ep.agree_term_services
	, p.personalization_id
	, p.redirect
FROM         
	dbo.member_hierarchy mh
INNER JOIN dbo.event_participation ep
	ON mh.member_hierarchy_id = ep.member_hierarchy_id
left join personalization p
	on p.event_participation_id = ep.event_participation_id
INNER JOIN event e
	ON e.event_id = ep.event_id
WHERE     
	(mh.member_hierarchy_id = @member_hierarchy_id)
  AND 	mh.active = 1
GO
