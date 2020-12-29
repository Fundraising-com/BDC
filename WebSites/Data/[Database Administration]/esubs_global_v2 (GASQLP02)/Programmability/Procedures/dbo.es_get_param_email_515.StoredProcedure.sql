USE [esubs_global_v2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_param_email_515]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		, ltrim(rtrim(ep.salutation)) as [supporter]
		, 515 as email_template_id
		, a.sponsor_name as participant_name
		, a.sponsor_first_name as participant_first_name
		, a.sponsor_last_name as participant_last_name
		, a.sponsor_email as participant_email
		, @identification as identification
		, e.event_name as campaign
		, e.event_id
		, (case when a.sponsor_image_path <> '' then 
			a.sponsor_image_path else 'Images/empty.gif' end) as image_path
		, case when ISNULL(b.aid,'')='' then 'frdotcom' ELSE b.aid end as [aid]
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
		join (
			select
				mh.member_hierarchy_id
				, event_id
				, case when u.email_address is null then m.email_address else u.email_address COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_email
				, case when u.first_name is null then m.first_name + ' ' + m.last_name COLLATE SQL_Latin1_General_CP1_CI_AS else u.first_name + ' ' + u.last_name COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_name
				, case when u.first_name is null then m.first_name COLLATE SQL_Latin1_General_CP1_CI_AS else u.first_name COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_first_name
                , case when u.last_name is null then m.last_name COLLATE SQL_Latin1_General_CP1_CI_AS else u.last_name COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_last_name
                , COALESCE(pim.image_url,'') as sponsor_image_path
                , redirect as participant_redirect
			from 
				member_hierarchy mh (nolock)
				join member m (nolock)        
				on m.member_id = mh.member_id
				left join [users] u (nolock)
				on m.[user_id] = u.[user_id]
				join event_participation ep (nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
				left join personalization p (nolock) on ep.event_participation_id = p.event_participation_id
				left join personalization_image pim (nolock)
				on p.personalization_id = pim.personalization_id and pim.image_approval_status_id = 3 and pim.isCoverAlbum = 1 and pim.deleted = 0
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
