USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_product]    Script Date: 02/14/2014 13:05:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Product
CREATE PROCEDURE [dbo].[efrstore_insert_product] @Product_id int OUTPUT, @Parent_product_id int, @Scratch_book_id int, @Name varchar(100), @Raising_potential decimal(15, 4), @Product_code varchar(20), @Enabled bit, @Is_inner bit, @Create_date datetime AS
begin

insert into Product(Parent_product_id, Scratch_book_id, Name, Raising_potential, Product_code, Enabled, Is_inner, Create_date) values(@Parent_product_id, @Scratch_book_id, @Name, @Raising_potential, @Product_code, @Enabled, @Is_inner, @Create_date)

select @Product_id = SCOPE_IDENTITY()

end
GO
