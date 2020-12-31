USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_ins_Campaign]    Script Date: 06/07/2017 09:33:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ins_Campaign]
	@CampaignID int OUT,
	@Status int,
--	@Country varchar(10),
	@FMID varchar(4),
	@DateChanged varchar(50),
	@Lang varchar(10),
	@StartDate datetime,
	@EndDate datetime,
	@IncentivesBillToID int,
	@BillToAccountID int,
--	@ShipToCampaignContactID int,
	@ShipToAccountID int,
	@EstimatedGross numeric(10,2),
	@NumberOfParticipants int,
	@NumberOfClassroooms int,
	@NumberOfStaff int,
	--@BillToCampaignContactID int,
	--@SuppliesCampaignContactID int,
	--@SuppliesShipToCampaignContactID int,
	--@SuppliesDeliveryDate datetime,
	@SpecialInstructions varchar(1000),
	@IsStaffOrder bit,
	@StaffOrderDiscount numeric(10,2),
	@IsTestCampaign bit,
	@DateModified datetime,
	@UserIDModified int,
	@IsPayLater bit,
	@IncentivesDistributionID int,
--	@FSOrderRecCreated bit,
--	@ApprovedStatusDate datetime,
	@MagnetStatementDate datetime,
--	@ProgramMagazine bit,
--	@ProgramMagazineExpress bit,
--	@ProgramMagnet bit,
--	@ProgramMagazineCombo bit,
--	@ProgramMagazineStaff bit,
--	@RewardsProgramCumulative bit,
--	@RewardsProgramChart bit,
--	@RewardsProgramDraw bit,
	@ContactName varchar(50),
	@ContactPhone varchar(50)

AS

DECLARE @Country varchar(10)
--if @Country IS NULL
--begin
--	SELECT @Country = Country FROM CAccount WHERE Id = @ShipToAccountID
--end
SELECT @Country = 'CA'

--insert the phone #
DECLARE @NewPhoneListID int				-----------------------------------------------
insert into PhoneList(CreateDate) values(GetDate())	--  Get a new PhoneListID   ---
SELECT @NewPhoneListID = @@Identity		-----------------------------------------------
/* Insert That Number */
DECLARE @Phone_ID int
EXEC pr_ins_Phone 	@Type			= 1, 
			@PhoneListID		= @NewPhoneListID, 
			@PhoneNumber		= @ContactPhone, 
			@BestTimeToCall	= null,
			@Phone_ID		= 0 

INSERT INTO Campaign (
	Status,
	Country,
	FMID,
	DateChanged,
	Lang,
	StartDate,
	EndDate,
	IncentivesBillToID,
	BillToAccountID,
--	ShipToCampaignContactID,
	ShipToAccountID,
	EstimatedGross,
	NumberOfParticipants,
	NumberOfClassroooms,
	NumberOfStaff,
--	BillToCampaignContactID,
--	SuppliesCampaignContactID,
--	SuppliesShipToCampaignContactID,
--	SuppliesDeliveryDate,
	SpecialInstructions,
	IsStaffOrder,
	StaffOrderDiscount,
	IsTestCampaign,
	DateModified,
	UserIDModified,
	IsPayLater,
	IncentivesDistributionID,
	FSOrderRecCreated,
	ApprovedStatusDate,
	MagnetStatementDate,
--	RewardsProgramCumulative,
--	RewardsProgramChart,
--	RewardsProgramDraw,
	ContactName,
	PhoneListID
)VALUES(
	@Status,
	@Country,
	@FMID,
	@DateChanged,
	@Lang,
	@StartDate,
	@EndDate,
	@IncentivesBillToID,
	@BillToAccountID,
--	@ShipToCampaignContactID,
	@ShipToAccountID,
	@EstimatedGross,
	@NumberOfParticipants,
	@NumberOfClassroooms,
	@NumberOfStaff,
--	@BillToCampaignContactID,
--	@SuppliesCampaignContactID,
--	@SuppliesShipToCampaignContactID,
--	@SuppliesDeliveryDate,
	@SpecialInstructions,
	@IsStaffOrder,
	@StaffOrderDiscount,
	@IsTestCampaign,
	@DateModified,
	@UserIDModified,
	@IsPayLater,
	@IncentivesDistributionID,
	0, --@FSOrderRecCreated, --The campaign MUST be created before Field Supplies can be generated, so this should always be false at insertion.
	getdate(), --@ApprovedStatusDate,
	@MagnetStatementDate,
--	@RewardsProgramCumulative,
--	@RewardsProgramChart,
--	@RewardsProgramDraw,
	@ContactName,
	@NewPhoneListID
);


SELECT @CampaignID = @@Identity

--insert program records 
/*
--insert , ProgramID, IsPreCollect, GroupProfit fields into CampaignProgram
INSERT INTO CampaignProgram
         SELECT @CampaignID AS CampaignID, 1 AS ProgramID, 'N' AS IsPreCollect, DefaultProfit AS GroupProfit 
            FROM Program 
         WHERE [ID] = 1 AND @ProgramMagazine = 1  --insert if Magazine Regular is true

INSERT INTO CampaignProgram
         SELECT @CampaignID AS CampaignID, 2 AS ProgramID, 'N' AS IsPreCollect, DefaultProfit AS GroupProfit 
            FROM Program 
         WHERE [ID] = 2 AND @ProgramMagazineExpress = 1  --insert if Magazine Express is true

INSERT INTO CampaignProgram
         SELECT @CampaignID AS CampaignID, 3 AS ProgramID, 'N' AS IsPreCollect, DefaultProfit AS GroupProfit 
            FROM Program 
         WHERE [ID] = 3 AND @ProgramMagnet = 1  --insert if Magnet is true

INSERT INTO CampaignProgram
         SELECT @CampaignID AS CampaignID, 13 AS ProgramID, 'N' AS IsPreCollect, DefaultProfit AS GroupProfit 
            FROM Program 
         WHERE [ID] = 13 AND @ProgramMagazineCombo = 1  --insert if MagCombo is true

INSERT INTO CampaignProgram
         SELECT @CampaignID AS CampaignID, 14 AS ProgramID, 'N' AS IsPreCollect, DefaultProfit AS GroupProfit 
            FROM Program 
         WHERE [ID] = 14 AND @ProgramMagazineStaff = 1  --insert if MagStaff  is true

---insert reward program records
INSERT INTO CampaignProgram
         SELECT @CampaignID AS CampaignID, 15 AS ProgramID, 'N' AS IsPreCollect, DefaultProfit AS GroupProfit 
            FROM Program 
         WHERE [ID] = 15 AND @RewardsProgramCumulative = 1  --insert if Cumulative Rewards  is true

INSERT INTO CampaignProgram
         SELECT @CampaignID AS CampaignID, 16 AS ProgramID, 'N' AS IsPreCollect, DefaultProfit AS GroupProfit 
            FROM Program 
         WHERE [ID] = 16 AND @RewardsProgramChart = 1  --insert if Chart Rewards is true

INSERT INTO CampaignProgram
         SELECT @CampaignID AS CampaignID, 9 AS ProgramID, 'N' AS IsPreCollect, DefaultProfit AS GroupProfit 
            FROM Program 
         WHERE [ID] = 9 AND @RewardsProgramDraw = 1  --insert if Draw Prizes is true
*/
GO
