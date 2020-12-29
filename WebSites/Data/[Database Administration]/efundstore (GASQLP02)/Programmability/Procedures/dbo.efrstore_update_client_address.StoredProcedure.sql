USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_client_address]    Script Date: 02/14/2014 13:06:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Client_address
CREATE PROCEDURE [dbo].[efrstore_update_client_address] @Address_id int, @Client_sequence_code char(2), @Client_id int, @Address_type char(2), @Street_address varchar(100), @State_code varchar(10), @City varchar(50), @Zip_code varchar(10), @Country_code varchar(10), @Attention_of varchar(100) AS
begin

update Client_address set Client_sequence_code=@Client_sequence_code, Client_id=@Client_id, Address_type=@Address_type, Street_address=@Street_address, State_code=@State_code, City=@City, Zip_code=@Zip_code, Country_code=@Country_code, Attention_of=@Attention_of where Address_id=@Address_id

end
GO
