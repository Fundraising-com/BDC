USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_328]    Script Date: 04/03/2015 12:42:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[es_get_param_email_328]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		-- , m.first_name + ' ' + m.last_name as [user_name]
		, ep.salutation as [user_name]
		, 328 as email_template_id
		--, m.email_address as user_email
		--, m.password
		, case when u.email_address is null then m.email_address else u.email_address COLLATE SQL_Latin1_General_CP1_CI_AS end as user_email
		, case when u.password is null then m.password else u.password COLLATE SQL_Latin1_General_CP1_CI_AS end as [password]
		, @identification as identification
		, e.redirect as group_name
		,(CASE WHEN pers.redirect IS NULL  THEN 'grouppage.aspx?e=' + CONVERT(VARCHAR(10), ep.event_id)  ELSE  pers.redirect END) AS redirect
		, pav.value as partner_name
        , e.event_id
	from
		member m with(nolock)
		inner join member_hierarchy mh with(nolock)
		on mh.member_id = m.member_id
		left join [users] u
		on m.[user_id] = u.[user_id]
		inner join [group] g with(nolock)
		on mh.member_hierarchy_id = g.sponsor_id
		inner join event_participation ep with(nolock)
		on ep.member_hierarchy_id = mh.member_hierarchy_id
		inner join event e with(nolock)
		on e.event_id = ep.event_id
		inner join efrcommon..partner_attribute_value pav with(nolock)
		on pav.partner_id = g.partner_id
		and pav.culture_code COLLATE Latin1_General_CI_AS = m.culture_code
		and partner_attribute_id = 3
		LEFT join personalization pers
		on ep.event_participation_id = pers.event_participation_id
		where 
			ep.event_participation_id= @identification
