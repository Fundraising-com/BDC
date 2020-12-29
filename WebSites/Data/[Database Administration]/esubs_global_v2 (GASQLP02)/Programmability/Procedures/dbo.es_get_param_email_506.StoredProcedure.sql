USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_506]    Script Date: 02/14/2014 13:06:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_param_email_506]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		, case when ep.salutation  is null 
		then m.first_name + ' ' + m.last_name 
		else ep.salutation COLLATE SQL_Latin1_General_CP1_CI_AI end as supporter 
		, 506 as email_template_id
		, @identification as identification
		, e.event_name as campaign
        , e.event_id
	from
		member m with(nolock)
		inner join member_hierarchy mh with(nolock)
		on mh.member_id = m.member_id
		inner join event_participation ep with(nolock)
		on ep.member_hierarchy_id = mh.member_hierarchy_id		
		inner join event e
		on ep.event_id = e.event_id
		where 
			ep.event_participation_id = @identification
GO
