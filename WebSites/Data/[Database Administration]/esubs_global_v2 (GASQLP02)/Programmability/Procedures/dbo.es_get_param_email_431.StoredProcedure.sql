USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_431]    Script Date: 02/14/2014 13:05:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_param_email_431]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		, ltrim(rtrim(ep.salutation)) as [supporter]
		, 431 as email_template_id
		, a.sponsor_name as participant_name
		, a.sponsor_email as participant_email
		, @identification as identification
		, e.event_name as campaign
		, e.event_id
		, (case when a.sponsor_image_path <> '' then 
			a.sponsor_image_path else 'Images/empty.gif' end) as image_path
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
				,m.email_address as sponsor_email
				,m.first_name + ' ' + m.last_name as sponsor_name
                , COALESCE(pim.image_url,'') as sponsor_image_path
			from 
				member_hierarchy mh with(nolock)
				inner join member m with(nolock)        
				on m.member_id = mh.member_id
				inner join event_participation ep with (nolock)
				on mh.member_hierarchy_id = ep.member_hierarchy_id
				left join personalization p with (nolock)
				on ep.event_participation_id = p.event_participation_id
				left join personalization_image pim with (nolock)
				on p.personalization_id = pim.personalization_id and pim.image_approval_status_id = 3 and pim.isCoverAlbum = 1 and pim.deleted = 0
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
