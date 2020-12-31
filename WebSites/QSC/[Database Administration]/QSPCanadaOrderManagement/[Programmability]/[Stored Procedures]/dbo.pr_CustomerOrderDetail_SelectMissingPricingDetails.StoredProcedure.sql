USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CustomerOrderDetail_SelectMissingPricingDetails]    Script Date: 06/07/2017 09:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_CustomerOrderDetail_SelectMissingPricingDetails]

AS

SELECT		cod.CustomerOrderHeaderInstance,
			cod.TransID,
			b.OrderID,
			ioi.InternetOrderID,
			cod.ProductCode,
			cod.ProductName,
			cod.Quantity,
			cod.Price,
			cod.ProductType,
			CASE camp.FMID
				WHEN '0508' THEN	'EFR'
				ELSE				'QSP Canada'
			END AS BusinessUnit,
			cod.Recipient,
			CASE coh.CustomerBillToInstance
				WHEN 0 THEN custCod.Address1
				ELSE		custCoh.Address1
			END,
			CASE coh.CustomerBillToInstance
				WHEN 0 THEN custCod.Address2
				ELSE		custCoh.Address2
			END,
			CASE coh.CustomerBillToInstance
				WHEN 0 THEN custCod.City
				ELSE		custCoh.City
			END,
			CASE coh.CustomerBillToInstance
				WHEN 0 THEN custCod.State
				ELSE		custCoh.State
			END AS Province,
			CASE coh.CustomerBillToInstance
				WHEN 0 THEN custCod.Zip
				ELSE		custCoh.Zip
			END AS PostalCode,
			cod.PricingDetailsID,
			coh.CampaignID,
			camp.IsStaffOrder AS StaffCampaign,
			addShip.StateProvince AS AccountShippingProvince,
			addBill.StateProvince AS AccountBillingProvince,
			tr.Description AS TaxRegion,
			cod.CreationDate,
			b.StatusInstance AS BatchStatusInstance,
			b.OrderQualifierID,
			cod.StatusInstance CODStatusInstance
FROM		CustomerOrderDetail cod
JOIN		CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		Batch b
				ON	b.ID = coh.OrderBatchID
				AND	b.[Date] = coh.OrderBatchDate
LEFT JOIN	InternetOrderID ioi
				ON	ioi.CustomerOrderHeaderInstance = coh.Instance
JOIN		QSPCanadaCommon.dbo.Season s
				ON	GETDATE() BETWEEN s.StartDate AND s.EndDate
				AND	s.Season IN ('Y')
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = b.AccountID
LEFT JOIN	QSPCanadaCommon..Address addShip
				ON	addShip.AddressListID = acc.AddressListID
				AND	addShip.Address_Type = 54001 --54001: Ship To
LEFT JOIN	QSPCanadaCommon..Address addBill
				ON	addBill.AddressListID = acc.AddressListID
				AND	addBill.Address_Type = 54002 --54002: Bill To
LEFT JOIN	QSPCanadaCommon..TaxRegionProvince trp
				ON	trp.Province = addBill.StateProvince
LEFT JOIN	QSPCanadaCommon..TaxRegion tr
				ON	tr.ID = trp.TaxRegionID
JOIN		Customer custCoh
				ON	custCoh.Instance = coh.CustomerBillToInstance
JOIN		Customer custCod
				ON	custCod.Instance = cod.CustomerShipToInstance
WHERE		ISNULL(cod.PricingDetailsID, 0) = 0
AND			cod.CreationDate BETWEEN s.StartDate AND s.EndDate
AND			cod.ProductCode <> 'NNNN'
AND			cod.StatusInstance not in (501, 512, 513)
AND			cod.DelFlag = 0
AND			b.StatusInstance <> 40005

AND			NOT EXISTS		(SELECT	1
							FROM	incident inc
							JOIN	incidentAction incA
										ON	inc.IncidentInstance = incA.IncidentInstance
							WHERE	inc.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
							AND		incA.ActionInstance IN (18, 27, 22) --Update Credit Card Info, CC Call Attempt 5, Remove From OEFU Report
							)

ORDER BY	cod.CreationDate
GO
