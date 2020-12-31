USE QSPCanadaFinance

SELECT		seas.FiscalYear,
			seas.Season,
			[add].StateProvince,
			SUM(inv.Invoice_Amount)
FROM		Invoice inv
JOIN		QSPCanadaOrderManagement..Batch b
				ON	b.OrderID = inv.Order_ID
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
LEFT JOIN	QSPCanadaCommon..Address [add]
				ON	[add].AddressListID = acc.AddressListID
				AND	[add].Address_Type = 54002 --Bill to
JOIN		QSPCanadaCommon..Season seas
				ON	camp.StartDate BETWEEN seas.StartDate AND seas.EndDate
				AND	seas.Season IN ('F', 'S')
GROUP BY	seas.FiscalYear,
			seas.Season,
			[add].StateProvince
ORDER BY	seas.FiscalYear,
			seas.Season,
			[add].StateProvince