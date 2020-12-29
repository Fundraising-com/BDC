USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_prizes_by_prize_type_id]    Script Date: 02/14/2014 13:06:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Prize_item
CREATE PROCEDURE [dbo].[es_get_prizes_by_prize_type_id] @Prize_type_id int AS
begin

select Prize_id, Prize_type_id, Prize_name, Create_date from Prize where Prize_type_id=@Prize_type_id

end
GO
