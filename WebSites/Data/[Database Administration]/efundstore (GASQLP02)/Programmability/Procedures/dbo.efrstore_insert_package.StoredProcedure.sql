USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_package]    Script Date: 02/14/2014 13:05:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Package
CREATE PROCEDURE [dbo].[efrstore_insert_package] @Package_id int OUTPUT, @Parent_package_id int, @Name varchar(100), @Profit_percentage tinyint, @Enabled bit, @Create_date datetime AS
begin

insert into Package(Parent_package_id, Name, Profit_percentage, Enabled, Create_date) values(@Parent_package_id, @Name, @Profit_percentage, @Enabled, @Create_date)

select @Package_id = SCOPE_IDENTITY()

end
GO
