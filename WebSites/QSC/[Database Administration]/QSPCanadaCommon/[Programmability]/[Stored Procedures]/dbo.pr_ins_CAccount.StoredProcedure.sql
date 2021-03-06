USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_ins_CAccount]    Script Date: 06/07/2017 09:33:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE   PROCEDURE [dbo].[pr_ins_CAccount]
	@Account_ID int output,
	@Name varchar(50),
	@Country varchar(10),
	@Lang varchar(10),
	@CAccountCodeClass varchar(10),
	@CAccountCodeGroup varchar(50),
	@PhoneListID int OUT,
	@AddressListID int OUT,
	@StatusID int,
	@Enrollment int,
	@Comment varchar(1000),
	@EMail varchar(75),
	@IsPrivateOrg bit,
	@IsAdultGroup bit,
	@Sponsor varchar(50),
	@ParentID int,
	@UserIDModified UserID_UDDT,
--	@Address1 varchar(50),
--	@Address2 varchar(50),
--	@City varchar(50),
--	@State char(2),
--	@Zip varchar(12),
--	@Zip4 varchar(12),
--	@County varchar(20),
--	@SalesRegionID int,
--	@StatementPrintCycleID int,
--	@StatementPrintSlot int,
--	@DateCreated datetime,
--	@DateUpdated datetime,
--	@UserIDUpdated varchar(50),
	@VendorNumber varchar(30),
	@VendorSiteName varchar(15),
	@VendorPayGroup varchar(25)
--	@OriginalAddress1 varchar(50),
--	@OriginalAddress2 varchar(50),
--	@OriginalCity varchar(50),
--	@OriginalState char(2),
--	@OriginalZip varchar(6),
--	@OriginalZip4 varchar(4),
--	@ShipToAddress1 varchar(50),
--	@ShipToAddress2 varchar(50),
--	@ShipToCity varchar(50),
--	@ShipToState char(2),
--	@ShipToZip varchar(6),
--	@ShipToZip4 varchar(4)

AS

--BEGIN TRANSACTION

-----------------------------------------------
--  Get a new PhoneListID   ---
-----------------------------------------------
insert into PhoneList(CreateDate) values(GetDate())	
SELECT @PhoneListID = @@Identity

-------------------------------------------------
--  Get a new AddressListID   ---
-------------------------------------------------
insert into AddressList(CreateDate) values(GetDate())	
SELECT @AddressListID = @@Identity





INSERT INTO CAccount (
	Name,
	Country,
	Lang,
	CAccountCodeClass,
	CAccountCodeGroup,
	PhoneListID,
	AddressListID,
--	Address1,
--	Address2,
--	City,
--	State,
--	Zip,
--	Zip4,
--	County,
	StatusID,
	Enrollment,
	Comment,
	EMail,
	IsPrivateOrg,
	IsAdultGroup,
	ParentID,
--	SalesRegionID,
--	StatementPrintCycleID,
--	StatementPrintSlot,
	DateUpdated,
	UserIdModified,
--	DateUpdated,
--	UserIDUpdated,
	VendorNumber,
	VendorSiteName,
	VendorPayGroup,
--	OriginalAddress1,
--	OriginalAddress2,
--	OriginalCity,
--	OriginalState,
--	OriginalZip,
--	OriginalZip4,
--	ShipToAddress1,
--	ShipToAddress2,
--	ShipToCity,
--	ShipToState,
--	ShipToZip,
--	ShipToZip4
	Sponsor
)VALUES(
	@Name,
	@Country,
	@Lang,
	@CAccountCodeClass,
	@CAccountCodeGroup,
	@PhoneListID,
	@AddressListID,
--	@Address1,
--	@Address2,
--	@City,
--	@State,
--	@Zip,
--	@Zip4,
--	@County,
	@StatusID,
	@Enrollment,
	@Comment,
	@EMail,
	@IsPrivateOrg,
	@IsAdultGroup,
	@ParentID,
--	@SalesRegionID,
--	@StatementPrintCycleID,
--	@StatementPrintSlot,
	getdate(),
	@UserIDModified,
--	@DateUpdated,
--	@UserIDUpdated,
	@VendorNumber,
	@VendorSiteName,
	@VendorPayGroup,
--	@OriginalAddress1,
--	@OriginalAddress2,
--	@OriginalCity,
--	@OriginalState,
--	@OriginalZip,
--	@OriginalZip4,
--	@ShipToAddress1,
--	@ShipToAddress2,
--	@ShipToCity,
--	@ShipToState,
--	@ShipToZip,
--	@ShipToZip4
	@Sponsor
)

SELECT @Account_ID = @@Identity

--commit transaction
GO
