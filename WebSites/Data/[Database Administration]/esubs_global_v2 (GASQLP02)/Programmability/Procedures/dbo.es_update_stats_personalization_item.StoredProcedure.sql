USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_stats_personalization_item]    Script Date: 02/14/2014 13:08:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Stats_personalization_item
CREATE PROCEDURE [dbo].[es_update_stats_personalization_item] @Stats_personalization_item_id int, @Description varchar(100), @Create_date datetime AS
begin

update Stats_personalization_item set Description=@Description, Create_date=@Create_date where Stats_personalization_item_id=@Stats_personalization_item_id

end
GO
