USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_scratch_book]    Script Date: 02/14/2014 13:07:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Scratch_book

CREATE  PROCEDURE [dbo].[efrcrm_insert_scratch_book] @Scratch_book_id int OUTPUT, @Product_class_id tinyint, @Supplier_id tinyint, @Package_id int, @Description varchar(50), @Raising_potential decimal, @Product_code varchar(10), @Current_description varchar(50), @Is_active bit, @Is_displayable bit, @Total_qty int AS

declare @id int
exec @id = sp_NewID  'Scratch_Book_ID', 'All'

begin

insert into Scratch_book(Scratch_book_id, Product_class_id, Supplier_id, Package_id, Description, Raising_potential, Product_code, Current_description, Is_active, Is_displayable, Total_qty) values(@id, @Product_class_id, @Supplier_id, @Package_id, @Description, @Raising_potential, @Product_code, @Current_description, @Is_active, @Is_displayable, @Total_qty)

select @Scratch_book_id = @id

end
GO
