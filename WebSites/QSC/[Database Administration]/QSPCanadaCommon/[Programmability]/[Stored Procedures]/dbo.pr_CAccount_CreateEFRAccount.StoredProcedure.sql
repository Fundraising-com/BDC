USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_CAccount_CreateEFRAccount]    Script Date: 06/07/2017 09:33:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_CAccount_CreateEFRAccount]

	@PartnerID		int

AS

--Address
DECLARE	@sstreet1			varchar(50),
		@sstreet2			varchar(50),
		@scity				varchar(50),
		@sstateProvince		varchar(10),
		@spostal_code		varchar(7),
		@szip4				varchar(4),
		@scountry			varchar(10),
		@iaddress_type		int,
		@iAddressListID		int,
		@iAddress_ID		int

SET		@sstreet1			= '20 RUE QUEEN'
SET		@sstreet2			= 'BUREAU 200'
SET		@scity				= 'MONTREAL'
SET		@sstateProvince		= 'QC'
SET		@spostal_code		= 'H3C2N5'
SET		@szip4				= ''
SET		@scountry			= 'CA'

--Phone
DECLARE	@iType				int,
		@iPhoneListID		int,
		@sPhoneNumber		varchar(50),
		@sBestTimeToCall	varchar(2000),
		@iID2				int

SET		@iType				= 30501
SET		@sPhoneNumber		= '514-875-1245'
SET		@sBestTimeToCall	= ''

--CAccount
DECLARE	@sName					varchar(50),
		@sCountry2				varchar(10),
		@sLang					varchar(10),
		@sCAccountCodeClass		varchar(10),
		@sCAccountCodeGroup		varchar(50),
		@iStatusID				int,
		@iEnrollment			int,
		@sComment				varchar(1000),
		@sEMail					varchar(75),
		@bIsPrivateOrg			bit,
		@bIsAdultGroup			bit,
		@iParentID				int,
		@iSalesRegionID			int,
		@iStatementPrintCycleID	int,
		@iStatementPrintSlot	int,
		@daDateCreatedTOSSthis	datetime,
		@daDateUpdated			datetime,
		@iUserIDModified		int,
		@sVendorNumber			varchar(30),
		@sVendorSiteName		varchar(15),
		@sVendorPayGroup		varchar(25),
		@sOriginalAddress1		varchar(50),
		@sOriginalAddress2		varchar(50),
		@sOriginalCity			varchar(50),
		@sOriginalState			char(2),
		@sOriginalZip			varchar(6),
		@sOriginalZip4			varchar(4),
		@sShipToAddress1		varchar(50),
		@sShipToAddress2		varchar(50),
		@sShipToCity			varchar(50),
		@sShipToState			char(2),
		@sShipToZip				varchar(6),
		@sShipToZip4			varchar(4),
		@sSponsor				varchar(50),
		@iCAccountID			int,
		@iBusinessUnitID		int

SET		@sCountry2				= 'CA'
SET		@sLang					= 'EN'
SET		@sCAccountCodeClass		= 'NSc'
SET		@sCAccountCodeGroup		= 'NSc7'
SET		@iStatusID				= 35001
SET		@iEnrollment			= 10
SET		@sComment				= ''
SET		@sEMail					= ''
SET		@bIsPrivateOrg			= CONVERT(BIT, 0)
SET		@bIsAdultGroup			= CONVERT(BIT, 0)
SET		@iParentID				= NULL
SET		@iSalesRegionID			= NULL
SET		@iStatementPrintCycleID	= NULL
SET		@iStatementPrintSlot	= NULL
SET		@daDateCreatedTOSSthis	= GETDATE()
SET		@daDateUpdated			= GETDATE()
SET		@iUserIDModified		= 612
SET		@sVendorNumber			= ''
SET		@sVendorSiteName		= ''
SET		@sVendorPayGroup		= ''
SET		@sOriginalAddress1		= ''
SET		@sOriginalAddress2		= ''
SET		@sOriginalCity			= ''
SET		@sOriginalState			= ''
SET		@sOriginalZip			= ''
SET		@sOriginalZip4			= ''
SET		@sShipToAddress1		= ''
SET		@sShipToAddress2		= ''
SET		@sShipToCity			= ''
SET		@sShipToState			= ''
SET		@sShipToZip				= ''
SET		@sShipToZip4			= ''
SET		@sSponsor				= ''
SET		@iBusinessUnitID		= 2 --2: EFR

--Campaign
DECLARE	@iStatus							int,
		@bRenewal							bit,
		@sCountry3							varchar(50),
		@sFMID								varchar(4),
		@sDateChanged						varchar(50),
		@sLang2								varchar(10),
		@daEndDate							datetime,
		@daStartDate						datetime,
		@iIncentivesBillToID				int,
		@iBillToAccountID					int,
		@iCampaignContactID					int,
		@iShipToAccountID					int,
		@dcEstimatedGross					numeric(10, 2),
		@iNumberOfParticipants				int,
		@iNumberOfClassroooms				int,
		@iNumberOfStaff						int,
		@iSuppliesCampaignContactID			int,
		@iSuppliesShipToCampaignContactID	int,
		@daSuppliesDeliveryDate				datetime,
		@sSpecialInstructions				varchar(1000),
		@bIsStaffOrder						bit,
		@dcStaffOrderDiscount				numeric(10, 2),
		@bIsTestCampaign					bit,
		@daDateModified						datetime,
		@iUserIDModified2					int,
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
		@iSuppliesAddressID					int,
		@dDateSubmitted						datetime,
		@iExtra_1Ups						int,
		@iExtra_GiftForm					int,
		@iExtra_MagBrochure					int,
		@bOnlineOnlyPrograms				bit,
		@bForceStatementPrint				bit,
		@bDisableStatementPrint				bit,
		@daCookieDoughDeliveryDate			datetime,
		@iID								int,
		@CampaignContactTitle				varchar(50),
		@CampaignContactFirstName			varchar(50),
		@CampaignContactLastName			varchar(50),
		@CampaignContactTypeID				int,
		@CampaignContactDateChanged			datetime

SELECT	@daEndDate = seas.EndDate,
		@daStartDate = seas.StartDate
FROM	Season seas
WHERE	GETDATE() BETWEEN seas.StartDate and seas.EndDate
AND		seas.Season = 'Y'

SET	@iStatus							= 37002
SET	@bRenewal							= CONVERT(BIT, 0)
SET	@sCountry3							= 'CA'
SET	@sFMID								= '0508'
SET	@sDateChanged						= GETDATE()
SET	@sLang2								= 'EN'
SET	@iIncentivesBillToID				= 51004
SET	@dcEstimatedGross					= 0.00
SET	@iNumberOfParticipants				= 10
SET	@iNumberOfClassroooms				= 1
SET	@iNumberOfStaff						= 1
SET	@iSuppliesCampaignContactID			= NULL
SET	@iSuppliesShipToCampaignContactID	= NULL
SET	@daSuppliesDeliveryDate				= '1995-01-01 00:00:00.000'
SET	@sSpecialInstructions				= NULL
SET	@bIsStaffOrder						= CONVERT(BIT, 0)
SET	@dcStaffOrderDiscount				= NULL
SET	@bIsTestCampaign					= CONVERT(BIT, 0)
SET	@daDateModified						= GETDATE()
SET	@iUserIDModified2					= 612
SET	@bIsPayLater						= CONVERT(BIT, 0)
SET	@iIncentivesDistributionID			= NULL
SET	@bFSRequired						= CONVERT(BIT, 0)
SET	@bFSExtraRequired					= CONVERT(BIT, 0)
SET	@bFSOrderRecCreated					= CONVERT(BIT, 0)
SET	@daApprovedStatusDate				= GETDATE()
SET	@daMagnetStatementDate				= NULL
SET	@bRewardsProgramCumulative			= NULL
SET	@bRewardsProgramChart				= NULL
SET	@bRewardsProgramDraw				= NULL
SET	@sContactName						= NULL
SET	@iSuppliesAddressID					= -1
SET @dDateSubmitted						= GETDATE()
SET	@iExtra_1Ups						= 0
SET	@iExtra_GiftForm					= 0
SET @iExtra_MagBrochure					= 0
SET	@bOnlineOnlyPrograms				= CONVERT(BIT, 1)
SET @bForceStatementPrint				= CONVERT(BIT, 0)
SET @bDisableStatementPrint				= CONVERT(BIT, 0)
SET @daCookieDoughDeliveryDate			= NULL
SET @CampaignContactTitle				= 'Ms'
SET @CampaignContactFirstName			= 'Melissa'
SET @CampaignContactLastName			= 'Cote'
SET @CampaignContactTypeID				= 0
SET @CampaignContactDateChanged			= GETDATE()

CREATE TABLE #CAccountInfo(
	[Id] [int] NOT NULL,
	[Name] [varchar](50) NULL,
	[Country] [varchar](10) NOT NULL,
	[Lang] [varchar](10) NULL,
	[CAccountCodeClass] [varchar](10) NULL,
	[CAccountCodeGroup] [varchar](50) NULL,
	[PhoneListID] [int] NULL,
	[AddressListID] [int] NULL,
	[Address1] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[State] [char](2) NULL,
	[Zip] [varchar](12) NULL,
	[Zip4] [varchar](12) NULL,
	[County] [varchar](20) NULL,
	[StatusID] [int] NULL,
	[Enrollment] [int] NULL,
	[Comment] [varchar](1000) NULL,
	[EMail] [varchar](75) NULL,
	[IsPrivateOrg] [bit] NULL,
	[IsAdultGroup] [bit] NULL,
	[ParentID] [int] NULL,
	[SalesRegionID] [int] NULL,
	[StatementPrintCycleID] [int] NULL,
	[StatementPrintSlot] [int] NULL,
	[DateCreatedTOSSthis] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
	[UserIDModified] [int] NOT NULL,
	[VendorNumber] [varchar](30) NULL,
	[VendorSiteName] [varchar](15) NULL,
	[VendorPayGroup] [varchar](25) NULL,
	[OriginalAddress1] [varchar](50) NULL,
	[OriginalAddress2] [varchar](50) NULL,
	[OriginalCity] [varchar](50) NULL,
	[OriginalState] [char](2) NULL,
	[OriginalZip] [varchar](6) NULL,
	[OriginalZip4] [varchar](4) NULL,
	[ShipToAddress1] [varchar](50) NULL,
	[ShipToAddress2] [varchar](50) NULL,
	[ShipToCity] [varchar](50) NULL,
	[ShipToState] [char](2) NULL,
	[ShipToZip] [varchar](6) NULL,
	[ShipToZip4] [varchar](4) NULL,
	[Sponsor] [varchar](50) NULL,
	[BusinessUnit] [int] NULL,
	[PartnerID] [int] NULL)

CREATE TABLE #EFRTableInsertionScript (
		Script	varchar(1000))

INSERT INTO #EFRTableInsertionScript VALUES (
'DECLARE @store_template_id INT')


	SET	@sName = 'EFUNDRAISING - ' + CONVERT(VARCHAR(10), @PartnerID)

	INSERT INTO #CAccountInfo
	EXEC pr_CAccount_Insert 
		@sName = @sName,
		@sCountry = @sCountry2,
		@sLang = @sLang,
		@sCAccountCodeClass = @sCAccountCodeClass,
		@sCAccountCodeGroup = @sCAccountCodeGroup,
		@iPhoneListID = 0,
		@iAddressListID = 0,
		@sAddress1 = NULL,
		@sAddress2 = NULL,
		@sCity = NULL,
		@sState = NULL,
		@sZip = NULL,
		@sZip4 = NULL,
		@sCounty = NULL,
		@iStatusID = @iStatusID,
		@iEnrollment = @iEnrollment,
		@sComment = @sComment,
		@sEMail = @sEMail,
		@bIsPrivateOrg = @bIsPrivateOrg,
		@bIsAdultGroup = @bIsAdultGroup,
		@iParentID = @iParentID,
		@iSalesRegionID = @iSalesRegionID,
		@iStatementPrintCycleID = @iStatementPrintCycleID,
		@iStatementPrintSlot = @iStatementPrintSlot,
		@daDateCreatedTOSSthis = @daDateCreatedTOSSthis,
		@daDateUpdated = @daDateUpdated,
		@iUserIDModified = @iUserIDModified,
		@sVendorNumber = @sVendorNumber,
		@sVendorSiteName = @sVendorSiteName,
		@sVendorPayGroup = @sVendorPayGroup,
		@sOriginalAddress1 = @sOriginalAddress1,
		@sOriginalAddress2 = @sOriginalAddress2,
		@sOriginalCity = @sOriginalCity,
		@sOriginalState = @sOriginalState,
		@sOriginalZip = @sOriginalZip,
		@sOriginalZip4 = @sOriginalZip4,
		@sShipToAddress1 = @sShipToAddress1,
		@sShipToAddress2 = @sShipToAddress2,
		@sShipToCity = @sShipToCity,
		@sShipToState = @sShipToState,
		@sShipToZip = @sShipToZip,
		@sShipToZip4 = @sShipToZip4,
		@sSponsor = @sSponsor,
		@iBusinessUnitID = @iBusinessUnitID,
		@iPartnerID = @PartnerID
	
	SELECT	@iAddressListID = AddressListID FROM #CAccountInfo

	EXEC pr_Address_Insert 
		@sstreet1,
		@sstreet2,
		@scity,
		@sstateProvince,
		@spostal_code,
		@szip4,
		@scountry,
		54001,
		@iAddressListID,
		@iAddress_ID

	EXEC pr_Address_Insert 
		@sstreet1,
		@sstreet2,
		@scity,
		@sstateProvince,
		@spostal_code,
		@szip4,
		@scountry,
		54002,
		@iAddressListID,
		@iAddress_ID

	SELECT	@iPhoneListID = PhoneListID FROM #CAccountInfo

	EXEC pr_Phone_Insert 
		@iType,
		@iPhoneListID,
		@sPhoneNumber,
		@sBestTimeToCall,
		@iID2

	SELECT	@iCAccountID = ID FROM #CAccountInfo

	EXEC QSPCanadaOrderManagement..pr_populate_Account @MinAccountID = @iCAccountID, @MaxAccountID = @iCAccountID

	EXEC pr_Contact_Insert
		@iContactListID = 0,
		@iCAccountID = 0,
		@sTitle = @CampaignContactTitle,
		@sFirstName = @CampaignContactFirstName,
		@sLastName = @CampaignContactLastName,
		@sMiddleInitial = NULL,
		@iTypeId = @CampaignContactTypeID,
		@sFunction = NULL,
		@sEmail = NULL,
		@iAddressID = 1,
		@iPhoneListID = 1,
		@bDeletedTF = 0,
		@dDateChanged = @CampaignContactDateChanged,
		@iID = @iCampaignContactID OUTPUT

	EXEC pr_Campaign_Insert
		@iStatus,
		@bRenewal,
		@sCountry3,
		@sFMID,
		@sDateChanged,
		@sLang2,
		@daEndDate,
		@daStartDate,
		@iIncentivesBillToID,
		@iCAccountID,
		@iCampaignContactID,
		@iCAccountID,
		@dcEstimatedGross,
		@iNumberOfParticipants,
		@iNumberOfClassroooms,
		@iNumberOfStaff,
		@iCampaignContactID,
		@iSuppliesCampaignContactID,
		@iSuppliesShipToCampaignContactID,
		@daSuppliesDeliveryDate,
		@sSpecialInstructions,
		@bIsStaffOrder,
		@dcStaffOrderDiscount,
		@bIsTestCampaign,
		@daDateModified,
		@iUserIDModified2,
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

		INSERT INTO #EFRTableInsertionScript VALUES (
'SELECT	@store_template_id = MAX(Store_Template_ID) + 1
FROM	Store_Template

INSERT INTO Store_Template(
		Store_Template_ID,
		Culture_Code,
		Aggregator_ID,
		Account_Number,
		[Description],
		Create_Date,
		Subdivision_Code)
VALUES	(
		@Store_Template_ID,
		''en-CA'',
		7,
		' + CONVERT(VARCHAR(10), @iCAccountID) + ',
		' + CONVERT(VARCHAR(10), @PartnerID) + ',
		GETDATE(),
		''CA-' + @sstateProvince + '''
		)
INSERT INTO	Partner_Store_Template(
				Partner_ID,
				Store_Template_ID,
				Create_Date)
VALUES	(
		' + CONVERT(varchar(10), @PartnerID) + ',
		@Store_Template_ID,
		GETDATE()
		)')

SELECT	*
FROM	#EFRTableInsertionScript

DROP TABLE #CAccountInfo
DROP TABLE #EFRTableInsertionScript
GO
