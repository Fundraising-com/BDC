USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_package_by_id]    Script Date: 02/14/2014 13:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Package
CREATE  PROCEDURE [dbo].[efrstore_get_package_by_id] @Package_id int AS
begin

select Package_id, Parent_package_id, [name], Profit_percentage, Enabled, Create_date from Package where Package_id=@Package_id

end
GO
