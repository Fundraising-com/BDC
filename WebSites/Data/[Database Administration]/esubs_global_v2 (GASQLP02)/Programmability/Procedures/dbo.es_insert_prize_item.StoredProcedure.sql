USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_prize_item]    Script Date: 02/14/2014 13:06:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Prize_item
CREATE PROCEDURE [dbo].[es_insert_prize_item] @Prize_item_id int OUTPUT, @Prize_id int, @Prize_item_code varchar(40), @Expiration_date datetime, @Create_date datetime AS
begin

insert into Prize_item(Prize_id, Prize_item_code, Expiration_date, Create_date) values(@Prize_id, @Prize_item_code, @Expiration_date, @Create_date)

select @Prize_item_id = SCOPE_IDENTITY()

end
GO
