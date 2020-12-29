USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_prize_item_next]    Script Date: 02/14/2014 13:06:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Prize_item
CREATE PROCEDURE [dbo].[es_get_prize_item_next] @prize_type int AS
begin

select top 1 pt.Prize_item_id, 
	pt.Prize_id, 
	pt.Prize_item_code, 
	pt.Expiration_date, 
	pt.create_date 
from Prize_item pt
	left outer join earned_prize pe
		on pe.prize_item_id = pt.prize_item_id
where pe.prize_item_id is null
	and pt.expiration_date > getdate() + 31
	and pt.prize_id = @prize_type

/*
 select top 1 @prize_item_id = pt.prize_item_id
     from prize_item pt
      left outer join earned_prize pe
on pe.prize_item_id = pt.prize_item_id
     where
      pt.prize_id = @prize_id
         and pe.prize_item_id is null
         and pt.expiration_date > getdate() +20
*/

end
GO
