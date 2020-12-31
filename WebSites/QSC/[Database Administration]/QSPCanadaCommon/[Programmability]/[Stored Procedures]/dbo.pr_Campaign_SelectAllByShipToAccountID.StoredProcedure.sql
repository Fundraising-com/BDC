USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Campaign_SelectAllByShipToAccountID]    Script Date: 06/07/2017 09:33:12 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select an existing row from the table 'Campaign'
-- based on the Primary Key.
-- Gets: @iID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Campaign_SelectAllByShipToAccountID]
	@iShipToAccountID int
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
	[DateSubmitted]
FROM [dbo].[Campaign]
WHERE
	[ShipToAccountID] = @iShipToAccountID
GO
