USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_event_profit_check]    Script Date: 02/14/2014 13:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_rpt_event_profit_check] @event_id int 

AS
BEGIN
 select p.cheque_number 
  , Convert(varchar(10),Isnull(pp.end_date, p.cheque_date), 101)  as check_date
     , p.paid_amount
 from payment_info pin 
  inner join payment p on pin.payment_info_id = p.payment_info_id
  left join
  ( 
   select pps.payment_id
     ,pps.payment_status_id
   from payment_payment_status pps
         inner join
        (
    select payment_id, max(create_date) as create_date
    from payment_payment_status
    group by payment_id
        ) ppsNew on ppsNew.payment_id = pps.payment_id and ppsNew.create_date = pps.create_date 
  ) pcancel on pcancel.payment_id = p.payment_id
        left join payment_period pp on pp.payment_period_id = p.payment_period_id
    where (event_id = @event_id and isnull(pcancel.payment_status_id, 2) <> 9)
    order by Isnull(pp.end_date, p.cheque_date)
END
GO
