USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_user]    Script Date: 02/14/2014 13:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Created by:
	Date:
	mod fblais : 2005-12-01 ajout du active pour gérer les unsubscribes
	
	exec [dbo].[es_get_user] 1015783
*/	
CREATE PROCEDURE [dbo].[es_get_user]
	@intHierarchyID int
AS

declare @userType int
set @userType = dbo.es_get_user_type(@intHierarchyID)

SELECT    
	  mh.member_hierarchy_id
	, mh.parent_member_hierarchy_id
	, mh.member_id
	, mh.creation_channel_id
	, COALESCE(u.culture_code COLLATE database_default,m.culture_code COLLATE database_default) as culture_code
	, COALESCE(u.opt_status_id,m.opt_status_id) as opt_status_id
	, COALESCE(u.first_name COLLATE database_default,m.first_name COLLATE database_default) as first_name
	, m.middle_name
	, COALESCE(u.last_name COLLATE database_default,m.last_name COLLATE database_default) as last_name
	, m.gender
	, COALESCE(u.email_address COLLATE database_default,m.email_address COLLATE database_default) as email_address
	, COALESCE(u.password COLLATE database_default,m.password COLLATE database_default) as password
	, m.bounced
	, m.parent_first_name
	, m.parent_last_name
	, m.external_member_id
	, COALESCE(u.partner_id,m.partner_id) as partner_id
	, m.comments
	, m.lead_id
	, m.facebook_id
	, cc.creation_channel_name
	, cc.description
	, cc.active
	, mh.unsubscribe
	, @userType as user_type
	, COALESCE(u.create_date,m.create_date) as member_created_date
	, mh.create_date as member_hierarchy_created_date
FROM         
	member_hierarchy mh with (nolock)
	INNER JOIN member m with (nolock)
	ON mh.member_id = m.member_id and m.deleted = 0
	JOIN creation_channel cc with (nolock)
	ON mh.creation_channel_id = cc.creation_channel_id
	LEFT JOIN users u with (nolock)
    on m.user_id = u.user_id
WHERE     
	(mh.member_hierarchy_id = @intHierarchyID) and mh.active = 1
GO
