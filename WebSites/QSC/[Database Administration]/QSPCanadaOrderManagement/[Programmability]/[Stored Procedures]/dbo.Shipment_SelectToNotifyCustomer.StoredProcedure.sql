USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[Shipment_SelectToNotifyCustomer]    Script Date: 06/07/2017 09:20:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Shipment_SelectToNotifyCustomer]

AS

SELECT		DISTINCT
			fm.FMID,
			fm.Email,
			swb.WayBillNumber,
			'Shipment Sent - ' + CONVERT(VARCHAR(MAX), acc.ID) + ' - ' + CONVERT(VARCHAR(MAX), camp.ID) + ' - ' + acc.Name AS [Subject]
FROM		Shipment ship
JOIN		ShipmentWayBill swb
				ON	swb.ShipmentID = ship.ID
JOIN		CustomerOrderDetail cod
				ON	cod.ShipmentID = ship.ID
JOIN		CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		Batch batch
				ON	batch.ID = coh.OrderBatchID
				AND	batch.Date = coh.OrderBatchDate
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = batch.CampaignID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.ShipToAccountID
JOIN		QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = camp.FMID
WHERE		ship.ShipmentDate > '2005-08-29'
AND			ship.FMEmailNotificationSent = '1995-01-01'
AND			ship.ShipmentDate <= GETDATE()
AND			cod.ProductType NOT IN (46001, 46006, 46007, 46012) --Don't send shipment emails for BHE
AND			batch.StatusInstance NOT IN (40005) --40005: Cancelled
AND			cod.DelFlag <> 1
AND			fm.FMID NOT IN ('0508')
AND	NOT		(cod.IsShippedToAccount = 0 AND batch.OrderQualifierID = 39009)
GROUP BY	fm.FMID,
			fm.Email,
			swb.WayBillNumber,
			'Shipment Sent - ' + CONVERT(VARCHAR(MAX), acc.ID) + ' - ' + CONVERT(VARCHAR(MAX), camp.ID) + ' - ' + acc.Name
GO
