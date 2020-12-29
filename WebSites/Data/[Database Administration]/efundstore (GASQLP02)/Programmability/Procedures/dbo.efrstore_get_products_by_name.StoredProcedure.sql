USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_products_by_name]    Script Date: 02/14/2014 13:05:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Product
create PROCEDURE [dbo].[efrstore_get_products_by_name]
    @name varchar(100)
AS
begin

SELECT     dbo.product.product_id, dbo.product.name, dbo.product.create_date, dbo.product.is_inner, dbo.product.enabled, dbo.product.product_code, 
                      dbo.product.raising_potential, dbo.product.scratch_book_id, dbo.product.parent_product_id
FROM         dbo.product whERE  [name] = @name

end
GO
