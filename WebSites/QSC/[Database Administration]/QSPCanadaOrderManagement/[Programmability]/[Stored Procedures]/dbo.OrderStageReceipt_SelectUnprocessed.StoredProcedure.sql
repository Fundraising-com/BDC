USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[OrderStageReceipt_SelectUnprocessed]    Script Date: 06/07/2017 09:19:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[OrderStageReceipt_SelectUnprocessed]

	@OrderStageReceiptBatchID	INT

AS

SELECT		osr.OrderStageReceiptID
FROM		OrderStageReceipt osr
WHERE		osr.StatusID IN (1) --1: Unprocessed
AND			osr.OrderStageReceiptBatchID = @OrderStageReceiptBatchID
AND			NOT EXISTS (SELECT	1
						FROM	OrderStageReceipt osrError
						WHERE	osrError.OrderStageReceiptBatchID = osr.OrderStageReceiptBatchID
						AND		osrError.StatusID >= 3) --Another receipt in same file is in Error
ORDER BY	osr.OrderStageReceiptID
GO
