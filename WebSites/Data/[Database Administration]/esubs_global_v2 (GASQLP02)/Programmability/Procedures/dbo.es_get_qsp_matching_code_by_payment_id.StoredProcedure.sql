USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_qsp_matching_code_by_payment_id]    Script Date: 02/14/2014 13:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Qsp_matching_code
CREATE PROCEDURE [dbo].[es_get_qsp_matching_code_by_payment_id] @payment_id int AS
begin

select pa.matching_code
   --pinfo.group_id, pinfo.event_id, pinfo.postal_address_id, pinfo.payment_name
from 
   payment_info pinfo
   INNER JOIN postal_address pa on pa.postal_address_id = pinfo.postal_address_id
   INNER JOIN qsp_matching_code qmc 
			on 
			(
				(qmc.cust_billing_matching_code is not null AND qmc.cust_billing_matching_code = pa.matching_code )
				OR 
				(qmc.cust_shipping_matching_code IS NOT NULL AND qmc.cust_shipping_matching_code = pa.matching_code)
				OR 
				(qmc.account_matching_code IS NOT NULL AND qmc.account_matching_code = pa.matching_code)
			)
where 
pa.matching_code is not null 
and pa.matching_code != 'invalid'
and pinfo.payment_info_id = @payment_id AND pinfo.active = 1
end
GO
