USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Campaign_SelectOne]    Script Date: 06/07/2017 09:33:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Campaign_SelectOne]
	@iID int
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	[ID],
	[Status],
	[Renewal],
	[Country],
	[FMID],
	[DateChanged],
	[Lang],
	[EndDate],
	[StartDate],
	ISNULL([IncentivesBillToID], 0) IncentivesBillToID,
	[BillToAccountID],
	[ShipToCampaignContactID],
	[ShipToAccountID],
	[EstimatedGross],
	[NumberOfParticipants],
	[NumberOfClassroooms],
	[NumberOfStaff],
	[BillToCampaignContactID],
	[SuppliesCampaignContactID],
	[SuppliesShipToCampaignContactID],
	[SuppliesDeliveryDate],
	[SpecialInstructions],
	[IsStaffOrder],
	[StaffOrderDiscount],
	[IsTestCampaign],
	[DateModified],
	[UserIDModified],
	[IsPayLater],
	[IncentivesDistributionID],
	[FSRequired],
	[FSExtraRequired],
	[FSOrderRecCreated],
	[ApprovedStatusDate],
	[MagnetStatementDate],
	[RewardsProgramCumulative],
	[RewardsProgramChart],
	[RewardsProgramDraw],
	[ContactName],
	[PhoneListID],
	[SuppliesAddressID],
	[DateSubmitted],
	[Extra_1UPS],
	[Extra_GiftForm],
	[Extra_MagBrochure],
	[CoolCardsBoxes],
	[OnlineOnlyPrograms],
	[ForceStatementPrint],
	[DisableStatementPrint],
	[CookieDoughDeliveryDate],
	[CarrierID],
	[Notes],
	[OnlineNutFree],
	[OnlineMagazineTRTOnly]
FROM [dbo].[Campaign]
WHERE
	[ID] = @iID
GO
