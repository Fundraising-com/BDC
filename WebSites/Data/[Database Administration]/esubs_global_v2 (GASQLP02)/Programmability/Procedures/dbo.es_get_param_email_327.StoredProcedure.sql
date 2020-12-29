USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_327]    Script Date: 02/14/2014 13:05:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
327 / Subject: Your online fundraising campaign registration is incomplete
	From System - Sponsor - Registration incomplete after 12 hrs
	Email template 327 , es_get_param_email_327
	Business Rule 71, esubs_global_v2.es_touch_business71
	es_touch_generate_business71

*/

CREATE PROCEDURE [dbo].[es_get_param_email_327]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		--, m.first_name + ' ' + m.last_name as [user_name]
		, ep.salutation as [user_name]
		, 327 as email_template_id
		, @identification as identification
		,(CASE WHEN pers.redirect IS NOT NULL THEN pers.redirect  ELSE  'grouppage.aspx?e=' + CONVERT(VARCHAR(10), ep.event_id) END) AS redirect
		, pav.value as partner_name

	from
		member m with(nolock)
		inner join member_hierarchy mh with(nolock)
		on mh.member_id = m.member_id
		inner join event_participation ep with(nolock)
		on ep.member_hierarchy_id = mh.member_hierarchy_id 
		inner join event_group eg with(nolock)
		on eg.event_id = ep.event_id
		inner join [group] g with(nolock)
		on g.group_id = eg.group_id
		inner join partner_attribute_value pav with(nolock)
		on pav.partner_id = g.partner_id
		and pav.culture_code COLLATE Latin1_General_CI_AS = m.culture_code
		and partner_attribute_id = 3
		left join personalization pers
		on ep.event_participation_id = pers.event_participation_id
		where 
			ep.event_participation_id = @identification
GO
