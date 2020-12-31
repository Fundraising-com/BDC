USE [QSPCanadaCommon]
GO

--Todo: ensure OrderID 1118826 is included
SELECT		fm.FirstName + ' ' + fm.LastName FMName, campCurrent.ID CampaignID, campCurrent.BillToAccountID AccountID, acc.Name AccountName,
			campCurrent.StartDate CampaignStartDate, campCurrent.EndDate CampaignEndDate,
			SUM(ISNULL(invSec.Net_Before_Tax, 0.00) - ISNULL(invSec.US_Postage_Amount, 0.00)) NetSales,
			SUM(CASE WHEN invSec.SECTION_TYPE_ID IN (1,11) THEN ISNULL(invSec.Net_Before_Tax, 0.00) - ISNULL(invSec.US_Postage_Amount, 0.00) ELSE 0 END) GiftAndJewelryNetSales,
			SUM(CASE invSec.SECTION_TYPE_ID WHEN 2 THEN ISNULL(invSec.Net_Before_Tax, 0.00) - ISNULL(invSec.US_Postage_Amount, 0.00) ELSE 0 END) MagazineNetSales,
			SUM(CASE invSec.SECTION_TYPE_ID WHEN 9 THEN ISNULL(invSec.Net_Before_Tax, 0.00) - ISNULL(invSec.US_Postage_Amount, 0.00) ELSE 0 END) CookieDoughNetSales,
			SUM(CASE invSec.SECTION_TYPE_ID WHEN 15 THEN ISNULL(invSec.Net_Before_Tax, 0.00) - ISNULL(invSec.US_Postage_Amount, 0.00) ELSE 0 END) DiscountCardsNetSales
FROM		Campaign campCurrent WITH (NOLOCK)
JOIN		CAccount acc WITH (NOLOCK) ON acc.ID = campCurrent.BillToAccountID
JOIN		FieldManager fm WITH (NOLOCK) ON fm.FMID = campCurrent.FMID
LEFT JOIN	QSPCanadaOrderManagement..Batch b WITH (NOLOCK) ON b.CampaignID = campCurrent.ID
LEFT JOIN	QSPCanadaFinance..Invoice inv WITH (NOLOCK) ON inv.ORDER_ID = b.OrderID
LEFT JOIN	QSPCanadaFinance..Invoice_Section invSec WITH (NOLOCK) ON inv.Invoice_Id = invSec.Invoice_Id AND invSec.Section_Type_ID IN (1,2,9,10,11,13,14,15,16) -- 1. Gift, 2. Magazine, 9. Cookie Dough, 10. Chocolate, 11. Jewelry, 13. Candles, 14. TRT, 15. Entertainment 
WHERE		campCurrent.StartDate BETWEEN '2018-01-01' AND '2018-02-01'
AND			b.Date between '2018-01-01' and '2018-03-01' --
AND			campCurrent.BillToAccountID NOT IN
(
	SELECT		campLastYear.BillToAccountID
	FROM		Campaign campLastYear 
	JOIN		QSPCanadaOrderManagement..Batch b ON b.CampaignID = campLastYear.ID
	WHERE		campLastYear.Status = 37002
	AND			campLastYear.StartDate BETWEEN '2016-07-01' AND '2017-06-30'
	AND			b.OrderQualifierID IN (39009, 39001, 39002)
)
OR campcurrent.id = 108136
GROUP BY	campCurrent.ID,
			campCurrent.BillToAccountID,
			acc.Name,
			fm.FirstName + ' ' + fm.LastName,
			campCurrent.StartDate,
			campCurrent.EndDate,
			campCurrent.DateSubmitted					 
ORDER BY	fm.FirstName + ' ' + fm.LastName

