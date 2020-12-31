USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Campaign_SelectNewAccountsContest]    Script Date: 06/07/2017 09:33:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Campaign_SelectNewAccountsContest]

AS

SELECT		campCurrent.ID CampaignID, campCurrent.BillToAccountID AccountID, fm.FirstName + ' ' + fm.LastName FMName,
			campCurrent.StartDate NewCampaignStartDate, campCurrent.EndDate NewCampaignEndDate, campCurrent.DateSubmitted NewCampaignCreationDate,
			SUM(ISNULL(invSec.Net_Before_Tax, 0.00) - ISNULL(invSec.US_Postage_Amount, 0.00)) NetSales
FROM		Campaign campCurrent WITH (NOLOCK)
JOIN		FieldManager fm WITH (NOLOCK) ON fm.FMID = campCurrent.FMID
LEFT JOIN	QSPCanadaOrderManagement..Batch b WITH (NOLOCK) ON b.CampaignID = campCurrent.ID
LEFT JOIN	QSPCanadaFinance..Invoice inv WITH (NOLOCK) ON inv.ORDER_ID = b.OrderID
LEFT JOIN	QSPCanadaFinance..Invoice_Section invSec WITH (NOLOCK) ON inv.Invoice_Id = invSec.Invoice_Id AND invSec.Section_Type_ID IN (1,2,9,10,11,13,14,15) -- 1. Gift, 2. Magazine, 9. Cookie Dough, 10. Chocolate, 11. Jewelry, 13. Candles, 14. TRT, 15. Entertainment 
WHERE		campCurrent.DateSubmitted BETWEEN '2015-10-12' AND '2015-11-30'
AND			campCurrent.StartDate BETWEEN '2015-07-01' AND '2015-12-31'
AND			campCurrent.Status = 37002
AND			campCurrent.ID NOT IN
(
	SELECT		campCurrent.ID
	FROM		Campaign campCurrent WITH (NOLOCK)
	JOIN		CampaignProgram cpCurrent WITH (NOLOCK) ON cpCurrent.CampaignID = campCurrent.ID AND cpCurrent.DeletedTF = 0
	JOIN		FieldManager fm WITH (NOLOCK) ON fm.FMID = campCurrent.FMID
	JOIN		Campaign campLastYear WITH (NOLOCK) ON campLastYear.BillToAccountID = campCurrent.BillToAccountID AND campLastYear.Status = 37002 AND campLastYear.StartDate BETWEEN '2014-07-01' AND '2014-12-31'
	--JOIN		CampaignProgram cpLastYear WITH (NOLOCK) ON cpLastYear.CampaignID = campLastYear.ID AND cpLastYear.DeletedTF = 0
	JOIN		CampaignProgram cpLastYear WITH (NOLOCK) ON cpLastYear.CampaignID = campLastYear.ID AND cpLastYear.DeletedTF = 0 AND (cpLastYear.ProgramID = cpCurrent.ProgramID OR (cpLastYear.ProgramID=1 and cpCurrent.ProgramID=2 or cpLastYear.ProgramID=2 and cpCurrent.ProgramID=1)) AND cpLastYear.ProgramID IN (1, 2, 44, 50, 52, 53, 54)
	WHERE		campCurrent.DateSubmitted BETWEEN '2015-10-12' AND '2015-11-30'
	AND			campCurrent.StartDate BETWEEN '2015-07-01' AND '2015-12-31'
	AND			campCurrent.Status = 37002
)
GROUP BY	campCurrent.ID,
			campCurrent.BillToAccountID,
			fm.FirstName + ' ' + fm.LastName,
			campCurrent.StartDate,
			campCurrent.EndDate,
			campCurrent.DateSubmitted					 
ORDER BY	fm.FirstName + ' ' + fm.LastName
GO
