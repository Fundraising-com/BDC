USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_stats_personalization_item]    Script Date: 02/14/2014 13:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Stats_personalization_item
CREATE PROCEDURE [dbo].[es_insert_stats_personalization_item] @Stats_personalization_item_id int OUTPUT, @Description varchar(100), @Create_date datetime AS
begin

insert into Stats_personalization_item(Description, Create_date) values(@Description, @Create_date)

select @Stats_personalization_item_id = SCOPE_IDENTITY()

end
GO
