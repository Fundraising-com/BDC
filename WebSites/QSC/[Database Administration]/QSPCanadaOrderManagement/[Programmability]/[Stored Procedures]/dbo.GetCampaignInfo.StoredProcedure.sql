USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetCampaignInfo]    Script Date: 06/07/2017 09:19:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[GetCampaignInfo] 
	@CampaignID int
AS

----------------------------------------------------------------
---   get the programs for this campaign ---
----------------------------------------------------------------
DECLARE @Programs varchar(1000)
SELECT @Programs = ''

DECLARE @Current varchar(255) 
DECLARE ProgramsCursor CURSOR FOR 
select 
	--CP.ProgramID,
	P.[Name] AS ProgramName
  from 
	QSPCanadaCommon.dbo.CampaignProgram CP
	left join QSPCanadaCommon.dbo.Program P
		on CP.ProgramID = P.[ID]
 WHERE 
 	CP.CampaignID = @CampaignID
	AND DeletedTF <> 1


OPEN ProgramsCursor
FETCH NEXT FROM ProgramsCursor INTO @Current

WHILE @@FETCH_STATUS = 0
BEGIN
	SELECT @Programs = @Programs + @Current + ';'
	FETCH NEXT FROM ProgramsCursor INTO @Current
END
CLOSE ProgramsCursor
DEALLOCATE ProgramsCursor



DECLARE @FY_Start datetime
DECLARE @FY_End datetime
EXEC [QSPCanadaCommon].[dbo].[pr_get_FYdates] @FY_Start OUTPUT , @FY_End OUTPUT 
--subtracting one year for leftover FY2005 items handled in 2006

IF getdate() < '2005-09-01'
BEGIN
	SET @FY_Start  = '2004-07-01'
	SET @FY_End    = '2005-06-30'
END
/*
 * Get the campaign information .
 * A problem arises if there is more than one contact
 * Or more than one non fax phone number per contact.
 * Multiple records will be returned
 ****************************************************/
select top 1
	BillToAccountID       = BillAcct.[Id],
	BillAcct.[Name], 
	ShipToAccountID    = ShipAcct.[Id],
	Camp.[FMID],
	FMFirstName           = FM.FirstName,
	FMLastName           = FM.LastName,
	ContactFirstName    = Cont.[FirstName],
	ContactLastName    = Cont.[LastName],
	ContactEmail            = Cont.[Email],
	ContactPhone          = PH.[PhoneNumber],
--	ContactPhoneType  = PHtype.[description] ,
	ContactFax               = PHF.[PhoneNumber],
	CampaignPrograms   = @Programs,
	Camp.[EstimatedGross],
	Camp.IsStaffOrder
 from 
	QSPCanadaCommon.dbo.Campaign Camp

	LEFT JOIN QSPCanadaCommon.dbo.FieldManager FM
		ON Camp.[FMID] = FM.[FMID]

	LEFT JOIN QSPCanadaCommon.dbo.CAccount     BillAcct
		ON Camp.[BillToAccountID] = BillAcct.[Id]

	/*
	LEFT JOIN QSPCanadaCommon.dbo.Contact      Cont
		ON BillAcct.[Id] = Cont.[CAccountID]
	*/
	LEFT JOIN QSPCanadaCommon.dbo.Contact      Cont
		ON Camp.[ShipToCampaignContactID] = Cont.[Id]

	LEFT JOIN QSPCanadaCommon.dbo.Phone        PH
		ON Cont.[PhoneListID] = PH.[PhoneListID] 
		AND PH.[Type] <> 30503 --not fax
		and PH.Type is not null

	LEFT JOIN QSPCanadaCommon.dbo.PhoneType        PHtype
		ON PH.[Type] = PHtype.[PhoneTypeID]
	
	LEFT JOIN QSPCanadaCommon.dbo.Phone        PHF
		ON Cont.[PhoneListID] = PHF.[PhoneListID] 
		AND PHF.[Type] = 30503 --fax

	LEFT JOIN QSPCanadaCommon.dbo.CAccount     ShipAcct
		ON Camp.[ShipToAccountID] = ShipAcct.[Id]

where 
	Camp.[ID]     = @CampaignID
--	AND Camp.StartDate BETWEEN @FY_Start AND @FY_End
GO
