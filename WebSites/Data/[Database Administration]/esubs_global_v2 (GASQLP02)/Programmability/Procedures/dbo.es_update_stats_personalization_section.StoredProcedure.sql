USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_stats_personalization_section]    Script Date: 02/14/2014 13:08:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Stats_personalization_section
CREATE PROCEDURE [dbo].[es_update_stats_personalization_section] @Stat_personalization_section_id int, @Description varchar(50), @Create_date datetime AS
begin

update Stats_personalization_section set Description=@Description, Create_date=@Create_date where Stats_personalization_section_id=@Stat_personalization_section_id

end
GO
