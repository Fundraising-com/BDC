USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_products_by_parent_id]    Script Date: 02/14/2014 13:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Product
CREATE PROCEDURE [dbo].[efrstore_get_products_by_parent_id] 
    @product_id int 
AS
begin

SELECT     dbo.product.product_id, dbo.product.name, dbo.product.create_date, dbo.product.is_inner, dbo.product.enabled, dbo.product.product_code, 
                      dbo.product.raising_potential, dbo.product.scratch_book_id, dbo.product.parent_product_id
FROM         dbo.product whERE     dbo.product.parent_product_id = @Product_id

end
GO
