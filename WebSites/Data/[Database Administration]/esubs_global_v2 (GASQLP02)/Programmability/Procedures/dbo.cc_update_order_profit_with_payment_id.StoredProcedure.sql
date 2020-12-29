USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_update_order_profit_with_payment_id]    Script Date: 02/14/2014 13:05:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

Description:
Created by: JF Lavigne
Created date: 


*/

CREATE   PROCEDURE [dbo].[cc_update_order_profit_with_payment_id] -- 1018365,'11-30-2006',33
    @event_id int
   ,@end_date datetime
    ,@new_payment_id int = null
    ,@old_payment_id int = null
    
AS
BEGIN
  
if @old_payment_id is null 
begin
    update order_profit set payment_id = @new_payment_id
    where event_id = @event_id and
      payment_id is null and
      order_date < @end_date

end
else
begin
    update order_profit set payment_id = @new_payment_id
    where event_id = @event_id and
      payment_id = @old_payment_id and
      order_date < @end_date

end
END
GO
