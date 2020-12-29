USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_382]    Script Date: 02/14/2014 13:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_param_email_382]
	@identification int
	,@source_id bigint
AS
BEGIN
    -- Prepare Table rows

	SELECT @identification  as identification
		, @source_id as source_id
		, ep.salutation as participant_name
		, ep.salutation as sponsor_name
		, 382 as email_template_id
		, m.email_address as email
		, m.[password] as pass
		, e.redirect 
		, pav.value as partner_name
        , m.[password] as participant_password
        , e.redirect as event_redirect
        , '' as table_rows
        , '' as contactname
	FROM member m with(nolock)
		INNER JOIN member_hierarchy mh with(nolock)
		    ON mh.member_id = m.member_id
		INNER JOIN event_participation ep with(nolock)
		    ON ep.member_hierarchy_id = mh.member_hierarchy_id
		INNER JOIN event e with(nolock)
		    ON ep.event_id = e.event_id
		INNER JOIN event_group eg with(nolock)
		    ON eg.event_id = e.event_id
		INNER JOIN [group] g with(nolock)
		    ON g.group_id = eg.group_id	
		INNER JOIN partner_attribute_value pav with(nolock)
		    ON pav.partner_id = g.partner_id
		    AND pav.culture_code COLLATE Latin1_General_CI_AS = e.culture_code
		    AND partner_attribute_id = 3
	WHERE ep.event_participation_id = @identification


    SELECT ep_child.salutation
     , lt.last_touch_sent
     , m.bounced
     , ft.next_touch_sent
     , CASE WHEN et.suppID IS NULL THEN 0 ELSE 1 END as bought_mag
     , CASE WHEN mh_grandchild.parent_member_hierarchy_id IS NULL THEN 0 ELSE 1 END as sent_emails
	FROM event_participation ep
        INNER JOIN member_hierarchy mh_child
            ON mh_child.parent_member_hierarchy_id = ep.member_hierarchy_id
        INNER JOIN event_participation ep_child
            ON ep_child.member_hierarchy_id = mh_child.member_hierarchy_id
        INNER JOIN member m
            ON m.member_id = mh_child.member_id
        LEFT JOIN (
                SELECT t.event_participation_id
                     , MAX(ti.launch_date) last_touch_sent
                FROM touch t
                    INNER JOIN touch_info ti
                        ON ti.touch_info_id = t.touch_info_id
                WHERE ti.launch_date < GETDATE()
                GROUP BY t.event_participation_id
            ) lt
            ON lt.event_participation_id = ep_child.event_participation_id
        LEFT JOIN (
                SELECT t.event_participation_id
                     , MIN(ti.launch_date) next_touch_sent
                FROM touch t
                    INNER JOIN touch_info ti
                        ON ti.touch_info_id = t.touch_info_id
                WHERE ti.launch_date > GETDATE()
                GROUP BY t.event_participation_id
            ) ft
            ON ft.event_participation_id = ep_child.event_participation_id
        LEFT JOIN QSPEcommerce..eFundraisingTransaction et
            ON et.suppID = ep_child.event_participation_id
        LEFT JOIN (
                SELECT parent_member_hierarchy_id
                FROM member_hierarchy
                WHERE parent_member_hierarchy_id IS NOT NULL
                GROUP BY parent_member_hierarchy_id
            ) mh_grandchild
            ON mh_grandchild.parent_member_hierarchy_id = mh_child.member_hierarchy_id
    WHERE ep.event_participation_id = 127277365


END
GO
