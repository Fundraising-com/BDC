USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_package]    Script Date: 02/14/2014 13:06:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Package
CREATE PROCEDURE [dbo].[efrstore_update_package] @Package_id int, @Parent_package_id int, @Name varchar(100), @Profit_percentage int, @Enabled bit, @Create_date datetime AS
begin

update Package set Parent_package_id=@Parent_package_id, Name=@Name, Profit_percentage=@Profit_percentage, Enabled=@Enabled, Create_date=@Create_date where Package_id=@Package_id

end
GO
