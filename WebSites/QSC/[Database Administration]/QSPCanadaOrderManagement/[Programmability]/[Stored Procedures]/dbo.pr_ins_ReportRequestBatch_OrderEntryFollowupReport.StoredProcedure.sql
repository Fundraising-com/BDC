USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ins_ReportRequestBatch_OrderEntryFollowupReport]    Script Date: 06/07/2017 09:20:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ins_ReportRequestBatch_OrderEntryFollowupReport]
  @ReportRequestBatchID int
, @pAccountID int = null
, @pCampaignID int = null
, @CreatedBy int = null
AS

INSERT INTO dbo.ReportRequestBatch_OrderEntryFollowupReport (
	  ReportRequestBatchID
	, pAccountID
	, pCampaignID
	, CreatedBy
) VALUES (
	  @ReportRequestBatchID
	, @pAccountID
	, @pCampaignID
	, @CreatedBy
);
GO
