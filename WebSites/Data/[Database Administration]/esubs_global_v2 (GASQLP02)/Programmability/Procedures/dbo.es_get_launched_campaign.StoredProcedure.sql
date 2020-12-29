USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_launched_campaign]    Script Date: 02/14/2014 13:05:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create PROCEDURE [dbo].[es_get_launched_campaign]
	@sponsor_id int  --member_hierarchy_id of the sponsor
AS
BEGIN
    SELECT e.event_id
    	    , e.event_name
    	    , e.start_date as launch_date
    	    , e.active
    FROM [group] as g
    	INNER JOIN event_group as eg
    		ON eg.group_id = g.group_id
    	INNER JOIN event as e
    		ON e.event_id = eg.event_id
    WHERE g.sponsor_id = @sponsor_id
END
GO
