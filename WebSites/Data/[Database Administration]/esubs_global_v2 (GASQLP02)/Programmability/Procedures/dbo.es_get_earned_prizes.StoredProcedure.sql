USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_earned_prizes]    Script Date: 02/14/2014 13:05:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Earned_prize
CREATE PROCEDURE [dbo].[es_get_earned_prizes] AS
begin

select Prize_item_id, Event_participation_id, Create_date from Earned_prize

end
GO
