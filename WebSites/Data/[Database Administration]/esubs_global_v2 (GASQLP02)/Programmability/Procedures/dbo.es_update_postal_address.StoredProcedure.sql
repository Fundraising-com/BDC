USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_postal_address]    Script Date: 02/14/2014 13:08:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Payment
create  PROCEDURE [dbo].[es_update_postal_address] @Address_1 nvarchar(200), @Address_2 nvarchar(200), @City varchar(50), @Zip_code nvarchar(10), @Country_code nvarchar(10), @Subdivision_code nvarchar(10), @Create_date datetime AS
begin

update Postal_address
set address_1 = @Address_1,
    address_2 = @Address_2,
    city = @City,
    zip_code = @Zip_code,
    subdivision_code = @Subdivision_code,
    country_code = @Country_code,
    create_date = @Create_date

end
GO
