USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_stats_personalizations]    Script Date: 02/14/2014 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Stats_personalization
CREATE PROCEDURE [dbo].[es_get_stats_personalizations] AS
begin

select Stats_personalization_id, Event_participation_id, Stats_personalization_item_id, Stats_personalization_section_id, Create_date from Stats_personalization

end
GO
