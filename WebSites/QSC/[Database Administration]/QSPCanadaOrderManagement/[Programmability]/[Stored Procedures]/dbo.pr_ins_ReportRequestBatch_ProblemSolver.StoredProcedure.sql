USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ins_ReportRequestBatch_ProblemSolver]    Script Date: 06/07/2017 09:20:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ins_ReportRequestBatch_ProblemSolver]
  @ReportRequestBatchID int
, @Lang varchar(2)
, @pCampaignID int
, @CreatedBy int = null
AS

INSERT INTO dbo.ReportRequestBatch_ProblemSolverReport (
	  ReportRequestBatchID
	, Lang
	, pCampaignID
	 ,CreateDate
	, CreatedBy
) VALUES (
	  @ReportRequestBatchID
	, @Lang
	, @pCampaignID
	,GetDate()
	, @CreatedBy
);
GO
