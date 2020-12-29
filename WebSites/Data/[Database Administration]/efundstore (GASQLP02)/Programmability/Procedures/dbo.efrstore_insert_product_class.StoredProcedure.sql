USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_product_class]    Script Date: 02/14/2014 13:05:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Product_class
CREATE PROCEDURE [dbo].[efrstore_insert_product_class] @Product_class_id int OUTPUT, @Division_id int, @Accounting_class_id tinyint, @Description varchar(50), @Display bit, @Minimum_order_qty tinyint AS
begin

insert into Product_class(Division_id, Accounting_class_id, Description, Display, Minimum_order_qty) values(@Division_id, @Accounting_class_id, @Description, @Display, @Minimum_order_qty)

select @Product_class_id = SCOPE_IDENTITY()

end
GO
