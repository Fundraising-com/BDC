USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ShipmentRequest_UpdateShipmentInfo]    Script Date: 06/07/2017 09:20:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShipmentRequest_UpdateShipmentInfo]

	@ShipmentRequestBatchID		INT,
	@ShipmentRequestFilename	VARCHAR(100)

AS

DECLARE	@CampaignID			INT,
		@OrderID			INT

SELECT	TOP 1 --It is assumed there is only 1 OrderID per file
		@OrderID = b.OrderID,
		@CampaignID = b.CampaignID
FROM	ShipmentRequestBatch srb
JOIN	ShipmentRequestOrder sro
			ON	sro.ShipmentRequestBatchID = srb.ShipmentRequestBatchID
JOIN	Batch b
			ON	b.OrderID = sro.OrderID
WHERE	srb.ShipmentRequestBatchID = @ShipmentRequestBatchID

DECLARE @ReceiptDate		DATETIME,
		@ImageDate			DATETIME,
		@DataCaptureDate	DATETIME,
		@VerificationDate	DATETIME,
		@EditDate			DATETIME,
		@TransmitDate		DATETIME,
		@ScanCount			INT,
		@FileName			VARCHAR(100),
		@PDFFileName		VARCHAR(100)

SELECT	@ReceiptDate = MAX(ReceiptDate),
		@ImageDate = MAX(ImageDate),
		@DataCaptureDate = MAX(DataCaptureDate),
		@VerificationDate = MAX(VerificationDate),
		@EditDate = MAX(EditDate),
		@TransmitDate = MAX(TransmitDate),
		@ScanCount = MIN(ScanCount),
		@FileName = MAX(ResolveFileName)
FROM	OrderStageTracking
WHERE	OrderID = @OrderID

SELECT	@PDFFileName =	CASE rrb.IsQSPPrint
							WHEN 0 THEN CONVERT(VARCHAR, @OrderID) + '.pdf'
							ELSE		NULL
						END
FROM	ReportRequestBatch rrb
WHERE	rrb.BatchOrderID = @OrderID


BEGIN TRANSACTION

INSERT	OrderStageTracking
(
	StageDate,
	CampaignID,
	OrderID,
	PDFFileName,
	BatchFilename,
	Stage, 
	Scancount,
	GroupID,
	GroupName,
	FMID,
	FMName,
	ReceiptDate,
	ImageDate,
	DataCaptureDate,
	VerificationDate,
	EditDate,
	TransmitDate,
	ResolveFilename
)
SELECT	CONVERT(VARCHAR, GETDATE(), 101),
		@CampaignID,
		@OrderID,
		@PDFFileName,
		@ShipmentRequestFileName,
		59006, --59006: At Unigisitx
		@ScanCount,
		acc.ID,
		acc.Name,
		camp.FMID,
		fm.LastName + ' ' + fm.FirstName,
		@ReceiptDate,
		@ImageDate,
		@DataCaptureDate,
		@VerificationDate,
		@Editdate,
		@TransmitDate,
		@FileName
FROM	Batch b
JOIN	QSPCanadaCommon..Campaign camp
			ON	camp.ID = b.CampaignID
JOIN	QSPCanadaCommon..CAccount acc
			ON	acc.ID = camp.BillToAccountID
JOIN	QSPCanadaCommon..FieldManager fm
			ON	fm.FMID = camp.FMID
WHERE	b.OrderID = @OrderID

UPDATE	bdc
SET		StatusInstance = 40012 --40012: Sent To TPL
FROM	BatchDistributionCenter bdc
JOIN	Batch b
			ON	b.ID = bdc.BatchID
			AND	b.Date = bdc.BatchDate
WHERE	b.OrderID = @OrderID
AND		bdc.DistributionCenterID = 2 --2: Unigistix

UPDATE	Batch
SET		StatusInstance = 40012 --40012: Sent To TPL
WHERE	OrderID = @OrderID

UPDATE	ShipmentRequestBatch
SET		[Filename] = @ShipmentRequestFilename
WHERE	ShipmentRequestBatchID = @ShipmentRequestBatchID

COMMIT TRANSACTION
GO
