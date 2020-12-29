USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_scratch_book]    Script Date: 02/14/2014 13:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Scratch_book
CREATE PROCEDURE [dbo].[efrcrm_update_scratch_book] @Scratch_book_id int, @Product_class_id tinyint, @Supplier_id tinyint, @Package_id int, @Description varchar(50), @Raising_potential decimal, @Product_code varchar(10), @Current_description varchar(50), @Is_active bit, @Is_displayable bit, @Total_qty int, @Replicated bit AS
begin

update Scratch_book set Product_class_id=@Product_class_id, Supplier_id=@Supplier_id, Package_id=@Package_id, Description=@Description, Raising_potential=@Raising_potential, Product_code=@Product_code, Current_description=@Current_description, Is_active=@Is_active, Is_displayable=@Is_displayable, Total_qty=@Total_qty, replicated=@Replicated where Scratch_book_id=@Scratch_book_id

end
GO
