--select * from qspcanadacommon..FieldManager
--where LastName like 'marl%'

SELECT		fm.FirstName FMFirstName, fm.LastName FMLastName, accFM.Id FMAccountID, campFM.ID FMCampaignID, b.OrderID, b.Date OrderDate, cdOrderQualifier.Description OrderQualifier, cdOrderStatus.Description OrderStatus,
			inv.INVOICE_ID InvoiceID, inv.INVOICE_AMOUNT InvoiceAmount
FROM		Batch b
JOIN		QSPCanadaCommon..Campaign campFM ON campFM.ID = b.CampaignID
JOIN		QSPCanadaCommon..CAccount accFM
				ON	accFM.ID = campFM.BillToAccountID
				AND	accFM.CAccountCodeClass = 'FM' --accFM.CAccountCodeGroup = 'Comm'
JOIN		QSPCanadaFinance..Invoice inv ON inv.ORDER_ID = b.OrderID
JOIN		QSPCanadaCommon..FieldManager fm ON fm.FMID = campFM.FMID
JOIN		QSPCanadaCommon..CodeDetail cdOrderQualifier on cdOrderQualifier.Instance = b.OrderQualifierID
JOIN		QSPCanadaCommon..CodeDetail cdOrderStatus on cdOrderStatus.Instance = b.StatusInstance
WHERE		campFM.[Status] = 37002
AND			b.[Date] BETWEEN '2013-07-01' AND '2014-07-27'
AND			fm.FMID IN ('1543','0003','0095','0046','1542','0005','1544','0074','0060','1545')
AND			inv.INVOICE_AMOUNT > 0.00
AND			b.OrderQualifierID <> 39015
ORDER BY	campFM.FMID


