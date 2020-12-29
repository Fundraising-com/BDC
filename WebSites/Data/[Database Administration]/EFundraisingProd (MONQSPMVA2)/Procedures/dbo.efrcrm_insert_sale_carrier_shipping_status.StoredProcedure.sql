USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_sale_carrier_shipping_status]    Script Date: 02/14/2014 13:07:20 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- Generate insert stored proc for Production_Status
CREATE PROCEDURE [dbo].[efrcrm_insert_sale_carrier_shipping_status] @carrier_shipping_status_id tinyint OUTPUT, @sales_id int, @status_entry_date datetime AS
begin

insert into sale_carrier_shipping_status(carrier_shipping_status_id, sales_id, status_entry_date) values(@carrier_shipping_status_id, @sales_id, @status_entry_date)

end
GO
