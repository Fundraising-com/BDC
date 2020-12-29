USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_packages_by_name]    Script Date: 02/14/2014 13:05:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Package
CREATE   PROCEDURE [dbo].[efrstore_get_packages_by_name]
                  @Name varchar(100)
AS
begin

select Package_id, Parent_package_id, Name, Profit_percentage, Enabled, Create_date
from Package
where [name] = @name


end
GO
