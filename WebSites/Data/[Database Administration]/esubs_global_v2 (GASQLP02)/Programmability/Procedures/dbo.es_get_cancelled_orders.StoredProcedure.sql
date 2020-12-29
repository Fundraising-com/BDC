USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_cancelled_orders]    Script Date: 02/14/2014 13:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[es_get_cancelled_orders] 
            @days INT = 1
AS

declare @start_date datetime
declare @end_date datetime
--declare @days int 

--set @days = 7 

set @start_date = getdate() - @days
set @end_date = getdate()
	select ep.event_id
        , sum(od.quantity)       as nb_subs 
   from qspecommerce.dbo.efundraisingtransaction et
		inner join esubs_global_v2.dbo.event_participation ep on ep.event_participation_id = et.suppid
		inner join qspfulfillment.dbo.[order] o on o.order_id = et.orderid
		inner join qspfulfillment.dbo.[order_detail] od on od.order_id = o.order_id
        iNNER JOIN dbo.es_get_valid_order_status() os ON os.order_status_id = o.order_status_id
	
	    inner join qspfulfillment.dbo.[order] o2 on o2.order_id = o.order_id 
			and o2.order_date between @start_date and @end_date
        LEFT JOIN dbo.es_get_valid_order_status() os2 ON os2.order_status_id = o2.order_status_id
	where os2.order_status_id is null    
	group by ep.event_id 
	having  count(o2.order_id) > 0 --or count(o2.order_id) is null
GO
