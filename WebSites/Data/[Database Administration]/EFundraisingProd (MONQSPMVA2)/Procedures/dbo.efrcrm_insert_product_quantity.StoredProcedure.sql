USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_product_quantity]    Script Date: 02/14/2014 13:07:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Product_Quantity
CREATE PROCEDURE [dbo].[efrcrm_insert_product_quantity] @Product_Quantity_ID int OUTPUT, @Scratch_Book_ID int, @Quantity int, @Comments text AS
begin

insert into Product_Quantity(Scratch_Book_ID, Quantity, Comments) values(@Scratch_Book_ID, @Quantity, @Comments)

select @Product_Quantity_ID = SCOPE_IDENTITY()

end
GO
