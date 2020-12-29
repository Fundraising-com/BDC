USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_461]    Script Date: 02/14/2014 13:05:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Create by : Melissa Cote
Create date : 2010.09.23
Group Sponsor Flow w/ sub pages (MAGAZINES ONLY)

EMAIL B / 461 / Subject: Your participation is key to [++campaign name++]
	Sponsor - Invites Participants w/ Sub Page - Email B (MAGAZINES ONLY)
	Email template 461, es_get_param_email_461
	Business Rule 177, esubs_global_v2.es_touch_business177

*/


CREATE procedure [dbo].[es_get_param_email_461]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		--, m.first_name + ' ' + m.last_name as [supporter]
		--, m.first_name + ' ' + .last_name as [participant_name]    
		, ep.salutation as supporter
		, ep.salutation as participant
        , ep.salutation as participant_name
		, 461 as email_template_id
		, a.sponsor_name 
		, a.sponsor_email
		, @identification as identification
		, e.event_name as campaign
        , g.group_name [group_name]
	from
		member m
		inner join member_hierarchy mh
		on mh.member_id = m.member_id
		inner join event_participation ep
		on ep.member_hierarchy_id = mh.member_hierarchy_id
		inner join event e
		on ep.event_id = e.event_id
		inner join (
			select
				member_hierarchy_id
				--,m.email_address as sponsor_email
				--,m.first_name + ' ' + m.last_name as sponsor_name
				, case when u.email_address is null then m.email_address else u.email_address COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_email
				, case when u.first_name is null then m.first_name + ' ' + m.last_name COLLATE SQL_Latin1_General_CP1_CI_AS else u.first_name + ' ' + u.last_name COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_name
			from 
				member_hierarchy mh
				inner join member m
				on m.member_id = mh.member_id
				left join users u 
				on u.[user_id] = m.[user_id]
			) a
		on a.member_hierarchy_id = mh.parent_member_hierarchy_id
        inner join event_group eg
        on eg.event_id = e.event_id
        inner join [group] g
        on g.group_id = eg.group_id
		where 
			ep.event_participation_id = @identification
GO
