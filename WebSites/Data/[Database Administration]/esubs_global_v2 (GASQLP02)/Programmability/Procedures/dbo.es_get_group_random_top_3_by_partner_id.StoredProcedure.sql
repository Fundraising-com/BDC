USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_group_random_top_3_by_partner_id]    Script Date: 02/14/2014 13:05:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Stored procedure
-- select * from event_total_amount
--select * from partner where partner_id = 143
--exec [es_get_group_random_top_3_by_partner_id] 143
CREATE PROCEDURE [dbo].[es_get_group_random_top_3_by_partner_id]
    @partner_id int
AS
BEGIN
SELECT top 3 g.group_id
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
		, e.event_id 
    FROM [group]  g with(nolock)
	inner join event_group eg with(nolock) on eg.group_id = g.group_id 
	inner join [event] e with(nolock)on e.event_id = eg.event_id 
	inner join dbo.event_total_amount eta with(nolock) on eta.event_id = eg.event_id
    WHERE g.partner_id =  @partner_id 
		and (total_amount > 1  or total_donation_amount > 1 ) 
		and e.active = 1
    order by newid()

END
GO
