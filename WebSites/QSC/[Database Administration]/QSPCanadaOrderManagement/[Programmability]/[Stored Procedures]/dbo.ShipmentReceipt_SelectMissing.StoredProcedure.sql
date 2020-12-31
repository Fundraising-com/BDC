USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ShipmentReceipt_SelectMissing]    Script Date: 06/07/2017 09:20:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShipmentReceipt_SelectMissing]

AS

SELECT		srcoh.CustomerOrderHeaderInstance,
			srcod.TransID,
			sro.OrderID,
			srcod.ProductCode,
			srcod.QtyOrder,
			sro.DateCreated AS ShipmentRequestDate,
			sro.CourierRequest AS FreightCarrier,
			sro.ServiceRequest,
			dc.Name AS DistributionCenter
FROM		ShipmentRequestCustomerOrderDetail srcod
JOIN		ShipmentRequestCustomerOrderHeader srcoh
				ON	srcoh.ShipmentRequestCustomerOrderHeaderID = srcod.ShipmentRequestCustomerOrderHeaderID
JOIN		ShipmentRequestOrder sro
				ON	sro.ShipmentRequestOrderID = srcoh.ShipmentRequestOrderID
JOIN		CustomerOrderHeader coh
				ON	coh.Instance = srcoh.CustomerOrderHeaderInstance
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
				AND	cod.TransID = srcod.TransID
JOIN		DistributionCenter dc
				ON	dc.ID = cod.DistributionCenterID
JOIN		QSPCanadaCommon..Season seas
				ON	GETDATE() BETWEEN seas.StartDate AND seas.EndDate
				AND	seas.Season <> 'Y'
LEFT JOIN	ShipmentReceipt sr
				ON	sr.CustomerOrderHeaderInstance = srcoh.CustomerOrderHeaderInstance
				AND	sr.TransID = srcod.TransID
				AND	sr.StatusID = 2 --2: Processed
WHERE		sr.ShipmentReceiptID IS NULL
AND			sro.DateCreated < DATEADD(DAY, -2, GETDATE())
AND			sro.DateCreated BETWEEN seas.StartDate AND seas.EndDate
ORDER BY	dc.Name,
			sro.OrderID,
			srcoh.CustomerOrderHeaderInstance,
			srcod.TransID
GO
