USE QSPCanadaOrderManagement
GO

--For Commissions, need to remove duplicates due to multiple ccs.CommissionPercentages per School

/*SELECT		fm.FirstName FMFirstName, fm.LastName FMLastName, accFM.Id FMAccountID, cFM.ID FMCampaignID, b.OrderID, b.OrderQualifierID, b.StatusInstance, b.IsInvoiced,
			cod.CustomerOrderHeaderInstance, cod.TransID, cod.ProductName, cod.ProductType, cust.*, accSchool.* */

SELECT		DISTINCT fm.FirstName FMFirstName, fm.LastName FMLastName, accFM.Id FMAccountID, cFM.ID FMCampaignID, b.OrderID, b.Date OrderDate, cdOrderQualifier.Description OrderQualifier, cdOrderStatus.Description OrderStatus,
			inv.INVOICE_ID InvoiceID, inv.INVOICE_AMOUNT InvoiceAmount, accSchool.Id SchoolAccountID, accSchool.Name SchoolName,
			ccs.FMID CommissionSplitFMID, fm2.FirstName FMSplitFirstName, fm2.LastName FMSplitLastName, ccs.CommissionPercentage, accFMSplit.Id FMSplitAccountID, campFMSplit.ID FMSplitCampaignID

FROM		Batch b
JOIN		QSPCanadaCommon..Campaign cFM ON cFM.ID = b.CampaignID
JOIN		QSPCanadaCommon..CAccount accFM ON accFM.Id = cFM.BillToAccountID
JOIN		QSPCanadaCommon..FieldManager fm ON fm.FMID = cFM.FMID
JOIN		CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
LEFT JOIN	QSPCanadaFinance..Invoice inv ON inv.INVOICE_ID = cod.InvoiceNumber
JOIN		QSPCanadaCommon..CodeDetail cdOrderQualifier on cdOrderQualifier.Instance = b.OrderQualifierID
JOIN		QSPCanadaCommon..CodeDetail cdOrderStatus on cdOrderStatus.Instance = b.StatusInstance
JOIN		QSPCanadaOrderManagement.dbo.Customer cust
				ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
										WHEN 0 THEN coh.CustomerBillToInstance
										ELSE		cod.CustomerShipToInstance
									END
LEFT JOIN	(SELECT MIN(Id) Id, Name, CAccountCodeClass, postal_code PostalCode FROM QSPCanadaCommon..CAccount acc JOIN QSPCanadaCommon..[Address] ad ON ad.AddressListID = acc.AddressListID AND ad.address_type = 54001 GROUP BY acc.Name, acc.CAccountCodeClass, ad.postal_code) AS accSchool ON accSchool.Name = cust.FirstName AND accSchool.PostalCode = cust.Zip AND ISNULL(accSchool.CAccountCodeClass, '') <> 'FM'
LEFT JOIN	(QSPCanadaCommon..Campaign camp JOIN QSPCanadaCommon..CampaignCommissionSplit ccs ON ccs.CampaignID = camp.ID JOIN QSPCanadaCommon..FieldManager fm2 ON fm2.FMID = ccs.FMID) ON camp.BillToAccountID = accSchool.Id AND ccs.FMID <> cFM.FMID 
LEFT JOIN	(QSPCanadaCommon..Campaign campFMSplit
JOIN		QSPCanadaCommon..CAccount accFMSplit
				ON	accFMSplit.ID = campFMSplit.BillToAccountID
				AND	accFMSplit.CAccountCodeGroup = 'Comm')
				ON	campFMSplit.FMID = ccs.FMID
				AND	campFMSplit.[Status] = 37002
WHERE		accFM.CAccountCodeClass = 'FM'
AND			b.StatusInstance <> 40005
AND			b.[Date] >= '2013-07-01'
AND			accSchool.Id IS NOT NULL
AND			camp.ID IS NOT NULL
ORDER BY	fm.FirstName, accSchool.Id, b.OrderID