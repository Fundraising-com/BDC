USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_promotion_destination_by_id]    Script Date: 02/14/2014 13:05:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Promotion_destination
CREATE PROCEDURE [dbo].[efrstore_get_promotion_destination_by_id] @Promotion_destination_id int AS
begin

select Promotion_destination_id, Url, Create_date from Promotion_destination where Promotion_destination_id=@Promotion_destination_id

end
GO
