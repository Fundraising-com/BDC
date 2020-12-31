USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_upd_Campaign]    Script Date: 06/07/2017 09:33:31 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_upd_Campaign]
	@CampaignID int,
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
--	@BillToCampaignContactID int,
--	@SuppliesCampaignContactID int,
--	@SuppliesShipToCampaignContactID int,
	@SuppliesDeliveryDate datetime,
	@SpecialInstructions varchar(1000),
	@IsStaffOrder bit,
	@StaffOrderDiscount numeric(10,2),
	@IsTestCampaign bit,
	@DateModified datetime,
	@UserIDModified int,
	@IsPayLater bit,
	@IncentivesDistributionID int,
	@FSOrderRecCreated bit,
--	@ApprovedStatusDate datetime,
	@MagnetStatementDate datetime,
	@ProgramMagazine bit,
	@ProgramMagazineExpress bit,
	@ProgramMagnet bit,
	@ProgramMagazineCombo bit,
	@ProgramMagazineStaff bit,
	@RewardsProgramCumulative bit,
	@RewardsProgramChart bit,
	@RewardsProgramDraw bit,
	@ContactName varchar(50),
	@ContactPhone varchar(50)
AS
/***********************************************
 **    Update The Campaign Record     **
 ***********************************************/
UPDATE 
	Campaign
SET 
	Status					= @Status,
--	Country					= @Country,
	FMID					= @FMID,
	DateChanged				= @DateChanged,
	Lang					= @Lang,
	StartDate				= @StartDate,
	EndDate				= @EndDate,
	IncentivesBillToID			= @IncentivesBillToID,
	BillToAccountID				= @BillToAccountID,
--	ShipToCampaignContactID		= @ShipToCampaignContactID,
	ShipToAccountID			= @ShipToAccountID,
	EstimatedGross				= @EstimatedGross,
	NumberOfParticipants			= @NumberOfParticipants,
	NumberOfClassroooms			= @NumberOfClassroooms,
	NumberOfStaff				= @NumberOfStaff,
--	BillToCampaignContactID		= @BillToCampaignContactID,
--	SuppliesCampaignContactID		= @SuppliesCampaignContactID,
--	SuppliesShipToCampaignContactID	= @SuppliesShipToCampaignContactID,
	SuppliesDeliveryDate			= @SuppliesDeliveryDate,
	SpecialInstructions			= @SpecialInstructions,
	IsStaffOrder				= @IsStaffOrder,
	StaffOrderDiscount			= @StaffOrderDiscount,
	IsTestCampaign				= @IsTestCampaign,
	DateModified				= @DateModified,
	UserIDModified				= @UserIDModified,
	IsPayLater				= @IsPayLater,
	IncentivesDistributionID			= @IncentivesDistributionID,
	FSOrderRecCreated			= @FSOrderRecCreated,
--	ApprovedStatusDate			= @ApprovedStatusDate,
	MagnetStatementDate			= @MagnetStatementDate,
--	RewardsProgramCumulative		= @RewardsProgramCumulative,	----Are there rules about
--	RewardsProgramChart			= @RewardsProgramChart,	----updating these fields after a campaign has begun ? 
--	RewardsProgramDraw			= @RewardsProgramDraw,	----Hmmmmmmmmmmmmmm..................................
	ContactName				= @ContactName
WHERE 
	[ID] = @CampaignID

/***********************************************
 **    Update The associated phone #  **
 ***********************************************/
UPDATE
	Phone
SET
	PhoneNumber = @ContactPhone
FROM
	Campaign
WHERE
	Campaign.PhoneListID = Phone.PhoneListID 
	AND Phone.Type = 1
	AND Campaign.[ID] = @CampaignID


/*****************************************************
 **   insert or update the attached programs   **
 ******************************************************/
DECLARE @RC int
DECLARE @BadPrograms varchar(300)
SELECT @BadPrograms = ''

exec @rc = pr_upd_CampaignProgram @CampaignID, 1, @ProgramMagazine
IF (@rc = -5) 
begin
	SELECT @badPrograms = @badPrograms + ', ' + 'Magazine';
end

exec @rc = pr_upd_CampaignProgram @CampaignID, 2, @ProgramMagazineExpress
IF (@rc = -5) 
begin
	SELECT @badPrograms = @badPrograms + ', ' + 'Magazine Express';
end

exec @rc = pr_upd_CampaignProgram @CampaignID, 3, @ProgramMagnet
IF (@rc = -5) 
begin
	SELECT @badPrograms = @badPrograms + ', ' + 'Magnet';
end

exec @rc = pr_upd_CampaignProgram @CampaignID, 13, @ProgramMagazineCombo
IF (@rc = -5) 
begin
	SELECT @badPrograms = @badPrograms + ', ' + 'Magazine Combo';
end

exec @rc = pr_upd_CampaignProgram @CampaignID, 14, @ProgramMagazineStaff
IF (@rc = -5) 
begin
	SELECT @badPrograms = @badPrograms + ', ' + 'Staff';
end


/*****************************************************
 **   insert or update the reward   programs   **
 ******************************************************/
exec @rc = pr_upd_CampaignProgram @CampaignID, 15, @RewardsProgramCumulative
IF (@rc = -5) 
begin
	SELECT @badPrograms = @badPrograms + ', ' + 'Cumulative Rewards';
end

exec @rc = pr_upd_CampaignProgram @CampaignID, 16, @RewardsProgramChart
IF (@rc = -5) 
begin
	SELECT @badPrograms = @badPrograms + ', ' + 'Chart Rewards';
end

exec @rc = pr_upd_CampaignProgram @CampaignID, 9, @RewardsProgramDraw
IF (@rc = -5) 
begin
	SELECT @badPrograms = @badPrograms + ', ' + 'Draw Prizes';
end


/******************
 **   Finish up  **
 ******************/
IF len(@BadPrograms) > 2 
begin
	SELECT substring(@BadPrograms, 3, len(@BadPrograms)- 2) AS ReturnMessage
end
ELSE
begin
	SELECT '' AS ReturnMessage
end
GO
