USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_352]    Script Date: 02/14/2014 13:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_param_email_352]
	@identification int
	,@source_id bigint
as

	select
		  @identification  as identification
		, @source_id as source_id
		, m.first_name + ' ' + m.last_name as [sponsor]
		, 352 as email_template_id
		, m.email_address as email
		, m.[password] as pass
		, e.redirect 
		, pav.value as partner
		, case when len(isnull(p.free_kit_url,'')) = 0 then 'www.efundraising.com' else p.free_kit_url end as partner_site
		, case when len(isnull(p.phone_number,'')) =0 then '1-866-313-8867' else p.phone_number end as partner_phone
	from 
		member m with(nolock)
		inner join member_hierarchy mh with(nolock)
		on mh.member_id = m.member_id
		inner join event_participation ep with(nolock)
		on ep.member_hierarchy_id = mh.member_hierarchy_id
		inner join event e with(nolock)
		on ep.event_id = e.event_id
		inner join event_group eg with(nolock)
		on eg.event_id = e.event_id
		inner join [group] g with(nolock)
		on g.group_id = eg.group_id	
		inner join partner_attribute_value pav with(nolock)
		on pav.partner_id = g.partner_id
		and pav.culture_code COLLATE Latin1_General_CI_AS = e.culture_code
		and partner_attribute_id = 3
		inner join efundweb.dbo.partner p with(nolock)
		on p.partner_id = g.partner_id
	where 
		ep.event_participation_id = @identification
GO
