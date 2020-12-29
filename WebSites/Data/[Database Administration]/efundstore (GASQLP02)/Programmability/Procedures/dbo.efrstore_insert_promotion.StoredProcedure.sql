USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_promotion]    Script Date: 02/14/2014 13:05:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Promotion
CREATE PROCEDURE [dbo].[efrstore_insert_promotion] @Promotion_id int OUTPUT, @Promotion_type_code char(3), @Promotion_destination_id int, @Name varchar(255), @Script_name varchar(255), @Active bit, @Create_date datetime AS
begin

insert into Promotion(Promotion_type_code, Promotion_destination_id, Name, Script_name, Active, Create_date) values(@Promotion_type_code, @Promotion_destination_id, @Name, @Script_name, @Active, @Create_date)

select @Promotion_id = SCOPE_IDENTITY()

end
GO
