USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_457]    Script Date: 02/14/2014 13:05:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Create by : Melissa Cote	
Create date : 2010.08.30 
Group Sponsor Flow w/out sub pages

REMINDS PARTICIPANTS
REMINDER / 457 / Subject: An update on our fundraising campaign for [++campaign name++]
	Sponsor - Reminds Participants w/out sub page - Reminder
	Email template 457, es_get_param_email_457
	Business Rule 173, esubs_global_v2.es_touch_business173
*/

CREATE PROCEDURE [dbo].[es_get_param_email_457]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		, ep.salutation as participant
		, 457 as email_template_id
		, a.sponsor_name
		, a.sponsor_email
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
				left join users u with(nolock)
				on u.[user_id] = m.[user_id]
				inner join event_participation ep with(nolock)
				on ep.member_hierarchy_id = mh.member_hierarchy_id
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
