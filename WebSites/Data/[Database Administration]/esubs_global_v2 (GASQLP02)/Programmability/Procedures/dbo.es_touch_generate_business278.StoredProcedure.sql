USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_generate_business278]    Script Date: 02/14/2014 13:07:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- =============================================
-- Author:  Jiro Hidaka
-- Create  date: 2013/10/31
-- Description:	14-week post sub purchase
--		Rule: Send after x (dynamic) month of subscription purchase,only for 
--		campaign that are still active and participant name is specified in
--      QSPFulFillment..Email.reciepent column
--		Repeat: No
--		Subject: [++group_name++] thanks you for your ongoing support!
--		Reply to Name: [++partner_name++]
--		Reply to Email: efr-online@magfundraising.com
--		From Name: [++partner_name++]
--		From Email: efr-online@magfundraising.com
--		Email Template : 507
-- exec [es_touch_generate_business278]
-- =================================================
-- =============================================
CREATE PROCEDURE [dbo].[es_touch_generate_business278]
AS
BEGIN
	SET NOCOUNT ON;

declare @touch_info_id int
declare @from_date datetime
declare @to_date datetime
declare @from_recurrence datetime
declare @to_recurrence datetime

set @from_date = CONVERT(VARCHAR(30),DATEADD(WEEK, -14, getdate()), 101)
set @to_date = CONVERT(VARCHAR(30),DATEADD(DAY, 1, DATEADD(WEEK, -14, getdate())), 101)
set @from_recurrence = CONVERT(VARCHAR(30),getdate(), 101)
set @to_recurrence = CONVERT(VARCHAR(30),DATEADD(day, 1, getdate()), 101)

--select @from_date,@to_date,@from_recurrence,@to_recurrence

    begin transaction

    insert into touch_info(
	    business_rule_id
	    ,launch_date
	    ,create_date
    )values(
	    278
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
			    , getdate() as create_date				
		from    event_participation ep with(nolock) 
		                join esubs_global_v2.dbo.es_get_valid_orders_items() tps on tps.supp_id = ep.event_participation_id
						join [event] e with(nolock) on ep.event_id = e.event_id
						join event_group eg with (nolock) on eg.event_id =e.event_id
						join [group] g with (nolock) on g.group_id = eg.group_id
						join partner_product_offer ppo with (nolock) on ppo.partner_id = g.partner_id
						join (
							select min(tps.order_id) as order_id, tps.EmailAddress
							from esubs_global_v2.dbo.es_get_valid_orders_items() tps
							where tps.EmailAddress not like '%@efundraising.com' and product_type_id = 1
							group by  tps.EmailAddress
						) tt1 on tt1.order_id = tps.order_id
							
						left join (select touch.event_participation_id from touch  with (nolock) join touch_info with (nolock) on touch.touch_info_id = touch_info.touch_info_id
						           where touch_info.business_rule_id in(278) and touch.processed in (0, 2) and touch_info.launch_date between @from_recurrence and @to_recurrence)t on t.event_participation_id = ep.event_participation_id
					where tps.create_date between @from_date and @to_date 
						and e.culture_code = 'en-US'
						and e.active = 1 and tps.product_type_id  = 1
						and ppo.product_offer_id not in (5,7)
						and t.event_participation_id is null
						and g.partner_id not in (832, 719); -- Box Tops, QSP						
						
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
