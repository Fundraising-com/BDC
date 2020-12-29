USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_partner_promotion]    Script Date: 02/14/2014 13:06:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Partner_promotion
CREATE PROCEDURE [dbo].[efrstore_update_partner_promotion] @Partner_promotion_id int, @Partner_id int, @Promotion_id int, @Create_date datetime AS
begin

update Partner_promotion set Partner_id=@Partner_id, Promotion_id=@Promotion_id, Create_date=@Create_date where Partner_promotion_id=@Partner_promotion_id

end
GO
