USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_362]    Script Date: 2/9/2017 11:21:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Craete by : Melissa Cote
Create date : 2010.08.30
Group Sponsor Flow w/ sub pages

KICKS-OFF; INVITES PARTICIPANTS W/ PROMPT TO PERSONALIZE
EMAIL A / 362 / Subject: Support our fundraising campaign for [++campaign name++]
	Sponsor - Invites Participants w/ Sub Page - Email A
	Email template 362, es_get_param_email_362
	Business Rule 96, esubs_global_v2.es_touch_business96
*/

ALTER procedure [dbo].[es_get_param_email_362]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		, case when ep.salutation  is null 
		then m.first_name + ' ' + m.last_name 
		else ep.salutation COLLATE SQL_Latin1_General_CP1_CI_AI end as participant 
		, case when ep.salutation  is null 
		then m.first_name
		else ep.salutation COLLATE SQL_Latin1_General_CP1_CI_AI end as participant_first_name 
		, case when ep.salutation  is null 
		then m.first_name + ' ' + m.last_name 
		else ep.salutation COLLATE SQL_Latin1_General_CP1_CI_AI end as supporter
		, 362 as email_template_id
		, a.sponsor_name
		, a.sponsor_email
		, @identification as identification
		, e.event_name as campaign
		, sp_redirect  as redirect
		, case when ISNULL(b.aid,'')='' then 'frdotcom' ELSE b.aid end as [aid]
		, e.event_id
		, CASE WHEN g.partner_id = 8404 THEN 'our Relay For Life team. There are three ways you can help.' ELSE 'a very special cause. There are two ways you can help.' END as [first_line]
		, CASE WHEN g.partner_id = 8404 THEN '<br/><strong>3.</strong> Get Nationwide savings when buying the Entertainment Book from our Page.' ELSE '' END as [second_line]
	from
		member m with (nolock)
		join member_hierarchy mh with (nolock)
		on mh.member_id = m.member_id
		join event_participation ep with (nolock)
		on ep.member_hierarchy_id = mh.member_hierarchy_id		
		join event e with (nolock)
		on ep.event_id = e.event_id
		join (
			select
				mh.member_hierarchy_id
				, ep.event_id
				, case when u.email_address is null then m.email_address else u.email_address COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_email
				, case when u.first_name is null then m.first_name + ' ' + m.last_name COLLATE SQL_Latin1_General_CP1_CI_AS else u.first_name + ' ' + u.last_name COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_name
				, (CASE WHEN pers.redirect IS NULL  THEN 'grouppage.aspx?e=' + CONVERT(VARCHAR(10), ep.event_id)  ELSE   pers.redirect END) as sp_redirect
			from 
				member_hierarchy mh with (nolock)
				join member m with (nolock)
				on m.member_id = mh.member_id
				left join [users] u with (nolock)
				on m.[user_id] = u.[user_id]
				join event_participation ep with (nolock)
				on ep.member_hierarchy_id = mh.member_hierarchy_id
				left join personalization pers with (nolock)
				on ep.event_participation_id = pers.event_participation_id
			) a		
			on a.member_hierarchy_id = mh.parent_member_hierarchy_id and a.event_id = e.event_id
		left join (
			select p.partner_id, pav.value as [aid]
		    from
				EFRCommon.dbo.partner p with (nolock)
				join EFRCommon.dbo.partner_attribute_value pav with (nolock)
				on p.partner_id = pav.partner_id
				join EFRCommon.dbo.partner_attribute pa with (nolock)
				on pav.partner_attribute_id = pa.partner_attribute_id 
			where pa.partner_attribute_id = 12 and p.is_active=1
		) b 
		on b.partner_id = m.partner_id
		JOIN event_group eg (NOLOCK) ON e.event_id = eg.event_id
		JOIN [group] g (NOLOCK) ON eg.group_id = g.group_id
		where 
			ep.event_participation_id = @identification
