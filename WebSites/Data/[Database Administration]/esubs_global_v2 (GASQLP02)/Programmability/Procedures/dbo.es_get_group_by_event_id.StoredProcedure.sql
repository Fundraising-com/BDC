USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_group_by_event_id]    Script Date: 02/14/2014 13:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Stored procedure
/*
	Created by: Philippe Girard
	Created on: November 8 2005
*/
create PROCEDURE [dbo].[es_get_group_by_event_id]
	@event_id int
AS
BEGIN
SELECT     
	g.group_id
	, g.parent_group_id
	, g.sponsor_id
	, g.partner_id
	, g.lead_id
	, g.external_group_id
	, g.group_name
	, g.test_group
	, g.expected_membership
	, g.redirect
	, g.group_url
	, g.comments
	, g.create_date
FROM         
	[group] g 
    INNER JOIN event_group eg
        ON eg.group_id = g.group_id
WHERE     
	(eg.event_id = @event_id)
END
GO
