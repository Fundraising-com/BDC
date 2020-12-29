USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_event_by_group_id]    Script Date: 02/14/2014 13:05:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[es_get_event_by_group_id]    Script Date: 08/18/2010 13:33:37 ******/

/*
	Created by: JF Buist
	Date:

	Description: 
*/
CREATE PROCEDURE [dbo].[es_get_event_by_group_id]
	@groupID int
AS
BEGIN
SELECT     
	  e.event_id
	, e.event_status_id
	, e.culture_code
	, e.event_name
	, e.start_date
	, e.end_date
--	, e.financial_goal
--	, e.image_url
	, e.active
	, e.comments
	, e.create_date
	, es.event_status_name
	, eg.group_id
	, g.partner_id
	, ep.event_participation_id  as sponsor_event_participation_id
	, p.redirect
    , e.group_type_id
	, e.group_type_id 
	, e.[profit_group_id]
	, e.[profit_calculated]
	, e.[processing_fee]
    , e.event_type_id
	, e.donation
    , et.event_type_name
    , e.discount_site
FROM         
	dbo.event e
INNER JOIN dbo.event_group  eg
	ON e.event_id = eg.event_id 
INNER JOIN event_status es
    	ON es.event_status_id = e.event_status_id 
INNER JOIN dbo.event_type et
	ON e.event_type_id = et.event_type_id
INNER JOIN event_participation ep
	ON ep.event_id = eg.event_id and ep.participation_channel_id = 3
INNER JOIN [group] g
	ON g.group_id = eg.group_id
LEFT JOIN personalization p
	ON ep.event_participation_id = p.event_participation_id
WHERE    
	 (eg.group_id = @groupID)

ORDER BY e.active DESC
END
GO
