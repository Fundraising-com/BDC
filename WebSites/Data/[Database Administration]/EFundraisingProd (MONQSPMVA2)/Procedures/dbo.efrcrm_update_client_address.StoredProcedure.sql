USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_client_address]    Script Date: 02/14/2014 13:07:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Client_address
CREATE   PROCEDURE [dbo].[efrcrm_update_client_address] @Address_id int, @Client_sequence_code char(2), @Client_id int, @Address_type char(2), @Street_address varchar(100), @State_code varchar(10), @Country_code varchar(10), @City varchar(50), @Zip_code varchar(10), @Attention_of varchar(100), @Matching_code varchar(50), @address_zone_id int, @phone_1 varchar(127), @phone_2 varchar(127), @location varchar(100), @pick_up bit, @warehouse_id int AS
begin

update Client_address set Client_sequence_code=@Client_sequence_code, Client_id=@Client_id, Address_type=@Address_type, Street_address=@Street_address, State_code=@State_code, Country_code=@Country_code, City=@City, Zip_code=@Zip_code, Attention_of=@Attention_of, Matching_code=@Matching_code, address_zone_id=@address_zone_id , phone_1=@phone_1 , phone_2=@phone_2, location = @location, pick_up = @pick_up, warehouse_id = @warehouse_id where Address_id=@Address_id

end
GO
