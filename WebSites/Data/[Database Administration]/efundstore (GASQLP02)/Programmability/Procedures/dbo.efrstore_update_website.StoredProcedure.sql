USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_website]    Script Date: 02/14/2014 13:06:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Website
CREATE PROCEDURE [dbo].[efrstore_update_website] @Website_id smallint, @Partner_id int, @Webproject_id tinyint, @Website_dns varchar(50) AS
begin

update Website set Partner_id=@Partner_id, Webproject_id=@Webproject_id, Website_dns=@Website_dns where Website_id=@Website_id

end
GO
