USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_accounting_class_shipping_feess]    Script Date: 02/14/2014 13:03:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Accounting_class_shipping_fees
CREATE PROCEDURE [dbo].[efrcrm_get_accounting_class_shipping_feess] AS
begin

select Accounting_class_id, Min_amount, Max_amount, Shipping_fee from Accounting_class_shipping_fees

end
GO
