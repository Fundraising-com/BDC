USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Address_SelectShippingAddressByAddressListID]    Script Date: 06/07/2017 09:33:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select the shipping address of a specific account from the table 'Address'
-- based on a foreign key field.
-- Gets: @iAddressListID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Address_SelectShippingAddressByAddressListID]
	@AddressListID int

AS
SET NOCOUNT ON
-- SELECT one or more existing rows from the table.
SELECT
	[address_id],
	[street1],
	[street2],
	[city],
	[stateProvince],
	[postal_code],
	[zip4],
	[country],
	[address_type],
	[AddressListID]
FROM [dbo].[Address]
WHERE
	[AddressListID] = @AddressListID
	AND [address_type] = 54001 --shipping address
GO
