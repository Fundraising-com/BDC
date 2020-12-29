USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_399]    Script Date: 02/14/2014 13:05:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: mcote Melissa Cote
-- ALTER date: 2008/02/07
-- Description: get param for BR 129-130
-- business logic to get good domain url 
-- =============================================
CREATE   procedure [dbo].[es_get_param_email_399]
	@identification int
	, @source_id bigint
AS
BEGIN

	SELECT DISTINCT 
		@source_id as source_id
		, m.first_name + ' ' + m.last_name as [participant]
		-- MCOTE  collate SQL_Latin1_General_CP1_CI_AI by the time we rebuild the table event_participantion to change the salutation field for  collate SQL_Latin1_General_CP1_CI_AI
	    , (case when ep.salutation is null then m.first_name + ' ' + m.last_name collate SQL_Latin1_General_CP1_CI_AI else ep.salutation collate SQL_Latin1_General_CP1_CI_AI end ) as participant
		, 399 as email_template_id
		, Convert(varchar(100), m.email_address) as sponsor_email
		, m.first_name + ' ' + m.last_name as sponsor_name
		, @identification as identification
		, e.event_name as campaign
		, fsm.first_name + ' ' + fsm.last_name as fsm
		, u.email as fsm_email -- email 
		, (case when g.partner_id = 0 
			then 'http://www.magfundraising.com/' + e.redirect 
			else 'http://online.qsp.com/' + e.redirect 
			end) as group_url
		, (case when g.partner_id = 0 
			then 'http://www.magfundraising.com/'
			else 'http://www.qsp.com/'
			end) as login_url
		, (case when g.partner_id = 0 
			then 'http://www.magfundraising.com/'
			else 'http://www.qsp.com/ManageFundraiser.aspx'
			end) as login_href
		, (case when g.partner_id = 0 
			then Convert(varchar(100), m.email_address)
			else Convert(varchar(100), act.fulf_account_id)
			end) as username
		, (case when g.partner_id = 0 
			then m.[password]
			else ar.[password] COLLATE SQL_Latin1_General_CP1_CI_AS end) as [password]
	from member m with(nolock)
		inner join member_hierarchy mh with(nolock)
			on mh.member_id = m.member_id
		inner join event_participation ep with(nolock)
			on ep.member_hierarchy_id = mh.member_hierarchy_id
		inner join event e with(nolock)
			on ep.event_id = e.event_id
		-- log table
		inner join event_group eg  with(nolock)
			on e.event_id = eg.event_id 
		inner join [group] g  with(nolock)
			on eg.group_id = g.group_id 
		inner join external_account ea with(nolock)
			on ea.event_participation_id = ep.event_participation_id
		-- QSP DB 
		inner join QSPFulfillment.dbo.Account act  with(nolock)
			on ea.online_account_id = act.account_id
		inner join QSPFulfillment.dbo.organization o with(nolock)
			on act.organization_id = o.organization_id
		--inner join QSPFulfillment.dbo.campaign c
			-- on act.account_id = c.account_id
		--inner join QSPFulfillment.dbo.[order] ord
			-- on c.campaign_id = ord.campaign_id 
		inner join QSPFulfillment.dbo.field_sales_manager fsm with(nolock)
			on act.fm_id = fsm.fm_id
			and fsm.deleted = 0
		inner join QSPFulfillment.dbo.[user] u with(nolock)
			on fsm.[user_id] = u.[user_id]
		LEFT JOIN [QSPFulfillment].[dbo].[reserved_account]r with(nolock)
			ON ea.[online_account_id] = r.Account_id
		LEFT JOIN [QSPEcommerce].[dbo].[AccountReportingUsers] ar with(nolock)
			--ON r.fulf_Account_id = ar.fulf_Account_id
			on o.[business_division_id] = ar.[x_business_division_id]
			AND act.[fulf_account_id] = ar.[fulf_account_id]
			AND ar.[DeletedTF] = 0 
	where ep.event_participation_id = @identification



END

--select * from QSPFulfillment.dbo.Account act
GO
