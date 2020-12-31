USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Campaign_SelectSearchForBatchReports]    Script Date: 06/07/2017 09:33:13 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Campaign_SelectSearchForBatchReports]

	@iID				int		= 0,
	@iAccountID			int		= 0,
	@sFMID			varchar(4)	= '',
	@daStartDate			datetime	= '1995-01-01',
	@daEndDate			datetime	= '1995-01-01',
	@dApprovedStatusDateFrom	datetime	= '1995-01-01',
	@dApprovedStatusDateTo	datetime	= '1995-01-01',
	@bIncludeOnlineOnlyCampaigns bit = 1,
	@bIncludePopcornCampaigns bit = 1

AS

	DECLARE	@sqlStatement	nvarchar(4000)

	SET @sqlStatement = 'SELECT	c.[ID],
			c.[Status],
			c.[Renewal],
			c.[Country],
			c.[FMID],
			coalesce(c.[DateChanged], ''1995-01-01'') AS DateChanged,
			c.[Lang],
			coalesce(c.[EndDate], ''1995-01-01'') AS EndDate,
			coalesce(c.[StartDate], ''1995-01-01'') AS StartDate,
			c.[IncentivesBillToID],
			c.[BillToAccountID],
			c.[ShipToCampaignContactID],
			c.[ShipToAccountID],
			c.[EstimatedGross],
			c.[NumberOfParticipants],
			c.[NumberOfClassroooms],
			c.[NumberOfStaff],
			c.[BillToCampaignContactID],
			c.[SuppliesCampaignContactID],
			c.[SuppliesShipToCampaignContactID],
			c.[SuppliesDeliveryDate],
			c.[SpecialInstructions],
			c.[IsStaffOrder],
			c.[StaffOrderDiscount],
			c.[IsTestCampaign],
			c.[DateModified],
			c.[UserIDModified],
			c.[IsPayLater],
			c.[IncentivesDistributionID],
			c.[FSRequired],
			c.[FSExtraRequired],
			c.[FSOrderRecCreated],
			c.[ApprovedStatusDate],
			c.[MagnetStatementDate],
			c.[RewardsProgramCumulative],
			c.[RewardsProgramChart],
			c.[RewardsProgramDraw],
			c.[ContactName],
			c.[PhoneListID],
			c.[SuppliesAddressID],
			c.[DateSubmitted],
			acc.[Name]	
	FROM	Campaign c,
			Contact BillToContact,
			Contact ShipToContact,
			CAccount acc
	WHERE	BillToContact.ID = c.BillToCampaignContactID
	AND		ShipToContact.ID = c.ShipToCampaignContactID
	AND		acc.ID = c.ShipToAccountID
	AND		c.IsStaffOrder = 0
	AND		c.Status = 37002
	AND		EXISTS
			(SELECT	cp.CampaignID
			FROM		CampaignProgram cp
			WHERE	cp.CampaignID = c.ID
			AND		cp.DeletedTF = 0) '

	IF(@iID <> 0)
	BEGIN
		SET @sqlStatement = @sqlStatement + ' AND c.ID = ' + convert(nvarchar, @iID)
	END

	IF(@iAccountID <> 0)
	BEGIN
		SET @sqlStatement = @sqlStatement + ' AND c.ShipToAccountID = ' + convert(nvarchar, @iAccountID)
	END

	IF(@sFMID <> '')
	BEGIN
		SET @sqlStatement = @sqlStatement + ' AND c.FMID = ''' + @sFMID + ''' '
	END

	IF(@daStartDate <> '1995-01-01' AND @daEndDate <> '1995-01-01')
	BEGIN
		SET @sqlStatement = @sqlStatement + ' AND c.StartDate BETWEEN ''' + convert(nvarchar, @daStartDate) + ''' AND ''' + convert(nvarchar, @daEndDate) + ''' '
	END

	IF(@dApprovedStatusDateFrom <> '1995-01-01' AND @dApprovedStatusDateTo <> '1995-01-01')
	BEGIN
		SET @sqlStatement = @sqlStatement + ' AND c.ApprovedStatusDate BETWEEN ''' + convert(nvarchar, @dApprovedStatusDateFrom) + ''' AND ''' + convert(nvarchar, @dApprovedStatusDateTo) + ''' '
	END

	IF(@bIncludeOnlineOnlyCampaigns <> 1)
	BEGIN
		SET @sqlStatement = @sqlStatement + ' AND c.OnlineOnlyPrograms = 0'
	END

	IF(@bIncludePopcornCampaigns <> 1)
	BEGIN
		SET @sqlStatement = @sqlStatement + ' AND c.ID NOT IN (SELECT CampaignID FROM CampaignProgram WHERE ProgramID = 61 AND DeletedTF = 0)'
	END

	-- Add the order by
	SET @sqlStatement = @sqlStatement + ' ORDER BY acc.[Name]'


	EXEC(@sqlStatement)
GO