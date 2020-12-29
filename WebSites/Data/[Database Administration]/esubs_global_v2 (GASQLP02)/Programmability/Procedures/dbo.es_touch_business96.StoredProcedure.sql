USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_business96]    Script Date: 02/14/2014 13:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Modified by :	Melissa Cote 
Date :			2010.08.27
Group Sponsor Flow w/ sub pages

KICKS-OFF; INVITES PARTICIPANTS W/ PROMPT TO PERSONALIZE
EMAIL A / 362 / Subject: Support our fundraising campaign for [++campaign name++]
	Sponsor - Invites Participants w/ Sub Page - Email A
	Email template 362, es_get_param_email_362
	Business Rule 96, esubs_global_v2.dbo.es_touch_business96
	exec [dbo].[es_touch_business96]
*/

CREATE PROCEDURE [dbo].[es_touch_business96]
AS
BEGIN
    select 
	    t.touch_id
	    , t.event_participation_id as identification
	    , g.partner_id
	    , et.reply_to_name
	    , et.reply_to_email_address as reply_to_email
	    , ep.salutation as to_name
	    , m.email_address as to_email
	    , et.from_email_address as from_email
	    , et.from_name
	    , et.bounce_email_address as bounce_email
	    , e.culture_code
	    , e.event_id
	    , et.param_procedure_call + ' ' +cast( t.event_participation_id as varchar(12)) + ', ' +  cast(t.touch_id as varchar(12)) as param_procedure_call
        , et.email_template_id
	    , ce.subject
	    , ce.body_txt as body_text
	    , ce.body_html
	    , etc.footer_text
	    , etc.footer_html
		, ti.launch_date
    from
	    touch t with (nolock)
	    inner join touch_info ti with (nolock)
	    on ti.touch_info_id = t.touch_info_id
	    and ti.business_rule_id = 96
	    inner join custom_email_template ce with (nolock)
	    on ce.touch_info_id = ti.touch_info_id
	    inner join event_participation ep with (nolock)
	    on t.event_participation_id = ep.event_participation_id
	    inner join member_hierarchy mh with (nolock)
	    on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join member m with (nolock)
	    on m.member_id = mh.member_id
	    inner join event e with (nolock)
	    on e.event_id = ep.event_id
	    inner join event_group eg with (nolock)
	    on e.event_id = eg.event_id
	    inner join [group] g with (nolock)
	    on g.group_id = eg.group_id	
	    inner join business_rule br with (nolock)
	    on br.business_rule_id = ti.business_rule_id
	    inner join email_template et with (nolock)
	    on et.email_template_id = br.email_template_id
	    inner join email_template_culture etc with (nolock)
	    on et.email_template_id = etc.email_template_id
	    and e.culture_code = etc.culture_code
		left join users u with (nolock)
			on m.[user_id] = u.[user_id]
    where t.processed = 0
      and mh.unsubscribe = 0
      and m.unsubscribe = 0
	  and (u.unsubscribe is null or u.unsubscribe = 0)
      and m.bounced = 0 
      and launch_date < getdate()
      and g.partner_id not in (719, 816)
	  and e.event_type_id = 1 -- group w/ participant sub page
	  and mh.active = 1
      and m.deleted = 0
END
GO
