USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_ins_Address]    Script Date: 06/07/2017 09:33:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ins_Address]
	@street1 varchar(50),
	@street2 varchar(50),
	@city varchar(50),
	@stateProvince varchar(10),
	@postal_code varchar(7),
	@zip4 varchar(4),
	@country varchar(10),
	@address_type int,
	@AddressListID int,
	@Address_ID int output
AS
INSERT INTO Address (
	street1,
	street2,
	city,
	stateProvince,
	postal_code,
	zip4,
	country,
	address_type,
	AddressListID
)VALUES(
	@street1,
	@street2,
	@city,
	@stateProvince,
	@postal_code,
	@zip4,
	@country,
	@address_type,
	@AddressListID)


SELECT @Address_ID = @@Identity
GO
