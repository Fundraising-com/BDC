USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ShipmentReceipt_UpdateShipmentInfo]    Script Date: 06/07/2017 09:20:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShipmentReceipt_UpdateShipmentInfo]

	@ShipmentReceiptID	INT

AS

DECLARE	@RunDate						DATETIME,
		@CustomerOrderHeaderInstance	INT,
		@TransID						INT,
		@OrderID						INT,
		@BatchID						INT,
		@BatchDate						DATETIME,
		@ProductType					INT,
		@DistributionCenterID			INT,
		@ShipmentID						INT,
		@ShipmentVariationID			INT,
		@QtyOrdered						INT,
		@QtyShipped						INT,
		@QtyReplaced					INT,
		@TrackingNumber					VARCHAR(50),	
		@ShipDate						DATETIME,
		@NumBoxes						INT,
		@CarrierName					VARCHAR(255),
		@Weight							DECIMAL(16, 6),
		@CarrierID						INT

SET	@RunDate = GETDATE()

SELECT		@CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance,
			@TransID = cod.TransID,
			@OrderID = b.OrderID,
			@BatchID = b.ID,
			@BatchDate = b.Date,
			@ProductType = cod.ProductType,
			@QtyOrdered = cod.Quantity - ISNULL(cod.QuantityShipped, 0),
			@QtyShipped = sr.QtyShipped,
			@TrackingNumber = sr.TrackingNumber,
			@ShipDate = sr.ShipDate,
			@NumBoxes = sr.NumBoxes,
			@CarrierName = sr.Courier,
			@Weight = sr.Weight,
			@DistributionCenterID = cod.DistributionCenterID
FROM		ShipmentReceipt sr
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = sr.CustomerOrderHeaderInstance
				AND	cod.TransID = sr.TransID
JOIN		CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		Batch b
				ON	b.ID = coh.OrderBatchID
				AND	b.Date = coh.OrderBatchDate
WHERE		sr.ShipmentReceiptID = @ShipmentReceiptID

SET	@CarrierID =	CASE @CarrierName
						WHEN 'Canada Post' THEN	53005
						WHEN 'Purolator' THEN	53010
						WHEN 'DHL' THEN	53001
						WHEN 'Sameday' THEN		53008
						WHEN '20/20' THEN		53009
						WHEN '20/20 TRANSPORTATION' THEN		53009
						WHEN '2020' THEN		53009
						WHEN 'Dicom' THEN		53011
						WHEN 'Gojit' THEN		53012
						WHEN 'Day&Ross' THEN	53013
						WHEN 'Canpar' THEN		53014
						WHEN 'CUST' THEN		53009
						WHEN 'PCSL' THEN		53010
						WHEN 'PUR' THEN			53010
						WHEN 'QSPD' THEN		53001
						ELSE					53007 --53007: Other Carrier
					END

BEGIN TRANSACTION

IF @QtyShipped > 0
BEGIN
	
	IF @QtyShipped <> @QtyOrdered
	BEGIN
		INSERT	ShipmentVariation
		(
				SessionID,
				CustomerOrderHeaderInstance,
				TransID,
				QuantityShipped,
				QuantityReplaced,
				ShipTF
		)
		SELECT	'',
				@CustomerOrderHeaderInstance,
				@TransID,
				@QtyShipped,
				0,
				1

		SET	@ShipmentVariationID = SCOPE_IDENTITY()

	END

	INSERT	Shipment
	(
		CarrierID,
		ShipmentDate,
		CountryCode,
		ExpectedDeliveryDate,
		NumberBoxes,
		Weight,
		DateModified,
		UserIDModified,
		OperatorName,
		NumberSkids,
		WeightUnitOfMeasure,
		Comment,
		FMEmailNotificationSent
	)
	SELECT	@CarrierID,
			@ShipDate,
			'CA',
			@ShipDate,
			@NumBoxes,
			@Weight,
			@RunDate,
			1,
			'',
			0,
			'',
			'',
			'1/1/95'

	SET	@ShipmentID = SCOPE_IDENTITY()

	UPDATE	cod
	SET		StatusInstance = CASE WHEN @QtyShipped <> @QtyOrdered THEN StatusInstance ELSE 508 END, --508: Order Detail Shipped
			ShipmentID = @ShipmentID,
			QuantityShipped = ISNULL(QuantityShipped, 0) + @QtyShipped
	FROM	CustomerOrderDetail cod
	WHERE	CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
	AND		TransID = @TransID

	DECLARE	@NotFulfilled	BIT

	SELECT	TOP 1
			@NotFulfilled = 1
	FROM	Batch b
	JOIN	CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
	JOIN	CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
	WHERE	b.OrderID = @OrderID
	AND		cod.DistributionCenterID > 0
	AND		cod.DelFlag <> 1
	AND		cod.StatusInstance NOT IN (506, 508) --506: Order Detail Voided Due To Error, 508: Order Detail Shipped

	UPDATE	b
	SET		StatusInstance = CASE @NotFulfilled
								WHEN 1 THEN 40014 --40014: Partially Fulfilled
								ELSE		40013 --40013: Fulfilled
							END
	FROM	Batch b
	WHERE	b.OrderID = @OrderID

	UPDATE	bdc
	SET		StatusInstance = CASE @NotFulfilled
								WHEN 1 THEN 40014 --40014: Partially Fulfilled
								ELSE		40011 --40011: Shipped
							END
	FROM	BatchDistributionCenter bdc
	WHERE	bdc.BatchID = @BatchID
	AND		bdc.BatchDate = @BatchDate

	INSERT ShipmentOrder
	(
		ShipmentID,
		OrderID,
		DistributionCenterID
	)
	SELECT	@ShipmentID,
			@orderID,
			@DistributionCenterID

	INSERT ShipmentWayBill
	(
		ShipmentID,
		WayBillNumber
	)
	SELECT	@ShipmentID,
			CASE WHEN ISNULL(@TrackingNumber,'') = '' THEN CONVERT(VARCHAR(50), @OrderID) ELSE @TrackingNumber END
END

UPDATE	OrderStageTracking
SET		PDFAckTPL = CASE WHEN LEN(PDFFilename) > 0 THEN 1 END,
		BatchAckTPL = 1,
		CampaignAckTPL = 1
WHERE	OrderID = @OrderID

UPDATE	ShipmentReceipt
SET		StatusID = 2 --2: Processed
WHERE	ShipmentReceiptID = @ShipmentReceiptID

COMMIT TRANSACTION
GO
