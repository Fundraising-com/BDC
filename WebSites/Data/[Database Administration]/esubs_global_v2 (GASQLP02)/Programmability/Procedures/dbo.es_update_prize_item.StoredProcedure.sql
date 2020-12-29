USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_prize_item]    Script Date: 02/14/2014 13:08:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Prize_item
CREATE PROCEDURE [dbo].[es_update_prize_item] @Prize_item_id int, @Prize_id int, @Prize_item_code varchar(40), @Expiration_date datetime, @Create_date datetime AS
begin

update Prize_item set Prize_id=@Prize_id, Prize_item_code=@Prize_item_code, Expiration_date=@Expiration_date, Create_date=@Create_date where Prize_item_id=@Prize_item_id

end
GO
