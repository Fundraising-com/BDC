USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_business120]    Script Date: 02/14/2014 13:07:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Philippe Girard
-- Create date: March 31, 2006
-- Description:	1st Reminder to supporter from sponsor
-- =============================================

/*
modified 
by:  Krystian Olszanski
on: April 27, 2006
modification: added condition for unsubscribed members 
*/

CREATE PROCEDURE [dbo].[es_touch_business120]
AS
BEGIN

--    drop table #tps

    -- pre-generate the tps
    create table #tps (
	    rownum int identity(1,1)
	    , orderid int
	    , quantity int
	    , price money
	    , suppID int
            , createdate datetime
    )
    
	INSERT INTO #tps (
		orderid
	     , quantity
	     , price
	     , suppID
	     , createdate
	)
	select orderid
	     , quantity
	     , price
	     , suppID
	     , createdate
	from
	(
	select o.order_id as orderid
	     , od.quantity
	     , od.price
	     , et.suppID
             , et.createdate
	from qspecommerce.dbo.efundraisingtransaction et with(nolock)
		inner join qspfulfillment.dbo.[order] o with(nolock) on o.order_id = et.orderid
		inner join qspfulfillment.dbo.[order_detail] od with(nolock) on od.order_id = o.order_id
		inner join event_participation ep with(nolock) on ep.event_participation_id = et.suppid
        inner join touch t with(nolock) on t.event_participation_id = ep.event_participation_id
        inner join touch_info ti with(nolock) on ti.touch_info_id = t.touch_info_id
	where o.order_status_id in ( 101, 110, 201, 301, 401, 501, 601, 701, 90, 9401 )
      and t.processed = 0
      and ti.launch_date < getdate()
      and ti.business_rule_id = 120
	/*union all
	select oh.ID as orderid
	     , sod.quantity
	     , cid.value as price
	     , et.suppID
	     , et.createdate
 	from qspecommerce.dbo.efundraisingtransaction et  with(nolock)
		inner join qspstore.dbo.orderheader oh with(nolock) on oh.id = et.orderid
		inner join qspstore.dbo.suborderheader soh with(nolock) on oh.id = soh.orderheaderid 
		inner join qspstore.dbo.suborderdetail sod with(nolock) on soh.id = sod.suborderheaderid
		inner join qspstore.dbo.catalogitemdetail cid with(nolock) on cid.id = sod.catalogitemdetailid
		inner join event_participation ep with(nolock) on ep.event_participation_id = et.suppid
        inner join touch t with(nolock) on t.event_participation_id = ep.event_participation_id
        inner join touch_info ti with(nolock) on ti.touch_info_id = t.touch_info_id
	WHERE oh.OrderTotal IS NOT NULL
	  AND oh.OrderStatusID NOT IN (1, 10, 11)
	  AND soh.ShipToAddressID <> 0
	  and oh.aggregatorid in (7,13)
      and t.processed = 0
      and ti.launch_date < getdate()
      and ti.business_rule_id = 120*/
	) t
	order by  createdate
 		, price
    
    create index ix_suppid on #tps (suppid)
    
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
	    touch t with(nolock)
	    inner join touch_info ti  with(nolock)
            on ti.touch_info_id = t.touch_info_id
	        and ti.business_rule_id = 120
	    inner join custom_email_template ce with(nolock)
	        on ce.touch_info_id = ti.touch_info_id
	    inner join event_participation ep with(nolock)
	        on t.event_participation_id = ep.event_participation_id
	    inner join member_hierarchy mh with(nolock)
	        on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join member m with(nolock)
	        on m.member_id = mh.member_id
	    inner join event e with(nolock)
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
	        on et.email_template_id = etc.email_template_id
	        and e.culture_code = etc.culture_code
        left join member_hierarchy child with(nolock)
            on child.parent_member_hierarchy_id = mh.member_hierarchy_id
        left join #tps tps with(nolock)
            on tps.suppID = ep.event_participation_id
    where t.processed = 0
      and ti.launch_date < getdate()
      and child.member_hierarchy_id is null
      and tps.suppID is null
      and mh.unsubscribe = 0
      and m.unsubscribe = 0
      and m.bounced = 0 
      and g.partner_id = 719
      and e.active = 1
      and mh.active = 1
      and m.deleted = 0
END
GO
