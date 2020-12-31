USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ShipmentRequest_SelectOrders]    Script Date: 06/07/2017 09:20:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShipmentRequest_SelectOrders]

	@DistributionCenterID INT

AS

SELECT		DISTINCT TOP 100
			b.OrderID,
			CASE	WHEN rrb.IsQSPPrint = 0 AND @DistributionCenterID IN (2) THEN CONVERT(BIT, 1)
					ELSE CONVERT(BIT, 0)
			END AS ReportGenerationRequired
FROM		Batch b
JOIN		CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
/*JOIN		BatchDistributionCenter bdc
				ON	bdc.BatchDate = b.Date
				AND	bdc.BatchID = b.ID
				AND	bdc.DistributionCenterID = @DistributionCenterID*/
LEFT JOIN	ReportRequestBatch rrb
				ON	rrb.BatchOrderID = b.OrderID
LEFT JOIN	QSPCanadaCommon..SystemErrorLog sel
				ON	sel.OrderID = b.OrderID
				AND	ISNULL(IsFixed, 0) = 0
				AND	ProcName = 'ShipmentRequest_ValidateOrder'
LEFT JOIN	(ShipmentRequestCustomerOrderHeader srcoh
JOIN	ShipmentRequestCustomerOrderDetail srcod
				ON	srcod.ShipmentRequestCustomerOrderHeaderID = srcoh.ShipmentRequestCustomerOrderHeaderID)
				ON	srcoh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	srcod.TransID = cod.TransID
WHERE		b.StatusInstance IN (40010, 40014, 40012) --40010: At Warehouse, 40014: Partially Fulfilled, 40012: Sent to TPL
AND			cod.DistributionCenterID = @DistributionCenterID
AND			cod.Delflag <> 1
AND			dbo.UDF_PDFGenerationStatus(b.OrderID) = 1 --PDF's must not be in a state of being generated
AND			cod.StatusInstance IN (509) --509: Order Detail Pending to TPL
AND			sel.OrderID IS NULL --Don't send Orders that are currently marked in error
AND			cod.CreationDate < DATEADD(HOUR, -1, GETDATE()) --Don't send Orders that are less than one hour old
AND			srcod.ShipmentRequestCustomerOrderDetailID IS NULL
AND			cod.CreationDate >= '2012-07-01'
GROUP BY	b.OrderID,
			rrb.IsQSPPrint
ORDER BY	b.OrderID
GO
