USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_promotion_by_id]    Script Date: 02/14/2014 13:05:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Promotion
CREATE PROCEDURE [dbo].[efrstore_get_promotion_by_id] @Promotion_id int AS
begin

select Promotion_id, Promotion_type_code, Promotion_destination_id, Name, Script_name, Active, Create_date from Promotion where Promotion_id=@Promotion_id

end
GO
