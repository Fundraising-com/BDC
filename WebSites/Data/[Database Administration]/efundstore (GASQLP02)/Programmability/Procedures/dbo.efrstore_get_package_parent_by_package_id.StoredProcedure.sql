USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_package_parent_by_package_id]    Script Date: 02/14/2014 13:05:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Package_desc
create PROCEDURE [dbo].[efrstore_get_package_parent_by_package_id]
                 @package_id INT
AS
BEGIN

select Package_id, Parent_package_id, [name], Profit_percentage, Enabled, Create_date 
FROM Package
WHERE package_id = @package_id
END
GO
