USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_products]    Script Date: 02/14/2014 13:05:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Product
CREATE  PROCEDURE [dbo].[efrstore_get_products] AS
begin

select Product_id, Parent_product_id, Scratch_book_id, [name], Raising_potential, Product_code, Enabled, Is_inner, Create_date from Product
order by name

end
GO
