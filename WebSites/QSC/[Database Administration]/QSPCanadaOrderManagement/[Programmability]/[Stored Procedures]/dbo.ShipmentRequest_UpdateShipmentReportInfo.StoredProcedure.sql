USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ShipmentRequest_UpdateShipmentReportInfo]    Script Date: 06/07/2017 09:20:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShipmentRequest_UpdateShipmentReportInfo]

	@OrderID	INT,
	@FileName	VARCHAR(200)

AS

UPDATE	OrderStageTracking
SET		PDFFilename = @FileName + '.pdf'
WHERE	OrderID = @OrderID

UPDATE	ReportRequestBatch
SET		IsMerged = 1
WHERE	BatchOrderID = @OrderID
GO
