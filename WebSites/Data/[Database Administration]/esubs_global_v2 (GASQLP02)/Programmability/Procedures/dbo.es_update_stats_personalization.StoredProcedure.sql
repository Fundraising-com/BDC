USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_stats_personalization]    Script Date: 02/14/2014 13:08:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Stats_personalization
CREATE PROCEDURE [dbo].[es_update_stats_personalization] @Stats_personalization_id int, @Event_participation_id int, @Stats_personalization_item_id int, @Stats_personalization_section_id char(10), @Create_date datetime AS
begin

update Stats_personalization set Event_participation_id=@Event_participation_id, Stats_personalization_item_id=@Stats_personalization_item_id, Stats_personalization_section_id=@Stats_personalization_section_id, Create_date=@Create_date where Stats_personalization_id=@Stats_personalization_id

end
GO
