USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[Statement_SelectMissingDueToMissingOrder]    Script Date: 06/07/2017 09:17:30 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[Statement_SelectMissingDueToMissingOrder]

AS

DECLARE @CampaignStartDate DATETIME,
		@CampaignEndDate DATETIME

SET @CampaignStartDate = '2014-07-01'
SET @CampaignEndDate = DATEADD(mm, -1, GETDATE())

SELECT		camp.FMID, fm.FirstName + ' ' + fm.LastName FMName, camp.ID CampaignID, camp.StartDate, camp.EndDate, acc.Id AccountID, acc.Name AccountName,
			CASE ISNULL(ref.Refund_ID, 0) WHEN 0 THEN 'No' ELSE 'Yes' END WasProfitChequeForced, ISNULL(SUM(sdCamp.TransactionAmount) * -1, 0) CampaignCreditBalance,
			QSPCanadaOrderManagement.dbo.UDF_ProgramsbyCampaign(camp.ID) Programs
FROM		QSPCanadaCommon..Campaign camp
JOIN		QSPCanadaCommon..FieldManager fm ON fm.FMID = camp.FMID
JOIN		QSPCanadaCommon..CAccount acc ON acc.Id = camp.BillToAccountID
LEFT JOIN	QSPCanadaOrderManagement..Batch bMain ON bMain.CampaignID = camp.ID AND bMain.OrderQualifierID IN (39001)
LEFT JOIN	QSPCanadaFinance.dbo.UDF_Statement_GetDetails_WithBusLogic('2099-12-31') sdCamp ON sdCamp.CampaignID = camp.ID
LEFT JOIN	QSPCanadaFinance..Refund ref ON ref.Campaign_ID = camp.ID
WHERE		ISNULL(camp.OnlineOnlyPrograms, 0) = 0
AND			camp.StartDate >= @CampaignStartDate
AND			camp.EndDate <= @CampaignEndDate
AND			camp.Status = 37002
AND			camp.FMID NOT IN ('0508', '0506')
AND			bMain.OrderID IS NULL
AND			acc.CAccountCodeGroup <> 'Comm'
GROUP BY	camp.FMID, fm.FirstName + ' ' + fm.LastName, camp.ID, camp.StartDate, camp.EndDate, acc.Id, acc.Name, CASE ISNULL(ref.Refund_ID, 0) WHEN 0 THEN 'No' ELSE 'Yes' END
HAVING		ISNULL(SUM(sdCamp.TransactionAmount) * -1, 0) > 5.00
ORDER BY	ISNULL(SUM(sdCamp.TransactionAmount), 0)
GO
