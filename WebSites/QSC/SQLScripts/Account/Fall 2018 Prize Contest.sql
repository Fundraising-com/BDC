USE QSPCanadaCommon
GO

SELECT		fm.FirstName FMFirstName, fm.LastName FMLastName, dm.Firstname DM, COUNT(DISTINCT camp.ID) NumberOfContracts, SUM(ISNULL(invSec.Net_Before_Tax, 0.00) - ISNULL(invSec.US_Postage_Amount, 0.00)) NetSales
FROM		Campaign camp
JOIN        CAccount acc ON acc.Id = camp.BillToAccountID
JOIN		FieldManager fm ON fm.FMID = camp.FMID
JOIN		FieldManager dm ON dm.FMID = fm.DMID
JOIN		QSPCanadaOrderManagement..Batch b WITH (NOLOCK) ON b.CampaignID = camp.ID
JOIN		QSPCanadaFinance..Invoice inv WITH (NOLOCK) ON inv.ORDER_ID = b.OrderID
JOIN		QSPCanadaFinance..Invoice_Section invSec WITH (NOLOCK) ON inv.Invoice_Id = invSec.Invoice_Id AND invSec.Section_Type_ID IN (1,2,9,10,11,13,14,15,16,17,18)
WHERE		camp.Status = 37002
AND			camp.StartDate BETWEEN '2018-07-01' AND '2018-12-31'
AND			camp.ApprovedStatusDate >= '2018-10-10'
AND			acc.CAccountCodeClass NOT IN ('FM')
GROUP BY	dm.Firstname, fm.FirstName, fm.LastName
