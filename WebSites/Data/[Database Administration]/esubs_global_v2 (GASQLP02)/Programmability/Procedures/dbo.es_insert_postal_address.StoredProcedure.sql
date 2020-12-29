USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_postal_address]    Script Date: 02/14/2014 13:06:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Payment
CREATE    PROCEDURE [dbo].[es_insert_postal_address] @postal_address_id int OUTPUT, @Address_1 nvarchar(200), @Address_2 nvarchar(200), @zip_code nvarchar(10), @City varchar(50), @Country_code nvarchar(10), @Subdivision_code nvarchar(10), @Create_date datetime AS
begin

insert into Postal_address
(address_1,address_2, city, zip_code, country_code, subdivision_code, create_date)
  values (@Address_1, @Address_2, @City, @zip_code, @Country_code, @Subdivision_code, @Create_date)

select @Postal_address_id = SCOPE_IDENTITY()

end
GO
