USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_ReportRequestBatchFilename]    Script Date: 06/07/2017 09:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vw_ReportRequestBatchFilename] AS

select ReportRequestBatchID, '\\tmpqspmw2\CanadaReports\BatchReports\GroupSummary\' as path, coalesce(Filename,'') as Filename from ReportRequestBatch_GroupSummaryReport where filename is not null
union all
select ReportRequestBatchID, '\\tmpqspmw2\CanadaReports\BatchReports\HomeRoomSummary\' as path, coalesce(Filename,'') as Filename from ReportRequestBatch_HomeRoomSummaryReport
union all
select ReportRequestBatchID, '\\tmpqspmw2\CanadaReports\BatchReports\ParticipantListing\' as path, coalesce(Filename,'') as Filename from ReportRequestBatch_ParticipantListing 
union all
select ReportRequestBatchID, '\\tmpqspmw2\CanadaReports\BatchReports\ProblemSolver\' as path, coalesce(Filename,'') as Filename from ReportRequestBatch_ProblemSolverReport 
union all
select ReportRequestBatchID, '\tmpqspmw2\CanadaReports\BatchReports\OrderEntryFollowup\' as path, coalesce(Filename,'') as Filename from ReportRequestBatch_OrderEntryFollowupReport
GO
