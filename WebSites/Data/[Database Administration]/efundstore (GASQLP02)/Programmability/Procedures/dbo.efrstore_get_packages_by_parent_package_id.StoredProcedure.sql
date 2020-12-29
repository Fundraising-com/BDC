USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_packages_by_parent_package_id]    Script Date: 02/14/2014 13:05:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[efrstore_get_packages_by_parent_package_id]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
   drop procedure [dbo].[efrstore_get_packages_by_parent_package_id];
GO
-- Generate get by id stored proc for Package
CREATE   PROCEDURE [dbo].[efrstore_get_packages_by_parent_package_id] @parent_package_id int

AS
begin

select p.Package_id, p.Parent_package_id,p. [name], p.Profit_percentage,p.Enabled, p.Create_date 
from Package p  inner join Package_desc pd on p.Package_id = pd.Package_id
where parent_package_id = @parent_package_id AND p.Enabled =1
order by pd.display_order
end
GO
