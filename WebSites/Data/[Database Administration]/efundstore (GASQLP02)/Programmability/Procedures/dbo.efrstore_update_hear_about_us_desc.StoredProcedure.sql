USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_hear_about_us_desc]    Script Date: 02/14/2014 13:06:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Hear_about_us_desc
CREATE PROCEDURE [dbo].[efrstore_update_hear_about_us_desc] @Hear_about_us_id tinyint, @Culture_code nvarchar(10), @Description varchar(100) AS
begin

update Hear_about_us_desc set Culture_code=@Culture_code, Description=@Description where Hear_about_us_id=@Hear_about_us_id

end
GO
