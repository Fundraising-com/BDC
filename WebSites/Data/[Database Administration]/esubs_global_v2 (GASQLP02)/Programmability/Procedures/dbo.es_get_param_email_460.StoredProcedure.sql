USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_460]    Script Date: 02/14/2014 13:05:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Create by : Melissa Cote
Create date : 2010.09.23
Group Sponsor Flow w/ sub pages (MAGAZINES ONLY)

EMAIL A / 460/ Subject: Support our fundraising campaign for [++campaign name++]
	Sponsor - Invites Participants w/ Sub Page - Email A (MAGAZINES ONLY)
	Email template 460, es_get_param_email_460
	Business Rule 176, esubs_global_v2.es_touch_business176
*/

CREATE procedure [dbo].[es_get_param_email_460]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		--, m.first_name + ' ' + m.last_name as [participant]
		, case when ep.salutation  is null 
		then m.first_name + ' ' + m.last_name 
		else ep.salutation COLLATE SQL_Latin1_General_CP1_CI_AI end as participant 
		, 460 as email_template_id
		, a.sponsor_name
		, a.sponsor_email
		, @identification as identification
		, e.event_name as campaign
		, sp_redirect  as redirect
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
				mh.member_hierarchy_id
				--,m.email_address as sponsor_email
				--,m.first_name + ' ' + m.last_name as sponsor_name
				, case when u.email_address is null then m.email_address else u.email_address COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_email
				, case when u.first_name is null then m.first_name + ' ' + m.last_name COLLATE SQL_Latin1_General_CP1_CI_AS else u.first_name + ' ' + u.last_name COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_name
				, redirect as sp_redirect
			from 
				member_hierarchy mh
				inner join member m
				on m.member_id = mh.member_id
				left join users u with(nolock)
				on u.[user_id] = m.[user_id]
				inner join event_participation ep
				on ep.member_hierarchy_id = mh.member_hierarchy_id
				inner join personalization per
				on ep.event_participation_id = per.event_participation_id
			) a
		on a.member_hierarchy_id = mh.parent_member_hierarchy_id
		where 
			ep.event_participation_id = @identification
GO
