USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_packages_root]    Script Date: 02/14/2014 13:05:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Package
CREATE  PROCEDURE [dbo].[efrstore_get_packages_root] AS
begin

select Package_id, Parent_package_id, [name], Profit_percentage, Enabled, Create_date from Package where parent_package_id is null or parent_package_id = 0

end
GO
