USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_end_event]    Script Date: 02/14/2014 13:04:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    procedure [dbo].[cc_end_event]
             @event_id int, 
             @end_event bit

as

if @end_event = 1
begin
      --DESACTIVE
      UPDATE  event 
      SET     end_date = getdate(), active = 0
      where   event_id = @event_id
   

end
else
begin
    
      --REACTIVATE
      UPDATE  event 
      SET     active = 1, end_date = null
      where   event_id = @event_id
        
end


if @@error = 0
	return  0
else
	return -1 --une erreur
GO
