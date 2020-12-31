
SELECT		campCurrent.ID CampaignID, campCurrent.BillToAccountID AccountID, acc.Name AccountName, fm.FirstName + ' ' + fm.LastName FMName,
			   campCurrent.StartDate NewCampaignStartDate, campCurrent.EndDate NewCampaignEndDate,
			   SUM(ISNULL(invSec.Net_Before_Tax, 0.00) - ISNULL(invSec.US_Postage_Amount, 0.00)) NetSales
FROM		   Campaign campCurrent WITH (NOLOCK)
JOIN        CAccount acc WITH (NOLOCK) ON acc.Id = campCurrent.BillToAccountID
JOIN		   FieldManager fm WITH (NOLOCK) ON fm.FMID = campCurrent.FMID
LEFT JOIN	QSPCanadaOrderManagement..Batch b WITH (NOLOCK) ON b.CampaignID = campCurrent.ID
LEFT JOIN	QSPCanadaFinance..Invoice inv WITH (NOLOCK) ON inv.ORDER_ID = b.OrderID
LEFT JOIN	QSPCanadaFinance..Invoice_Section invSec WITH (NOLOCK) ON inv.Invoice_Id = invSec.Invoice_Id AND invSec.Section_Type_ID IN (2) -- 1. Gift, 2. Magazine, 9. Cookie Dough, 10. Chocolate, 11. Jewelry, 13. Candles, 14. TRT, 15. Entertainment 
WHERE		   campCurrent.StartDate BETWEEN '2017-07-01' AND '2017-12-31'
AND			campCurrent.Status = 37002
--AND         ISNULL(acc.ParentID, 0) NOT IN (34838) -- BDC Referred Account
AND			campCurrent.ID NOT IN
(
	SELECT	campCurrent.ID
	FROM		Campaign campCurrent WITH (NOLOCK)
	JOIN		CampaignProgram cpCurrent WITH (NOLOCK) ON cpCurrent.CampaignID = campCurrent.ID AND cpCurrent.DeletedTF = 0
	JOIN		FieldManager fm WITH (NOLOCK) ON fm.FMID = campCurrent.FMID
	JOIN		Campaign campLastYear WITH (NOLOCK) ON campLastYear.BillToAccountID = campCurrent.BillToAccountID AND campLastYear.Status = 37002 AND campLastYear.StartDate BETWEEN '2016-07-01' AND '2017-06-30'
	--JOIN		CampaignProgram cpLastYear WITH (NOLOCK) ON cpLastYear.CampaignID = campLastYear.ID AND cpLastYear.DeletedTF = 0
	JOIN		CampaignProgram cpLastYear WITH (NOLOCK) ON cpLastYear.CampaignID = campLastYear.ID AND cpLastYear.DeletedTF = 0 AND cpLastYear.ProgramID IN (1, 2)
	WHERE		campCurrent.StartDate BETWEEN '2017-07-01' AND '2017-12-31'
	AND		campCurrent.Status = 37002
)
GROUP BY	   campCurrent.ID,
			   campCurrent.BillToAccountID,
            acc.Name,
			   fm.FirstName + ' ' + fm.LastName,
			   campCurrent.StartDate,
			   campCurrent.EndDate,
			   campCurrent.DateSubmitted					 
HAVING		SUM(ISNULL(invSec.Net_Before_Tax, 0.00) - ISNULL(invSec.US_Postage_Amount, 0.00)) > 2000
ORDER BY	fm.FirstName + ' ' + fm.LastName
