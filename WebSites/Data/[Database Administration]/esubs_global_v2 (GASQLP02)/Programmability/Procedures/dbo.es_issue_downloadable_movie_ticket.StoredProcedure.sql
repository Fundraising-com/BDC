USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_issue_downloadable_movie_ticket]    Script Date: 02/14/2014 13:06:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_issue_downloadable_movie_ticket]
   @event_participation_id int
AS
BEGIN

    begin transaction

    declare @code varchar (30)
    declare @prize_item_id int
    declare @retour varchar(100)

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
    and  	expiration_date > getdate() + 31


    if @code is null
    begin
	    set @retour = 'No movie ticket is available'
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
		    set @retour = 'An error occured inserting the prize'
	    end
	    else
               set @retour = @code
	       commit transaction
            
    end
    	
    select @retour
END
GO
