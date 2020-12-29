USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_site_personalization]    Script Date: 02/14/2014 13:06:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[es_get_site_personalization]
	@event_participation_id int
as
BEGIN
declare @event_id int
declare @parent_member_hierarchy_id int

select
	@event_id=event_id
	,@parent_member_hierarchy_id = parent_member_hierarchy_id
from
	event_participation e
	inner join member_hierarchy m
	on e.member_hierarchy_id = m.member_hierarchy_id
where
	event_participation_id = @event_participation_id

-- if we have already the sponsor no need to search further
IF @parent_member_hierarchy_id IS NOT NULL
BEGIN
	WHILE NOT EXISTS(
		SELECT personalization_id 
		FROM personalization p
			inner join event_participation ep
			on p.event_participation_id = ep.event_participation_id
		WHERE 	event_id =@event_id
		and  	ep.member_hierarchy_id = @parent_member_hierarchy_id
		) AND @parent_member_hierarchy_id IS NOT NULL
	BEGIN
		select
			@event_id=event_id
			,@parent_member_hierarchy_id = parent_member_hierarchy_id
		from
			event_participation e
			inner join member_hierarchy m
				on e.member_hierarchy_id = m.member_hierarchy_id
		WHERE m.member_hierarchy_id = @parent_member_hierarchy_id
		
	END
END

IF EXISTS(SELECT personalization_id FROM 
	personalization p
	inner join event_participation ep
	on p.event_participation_id = ep.event_participation_id
WHERE 
	event_id =@event_id
and  	ep.member_hierarchy_id = @parent_member_hierarchy_id)
BEGIN
SELECT 
	 personalization_id
	, p.event_participation_id
	, header_title1
	, header_title2
	, body
	, fundraising_goal
	, site_bgcolor
	, header_bgcolor
	, header_color
	, group_url
	, image_url
	, image_motivator
    , redirect
    , displayGroupMessage
    , remind_later
FROM 
	personalization p
	inner join event_participation ep
	on p.event_participation_id = ep.event_participation_id
WHERE 
	event_id =@event_id
and  	ep.member_hierarchy_id = @parent_member_hierarchy_id
END
ELSE
BEGIN
-- MODIF MEL 
-- FOR UNKNOWN SUPPORTERS THAT ARE COMMING FROM THE FIND MY GROUP
IF EXISTS (
	SELECT 
		personalization_id
		, event_participation_id
		, header_title1
		, header_title2
		, body
		, fundraising_goal
		, site_bgcolor
		, header_bgcolor
		, header_color
		, group_url
		, image_url
        , image_motivator
        , redirect
        , displayGroupMessage
        , remind_later
	FROM personalization
	WHERE event_participation_id = @event_participation_id)
BEGIN
	SELECT 
		personalization_id
		, event_participation_id
		, header_title1
		, header_title2
		, body
		, fundraising_goal
		, site_bgcolor
		, header_bgcolor
		, header_color
		, group_url
		, image_url
        , image_motivator
        , redirect
        , displayGroupMessage
        , remind_later
	FROM personalization
	WHERE event_participation_id = @event_participation_id
	
END
ELSE
BEGIN
-- END MODIF MEL
	    DECLARE @part_name as varchar(1000)
	    DECLARE @group_name as varchar(255)
	    
	    SELECT @part_name = m.first_name + ' ' + m.last_name
	           ,@group_name = g.group_name
	    FROM event_participation AS ep
	        INNER JOIN event_group as eg
	            ON eg.event_id = ep.event_id
	        INNER JOIN [group] as g
	            ON g.group_id = eg.group_id
	        INNER JOIN member_hierarchy AS mh
	            ON mh.member_hierarchy_id = ep.member_hierarchy_id
	        LEFT JOIN member_hierarchy AS mhp
	            ON mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	        LEFT JOIN member as m
	            ON m.member_id = mhp.member_id
	    WHERE ep.event_participation_id = @event_participation_id
	   
		-- if we still can't find a customization we should give default value.
		SELECT 
			0 as personalization_id
			, @event_participation_id as event_participation_id
			, 'Fundraising page of ' + @part_name as header_title1
			, @group_name as header_title2
			, 'Welcome to our Fundraising web site. With your help, we are sure to earn the funds we need this year! Thank you for your support!' as body
			, NULL as fundraising_goal
			, 'A5A5A5' as site_bgcolor
			, 'EB3E30' as header_bgcolor
			, 'FFFFFF' as header_color
			, NULL as group_url
			, '' as image_url
            , 1 as image_motivator
            , '' as redirect
            , 1 as displayGroupMessage
            , 0 as remind_later
END
END
END
GO
