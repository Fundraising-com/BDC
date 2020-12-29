USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_client_address_by_id]    Script Date: 02/14/2014 13:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Client_address
CREATE PROCEDURE [dbo].[efrstore_get_client_address_by_id] @Address_id int AS
begin

select Address_id, Client_sequence_code, Client_id, Address_type, Street_address, State_code, City, Zip_code, Country_code, Attention_of from Client_address where Address_id=@Address_id

end
GO
