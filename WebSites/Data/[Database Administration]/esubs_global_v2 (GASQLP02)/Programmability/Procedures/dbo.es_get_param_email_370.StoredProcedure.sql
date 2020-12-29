USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_370]    Script Date: 01/07/2015 14:58:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Craete by : Melissa Cote	
Create date : 2010.08.30
Participant Flow w/ and w/out sub pages

UPDATE BY: JIRO HIDAKA
DATE: July  2, 2013
Fixed the participant redirect

INVITES SUPPORTERS
EMAIL A / 370 / Subject: I need your help for a fundraiser for [++campaign name++]
	Participant - Invite Supporters - Email A
	Email template id 370, es_get_param_email_370
	Business Rule 104, esubs_global_v2.es_touch_business104
	EXEC es_get_param_email_370 132991222, 19106687
*/
ALTER PROCEDURE [dbo].[es_get_param_email_370]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		, ep.salutation as [supporter]
		, 370 as email_template_id
		, a.sponsor_name as participant_name
		, a.sponsor_email as participant_email
		, @identification as identification
		, e.event_name as campaign
		, case when ISNULL(b.aid,'')='' then 'frdotcom' ELSE b.aid end as [aid]
		, CASE WHEN ppo.product_offer_id=2 THEN 1284 ELSE 933 END as storefront_category_id
		, e.event_id
		, (CASE WHEN sponsor.redirect IS NULL OR a.participant_redirect IS NULL 
		       THEN 'grouppage.aspx?touch_id=' + CONVERT(VARCHAR(10), @source_id)  
		       ELSE  rtrim(ltrim(sponsor.redirect))+'/'+ rtrim(ltrim(a.participant_redirect)) 
		   END) AS redirect
	from
		member m (nolock)
		join member_hierarchy mh (nolock)
		on mh.member_id = m.member_id
		join event_participation ep (nolock)
		on ep.member_hierarchy_id = mh.member_hierarchy_id
		join event e (nolock)
		on ep.event_id = e.event_id
		join event_group eg (nolock)
	    on e.event_id = eg.event_id
	    join [group] g (nolock)
	    on g.group_id = eg.group_id
	    join partner_product_offer ppo (nolock)
	    on g.partner_id = ppo.partner_id
		join (
			select
				mh.member_hierarchy_id
				, event_id
				, case when u.email_address is null then m.email_address else u.email_address COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_email
				, case when u.first_name is null then m.first_name + ' ' + m.last_name COLLATE SQL_Latin1_General_CP1_CI_AS else u.first_name + ' ' + u.last_name COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_name
				, redirect as participant_redirect
			from 
				member_hierarchy mh (nolock)
				join member m (nolock)
				on m.member_id = mh.member_id
				left join [users] u (nolock)
				on m.[user_id] = u.[user_id]
				join event_participation (nolock) on event_participation.member_hierarchy_id = mh.member_hierarchy_id
				left join personalization (nolock) on personalization.event_participation_id = event_participation.event_participation_id 
			) a
		on a.member_hierarchy_id = mh.parent_member_hierarchy_id and a.event_id = e.event_id
		join (
			select ep.event_id, p.redirect from event_participation ep (nolock) left join personalization p (nolock)
			on ep.event_participation_id = p.event_participation_id
			where participation_channel_id=3 and p.redirect IS NOT NULL
		) sponsor
		on sponsor.event_id = ep.event_id
		left join (
			select p.partner_id, pav.value as [aid]
		    from
				EFRCommon.dbo.partner p (nolock)
				join EFRCommon.dbo.partner_attribute_value pav (nolock)
				on p.partner_id = pav.partner_id
				join EFRCommon.dbo.partner_attribute pa (nolock)
				on pav.partner_attribute_id = pa.partner_attribute_id 
			where pa.partner_attribute_id = 12 and p.is_active=1
		) b 
		on b.partner_id = m.partner_id
		where 
			ep.event_participation_id = @identification
