USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_postal_address]    Script Date: 02/14/2014 13:07:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Postal_address
CREATE PROCEDURE [dbo].[efrcrm_insert_postal_address] @Postal_address_id int OUTPUT, @Address varchar(100),  @City varchar(100), @Zip_code varchar(10), @Country_code nvarchar(4), @Subdivision_code nvarchar(14), @Create_date datetime AS
begin

insert into Postal_address(Address, City, Zip_code, Country_code, Subdivision_code, Create_date) values(@Address,  @City, @Zip_code, @Country_code, @Subdivision_code, @Create_date)

select @Postal_address_id = SCOPE_IDENTITY()

end
GO
