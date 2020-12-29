USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_product]    Script Date: 02/14/2014 13:06:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Product
CREATE PROCEDURE [dbo].[efrstore_update_product] @Product_id int, @Parent_product_id int, @Scratch_book_id int, @Name varchar(100), @Raising_potential decimal(15, 4), @Product_code varchar(20), @Enabled bit, @Is_inner bit, @Create_date datetime AS
begin

update Product set Parent_product_id=@Parent_product_id, Scratch_book_id=@Scratch_book_id, Name=@Name, Raising_potential=@Raising_potential, Product_code=@Product_code, Enabled=@Enabled, Is_inner=@Is_inner, Create_date=@Create_date where Product_id=@Product_id

end
GO
