USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_accounting_class_shipping_fee]    Script Date: 02/14/2014 13:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Accounting_class_shipping_fee
CREATE PROCEDURE [dbo].[efrstore_update_accounting_class_shipping_fee] @Accounting_class_id tinyint, @Min_amount numeric, @Max_amount numeric, @Shipping_fee tinyint AS
begin

update Accounting_class_shipping_fee set Min_amount=@Min_amount, Max_amount=@Max_amount, Shipping_fee=@Shipping_fee where Accounting_class_id=@Accounting_class_id

end
GO
