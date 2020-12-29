USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_business98]    Script Date: 02/14/2014 13:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Modified by :	Melissa Cote 
Date :			2010.08.27
Group Sponsor Flow w/ sub pages

KICKS-OFF; INVITES PARTICIPANTS W/ PROMPT TO PERSONALIZE

 EMAIL C / 364 / Subject: Support our fundraising campaign for [++campaign name++]
	Sponsor - Invites Participants w/ Sub Page - Email C
	Email template 364, es_get_param_email_364
	Business Rule 98, esubs_global_v2.dbo.es_touch_business98
	

*/

CREATE PROCEDURE [dbo].[es_touch_business98]
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
	    and ti.business_rule_id = 98
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
		left join users u
			on m.[user_id] = u.[user_id]
    where t.processed = 0
      and mh.unsubscribe = 0
      and m.unsubscribe = 0
	  and (u.unsubscribe is null or u.unsubscribe = 0)
      and m.bounced = 0 
      and launch_date < getdate()
	  and e.event_type_id = 1 -- group w/ participant sub page
      and g.partner_id not in (719, 816)
      and mh.active = 1
      and m.deleted = 0
      and e.active = 1
      and mh.member_hierarchy_id not in (
        select parent_member_hierarchy_id
        from member_hierarchy
        where parent_member_hierarchy_id is not null
      )
      and ep.event_participation_id not in (
        select et.suppID
	    from qspecommerce.dbo.efundraisingtransaction et
		    inner join qspfulfillment.dbo.[order] o on o.order_id = et.orderid
		    inner join qspfulfillment.dbo.[order_detail] od on od.order_id = o.order_id
		    inner join event_participation ep on ep.event_participation_id = et.suppid
	    where o.order_status_id in ( 101, 110, 201, 301, 401, 501, 601, 701, 90, 9401 )
        group by et.suppID	   
      )
END
GO
