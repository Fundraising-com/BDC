USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_pick_order]    Script Date: 06/07/2017 09:20:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_pick_order]
	  @OrderID int
	, @DistributionCenterID int
	, @CreatedBY int = null
as


-- based on the ProducePickListChoose page from the web app
-----------------------------------------------------------------------------------

/* 
  * Get Batch info By OrderId 
  *************************************************************/
DECLARE @BatchID int
DECLARE @BatchDATE datetime
DECLARE @CampaignID int
DECLARE @Language varchar(10)

SELECT
	 @BatchID 		= B.[id]
	,@BatchDATE 	= B.[date]
	,@CampaignID 	= B.CampaignID 
	,@Language 		= UPPER(C.Lang)  --assumes 'fr' and 'en' only, doesn't look for 'en-CA' etc.
FROM
	QSPCanadaOrderManagement.dbo.Batch B
	LEFT JOIN QSPCanadaCommon.dbo.CAccount C ON B.AccountId = C.[Id]
WHERE
	OrderId = @OrderID

/* 
  * Add Statii to BatchDistributionCenter table
  *************************************************************/
/*exec QSPCanadaOrderManagement.dbo.pr_Insert_BatchDistributionCenter
  @DistributionCenterId 	= @DistributionCenterID
, @OrderId 			= @OrderID ; 
*/
/* 
  * Reserve Quantities (Individual Statuses will be updated)
  * Set Status To Picked For Batch
  *************************************************************/
/*exec QSPCanadaOrderManagement.dbo.pr_ProcessReserveQuantities_ByOrderId
  @DistributionCenterId 	= @DistributionCenterID
, @OrderId 			= @OrderID ; 
*/


/* 
  * Generate Reports
  * This setup will generate all reports for all batchs
  * TODO: add logic to determine what reports to run, 
  * by batch type, campaign type, programs running, etc
  * ALSO : how many copies, should certain reports by run twice as they currently are ? 
  *************************************************************/

DECLARE @RRBID int
exec dbo.pr_ins_ReportRequestBatch 
@BatchOrderId 		= @OrderID
, @TypeId 			= 1 -- ?? 1 = picked order, 2 = one off report ? this hasnt been defined yet.
, @CreatedBy 		= @CreatedBY 
, @ReportRequestBatchID 	= @RRBID OUTPUT ;

--PickList
exec dbo.pr_ins_ReportRequestBatch_PrintPickList 
@ReportRequestBatchID 	= @RRBID
--, @pOrderId 			= @OrderID
, @pBatchId 			= @BatchID
, @pBatchDate 		= @BatchDATE
, @pReportType 		= 1 --1 is PickList, 2 is Packing Slip
, @pShipDateFrom 		= null
, @pShipDateTo 		= null
, @CreatedBy 		= @CreatedBY ;

exec dbo.pr_ins_ReportRequestBatch_BHEShippingLabelsReport 
@ReportRequestBatchID 	= @RRBID
--, @pOrderId 			= @OrderID
, @CreatedBy 		= @CreatedBY ;

exec dbo.pr_ins_ReportRequestBatch_ParticipantListing
@ReportRequestBatchID 	= @RRBID
, @Lang 			= @Language
--, @pOrderId 			= @OrderID
, @CreatedBy 		= @CreatedBY ;

exec dbo.pr_ins_ReportRequestBatch_HomeroomSummaryReport
@ReportRequestBatchID 	= @RRBID
--, @pOrderId 			= @OrderID
, @pBatchId 			= @BatchID
, @pBatchDate 		= @BatchDATE
, @CreatedBy 		= @CreatedBY ;

exec dbo.pr_ins_ReportRequestBatch_GroupSummaryReport
@ReportRequestBatchID 	= @RRBID
--, @pOrderId 			= @OrderID
, @pBatchId 			= @BatchID
, @pBatchDate 		= @BatchDATE
, @CreatedBy 		= @CreatedBY ;

exec dbo.pr_ins_ReportRequestBatch_MagazineItemsSummary
@ReportRequestBatchID 	= @RRBID
, @Lang 			= @Language
--, @pOrderId 			= @OrderID
, @pCampaignID 		= @CampaignID
, @CreatedBy 		= @CreatedBY ;

exec dbo.pr_ins_ReportRequestBatch_ProblemSolverReport
@ReportRequestBatchID 	= @RRBID
, @Lang 			= @Language
--, @pOrderId 			= @OrderID
, @pCampaignID 		= @CampaignID
, @CreatedBy 		= @CreatedBY ;

exec dbo.pr_ins_ReportRequestBatch_TeacherBoxLabelsReport 
@ReportRequestBatchID 	= @RRBID
, @Lang 			= @Language
--, @pOrderId 			= @OrderID
, @pTeacherID 		= null
, @pTotalLabels 		= null
, @CreatedBy 		= @CreatedBY ;

exec dbo.pr_ins_ReportRequestBatch_OrderEntryFollowupReport
@ReportRequestBatchID 	= @RRBID
, @Lang 			= @Language
--, @pOrderId 			= @OrderID
, @pAccountID 		= null
, @pCampaignID 		= null
, @CreatedBy 		= @CreatedBY ;

exec dbo.pr_ins_ReportRequestBatch_PriceDiscrepancyReport
@ReportRequestBatchID 	= @RRBID
--, @pOrderId 			= @OrderID
, @pAccountID 		= null
, @pCampaignID 		= null
, @pFMID 			= null
, @pInvoiceID 		= null
, @CreatedBy 		= @CreatedBY ;

--Packing Slip
exec dbo.pr_ins_ReportRequestBatch_PrintPickList 
@ReportRequestBatchID 	= @RRBID
--, @pOrderId 			= @OrderID
, @pBatchId 			= @BatchID
, @pBatchDate 		= @BatchDATE
, @pReportType 		= 2 --1 is PickList, 2 is Packing Slip
, @pShipDateFrom 		= null
, @pShipDateTo 		= null
, @CreatedBy 		= @CreatedBY ;
GO
