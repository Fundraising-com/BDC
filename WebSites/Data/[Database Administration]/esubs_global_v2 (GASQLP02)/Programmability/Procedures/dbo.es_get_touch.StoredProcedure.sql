USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_touch]    Script Date: 02/14/2014 13:06:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
mod fblais : 2005-12-01 ajout du active pour gérer les unsubscribes

mod jfbuist: 4 avril 2011, added touch_id and processed as part of the address bool project.

*/
CREATE PROCEDURE [dbo].[es_get_touch]
	@touch_id int
AS
BEGIN
SELECT 
	t.touch_id
	, t.processed
	,ep.event_participation_id
	,ep.event_id
	,mh.member_hierarchy_id
	,mh.member_id
FROM
	touch t
	inner join event_participation ep
	on ep.event_participation_id = t.event_participation_id
	inner join member_hierarchy mh
	on mh.member_hierarchy_id = ep.member_hierarchy_id
WHERE
	t.touch_id = @touch_id
and	mh.active =1
END
GO
