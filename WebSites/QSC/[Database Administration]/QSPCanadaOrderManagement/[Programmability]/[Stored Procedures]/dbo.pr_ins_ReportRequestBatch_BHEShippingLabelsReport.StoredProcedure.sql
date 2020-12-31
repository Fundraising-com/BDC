USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ins_ReportRequestBatch_BHEShippingLabelsReport]    Script Date: 06/07/2017 09:20:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ins_ReportRequestBatch_BHEShippingLabelsReport]
  @ReportRequestBatchID int
, @CreatedBy int = null
AS

INSERT INTO dbo.ReportRequestBatch_BHEShippingLabelsReport (
	  ReportRequestBatchID
	, CreatedBy
) VALUES (
	  @ReportRequestBatchID
	, @CreatedBy
) ;
GO
