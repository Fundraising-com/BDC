USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_391]    Script Date: 02/14/2014 13:05:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_param_email_391]
		@identification int
		,@source_id bigint
as

	select 	top 1
		 @source_id as source_id
		, ep.salutation as [user_name]
		, 391 as email_template_id
		, @identification as identification
		, pav.value as partner_name
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
		and pav.culture_code COLLATE Latin1_General_CI_AS= e.culture_code
		and partner_attribute_id = 3
        /*
		inner join earned_prize pe
		on pe.event_participation_id = ep.event_participation_id
		inner join prize_item pt
		on pt.prize_item_id = pe.prize_item_id
		and pt.prize_id = 6*/
		where ep.event_participation_id = @identification
GO
