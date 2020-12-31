USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Address_SelectAll]    Script Date: 06/07/2017 09:33:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================================
-- Author:		Jeff Miles
-- Create date: Jan/23/2006
-- Description:	Selects row(s) from the Address Table
-- ============================================================
CREATE PROCEDURE [dbo].[pr_Address_SelectAll]
	@Address_id			int			= null,
	@Street1			varchar(50)	= '',
	@Street2			varchar(50)	= '',
	@City				varchar(50)	= '',
	@StateProvince		varchar(10)	= '',
	@Postal_Code		varchar(7)	= '',
	@Zip4				varchar(4)	= '',
	@Country			varchar(10)	= '',
	@Address_Type		int			= null,
	@AddressListID		int			= null

AS
SET NOCOUNT ON;

SELECT		Address_ID,
			Street1,
			Street2,
			City,
			StateProvince,
			Postal_Code,
			Zip4,
			Country,
			Address_Type,
			AddressListID
FROM		Address
WHERE		--Address_ID = @Address_id
--AND		Street1 = @Street1
--AND		Street2 = @Street2
--AND		City = @City
--AND		StateProvince = @StateProvince
--AND		Postal_Code = @Postal_code
--AND		Zip4 = @Zip4
--AND		Country = @Country
--AND		Address_Type = @Address_type
			AddressListID = @AddressListID
ORDER BY	Address_ID, Address_Type
GO
