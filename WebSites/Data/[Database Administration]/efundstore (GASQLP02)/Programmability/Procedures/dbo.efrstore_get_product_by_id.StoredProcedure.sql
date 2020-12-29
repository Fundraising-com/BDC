USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_product_by_id]    Script Date: 02/14/2014 13:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Product
CREATE PROCEDURE [dbo].[efrstore_get_product_by_id] @Product_id int AS
begin

select Product_id, Parent_product_id, Scratch_book_id, [name], Raising_potential, Product_code, Enabled, Is_inner, Create_date from Product where Product_id=@Product_id

end
GO
