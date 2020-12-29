USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_scratch_book_by_product_code]    Script Date: 02/14/2014 13:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Scratch_book
CREATE PROCEDURE [dbo].[efrcrm_get_scratch_book_by_product_code] @product_code varchar(15) AS
begin

select Scratch_book_id, Product_class_id, Supplier_id, Package_id, Description, Raising_potential, Product_code, Current_description, Is_active, Is_displayable, Total_qty from Scratch_book where product_code=@product_code

end
GO
