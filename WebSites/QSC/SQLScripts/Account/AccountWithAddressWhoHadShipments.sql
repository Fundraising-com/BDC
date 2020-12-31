USE QSPCanadaCommon
GO

SELECT		DISTINCT
			acc.Id AccountID,
			acc.Name AccountName,
			ad.street1 Address1,
			ad.street2 Address2,
			ad.City,
			ad.stateProvince Province,
			ad.postal_code PostalCode,
			ISNULL(phM.PhoneNumber, phW.PhoneNumber) PhoneNumber,
			ISNULL(phM.BestTimeToCall, phW.BestTimeToCall) BestTimeToCall
FROM		QSPCanadaOrderManagement..Batch b
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
LEFT JOIN	QSPCanadaCommon..Address [ad]
				ON	[ad].AddressListID = acc.AddressListID
				AND	[ad].Address_Type = 54001 --Ship to
LEFT JOIN	QSPCanadaCommon..Phone phM
				ON	phM.PhoneListID = acc.PhoneListID
				AND	phM.Type = 30505
LEFT JOIN	QSPCanadaCommon..Phone phW
				ON	phW.PhoneListID = acc.PhoneListID
				AND	phW.Type = 30501
JOIN		QSPCanadaOrderManagement..ShipmentRequestOrder sro
				ON	sro.OrderID = b.OrderID
JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh
				ON	coh.OrderBatchDate = b.Date
				AND	coh.OrderBatchID = b.ID
JOIN		QSPCanadaOrderManagement..CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE		sro.DateCreated BETWEEN '2013-01-01' AND '2013-12-31'
AND			cod.ProductType IN (46002, 46004, 46008, 46013, 46014, 46018, 46020)
ORDER BY	acc.ID