USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_package_by_product_id]    Script Date: 02/14/2014 13:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Package
CREATE  PROCEDURE [dbo].[efrstore_get_package_by_product_id] 
@Product_id INT
AS
BEGIN

SELECT TOP 1 p.Package_id,p. Parent_package_id, p.[name], p.Profit_percentage,p.Enabled,p.Create_date
FROM Package p INNER JOIN Product_package pp ON p.package_id = pp.package_id INNER JOIN Product pr ON pp.product_id = pr.product_id
WHERE pp.product_id=@Product_id

END
GO
