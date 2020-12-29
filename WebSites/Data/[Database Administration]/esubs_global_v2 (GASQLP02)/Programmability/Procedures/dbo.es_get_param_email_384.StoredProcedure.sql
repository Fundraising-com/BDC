USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_384]    Script Date: 02/14/2014 13:05:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[es_get_param_email_384]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		, ep.salutation as parent_name
		, 384 as email_template_id
		, a.sponsor_name 
		, a.sponsor_email
		, @identification as identification
		, e.event_name as campaign
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
				,m.email_address as sponsor_email
				,m.first_name + ' ' + m.last_name as sponsor_name
			from 
				member_hierarchy mh with(nolock)
				inner join member m with(nolock)
				on m.member_id = mh.member_id
			) a
		on a.member_hierarchy_id = mh.parent_member_hierarchy_id
        inner join event_group eg with(nolock)
        on eg.event_id = e.event_id
        inner join [group] g with(nolock)
        on g.group_id = eg.group_id
		where 
			ep.event_participation_id = @identification
GO
