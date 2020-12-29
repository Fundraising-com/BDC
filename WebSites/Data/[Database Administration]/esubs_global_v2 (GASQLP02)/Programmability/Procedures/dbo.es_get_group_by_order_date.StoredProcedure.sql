USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_group_by_order_date]    Script Date: 02/14/2014 13:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_group_by_order_date] @startDate datetime, @endDate datetime AS

    SELECT distinct
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
    FROM [group] g 
	inner join event_group eg 
		on eg.group_id = g.group_id
	INNER JOIN event e 
		on eg.event_id = e.event_id
	inner join event_participation ep 
		on e.event_id = ep.event_id
	INNER JOIN QSPEcommerce.dbo.EfundraisingTransaction et 
		ON ep.event_participation_id = et.SuppId
WHERE (et.CreateDate > @startDate) 
	AND (et.CreateDate < @endDate)
GO
