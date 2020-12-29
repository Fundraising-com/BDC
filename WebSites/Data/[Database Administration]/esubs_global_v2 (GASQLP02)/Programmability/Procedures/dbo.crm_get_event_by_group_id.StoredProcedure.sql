USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[crm_get_event_by_group_id]    Script Date: 02/14/2014 13:05:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[crm_get_event_by_group_id]
	@groupID int
AS
BEGIN
SELECT     
	  e.event_id
	, e.event_type_id
	, e.culture_code
	, e.event_name
	, e.start_date
	, e.end_date
--	, e.financial_goal
--	, e.image_url
	, e.active
	, e.comments
	, e.create_date
	, et.event_type_name
	, eg.group_id
	, g.partner_id
	, ep.event_participation_id  as sponsor_event_participation_id
	, e.redirect
    , e.group_type_id
	, e.group_type_id 
	, e.[profit_id]
	, e.[profit_calculated]
	, e.[processing_fee]
FROM         
	dbo.event e
	INNER JOIN dbo.event_group  eg
	ON e.event_id = eg.event_id 
	INNER JOIN dbo.event_type et
	ON e.event_type_id = et.event_type_id
	inner join event_participation ep
	on ep.event_id = eg.event_id
	and ep.participation_channel_id = 3
	inner join [group] g
	on g.group_id = eg.group_id
WHERE    
	 (eg.group_id = @groupID)
ORDER BY event_participation_id desc
	
END
GO
