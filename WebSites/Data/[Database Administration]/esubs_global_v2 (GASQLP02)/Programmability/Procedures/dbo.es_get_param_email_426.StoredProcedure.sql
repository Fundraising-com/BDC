USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_426]    Script Date: 02/14/2014 13:05:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =================================================
-- Author:		Melissa Cote	
-- ALTER  date: 2008-11-27
-- Description:	Participant – Daily notification of sale
--		Rule: Send on event of sale associated with participant
--		Repeat: Yes – Once a day max
--		Subject: One of your friends bought a magazine from your fundraiser!
--		Reply to Name: <Partner Name>
--		Reply to Email: efr-online@magfundraising.com
--		From Name: <Partner Name>
--		From Email: efr-online@magfundraising.com
--		Business rule:  148
-- =================================================

CREATE PROCEDURE [dbo].[es_get_param_email_426]
		@identification int
		,@source_id bigint
as

	
	select 
		 @source_id as source_id
		--, m.first_name + ' ' + m.last_name as [Participant]
		-- , m.email_address as sponsor_email
		, case when u.email_address is null then m.email_address else u.email_address COLLATE SQL_Latin1_General_CP1_CI_AS end as [Participant]
		, case when u.first_name is null then m.first_name + ' ' + m.last_name COLLATE SQL_Latin1_General_CP1_CI_AS else u.first_name + ' ' + u.last_name COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_email
		, 426 as email_template_id
		, @identification as identification
		, e.redirect as group_name
		, e.event_name as campaign
		, pav.value as partner_name
	from
		member m with(nolock)
		left join users u
		on u.[user_id] = m.[user_id]
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
		and pav.culture_code  COLLATE Latin1_General_CI_AS = e.culture_code
		and partner_attribute_id = 3
		where 
			ep.event_participation_id = @identification
GO
