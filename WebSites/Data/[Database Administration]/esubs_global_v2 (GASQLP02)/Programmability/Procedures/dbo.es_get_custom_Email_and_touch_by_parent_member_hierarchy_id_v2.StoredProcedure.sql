USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_custom_Email_and_touch_by_parent_member_hierarchy_id_v2]    Script Date: 02/14/2014 13:05:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	exec [dbo].[es_get_custom_Email_and_touch_by_parent_member_hierarchy_id] 3273733,1128738
*/
create PROCEDURE [dbo].[es_get_custom_Email_and_touch_by_parent_member_hierarchy_id_v2] 
@parent_member_hierarchy_id int,
@event_id int = NULL

AS
begin

-- draft w/out participants
SELECT     ti.launch_date, 
		   ti.reminder_interval_day,
		   ti.business_rule_id, 
		   cet.subject, 
		   cet.body_txt, 
           cet.body_html, 
           ti.touch_info_id, 
           ti.visitor_log_id, 
           ti.create_date, 
           cet.custom_email_template_id, 
           mh.member_hierarchy_id, 
           mh.parent_member_hierarchy_id, 
		   t.touch_id as touch_touch_id, 
		   t.event_participation_id as touch_event_participation_id , 
		   t.processed as touch_processed, 
           t.msrepl_tran_version as touch_msrepl_tran_version, 
		   t.create_date as touch_create_date , 
		   t.touch_info_id as touch_touch_info_id, 
		   t.member_hierarchy_id as touch_member_hierarchy_id,
		   mt.member_type_id,
		   mt.member_type_name,
		   mt.email_description 
FROM  dbo.touch t with (nolock) 
INNER JOIN dbo.member_hierarchy mh with (nolock) on mh.member_hierarchy_id = t.member_hierarchy_id
INNER JOIN dbo.touch_info ti with (nolock) ON t.touch_info_id = ti.touch_info_id 
INNER JOIN dbo.custom_email_template cet with (nolock) ON ti.touch_info_id = cet.touch_info_id
INNER JOIN dbo.business_rule brule with (nolock) ON ti.business_rule_id= brule.business_rule_id
INNER JOIN dbo.member_type mt with (nolock) ON brule.member_type_id = mt.member_type_id
--LEFT JOIN dbo.event_participation ep ON ep.event_participation_id = t.event_participation_id 
WHERE  (t.member_hierarchy_id = @parent_member_hierarchy_id) and processed not in (4, 8, 14)
       
union

-- emails and draft w/ participants
SELECT     ti.launch_date, 
		   ti.reminder_interval_day,
		   ti.business_rule_id, 
		   cet.subject, 
		   cet.body_txt, 
           cet.body_html, 
           ti.touch_info_id, 
           ti.visitor_log_id, 
           ti.create_date, 
           cet.custom_email_template_id, 
           mh.member_hierarchy_id, 
           mh.parent_member_hierarchy_id, 
		   t.touch_id as touch_touch_id, 
		   t.event_participation_id as touch_event_participation_id , 
		   t.processed as touch_processed, 
           t.msrepl_tran_version as touch_msrepl_tran_version, 
		   t.create_date as touch_create_date , 
		   t.touch_info_id as touch_touch_info_id, 
		   t.member_hierarchy_id as touch_member_hierarchy_id,
		   mt.member_type_id,
		   mt.member_type_name,
		   mt.email_description 
FROM  dbo.touch t with (nolock) 
INNER JOIN dbo.event_participation ep with (nolock) ON ep.event_participation_id = t.event_participation_id
INNER JOIN dbo.event e with (nolock) ON ep.event_id = e.event_id
INNER JOIN dbo.member_hierarchy mh with (nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id 
--on mh.parent_member_hierarchy_id = t.member_hierarchy_id
INNER JOIN dbo.touch_info ti with (nolock) ON t.touch_info_id = ti.touch_info_id 
INNER JOIN dbo.custom_email_template cet with (nolock) ON ti.touch_info_id = cet.touch_info_id
INNER JOIN dbo.business_rule brule with (nolock) ON ti.business_rule_id= brule.business_rule_id
INNER JOIN dbo.member_type mt with (nolock) ON brule.member_type_id = mt.member_type_id
WHERE  (mh.parent_member_hierarchy_id = @parent_member_hierarchy_id) and t.processed not in (4, 8, 14) and brule.business_rule_id not in (75,148,150,160)
	   and (e.event_id = @event_id OR @event_id IS NULL)
end
GO
