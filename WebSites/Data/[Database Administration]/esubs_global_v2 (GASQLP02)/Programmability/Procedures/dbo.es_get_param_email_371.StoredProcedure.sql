USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_371]    Script Date: 02/14/2014 13:05:47 ******/
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

INVITES SUPPORTERS

EMAIL B / 371 / Subject: Do you have a minute for my fundraiser with [++campaign name++}
	Participant - Invite Supporters - Email B
	Email template id 371, es_get_param_email_371
	Business Rule 105, esubs_global_v2.es_touch_business105
*/	

CREATE PROCEDURE [dbo].[es_get_param_email_371]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		, ep.salutation as [supporter]
		, 371 as email_template_id
		, a.sponsor_name as participant_name 
		, a.sponsor_email as participant_email
		, @identification as identification
		, e.event_name as campaign
		, case when ISNULL(b.aid,'')='' then 'frdotcom' ELSE b.aid end as [aid]
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
				, event_id
				, case when u.email_address is null then m.email_address else u.email_address COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_email
				, case when u.first_name is null then m.first_name + ' ' + m.last_name COLLATE SQL_Latin1_General_CP1_CI_AS else u.first_name + ' ' + u.last_name COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_name
			from 
				member_hierarchy mh with(nolock)
				inner join member m with(nolock)
				on m.member_id = mh.member_id
				left join [users] u with(nolock)
				on m.[user_id] = u.[user_id]
				inner join event_participation with (nolock) on event_participation.member_hierarchy_id = mh.member_hierarchy_id 
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
		where 
			ep.event_participation_id = @identification
GO
