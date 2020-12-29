USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_event_by_event_id]    Script Date: 02/14/2014 13:05:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Created by: JF Buist
	Date:

	Description:
*/
CREATE PROCEDURE [dbo].[es_get_event_by_event_id]
	@eventID int
AS
BEGIN
DECLARE @t_eventID int
SET @t_eventID = @eventID
SELECT    
	e.event_id
    , e.event_status_id
	, e.culture_code
	, e.event_name
	, e.start_date
	, e.end_date	
	, e.active
	, e.comments
	, e.create_date
	, es.event_status_name 
	, eg.group_id
	, g.partner_id
	, ep.event_participation_id  as sponsor_event_participation_id
	, e.redirect
    , e.group_type_id
	, e.[profit_group_id]
    , e.[profit_calculated]
    , e.[processing_fee]
    , e.event_type_id
	, e.donation
	, e.date_of_event
    , et.event_type_name
    , e.discount_site
FROM         
	event e 
	INNER JOIN event_group eg
	ON e.event_id = eg.event_id 
    INNER JOIN event_status es
    ON es.event_status_id = e.event_status_id 
	INNER JOIN event_type et 
	ON e.event_type_id = et.event_type_id
	inner join [group] g
	on g.group_id = eg.group_id
	inner join event_participation ep
	on ep.event_id = eg.event_id
	and ep.participation_channel_id = 3
WHERE     
	(eg.event_id = @t_eventID)
END
GO
