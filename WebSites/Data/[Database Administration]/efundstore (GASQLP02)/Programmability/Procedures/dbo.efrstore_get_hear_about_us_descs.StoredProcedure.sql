USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_hear_about_us_descs]    Script Date: 02/14/2014 13:05:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Hear_about_us_desc
CREATE PROCEDURE [dbo].[efrstore_get_hear_about_us_descs] AS
begin

select Hear_about_us_id, Culture_code, Description from Hear_about_us_desc

end
GO
