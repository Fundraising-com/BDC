USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_CAccount_Insert]    Script Date: 06/07/2017 09:33:11 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_CAccount_Insert]
	@sName varchar(50),
	@sCountry varchar(10),
	@sLang varchar(10),
	@sCAccountCodeClass varchar(10),
	@sCAccountCodeGroup varchar(50),
	@iPhoneListID int,
	@iAddressListID int,
	@sAddress1 varchar(50),
	@sAddress2 varchar(50),
	@sCity varchar(50),
	@sState char(2),
	@sZip varchar(12),
	@sZip4 varchar(12),
	@sCounty varchar(20),
	@iStatusID int,
	@iEnrollment int,
	@sComment varchar(1000),
	@sEMail varchar(75),
	@bIsPrivateOrg bit,
	@bIsAdultGroup bit,
	@iParentID int,
	@iSalesRegionID int,
	@iStatementPrintCycleID int,
	@iStatementPrintSlot int,
	@daDateCreatedTOSSthis datetime,
	@daDateUpdated datetime,
	@iUserIDModified int,
	@sVendorNumber varchar(30),
	@sVendorSiteName varchar(15),
	@sVendorPayGroup varchar(25),
	@sOriginalAddress1 varchar(50),
	@sOriginalAddress2 varchar(50),
	@sOriginalCity varchar(50),
	@sOriginalState char(2),
	@sOriginalZip varchar(6),
	@sOriginalZip4 varchar(4),
	@sShipToAddress1 varchar(50),
	@sShipToAddress2 varchar(50),
	@sShipToCity varchar(50),
	@sShipToState char(2),
	@sShipToZip varchar(6),
	@sShipToZip4 varchar(4),
	@sSponsor varchar(50),
	@iBusinessUnitID int,
	@iPartnerID int = null,
	@sProfitChequePayee varchar(50)

AS

DECLARE	@iCAccountID		int

DECLARE	@iNewPhoneListID	int
DECLARE	@iNewAddressListID	int

INSERT INTO	PhoneList
		(CreateDate,
		DeletedTF)
VALUES	(getdate(),
		0)

SET @iNewPhoneListID = SCOPE_IDENTITY()

INSERT INTO	AddressList
		(CreateDate,
		DeletedTF)
VALUES	(getdate(),
		0)

SET @iNewAddressListID = SCOPE_IDENTITY()

-- INSERT a new row in the table.
INSERT [dbo].[CAccount]
(
	[Name],
	[Country],
	[Lang],
	[CAccountCodeClass],
	[CAccountCodeGroup],
	[PhoneListID],
	[AddressListID],
	[Address1],
	[Address2],
	[City],
	[State],
	[Zip],
	[Zip4],
	[County],
	[StatusID],
	[Enrollment],
	[Comment],
	[EMail],
	[IsPrivateOrg],
	[IsAdultGroup],
	[ParentID],
	[SalesRegionID],
	[StatementPrintCycleID],
	[StatementPrintSlot],
	[DateCreatedTOSSthis],
	[DateUpdated],
	[UserIDModified],
	[VendorNumber],
	[VendorSiteName],
	[VendorPayGroup],
	[OriginalAddress1],
	[OriginalAddress2],
	[OriginalCity],
	[OriginalState],
	[OriginalZip],
	[OriginalZip4],
	[ShipToAddress1],
	[ShipToAddress2],
	[ShipToCity],
	[ShipToState],
	[ShipToZip],
	[ShipToZip4],
	[Sponsor],	[BusinessUnitID],
	[PartnerID],
	[ProfitChequePayee]
)
VALUES
(
	@sName,
	@sCountry,
	@sLang,
	@sCAccountCodeClass,
	@sCAccountCodeGroup,
	@iNewPhoneListID,
	@iNewAddressListID,
	@sAddress1,
	@sAddress2,
	@sCity,
	@sState,
	@sZip,
	@sZip4,
	@sCounty,
	@iStatusID,
	@iEnrollment,
	@sComment,
	@sEMail,
	@bIsPrivateOrg,
	@bIsAdultGroup,
	@iParentID,
	@iSalesRegionID,
	@iStatementPrintCycleID,
	@iStatementPrintSlot,
	@daDateCreatedTOSSthis,
	@daDateUpdated,
	@iUserIDModified,
	@sVendorNumber,
	@sVendorSiteName,
	@sVendorPayGroup,
	@sOriginalAddress1,
	@sOriginalAddress2,
	@sOriginalCity,
	@sOriginalState,
	@sOriginalZip,
	@sOriginalZip4,
	@sShipToAddress1,
	@sShipToAddress2,
	@sShipToCity,
	@sShipToState,
	@sShipToZip,
	@sShipToZip4,
	@sSponsor,
	@iBusinessUnitID,
	@iPartnerID,
	@sProfitChequePayee
)

SET @iCAccountID = SCOPE_IDENTITY()

-- Ben - 2006-01-04: Called from the application after address is inserted
--EXEC QSPCanadaOrderManagement..pr_populate_Account @MinAccountID = @iCAccountID, @MaxAccountID = @iCAccountID

SELECT	[Id],
		[Name],
		[Country],
		[Lang],
		[CAccountCodeClass],
		[CAccountCodeGroup],
		[PhoneListID],
		[AddressListID],
		[Address1],
		[Address2],
		[City],
		[State],
		[Zip],
		[Zip4],
		[County],
		[StatusID],
		[Enrollment],
		[Comment],
		[EMail],
		[IsPrivateOrg],
		[IsAdultGroup],
		[ParentID],
		[SalesRegionID],
		[StatementPrintCycleID],
		[StatementPrintSlot],
		[DateCreatedTOSSthis],
		[DateUpdated],
		[UserIDModified],
		[VendorNumber],
		[VendorSiteName],
		[VendorPayGroup],
		[OriginalAddress1],
		[OriginalAddress2],
		[OriginalCity],
		[OriginalState],
		[OriginalZip],
		[OriginalZip4],
		[ShipToAddress1],
		[ShipToAddress2],
		[ShipToCity],
		[ShipToState],
		[ShipToZip],
		[ShipToZip4],
		[Sponsor],
		[BusinessUnitID],
		[PartnerID],
		[ProfitChequePayee]
FROM		CAccount
WHERE	Id = @iCAccountID
GO
