USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_CAccount_SelectAll]    Script Date: 06/07/2017 09:33:11 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'CAccount'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_CAccount_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[Id],
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
	[ProfitChequePayee]
FROM [dbo].[CAccount]
ORDER BY 
	[Id] ASC
GO
