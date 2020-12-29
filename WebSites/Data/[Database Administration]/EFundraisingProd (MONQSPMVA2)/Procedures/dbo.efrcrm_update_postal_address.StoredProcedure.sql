USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_postal_address]    Script Date: 02/14/2014 13:08:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Postal_address
CREATE PROCEDURE [dbo].[efrcrm_update_postal_address] @Postal_address_id int, @Address varchar(100),  @City varchar(100), @Zip_code varchar(10), @Country_code nvarchar(4), @Subdivision_code nvarchar(14), @Create_date datetime AS
begin

update Postal_address set Address=@Address, City=@City, Zip_code=@Zip_code, Country_code=@Country_code, Subdivision_code=@Subdivision_code, Create_date=@Create_date where Postal_address_id=@Postal_address_id

end
GO
