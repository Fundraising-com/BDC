USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sale_carrier_shipping_statuss]    Script Date: 02/14/2014 13:05:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Sale_carrier_shipping_status
CREATE PROCEDURE [dbo].[efrcrm_get_sale_carrier_shipping_statuss] AS
begin

select Carrier_shipping_status_id, Sales_id, Status_entry_date from Sale_carrier_shipping_status

end
GO
