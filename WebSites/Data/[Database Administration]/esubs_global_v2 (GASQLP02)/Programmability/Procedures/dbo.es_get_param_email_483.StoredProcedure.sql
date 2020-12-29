USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_483]    Script Date: 02/14/2014 13:06:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_param_email_483]
	@identification int
	,@source_id bigint
as

select top 1
	@source_id as source_id
	--, m.first_name + ' ' + m.last_name as [user_name]
	--, ep.salutation as [user_name]
	-- MCOTE  collate SQL_Latin1_General_CP1_CI_AI by the time we rebuild the table event_participantion to change the salutation field for  collate SQL_Latin1_General_CP1_CI_AI
	  --  , (case when ep.salutation is null then m.first_name + ' ' + m.last_name collate SQL_Latin1_General_CP1_CI_AI else ep.salutation collate SQL_Latin1_General_CP1_CI_AI end ) as [user_name]
	, sponsor_name
	, 483 as email_template_id
	, max(case when prize_item_code_id = 1 then prize_item_code end) as prize_code_1 
	, max(case when prize_item_code_id = 2 then prize_item_code end) as prize_code_2 
	, @identification as identification
	--, expiration_date 
	, partner_name

	from (
	select 
		(case when ep.salutation is null then m.first_name + ' ' + m.last_name collate SQL_Latin1_General_CP1_CI_AI else ep.salutation collate SQL_Latin1_General_CP1_CI_AI end ) as sponsor_name
		, ROW_NUMBER() OVER(ORDER BY pt.prize_item_id  ASC) as prize_item_code_id
		, pt.prize_item_code
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
			and pav.culture_code COLLATE Latin1_General_CI_AS = e.culture_code
			and partner_attribute_id = 3
		
		inner join earned_prize pe
			on pe.event_participation_id = ep.event_participation_id
		inner join prize_item pt
			on pt.prize_item_id = pe.prize_item_id
			and pt.prize_id = 12
		
		where ep.event_participation_id = @identification
	
) te
group by [sponsor_name]
	--, (case when prize_item_code_id = 1 then prize_item_code end)
	--, (case when prize_item_code_id = 2 then prize_item_code end)
	, partner_name
GO
