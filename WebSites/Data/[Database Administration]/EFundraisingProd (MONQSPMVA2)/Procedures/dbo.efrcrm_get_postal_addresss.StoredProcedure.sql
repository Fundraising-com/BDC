USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_postal_addresss]    Script Date: 02/14/2014 13:05:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Postal_address
CREATE PROCEDURE [dbo].[efrcrm_get_postal_addresss] AS
begin

select Postal_address_id, Address, City, Zip_code, Country_code, Subdivision_code, Create_date from Postal_address

end
GO
