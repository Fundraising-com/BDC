USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Select_BatchReportsInfo]    Script Date: 06/07/2017 09:20:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Select_BatchReportsInfo]

@BatchOrderId int,
@ShipmentGroupID int,
@FMID varchar(4)='9999'  

AS

-- Saqib - july 2005
-- pull reports status and names. used in  BatchReportStatus.aspx
	
   IF (@FMID='9999')

   BEGIN

--pick list

  Select pl.FileName,
	 rr.BatchOrderID,
	 pl.CreateDate as CreateDate,
	 rtpl.ReportTypeId as ReportTypeID,
	 rtpl.Description as ReportType,
   	 Case  when RunDateStart is null or QueueDate is null then 'In Process - Check after few minutes' else 'Completed' end as LastStatus,  
	 Null as RSSubscriptionId  
  From QspCanadaOrderManagement.dbo.ReportRequestBatch as rr,
       QspCanadaOrderManagement.dbo.ReportRequestBatch_PrintPickList as pl,
       QspCanadaOrderManagement.dbo.ReportRequestBatchType  rtpl  
  Where rr.id  =  pl.reportRequestBatchid 
    and  pl.pReportType  = 1    
    and  rtpl.ReportTypeId = 1  
    and  rr.BatchOrderID = @BatchOrderId   
	and  rr.ShipmentGroupID   = @ShipmentGroupID

Union All

--packing slip

  Select pl.FileName,
	 rr.BatchOrderID,
	 pl.CreateDate as CreateDate ,
	 rtpl.ReportTypeId as ReportTypeID,
	 rtpl.Description as ReportType,
   	 Case  when RunDateStart is null  or QueueDate is null  then 'In Process - Check after few minutes' else 'Completed' end as LastStatus,  
	 Null as RSSubscriptionId  
  From QspCanadaOrderManagement.dbo.ReportRequestBatch as rr,
       QspCanadaOrderManagement.dbo.ReportRequestBatch_PrintPickList as pl,
       QspCanadaOrderManagement.dbo.ReportRequestBatchType  rtpl  
  Where rr.id  =  pl.reportRequestBatchid 
    and  pl.pReportType  = 2    
    and  rtpl.ReportTypeId = 2  
    and  rr.BatchOrderID = @BatchOrderId 
	and  rr.ShipmentGroupID   = @ShipmentGroupID

Union All

-- bhe

  Select bhe.FileName, 
	 rr.BatchOrderID,
	 bhe.CreateDate as CreateDate,
	 rtbhe.ReportTypeId as ReportTypeID,
	 rtbhe.Description as ReportType,
   	 Case  when RunDateStart is null  or QueueDate is null  then 'In Process - Check after few minutes' else 'Completed' end as LastStatus,  
	 Null as RSSubscriptionId  
  From QspCanadaOrderManagement.dbo.ReportRequestBatch as rr,
       QspCanadaOrderManagement.dbo.ReportRequestBatch_BHEShippingLabelsReport as bhe,
       QspCanadaOrderManagement.dbo.ReportRequestBatchType  rtbhe  
  Where rr.id  =  bhe.reportRequestBatchid   
    and  rtbhe.ReportTypeId = 3  
    and  rr.BatchOrderID = @BatchOrderId 
	and  rr.ShipmentGroupID   = @ShipmentGroupID

Union All   

--particpant listing

  Select pls.FileName, 
	 rr.BatchOrderID,
	 pls.CreateDate as CreateDate,
	 rtpls.ReportTypeId as ReportTypeID,
	 rtpls.Description as ReportType,
   	 Case  when RunDateStart is null  or QueueDate is null  then 'In Process - Check after few minutes' else 'Completed' end as LastStatus,  
	 Null as RSSubscriptionId  
  From QspCanadaOrderManagement.dbo.ReportRequestBatch as rr,
       QspCanadaOrderManagement.dbo.ReportRequestBatch_ParticipantListing as pls,
       QspCanadaOrderManagement.dbo.ReportRequestBatchType  rtpls  
  Where rr.id  =  pls.reportRequestBatchid   
    and  rtpls.ReportTypeId = 5  
    and  rr.BatchOrderID = @BatchOrderId 
	and  rr.ShipmentGroupID   = @ShipmentGroupID

Union All  

--homeroom

  Select hr.FileName, 
	 rr.BatchOrderID,
	 hr.CreateDate as CreateDate,
	 rthr.ReportTypeId as ReportTypeID,
	 rthr.Description as ReportType,
   	 Case  when RunDateStart is null  or QueueDate is null  then 'In Process - Check after few minutes' else 'Completed' end as LastStatus,  
	 Null as RSSubscriptionId  
  From QspCanadaOrderManagement.dbo.ReportRequestBatch as rr,
       QspCanadaOrderManagement.dbo.ReportRequestBatch_HomeroomSummaryReport as hr,
       QspCanadaOrderManagement.dbo.ReportRequestBatchType  rthr  
  Where rr.id  =  hr.reportRequestBatchid   
    and  rthr.ReportTypeId = 6  
    and  rr.BatchOrderID = @BatchOrderId 
	and  rr.ShipmentGroupID   = @ShipmentGroupID

Union All  

--group room

    Select gr.FileName, 
	 rr.BatchOrderID,
	 gr.CreateDate as CreateDate,
	 rt.ReportTypeId  as ReportTypeID,
	 rt.Description as ReportType,
   	 Case  when RunDateStart is null  or QueueDate is null  then 'In Process - Check after few minutes' else 'Completed' end as LastStatus,  
	 Null as RSSubscriptionId  
  From QspCanadaOrderManagement.dbo.ReportRequestBatch as rr,
       QspCanadaOrderManagement.dbo.ReportRequestBatch_GroupSummaryReport as gr,
       QspCanadaOrderManagement.dbo.ReportRequestBatchType  rt  
  Where rr.id  =  gr.reportRequestBatchid   
    and  rt.ReportTypeId   = 7  
    and  rr.BatchOrderID = @BatchOrderId 
	and  rr.ShipmentGroupID   = @ShipmentGroupID

Union All  
--mag item

  Select rep.FileName, 
	 rr.BatchOrderID,
	 rep.CreateDate as CreateDate,
	 rt.ReportTypeId  as ReportTypeID,
	 rt.Description as ReportType,
   	 Case  when RunDateStart is null  or QueueDate is null  then 'In Process - Check after few minutes' else 'Completed' end as LastStatus,  
	 Null as RSSubscriptionId  
  From QspCanadaOrderManagement.dbo.ReportRequestBatch as rr,
       QspCanadaOrderManagement.dbo.ReportRequestBatch_MagazineItemsSummary as rep,
       QspCanadaOrderManagement.dbo.ReportRequestBatchType  rt  
  Where rr.id  =  rep.reportRequestBatchid   
    and  rt.ReportTypeId   = 8  
    and  rr.BatchOrderID = @BatchOrderId 
	and  rr.ShipmentGroupID   = @ShipmentGroupID

Union All  

--problem solver

  Select rep.FileName, 
	 rr.BatchOrderID,
	 rep.CreateDate as CreateDate,
	 rt.ReportTypeId  as ReportTypeID,
	 rt.Description as ReportType,
   	 Case  when RunDateStart is null  or QueueDate is null  then 'In Process - Check after few minutes' else 'Completed' end as LastStatus,  
	 Null as RSSubscriptionId  
  From QspCanadaOrderManagement.dbo.ReportRequestBatch as rr,
       QspCanadaOrderManagement.dbo.ReportRequestBatch_ProblemSolverReport as rep,
       QspCanadaOrderManagement.dbo.ReportRequestBatchType  rt  
  Where rr.id  =  rep.reportRequestBatchid   
    and  rt.ReportTypeId   = 9  
    and  rr.BatchOrderID = @BatchOrderId 
	and  rr.ShipmentGroupID   = @ShipmentGroupID

Union All  

--teacher label

  Select rep.FileName, 
	 rr.BatchOrderID,
	 rep.CreateDate as CreateDate,
	 rt.ReportTypeId  as ReportTypeID,
	 rt.Description as ReportType,
   	 Case  when RunDateStart is null  or QueueDate is null  then 'In Process - Check after few minutes' else 'Completed' end as LastStatus,  
	 Null as RSSubscriptionId  
  From QspCanadaOrderManagement.dbo.ReportRequestBatch as rr,
       QspCanadaOrderManagement.dbo.ReportRequestBatch_TeacherBoxLabelsReport as rep,
       QspCanadaOrderManagement.dbo.ReportRequestBatchType  rt  
  Where rr.id  =  rep.reportRequestBatchid   
    and  rt.ReportTypeId   = 4  
    and  rr.BatchOrderID = @BatchOrderId 
	and  rr.ShipmentGroupID   = @ShipmentGroupID

Union All  

--OE

  Select rep.FileName, 
	 rr.BatchOrderID,
	 rep.CreateDate as CreateDate,
	 rt.ReportTypeId  as ReportTypeID,
	 rt.Description as ReportType,
   	 Case  when RunDateStart is null  or QueueDate is null  then 'In Process - Check after few minutes' else 'Completed' end as LastStatus,  
	 Null as RSSubscriptionId  
  From QspCanadaOrderManagement.dbo.ReportRequestBatch as rr,
       QspCanadaOrderManagement.dbo.ReportRequestBatch_OrderEntryFollowupReport as rep,
       QspCanadaOrderManagement.dbo.ReportRequestBatchType  rt  
  Where rr.id  =  rep.reportRequestBatchid   
    and  rt.ReportTypeId   = 10  
    and  rr.BatchOrderID = @BatchOrderId 
	and  rr.ShipmentGroupID   = @ShipmentGroupID

Union All  

-- Price desc

  Select rep.FileName, 
	 rr.BatchOrderID,
	 rep.CreateDate as CreateDate,
	 rt.ReportTypeId  as ReportTypeID,
	 rt.Description as ReportType,
   	 Case  when RunDateStart is null  or QueueDate is null  then 'In Process - Check after few minutes' else 'Completed' end as LastStatus,  
	 Null as RSSubscriptionId  
  From QspCanadaOrderManagement.dbo.ReportRequestBatch as rr,
       QspCanadaOrderManagement.dbo.ReportRequestBatch_PriceDiscrepancyReport as rep,
       QspCanadaOrderManagement.dbo.ReportRequestBatchType  rt  
  Where rr.id  =  rep.reportRequestBatchid   
    and  rt.ReportTypeId   = 11  
    and  rr.BatchOrderID = @BatchOrderId 
	and  rr.ShipmentGroupID   = @ShipmentGroupID
ORDER BY  CreateDate Desc

END

  ELSE  -- DONT LET FM SEE W/H STUFF

  Begin 


  Select pl.FileName,
	 rr.BatchOrderID,
	 pl.CreateDate as CreateDate,
	 rtpl.ReportTypeId as ReportTypeID,
	 rtpl.Description as ReportType,
   	 Case  when RunDateStart is null  or QueueDate is null  then 'In Process - Check after few minutes' else 'Completed' end as LastStatus,  
	 Null as RSSubscriptionId  
  From QspCanadaOrderManagement.dbo.ReportRequestBatch as rr,
       QspCanadaOrderManagement.dbo.ReportRequestBatch_PrintPickList as pl,
       QspCanadaOrderManagement.dbo.ReportRequestBatchType  rtpl  
  Where rr.id  =  pl.reportRequestBatchid 
    and  pl.pReportType  = 1    
    and  rtpl.ReportTypeId = 1  
    and  rr.BatchOrderID = @BatchOrderId     
	and  rr.ShipmentGroupID   = @ShipmentGroupID

Union All



  Select pls.FileName, 
	 rr.BatchOrderID,
	 pls.CreateDate as CreateDate,
	 rtpls.ReportTypeId as ReportTypeID,
	 rtpls.Description as ReportType,
   	 Case  when RunDateStart is null  or QueueDate is null  then 'In Process - Check after few minutes' else 'Completed' end as LastStatus,  
	 Null as RSSubscriptionId  
  From QspCanadaOrderManagement.dbo.ReportRequestBatch as rr,
       QspCanadaOrderManagement.dbo.ReportRequestBatch_ParticipantListing as pls,
       QspCanadaOrderManagement.dbo.ReportRequestBatchType  rtpls  
  Where rr.id  =  pls.reportRequestBatchid   
    and  rtpls.ReportTypeId = 5  
    and  rr.BatchOrderID = @BatchOrderId 
	and  rr.ShipmentGroupID   = @ShipmentGroupID

Union All  

  Select hr.FileName, 
	 rr.BatchOrderID,
	 hr.CreateDate as CreateDate,
	 rthr.ReportTypeId as ReportTypeID,
	 rthr.Description as ReportType,
   	 Case  when RunDateStart is null  or QueueDate is null  then 'In Process - Check after few minutes' else 'Completed' end as LastStatus,  
	 Null as RSSubscriptionId  
  From QspCanadaOrderManagement.dbo.ReportRequestBatch as rr,
       QspCanadaOrderManagement.dbo.ReportRequestBatch_HomeroomSummaryReport as hr,
       QspCanadaOrderManagement.dbo.ReportRequestBatchType  rthr  
  Where rr.id  =  hr.reportRequestBatchid   
    and  rthr.ReportTypeId = 6  
    and  rr.BatchOrderID = @BatchOrderId 
	and  rr.ShipmentGroupID   = @ShipmentGroupID

Union All  

    Select gr.FileName, 
	 rr.BatchOrderID,
	 gr.CreateDate as CreateDate,
	 rt.ReportTypeId  as ReportTypeID,
	 rt.Description as ReportType,
   	 Case  when RunDateStart is null  or QueueDate is null  then 'In Process - Check after few minutes' else 'Completed' end as LastStatus,  
	 Null as RSSubscriptionId  
  From QspCanadaOrderManagement.dbo.ReportRequestBatch as rr,
       QspCanadaOrderManagement.dbo.ReportRequestBatch_GroupSummaryReport as gr,
       QspCanadaOrderManagement.dbo.ReportRequestBatchType  rt  
  Where rr.id  =  gr.reportRequestBatchid   
    and  rt.ReportTypeId   = 7  
    and  rr.BatchOrderID = @BatchOrderId 
	and  rr.ShipmentGroupID   = @ShipmentGroupID

Union All  

  Select rep.FileName, 
	 rr.BatchOrderID,
	 rep.CreateDate as CreateDate,
	 rt.ReportTypeId  as ReportTypeID,
	 rt.Description as ReportType,
   	 Case  when RunDateStart is null  or QueueDate is null  then 'In Process - Check after few minutes' else 'Completed' end as LastStatus,  
	 Null as RSSubscriptionId  
  From QspCanadaOrderManagement.dbo.ReportRequestBatch as rr,
       QspCanadaOrderManagement.dbo.ReportRequestBatch_MagazineItemsSummary as rep,
       QspCanadaOrderManagement.dbo.ReportRequestBatchType  rt  
  Where rr.id  =  rep.reportRequestBatchid   
    and  rt.ReportTypeId   = 8  
    and  rr.BatchOrderID = @BatchOrderId 
	and  rr.ShipmentGroupID   = @ShipmentGroupID

Union All  

  Select rep.FileName, 
	 rr.BatchOrderID,
	 rep.CreateDate as CreateDate,
	 rt.ReportTypeId  as ReportTypeID,
	 rt.Description as ReportType,
   	 Case  when RunDateStart is null  or QueueDate is null  then 'In Process - Check after few minutes' else 'Completed' end as LastStatus,  
	 Null as RSSubscriptionId  
  From QspCanadaOrderManagement.dbo.ReportRequestBatch as rr,
       QspCanadaOrderManagement.dbo.ReportRequestBatch_ProblemSolverReport as rep,
       QspCanadaOrderManagement.dbo.ReportRequestBatchType  rt  
  Where rr.id  =  rep.reportRequestBatchid   
    and  rt.ReportTypeId   = 9  
    and  rr.BatchOrderID = @BatchOrderId 
	and  rr.ShipmentGroupID   = @ShipmentGroupID

Union All  


--OE

  Select rep.FileName, 
	 rr.BatchOrderID,
	 rep.CreateDate as CreateDate,
	 rt.ReportTypeId  as ReportTypeID,
	 rt.Description as ReportType,
   	 Case  when RunDateStart is null  or QueueDate is null  then 'In Process - Check after few minutes' else 'Completed' end as LastStatus,  
	 Null as RSSubscriptionId  
  From QspCanadaOrderManagement.dbo.ReportRequestBatch as rr,
       QspCanadaOrderManagement.dbo.ReportRequestBatch_OrderEntryFollowupReport as rep,
       QspCanadaOrderManagement.dbo.ReportRequestBatchType  rt  
  Where rr.id  =  rep.reportRequestBatchid   
    and  rt.ReportTypeId   = 10  
    and  rr.BatchOrderID = @BatchOrderId 
	and  rr.ShipmentGroupID   = @ShipmentGroupID

Union All  



-- Price desc

  Select rep.FileName, 
	 rr.BatchOrderID,
	 rep.CreateDate as CreateDate,
	 rt.ReportTypeId  as ReportTypeID,
	 rt.Description as ReportType,
   	 Case  when RunDateStart is null  or QueueDate is null  then 'In Process - Check after few minutes' else 'Completed' end as LastStatus,  
	 Null as RSSubscriptionId  
  From QspCanadaOrderManagement.dbo.ReportRequestBatch as rr,
       QspCanadaOrderManagement.dbo.ReportRequestBatch_PriceDiscrepancyReport as rep,
       QspCanadaOrderManagement.dbo.ReportRequestBatchType  rt  
  Where rr.id  =  rep.reportRequestBatchid   
    and  rt.ReportTypeId   = 11  
    and  rr.BatchOrderID = @BatchOrderId 
	and  rr.ShipmentGroupID   = @ShipmentGroupID
ORDER BY  CreateDate Desc
	

END
GO
