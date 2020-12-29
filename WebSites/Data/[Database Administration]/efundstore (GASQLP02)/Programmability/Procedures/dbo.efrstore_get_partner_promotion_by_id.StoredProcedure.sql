USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_partner_promotion_by_id]    Script Date: 02/14/2014 13:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Partner_promotion
CREATE PROCEDURE [dbo].[efrstore_get_partner_promotion_by_id] @Partner_promotion_id int AS
begin

select Partner_promotion_id, Partner_id, Promotion_id, Create_date from Partner_promotion where Partner_promotion_id=@Partner_promotion_id

end
GO
