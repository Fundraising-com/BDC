USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_hear_about_us_desc]    Script Date: 02/14/2014 13:05:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Hear_about_us_desc
CREATE PROCEDURE [dbo].[efrstore_insert_hear_about_us_desc] @Hear_about_us_id int OUTPUT, @Culture_code nvarchar(10), @Description varchar(100) AS
begin

insert into Hear_about_us_desc(Culture_code, Description) values(@Culture_code, @Description)

select @Hear_about_us_id = SCOPE_IDENTITY()

end
GO
