USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ins_ReportRequestBatch_PrintPickList]    Script Date: 06/07/2017 09:20:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ins_ReportRequestBatch_PrintPickList]
  @ReportRequestBatchID int
, @pBatchId int
, @pBatchDate datetime
, @pReportType int
, @pShipDateFrom datetime = null
, @pShipDateTo datetime = null
, @CreatedBy int = null
AS

INSERT INTO dbo.ReportRequestBatch_PrintPickList (
	  ReportRequestBatchID
	, pBatchId
	, pBatchDate
	, pReportType
	, pShipDateFrom
	, pShipDateTo
	 ,CreateDate
	, CreatedBy
) VALUES (
	  @ReportRequestBatchID
	, @pBatchId
	, @pBatchDate
	, @pReportType
	, @pShipDateFrom
	, @pShipDateTo
	,GetDate()
	, @CreatedBy
)
GO
