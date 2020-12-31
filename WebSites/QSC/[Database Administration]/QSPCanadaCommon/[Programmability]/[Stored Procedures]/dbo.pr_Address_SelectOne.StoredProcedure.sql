USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Address_SelectOne]    Script Date: 06/07/2017 09:33:10 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select an existing row from the table 'Address'
-- based on the Primary Key.
-- Gets: @iaddress_id int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Address_SelectOne]
	@iaddress_id int
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
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
	[address_id] = @iaddress_id
GO
