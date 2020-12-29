USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_320]    Script Date: 04/27/2015 09:28:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[es_get_param_email_320]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		--, m.first_name + ' ' + m.last_name as [participant]
		, ep.salutation as participant
		, 320 as email_template_id
		, a.sponsor_name
		, a.sponsor_email
		, @identification as identification
		, e.event_name as campaign
		,e.event_id
	from
		member m with(nolock)
		inner join member_hierarchy mh with(nolock)
		on mh.member_id = m.member_id
		inner join event_participation ep with(nolock)
		on ep.member_hierarchy_id = mh.member_hierarchy_id
		inner join event e with(nolock)
		on ep.event_id = e.event_id
		inner join (
			select
				member_hierarchy_id
				--,m.email_address as sponsor_email
				--,m.first_name + ' ' + m.last_name as sponsor_name
				, case when u.email_address is null then m.email_address else u.email_address COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_email
				, case when u.first_name is null then m.first_name + ' ' + m.last_name COLLATE SQL_Latin1_General_CP1_CI_AS else u.first_name + ' ' + u.last_name COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_name
			from 
				member_hierarchy mh with(nolock)
				inner join member m with(nolock)
				on m.member_id = mh.member_id
				left join [users] u with(nolock)
				on m.[user_id] = u.[user_id]
			) a
		on a.member_hierarchy_id = mh.parent_member_hierarchy_id
		where 
			ep.event_participation_id = @identification
