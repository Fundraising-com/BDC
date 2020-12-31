USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Address_SelectAllByAddressListID]    Script Date: 06/07/2017 09:33:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select one or more existing rows from the table 'Address'
-- based on a foreign key field.
-- Gets: @iAddressListID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Address_SelectAllByAddressListID]
	@iAddressListID int

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
	[AddressListID] = @iAddressListID
ORDER BY
	[address_type]
GO
