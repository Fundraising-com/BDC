USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_342]    Script Date: 02/14/2014 13:05:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
GSCOUTS prize
*/
CREATE PROCEDURE [dbo].[es_get_param_email_342]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		, m.parent_first_name + ' ' + m.parent_last_name as [parent_name]
		, 342 as email_template_id
		, @identification as identification
	from
		member m with(nolock)
		inner join member_hierarchy mh with(nolock)
		on mh.member_id = m.member_id
		inner join event_participation ep with(nolock)
		on ep.member_hierarchy_id = mh.member_hierarchy_id
		
	where 
		ep.event_participation_id = @identification
GO
