USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_event_participation_email_sent]    Script Date: 02/14/2014 13:05:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*  retourne group emails
    mcote 2009-03-27

	exec [es_get_event_participation_email_sent] 1005065
	
*/

CREATE procedure [dbo].[es_get_event_participation_email_sent]
	@event_participation_id int
as
BEGIN

	select 
		Convert(datetime, Convert(varchar(2), month(launch_date)) + '/' + convert(varchar(2), day(launch_date)) + '/' + convert(varchar(4), year(launch_date))) as launch_date
		, (case 
				-- Maximum 30 caracteres for description	then 'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX'
				when br.business_rule_id in (59,96,115)		then 'Participant First Email'
				when br.business_rule_id in (97, 116)		then 'Participant First Reminder'
				when br.business_rule_id in (98, 117)		then 'Participant Second Reminder'
				when br.business_rule_id = 99				then 'Participant Reminder'
				when br.business_rule_id = 100				then 'Participant Personal Note'
				when br.business_rule_id = 101				then 'Supporter First Email'
				when br.business_rule_id = 102				then 'Supporter First Reminder'
				when br.business_rule_id = 103				then 'Supporter Second Reminder'
			else '' end ) as email_desc
		--, br.business_rule_id
		--, business_rule_name
		, count(*) as email_sent
	from touch t with(nolock)
	inner join touch_info ti with(nolock) on ti.touch_info_id = t.touch_info_id
	inner join business_rule br with(nolock) on ti.business_rule_id = br.business_rule_id
	inner join event_participation ep with(nolock) on ep.event_participation_id = t.event_participation_id
	--inner join event_group eg with(nolock) on ep.event_id = eg.event_id
	--inner join [group] g with(nolock) on eg.group_id = g.group_id
	--inner join partner p with(nolock) on g.partner_id = p.partner_id
	where ep.event_participation_id = @event_participation_id --launch_date between @start_date and @end_date 
		and br.business_rule_id in (59,96,115,97,116,98,117,99,100,101,102,103)
	group by Convert(datetime,convert(varchar(2), month(launch_date)) + '/' + convert(varchar(2), day(launch_date)) + '/' + convert(varchar(4), year(launch_date)))
		,br.business_rule_id , business_rule_name
	order by Convert(datetime,convert(varchar(2), month(launch_date)) + '/' + convert(varchar(2), day(launch_date)) + '/' + convert(varchar(4), year(launch_date))) desc
		,br.business_rule_id , business_rule_name
END
GO
