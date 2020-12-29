USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_event_participation_by_touch_id]    Script Date: 02/14/2014 13:05:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_event_participation_by_touch_id]
	@touch_id int
AS
BEGIN
SELECT     
	ep.event_participation_id
	, ep.event_id
	, ep.member_hierarchy_id
	, ep.participation_channel_id
	, ep.salutation
	, pc.participation_channel_name	
    , ep.coppa_month
    , ep.coppa_year
    , ep.agree_term_services
FROM         
	event_participation ep
LEFT JOIN participation_channel pc
	ON ep.participation_channel_id = pc.participation_channel_id 
INNER JOIN touch t
	ON ep.event_participation_id = t.event_participation_id
inner join member_hierarchy mh
	on mh.member_hierarchy_id = ep.member_hierarchy_id
WHERE     
	(t.touch_id = @touch_id)
END
GO
