USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_product_class]    Script Date: 02/14/2014 13:06:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Product_class
CREATE PROCEDURE [dbo].[efrstore_update_product_class] @Product_class_id int, @Division_id int, @Accounting_class_id tinyint, @Description varchar(50), @Display bit, @Minimum_order_qty tinyint AS
begin

update Product_class set Division_id=@Division_id, Accounting_class_id=@Accounting_class_id, Description=@Description, Display=@Display, Minimum_order_qty=@Minimum_order_qty where Product_class_id=@Product_class_id

end
GO
