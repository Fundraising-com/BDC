USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_455]    Script Date: 02/14/2014 13:05:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[es_get_param_email_455]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		--, m.first_name + ' ' + m.last_name as [supporter]
		--, m.first_name + ' ' + m.last_name as [participant]    
		, ltrim(rtrim(ep.salutation)) as supporter
		, ltrim(rtrim(ep.salutation)) as participant
		, 455 as email_template_id
		, a.sponsor_name 
		, a.sponsor_email
		, @identification as identification
		, e.event_name as campaign
      	, ep.salutation as participant_name
        , e.event_id
        , (case when a.sponsor_image_path <> '' then 
		   a.sponsor_image_path else 'Images/empty.gif' end) as image_path
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
		on a.member_hierarchy_id = mh.parent_member_hierarchy_id
        inner join event_group eg
        on eg.event_id = e.event_id
        inner join [group] g
        on g.group_id = eg.group_id
		where 
			ep.event_participation_id = @identification
GO
