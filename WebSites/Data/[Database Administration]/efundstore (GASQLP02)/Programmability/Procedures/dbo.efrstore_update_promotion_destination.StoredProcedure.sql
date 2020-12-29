USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_promotion_destination]    Script Date: 02/14/2014 13:06:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Promotion_destination
CREATE PROCEDURE [dbo].[efrstore_update_promotion_destination] @Promotion_destination_id int, @Url varchar(255), @Create_date datetime AS
begin

update Promotion_destination set Url=@Url, Create_date=@Create_date where Promotion_destination_id=@Promotion_destination_id

end
GO
