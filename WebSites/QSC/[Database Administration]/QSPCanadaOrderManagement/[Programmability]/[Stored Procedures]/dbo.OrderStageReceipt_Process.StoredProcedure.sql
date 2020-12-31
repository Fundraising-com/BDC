USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[OrderStageReceipt_Process]    Script Date: 06/07/2017 09:19:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[OrderStageReceipt_Process]

	@OrderStageReceiptID		INT,
	@IsOrderStageReceiptValid	BIT OUTPUT

AS

SET @IsOrderStageReceiptValid = CONVERT(BIT, 1)

DECLARE	@Error							BIT,
		@ErrorMessage					VARCHAR(1000),
		@RecExist						BIT,
		@OrderID						INT,
		@CustomerOrderHeaderInstance	INT,
		@TransID						INT

SELECT		TOP 1
			@Error = 1
FROM		OrderStageReceipt osr
WHERE		osr.OrderStageReceiptID = @OrderStageReceiptID
AND			(ISNULL(osr.StageID, 0) = 0
OR			osr.StageID NOT BETWEEN 59000 AND 59007)

IF ISNULL(@Error, 0) = 1
BEGIN
	UPDATE	OrderStageReceipt
	SET		[StatusID] = 4 --4: Order Stage is Invalid
	WHERE	OrderStageReceiptID = @OrderStageReceiptID
	
	SET @Error = CONVERT(BIT, 0)
	SET @IsOrderStageReceiptValid = CONVERT(BIT, 0)
END

SELECT		TOP 1
			@Error = CONVERT(BIT, 1)
FROM		OrderStageReceiptBatch osrb
JOIN		OrderStageReceipt osr
				ON	osr.OrderStageReceiptBatchID = osrb.OrderStageReceiptBatchID
WHERE		osr.OrderStageReceiptID = @OrderStageReceiptID
AND			((ISNULL(osrb.SnapShotDate, '01/01/1995') = '01/01/1995'
OR			DATEDIFF(WEEK, osrb.SnapShotDate, GETDATE()) > 2
OR			ISNULL(osr.ReceiptDate, '01/01/1995') = '01/01/1995')
OR			DATEDIFF(WEEK, osr.ReceiptDate, GETDATE()) > 2
OR			(ISNULL(osr.Imagedate, '01/01/1995') = '01/01/1995' AND osr.StageID = 59001)
OR			(DATEDIFF(WEEK, osr.Imagedate, GETDATE()) > 2 AND osr.StageID = 59001)
OR			(ISNULL(osr.DataCaptureDate, '01/01/1995') = '01/01/1995' AND osr.StageID = 59002)
OR			(DATEDIFF(WEEK, osr.DataCaptureDate, GETDATE()) > 2 AND osr.StageID = 59002)
OR			(ISNULL(osr.VerificationDate, '01/01/1995') = '01/01/1995' AND osr.StageID = 59003)
OR			(DATEDIFF(WEEK, osr.VerificationDate, GETDATE()) > 2 AND osr.StageID = 59003)
OR			(ISNULL(osr.EditDate, '01/01/1995') = '01/01/1995' AND osr.StageID = 59004)
OR			(DATEDIFF(WEEK, osr.EditDate, GETDATE()) > 2 AND osr.StageID = 59004)
OR			(ISNULL(osr.TransmitDate, '01/01/1995') = '01/01/1995' AND osr.StageID = 59005)
OR			(DATEDIFF(WEEK, osr.TransmitDate, GETDATE()) > 2 AND osr.StageID = 59005))

IF ISNULL(@Error, 0) = 1
BEGIN
	UPDATE	OrderStageReceipt
	SET		[StatusID] = 5 --5: Blank or old Snapshot date / Receipt date / Image date / Datacapture Date / Verfication Date / Edit Date / Transmit Date
	WHERE	OrderStageReceiptID = @OrderStageReceiptID
	
	SET @Error = CONVERT(BIT, 0)
	SET @IsOrderStageReceiptValid = CONVERT(BIT, 0)
END

SELECT		TOP 1
			@Error = CONVERT(BIT, 1)
FROM		OrderStageReceipt osr
JOIN		OrderStageReceiptBatch osrb
				ON	osrb.OrderStageReceiptBatchID = osr.OrderStageReceiptBatchID
WHERE		osr.OrderStageReceiptID = @OrderStageReceiptID
AND			ISNULL(osrb.TransmissionSequenceID, 0) = 0

IF ISNULL(@Error, 0) = 1
BEGIN
	UPDATE	OrderStageReceipt
	SET		[StatusID] = 6 --6: Transmission Sequence Number is missing
	WHERE	OrderStageReceiptID = @OrderStageReceiptID
	
	SET @Error = CONVERT(BIT, 0)
	SET @IsOrderStageReceiptValid = CONVERT(BIT, 0)
END

SELECT		TOP 1
			@Error = CONVERT(BIT, 1)
FROM		OrderStageReceipt osr
WHERE		osr.OrderStageReceiptID = @OrderStageReceiptID
AND			(ISNULL(osr.CampaignID, 0) = 0 
OR			ISNULL(osr.GroupID, 0) = 0)

IF ISNULL(@Error, 0) = 1
BEGIN
	UPDATE	OrderStageReceipt
	SET		[StatusID] = 7 --7: Missing CampaignID or GroupID
	WHERE	OrderStageReceiptID = @OrderStageReceiptID
	
	SET @Error = CONVERT(BIT, 0)
	SET @IsOrderStageReceiptValid = CONVERT(BIT, 0)
END

SELECT		TOP 1
			@Error = CONVERT(BIT, 1)
FROM		OrderStageReceipt osr
LEFT JOIN	QSPCanadaCommon..Campaign camp
				ON	camp.ID = osr.CampaignID
				AND	camp.FMID = osr.FMID
WHERE		osr.OrderStageReceiptID = @OrderStageReceiptID
AND			ISNULL(camp.ID, 0) = 0

IF ISNULL(@Error, 0) = 1
BEGIN
	UPDATE	OrderStageReceipt
	SET		[StatusID] = 8 --8: Invalid CampaignID, or FMID
	WHERE	OrderStageReceiptID = @OrderStageReceiptID
	
	SET @Error = CONVERT(BIT, 0)
	SET @IsOrderStageReceiptValid = CONVERT(BIT, 0)
END

SELECT		TOP 1
			@Error = CONVERT(BIT, 1)
FROM		OrderStageReceipt osrCurrent
JOIN		OrderStageReceiptBatch osrbCurrent
				ON	osrbCurrent.OrderStageReceiptBatchID = osrCurrent.OrderStageReceiptBatchID
JOIN		OrderStageReceiptBatch osrbExisting
				ON	osrbExisting.TransmissionSequenceID = osrbCurrent.TransmissionSequenceID
JOIN		OrderStageReceipt osrExisting
				ON	osrExisting.OrderStageReceiptBatchID = osrbExisting.OrderStageReceiptBatchID
WHERE		osrCurrent.OrderStageReceiptID = @OrderStageReceiptID
AND			osrExisting.StatusID IN (2) --2: Processed

IF ISNULL(@Error, 0) = 1
BEGIN
	UPDATE	OrderStageReceipt
	SET		[StatusID] = 9 --9: OrderStage Receipt File for Order was already processed
	WHERE	OrderStageReceiptID = @OrderStageReceiptID
	
	SET @Error = CONVERT(BIT, 0)
	SET @IsOrderStageReceiptValid = CONVERT(BIT, 0)
END
GO
