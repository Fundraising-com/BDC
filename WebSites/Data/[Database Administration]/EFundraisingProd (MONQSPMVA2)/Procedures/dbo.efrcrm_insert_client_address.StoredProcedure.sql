USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_client_address]    Script Date: 02/14/2014 13:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Client_address
CREATE    PROCEDURE [dbo].[efrcrm_insert_client_address] 
    @Address_id int OUTPUT
    , @Client_sequence_code char(2)
    , @Client_id int
    , @Address_type char(2)
    , @Street_address varchar(100)
    , @State_code varchar(10)
    , @Country_code varchar(10)
    , @City varchar(50)
    , @Zip_code varchar(10)
    , @Attention_of varchar(100)
    , @Matching_code varchar(50)
    , @address_zone_id int = 3
    , @phone_1 varchar(127) = NULL
    , @phone_2 varchar(127) = NULL
    , @location varchar(100) = null
    , @pick_up bit = 0
    , @warehouse_id int = null

AS
begin

declare @id int
exec @id = sp_NewID  'Address_ID', 'All'
set @Address_id = @id



insert into Client_address (
    Address_id
    , Client_sequence_code
    , Client_id
    , Address_type
    , Street_address
    , State_code
    , Country_code
    , City
    , Zip_code
    , Attention_of
    , Matching_code
    , address_zone_id
    , phone_1
    , phone_2
    , location
    , pick_up
    , warehouse_id
) values(
    @id
    , @Client_sequence_code
    , @Client_id
    , @Address_type
    , @Street_address
    , @State_code
    , @Country_code
    , @City
    , @Zip_code
    , @Attention_of
    , @Matching_code
    , @address_zone_id
    , @phone_1
    , @phone_2
    , @location
    , @pick_up
    , @warehouse_id
)

end
GO
