USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_product_parent_by_product_id]    Script Date: 02/14/2014 13:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Package_desc
create  PROCEDURE [dbo].[efrstore_get_product_parent_by_product_id]
                 @product_id INT
AS
BEGIN

select Product_id, Parent_product_id, Scratch_book_id, [name], Raising_potential, Product_code, Enabled, Is_inner, Create_date 
FROM Product
WHERE product_id = @product_id
END
GO
