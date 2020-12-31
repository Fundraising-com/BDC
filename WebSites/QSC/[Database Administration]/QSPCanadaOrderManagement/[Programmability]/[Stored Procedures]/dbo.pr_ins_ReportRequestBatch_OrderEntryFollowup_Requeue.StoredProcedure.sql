USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ins_ReportRequestBatch_OrderEntryFollowup_Requeue]    Script Date: 06/07/2017 09:20:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ins_ReportRequestBatch_OrderEntryFollowup_Requeue]
  
	@OrderID INT

AS

UPDATE	ReportRequestBatch_OrderEntryFollowupReport
SET		CreateDate = GETDATE(),
		QUEUEDATE = NULL,
		RUNDATESTART = NULL,
		[FILENAME] = NULL 
WHERE	ReportRequestBatchID IN (SELECT	ID
								FROM	ReportRequestBatch
								WHERE	BatchOrderID = @OrderID)
GO
