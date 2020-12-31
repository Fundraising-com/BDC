USE [QSPCanadaFinance]
GO

--Details
SELECT		fm.FMID,
			fm.Firstname AS FMFirstName,
			fm.Lastname AS FMLastName,
			accFM.ID AS FMAccountID,
			campFM.ID AS FMCampaignID,
			acc.ID AS SchoolAccountID,
			acc.Name AS SchoolName,
			camp.ID AS SchoolCampaignID,
			b.OrderID,
			(invSec.NET_BEFORE_TAX - ISNULL(invSec.US_Postage_Amount, 0.00)) AS NetAmount,
			(invSec.NET_BEFORE_TAX - ISNULL(invSec.US_Postage_Amount, 0.00)) * 0.04 AS FourPercentOfNetAmount
INTO		#Details
FROM		QSPCanadaCommon..Campaign camp
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
				AND	acc.CAccountCodeClass NOT IN ('FM')
JOIN		QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = camp.FMID
JOIN		QSPCanadaOrderManagement..Batch b
				ON	b.CampaignID = camp.ID
JOIN		QSPCanadaFinance..Invoice inv
				ON	inv.Order_ID = b.OrderID
JOIN		QSPCanadaFinance..Invoice_Section invSec
				ON	invSec.Invoice_ID = inv.Invoice_ID

LEFT JOIN	(QSPCanadaCommon..Campaign campFM
JOIN		QSPCanadaCommon..CAccount accFM
				ON	accFM.ID = campFM.BillToAccountID
				AND	accFM.CAccountCodeGroup = 'Comm')
				
				ON	campFM.FMID = fm.FMID
				AND	campFM.[Status] = 37002

WHERE		invSec.Section_Type_ID IN (10)
AND			inv.Invoice_Effective_Date BETWEEN '2013-01-01' AND '2013-07-01'
ORDER BY	fm.FMID,
			accFM.ID,
			campFM.ID

--Aggregation
SELECT		d.FMID,
			d.FMFirstName,
			d.FMLastName,
			d.FMAccountID,
			d.FMCampaignID,
			SUM(d.NetAmount) AS NetAmount,
			SUM(d.FourPercentOfNetAmount) AS FourPercentOfNetAmount
INTO		#Aggregation
FROM		#Details d
GROUP BY	d.FMID,
			d.FMFirstName,
			d.FMLastName,
			d.FMAccountID,
			d.FMCampaignID
ORDER BY	d.FMID,
			d.FMAccountID,
			d.FMCampaignID

--Bad invoice fix
/*UPDATE	#Aggregation
SET		NetAmount = NetAmount + 13248.00,
		FourPercentOfNetAmount = FourPercentOfNetAmount + (13248.00 * 0.04)
WHERE	FMID = '0031'

UPDATE	#Aggregation
SET		NetAmount = NetAmount + 1680.00,
		FourPercentOfNetAmount = FourPercentOfNetAmount + (1680.00 * 0.04)
WHERE	FMID = '0007'*/

SELECT		*
FROM		#Details

SELECT		*
FROM		#Aggregation

--Chocolate FM sample orders totals
SELECT		fm.FMID,
			fm.FirstName,
			fm.LastName,
			SUM(inv.INVOICE_AMOUNT) GrossAmount
FROM		QSPCanadaCommon..Campaign camp
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
				AND	acc.CAccountCodeClass IN ('FM')
JOIN		QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = camp.FMID
JOIN		QSPCanadaOrderManagement..Batch b
				ON	b.CampaignID = camp.ID
JOIN		QSPCanadaFinance..Invoice inv
				ON	inv.Order_ID = b.OrderID
JOIN		QSPCanadaFinance..Invoice_Section invSec
				ON	invSec.Invoice_ID = inv.Invoice_ID
WHERE		invSec.Section_Type_ID IN (10)
AND			inv.Invoice_Effective_Date BETWEEN '2013-01-01' AND '2013-07-01'
GROUP BY	fm.FMID,
			fm.FirstName,
			fm.LastName
ORDER BY	fm.FMID

DROP TABLE #Aggregation
DROP TABLE #Details