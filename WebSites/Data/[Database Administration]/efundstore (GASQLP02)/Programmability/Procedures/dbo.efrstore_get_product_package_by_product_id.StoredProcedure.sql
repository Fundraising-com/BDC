USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_product_package_by_product_id]    Script Date: 02/14/2014 13:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Product_package
create  PROCEDURE [dbo].[efrstore_get_product_package_by_product_id] @Product_id int AS
begin

select Product_id, Package_id, Display_order, Display, Display_multi_level, Display_inner_product from Product_package where Product_id=@Product_id

end
GO
