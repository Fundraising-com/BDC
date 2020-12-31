USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GetFilesForPrint]    Script Date: 06/07/2017 09:20:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_GetFilesForPrint]

@pOrderID int,
@pShipmentGroupID int = null

AS
--saqib may 2005



DECLARE @BatchID int, @BatchDATE datetime, @BHE_WH int, @Prizes_WH int

SELECT @BatchID 	= B.[id],
	 @BatchDATE 	= B.[date]
FROM	QSPCanadaOrderManagement.dbo.Batch B
WHERE OrderId = @pOrderID


  Select top 1 @BHE_WH = DistributionCenterID
  from QSPCanadaOrderManagement..BatchDistributionCenter 
  where batchID =    @BatchID
  and BatchDate  =  @BatchDATE
  and QspProductLine in ( 46006,46007,46012) --bhe items

  set @BHE_WH = isnull(@BHE_WH,1)


  Select top 1 @Prizes_WH = DistributionCenterID
  from QSPCanadaOrderManagement..BatchDistributionCenter 
  where batchID =    @BatchID
  and BatchDate  =  @BatchDATE
  and QspProductLine in ( 46013,46014,46015) --prizes (for teacher box labels)

 set @Prizes_WH  = isnull(@Prizes_WH,1)


--IMPORTANT- please do not change the columns sequence, local variables are storing them in seqeunce in aspx

 Select	pp.[FileName] as 'PickList',
	ps.[FileName] as 'PackingSlip',
	bhe.[FileName]as 'BHE',
	pl.[FileName]as 'ParticipantListing',
	hr.[FileName]as 'HomeRoom',
	gr.[FileName]as 'GroupRoom',
	mi.[FileName]as 'MagazineItems',
	pr.[FileName]as 'ProblemSolver',
	tb.[FileName]as 'TeacherBox',
	oe.[FileName]as 'OrderEntry',
	pd.[FileName]as 'PriceDiscrepancy',
	@BHE_WH as 'BHEWH',
	@Prizes_WH as 'PrizesWH'
 from 	QspCanadaorderManagement.dbo.ReportRequestBatch  rb,
      	QspCanadaorderManagement.dbo.ReportRequestBatch_PrintPickList  pp,
      	QspCanadaorderManagement.dbo.ReportRequestBatch_PrintPickList  ps,
	QspCanadaorderManagement.dbo.ReportRequestBatch_BHEShippingLabelsReport as bhe, 
	QspCanadaorderManagement.dbo.ReportRequestBatch_ParticipantListing pl, 
	QspCanadaorderManagement.dbo.ReportRequestBatch_HomeroomSummaryReport hr, 
	QspCanadaorderManagement.dbo.ReportRequestBatch_GroupSummaryReport gr, 
	QspCanadaorderManagement.dbo.ReportRequestBatch_MagazineItemsSummary mi, 
	QspCanadaorderManagement.dbo.ReportRequestBatch_ProblemSolverReport pr,  
	QspCanadaorderManagement.dbo.ReportRequestBatch_TeacherBoxLabelsReport tb, 
	QspCanadaorderManagement.dbo.ReportRequestBatch_OrderEntryFollowupReport oe,
	QspCanadaorderManagement.dbo.ReportRequestBatch_PriceDiscrepancyReport pd 
 where (rb.id  *= pp.ReportRequestBatchId and pp.pReportType = 1) 
       and (rb.id  *= ps.ReportRequestBatchId  and ps.pReportType = 2)
       and rb.id  *= bhe.ReportRequestBatchId
       and rb.id  *= pl.ReportRequestBatchId
       and rb.id  *= hr.ReportRequestBatchId
       and rb.id  *= gr.ReportRequestBatchId
       and rb.id  *= mi.ReportRequestBatchId
       and rb.id  *= pr.ReportRequestBatchId
       and rb.id  *= tb.ReportRequestBatchId
       and rb.id  *= oe.ReportRequestBatchId
       and rb.id  *= pd.ReportRequestBatchId
       and rb.BatchOrderId = @pOrderID
	   and rb.ShipmentGroupID = @pShipmentGroupID
	   and rb.IsPrinted = 0
GO
