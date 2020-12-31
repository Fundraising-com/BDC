USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ins_ReportRequestBatch_MagazineItemsSummary]    Script Date: 06/07/2017 09:20:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ins_ReportRequestBatch_MagazineItemsSummary]
  @ReportRequestBatchID int
, @Lang varchar(2)
, @pCampaignID int
, @CreatedBy int = null
AS

INSERT INTO dbo.ReportRequestBatch_MagazineItemsSummary (
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
