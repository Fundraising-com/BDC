USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_hear_about_us]    Script Date: 02/14/2014 13:06:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Hear_about_us
CREATE PROCEDURE [dbo].[efrstore_update_hear_about_us] @Hear_about_us_id tinyint, @Party_type_id tinyint, @Name varchar(50), @Order_on_web tinyint, @Is_active bit AS
begin

update Hear_about_us set Party_type_id=@Party_type_id, Name=@Name, Order_on_web=@Order_on_web, Is_active=@Is_active where Hear_about_us_id=@Hear_about_us_id

end
GO
