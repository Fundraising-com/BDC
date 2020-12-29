USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_group_by_email]    Script Date: 02/14/2014 13:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
    Created by: Philippe Girard
    Created on: 24 august 2005

    
*/
CREATE PROCEDURE [dbo].[es_get_group_by_email]
    @partner_id int
    ,@email varchar(100)
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
            INNER JOIN member_hierarchy mh
                ON mh.member_hierarchy_id = g.sponsor_id
            INNER JOIN member m
                ON m.member_id = mh.member_id
    WHERE     
          g.partner_id = @partner_id
      AND m.email_address = @email
END
GO
