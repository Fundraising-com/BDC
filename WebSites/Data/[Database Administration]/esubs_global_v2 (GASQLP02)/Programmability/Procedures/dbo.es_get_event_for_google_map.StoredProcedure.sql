USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_event_for_google_map]    Script Date: 02/14/2014 13:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Created by: JF Buist
	Date:

	Description:
*/
CREATE PROCEDURE [dbo].[es_get_event_for_google_map]
AS
BEGIN
SELECT distinct
	e.event_id
	, e.event_type_id
	, e.culture_code
	, e.event_name
	, e.start_date
	, e.end_date	
	, e.active
	, e.comments
	, e.create_date
	, et.event_type_name
	, eg.group_id
	, g.partner_id
	, 0  as sponsor_event_participation_id
	, e.redirect
    , e.group_type_id
    , e.discount_site
FROM         
	event e 
inner join event_group eg
	on e.event_id = eg.event_id
INNER JOIN event_type et 
	ON e.event_type_id = et.event_type_id
inner join [group] g
	on eg.group_id = g.group_id
--inner join event_participation ep
--	on ep.event_id = eg.event_id
inner join partner p
	on g.partner_id = p.partner_id
WHERE     
	e.create_date > dateadd(year, -1, getdate())
and
	p.partner_id = 0

END
GO
