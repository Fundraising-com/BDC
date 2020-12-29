USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_440]    Script Date: 04/20/2015 14:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

ALTER procedure [dbo].[es_get_param_email_440]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id 
		, ltrim(rtrim(ep.salutation)) as supporter
		, ltrim(rtrim(ep.salutation)) as participant
		, 440 as email_template_id
		, a.sponsor_name 
		, a.sponsor_email
		, @identification as identification
		, e.event_name as campaign
      	, ep.salutation as participant_name
        , e.event_id
		, (case when a.sponsor_image_path <> '' then 
		   a.sponsor_image_path else 'Content/images/personalization/groupphotoplaceholder.gif' end) as image_path
		, sp_redirect  as redirect
		, case when ISNULL(b.aid,'')='' then 'frdotcom' ELSE b.aid end as [aid]
	from
		member m with (nolock)
		inner join member_hierarchy mh with (nolock)
		on mh.member_id = m.member_id
		inner join event_participation ep with (nolock)
		on ep.member_hierarchy_id = mh.member_hierarchy_id
		inner join event e with (nolock)
		on ep.event_id = e.event_id
		inner join (
			select
				mh.member_hierarchy_id
				, ep.event_id
				, case when u.email_address is null then m.email_address else u.email_address COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_email
				, case when u.first_name is null then m.first_name + ' ' + m.last_name COLLATE SQL_Latin1_General_CP1_CI_AS else u.first_name + ' ' + u.last_name COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_name
				, (CASE WHEN pers.redirect IS NULL  THEN 'grouppage.aspx?e=' + CONVERT(VARCHAR(10), ep.event_id)  ELSE   pers.redirect END) as sp_redirect
				, (CASE WHEN pim.image_url IS NULL THEN pim2.image_url ELSE pim.image_url END) as sponsor_image_path
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
				left join 
				(select pim.personalization_id, pim.image_url
				 from personalization_image pim with (nolock)
				where pim.image_approval_status_id = 3 and pim.deleted = 0 and pim.isCoverAlbum = 1
				 ) pim 
				on pim.personalization_id = pers.personalization_id
				left join 
				(select top 1 pers.event_participation_id, pim.image_url
				 from event_participation ep (nolock) 
				 join member_hierarchy mh (nolock)
				 on ep.member_hierarchy_id = mh.member_hierarchy_id
				 join member_hierarchy mhp (nolock)
				 on mh.parent_member_hierarchy_id = mhp.member_hierarchy_id
				 join event_participation epp (nolock)
				 on mhp.member_hierarchy_id = epp.member_hierarchy_id
				 join personalization pers with (nolock)
				 on epp.event_participation_id = pers.event_participation_id 
				 join personalization_image pim with (nolock)
				 on pers.personalization_id = pim.personalization_id
				where pim.image_approval_status_id = 3 and pim.deleted = 0 and pim.isCoverAlbum = 0
				  and ep.event_participation_id=@identification
				order by pim.create_date desc
				 ) pim2
				on ep.event_participation_id = pim2.event_participation_id
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
        inner join event_group eg
        on eg.event_id = e.event_id
        inner join [group] g
        on g.group_id = eg.group_id
		where 
			ep.event_participation_id = @identification
