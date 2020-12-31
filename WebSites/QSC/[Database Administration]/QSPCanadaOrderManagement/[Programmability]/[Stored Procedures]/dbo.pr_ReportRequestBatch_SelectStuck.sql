USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ReportRequestBatch_SelectStuck]    Script Date: 06/07/2017 09:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ReportRequestBatch_SelectStuck]

AS

SELECT		pl.[FileName],
			rrb.BatchOrderID OrderID,
			pl.CreateDate CreateDate
FROM		ReportRequestBatch rrb
JOIN		ReportRequestBatch_PrintPickList pl ON pl.ReportRequestBatchID = rrb.ID
WHERE		pl.pReportType  = 1    
AND			(RunDateStart IS NULL OR QueueDate IS NULL)
AND			pl.CreateDate >= '2018-07-01'

GO
