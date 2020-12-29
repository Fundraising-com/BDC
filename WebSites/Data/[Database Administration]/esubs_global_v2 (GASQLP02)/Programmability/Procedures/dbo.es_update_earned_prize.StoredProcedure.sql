USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_earned_prize]    Script Date: 02/14/2014 13:07:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Earned_prize
CREATE PROCEDURE [dbo].[es_update_earned_prize] @Prize_item_id int, @Event_participation_id int, @Create_date datetime AS
begin

update Earned_prize set Event_participation_id=@Event_participation_id, Create_date=@Create_date where Prize_item_id=@Prize_item_id

end
GO
