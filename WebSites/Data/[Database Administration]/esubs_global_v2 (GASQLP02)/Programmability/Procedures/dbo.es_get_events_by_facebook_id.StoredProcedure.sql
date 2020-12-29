USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_events_by_facebook_id]    Script Date: 02/14/2014 13:05:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
    Name: es_get_event_by_order_date
    Created by: Philippe Girard
    Created on: 2007-07-17
    Description:
    
*/

CREATE PROCEDURE [dbo].[es_get_events_by_facebook_id]
	@facebookID int
AS
BEGIN

    select e.event_id
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
	    , ep.event_participation_id  as sponsor_event_participation_id
	    , e.redirect
        , e.group_type_id
        , e.discount_site
    from event e
        inner join event_type et
            on et.event_type_id = e.event_type_id
        inner join event_group eg
            on eg.event_id = e.event_id
        inner join [group] g
            on g.group_id = eg.group_id
        inner join event_participation ep
            on ep.event_id = e.event_id
	inner join member_hierarchy mh 
	    on g.sponsor_id = mh.member_hierarchy_id
	inner join member m
	    on m.member_id = mh.member_id
    where m.facebook_id = @facebookID

END
GO
