USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_481]    Script Date: 02/14/2014 13:06:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_param_email_481]
		@identification int
		,@source_id bigint
as
			
	select 
		 @source_id as source_id
		, ep.salutation as [sponsor_name]
		, 481 as email_template_id
		, case when u.email_address is null then m.email_address else u.email_address COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_email
		, case when u.password is null then m.password else u.password COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_password
		, @identification as identification
		, e.event_name as group_name
		,(CASE WHEN pers.redirect IS NULL THEN 'grouppage.aspx?e=' + CONVERT(VARCHAR(10), ep.event_id) ELSE pers.redirect END) AS redirect
		, p.partner_name
		, 'Mar 1, 2012' as salesenddate
		, 'April, 2012' as paymentdate
	from
		member m with (nolock)
		inner join member_hierarchy mh with (nolock)
		on mh.member_id = m.member_id
		left join [users] u with (nolock)
		on m.[user_id] = u.[user_id]
		inner join [group] g with (nolock)
		on mh.member_hierarchy_id = g.sponsor_id
		inner join event_participation ep with (nolock)
		on ep.member_hierarchy_id = mh.member_hierarchy_id
		inner join event e with(nolock)
		on e.event_id = ep.event_id
		inner join partner p with (nolock)
		on p.partner_id = g.partner_id
		LEFT join personalization pers with(nolock)
		on ep.event_participation_id = pers.event_participation_id
		where 
			ep.event_participation_id= @identification
GO
