USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_CAccount_Update]    Script Date: 06/07/2017 09:33:11 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_CAccount_Update]
	@iId int,
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
	@sProfitChequePayee varchar(50)

AS

SET NOCOUNT ON
-- UPDATE an existing row in the table.
UPDATE [dbo].[CAccount]
SET 
	[Name] = @sName,
	[Country] = @sCountry,
	[Lang] = @sLang,
	[CAccountCodeClass] = @sCAccountCodeClass,
	[CAccountCodeGroup] = @sCAccountCodeGroup,
	[PhoneListID] = @iPhoneListID,
	[AddressListID] = @iAddressListID,
	[Address1] = @sAddress1,
	[Address2] = @sAddress2,
	[City] = @sCity,
	[State] = @sState,
	[Zip] = @sZip,
	[Zip4] = @sZip4,
	[County] = @sCounty,
	[StatusID] = @iStatusID,
	[Enrollment] = @iEnrollment,
	[Comment] = @sComment,
	[EMail] = @sEMail,
	[IsPrivateOrg] = @bIsPrivateOrg,
	[IsAdultGroup] = @bIsAdultGroup,
	[ParentID] = @iParentID,
	[SalesRegionID] = @iSalesRegionID,
	[StatementPrintCycleID] = @iStatementPrintCycleID,
	[StatementPrintSlot] = @iStatementPrintSlot,
	[DateCreatedTOSSthis] = @daDateCreatedTOSSthis,
	[DateUpdated] = @daDateUpdated,
	[UserIDModified] = @iUserIDModified,
	[VendorNumber] = @sVendorNumber,
	[VendorSiteName] = @sVendorSiteName,
	[VendorPayGroup] = @sVendorPayGroup,
	[OriginalAddress1] = @sOriginalAddress1,
	[OriginalAddress2] = @sOriginalAddress2,
	[OriginalCity] = @sOriginalCity,
	[OriginalState] = @sOriginalState,
	[OriginalZip] = @sOriginalZip,
	[OriginalZip4] = @sOriginalZip4,
	[ShipToAddress1] = @sShipToAddress1,
	[ShipToAddress2] = @sShipToAddress2,
	[ShipToCity] = @sShipToCity,
	[ShipToState] = @sShipToState,
	[ShipToZip] = @sShipToZip,
	[ShipToZip4] = @sShipToZip4,
	[Sponsor] = @sSponsor,
	[BusinessUnitID] = @iBusinessUnitID,
	[ProfitChequePayee] = @sProfitChequePayee
WHERE
	[Id] = @iId

EXEC QSPCanadaOrderManagement..pr_Account_Update @MinAccountID = @iId, @MaxAccountID = @iId
GO
