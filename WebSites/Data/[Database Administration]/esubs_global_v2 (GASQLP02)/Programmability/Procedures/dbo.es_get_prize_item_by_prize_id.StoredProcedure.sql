USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_prize_item_by_prize_id]    Script Date: 02/14/2014 13:06:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Prize_item
CREATE PROCEDURE [dbo].[es_get_prize_item_by_prize_id] @Prize_id int AS
begin

select Prize_item_id, Prize_id, Prize_item_code, Expiration_date, Create_date from Prize_item where Prize_id=@Prize_id

end
GO
