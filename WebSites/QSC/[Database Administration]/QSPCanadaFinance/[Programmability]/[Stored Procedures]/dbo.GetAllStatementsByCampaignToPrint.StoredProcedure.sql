USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetAllStatementsByCampaignToPrint]    Script Date: 06/07/2017 09:17:14 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetAllStatementsByCampaignToPrint]

	@FiscalYear		INT = NULL,
	@Realtime		BIT,
	@CampaignID		INT = NULL,
	@AccountID		INT = NULL,
	@AccountName	VARCHAR(50) = NULL,
	@FMID			VARCHAR(10) = NULL,
	@FMLastName		VARCHAR(50) = NULL, 
	@StatementRunID	INT = NULL

AS

IF @AccountName = '' SET @AccountName = NULL
IF @FMID = '' SET @FMID = NULL
IF @FMLastName = '' SET @FMLastName = NULL

DECLARE @SeasonStartDate	DATETIME,
		@SeasonEndDate		DATETIME

IF ISNULL(@FiscalYear, 0) > 0
BEGIN
	SELECT	@SeasonStartDate = StartDate,
			@SeasonEndDate = EndDate
	FROM	QSPCanadaCommon..Season
	WHERE	FiscalYear = @FiscalYear
	AND		Season IN ('Y')
END

IF @Realtime = CONVERT(BIT, 1)
BEGIN
	SELECT		acc.ID AS AccountID,
				camp.ID as CampaignID,
				acc.Name,
				fm.FMID, 
				fm.LastName,
				fm.FirstName,
				camp.Lang, 
				null as StatementID, 
				null as StatementRunID, 
				getdate() as StatementDate
	FROM		QSPCanadaCommon..CAccount acc
	JOIN		QSPCanadaCommon..Campaign camp
					ON	camp.BillToAccountID = acc.ID
					AND	camp.StartDate BETWEEN ISNULL(@SeasonStartDate, '1900-01-01') AND ISNULL(@SeasonEndDate, '2100-01-01')
	JOIN		QSPCanadaCommon..FieldManager fm
					ON	fm.FMID = camp.FMID
	WHERE		camp.ID = ISNULL(@CampaignID, camp.ID)
	AND			acc.ID = ISNULL(@AccountID, acc.ID)
	AND			acc.Name = ISNULL(@AccountName, acc.Name)
	AND			(fm.FMID = ISNULL(@FMID, fm.FMID) OR @FMID = QSPCanadaCommon.dbo.UDF_Account_GetFMID(camp.BillToAccountID, GETDATE()))
	AND			fm.LastName = ISNULL(@FMLastname, fm.LastName)
	--AND			camp.ID IN (SELECT DISTINCT CampaignID FROM dbo.UDF_Statement_GetDetails(NULL, NULL, NULL) WHERE TransactionAmount <> 0.00)
END
ELSE
BEGIN
	SELECT		stat.AccountID,
				stat.CampaignID,
				stat.AccountName AS [Name],
				stat.FMID, 
				stat.FMLastName AS LastName,
				stat.FMFirstName AS FirstName,
				stat.Lang, 
				stat.StatementID, 
				stat.StatementRunID, 
				stat.StatementDate
	FROM		[Statement] stat
	JOIN		QSPCanadaCommon..Campaign camp
					ON	camp.ID = stat.CampaignID
					AND	camp.StartDate BETWEEN ISNULL(@SeasonStartDate, '1900-01-01') AND ISNULL(@SeasonEndDate, '2100-01-01')
	WHERE		(stat.StatementRunID = @StatementRunID OR @StatementRunID IS NULL)
	AND			stat.CampaignID = ISNULL(@CampaignID, stat.CampaignID)
	AND			stat.AccountID = ISNULL(@AccountID, stat.AccountID)
	AND			stat.AccountName = ISNULL(@AccountName, stat.AccountName)
	AND			(stat.FMID = ISNULL(@FMID, stat.FMID) OR @FMID = QSPCanadaCommon.dbo.UDF_Account_GetFMID(camp.BillToAccountID, GETDATE()))
	AND			stat.FMLastName = ISNULL(@FMLastname, stat.FMLastName)	
END
GO
