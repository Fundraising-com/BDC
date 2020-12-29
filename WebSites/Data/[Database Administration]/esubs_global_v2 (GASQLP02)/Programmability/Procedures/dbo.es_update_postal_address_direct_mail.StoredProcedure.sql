USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_postal_address_direct_mail]    Script Date: 02/14/2014 13:08:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Payment
CREATE PROCEDURE [dbo].[es_update_postal_address_direct_mail] @postal_address_id int, @Address_1 nvarchar(200), @Address_2 nvarchar(200), @zip_code nvarchar(10), @City varchar(50), @Country_code nvarchar(10), @Subdivision_code nvarchar(10), @Create_date datetime AS
begin

update Postal_address
set address_1 = @Address_1
	,address_2 = @Address_2
	, city = @City
	, zip_code = @zip_code
	, country_code = @Country_code
	, subdivision_code = @Subdivision_code
	, create_date = @Create_date
where postal_address_id = @postal_address_id;

end
GO
