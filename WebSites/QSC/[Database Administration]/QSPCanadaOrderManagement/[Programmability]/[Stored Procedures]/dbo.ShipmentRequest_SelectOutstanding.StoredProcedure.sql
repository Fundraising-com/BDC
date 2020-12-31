USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ShipmentRequest_SelectOutstanding]    Script Date: 06/07/2017 09:20:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShipmentRequest_SelectOutstanding]

	@DistributionCenterID	INT = NULL

AS

SELECT		sro.OrderID,
			sro.DateCreated AS ShipmentRequestDate,
			CASE sro.OrderType
				WHEN 1 THEN 'BHE'
				ELSE		'NON-BHE'
			END AS OrderType,
			COUNT(srcod.ShipmentRequestCustomerOrderDetailID) AS TotalItems,
			SUM(srcod.QtyOrder) AS TotalQuantity,
			acc.ID AS AccountID,
			acc.Name AS AccountName,
			fm.FMID,
			fm.FirstName AS FMFirstName,
			fm.LastName AS FMLastName,
			dc.Name AS DistributionCenter
FROM		CustomerOrderDetail cod
JOIN		QSPCanadaCommon..Season seas
				ON	GETDATE() BETWEEN seas.StartDate AND seas.EndDate
				AND	seas.Season IN ('Y')
JOIN		ShipmentRequestCustomerOrderDetail srcod
				ON	srcod.TransID = cod.TransID
JOIN		ShipmentRequestCustomerOrderHeader srcoh
				ON	srcoh.ShipmentRequestCustomerOrderHeaderID = srcod.ShipmentRequestCustomerOrderHeaderID
				AND	srcoh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
JOIN		ShipmentRequestOrder sro
				ON	sro.ShipmentRequestOrderID = srcoh.ShipmentRequestOrderID
JOIN		Batch batch
				ON	batch.OrderID = sro.OrderID
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = batch.CampaignID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.ShipToAccountID
JOIN		QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = camp.FMID
JOIN		DistributionCenter dc
				ON	dc.ID = cod.DistributionCenterID
WHERE		cod.StatusInstance = 509 --509: Order Detail Pending to TPL
AND			sro.DateCreated BETWEEN seas.StartDate AND seas.EndDate
AND			cod.DistributionCenterID = ISNULL(@DistributionCenterID, cod.DistributionCenterID)
GROUP BY	sro.OrderID,
			sro.DateCreated,
			CASE sro.OrderType
				WHEN 1 THEN 'BHE'
				ELSE		'NON-BHE'
			END,
			acc.ID,
			acc.Name,
			fm.FMID,
			fm.FirstName,
			fm.LastName,
			dc.Name
ORDER BY	ShipmentRequestDate
GO
