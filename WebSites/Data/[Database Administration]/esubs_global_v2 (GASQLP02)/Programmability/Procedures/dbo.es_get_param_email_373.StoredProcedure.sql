USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_373]    Script Date: 02/14/2014 13:05:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Create by : Melissa Cote 
Create date : 2010.08.30
Participant Flow w/ and w/out sub pages

UPDATE BY: JIRO HIDAKA
DATE: July  2, 2013
Fixed the participant redirect

REMINDS SUPPORTERS
REMINDER / 373 / Subject: Do you have a minute for my fundraiser with [++campaign name++]
	Participant - Reminds Supporters - Reminder
	Email template id 373, es_get_param_email_373
	Business Rule 107, esubs_global_v2.es_touch_business107
*/

CREATE PROCEDURE [dbo].[es_get_param_email_373]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		, ep.salutation as [supporter]
		, 373 as email_template_id
		, a.sponsor_name as participant_name
		, a.sponsor_email as participant_email
		, @identification as identification
		, e.event_name as campaign
		,(CASE WHEN sponsor.redirect IS NULL OR a.participant_redirect IS NULL 
		       THEN 'grouppage.aspx?touch_id=' + CONVERT(VARCHAR(10), @source_id)  
		       ELSE  rtrim(ltrim(sponsor.redirect))+'/' + rtrim(ltrim(a.participant_redirect)) 
		  END) AS redirect
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
				mh.member_hierarchy_id
				, case when u.email_address is null then m.email_address else u.email_address COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_email
				, case when u.first_name is null then m.first_name + ' ' + m.last_name COLLATE SQL_Latin1_General_CP1_CI_AS else u.first_name + ' ' + u.last_name COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_name
				, redirect as participant_redirect
			from 
				member_hierarchy mh with(nolock)
				inner join member m with(nolock)
				on m.member_id = mh.member_id
				left join [users] u with(nolock)
				on m.[user_id] = u.[user_id]
				inner join event_participation with (nolock) on event_participation.member_hierarchy_id = mh.member_hierarchy_id 
				left join personalization with (nolock) on personalization.event_participation_id = event_participation.event_participation_id 
			) a
		on a.member_hierarchy_id = mh.parent_member_hierarchy_id
		inner join (
			select ep.event_id, p.redirect from event_participation ep with (nolock) left join personalization p with (nolock)
			on ep.event_participation_id = p.event_participation_id
			where participation_channel_id=3 and p.redirect IS NOT NULL
		) sponsor
		on sponsor.event_id = ep.event_id
		where 
			ep.event_participation_id = @identification
GO
