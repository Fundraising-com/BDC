USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_business151]    Script Date: 02/14/2014 13:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec [es_touch_business151]
CREATE PROCEDURE [dbo].[es_touch_business151]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN
    select top 250
	    t.touch_id
	    , t.event_participation_id as identification
	    , g.partner_id
	    , et.reply_to_name
	    , et.reply_to_email_address as reply_to_email
	    , dbo.[es_insert_space_before_CapCase](em.recipient_name) as [to_name]
		, em.email_address as [to_email]
	    , et.from_email_address as from_email
	    , et.from_name
	    , et.bounce_email_address as bounce_email
	    , e.culture_code
		, e.event_id
	    , et.param_procedure_call + ' ' +cast( efrtr.Id as varchar(12)) + ', ' +  cast(t.touch_id as varchar(12)) as param_procedure_call
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
			and ti.business_rule_id = 151
	    inner join event_participation ep with(nolock) 
	    	on t.event_participation_id = ep.event_participation_id
	    inner join member_hierarchy mh with(nolock)
			on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join member m with(nolock)
			on m.member_id = mh.member_id
	    inner join [event] e with(nolock)
			on e.event_id = ep.event_id
	    inner join event_group eg with(nolock)
			on e.event_id = eg.event_id
	    inner join [group] g with(nolock)
			on g.group_id = eg.group_id	
	    inner join business_rule br with(nolock)
			on br.business_rule_id = ti.business_rule_id
	    inner join email_template et with(nolock)
			on et.email_template_id = br.email_template_id
	    inner join email_template_culture etc with(nolock)
			on etc.email_template_id = et.email_template_id
			and etc.culture_code = e.culture_code
		inner join [partner] p  with(nolock)
			on p.partner_id = g.partner_id
		inner join qspecommerce..efundraisingtransaction efrtr with(nolock)
			on ep.event_participation_id = efrtr.SuppId
		inner join qspfulfillment..[order] ord with(nolock)
			on efrtr.OrderId = ord.order_id
		inner join qspfulfillment..email em with(nolock)
			on ord.billing_email_id = em.email_id
		inner join (
			select min(ord.order_id) as efrtOrderid, em.email_id
			from qspecommerce..efundraisingtransaction efrtr with(nolock)
				--on ep.event_participation_id = efrtr.SuppId
			inner join qspfulfillment..[order] ord with(nolock)
				on efrtr.OrderId = ord.order_id
			inner join qspfulfillment..email em with(nolock)
				on ord.billing_email_id = em.email_id
			group by  em.email_id
			) tt1 on tt1.efrtOrderid = efrtr.OrderId
    where t.processed = 0
	  and mh.unsubscribe = 0
	  and m.unsubscribe = 0
      and m.bounced = 0 
	  and e.active = 1
	  and g.partner_id not in (832, 719) -- Box Tops, QSP
	  and mh.active = 1
      and m.deleted = 0
	END
END
GO
