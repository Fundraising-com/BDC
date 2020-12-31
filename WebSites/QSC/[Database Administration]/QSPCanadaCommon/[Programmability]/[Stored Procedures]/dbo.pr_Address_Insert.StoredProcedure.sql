USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Address_Insert]    Script Date: 06/07/2017 09:33:10 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will insert 1 row in the table 'Address'
-- Gets: @sstreet1 varchar(50)
-- Gets: @sstreet2 varchar(50)
-- Gets: @scity varchar(50)
-- Gets: @sstateProvince varchar(10)
-- Gets: @spostal_code varchar(7)
-- Gets: @szip4 varchar(4)
-- Gets: @scountry varchar(10)
-- Gets: @iaddress_type int
-- Gets: @iAddressListID int
-- Returns: @iaddress_id int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Address_Insert]
	@sstreet1 varchar(50),
	@sstreet2 varchar(50),
	@scity varchar(50),
	@sstateProvince varchar(10),
	@spostal_code varchar(7),
	@szip4 varchar(4),
	@scountry varchar(10),
	@iaddress_type int,
	@iAddressListID int,
	@iaddress_id int OUTPUT
AS
-- INSERT a new row in the table.
INSERT [dbo].[Address]
(
	[street1],
	[street2],
	[city],
	[stateProvince],
	[postal_code],
	[zip4],
	[country],
	[address_type],
	[AddressListID]
)
VALUES
(
	@sstreet1,
	@sstreet2,
	@scity,
	@sstateProvince,
	@spostal_code,
	@szip4,
	@scountry,
	@iaddress_type,
	@iAddressListID
)
-- Get the IDENTITY value for the row just inserted.
SELECT @iaddress_id=SCOPE_IDENTITY()
GO
