USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_509]    Script Date: 02/14/2014 13:06:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_param_email_509]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		, 509 as email_template_id
		, case when u.email_address is null then m.email_address else u.email_address COLLATE SQL_Latin1_General_CP1_CI_AS end as participant_email
		, case when u.first_name is null then m.first_name + ' ' + m.last_name COLLATE SQL_Latin1_General_CP1_CI_AS else u.first_name + ' ' + u.last_name COLLATE SQL_Latin1_General_CP1_CI_AS end as participant_name
		, @identification as identification
		, e.event_name as campaign
		, CASE WHEN p.redirect IS NULL THEN 'ParticipantHome.aspx?e='+RTRIM(CAST(e.event_id AS CHAR))+'&ph='+RTRIM(CAST(ep.event_participation_id AS CHAR))+ 
                                            (CASE WHEN p.personalization_id IS NULL THEN '' ELSE '&pers='+RTRIM(CAST(p.personalization_id AS CHAR)) END)
          ELSE RTRIM(parent.sponsor_redirect)+'/'+RTRIM(p.redirect)
		  END as redirect
        , e.event_id
	from
		member m with(nolock)
		inner join member_hierarchy mh with(nolock)
		on mh.member_id = m.member_id
		inner join event_participation ep with(nolock)
		on ep.member_hierarchy_id = mh.member_hierarchy_id
		left join [users] u with(nolock)
		on m.[user_id] = u.[user_id]		
		inner join event e
		on ep.event_id = e.event_id
		left join personalization p with(nolock)
		on ep.event_participation_id = p.event_participation_id
		inner join (
			select mhp.member_hierarchy_id, pp.redirect as sponsor_redirect
			  from event_participation epp with (nolock) join member_hierarchy mhp with (nolock)
			    on epp.member_hierarchy_id = mhp.member_hierarchy_id join member mp with (nolock)
			    on mhp.member_id = mp.member_id left join users up with (nolock)
			    on mp.[user_id] = up.[user_id] join personalization pp with (nolock)
		        on epp.event_participation_id = pp.event_participation_id
		     where mp.unsubscribe = 0 and epp.participation_channel_id=3
			   and (up.unsubscribe is null or up.unsubscribe = 0)
			   and mp.bounced = 0
			   and mp.partner_id not in (719, 131, 782, 143, 807, 816, 70, 126, 668, 857, 832)
			   and mhp.active = 1
			   and mp.deleted = 0
		) parent on mh.parent_member_hierarchy_id = parent.member_hierarchy_id
		where 
			ep.event_participation_id = @identification
GO
