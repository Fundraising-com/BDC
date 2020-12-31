USE [QSPCanadaCommon]

DECLARE	@iStatus							int,
		@bRenewal							bit,
		@sCountry							varchar(50),
		@sFMID								varchar(4),
		@sDateChanged						varchar(50),
		@sLang								varchar(10),
		@daEndDate							datetime,
		@daStartDate						datetime,
		@iIncentivesBillToID				int,
		@iBillToAccountID					int,
		@iShipToCampaignContactID			int,
		@iShipToAccountID					int,
		@dcEstimatedGross					numeric(10, 2),
		@iNumberOfParticipants				int,
		@iNumberOfClassroooms				int,
		@iNumberOfStaff						int,
		@iBillToCampaignContactID			int,
		@iSuppliesCampaignContactID			int,
		@iSuppliesShipToCampaignContactID	int,
		@daSuppliesDeliveryDate				datetime,
		@sSpecialInstructions				varchar(1000),
		@bIsStaffOrder						bit,
		@dcStaffOrderDiscount				numeric(10, 2),
		@bIsTestCampaign					bit,
		@daDateModified						datetime,
		@iUserIDModified					int,
		@bIsPayLater						bit,
		@iIncentivesDistributionID			int,
		@bFSRequired						bit,
		@bFSExtraRequired					bit,
		@bFSOrderRecCreated					bit,
		@daApprovedStatusDate				datetime,
		@daMagnetStatementDate				datetime,
		@bRewardsProgramCumulative			bit,
		@bRewardsProgramChart				bit,
		@bRewardsProgramDraw				bit,
		@sContactName						varchar(50),
		@iPhoneListID						int,
		@iSuppliesAddressID					int,
		@dDateSubmitted						datetime,
		@iExtra_1Ups						int,
		@iExtra_GiftForm					int,
		@iExtra_MagBrochure					bit,
		@bOnlineOnlyPrograms				bit,
		@bForceStatementPrint				bit,
		@bDisableStatementPrint				bit,
		@daCookieDoughDeliveryDate			datetime,
		@iID								int

SELECT	@daEndDate = seas.EndDate,
		@daStartDate = seas.StartDate
FROM	Season seas
WHERE	'2014-07-02' BETWEEN seas.StartDate and seas.EndDate
AND		seas.Season = 'Y'

SET	@sDateChanged						= GETDATE()
SET	@daApprovedStatusDate				= GETDATE()
SET @dDateSubmitted						= GETDATE()

DECLARE CampaignInfo CURSOR FOR
SELECT	camp.BillToAccountID,
		camp.ShipToCampaignContactID,
		camp.ShipToAccountID,
		camp.Status,
		camp.Renewal,
		camp.Country,
		camp.FMID,
		camp.Lang,
		camp.IncentivesBillToID,
		camp.EstimatedGross,
		camp.NumberOfParticipants,
		camp.NumberOfClassroooms,
		camp.NumberOfStaff,
		camp.BillToCampaignContactID,
		camp.SuppliesCampaignContactID,
		camp.SuppliesShipToCampaignContactID,
		camp.SuppliesDeliveryDate,
		camp.SpecialInstructions,
		camp.IsStaffOrder,
		camp.StaffOrderDiscount,
		camp.IsTestCampaign,
		camp.UserIDModified,
		camp.IsPayLater,
		camp.IncentivesDistributionID,
		camp.FSRequired,
		camp.FSExtraRequired,
		camp.FSOrderRecCreated,
		camp.MagnetStatementDate,
		camp.RewardsProgramCumulative,
		camp.RewardsProgramChart,
		camp.RewardsProgramDraw,
		camp.ContactName,
		camp.PhoneListID,
		camp.SuppliesAddressID,
		camp.Extra_1Ups,
		camp.Extra_MagBrochure,
		camp.Extra_GiftForm,
		camp.ForceStatementPrint,
		camp.DisableStatementPrint,
		camp.OnlineOnlyPrograms,
		camp.CookieDoughDeliveryDate
FROM	Campaign camp
JOIN	CAccount acc ON acc.ID = camp.ShipToAccountID
JOIN	[Address] ad ON ad.AddressListID = acc.AddressListID AND ad.address_type = 54001
WHERE	camp.FMID = '0508'
AND		camp.StartDate BETWEEN '2013-07-01' AND '2014-06-30'
AND		ad.StateProvince = 'QC' --Starting 07/2014 only continue with 1 account per partner
AND		ISNULL(acc.PartnerID, 0) > 0

OPEN CampaignInfo
FETCH NEXT FROM CampaignInfo INTO	@iBillToAccountID, @iShipToCampaignContactID, @iShipToAccountID, @iStatus, @bRenewal, @sCountry, @sFMID, @sLang, @iIncentivesBillToID, @dcEstimatedGross,
									@iNumberOfParticipants, @iNumberOfClassroooms, @iNumberOfStaff, @iBillToCampaignContactID, @iSuppliesCampaignContactID,
									@iSuppliesShipToCampaignContactID, @daSuppliesDeliveryDate, @sSpecialInstructions, @bIsStaffOrder,
									@dcStaffOrderDiscount, @bIsTestCampaign, @iUserIDModified, @bIsPayLater, @iIncentivesDistributionID,
									@bFSRequired, @bFSExtraRequired, @bFSOrderRecCreated, @daMagnetStatementDate, @bRewardsProgramCumulative,
									@bRewardsProgramChart, @bRewardsProgramDraw, @sContactName, @iPhoneListID, @iSuppliesAddressID, @iExtra_1Ups, @iExtra_MagBrochure,
									@iExtra_GiftForm, @bForceStatementPrint, @bDisableStatementPrint, @bOnlineOnlyPrograms, @daCookieDoughDeliveryDate

WHILE(@@FETCH_STATUS = 0)
BEGIN

	EXEC pr_Campaign_Insert
		@iStatus,
		@bRenewal,
		@sCountry,
		@sFMID,
		@sDateChanged,
		@sLang,
		@daEndDate,
		@daStartDate,
		@iIncentivesBillToID,
		@iBillToAccountID,
		@iShipToCampaignContactID,
		@iShipToAccountID,
		@dcEstimatedGross,
		@iNumberOfParticipants,
		@iNumberOfClassroooms,
		@iNumberOfStaff,
		@iBillToCampaignContactID,
		@iSuppliesCampaignContactID,
		@iSuppliesShipToCampaignContactID,
		@daSuppliesDeliveryDate,
		@sSpecialInstructions,
		@bIsStaffOrder,
		@dcStaffOrderDiscount,
		@bIsTestCampaign,
		@daDateModified,
		@iUserIDModified,
		@bIsPayLater,
		@iIncentivesDistributionID,
		@bFSRequired,
		@bFSExtraRequired,
		@bFSOrderRecCreated,
		@daApprovedStatusDate,
		@daMagnetStatementDate,
		@bRewardsProgramCumulative,
		@bRewardsProgramChart,
		@bRewardsProgramDraw,
		@sContactName,
		@iPhoneListID,
		@iSuppliesAddressID,
		@dDateSubmitted,
		@iExtra_1Ups,
		@iExtra_GiftForm,
		@iExtra_MagBrochure,
		@bOnlineOnlyPrograms,
		@bForceStatementPrint,
		@bDisableStatementPrint,
		@daCookieDoughDeliveryDate,
		@iID = @iID OUTPUT

	EXEC pr_CampaignProgram_Insert
		@iCampaignID = @iID,
		@iProgramID = 1,
		@sIsPreCollect = 'Y',
		@dcGroupProfit = 37.00,
		@bDeletedTF = 0,
		@bIsPersonalize = 0,
		@bBlackboardPacket = 0

	EXEC pr_CampaignProgram_Insert
		@iCampaignID = @iID,
		@iProgramID = 52,
		@sIsPreCollect = 'Y',
		@dcGroupProfit = 40.00,
		@bDeletedTF = 0,
		@bIsPersonalize = 0,
		@bBlackboardPacket = 0

FETCH NEXT FROM CampaignInfo INTO	@iBillToAccountID, @iShipToCampaignContactID, @iShipToAccountID, @iStatus, @bRenewal, @sCountry, @sFMID, @sLang, @iIncentivesBillToID, @dcEstimatedGross,
									@iNumberOfParticipants, @iNumberOfClassroooms, @iNumberOfStaff, @iBillToCampaignContactID, @iSuppliesCampaignContactID,
									@iSuppliesShipToCampaignContactID, @daSuppliesDeliveryDate, @sSpecialInstructions, @bIsStaffOrder,
									@dcStaffOrderDiscount, @bIsTestCampaign, @iUserIDModified, @bIsPayLater, @iIncentivesDistributionID,
									@bFSRequired, @bFSExtraRequired, @bFSOrderRecCreated, @daMagnetStatementDate, @bRewardsProgramCumulative,
									@bRewardsProgramChart, @bRewardsProgramDraw, @sContactName, @iPhoneListID, @iSuppliesAddressID, @iExtra_1Ups, @iExtra_MagBrochure,
									@iExtra_GiftForm, @bForceStatementPrint, @bDisableStatementPrint, @bOnlineOnlyPrograms, @daCookieDoughDeliveryDate

END
CLOSE CampaignInfo
DEALLOCATE CampaignInfo