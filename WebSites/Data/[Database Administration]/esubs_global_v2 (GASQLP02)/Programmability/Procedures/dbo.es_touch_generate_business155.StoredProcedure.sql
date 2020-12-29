USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_generate_business155]    Script Date: 02/14/2014 13:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Pavel Tarassov
-- Create date: 10-11-2009
-- Description: Runs once per day, sends email to the sponsor of the campaing
-- whenever first subs is generated in the event but sponsor did not provide 
-- the payment info yet (missing payment_info is null still)
-- =============================================

CREATE PROCEDURE [dbo].[es_touch_generate_business155]
AS
BEGIN
SET NOCOUNT ON;
		
declare @touch_info_id int

begin transaction
		
insert into touch_info(
business_rule_id
,launch_date
,create_date
)values(
155
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
		inner join payment_info [pi] with(nolock)
		on [pi].group_id = g.group_id
		and [pi].event_id =  e.event_id
		inner  join (
		select distinct event_id 
						from qspCanadaOrderManagement.dbo.CustomerOrderDetail cod  with(nolock)
						INNER JOIN qspCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory hist with(nolock) on hist.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance and hist.transid = cod.transid 
						INNER JOIN qspCanadaOrderManagement.dbo.InternetOrderID ioid with(nolock) ON cod.CustomerOrderHeaderInstance = ioid.CustomerOrderHeaderInstance
						INNER JOIN qspEcommerce.dbo.cart c with(nolock) on c.eds_order_id = ioid.internetorderid
						INNER JOIN qspFulfillment.dbo.[Order] o with(nolock) on o.order_id = c.x_order_id
						INNER JOIN qspecommerce.dbo.efundraisingtransaction et with(nolock) on et.orderid = o.order_id
						INNER JOIN  event_participation ep with(nolock) on ep.event_participation_id = et.suppid
						where et.CreateDate > '11-09-2009' 
						and cod.delflag = 0 
						and hist.status in (42001,42000, 42010)
		group by event_id
		having count (et.orderid) > 1
		)tps on tps.event_id = e.event_id
		left outer join 
		(
		select  t.event_participation_id 	
		, max(t.create_date) as create_date 
		from
		touch t with(nolock)
		inner join touch_info ti with(nolock)
		on ti.touch_info_id = t.touch_info_id
		and business_rule_id = 155
		group by t.event_participation_id 
		) t
		on t.event_participation_id = ep.event_participation_id
		
		
		where
		(t.event_participation_id  is null)
		and [pi].postal_address_id is null
		and e.culture_code = 'en-CA'
		and g.partner_id <> 143
		and e.create_date > '11-09-2009'
		and e.active = 1
	

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
