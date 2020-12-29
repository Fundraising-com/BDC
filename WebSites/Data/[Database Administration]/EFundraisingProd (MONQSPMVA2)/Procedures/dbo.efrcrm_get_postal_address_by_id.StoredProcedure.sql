USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_postal_address_by_id]    Script Date: 02/14/2014 13:05:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Postal_address
CREATE PROCEDURE [dbo].[efrcrm_get_postal_address_by_id] @Postal_address_id int AS
begin

select Postal_address_id, Address, City, Zip_code, Country_code, Subdivision_code, Create_date from Postal_address where Postal_address_id=@Postal_address_id

end
GO
