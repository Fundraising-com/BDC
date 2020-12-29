USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_sales_by_touch_info_id]    Script Date: 02/14/2014 13:06:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================
-- Author:		Jiro Hidaka
-- Create date: June 30, 2011
-- Description:	Returns all sales by touch info id
--      EXCLUDE Holiday Reminders
-- ===============================================
CREATE PROCEDURE [dbo].[es_get_sales_by_touch_info_id] 
	@touch_info_id int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT SUM(COALESCE(od.price*od.quantity,0)) as order_amount
		 , SUM(COALESCE(od.quantity,0)) as quantity
         , t.touch_id 
         , ep.event_participation_id
         , ep.member_hierarchy_id
         , t.touch_info_id
	FROM dbo.touch t with(nolock)
		INNER JOIN event_participation ep with(nolock)
            ON t.event_participation_id  = ep.event_participation_id
        INNER JOIN touch_info ti with(nolock) 
            ON ti.touch_info_id = t.touch_info_id
        INNER JOIN business_rule br with (nolock)
            ON ti.business_rule_id = br.business_rule_id
        INNER JOIN email_template etp with (nolock)
            ON br.email_template_id = etp.email_template_id
		LEFT JOIN QSPEcommerce.dbo.efundraisingtransaction as et with(nolock)
            ON ep.event_participation_id = et.suppid and t.touch_id = et.TouchId
		LEFT JOIN QSPFulfillment.dbo.[order] as o with(nolock) 
			ON et.orderID = o.order_id AND o.deleted = 0 AND o.create_date >= t.create_date
		LEFT JOIN QSPFulfillment.dbo.[order_detail] as od with(nolock)
            ON od.order_id = o.order_id AND od.deleted = 0 
		LEFT JOIN [esubs_global_v2].[dbo].es_get_valid_order_status() os ON os.order_status_id = o.order_status_id
--		LEFT JOIN web_tracking_2..visitor_log vl with(nolock) on vl.visitor_log_id =  right(qxtrak, (case when len(qxtrak) > 10 and CHARINDEX('VLID',qxtrak) <> 0 then (len(qxtrak) - CHARINDEX('VLID',qxtrak)-3) else 0 end))
--              and vl.touch_id is not null and vl.touch_id = t.touch_id
	WHERE t.touch_info_id = @touch_info_id AND etp.email_template_name <> 'Holiday Reminder' 
    GROUP BY t.touch_id, ep.event_participation_id, ep.member_hierarchy_id, t.touch_info_id    
    HAVING SUM(COALESCE(od.price*od.quantity,0)) > 0
END
GO
