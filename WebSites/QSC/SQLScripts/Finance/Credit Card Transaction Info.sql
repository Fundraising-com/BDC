USE QSPCanadaOrderManagement
GO

SELECT		cdPaymentMethod.Description PaymentMethod,
			CASE acc.BusinessUnitID WHEN 1 THEN 'QSP' ELSE 'BDC' END BusinessUnit,
			CASE b.OrderQualifierID WHEN 39009 THEN 'Online' ELSE 'Landed' END OrderSource,
			SUM(cph.TotalAmount) Amount
FROM		Batch b
JOIN		CustomerOrderHeader coh ON coh.OrderBatchDate = b.Date AND coh.OrderBatchID = b.ID
JOIN		CustomerPaymentHeader cph ON cph.CustomerOrderHeaderInstance = coh.Instance
JOIN		CreditCardPayment ccp ON ccp.CustomerPaymentHeaderInstance = cph.Instance
JOIN		QSPCanadaCommon..Campaign camp ON camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..CAccount acc ON acc.Id = camp.BillToAccountID
JOIN		QSPCanadaCommon..CodeDetail cdPaymentMethod ON cdPaymentMethod.Instance = coh.PaymentMethodInstance
WHERE		ccp.StatusInstance IN (19000)
AND			ccp.DateCreated BETWEEN '2014-01-01' AND '2015-01-01'
GROUP BY	cdPaymentMethod.Description,
			acc.BusinessUnitID,
			CASE b.OrderQualifierID WHEN 39009 THEN 'Online' ELSE 'Landed' END