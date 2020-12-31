USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_Address_by_type]    Script Date: 06/07/2017 09:33:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_Address_by_type]
	@type int,
	@AddressListId int
AS


SELECT 
	[address_id], 
	[street1], 
	[street2], 
	[city], 
	[stateProvince], 
	[postal_code], 
	[zip4], 
	[country]
FROM 
	Address
WHERE 
	address_type = @type
	AND AddressListID = @AddressListId
GO
