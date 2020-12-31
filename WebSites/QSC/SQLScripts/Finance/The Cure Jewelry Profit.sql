SELECT		inv.INVOICE_EFFECTIVE_DATE InvoiceDate, acc.ID AccountID, acc.Name AccountName, camp.ID CampaignID, b.OrderID, b.Date OrderDate, inv.INVOICE_ID InvoiceID,
			cdOQ.Description OrderSource, invSec.TOTAL_TAX_INCLUDED GrossAmount, invSec.ThirdParty_Profit_Amount TheCureProfitAmount
FROM		Invoice_Section invSec
JOIN		Invoice inv ON inv.Invoice_ID = invSec.Invoice_ID
JOIN		QSPCanadaOrderManagement..Batch b ON b.OrderID = inv.ORDER_ID
JOIN		QSPCanadaCommon..Campaign camp ON camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..CAccount acc ON acc.ID = camp.BillToAccountID
JOIN		QSPCanadaCommon..CodeDetail cdOQ ON cdOQ.Instance = b.OrderQualifierID
WHERE		SECTION_TYPE_ID = 11
AND			inv.INVOICE_EFFECTIVE_DATE BETWEEN '2018-07-01' AND '2018-10-01'
ORDER BY	inv.Invoice_ID
