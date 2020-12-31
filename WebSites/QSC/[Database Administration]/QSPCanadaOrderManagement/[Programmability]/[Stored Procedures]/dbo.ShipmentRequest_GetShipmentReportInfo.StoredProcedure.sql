USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ShipmentRequest_GetShipmentReportInfo]    Script Date: 06/07/2017 09:20:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShipmentRequest_GetShipmentReportInfo]

	@OrderID	INT

AS

SELECT	'GroupSummary' AS [Type],
		[Filename]
FROM	ReportRequestBatch_GroupSummaryReport rrbgsr
JOIN	ReportRequestBatch rrb
			ON	rrbgsr.ReportRequestBatchID = rrb.ID
WHERE	rrb.BatchOrderID = @OrderID
AND		rrbgsr.[Filename] IS NOT NULL

UNION ALL

SELECT	'HomeRoomSummary' AS [Type],
		[Filename]
FROM	ReportRequestBatch_HomeRoomSummaryReport rrbhrsr
JOIN	ReportRequestBatch rrb
			ON	rrbhrsr.ReportRequestBatchID = rrb.ID
WHERE	rrb.BatchOrderID = @OrderID

UNION ALL

SELECT	'ParticipantListing' AS [Type],
		[Filename]
FROM	ReportRequestBatch_ParticipantListing rrbpl
JOIN	ReportRequestBatch rrb
			ON	rrbpl.ReportRequestBatchID = rrb.ID
WHERE	rrb.BatchOrderID = @OrderID

UNION ALL

SELECT	'ProblemSolver' AS [Type],
		[Filename]
FROM	ReportRequestBatch_ProblemSolverReport rrbps
JOIN	ReportRequestBatch rrb
			ON	rrbps.ReportRequestBatchID = rrb.ID
WHERE	rrb.BatchOrderID = @OrderID

UNION ALL

SELECT	'OrderEntryFollowup' AS [Type],
		[Filename]
FROM	ReportRequestBatch_OrderEntryFollowupReport rrboefur
JOIN	ReportRequestBatch rrb
			ON	rrboefur.ReportRequestBatchID = rrb.ID
WHERE	rrb.BatchOrderID = @OrderID

UNION ALL

SELECT	'PickList' AS [Type],
		[Filename]
FROM	ReportRequestBatch_PrintPickList rrbppl
JOIN	ReportRequestBatch rrb
			ON	rrbppl.ReportRequestBatchID = rrb.ID
WHERE	rrb.BatchOrderID = @OrderID
AND		rrbppl.pReportType = 1
   
ORDER BY [Type]
GO
