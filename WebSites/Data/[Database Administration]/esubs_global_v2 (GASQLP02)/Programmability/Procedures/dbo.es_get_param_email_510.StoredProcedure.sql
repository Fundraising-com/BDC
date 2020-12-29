USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_510]    Script Date: 02/14/2014 13:06:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_param_email_510]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		, 508 as email_template_id
		, case when u.email_address is null then m.email_address else u.email_address COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_email
		, case when u.first_name is null then m.first_name + ' ' + m.last_name COLLATE SQL_Latin1_General_CP1_CI_AS else u.first_name + ' ' + u.last_name COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_name
		, @identification as identification
		, e.event_name as campaign
		, p.redirect
        , e.event_id
	from
		member m with(nolock)
		inner join member_hierarchy mh with(nolock)
		on mh.member_id = m.member_id
		inner join event_participation ep with(nolock)
		on ep.member_hierarchy_id = mh.member_hierarchy_id
		left join [users] u with(nolock)
		on m.[user_id] = u.[user_id]		
		inner join event e
		on ep.event_id = e.event_id
		left join personalization p with(nolock)
		on ep.event_participation_id = p.event_participation_id
		where 
			ep.event_participation_id = @identification
GO
