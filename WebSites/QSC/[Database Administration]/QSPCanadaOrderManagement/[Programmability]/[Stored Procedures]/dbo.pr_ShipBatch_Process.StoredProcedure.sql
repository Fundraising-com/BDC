USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ShipBatch_Process]    Script Date: 10/23/2017 10:46:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pr_ShipBatch_Process]

AS

DECLARE @ShipmentID INT,
		@OrderID INT,
		@DistributionCenterID INT,
		@ShipmentGroupID INT,
		@BatchDate DATETIME, 
		@Id INT,
		@StatusInstance INT,
		@OutstandingShipmentsForShipmentGroup BIT

DECLARE C1 CURSOR FOR
SELECT	s.Id, so.OrderId, so.DistributionCenterID, s.ShipmentGroupID 
FROM	Shipment s
JOIN	ShipmentOrder so ON so.ShipmentID = s.ID
WHERE	s.ProcessedDate IS NULL

OPEN C1
FETCH NEXT FROM C1 INTO @ShipmentID, @OrderID, @DistributionCenterID, @ShipmentGroupID
		
WHILE @@Fetch_Status = 0
BEGIN

	BEGIN TRAN

	SELECT	@batchdate = [Date],
			@Id = [id]
	FROM	Batch 
	WHERE	OrderID = @orderID
						
	SELECT	cod.CustomerOrderHeaderInstance, cod.TransID, b.OrderID, pl.ShipmentGroupID
	INTO	#cod
	FROM	Batch b
	JOIN	CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
	JOIN	CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
	JOIN	QSPCanadaCommon..QSPProductLine pl
				ON	pl.ID = cod.ProductType
	WHERE	((b.OrderID = @OrderID AND (b.OrderQualifierID NOT IN (39009) OR cod.IsShippedToAccount = 0))
	OR		(cod.IsShippedToAccount = 1 AND b.OrderID IN (SELECT DISTINCT OnlineOrderID  
															FROM OnlineOrderMappingTable  
															WHERE LandedOrderID = @OrderID)))
	AND		cod.DistributionCenterID = @DistributionCenterID
	AND		cod.StatusInstance IN (509, 510, 511)

	SELECT	DISTINCT OrderID
	INTO	#batch
	FROM	#cod
	UNION	SELECT @OrderID --Ensure the main order is included in the case of a Fake Landed Order
			
	DECLARE @VariationCount int
			
	SELECT	@VariationCount =  Count(*)
	FROM	ShipmentVariation sv
	JOIN	#cod cod ON cod.CustomerOrderHeaderInstance = sv.CustomerOrderHeaderInstance AND cod.TransID = sv.TransId
	WHERE	cod.ShipmentGroupID = ISNULL(@ShipmentGroupID, cod.ShipmentGroupID)								

	IF @VariationCount > 0 
	BEGIN
		UPDATE	cod
		SET		StatusInstance = 508,
				ShipmentID = @ShipmentID,
				ReplacedProductCode = CASE WHEN sv.ReplacementItemId > 0 THEN sv.ReplacementItemId ELSE NULL END,
				ReplacedProductQty = CASE WHEN sv.QuantityReplaced > 0 THEN sv.QuantityReplaced ELSE NULL END,
				QuantityShipped = sv.QuantityShipped,
				Comment = sv.Comment,
				CustomerComment = sv.CustomerComment
		FROM 	CustomerOrderDetail cod
		JOIN	CustomerOrderHeader coh ON coh.Instance = cod.CustomerOrderHeaderInstance
		JOIN	Batch b ON b.ID = coh.OrderBatchID AND b.Date = coh.OrderBatchDate
		JOIN	ShipmentVariation sv ON sv.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance AND sv.TransID = cod.TransID
		JOIN	#cod o ON o.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance AND o.TransID = cod.TransID
		WHERE 	sv.ShipTF = 1
		AND		o.ShipmentGroupID = ISNULL(@ShipmentGroupID, o.ShipmentGroupID)	 						
	END
	ELSE
	BEGIN
		UPDATE	cod 
		SET		StatusInstance = 508,
				ShipmentID = @ShipmentID,
				QuantityShipped = Quantity
		FROM 	CustomerOrderDetail cod
		JOIN	CustomerOrderHeader coh ON coh.Instance = cod.CustomerOrderHeaderInstance
		JOIN	Batch b ON b.ID = coh.OrderBatchID AND b.Date = coh.OrderBatchDate
		JOIN	#cod o ON o.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance AND o.TransID = cod.TransID
		WHERE	o.ShipmentGroupID = ISNULL(@ShipmentGroupID, o.ShipmentGroupID)
	END

	SELECT		b.OrderID,
				cod.ProductType,
				SUM(CASE WHEN cod.StatusInstance IN (511, 510, 509) THEN cod.Quantity ELSE 0 END) QuantityOutstanding
	INTO		#OutstandingShipmentsPerOrderPerPL
	FROM		Batch b
	JOIN		#batch ON #batch.OrderID = b.OrderID
	JOIN		CustomerOrderHeader coh ON coh.OrderBatchDate = b.Date AND coh.OrderBatchID = b.ID
	LEFT JOIN	CustomerOrderDetail cod
					ON	cod.CustomerOrderHeaderInstance = coh.Instance
					AND cod.DistributionCenterID = @DistributionCenterID
	GROUP BY	b.OrderID,
				cod.ProductType

	SELECT		osopl.ProductType,
				SUM(osopl.QuantityOutstanding) QuantityOutstanding
	INTO		#OutstandingShipmentsPerPL
	FROM		#OutstandingShipmentsPerOrderPerPL osopl
	GROUP BY	osopl.ProductType

	SELECT		osopl.OrderID,
				SUM(osopl.QuantityOutstanding) QuantityOutstanding
	INTO		#OutstandingShipmentsPerOrder
	FROM		#OutstandingShipmentsPerOrderPerPL osopl
	GROUP BY	osopl.OrderID

	SELECT		SUM(oso.QuantityOutstanding) QuantityOutstanding
	INTO		#OutstandingShipments
	FROM		#OutstandingShipmentsPerOrder oso

	--Update Online Account Delivered Orders
	UPDATE	bdc
	SET		StatusInstance = CASE osopl.QuantityOutstanding WHEN 0 THEN 40011 ELSE 40014 END
	FROM	BatchDistributionCenter bdc
	JOIN	Batch b ON b.ID = bdc.BatchID AND b.Date = bdc.BatchDate
	JOIN	#OutstandingShipmentsPerOrderPerPL osopl ON osopl.OrderID = b.OrderID AND bdc.QSPProductLine = osopl.ProductType
	WHERE	osopl.OrderID <> @OrderID

	UPDATE	bdc
	SET		StatusInstance = CASE ospl.QuantityOutstanding WHEN 0 THEN 40011 ELSE 40014 END
	FROM	BatchDistributionCenter bdc
	JOIN	Batch b ON b.ID = bdc.BatchID AND b.Date = bdc.BatchDate
	JOIN	#OutstandingShipmentsPerPL ospl ON bdc.QSPProductLine = ospl.ProductType
	WHERE	b.OrderID = @OrderID
				
	--Update Online Account Delivered Orders
	UPDATE	b
	SET		StatusInstance = CASE oso.QuantityOutstanding WHEN 0 THEN 40013 ELSE 40014 END
	FROM	Batch b
	JOIN	#OutstandingShipmentsPerOrder oso ON oso.OrderID = b.OrderID
	WHERE	oso.OrderID <> @OrderID

	UPDATE	b
	SET		StatusInstance = CASE os.QuantityOutstanding WHEN 0 THEN 40013 ELSE 40014 END
	FROM	Batch b
	JOIN	#OutstandingShipments os ON 1 = 1
	WHERE	b.OrderID = @orderID

	UPDATE	Shipment
	SET		ProcessedDate = GETDATE()
	WHERE	Id = @ShipmentID

	SET @OutstandingShipmentsForShipmentGroup = NULL

	SELECT		@OutstandingShipmentsForShipmentGroup = CONVERT(BIT, 1)
	FROM 		CustomerOrderDetail cod
	JOIN		CustomerOrderHeader coh ON coh.Instance = cod.CustomerOrderHeaderInstance
	JOIN		Batch b ON b.ID = coh.OrderBatchID AND b.Date = coh.OrderBatchDate
	JOIN		#cod o ON o.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance AND o.TransID = cod.TransID
	WHERE		o.ShipmentGroupID = ISNULL(@ShipmentGroupID, o.ShipmentGroupID)
	AND			b.StatusInstance = 40014
	AND			cod.StatusInstance IN (511, 510, 509)

	IF (@OutstandingShipmentsForShipmentGroup = 1)
	BEGIN
		UPDATE	QSPCanadacommon..SystemErrorLog
		SET		IsReviewed=1, IsFixed=1
		WHERE	OrderID = @OrderID

		EXEC pr_Ins_Report_Parameters_V2 @OrderId, -1, @ShipmentGroupID
	END

	COMMIT TRAN

   DROP TABLE #cod
   DROP TABLE #batch
   DROP TABLE #OutstandingShipmentsPerOrderPerPL
   DROP TABLE #OutstandingShipmentsPerPL
   DROP TABLE #OutstandingShipmentsPerOrder
   DROP TABLE #OutstandingShipments

	FETCH NEXT FROM C1 INTO @ShipmentID, @OrderID, @DistributionCenterID, @ShipmentGroupID
END
CLOSE C1
DEALLOCATE C1
GO