USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_generate_business273]    Script Date: 02/14/2014 13:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- =============================================
-- Author:  Pavel Tarassov
-- Create  date: 2013/05/06
-- Description:	Supporter subscription renewal
--		Rule: Send after x (dynamic) month of subscription purchase,only for 
--		campaign that are still active and participant name is specified in
--      QSPFulFillment..Email.reciepent column
--		Repeat: No
--		Subject: [++group_name++] thanks you for your ongoing support!
--		Reply to Name: [++partner_name++]
--		Reply to Email: efr-online@magfundraising.com
--		From Name: [++partner_name++]
--		From Email: efr-online@magfundraising.com
--		Email Template : 429
-- exec [es_touch_generate_business273]
-- =================================================
-- =============================================
CREATE PROCEDURE [dbo].[es_touch_generate_business273]

AS
BEGIN
	SET NOCOUNT ON;
    
 

declare @touch_info_id int
declare @from_date datetime
declare @to_date datetime
declare @from_recurrence datetime
declare @to_recurrence datetime
declare @time_gap int
declare @day_recurrence int
declare @backward int 

set @time_gap = 210 -- 7 months
set @day_recurrence = 30 -- 1 month
set @backward = 10 -- 1 month -- last was 200


set @from_date = CONVERT(VARCHAR(30),DATEADD(day, -@time_gap - @backward, getdate()), 101)
set @to_date = CONVERT(VARCHAR(30),DATEADD(day, -@time_gap +1, getdate()), 101)
set @from_recurrence = CONVERT(VARCHAR(30),DATEADD(day, - @day_recurrence, getdate()), 101)
set @to_recurrence = CONVERT(VARCHAR(30),DATEADD(day, 1, getdate()), 101)

    begin transaction

    insert into touch_info(
	    business_rule_id
	    ,launch_date
	    ,create_date
    )values(
	    273
	    ,getdate()
	    ,getdate()
    )
	
	set @touch_info_id = SCOPE_IDENTITY()

    if @@error <> 0
    begin
	    rollback transaction
    end
    else
    begin
    	
	    

	    insert into touch(event_participation_id,touch_info_id,processed,create_date)
		select DISTINCT 
		
			    ep.event_participation_id
			    ,@touch_info_id as touch_info_id
			    ,0 as processed
			    , getdate() as create_date --!!
				
		from    qspecommerce.dbo.efundraisingtransaction et with(nolock)
						inner join qspfulfillment.dbo.[order] o with(nolock) on o.order_id = et.orderid
						inner join event_participation ep with(nolock) on ep.event_participation_id = et.suppid
						inner join [event] e with(nolock) on ep.event_id = e.event_id
						inner join event_group eg with (nolock) on eg.event_id =e.event_id
						inner join [group] g with (nolock) on g.group_id = eg.group_id
						inner join partner_product_offer ppo with (nolock) on ppo.partner_id = g.partner_id
						inner join qspfulfillment.dbo.order_detail  od with(nolock) on od.order_id = o.order_id
						inner join qspfulfillment.dbo.catalog_item_detail cod with(nolock) on cod.catalog_item_detail_id = od.catalog_item_detail_id
						inner join qspfulfillment.dbo.catalog_item ci with(nolock) on ci.catalog_item_id =  cod.catalog_item_id 
						inner join qspfulfillment.dbo.product p with(nolock) on p.product_id = ci.product_id
						inner join dbo.es_get_valid_order_status() os on o.order_status_id = os.order_status_id
						inner join (
							select min(ord.order_id) as efrtOrderid, em.email_id
							from qspecommerce..efundraisingtransaction efrtr with(nolock)
								--on ep.event_participation_id = efrtr.SuppId
							inner join qspfulfillment..[order] ord with(nolock)on efrtr.OrderId = ord.order_id 
							inner join qspfulfillment..email em with(nolock) on ord.billing_email_id = em.email_id
							inner join qspfulfillment.dbo.order_detail  od with(nolock) on od.order_id = ord.order_id
							inner join qspfulfillment.dbo.catalog_item_detail cod with(nolock) on cod.catalog_item_detail_id = od.catalog_item_detail_id
							inner join qspfulfillment.dbo.catalog_item ci with(nolock) on ci.catalog_item_id =  cod.catalog_item_id 
							inner join qspfulfillment.dbo.product p with(nolock) on p.product_id = ci.product_id and p.product_type_id  = 1
							inner join dbo.es_get_valid_order_status() os on ord.order_status_id = os.order_status_id
							where em.email_address not like '%@efundraising.com'
							group by  em.email_id
						) tt1 on tt1.efrtOrderid = et.OrderId
							
						left join (select touch.event_participation_id from touch  with (nolock) inner join touch_info with (nolock) on touch.touch_info_id = touch_info.touch_info_id
						where touch_info.business_rule_id in(273, 208) and touch.processed in (0, 2) and touch_info.launch_date between @from_recurrence and @to_recurrence)t on t.event_participation_id = ep.event_participation_id
					where et.CreateDate between @from_date and @to_date 
						and e.culture_code = 'en-US'
						and e.active = 1 and p.product_type_id  = 1  -- BY PT: send renewal only for magazine purchases (ie product type is 1)
						and ppo.product_offer_id not in (5,7)
						and t.event_participation_id is null
						and g.partner_id not in (832, 719) -- Box Tops, QSP
						
						
		IF @@ROWCOUNT = 0 -- no rows inserted
	    begin
		    rollback transaction
	    end
	    else
	    begin
		    commit transaction
	    end
    end
END
GO
