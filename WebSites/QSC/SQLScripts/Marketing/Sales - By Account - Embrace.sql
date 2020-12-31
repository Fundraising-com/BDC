USE QSPCanadaFinance

SELECT		acc.Id AccountID,
			acc.Name AccountName,
			ad.stateProvince Province,
			SUM(CASE WHEN DATEPART(yy, inv.INVOICE_DATE) = 2014 AND invSec.SECTION_TYPE_ID IN (1,11,13) THEN (invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00)) ELSE 0.00 END) Fall2014NetGiftJewelryCandleSales,
			SUM(CASE WHEN DATEPART(yy, inv.INVOICE_DATE) = 2015 AND invSec.SECTION_TYPE_ID IN (1,11,13) THEN (invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00)) ELSE 0.00 END) Fall2015NetGiftJewelryCandleSales,
			SUM(CASE WHEN DATEPART(yy, inv.INVOICE_DATE) = 2014 AND invSec.SECTION_TYPE_ID IN (1,11,13) THEN (invSec.ITEM_COUNT) ELSE 0.00 END) Fall2014GiftUnits,
			SUM(CASE WHEN DATEPART(yy, inv.INVOICE_DATE) = 2015 AND invSec.SECTION_TYPE_ID IN (1,11,13) THEN (invSec.ITEM_COUNT) ELSE 0.00 END) Fall2015GiftUnits,

			SUM(CASE WHEN DATEPART(yy, inv.INVOICE_DATE) = 2014 AND invSec.SECTION_TYPE_ID = 2 THEN (invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00)) ELSE 0.00 END) Fall2014NetMagSales,
			SUM(CASE WHEN DATEPART(yy, inv.INVOICE_DATE) = 2015 AND invSec.SECTION_TYPE_ID = 2 THEN (invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00)) ELSE 0.00 END) Fall2015NetMagSales,
			SUM(CASE WHEN DATEPART(yy, inv.INVOICE_DATE) = 2014 AND invSec.SECTION_TYPE_ID = 2 THEN (invSec.ITEM_COUNT) ELSE 0.00 END) Fall2014MagUnits,
			SUM(CASE WHEN DATEPART(yy, inv.INVOICE_DATE) = 2015 AND invSec.SECTION_TYPE_ID = 2 THEN (invSec.ITEM_COUNT) ELSE 0.00 END) Fall2015MagUnits,

			SUM(CASE WHEN DATEPART(yy, inv.INVOICE_DATE) = 2014 AND invSec.SECTION_TYPE_ID = 9 THEN (invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00)) ELSE 0.00 END) Fall2014NetCookieDoughSales,
			SUM(CASE WHEN DATEPART(yy, inv.INVOICE_DATE) = 2015 AND invSec.SECTION_TYPE_ID = 9 THEN (invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00)) ELSE 0.00 END) Fall2015NetCookieDoughSales,
			SUM(CASE WHEN DATEPART(yy, inv.INVOICE_DATE) = 2014 AND invSec.SECTION_TYPE_ID = 9 THEN (invSec.ITEM_COUNT) ELSE 0.00 END) Fall2014CookieDoughUnits,
			SUM(CASE WHEN DATEPART(yy, inv.INVOICE_DATE) = 2015 AND invSec.SECTION_TYPE_ID = 9 THEN (invSec.ITEM_COUNT) ELSE 0.00 END) Fall2015CookieDoughUnits,

			SUM(CASE WHEN DATEPART(yy, inv.INVOICE_DATE) = 2014 AND invSec.SECTION_TYPE_ID = 14 THEN (invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00)) ELSE 0.00 END) Fall2014NetTRTSales,
			SUM(CASE WHEN DATEPART(yy, inv.INVOICE_DATE) = 2015 AND invSec.SECTION_TYPE_ID = 14 THEN (invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00)) ELSE 0.00 END) Fall2015NetTRTSales,
			SUM(CASE WHEN DATEPART(yy, inv.INVOICE_DATE) = 2014 AND invSec.SECTION_TYPE_ID = 14 THEN (invSec.ITEM_COUNT) ELSE 0.00 END) Fall2014TRTUnits,
			SUM(CASE WHEN DATEPART(yy, inv.INVOICE_DATE) = 2015 AND invSec.SECTION_TYPE_ID = 14 THEN (invSec.ITEM_COUNT) ELSE 0.00 END) Fall2015TRTUnits,

			SUM(CASE WHEN DATEPART(yy, inv.INVOICE_DATE) = 2014 AND invSec.SECTION_TYPE_ID = 15 THEN (invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00)) ELSE 0.00 END) Fall2014NetEntertainmentSales,
			SUM(CASE WHEN DATEPART(yy, inv.INVOICE_DATE) = 2015 AND invSec.SECTION_TYPE_ID = 15 THEN (invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00)) ELSE 0.00 END) Fall2015NetEntertainmentSales,
			SUM(CASE WHEN DATEPART(yy, inv.INVOICE_DATE) = 2014 AND invSec.SECTION_TYPE_ID = 15 THEN (invSec.ITEM_COUNT) ELSE 0.00 END) Fall2014EntertainmentUnits,
			SUM(CASE WHEN DATEPART(yy, inv.INVOICE_DATE) = 2015 AND invSec.SECTION_TYPE_ID = 15 THEN (invSec.ITEM_COUNT) ELSE 0.00 END) Fall2015EntertainmentUnits

FROM		Invoice inv
JOIN		INVOICE_SECTION invSec
				ON	invSec.INVOICE_ID = inv.INVOICE_ID
JOIN		QSPCanadaProduct..ProgramSectionType pst ON pst.ID = invSec.SECTION_TYPE_ID
JOIN		QSPCanadaOrderManagement..Batch b
				ON	b.OrderID = inv.Order_ID
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
JOIN		QSPCanadaCommon..Campaign campEmbrace ON campEmbrace.BillToAccountID = acc.Id
LEFT JOIN	QSPCanadaCommon..Address [ad]
				ON	[ad].AddressListID = acc.AddressListID
				AND	[ad].Address_Type = 54001 --Ship to
WHERE		(inv.INVOICE_DATE BETWEEN '2014-07-01' AND '2014-12-05' OR inv.INVOICE_DATE BETWEEN '2015-07-01' AND '2015-12-05')
AND			invSec.SECTION_TYPE_ID IN (1, 2, 9, 11, 13, 14, 15)
and			campEmbrace.ID in (select cp.CampaignID from QSPCanadaCommon..CampaignProgram cp where cp.DeletedTF = 0 and cp.ProgramID in (53))
GROUP BY	acc.ID,
			acc.Name,
			ad.stateProvince
ORDER BY	acc.Id