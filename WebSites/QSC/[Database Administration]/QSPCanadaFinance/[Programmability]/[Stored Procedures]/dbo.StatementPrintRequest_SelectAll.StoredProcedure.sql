USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[StatementPrintRequest_SelectAll]    Script Date: 06/07/2017 09:17:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[StatementPrintRequest_SelectAll]

	@FMID							VARCHAR(10) = NULL,
	@DMID							VARCHAR(10) = NULL,
	@CampaignID						INT = NULL,
	@AccountID						INT = NULL,
	@FiscalYear						INT = NULL,
	@StatementPrintRequestBatchID	INT = NULL,
	@NotifyFM						BIT = NULL,
	@StatementRunID					INT = NULL

AS

DECLARE	@RunDate	DATETIME
SET @RunDate = GETDATE()

DECLARE	@SeasonStartDate	DATETIME,
		@SeasonEndDate		DATETIME

IF @FiscalYear IS NOT NULL
BEGIN
	SELECT	@SeasonStartDate = seas.StartDate,
			@SeasonEndDate = seas.EndDate
	FROM	QSPCanadaCommon..Season seas
	WHERE	seas.FiscalYear = @FiscalYear
	AND		seas.Season IN ('Y')
END

CREATE TABLE #StatementPrintRequestData
(
	StatementPrintRequestID	INT,
	AccountID				INT,
	AccountName				VARCHAR(MAX),
	CampaignID				INT,
	StatementID				INT,
	StatementDate			DATETIME,
	Balance					NUMERIC(12, 2),
	FMID					VARCHAR(MAX),
	FMFirstName				VARCHAR(MAX),
	FMLastName				VARCHAR(MAX),
	DMID					VARCHAR(MAX),
	DMFirstName				VARCHAR(MAX),
	DMLastName				VARCHAR(MAX)
)

INSERT INTO #StatementPrintRequestData
(
	StatementPrintRequestID,
	AccountID,
	AccountName,
	CampaignID,
	StatementID,
	StatementDate,
	Balance,
	FMID,
	FMFirstName,
	FMLastName,
	DMID,
	DMFirstName,
	DMLastName
)
SELECT	spr.StatementPrintRequestID,
		acc.ID,
		acc.Name,
		camp.ID,
		stat.StatementID,
		stat.StatementDate,
		stat.Balance,
		fm.FMID,
		fm.FirstName,
		fm.LastName,
		dm.FMID,
		dm.FirstName,
		dm.LastName	
FROM	StatementPrintRequest spr
JOIN	[Statement] stat
			ON	stat.StatementID = spr.StatementID
JOIN	QSPCanadaCommon..Campaign camp
			ON	camp.ID = stat.CampaignID
JOIN	QSPCanadaCommon..FieldManager fm
			ON	fm.FMID = camp.FMID
JOIN	QSPCanadaCommon..FieldManager dm
			ON	dm.FMID = fm.DMID
JOIN	QSPCanadaCommon..CAccount acc
			ON	acc.ID = camp.BillToAccountID
WHERE	fm.FMID = ISNULL(@FMID, fm.FMID)
AND		dm.FMID = ISNULL(@DMID, dm.FMID)
AND		camp.ID = ISNULL(@CampaignID, camp.ID)
AND		acc.ID = ISNULL(@AccountID, acc.ID)
AND		camp.StartDate BETWEEN ISNULL(@SeasonStartDate, camp.StartDate) AND ISNULL(@SeasonEndDate, camp.EndDate)
AND		(spr.StatementPrintRequestBatchID = @StatementPrintRequestBatchID OR @StatementPrintRequestBatchID IS NULL)
AND		(ISNULL(@NotifyFM, 0) = 0 OR spr.FMNotificationDate IS NULL)
AND		(stat.StatementRunID = @StatementRunID OR @StatementRunID IS NULL)

IF @NotifyFM = 1
BEGIN
	UPDATE	spr
	SET		FMNotificationDate = @RunDate
	FROM	StatementPrintRequest spr
	JOIN	#StatementPrintRequestData sprd
				ON	sprd.StatementPrintRequestID = spr.StatementPrintRequestID
END

SELECT	StatementPrintRequestID,
		AccountID,
		AccountName,
		CampaignID,
		StatementID,
		StatementDate,
		Balance,
		FMID,
		FMFirstName,
		FMLastName,
		DMID,
		DMFirstName,
		DMLastName
FROM	#StatementPrintRequestData
GO
