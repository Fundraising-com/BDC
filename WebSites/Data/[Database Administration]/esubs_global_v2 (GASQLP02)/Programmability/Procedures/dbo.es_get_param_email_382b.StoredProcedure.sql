USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_382b]    Script Date: 02/14/2014 13:05:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_param_email_382b]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		-- , m.first_name + ' ' + m.last_name as [user_name]
		, ep.salutation as [user_name]
		, 382 as email_template_id
		, m.email_address as user_email
		, m.password
		, @identification as identification
		, e.redirect as group_name
		, pav.value as partner_name

	from
		member m with(nolock)
		inner join member_hierarchy mh with(nolock)
		on mh.member_id = m.member_id
		inner join [group] g with(nolock)
		on mh.member_hierarchy_id = g.sponsor_id
		inner join event_participation ep with(nolock)
		on ep.member_hierarchy_id = mh.member_hierarchy_id
		inner join event e with(nolock)
		on e.event_id = ep.event_id
		inner join partner_attribute_value pav with(nolock)
		on pav.partner_id = g.partner_id
		and pav.culture_code COLLATE Latin1_General_CI_AS = m.culture_code
		and partner_attribute_id = 3
		where 
			ep.event_participation_id= @identification
GO
