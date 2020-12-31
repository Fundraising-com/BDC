USE QSPCanadaOrderManagement
GO

SELECT		TOP 10
			acc.ID AccountID,
			acc.Name AccountName,
			fm.FirstName + ' ' + fm.LastName FM,
			SUM(ISNULL(invSec.Total_Tax_Excluded, 0) - ISNULL(US_Postage_Amount, 0.00)) AdjustedGross,
			ad.street1 Address1,
			ad.street2 Address2,
			ad.City,
			ad.postal_code,
			ad.stateProvince Province
FROM		QSPCanadaFinance..Invoice inv
JOIN		QSPCanadaFinance..Invoice_Section invSec ON	invSec.INVOICE_ID = inv.INVOICE_ID
JOIN		QSPCanadaOrderManagement..Batch b ON b.OrderID = inv.ORDER_ID
JOIN		QSPCanadaCommon..Campaign camp ON camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..CAccount acc ON acc.Id = camp.BillToAccountID
JOIN		QSPCanadaCommon..FieldManager fm ON fm.FMID = camp.FMID
LEFT JOIN	QSPCanadaCommon..Address ad ON ad.AddressListID = acc.AddressListID AND ad.address_type = 54001
WHERE		camp.StartDate BETWEEN '2015-07-01' AND '2016-06-30'
AND			invSec.SECTION_TYPE_ID IN (1, 2, 9, 10, 11, 13, 14, 15)
AND			acc.BusinessUnitID = 1
GROUP BY	acc.ID,
			acc.Name,
			fm.FirstName + ' ' + fm.LastName,
			ad.street1,
			ad.street2,
			ad.City,
			ad.postal_code,
			ad.stateProvince
ORDER BY	SUM(invSec.Total_Tax_Excluded - ISNULL(US_Postage_Amount, 0.00)) DESC
