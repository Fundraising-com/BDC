USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Address_Update]    Script Date: 06/07/2017 09:33:11 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will update an existing row in the table 'Address'
-- Gets: @iaddress_id int
-- Gets: @sstreet1 varchar(50)
-- Gets: @sstreet2 varchar(50)
-- Gets: @scity varchar(50)
-- Gets: @sstateProvince varchar(10)
-- Gets: @spostal_code varchar(7)
-- Gets: @szip4 varchar(4)
-- Gets: @scountry varchar(10)
-- Gets: @iaddress_type int
-- Gets: @iAddressListID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Address_Update]
	@iaddress_id int,
	@sstreet1 varchar(50),
	@sstreet2 varchar(50),
	@scity varchar(50),
	@sstateProvince varchar(10),
	@spostal_code varchar(7),
	@szip4 varchar(4),
	@scountry varchar(10),
	@iaddress_type int,
	@iAddressListID int
AS
SET NOCOUNT ON
-- UPDATE an existing row in the table.
UPDATE [dbo].[Address]
SET 
	[street1] = @sstreet1,
	[street2] = @sstreet2,
	[city] = @scity,
	[stateProvince] = @sstateProvince,
	[postal_code] = @spostal_code,
	[zip4] = @szip4,
	[country] = @scountry,
	[address_type] = @iaddress_type,
	[AddressListID] = @iAddressListID
WHERE
	[address_id] = @iaddress_id
GO
