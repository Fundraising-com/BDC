USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Campaign_Update]    Script Date: 06/07/2017 09:33:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Campaign_Update]
	@iID int,
	@iStatus int,
	@bRenewal bit,
	@sCountry varchar(50),
	@sFMID varchar(4),
	@sDateChanged varchar(50),
	@sLang varchar(10),
	@daEndDate datetime,
	@daStartDate datetime,
	@iIncentivesBillToID int,
	@iBillToAccountID int,
	@iShipToCampaignContactID int,
	@iShipToAccountID int,
	@dcEstimatedGross numeric(10, 2),
	@iNumberOfParticipants int,
	@iNumberOfClassroooms int,
	@iNumberOfStaff int,
	@iBillToCampaignContactID int,
	@iSuppliesCampaignContactID int,
	@iSuppliesShipToCampaignContactID int,
	@daSuppliesDeliveryDate datetime,
	@sSpecialInstructions varchar(1000),
	@bIsStaffOrder bit,
	@dcStaffOrderDiscount numeric(10, 2),
	@bIsTestCampaign bit,
	@daDateModified datetime,
	@iUserIDModified int,
	@bIsPayLater bit,
	@iIncentivesDistributionID int,
	@bFSRequired bit,
	@bFSExtraRequired bit,
	@bFSOrderRecCreated bit,
	@daApprovedStatusDate datetime,
	@daMagnetStatementDate datetime,
	@bRewardsProgramCumulative bit,
	@bRewardsProgramChart bit,
	@bRewardsProgramDraw bit,
	@sContactName varchar(50),
	@iPhoneListID int,
	@iSuppliesAddressID int,
	@dDateSubmitted datetime,
	@iExtra_1Ups int,
	@iExtra_GiftForm int,
	@iExtra_MagBrochure int,
	@iCoolCardsBoxes int,
	@bOnlineOnlyPrograms bit,
	@bForceStatementPrint bit,
	@bDisableStatementPrint bit,
	@daCookieDoughDeliveryDate datetime,
	@iCarrierID int,
	@sNotes nvarchar(4000),
	@bOnlineNutFree bit,
	@bOnlineMagazineTRTOnly bit


AS
SET NOCOUNT ON

IF @iCoolCardsBoxes = -1
	SET @iCoolCardsBoxes = null

-- UPDATE an existing row in the table.
UPDATE [dbo].[Campaign]
SET 
	[Status] = @iStatus,
	[Renewal] = @bRenewal,
	[Country] = @sCountry,
	[FMID] = @sFMID,
	[DateChanged] = @sDateChanged,
	[Lang] = @sLang,
	[EndDate] = @daEndDate,
	[StartDate] = @daStartDate,
	[IncentivesBillToID] = @iIncentivesBillToID,
	[BillToAccountID] = @iBillToAccountID,
	[ShipToCampaignContactID] = @iShipToCampaignContactID,
	[ShipToAccountID] = @iShipToAccountID,
	[EstimatedGross] = @dcEstimatedGross,
	[NumberOfParticipants] = @iNumberOfParticipants,
	[NumberOfClassroooms] = @iNumberOfClassroooms,
	[NumberOfStaff] = @iNumberOfStaff,
	[BillToCampaignContactID] = @iBillToCampaignContactID,
	[SuppliesCampaignContactID] = @iSuppliesCampaignContactID,
	[SuppliesShipToCampaignContactID] = @iSuppliesShipToCampaignContactID,
	[SuppliesDeliveryDate] = @daSuppliesDeliveryDate,
	[SpecialInstructions] = @sSpecialInstructions,
	[IsStaffOrder] = @bIsStaffOrder,
	[StaffOrderDiscount] = @dcStaffOrderDiscount,
	[IsTestCampaign] = @bIsTestCampaign,
	[DateModified] = @daDateModified,
	[UserIDModified] = @iUserIDModified,
	[IsPayLater] = @bIsPayLater,
	[IncentivesDistributionID] = @iIncentivesDistributionID,
	[FSRequired] = @bFSRequired,
	[FSExtraRequired] = @bFSExtraRequired,
	[FSOrderRecCreated] = @bFSOrderRecCreated,
	[ApprovedStatusDate] = @daApprovedStatusDate,
	[MagnetStatementDate] = @daMagnetStatementDate,
	[RewardsProgramCumulative] = @bRewardsProgramCumulative,
	[RewardsProgramChart] = @bRewardsProgramChart,
	[RewardsProgramDraw] = @bRewardsProgramDraw,
	[ContactName] = @sContactName,
	[PhoneListID] = @iPhoneListID,
	[SuppliesAddressID] = @iSuppliesAddressID,
	[DateSubmitted] = @dDateSubmitted,
	[Extra_1UPs]= ISNULL(@iExtra_1Ups,0),
	[Extra_GiftForm]=ISNULL(@iExtra_GiftForm,0),
	[Extra_MagBrochure] = ISNULL(@iExtra_MagBrochure,0),
	[CoolCardsBoxes] = @iCoolCardsBoxes,
	[OnlineOnlyPrograms]=@bOnlineOnlyPrograms,
	[ForceStatementPrint]=@bForceStatementPrint,
	[DisableStatementPrint]=@bDisableStatementPrint,
	[CookieDoughDeliveryDate] = @daCookieDoughDeliveryDate,
	[CarrierID] = @iCarrierID,
	[Notes] = @sNotes,
	[OnlineNutFree] = @bOnlineNutFree,
	[OnlineMagazineTRTOnly] = @bOnlineMagazineTRTOnly
WHERE
	[ID] = @iID

UPDATE   CAccount
SET      DateUpdated = @daDateModified
WHERE    Id = @iBillToAccountID
GO
