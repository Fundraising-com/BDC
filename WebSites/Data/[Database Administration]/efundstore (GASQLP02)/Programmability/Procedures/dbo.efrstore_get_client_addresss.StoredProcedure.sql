USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_client_addresss]    Script Date: 02/14/2014 13:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Client_address
CREATE PROCEDURE [dbo].[efrstore_get_client_addresss] AS
begin

select Address_id, Client_sequence_code, Client_id, Address_type, Street_address, State_code, City, Zip_code, Country_code, Attention_of from Client_address

end
GO
