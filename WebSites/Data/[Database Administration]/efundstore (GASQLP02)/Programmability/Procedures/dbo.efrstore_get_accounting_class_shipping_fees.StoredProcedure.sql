USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_accounting_class_shipping_fees]    Script Date: 02/14/2014 13:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Accounting_class_shipping_fee
CREATE PROCEDURE [dbo].[efrstore_get_accounting_class_shipping_fees] AS
begin

select Accounting_class_id, Min_amount, Max_amount, Shipping_fee from Accounting_class_shipping_fee

end
GO
