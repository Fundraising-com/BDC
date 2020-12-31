USE QSPCanadaFinance

SELECT		acc.Id AccountID,
			acc.Name AccountName,
			ad.stateProvince Province,
			SUM(CASE WHEN invSec.section_type_id = 9 AND camp.StartDate BETWEEN '2014-07-01' AND '2014-12-31' THEN invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00) ELSE 0.00 END) Fall2014CDNetAmount,
			SUM(CASE WHEN invSec.section_type_id = 9 AND camp.StartDate BETWEEN '2015-07-01' AND '2015-12-31' THEN invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00) ELSE 0.00 END) Fall2015CDNetAmount,
			SUM(CASE WHEN invSec.section_type_id = 9 AND camp.StartDate BETWEEN '2014-07-01' AND '2014-12-31' THEN invSec.Total_Tax_Excluded - ISNULL(invSec.US_Postage_Amount, 0.00) ELSE 0.00 END) Fall2014CDAdjustedGross,
			SUM(CASE WHEN invSec.section_type_id = 9 AND camp.StartDate BETWEEN '2015-07-01' AND '2015-12-31' THEN invSec.Total_Tax_Excluded - ISNULL(invSec.US_Postage_Amount, 0.00) ELSE 0.00 END) Fall2015CDAdjustedGross,
			SUM(CASE WHEN invSec.section_type_id = 9 AND camp.StartDate BETWEEN '2014-07-01' AND '2014-12-31' THEN invSec.ITEM_COUNT ELSE 0 END) Fall2014CDNumUnits,
			SUM(CASE WHEN invSec.section_type_id = 9 AND camp.StartDate BETWEEN '2015-07-01' AND '2015-12-31' THEN invSec.ITEM_COUNT ELSE 0 END) Fall2015CDNumUnits,
			SUM(CASE WHEN invSec.section_type_id = 2 AND camp.StartDate BETWEEN '2014-07-01' AND '2014-12-31' THEN invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00) ELSE 0.00 END) Fall2014MagNetAmount,
			SUM(CASE WHEN invSec.section_type_id = 2 AND camp.StartDate BETWEEN '2015-07-01' AND '2015-12-31' THEN invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00) ELSE 0.00 END) Fall2015MagNetAmount,
			SUM(CASE WHEN invSec.section_type_id = 2 AND camp.StartDate BETWEEN '2014-07-01' AND '2014-12-31' THEN invSec.Total_Tax_Excluded - ISNULL(invSec.US_Postage_Amount, 0.00) ELSE 0.00 END) Fall2014MagAdjustedGross,
			SUM(CASE WHEN invSec.section_type_id = 2 AND camp.StartDate BETWEEN '2015-07-01' AND '2015-12-31' THEN invSec.Total_Tax_Excluded - ISNULL(invSec.US_Postage_Amount, 0.00) ELSE 0.00 END) Fall2015MagAdjustedGross,
			SUM(CASE WHEN invSec.section_type_id = 2 AND camp.StartDate BETWEEN '2014-07-01' AND '2014-12-31' THEN invSec.ITEM_COUNT ELSE 0 END) Fall2014MagNumUnits,
			SUM(CASE WHEN invSec.section_type_id = 2 AND camp.StartDate BETWEEN '2015-07-01' AND '2015-12-31' THEN invSec.ITEM_COUNT ELSE 0 END) Fall2015MagNumUnits
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
WHERE		(camp.StartDate BETWEEN '2014-07-01' AND '2014-12-31' OR camp.StartDate BETWEEN '2015-07-01' AND '2015-12-31')
AND			invSec.section_type_id in (2, 9) --Mag, Cookie Dough
GROUP BY	acc.ID,
			acc.Name,
			ad.street1,
			ad.street2,
			ad.City,
			ad.stateProvince,
			ad.postal_code
HAVING		SUM(CASE WHEN invSec.section_type_id = 9 AND camp.StartDate BETWEEN '2014-07-01' AND '2014-12-31' THEN invSec.ITEM_COUNT ELSE 0.00 END) = 0
AND			SUM(CASE WHEN invSec.section_type_id = 9 AND camp.StartDate BETWEEN '2015-07-01' AND '2015-12-31' THEN invSec.ITEM_COUNT ELSE 0.00 END) > 0
AND			SUM(CASE WHEN invSec.section_type_id = 2 AND camp.StartDate BETWEEN '2015-07-01' AND '2015-12-31' THEN invSec.ITEM_COUNT ELSE 0.00 END) > 0
ORDER BY	acc.ID