USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_product_package_by_package_id]    Script Date: 02/14/2014 13:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Product_package
CREATE  PROCEDURE [dbo].[efrstore_get_product_package_by_package_id] @Package_id int AS
begin

select Product_id, Package_id, Display_order, Display, Create_date from Product_package where Package_id=@Package_id

end
GO
