USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_accounting_class_by_id]    Script Date: 02/14/2014 13:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Accounting_class
CREATE PROCEDURE [dbo].[efrstore_get_accounting_class_by_id] @Accounting_class_id int AS
begin

select Accounting_class_id, Carrier_id, Shipping_option_id, Description, Rank, Delivery_days, Shipping_fees, Free_shipping_amount from Accounting_class where Accounting_class_id=@Accounting_class_id

end
GO
