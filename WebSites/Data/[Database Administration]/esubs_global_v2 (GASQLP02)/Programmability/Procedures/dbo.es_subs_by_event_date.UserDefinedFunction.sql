USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_subs_by_event_date]    Script Date: 02/14/2014 13:08:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[es_subs_by_event_date] (
		@event_id int
		, @from_date datetime
		, @end_date datetime
)  
RETURNS int AS  
BEGIN 
	declare @retour as int

	select 
		@retour = isnull(sum(quantity),0)	
	from
	(
	select o.order_id as orderid
	     , od.quantity
	     , od.price
	     , et.suppID
         , o.update_date as updatedate
	from qspecommerce.dbo.efundraisingtransaction et
		inner join qspfulfillment.dbo.[order] o on o.order_id = et.orderid
		inner join qspfulfillment.dbo.[order_detail] od on od.order_id = o.order_id
		inner join event_participation ep on ep.event_participation_id = et.suppid
		INNER JOIN dbo.es_get_valid_order_status() os ON os.order_status_id = o.order_status_id
	where  ep.event_id = @event_id
		and o.order_date between @from_date and @end_date )t
	
	
    
	return @retour

END
GO
