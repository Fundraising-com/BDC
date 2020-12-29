USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_hear_about_us]    Script Date: 02/14/2014 13:05:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Hear_about_us
CREATE PROCEDURE [dbo].[efrstore_insert_hear_about_us] @Hear_about_us_id int OUTPUT, @Party_type_id tinyint, @Name varchar(50), @Order_on_web tinyint, @Is_active bit AS
begin

insert into Hear_about_us(Party_type_id, Name, Order_on_web, Is_active) values(@Party_type_id, @Name, @Order_on_web, @Is_active)

select @Hear_about_us_id = SCOPE_IDENTITY()

end
GO
