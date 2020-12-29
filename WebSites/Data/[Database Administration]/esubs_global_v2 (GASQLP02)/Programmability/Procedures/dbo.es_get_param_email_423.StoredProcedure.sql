USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_423]    Script Date: 02/14/2014 13:05:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_param_email_423]
		@identification int
		,@source_id bigint
as

	declare @from_date datetime
	declare @to_date datetime
	declare @week_emails int 
	declare @total_emails int 
	declare @week_subs int 
	declare @total_subs int 
	declare @week_amount DECIMAL(9,2)  
	declare @total_amount DECIMAL(9,2)  

	set @from_date = getdate() 
	set @from_date = DATEADD(day, -7-DATEPART(dw, @from_date), @from_date)
	set @to_date = DATEADD(day, 7, @from_date)

	select 
		@week_emails = dbo.es_count_event_supp_invited_date (event_id, @from_date, @to_date)
		, @total_emails = dbo.es_count_event_supp_invited (event_id)
		, @week_subs = dbo.es_subs_by_event_date(event_id, @from_date, @to_date)
		, @total_subs = dbo.es_subs_by_event  (event_id)
		, @week_amount = dbo.es_amount_by_event_date(event_id, @from_date, @to_date)
		, @total_amount = dbo.es_amount_by_event  (event_id)
		from event_participation  with(nolock)
		where event_participation_id = @identification

	select 
		 @source_id as source_id
		, m.first_name + ' ' + m.last_name as [sponsor_name]
		, ep.salutation as [user_name]
		, 423 as email_template_id
		, m.email_address as user_email
		, m.password
		, @identification as identification
		, e.redirect as group_name
		, e.event_name as campaign
		, pav.value as partner_name
		,  convert(varchar(10), @from_date, 101) as from_day
		, convert(varchar(10), @to_date, 101) as to_day
		, (case 
			when @week_emails > 0 and @week_subs > 0 then 
			'	-	Your group sent ' + convert(varchar(20), @week_emails) + ' emails to friends and family, for a total of ' +convert(varchar(20), @total_emails)+ ' emails sent'
			+'	<br />-	Your group sold ' + convert(varchar(20), @week_subs) + ' magazine subscriptions, for a total of ' + convert(varchar(20), @total_subs) + ' magazines sold'
			+'	<br />-	Your group raised $'+convert(varchar(20), @week_amount)+', for a total of $' + convert(varchar(20), @total_amount)
			when @week_emails > 0 and @week_subs <= 0 then 
			'	-	Your group sent ' + convert(varchar(20), @week_emails) + ' emails to friends and family, for a total of ' +convert(varchar(20), @total_emails)+ ' emails sent'
			when @week_subs > 0 then
			'	<br />-	Your group sold ' + convert(varchar(20), @week_subs) + ' magazine subscriptions, for a total of ' + convert(varchar(20), @total_subs) + ' magazines sold'
			+'	<br />-	Your group raised $'+convert(varchar(20), @week_amount)+', for a total of $' + convert(varchar(20), @total_amount)
			else '' end)
			as txt_stats
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
		where 
			ep.event_participation_id = @identification
GO
