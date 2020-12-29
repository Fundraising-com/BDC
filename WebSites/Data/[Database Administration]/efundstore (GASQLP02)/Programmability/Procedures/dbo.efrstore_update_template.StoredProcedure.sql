USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_template]    Script Date: 02/14/2014 13:06:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Template
CREATE PROCEDURE [dbo].[efrstore_update_template] @Template_id int, @Name varchar(100), @Path varchar(1000), @Create_date datetime AS
begin

update Template set Name=@Name, Path=@Path, Create_date=@Create_date where Template_id=@Template_id

end
GO
