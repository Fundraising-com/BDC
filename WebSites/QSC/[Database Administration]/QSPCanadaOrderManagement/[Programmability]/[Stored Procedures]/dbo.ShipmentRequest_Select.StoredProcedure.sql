USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ShipmentRequest_Select]    Script Date: 06/07/2017 09:20:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShipmentRequest_Select]

	@ShipmentRequestBatchID	INT

AS

SELECT		DISTINCT
			1 AS Tag,
			NULL AS Parent,
			sro.OrderID AS [Batch!1!BatchOrderID!Element],
			sro.OrderType AS [Batch!1!OrderType!Element],
			ISNULL(CONVERT(VARCHAR(10), sro.DateCreated, 120), '') AS [Batch!1!OrderDate!Element],
			ISNULL(sro.PDFFileName, '') AS [Batch!1!PDFFileName!Element],
			sro.CourierRequest AS [Batch!1!CourierRequest!Element],
			sro.ServiceRequest AS [Batch!1!ServiceRequest!Element],
			ISNULL(CONVERT(VARCHAR(10), sro.RequestedShipDate, 120), '') AS [Batch!1!RequestedShipDate!Element],
			NULL AS [OrderHeader!2!BatchOrderID!Element],
			NULL AS [OrderHeader!2!OrderHeaderID!Element],
			NULL AS [OrderHeader!2!StudentName!Element],
			NULL AS [OrderHeader!2!ClassID!Element],
			NULL AS [OrderHeader!2!ClassName!Element],
			NULL AS [OrderDetail!3!BatchOrderID!Element],
			NULL AS [OrderDetail!3!OrderHeaderID!Element],
			NULL AS [OrderDetail!3!LineNumber!Element],
			NULL AS [OrderDetail!3!ProductCode!Element],
			NULL AS [OrderDetail!3!QtyOrder!Element],
			NULL AS [OrderDetail!3!ShipToName!Element],
			NULL AS [OrderDetail!3!ShipToAddress1!Element],
			NULL AS [OrderDetail!3!ShipToAddress2!Element],
			NULL AS [OrderDetail!3!ShipToCity!Element],
			NULL AS [OrderDetail!3!ShipToZipCode!Element],
			NULL AS [OrderDetail!3!ShipToCountry!Element],
			NULL AS [OrderDetail!3!ShipToProvince!Element],
			NULL AS [OrderDetail!3!ShipToContactName!Element],
			NULL AS [OrderDetail!3!ShipToPhoneNumber!Element]
FROM		ShipmentRequestBatch srb
JOIN		ShipmentRequestOrder sro
				ON	srb.ShipmentRequestBatchID = sro.ShipmentRequestBatchID
WHERE		srb.ShipmentRequestBatchID = @ShipmentRequestBatchID

UNION ALL

SELECT		DISTINCT
			2 AS Tag,
			1 AS Parent,
			sro.OrderID,
			sro.OrderType,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			sro.OrderID,
			srcoh.CustomerOrderHeaderInstance,
			ISNULL(srcoh.StudentName, ''),
			srcoh.ClassID,
			ISNULL(srcoh.ClassName, ''),
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL
FROM		ShipmentRequestBatch srb
JOIN		ShipmentRequestOrder sro
				ON	srb.ShipmentRequestBatchID = sro.ShipmentRequestBatchID
JOIN		ShipmentRequestCustomerOrderHeader srcoh
				ON	srcoh.ShipmentRequestOrderID = sro.ShipmentRequestOrderID
WHERE		srb.ShipmentRequestBatchID = @ShipmentRequestBatchID

UNION ALL

SELECT		DISTINCT
			3 AS Tag,
			2 AS Parent,
			sro.OrderID,
			sro.OrderType,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL,
			sro.OrderID,
			srcoh.CustomerOrderHeaderInstance,
			NULL,
			NULL,
			NULL,
			sro.OrderID,
			srcoh.CustomerOrderHeaderInstance,
			srcod.TransID,
			ISNULL(srcod.ProductCode, ''),
			ISNULL(srcod.QtyOrder, 0),
			ISNULL(srcod.ShipToName, ''),
			ISNULL(srcod.ShipToAddress1, ''),
			ISNULL(srcod.ShipToAddress2, ''),
			ISNULL(srcod.ShipToCity, ''),
			ISNULL(srcod.ShipToZipCode, ''),
			ISNULL(srcod.ShipToCountry, ''),
			ISNULL(srcod.ShipToProvince, ''),
			ISNULL(srcod.ShipToContactName, ''),
			ISNULL(srcod.ShipToPhoneNumber, '')			
FROM		ShipmentRequestBatch srb
JOIN		ShipmentRequestOrder sro
				ON	srb.ShipmentRequestBatchID = sro.ShipmentRequestBatchID
JOIN		ShipmentRequestCustomerOrderHeader srcoh
				ON	srcoh.ShipmentRequestOrderID = sro.ShipmentRequestOrderID
JOIN		ShipmentRequestCustomerOrderDetail srcod
				ON	srcod.ShipmentRequestCustomerOrderHeaderID = srcoh.ShipmentRequestCustomerOrderHeaderID
WHERE		srb.ShipmentRequestBatchID = @ShipmentRequestBatchID

ORDER BY	[Batch!1!BatchOrderID!Element],
			[Batch!1!OrderType!Element],
			[OrderHeader!2!OrderHeaderID!Element],
			[OrderDetail!3!LineNumber!Element]

FOR XML EXPLICIT
GO
