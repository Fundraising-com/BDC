USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_member]    Script Date: 03/11/2015 16:36:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
	Created by:
	Date:
	mod fblais : 2005-12-01 ajout du active pour gérer les unsubscribes
	
	EXEC [dbo].[es_get_member] 9487395
*/	
ALTER PROCEDURE [dbo].[es_get_member]
	@intHierarchyID int
AS

declare @userType int
set @userType = dbo.es_get_user_type(@intHierarchyID)

SELECT    
	mh.member_hierarchy_id
	, mh.parent_member_hierarchy_id
	, mh.member_id
	, mh.creation_channel_id
	, CASE WHEN @userType = 3 THEN m.culture_code COLLATE DATABASE_DEFAULT ELSE ISNULL(u.culture_code, m.culture_code) COLLATE DATABASE_DEFAULT END as culture_code
	, CASE WHEN @userType = 3 THEN m.opt_status_id ELSE u.opt_status_id END as opt_status_id
	, CASE WHEN @userType = 3 THEN [dbo].[fn_RemoveExtraChars](m.first_name) COLLATE DATABASE_DEFAULT ELSE [dbo].[fn_RemoveExtraChars](ISNULL(u.first_name, m.first_name)) COLLATE DATABASE_DEFAULT END as first_name
	, m.middle_name
	, CASE WHEN @userType = 3 THEN [dbo].[fn_RemoveExtraChars](m.last_name) COLLATE DATABASE_DEFAULT ELSE [dbo].[fn_RemoveExtraChars](ISNULL(u.last_name, m.last_name)) COLLATE DATABASE_DEFAULT END as last_name
	, m.gender
	, CASE WHEN @userType = 3 THEN m.email_address COLLATE DATABASE_DEFAULT ELSE ISNULL(u.email_address, m.email_address) COLLATE DATABASE_DEFAULT END as email_address
	, CASE WHEN @userType = 3 THEN m.password COLLATE DATABASE_DEFAULT ELSE ISNULL(u.password, m.password) COLLATE DATABASE_DEFAULT END as password
	, m.bounced
	, m.parent_first_name
	, m.parent_last_name
	, m.external_member_id
	, CASE WHEN @userType = 3 THEN m.partner_id ELSE ISNULL(u.partner_id, m.partner_id) END as partner_id
	, m.comments
	, m.lead_id
	, m.facebook_id
	, cc.creation_channel_name
	, cc.description
	, cc.active
	, mh.unsubscribe
	, @userType as user_type
	, m.create_date as member_created_date
	, mh.create_date as member_hierarchy_created_date
    , m.deleted
    , m.[user_id]
FROM         
	member_hierarchy mh
	INNER JOIN member m
	ON mh.member_id = m.member_id 
	LEFT OUTER JOIN creation_channel cc
	ON mh.creation_channel_id = cc.creation_channel_id
	LEFT OUTER JOIN users u
	ON m.[user_id] = u.[user_id]
WHERE     
	(mh.member_hierarchy_id = @intHierarchyID)
and	mh.active = 1
