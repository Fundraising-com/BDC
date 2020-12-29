USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_product_packages]    Script Date: 02/14/2014 13:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Product_package
CREATE PROCEDURE [dbo].[efrstore_get_product_packages] AS
begin

select Product_id, Package_id, Display_order, Display, Create_date from Product_package

end
GO
