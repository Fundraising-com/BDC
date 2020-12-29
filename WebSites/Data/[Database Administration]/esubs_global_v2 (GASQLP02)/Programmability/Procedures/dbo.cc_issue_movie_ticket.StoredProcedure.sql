USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_issue_movie_ticket]    Script Date: 03/02/2015 23:25:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER  procedure [dbo].[cc_issue_movie_ticket] --126407302
   @event_participation_id int
  
as

declare @code varchar (30)
declare @prize_item_id int
declare @retour varchar(100)

begin transaction

select top 1
	 @prize_item_id = [pi].prize_item_id,
         @code = [pi].prize_item_code
from 
	prize_item [pi] 
	left outer join earned_prize ep	
	on [pi].prize_item_id = ep.prize_item_id    
where	
	prize_id = 6
and	ep.create_date is null
and expiration_date > getdate() + 31


if @code is null
begin
	set @retour = '[No movie ticket is available]'
	rollback transaction	
end
else
begin
	insert into earned_prize
	(prize_item_id,event_participation_id,create_date)
	values
	(@prize_item_id,@event_participation_id,getdate())

	if @@error <> 0 
	begin
		rollback transaction
		set @retour = '[An error occured inserting the prize]'
	end
	else
           set @retour = @code
	   commit transaction
        
end
	
select @retour


