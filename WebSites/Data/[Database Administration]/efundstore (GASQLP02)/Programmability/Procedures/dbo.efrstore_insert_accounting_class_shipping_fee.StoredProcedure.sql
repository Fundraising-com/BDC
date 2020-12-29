USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_accounting_class_shipping_fee]    Script Date: 02/14/2014 13:05:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Accounting_class_shipping_fee
CREATE PROCEDURE [dbo].[efrstore_insert_accounting_class_shipping_fee] @Accounting_class_id int OUTPUT, @Min_amount numeric, @Max_amount numeric, @Shipping_fee tinyint AS
begin

insert into Accounting_class_shipping_fee(Min_amount, Max_amount, Shipping_fee) values(@Min_amount, @Max_amount, @Shipping_fee)

select @Accounting_class_id = SCOPE_IDENTITY()

end
GO
