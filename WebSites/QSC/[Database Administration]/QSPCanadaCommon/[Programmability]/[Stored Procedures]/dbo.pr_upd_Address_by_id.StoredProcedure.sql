USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_upd_Address_by_id]    Script Date: 06/07/2017 09:33:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_upd_Address_by_id]
	@address_id int,
	@street1 varchar(50),
	@street2 varchar(50),
	@city varchar(50),
	@stateProvince varchar(10),
	@postal_code varchar(7),
	@zip4 varchar(4),
	@country varchar(10),
	@address_type int,
	@AddressListID int
AS
UPDATE 
	Address 
SET 
	street1 = @street1,
	street2 = @street2,
	city = @city,
	stateProvince = @stateProvince,
	postal_code = @postal_code,
	zip4 = @zip4,
	country = @country,
	address_type = @address_type,
	AddressListID = @AddressListID
WHERE 
	address_id = @address_id
GO
