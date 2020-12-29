USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_business129]    Script Date: 02/14/2014 13:07:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	mcote	Melissa Cote
-- ALTER  date: 2008/01/30
-- Description:	first email from FSM to sponsor
--		touch id need to be generated from the new interface
--		pull out all sponsor that should receive email from FSM
-- =============================================
CREATE    PROCEDURE [dbo].[es_touch_business129]
AS
BEGIN
    select 
	    t.touch_id
	    , t.event_participation_id as identification
	    , ep.event_id
	    , g.partner_id
	    , et.reply_to_name
	    , et.reply_to_email_address as reply_to_email
	    -- , m.first_name + ' ' + m.last_name as to_name
	    , ep.salutation as to_name
	    , m.email_address as to_email
	    , et.from_email_address as from_email
	    , et.from_name
	    , et.bounce_email_address as bounce_email
	    , m.culture_code
	    , et.param_procedure_call + ' ' +cast( t.event_participation_id as varchar(12)) + ', ' +  cast(t.touch_id as varchar(12)) as param_procedure_call
            , et.email_template_id
	    , etc.subject
	    , etc.body_text as body_text
	    , etc.body_html
	    , etc.footer_text
	    , etc.footer_html
		, ti.launch_date
    from touch t with(nolock)
	    inner join touch_info ti with(nolock)
			on ti.touch_info_id = t.touch_info_id
			and ti.business_rule_id = 129
	    inner join event_participation ep with(nolock)
			on ep.event_participation_id = t.event_participation_id
		inner join event e with(nolock)
			on e.event_id = ep.event_id
	    inner join member_hierarchy mh with(nolock)
			on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join member m with(nolock)
			on m.member_id = mh.member_id
	    inner join [group] g with(nolock)
			on g.sponsor_id = mh.member_hierarchy_id
	    inner join business_rule br with(nolock)
			on br.business_rule_id = ti.business_rule_id
	    inner join email_template et with(nolock)
			on et.email_template_id = br.email_template_id
	    inner join email_template_culture etc with(nolock)
			on etc.email_template_id = et.email_template_id
			and etc.culture_code = m.culture_code
	    inner join external_account ea  with(nolock)
			on ep.event_participation_id = ea.event_participation_id 
    where t.processed = 0
	  and mh.unsubscribe = 0
	  and m.unsubscribe = 0
      --and m.bounced = 0 
	  and mh.creation_channel_id = 42
	  and e.active = 1
      and mh.active = 1
      and m.deleted = 0
END
GO
