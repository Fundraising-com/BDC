USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cm_get_group_by_lead_id]    Script Date: 02/14/2014 13:05:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Updated by Melissa Cote 
Update date 2010.09.27

Change completely the logic to revert back to original logic 


EXEC dbo.cm_get_group_by_lead_id
 @lead_id = 867172, -- int
    @email_address = '', -- varchar(50)
    @first_name = '', -- varchar(50)
    @last_name = '' -- varchar(50)
*/
CREATE   PROCEDURE [dbo].[cm_get_group_by_lead_id] 
--524373, 'mkettell@suffolk.lib.ny.us'
    @lead_id int,
    @email_address varchar(50),
    @first_name varchar(50) = null,
    @last_name varchar(50) = null

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
        , m.email_address as username
        , m.password
        , e.active
    FROM         
    	[group] g 
            inner join member_hierarchy mh
                on mh.member_hierarchy_id = g.sponsor_id
            inner join member m
                on m.member_id = mh.member_id
        INNER JOIN dbo.event_group eg ON eg.group_id = g.group_id
        INNER JOIN dbo.event e ON e.event_id= eg.event_id
		
    WHERE     
    	g.lead_id = @lead_id --and m.email_address = @email_address
   	order by e.active DESC , g.group_id desc
END
GO
