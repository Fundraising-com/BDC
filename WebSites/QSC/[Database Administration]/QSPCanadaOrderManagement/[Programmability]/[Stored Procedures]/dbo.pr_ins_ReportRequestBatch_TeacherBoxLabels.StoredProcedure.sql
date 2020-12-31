USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ins_ReportRequestBatch_TeacherBoxLabels]    Script Date: 06/07/2017 09:20:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ins_ReportRequestBatch_TeacherBoxLabels]
  @ReportRequestBatchID int
, @Lang varchar(2)
, @pTeacherID int = null
, @pTotalLabels int = null
, @CreatedBy int = null
AS

INSERT INTO dbo.ReportRequestBatch_TeacherBoxLabelsReport (
	  ReportRequestBatchID
	, Lang
	, pTeacherID
	, pTotalLabels
	 ,CreateDate
	, CreatedBy
) VALUES (
	  @ReportRequestBatchID
	, @Lang
	, @pTeacherID
	, @pTotalLabels
	,Getdate()
	, @CreatedBy
);
GO
