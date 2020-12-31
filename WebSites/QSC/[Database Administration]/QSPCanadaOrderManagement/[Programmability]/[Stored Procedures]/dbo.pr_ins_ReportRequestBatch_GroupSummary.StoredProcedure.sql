USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ins_ReportRequestBatch_GroupSummary]    Script Date: 06/07/2017 09:20:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ins_ReportRequestBatch_GroupSummary]
  @ReportRequestBatchID int
, @pBatchId int
, @pBatchDate datetime
, @CreatedBy int = null
AS

INSERT INTO dbo.ReportRequestBatch_GroupSummaryReport (
	  ReportRequestBatchID
	, pBatchId
	, pBatchDate
	 ,CreateDate
	, CreatedBy
) VALUES (
	  @ReportRequestBatchID
	, @pBatchId
	, @pBatchDate
	,GetDate()
	, @CreatedBy
);
GO
