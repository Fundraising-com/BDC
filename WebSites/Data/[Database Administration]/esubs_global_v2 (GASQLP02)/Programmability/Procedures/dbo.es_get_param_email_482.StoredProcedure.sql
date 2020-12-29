USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_482]    Script Date: 02/14/2014 13:06:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =================================================
-- Author:		Jiro Hidaka	
-- Create date: 2011-10-5
-- Description:	BoxTops SC – Weekly Progress Report
--		Rule: 1 Week after 1st Sub, and every Week After
--		-	Send only if actions were taken – either subs sold, emails sent, or both
--			o	Minimum requirement for sending: 1 sub sold or 1 email sent
--		-	Include emails sent line only if emails were sent
--		-	Include subs sold line only if subs were sold
--		-	Include amount raised line only if money was raised
--		-	If no action taken, do not send
--		Repeat: Yes
--		business rule 198
-- =================================================
CREATE PROCEDURE [dbo].[es_get_param_email_482]
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
		, @total_subs = dbo.es_subs_by_event (event_id)
		, @week_amount = dbo.es_amount_by_event_date(event_id, @from_date, @to_date)
		, @total_amount = dbo.es_amount_by_event  (event_id)
		from event_participation  with(nolock)
		where event_participation_id = @identification

	select 
		 @source_id as source_id
		, m.first_name + ' ' + m.last_name as [sponsor_name]
		, 482 as email_template_id
        , case when u.email_address is null then m.email_address else u.email_address COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_email
		, case when u.password is null then m.password else u.password COLLATE SQL_Latin1_General_CP1_CI_AS end as sponsor_password
		, @identification as identification
		, e.redirect as group_name
		, e.event_name as campaign
		, p.partner_name
        , (CASE WHEN pers.redirect IS NULL THEN 'grouppage.aspx?e=' + CONVERT(VARCHAR(10), ep.event_id) ELSE pers.redirect END) AS redirect
		, convert(varchar(10), @from_date, 101) as from_day
		, convert(varchar(10), @to_date, 101) as to_day
		, @week_subs as weekly_stats
        , @total_amount as total_stats
	from
		member m with(nolock)
		inner join member_hierarchy mh with(nolock)
		on mh.member_id = m.member_id
		left join [users] u with (nolock)
		on m.[user_id] = u.[user_id]
		inner join event_participation ep with(nolock)
		on ep.member_hierarchy_id = mh.member_hierarchy_id
		inner join event e with(nolock)
		on ep.event_id = e.event_id
		inner join event_group eg with(nolock)
		on eg.event_id = e.event_id
		inner join [group] g with(nolock)
		on g.group_id = eg.group_id
		inner join partner p with (nolock)
		on p.partner_id = g.partner_id
        left join personalization pers with(nolock)
		on ep.event_participation_id = pers.event_participation_id
		where 
			ep.event_participation_id = @identification
GO
