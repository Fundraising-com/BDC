USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_463]    Script Date: 02/14/2014 13:05:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
Create by : Melissa Cote
Create date : 2010.09.23
Group Sponsor Flow w/ sub pages (MAGAZINES ONLY)

EMAIL D / 463 / Subject: Last chance to make our fundraiser a success!
	Sponsor - Invites Participants w/ Sub Page - Email D (MAGAZINES ONLY)
	Email template 463, es_get_param_email_463
	Business Rule 179, esubs_global_v2.dbo.es_touch_business179

*/
CREATE procedure [dbo].[es_get_param_email_463]
		@identification int
		,@source_id bigint
as
	select 
		 @source_id as source_id
		--, m.first_name + ' ' + m.last_name as [supporter]
		--, m.first_name + ' ' + m.last_name as [participant]    
		, ep.salutation as supporter
		, ep.salutation as participant
		, 463 as email_template_id
		, a.sponsor_name 
		, a.sponsor_email
		, @identification as identification
		, e.event_name as campaign
      	, ep.salutation as participant_name
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
				member_hierarchy_id
				,m.email_address as sponsor_email
				,m.first_name + ' ' + m.last_name as sponsor_name
			from 
				member_hierarchy mh
				inner join member m
				on m.member_id = mh.member_id
			) a
		on a.member_hierarchy_id = mh.parent_member_hierarchy_id
        inner join event_group eg
        on eg.event_id = e.event_id
        inner join [group] g
        on g.group_id = eg.group_id
		where 
			ep.event_participation_id = @identification
GO
