USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_campaign_supporter]    Script Date: 02/14/2014 13:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_rpt_campaign_supporter]
	@event_id int,
	@event_participation_id int = null
AS
BEGIN
    SET NOCOUNT ON  
       SELECT lower(ISNULL(rtrim(ltrim(c.first_name)), '') + ' ' + ISNULL(rtrim(ltrim(c.last_name)), '')) as fullname
	 , et.createdate as create_date
	 , et.ImageMotivationComment
	 , sum(od.price*od.quantity) as AmountTotal
	 FROM event_participation ep with (nolock)
	INNER join qspecommerce.dbo.efundraisingtransaction et with (nolock) on et.suppid = ep.event_participation_id
	INNER join qspfulfillment.dbo.[order] o with (nolock) on o.order_id = et.orderid
	INNER join qspfulfillment.dbo.[order_detail] od with (nolock) ON od.order_id = o.order_id
	INNER join member_hierarchy mh  with (nolock) on ep.member_hierarchy_id = mh.member_hierarchy_id
        INNER join member m  with (nolock) on mh.member_id = m.member_id
	INNER join qspfulfillment.dbo.[customer] c with (nolock) on c.customer_id = o.customer_id
    INNER JOIN event e with (nolock) on ep.event_id = e.event_id
	INNER JOIN [esubs_global_v2].[dbo].es_get_valid_order_status() os ON os.order_status_id = o.order_status_id
	WHERE e.event_id = @event_id
	  AND ep.event_participation_id = ISNULL(@event_participation_id, ep.event_participation_id)
      	  AND et.IsImageMotivation = 1	
      	  AND ISNULL(rtrim(ltrim(c.first_name)), '') + ' ' + ISNULL(rtrim(ltrim(c.last_name)), '')<>' '
	--  AND len(rtrim(ltrim(et.ImageMotivationComment))) > 0
        GROUP BY 
	   lower(ISNULL(rtrim(ltrim(c.first_name)), '') + ' ' + ISNULL(rtrim(ltrim(c.last_name)), ''))
	 , et.AmountTotal
	 , et.createdate
	 , et.ImageMotivationComment
    	ORDER BY sum(od.price*od.quantity) desc
END
GO
