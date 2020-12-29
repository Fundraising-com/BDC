USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_stats_personalization_by_id]    Script Date: 02/14/2014 13:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Stats_personalization
CREATE PROCEDURE [dbo].[es_get_stats_personalization_by_id] @Stats_personalization_id int AS
begin

select Stats_personalization_id, Event_participation_id, Stats_personalization_item_id, Stats_personalization_section_id, Create_date from Stats_personalization where Stats_personalization_id=@Stats_personalization_id

end
GO
