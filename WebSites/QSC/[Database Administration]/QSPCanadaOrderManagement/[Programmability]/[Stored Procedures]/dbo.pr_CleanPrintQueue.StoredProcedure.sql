USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CleanPrintQueue]    Script Date: 06/07/2017 09:19:47 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE Procedure [dbo].[pr_CleanPrintQueue]
 @OrderID int,
 @ShipmentGroupID int = null
as

delete from QspCanadaOrderManagement.dbo.[ReportRequestBatch_PrintPickList] 
where reportRequestBatchId in (select id from QspCanadaOrderManagement.dbo.[ReportRequestBatch] 
where batchorderid  = @OrderID
and (ShipmentGroupID = @ShipmentGroupID OR @ShipmentGroupID IS NULL))

delete from QspCanadaOrderManagement.dbo.[ReportRequestBatch_BHEShippingLabelsReport] 
where reportRequestBatchId in (select id from QspCanadaOrderManagement.dbo.[ReportRequestBatch] 
where batchorderid  = @OrderID
and (ShipmentGroupID = @ShipmentGroupID OR @ShipmentGroupID IS NULL))

delete from QspCanadaOrderManagement.dbo.[ReportRequestBatch_ParticipantListing] 
where reportRequestBatchId in (select id from QspCanadaOrderManagement.dbo.[ReportRequestBatch] 
where batchorderid  = @OrderID
and (ShipmentGroupID = @ShipmentGroupID OR @ShipmentGroupID IS NULL))

delete from QspCanadaOrderManagement.dbo.[ReportRequestBatch_HomeroomSummaryReport] 
where reportRequestBatchId in (select id from QspCanadaOrderManagement.dbo.[ReportRequestBatch] 
where batchorderid  = @OrderID
and (ShipmentGroupID = @ShipmentGroupID OR @ShipmentGroupID IS NULL))

delete from QspCanadaOrderManagement.dbo.[ReportRequestBatch_GroupSummaryReport] 
where reportRequestBatchId in (select id from QspCanadaOrderManagement.dbo.[ReportRequestBatch] 
where batchorderid  = @OrderID
and (ShipmentGroupID = @ShipmentGroupID OR @ShipmentGroupID IS NULL))

delete from QspCanadaOrderManagement.dbo.[ReportRequestBatch_MagazineItemsSummary] 
where reportRequestBatchId in (select id from QspCanadaOrderManagement.dbo.[ReportRequestBatch] 
where batchorderid  = @OrderID
and (ShipmentGroupID = @ShipmentGroupID OR @ShipmentGroupID IS NULL))

delete from QspCanadaOrderManagement.dbo.[ReportRequestBatch_ProblemSolverReport] 
where reportRequestBatchId in (select id from QspCanadaOrderManagement.dbo.[ReportRequestBatch] 
where batchorderid  = @OrderID
and (ShipmentGroupID = @ShipmentGroupID OR @ShipmentGroupID IS NULL))

delete from QspCanadaOrderManagement.dbo.[ReportRequestBatch_TeacherBoxLabelsReport] 
where reportRequestBatchId in (select id from QspCanadaOrderManagement.dbo.[ReportRequestBatch] 
where batchorderid  = @OrderID
and (ShipmentGroupID = @ShipmentGroupID OR @ShipmentGroupID IS NULL))

delete from QspCanadaOrderManagement.dbo.[ReportRequestBatch_OrderEntryFollowupReport] 
where reportRequestBatchId in (select id from QspCanadaOrderManagement.dbo.[ReportRequestBatch] 
where batchorderid  = @OrderID
and (ShipmentGroupID = @ShipmentGroupID OR @ShipmentGroupID IS NULL))

delete from QspCanadaOrderManagement.dbo.[ReportRequestBatch_PriceDiscrepancyReport] 
where reportRequestBatchId in (select id from QspCanadaOrderManagement.dbo.[ReportRequestBatch] 
where batchorderid  = @OrderID
and (ShipmentGroupID = @ShipmentGroupID OR @ShipmentGroupID IS NULL))

delete from QspCanadaOrderManagement.dbo.[ReportRequestBatch] 
where  batchorderid  = @OrderID
and (ShipmentGroupID = @ShipmentGroupID OR @ShipmentGroupID IS NULL)
GO
