USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_464]    Script Date: 02/14/2014 13:05:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Craete by : Melissa Cote	
Craete date : 2010.08.30
Participant Flow w/ and w/out sub pages

INVITES SUPPORTERS
EMAIL A / 464 / Subject: I need your help for a fundraiser for [++campaign name++]
	Participant - Invite Supporters - Email A (MAGAZINES ONLY)
	Email template id 464, es_get_param_email_464
	Business Rule 180, esubs_global_v2.es_touch_business180

*/

CREATE PROCEDURE [dbo].[es_get_param_email_464]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		-- , m.first_name + ' ' + m.last_name as [supporter]
		, ep.salutation as [supporter]
		, 464 as email_template_id
		, a.sponsor_name as participant_name
		, a.sponsor_email as participant_email
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
				--,m.email_address as sponsor_email
				--,m.first_name + ' ' + m.last_name as sponsor_name
				, case when u.email_address is null then m.email_address else u.email_address COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_email
				, case when u.first_name is null then m.first_name + ' ' + m.last_name COLLATE SQL_Latin1_General_CP1_CI_AS else u.first_name + ' ' + u.last_name COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_name
			from 
				member_hierarchy mh with(nolock)
				inner join member m with(nolock)
				on m.member_id = mh.member_id
				left join users u with(nolock)
				on u.[user_id] = m.[user_id]
			) a
		on a.member_hierarchy_id = mh.parent_member_hierarchy_id
		where 
			ep.event_participation_id = @identification
GO
