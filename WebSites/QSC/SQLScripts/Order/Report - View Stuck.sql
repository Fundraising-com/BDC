  Select pl.FileName,
	 rr.BatchOrderID,
	 pl.CreateDate as CreateDate,
	 rtpl.ReportTypeId as ReportTypeID,
	 rtpl.Description as ReportType,
   	 Case  when RunDateStart is null or QueueDate is null then 'In Process - Check after few minutes' else 'Completed' end as LastStatus,  
	 Null as RSSubscriptionId  , b.*
  From QspCanadaOrderManagement.dbo.ReportRequestBatch as rr join batch b on b.orderid = rr.BatchOrderId,
       QspCanadaOrderManagement.dbo.ReportRequestBatch_PrintPickList as pl,
       QspCanadaOrderManagement.dbo.ReportRequestBatchType  rtpl  
  Where rr.id  =  pl.reportRequestBatchid 
    and  pl.pReportType  = 1    
    and  rtpl.ReportTypeId = 1  
	and (RunDateStart is null or QueueDate is null)
order by b.date desc
    --and  rr.BatchOrderID = @BatchOrderId   
	--and  rr.ShipmentGroupID   = @ShipmentGroupID