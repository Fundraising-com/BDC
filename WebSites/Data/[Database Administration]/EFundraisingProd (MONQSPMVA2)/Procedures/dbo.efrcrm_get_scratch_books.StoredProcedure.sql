USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_scratch_books]    Script Date: 02/14/2014 13:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Scratch_book
CREATE PROCEDURE [dbo].[efrcrm_get_scratch_books] AS
begin

select Scratch_book_id, Product_class_id, Supplier_id, Package_id, Description, Raising_potential, Product_code, Current_description, Is_active, Is_displayable, Total_qty, fixed_profit 
from Scratch_book

end
GO
