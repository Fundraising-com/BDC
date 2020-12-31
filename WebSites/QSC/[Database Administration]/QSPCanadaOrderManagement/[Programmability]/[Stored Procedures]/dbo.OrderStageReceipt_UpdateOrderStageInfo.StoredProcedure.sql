USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[OrderStageReceipt_UpdateOrderStageInfo]    Script Date: 06/07/2017 09:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[OrderStageReceipt_UpdateOrderStageInfo]

	@OrderStageReceiptID	INT

AS

DECLARE @IsErrorInFile BIT
SET	@IsErrorInFile = 0 --Any files in error will reject the file and not get to this point

BEGIN TRANSACTION

INSERT  OrderStageTracking
(
	StageDate,
	CampaignID,
	OrderID,
	FMID,
	Stage,
	Scancount,
	GroupID,
	GroupName,
	FMName,
	ReceiptDate,
	ImageDate,
	DataCaptureDate,
	VerificationDate,
	EditDate,
	TransmitDate,
	TransmissionSequence,
	TotalReceived,
	ResolveFilename,
	ResolveFileInError,
	Units
)
SELECT	osrb.SnapShotDate,
		osr.CampaignID,
		osr.OrderID,
		osr.FMID,
		osr.StageID,
		osr.ScanCount,
		osr.GroupID,
		osr.GroupName,
		osr.FMName,
		osr.ReceiptDate,
		osr.ImageDate,
		osr.DataCaptureDate,
		osr.VerificationDate,
		osr.EditDate,
		osr.TransmitDate,
		osrb.TransmissionSequenceID,
		osrb.TotalReceived,
		osrb.FileName,
		@IsErrorInFile,
		osr.Units
FROM	OrderStageReceiptBatch osrb
JOIN	OrderStageReceipt osr
			ON	osr.OrderStageReceiptBatchID = osrb.OrderStageReceiptBatchID
WHERE	osr.OrderStageReceiptID = @OrderStageReceiptID

UPDATE	OrderStageReceipt
SET		StatusID = 2 --2: Processed
WHERE	OrderStageReceiptID = @OrderStageReceiptID

COMMIT TRANSACTION
GO
