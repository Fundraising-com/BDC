USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_earned_prize]    Script Date: 02/14/2014 13:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Earned_prize
CREATE PROCEDURE [dbo].[es_insert_earned_prize] @Prize_item_id int, @Event_participation_id int, @Create_date datetime AS
begin

insert into Earned_prize(prize_item_id, Event_participation_id, Create_date) 
values(@Prize_item_id,@Event_participation_id, @Create_date)

--select @Prize_item_id = SCOPE_IDENTITY()

end
GO
