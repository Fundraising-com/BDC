USE QSPCanadaFinance

SELECT		acc.Id AccountID,
			camp.ID CampaignID,
			acc.Name AccountName,
			ad.street1 Address1,
			ad.street2 Address2,
			ad.City,
			ad.stateProvince Province,
			ad.postal_code PostalCode,
			SUM(invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00)) CommissionableSalesAmount
FROM		Invoice inv
JOIN		INVOICE_SECTION invSec
				ON	invSec.INVOICE_ID = inv.INVOICE_ID
JOIN		QSPCanadaOrderManagement..Batch b
				ON	b.OrderID = inv.Order_ID
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
LEFT JOIN	QSPCanadaCommon..Address [ad]
				ON	[ad].AddressListID = acc.AddressListID
				AND	[ad].Address_Type = 54001 --Ship to
WHERE		camp.StartDate BETWEEN '2012-07-01' AND '2013-06-30'
AND			camp.FMID = '0079'
GROUP BY	acc.ID,
			camp.ID,
			acc.Name,
			ad.street1,
			ad.street2,
			ad.City,
			ad.stateProvince,
			ad.postal_code
ORDER BY	camp.ID