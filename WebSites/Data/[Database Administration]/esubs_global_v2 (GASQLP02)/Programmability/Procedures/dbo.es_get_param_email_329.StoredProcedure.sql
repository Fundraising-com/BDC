USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_329]    Script Date: 04/02/2015 13:24:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
329 / Subject: You’ve successfully launched your fundraising campaign
	From System - Sponsor - Registration confirmation w/ successful Launch
	Email template 329, es_get_param_email_329
	Business Rule 70, esubs_global_v2.es_touch_business70
	[es_touch_generate_business70]
	*** redirect
	

*/

ALTER PROCEDURE [dbo].[es_get_param_email_329]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		, ep.salutation as [user_name]
		, 329 as email_template_id
		--, m.email_address as user_email
		--, m.password
		, case when u.email_address is null then m.email_address else u.email_address COLLATE SQL_Latin1_General_CP1_CI_AS end as user_email
		, case when u.password is null then m.password COLLATE SQL_Latin1_General_CP1_CI_AS else u.password COLLATE SQL_Latin1_General_CP1_CI_AS end as [password]
		, @identification as identification
		, e.redirect as group_name
		,(CASE WHEN pers.redirect IS NULL THEN 'grouppage.aspx?e=' + CONVERT(VARCHAR(10), ep.event_id)  ELSE  pers.redirect END) AS redirect
		, pav.value as partner_name
        , e.event_id
	from
		member m with(nolock)
		inner join member_hierarchy mh with(nolock)
		on mh.member_id = m.member_id
		left join [users] u
		on m.[user_id] = u.[user_id]
		inner join event_participation ep with(nolock)
		on ep.member_hierarchy_id = mh.member_hierarchy_id
		inner join event e with(nolock)
		on ep.event_id = e.event_id
		inner join event_group eg with(nolock)
		on eg.event_id = e.event_id
		inner join [group] g with(nolock)
		on g.group_id = eg.group_id
		inner join partner_attribute_value pav with(nolock)
		on pav.partner_id = g.partner_id
		and pav.culture_code  COLLATE Latin1_General_CI_AS = e.culture_code
		and partner_attribute_id = 3
		left join personalization pers
		on ep.event_participation_id = pers.event_participation_id
		where 
			ep.event_participation_id = @identification
