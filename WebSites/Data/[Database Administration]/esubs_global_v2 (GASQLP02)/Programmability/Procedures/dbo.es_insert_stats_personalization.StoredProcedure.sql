USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_stats_personalization]    Script Date: 02/14/2014 13:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Stats_personalization
CREATE PROCEDURE [dbo].[es_insert_stats_personalization] @Stats_personalization_id int OUTPUT, @Event_participation_id int, @Stats_personalization_item_id int, 
@Stats_personalization_section_id int, @Create_date datetime AS
begin

insert into Stats_personalization(Event_participation_id, Stats_personalization_item_id, Stats_personalization_section_id, Create_date) 
values(@Event_participation_id, @Stats_personalization_item_id, @Stats_personalization_section_id, @Create_date)

select @Stats_personalization_id = SCOPE_IDENTITY()

end
GO
