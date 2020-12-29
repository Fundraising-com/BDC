USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_stats_personalization_section_by_id]    Script Date: 02/14/2014 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Stats_personalization_section
CREATE PROCEDURE [dbo].[es_get_stats_personalization_section_by_id] @Stats_personalization_section_id int AS
begin

select Stats_personalization_section_id, Description, Create_date from Stats_personalization_section where Stats_personalization_section_id=@Stats_personalization_section_id

end
GO
