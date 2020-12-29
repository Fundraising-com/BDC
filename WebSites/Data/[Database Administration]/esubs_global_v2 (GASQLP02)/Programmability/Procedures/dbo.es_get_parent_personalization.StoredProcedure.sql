USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_parent_personalization]    Script Date: 02/14/2014 13:06:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[es_get_parent_personalization]
	@event_participation_id int
as 

declare @event_id int
declare @parent_member_hierarchy_id int
declare @member_hierarchy_id int
declare @user_type int
declare @part_name varchar(1000)

SET @part_name = ''

select
	@event_id=event_id
	,@parent_member_hierarchy_id = parent_member_hierarchy_id
	,@member_hierarchy_id = m.member_hierarchy_id
from
	event_participation e
	inner join member_hierarchy m
	on e.member_hierarchy_id = m.member_hierarchy_id
where
	@event_participation_id = event_participation_id

SET @user_type = dbo.es_get_user_type(@member_hierarchy_id)

PRINT @user_type

-- when the proc is called for the participant wizard
IF @user_type = 2
BEGIN
    SELECT @part_name = LTRIM(RTRIM(LTRIM(ISNULL(m.first_name,''))) + ' ' + LTRIM(ISNULL(m.middle_name,'') + ' ') + RTRIM(LTRIM(ISNULL(m.last_name,''))))
    FROM member m
        INNER JOIN member_hierarchy mh
            ON mh.member_id = m.member_id
    WHERE mh.member_hierarchy_id = @member_hierarchy_id
END
    
SELECT 
	 personalization_id
    	, p.event_participation_id
	--, CASE WHEN @user_type = 2 THEN ISNULL(@part_name,'') ELSE header_title1 END as header_title1
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
WHERE event_id = @event_id
and ep.member_hierarchy_id = @parent_member_hierarchy_id
GO
