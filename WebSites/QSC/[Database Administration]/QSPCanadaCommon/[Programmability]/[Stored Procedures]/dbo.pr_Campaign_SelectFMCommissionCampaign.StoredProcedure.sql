USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Campaign_SelectFMCommissionCampaign]    Script Date: 06/07/2017 09:33:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Campaign_SelectFMCommissionCampaign]
	@FMID VARCHAR(4)
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	camp.[ID],
	[Status],
	[Renewal],
	camp.[Country],
	[FMID],
	[DateChanged],
	camp.[Lang],
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
	camp.[UserIDModified],
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
	camp.[PhoneListID],
	[SuppliesAddressID],
	[DateSubmitted],
	[Extra_1UPS],
	[Extra_GiftForm],
	[Extra_MagBrochure],
	[OnlineOnlyPrograms],
	[ForceStatementPrint],
	[DisableStatementPrint],
	[CookieDoughDeliveryDate]
FROM	Campaign camp
JOIN	CAccount acc
			ON	acc.Id = camp.BillToAccountID
WHERE	camp.FMID = @FMID
AND		acc.CAccountCodeGroup = 'Comm'
AND		camp.Status = 37002
GO
