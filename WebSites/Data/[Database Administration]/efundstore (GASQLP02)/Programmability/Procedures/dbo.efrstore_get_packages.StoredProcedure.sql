USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_packages]    Script Date: 02/14/2014 13:05:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Package
CREATE  PROCEDURE [dbo].[efrstore_get_packages] AS
begin

select Package_id, Parent_package_id, Name, Profit_percentage, Enabled, Create_date from Package
order by name

end
GO
