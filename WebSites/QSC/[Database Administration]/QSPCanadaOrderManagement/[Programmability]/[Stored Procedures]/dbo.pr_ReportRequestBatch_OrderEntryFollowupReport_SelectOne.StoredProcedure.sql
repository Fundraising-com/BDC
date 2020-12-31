USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ReportRequestBatch_OrderEntryFollowupReport_SelectOne]    Script Date: 06/07/2017 09:20:31 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_ReportRequestBatch_OrderEntryFollowupReport_SelectOne]

	@iCustomerOrderHeaderInstance	int

AS

SELECT	coalesce(rrb.DatePrinted, '1995-01-01') AS DatePrinted
FROM		ReportRequestBatch_OrderEntryFollowupReport oe,
		ReportRequestBatch rrb,
		Batch b,
		CustomerOrderHeader coh
WHERE	rrb.ID = oe.ReportRequestBatchID
AND		b.OrderID = rrb.BatchOrderID
AND		coh.OrderBatchID = b.ID
AND		coh.OrderBatchDate = b.Date
AND		coh.Instance = @iCustomerOrderHeaderInstance
AND		rrb.IsPrinted = 1
GO
