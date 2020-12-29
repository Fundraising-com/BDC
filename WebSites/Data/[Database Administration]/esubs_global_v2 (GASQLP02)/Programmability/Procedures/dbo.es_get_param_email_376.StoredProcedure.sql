USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_376]    Script Date: 02/14/2014 13:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_param_email_376]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		, m.first_name + ' ' + m.last_name as [supporter]
        , 'Connie Copeland' as sponsor_name
		, 'magazineagency@kappadelta.org' as sponsor_email
		, @identification as identification
	from
		member m with(nolock)
		inner join member_hierarchy mh with(nolock)
		on mh.member_id = m.member_id
		inner join event_participation ep with(nolock)
		on ep.member_hierarchy_id = mh.member_hierarchy_id
		/*inner join (
			select
				member_hierarchy_id
				,m.email_address as sponsor_email
				,m.first_name + ' ' + m.last_name as sponsor_name
			from 
				member_hierarchy mh
				inner join member m
				on m.member_id = mh.member_id
			) a
		on a.member_hierarchy_id = mh.parent_member_hierarchy_id
        */
		where 
			ep.event_participation_id = @identification
GO
