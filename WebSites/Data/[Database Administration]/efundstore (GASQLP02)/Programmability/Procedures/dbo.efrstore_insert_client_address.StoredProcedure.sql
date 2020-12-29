USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_client_address]    Script Date: 02/14/2014 13:05:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Client_address
CREATE PROCEDURE [dbo].[efrstore_insert_client_address] @Address_id int OUTPUT, @Client_sequence_code char(2), @Client_id int, @Address_type char(2), @Street_address varchar(100), @State_code varchar(10), @City varchar(50), @Zip_code varchar(10), @Country_code varchar(10), @Attention_of varchar(100) AS
begin

insert into Client_address(Client_sequence_code, Client_id, Address_type, Street_address, State_code, City, Zip_code, Country_code, Attention_of) values(@Client_sequence_code, @Client_id, @Address_type, @Street_address, @State_code, @City, @Zip_code, @Country_code, @Attention_of)

select @Address_id = SCOPE_IDENTITY()

end
GO
