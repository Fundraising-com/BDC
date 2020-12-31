USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ins_ReportRequestBatch_PriceDiscrepancyReport]    Script Date: 06/07/2017 09:20:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ins_ReportRequestBatch_PriceDiscrepancyReport]
  @ReportRequestBatchID int
, @pAccountID int = null
, @pCampaignID int = null
, @pFMID varchar(4) = null
, @pInvoiceID int = null
, @CreatedBy int = null
AS

INSERT INTO dbo.ReportRequestBatch_PriceDiscrepancyReport (
	  ReportRequestBatchID
	, pAccountID
	, pCampaignID
	, pFMID
	, pInvoiceID
	, CreatedBy
) VALUES (
	  @ReportRequestBatchID
	, @pAccountID
	, @pCampaignID
	, @pFMID
	, @pInvoiceID
	, @CreatedBy
);
GO
