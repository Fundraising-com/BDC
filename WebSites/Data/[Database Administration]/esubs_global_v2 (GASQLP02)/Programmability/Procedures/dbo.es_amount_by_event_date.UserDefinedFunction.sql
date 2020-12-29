USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_amount_by_event_date]    Script Date: 02/14/2014 13:08:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[es_amount_by_event_date] (
		@event_id int
		, @from_date datetime
		, @end_date datetime
)  
RETURNS int AS  
BEGIN 
	declare @retour as int


	
	select 
		@retour = isnull(sum(price),0)	
	from
	(
	select order_id as orderid
	     , quantity
	     , total_amount / quantity as price
	     , supp_ID
         , create_date as updatedate
	from [es_get_valid_orders_items_by_event_id] (@event_id)
	where create_date between @from_date and @end_date 
	) t
    
	return @retour

END
GO
