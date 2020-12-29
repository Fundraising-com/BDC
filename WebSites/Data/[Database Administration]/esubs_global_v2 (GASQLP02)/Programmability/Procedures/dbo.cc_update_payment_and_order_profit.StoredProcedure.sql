USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_update_payment_and_order_profit]    Script Date: 02/14/2014 13:05:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

Description:
Created by: JF Lavigne
Created date: 

Ex: dbo.cc_updatePaymentAndOrderProfit 1009342,1008316

*/

CREATE  PROCEDURE [dbo].[cc_update_payment_and_order_profit]
    @good_event_id int
    , @rip_event_id int
AS
BEGIN
    declare @rip_payment_info_id int
    declare @good_payment_info_id int
       
    --get payment info id
    select @rip_payment_info_id = pi.payment_info_id
    from event_group ep
        left join payment_info pi on ep.group_id = pi.group_id
    where ep.event_id = @rip_event_id
       
    select @good_payment_info_id = pi.payment_info_id
    from event_group ep
        left join payment_info pi on ep.group_id = pi.group_id
    where ep.event_id = @good_event_id
       

    UPDATE order_profit
    set event_id = @good_event_id
    where event_id = @rip_event_id
    
    if @rip_payment_info_id is not null and @good_payment_info_id is not null
        UPDATE payment 
        set payment_info_id = @good_payment_info_id
        where payment_info_id in (select pi.payment_info_id from event_group ep
                                  left join payment_info pi on ep.group_id = pi.group_id
                                   where ep.event_id = @rip_event_id)
    else
        return -1


    --c impossiblee d'automatiser totalment les 100% mais..
    --si ya 2 100%, on enleve toujours le plus recent si il n'a pas de cheque
    declare @count int

    --2 100% ?
    select @count = count(order_profit_id)
    from order_profit
    where event_id = @good_event_id 
      and profit = 1.0

        
    if @count = 2
    begin   
        update order_profit
        set profit = 0.4
          , total_profit = item_price * 0.4
        where  order_profit_id = (
            --prend le 100% le plus recent          
            SELECT TOP 1 order_profit_id
            FROM dbo.order_profit
            where profit = 1.0
              AND payment_id is null
              and event_id = @good_event_id
            ORDER BY order_date DESC
        )
        
        return 0
        
    end
    else
    begin
        return -1 
    end
END
GO
