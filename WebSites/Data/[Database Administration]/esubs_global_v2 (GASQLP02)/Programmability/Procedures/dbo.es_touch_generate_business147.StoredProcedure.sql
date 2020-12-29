USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_generate_business147]    Script Date: 02/14/2014 13:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Melissa Cote
-- Create date: 2008/10/17
-- Description: Registered – Sponsor Weekly Progress Report (CAN)
--   --
-- Rule: 1 Week after 1st Sub, and every Week After
-- - Send only if actions were taken – either subs sold, emails sent, or both
-- o Minimum requirement for sending: 1 sub sold or 1 email sent
-- - Include emails sent line only if emails were sent
-- - Include subs sold line only if subs were sold
-- - Include amount raised line only if money was raised
-- - If no action taken, do not send
-- Repeat: Yes
--
-- =============================================
CREATE PROCEDURE [dbo].[es_touch_generate_business147]
AS
BEGIN
SET NOCOUNT ON;
  
declare @touch_info_id int
declare @from_date datetime
declare @to_date datetime
  
set @from_date = getdate()
set @from_date = DATEADD(day, -7-DATEPART(dw, @from_date), @from_date)
set @to_date = DATEADD(day, 7, @from_date)
  

  
begin transaction
  
insert into touch_info(
   business_rule_id
   ,launch_date
   ,create_date
   )values(
   147
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
   select
    ep.event_participation_id
    ,@touch_info_id as touch_info_id
    ,0 as processed
    , getdate() as create_date
   from
    event e with(nolock)
     inner join event_group eg with(nolock)
      on eg.event_id = e.event_id
     inner join [group] g with(nolock)
      on g.group_id = eg.group_id
     inner join event_participation ep with(nolock)
      on e.event_id = ep.event_id
     and ep.participation_channel_id = 3 --sponsor
   left outer join (
     SELECT t.event_participation_id
      , MAX(t.create_date) AS create_date
     from
      touch t with(nolock)
       inner join touch_info ti with(nolock)
        on ti.touch_info_id = t.touch_info_id
         and business_rule_id = 147
	 GROUP BY t.event_participation_id
       ) t
       
       on t.event_participation_id = ep.event_participation_id
       left outer join 
       (
        select count(distinct mh.member_id) as nb_emails
          , event_id
        from event_participation ep with(nolock)
         inner join member_hierarchy mh with(nolock)
          on mh.member_hierarchy_id = ep.member_hierarchy_id
        where mh.creation_channel_id in(12,14,29,32, 35,38)
         and ep.create_date between @from_date and @to_date
        group by event_id
       ) supp on supp.event_id = e.event_id
       left outer join 
       (
        select
         sum(od.quantity) as nb_subs
         , event_id
        from qspecommerce.dbo.efundraisingtransaction et with(nolock)
         inner join qspfulfillment.dbo.[order] o with(nolock) on o.order_id = et.orderid
         inner join qspfulfillment.dbo.[order_detail] od with(nolock) on od.order_id = o.order_id
         inner join event_participation ep with(nolock) on ep.event_participation_id = et.suppid
         inner join dbo.es_get_valid_order_status() os on o.order_status_id = os.order_status_id
        where o.order_date between @from_date and @to_date
         group by event_id
       )tps on tps.event_id = e.event_id
  
     where
      (t.event_participation_id is null or DATEDIFF(day, t.create_date, @to_date) > 7)
        and e.culture_code = 'en-CA'
		and e.active = 1
        and g.partner_id <> 143
        and (isnull(nb_emails, 0) > 0 or isnull(nb_subs, 0) > 0)
  
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
