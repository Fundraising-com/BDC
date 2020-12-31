USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ins_ReportRequestBatch_OrderEntryFollowup]    Script Date: 06/07/2017 09:20:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ins_ReportRequestBatch_OrderEntryFollowup]
  @ReportRequestBatchID int
, @pAccountID int = null
, @pCampaignID int = null
, @CreatedBy int = null
AS

INSERT INTO dbo.ReportRequestBatch_OrderEntryFollowupReport (
	  ReportRequestBatchID
	, pAccountID
	, pCampaignID
	 ,CreateDate
	, CreatedBy
) VALUES (
	  @ReportRequestBatchID
	, @pAccountID
	, @pCampaignID
	,Getdate()
	, @CreatedBy
);
GO
