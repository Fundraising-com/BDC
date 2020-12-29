USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_group]    Script Date: 02/14/2014 13:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
	Created by: JF Buist
	Date:
*/
CREATE PROCEDURE [dbo].[es_get_group]
	@groupID int
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
--	, gt.group_type_name
--	, gt.first_level_display
FROM         
	[group] g 
--	INNER JOIN group_type gt
--	ON g.group_type_id = gt.group_type_id
WHERE     
	(g.group_id = @groupID)
END
GO
