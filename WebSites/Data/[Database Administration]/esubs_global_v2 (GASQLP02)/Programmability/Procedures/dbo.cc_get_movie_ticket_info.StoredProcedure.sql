USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_get_movie_ticket_info]    Script Date: 02/14/2014 13:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[cc_get_movie_ticket_info] 
   @event_participation_id int
  
as
--set @identification = 89943
--set @ident_type_id = 2


select top 1
	 [pi].prize_item_code
	, [pi].create_date
	, [pi].expiration_date
from 
	earned_prize ep
	inner join prize_item [pi]
	on [pi].prize_item_id = ep.prize_item_id
    --    inner join event_participation ep
     --   on epr.event_participation_id = ep.event_participation_id 
    
where 
 	ep.event_participation_id = @event_participation_id
	
order by [pi].create_date desc
GO
