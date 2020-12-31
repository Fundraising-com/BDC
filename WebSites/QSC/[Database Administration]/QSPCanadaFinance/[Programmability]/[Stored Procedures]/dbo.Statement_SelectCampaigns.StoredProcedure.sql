USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[Statement_SelectCampaigns]    Script Date: 06/07/2017 09:17:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Statement_SelectCampaigns]

	@StatementRunID	INT

AS

DECLARE	@StatementRunClosed		BIT,
		@DateTo					DATETIME,
		@StatementsInOwingOnly	BIT

SELECT	@StatementRunClosed = StatementRunClosed,
		@StatementsInOwingOnly = StatementsInOwingOnly
FROM	StatementRun
WHERE	StatementRunID = @StatementRunID

SET @DateTo = CONVERT(VARCHAR(10), GETDATE(), 120) --Transactions until midnight last night

CREATE TABLE #CampaignBalance
(
	CampaignID		INT,
	CampaignBalance	NUMERIC(12, 2)
)

INSERT		#CampaignBalance
SELECT		sdCamp.CampaignID,
			ISNULL(SUM(sdCamp.TransactionAmount), 0)
FROM		dbo.UDF_Statement_GetDetails_WithBusLogic(@DateTo) sdCamp
GROUP BY	sdCamp.CampaignID

CREATE TABLE #CampaignWithNewTransaction
(
	CampaignID		INT
)

INSERT		#CampaignWithNewTransaction
SELECT		sdCamp.CampaignID
FROM		dbo.UDF_Statement_GetDetails_WithBusLogic(@DateTo) sdCamp
WHERE		ISNULL(sdCamp.TransactionDate, '1900-01-02') >	ISNULL(	(SELECT		TOP 1
																				StatementDate
																	FROM		[Statement] stat
																	WHERE		stat.CampaignID = sdCamp.CampaignID
																	ORDER BY	StatementDate DESC), '1900-01-01')
GROUP BY	sdCamp.CampaignID


SELECT		DISTINCT
			camp.ID AS CampaignID,
			CONVERT(VARCHAR(MAX), ISNULL(campBal.CampaignBalance, 0)) AS CampaignBalance,
			CASE	WHEN camp.OnlineOnlyPrograms = 1 OR camp.IsStaffOrder = 1 THEN	CONVERT(BIT, 1)
					ELSE															CONVERT(BIT, 0)
			END AS IsCampaignOnlineOnly,
			CASE WHEN camp.ID IN (	SELECT	b.CampaignID
									FROM	QSPCanadaOrderManagement..Batch b
									JOIN	Invoice inv
												ON	inv.Order_ID = b.OrderID
									WHERE	b.OrderQualifierID IN (39001) --39001: Main Order
									AND		b.Date < @DateTo)
				THEN CONVERT(BIT, 1)
				ELSE CONVERT(BIT, 0)
			END AS MainOrderInvoiced,
			acc.BusinessUnitID,
			ISNULL(camp.ForceStatementPrint, 0) AS ForceStatementPrintRequest,
			@DateTo AS DateTo
FROM		QSPCanadaCommon..Campaign camp
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
JOIN		#CampaignBalance campBal
				ON	campBal.CampaignID = camp.ID
WHERE		(@StatementRunClosed = 0 --Only generate statements if latest statement run not closed
AND			camp.StartDate >= '2009-07-01' --Start of statement automation
AND			acc.CAccountCodeClass NOT IN ('FM')
AND			(campBal.CampaignBalance <> 0.00 OR camp.ID IN (SELECT CampaignID FROM #CampaignWithNewTransaction)) --If $0 balance must have a new transaction since last statement
AND			(@StatementsInOwingOnly = 0 OR campBal.CampaignBalance >= 0.00)
AND			camp.ID NOT	IN (SELECT CampaignID FROM [Statement] stat JOIN StatementPrintRequest spr ON spr.StatementID = stat.StatementID WHERE spr.CreationDate >= DATEADD(dd, -25, GETDATE())) --Don't send a statement if already sent in last 25 days
AND			ISNULL(camp.DisableStatementPrint, 0) <> 1)
OR			camp.ForceStatementPrint = 1 --Manual override
ORDER BY	campBal.CampaignBalance, --So cheques will be created in order of amount
			camp.ID
GO
