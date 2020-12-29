USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_promotion]    Script Date: 02/14/2014 13:06:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Promotion
CREATE PROCEDURE [dbo].[efrstore_update_promotion] @Promotion_id int, @Promotion_type_code char(3), @Promotion_destination_id int, @Name varchar(255), @Script_name varchar(255), @Active bit, @Create_date datetime AS
begin

update Promotion set Promotion_type_code=@Promotion_type_code, Promotion_destination_id=@Promotion_destination_id, Name=@Name, Script_name=@Script_name, Active=@Active, Create_date=@Create_date where Promotion_id=@Promotion_id

end
GO
