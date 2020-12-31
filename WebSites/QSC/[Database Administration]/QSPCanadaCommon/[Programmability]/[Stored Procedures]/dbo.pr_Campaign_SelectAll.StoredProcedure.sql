USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Campaign_SelectAll]    Script Date: 06/07/2017 09:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'Campaign'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Campaign_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
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
	[IncentivesBillToID],
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
	[DisableStatementPrint]
FROM [dbo].[Campaign]
ORDER BY 
	[ID] ASC
GO
