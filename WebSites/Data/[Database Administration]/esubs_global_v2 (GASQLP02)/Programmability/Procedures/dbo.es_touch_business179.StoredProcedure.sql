USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_business179]    Script Date: 03/20/2015 12:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

/*
Modified by :	Melissa Cote 
Date :			2010.08.27
Group Sponsor Flow w/ sub pages

KICKS-OFF; INVITES PARTICIPANTS W/ PROMPT TO PERSONALIZE

EMAIL D / 463 / Subject: Last chance to make our fundraiser a success!
	Sponsor - Invites Participants w/ Sub Page - Email D (MAGAZINES ONLY)
	Email template 463, es_get_param_email_463
	Business Rule 179, esubs_global_v2.dbo.es_touch_business179
	EXEC [dbo].[es_touch_business179]
*/

ALTER PROCEDURE [dbo].[es_touch_business179]
AS
BEGIN
    select 
	    t.touch_id
	    , t.event_participation_id as identification
	    , g.partner_id
	    , et.reply_to_name
	    , et.reply_to_email_address as reply_to_email
	    --, m.first_name + ' ' + m.last_name as to_name
	    , ep.salutation as to_name
	    , m.email_address as to_email
	    , et.from_email_address as from_email
	    , et.from_name
	    , et.bounce_email_address as bounce_email
	    , e.culture_code
	    , et.param_procedure_call + ' ' +cast( t.event_participation_id as varchar(12)) + ', ' +  cast(t.touch_id as varchar(12)) as param_procedure_call
	    , ce.subject
	    , ce.body_txt as body_text
	    , ce.body_html
	    , etc.footer_text
	    , etc.footer_html
	    , e.event_id
	    , et.email_template_id
		, ti.launch_date
    from
	    touch t
	    inner join touch_info ti
	    on ti.touch_info_id = t.touch_info_id
	    and ti.business_rule_id = 179
	    inner join custom_email_template ce
	    on ce.touch_info_id = ti.touch_info_id
	    inner join event_participation ep
	    on t.event_participation_id = ep.event_participation_id
	    inner join member_hierarchy mh
	    on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join member m
	    on m.member_id = mh.member_id
	    inner join event e
	    on e.event_id = ep.event_id
	    inner join event_group eg
	    on e.event_id = eg.event_id
	    inner join [group] g
	    on g.group_id = eg.group_id	
	    inner join business_rule br
	    on br.business_rule_id = ti.business_rule_id
	    inner join email_template et
	    on et.email_template_id = br.email_template_id
	    inner join email_template_culture etc
	    on et.email_template_id = etc.email_template_id
	    and e.culture_code = etc.culture_code
	    left join esubs_global_v2.dbo.es_get_valid_orders_items() tps
	    on tps.supp_id = ep.event_participation_id
    where t.processed = 0
      and mh.unsubscribe = 0
      and m.unsubscribe = 0
      and m.bounced = 0 
      and launch_date < getdate()
      and e.event_type_id = 1 -- group w/ participant sub page 
      and g.partner_id not in (719, 816, 708)
      and mh.active = 1
      and m.deleted = 0
      and e.active = 1
      and mh.member_hierarchy_id not in (
        select parent_member_hierarchy_id
        from member_hierarchy
        where parent_member_hierarchy_id is not null
      )
      and tps.supp_id is null
END

