USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_hear_about_us_by_id]    Script Date: 02/14/2014 13:05:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Hear_about_us
CREATE PROCEDURE [dbo].[efrstore_get_hear_about_us_by_id] @Hear_about_us_id int AS
begin

select Hear_about_us_id, Party_type_id, Name, Order_on_web, Is_active from Hear_about_us where Hear_about_us_id=@Hear_about_us_id

end
GO
