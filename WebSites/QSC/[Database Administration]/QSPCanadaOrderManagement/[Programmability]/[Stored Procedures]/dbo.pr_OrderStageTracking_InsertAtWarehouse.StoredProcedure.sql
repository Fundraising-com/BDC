USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_OrderStageTracking_InsertAtWarehouse]    Script Date: 06/07/2017 09:20:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_OrderStageTracking_InsertAtWarehouse]

	@OrderID INT

AS

DECLARE @ShippableItemCount INT

SELECT	@ShippableItemCount = COUNT(*)
FROM	BatchDistributionCenter bdc
JOIN	Batch b ON b.ID = bdc.BatchID AND b.Date = bdc.BatchDate
WHERE	bdc.DistributionCenterID = 1
AND		b.OrderID = @orderid
				
IF @ShippableItemCount > 0
BEGIN
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
			camp.ID,
			@orderid,
			@PDFFileName,
			NULL,
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
	WHERE	b.OrderID = @orderid

END
GO
