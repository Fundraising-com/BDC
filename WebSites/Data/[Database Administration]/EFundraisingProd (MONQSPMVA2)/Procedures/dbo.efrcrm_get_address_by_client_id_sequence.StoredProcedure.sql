USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_address_by_client_id_sequence]    Script Date: 02/14/2014 13:03:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Client_address
CREATE   PROCEDURE [dbo].[efrcrm_get_address_by_client_id_sequence] @client_id int, @client_sequence_code varchar(2)  AS
begin

select Address_id, Client_sequence_code, Client_id, Address_type, Street_address, State_code, Country_code, City, Zip_code, Attention_of, Matching_code,address_zone_id, phone_1, phone_2,location, pick_up, warehouse_id
from Client_address 
where 
Client_id=@client_id
and
Client_sequence_code = @client_sequence_code

end
GO
