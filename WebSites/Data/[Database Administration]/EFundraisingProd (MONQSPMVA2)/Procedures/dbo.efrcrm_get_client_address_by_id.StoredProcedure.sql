USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_client_address_by_id]    Script Date: 02/14/2014 13:04:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Client_address
CREATE   PROCEDURE [dbo].[efrcrm_get_client_address_by_id] @Address_id int AS
begin

select Address_id, Client_sequence_code, Client_id, Address_type, Street_address, State_code, Country_code, City, Zip_code, Attention_of, Matching_code,  
	address_zone_id, phone_1, phone_2, location,pick_up,warehouse_id
from Client_address where Address_id=@Address_id

end
GO
