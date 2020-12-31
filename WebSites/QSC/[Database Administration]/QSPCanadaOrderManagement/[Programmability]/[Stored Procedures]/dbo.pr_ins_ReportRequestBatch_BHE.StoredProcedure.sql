USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ins_ReportRequestBatch_BHE]    Script Date: 06/07/2017 09:20:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ins_ReportRequestBatch_BHE]
  @ReportRequestBatchID int
, @CreatedBy int = null
AS

INSERT INTO dbo.ReportRequestBatch_BHEShippingLabelsReport (
	  ReportRequestBatchID
	 ,CreateDate
	, CreatedBy
) VALUES (
	  @ReportRequestBatchID
	,Getdate()
	, @CreatedBy
) ;
GO
