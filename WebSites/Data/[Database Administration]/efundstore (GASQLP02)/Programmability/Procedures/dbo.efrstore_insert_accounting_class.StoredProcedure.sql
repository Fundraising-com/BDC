USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_accounting_class]    Script Date: 02/14/2014 13:05:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Accounting_class
CREATE PROCEDURE [dbo].[efrstore_insert_accounting_class] @Accounting_class_id smallint OUTPUT, @Carrier_id tinyint, @Shipping_option_id tinyint, @Description varchar(50), @Rank int, @Delivery_days tinyint, @Shipping_fees tinyint, @Free_shipping_amount int AS
begin

insert into Accounting_class(Carrier_id, Shipping_option_id, Description, Rank, Delivery_days, Shipping_fees, Free_shipping_amount) values(@Carrier_id, @Shipping_option_id, @Description, @Rank, @Delivery_days, @Shipping_fees, @Free_shipping_amount)

select @Accounting_class_id = SCOPE_IDENTITY()

end
GO
