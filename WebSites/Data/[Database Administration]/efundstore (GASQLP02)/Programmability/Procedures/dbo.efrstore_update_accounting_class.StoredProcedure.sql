USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_accounting_class]    Script Date: 02/14/2014 13:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Accounting_class
CREATE PROCEDURE [dbo].[efrstore_update_accounting_class] @Accounting_class_id tinyint, @Carrier_id tinyint, @Shipping_option_id tinyint, @Description varchar(50), @Rank int, @Delivery_days tinyint, @Shipping_fees tinyint, @Free_shipping_amount int AS
begin

update Accounting_class set Carrier_id=@Carrier_id, Shipping_option_id=@Shipping_option_id, Description=@Description, Rank=@Rank, Delivery_days=@Delivery_days, Shipping_fees=@Shipping_fees, Free_shipping_amount=@Free_shipping_amount where Accounting_class_id=@Accounting_class_id

end
GO
