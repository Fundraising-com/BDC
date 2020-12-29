USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_product_quantity_by_id]    Script Date: 02/14/2014 13:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Product_Quantity
CREATE PROCEDURE [dbo].[efrcrm_get_product_quantity_by_id] @Product_Quantity_ID int AS
begin

select Product_Quantity_ID, Scratch_Book_ID, Quantity, Comments from Product_Quantity where Product_Quantity_ID=@Product_Quantity_ID

end
GO
