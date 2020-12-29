USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_business128]    Script Date: 02/14/2014 13:07:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Business rule eLaunch #1
	Created by: Philippe Girard
	Created on: 2008/01/39
*/
CREATE   PROCEDURE [dbo].[es_touch_business128]
AS
BEGIN
	
	select 
		t.touch_id
		, t.event_participation_id as identification
		, 0 as partner_id
		, et.reply_to_name
		, et.reply_to_email_address as reply_to_email
		, fsm.first_name + ' ' + fsm.last_name as to_name
		, u.email as to_email
		, et.from_email_address as from_email
		, et.from_name
		, et.bounce_email_address as bounce_email
		, 'en-US' as culture_code
		, 0 as event_id
		, et.param_procedure_call + ' ' + cast(t.touch_id as varchar(12)) as param_procedure_call
		, et.email_template_id
		, etc.subject
		, etc.body_text as body_text
		, etc.body_html
		, etc.footer_text
		, etc.footer_html
		, ea.fsm_id
    from
	    touch t with(nolock)
	    inner join touch_info ti  with(nolock)
            on ti.touch_info_id = t.touch_info_id
	        and ti.business_rule_id = 128
		inner join external_account ea with(nolock)
			on ea.touch_id = t.touch_id
		inner join QSPFulfillment.dbo.Account A  with(nolock)
			on A.account_id = ea.food_account_id
		    and A.[deleted] = 0
		inner join QSPFulfillment.dbo.field_sales_manager fsm with(nolock)
			on fsm.fm_id = A.fm_id
			and fsm.[deleted] = 0
		inner join QSPFulfillment.dbo.[user] u with(nolock)
			on u.[user_id] = fsm.[user_id]
           and u.[deleted] = 0
	    inner join business_rule br with(nolock)
	        on br.business_rule_id = ti.business_rule_id
	    inner join email_template et with(nolock)
	        on et.email_template_id = br.email_template_id
	    inner join email_template_culture etc with(nolock)
	        on et.email_template_id = etc.email_template_id
	        and etc.culture_code = 'en-US'
    where t.processed = 0
      and launch_date < getdate()
END
GO
