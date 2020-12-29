USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_product_quantity]    Script Date: 02/14/2014 13:08:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Product_Quantity
CREATE PROCEDURE [dbo].[efrcrm_update_product_quantity] @Product_Quantity_ID int, @Scratch_Book_ID int, @Quantity int, @Comments text AS
begin

update Product_Quantity set Scratch_Book_ID=@Scratch_Book_ID, Quantity=@Quantity, Comments=@Comments where Product_Quantity_ID=@Product_Quantity_ID

end
GO
