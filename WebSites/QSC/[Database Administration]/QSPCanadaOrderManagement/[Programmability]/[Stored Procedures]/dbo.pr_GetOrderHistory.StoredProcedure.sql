USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GetOrderHistory]    Script Date: 06/07/2017 09:20:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_GetOrderHistory]
@pOrderID int,
@pCampaignID int,
@pAccountID int,
@pOrderStatus int,
@pOrderQualifier int,
@pWareHouse int,
@pFromDateReceived datetime,
@pToDateReceived datetime,
@FMID varchar(4)='9999'  

AS

SET NoCount ON

IF @pOrderID = 0  
 BEGIN
   
   SET @pOrderID = NULL
 END

IF @pCampaignID = 0  
 BEGIN
   
   SET @pCampaignID = NULL
 END

IF @pAccountID = 0  
 BEGIN
   
   SET @pAccountID = NULL
 END

IF @pOrderStatus = 0  
 BEGIN
   
   SET @pOrderStatus = NULL
 END

IF @pOrderQualifier = 0  
 BEGIN
   
   SET @pOrderQualifier = NULL
 END


IF  (	@pOrderID 		is not null or
	@pCampaignID 		is not null or
	@pAccountID  		is not null or
	@pOrderStatus 		is not null or
	@pOrderQualifier	is not null or
	@pWareHouse 		is not null or
--	@pProductLine 		is not null or
	(@pFromDateReceived 	is not null  and @pToDateReceived is not null )   

     )

BEGIN

	SELECT @pToDateReceived = DateAdd(day,1,@pToDateReceived) --to make ToDate inclusive

   IF @pWareHouse = 0  

      BEGIN

         IF (@FMID='9999') -- if not FM then 
  
        BEGIN
	
      SELECT
	 batch.OrderiD,
	 batch.CampaignID,
	 Convert(varchar(10),rr.DatePrinted,101) as DatePrinted,
   	 QSPCanadaOrderManagement.dbo.UDF_ProgramsbyCampaign( batch.CampaignID ) as Programs,
	 QSPCanadaOrderManagement.dbo.UDF_GetDistributionCentreNamesByOrder( batch.OrderiD ) as Warehouse,
	 Convert(varchar(10),DateReceived,101) as DateOrderReceived,
	 batch.StatusInstance,
	 cd.Description as BatchStatus,
	 cdQF.Description as OrderQualifier,
	 cdOT.Description as OrderType,
	 CASE WHEN batch.OrderQualifierID IN (39001, 39002, 39006, 39007, 39015, 39018, 39019, 39020, 39022, 39023) AND batch.StatusInstance IN (40002, 40010) THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END as OrderCancelAllowed

     FROM
	QSPCanadaOrderManagement.dbo.Batch Batch,
	QSPCanadaOrderManagement.dbo.ReportRequestBatch rr,
	QspCanadaCommon.dbo.CodeDetail cd,
	QspCanadaCommon.dbo.CodeDetail cdQF,
	QspCanadaCommon.dbo.CodeDetail cdOT
	
     WHERE	
	 batch.orderid  *=  rr.batchOrderId
	and batch.StatusInstance  *= cd.Instance
	and batch.OrderQualifierid  *= cdQF.Instance
	and batch.OrderTypeCode *= cdOT.Instance
    	and batch.Orderid  		= IsNull(@pOrderID, Orderid)
             and batch.CampaignID 		= IsNull(@pCampaignID, batch.CampaignID)
             and batch.AccountID 		= IsNull(@pAccountID, batch.AccountID)
             and batch.StatusInstance 	= IsNull(@pOrderStatus, batch.StatusInstance)
             and batch.OrderQualifierID 	= IsNull(@pOrderQualifier, batch.OrderQualifierID)
	and batch.DateReceived >= IsNull(@pFromDateReceived, batch.DateReceived)    
	and batch.DateReceived <= IsNull(@pToDateReceived, batch.DateReceived)

     order by batch.AccountID, batch.CampaignID,batch.OrderiD

    END

   ELSE -- if FM then 
    
   BEGIN

          SELECT
	 batch.OrderiD,
	 batch.CampaignID,
	 Convert(varchar(10),rr.DatePrinted,101) as DatePrinted,
   	 QSPCanadaOrderManagement.dbo.UDF_ProgramsbyCampaign( batch.CampaignID ) as Programs,
	 QSPCanadaOrderManagement.dbo.UDF_GetDistributionCentreNamesByOrder( batch.OrderiD ) as Warehouse,
	 Convert(varchar(10),DateReceived,101) as DateOrderReceived,
	 batch.StatusInstance,
	 cd.Description as BatchStatus,
	 cdQF.Description as OrderQualifier,
	 cdOT.Description as OrderType,
	 CONVERT(BIT, 0) as OrderCancelAllowed
	  
     FROM
	QSPCanadaOrderManagement.dbo.Batch Batch,
	QSPCanadaCommon.dbo.Campaign CA,
	QSPCanadaOrderManagement.dbo.ReportRequestBatch rr,
	QspCanadaCommon.dbo.CodeDetail cd,
	QspCanadaCommon.dbo.CodeDetail cdQF,
	QspCanadaCommon.dbo.CodeDetail cdOT
	
     WHERE	
	 batch.orderid  *=  rr.batchOrderId
	and batch.StatusInstance  *= cd.Instance
	and batch.OrderQualifierid  *= cdQF.Instance
	and batch.OrderTypeCode *= cdOT.Instance
	and batch.CampaignID 		= ca.id 
	and ca.FMID  			= @FMID
    	and batch.Orderid  		= IsNull(@pOrderID, Orderid)
             and batch.CampaignID 		= IsNull(@pCampaignID, batch.CampaignID)
             and batch.AccountID 		= IsNull(@pAccountID, batch.AccountID)
             and batch.StatusInstance 	= IsNull(@pOrderStatus, batch.StatusInstance)
             and batch.OrderQualifierID 	= IsNull(@pOrderQualifier, batch.OrderQualifierID)
	and batch.DateReceived 	>= IsNull(@pFromDateReceived, batch.DateReceived)    
	and batch.DateReceived 	<= IsNull(@pToDateReceived, batch.DateReceived)

     Order by batch.AccountID, batch.CampaignID,batch.OrderiD

   END 
    
 
   END

   ELSE -- if user has selected ware house


         IF (@FMID='9999') -- if not FM then 
  
        BEGIN
	

            SELECT
	 batch.OrderiD,
	 batch.CampaignID,
	 Convert(varchar(10),rr.DatePrinted,101) as DatePrinted,
   	 QSPCanadaOrderManagement.dbo.UDF_ProgramsbyCampaign( batch.CampaignID ) as Programs,
	 QSPCanadaOrderManagement.dbo.UDF_GetDistributionCentreNamesByOrder( batch.OrderiD ) as Warehouse,
	 Convert(varchar(10),DateReceived,101) as DateOrderReceived,
	 batch.StatusInstance,
	 cd.Description as BatchStatus,
	 cdQF.Description as OrderQualifier,
	 cdOT.Description as OrderType,
	 CASE WHEN batch.OrderQualifierID IN (39006, 39007, 39015, 39018, 39019, 39020, 39022, 39023) AND batch.StatusInstance IN (40002, 40010) THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END as OrderCancelAllowed
	  
     FROM
	QSPCanadaOrderManagement.dbo.Batch Batch,
	QSPCanadaOrderManagement.dbo.ReportRequestBatch rr,
	QspCanadaCommon.dbo.CodeDetail cd,
	QspCanadaCommon.dbo.CodeDetail cdQF,
	QspCanadaCommon.dbo.CodeDetail cdOT
	
     WHERE	
	 batch.orderid  *=  rr.batchOrderId
	and batch.StatusInstance  *= cd.Instance
	and batch.OrderQualifierid  *= cdQF.Instance
	and batch.OrderTypeCode *= cdOT.Instance
    	and batch.Orderid  		= IsNull(@pOrderID, Orderid)
             and batch.CampaignID 		= IsNull(@pCampaignID, batch.CampaignID)
             and batch.AccountID 		= IsNull(@pAccountID, batch.AccountID)
             and batch.StatusInstance 	= IsNull(@pOrderStatus, batch.StatusInstance)
             and batch.OrderQualifierID 	= IsNull(@pOrderQualifier, batch.OrderQualifierID)
	and batch.DateReceived >= IsNull(@pFromDateReceived, batch.DateReceived)    
	and batch.DateReceived <= IsNull(@pToDateReceived, batch.DateReceived)
    and batch.OrderID in  (	  Select Distinct batch.OrderID
				  From QSPCanadaOrderManagement.dbo.BatchDistributionCenter bdc,
				           QSPCanadaOrderManagement.dbo.Batch batch
				 where bdc.batchid  = batch.id
			 	and bdc.batchdate  = batch.date
				and bdc.DistributionCenterID  = @pWareHouse 
			    	and batch.Orderid  		= IsNull(@pOrderID, Orderid)
			             and batch.CampaignID 		= IsNull(@pCampaignID, batch.CampaignID)
			             and batch.AccountID 		= IsNull(@pAccountID, batch.AccountID)
			             and batch.StatusInstance 	= IsNull(@pOrderStatus, batch.StatusInstance)
						 and batch.OrderQualifierID 	= IsNull(@pOrderQualifier, batch.OrderQualifierID)
				and batch.DateReceived 	>= IsNull(@pFromDateReceived, batch.DateReceived)    
				and batch.DateReceived 	<= IsNull(@pToDateReceived, batch.DateReceived)
 
)
     Order by batch.AccountID, batch.CampaignID,batch.OrderiD

  END

  ELSE  -- IF FM then 

    BEGIN

            SELECT
	 batch.OrderiD,
	 batch.CampaignID,
	 Convert(varchar(10),rr.DatePrinted,101) as DatePrinted,
   	 QSPCanadaOrderManagement.dbo.UDF_ProgramsbyCampaign( batch.CampaignID ) as Programs,
	 QSPCanadaOrderManagement.dbo.UDF_GetDistributionCentreNamesByOrder( batch.OrderiD ) as Warehouse,
	 Convert(varchar(10),DateReceived,101) as DateOrderReceived,
	 batch.StatusInstance,
	 cd.Description as BatchStatus,
	 cdQF.Description as OrderQualifier,
	 cdOT.Description as OrderType,
	 CONVERT(BIT, 0) as OrderCancelAllowed
	  
     FROM
	QSPCanadaOrderManagement.dbo.Batch Batch,
	QSPCanadaOrderManagement.dbo.ReportRequestBatch rr,
	QspCanadaCommon.dbo.CodeDetail cd,
	QspCanadaCommon.dbo.CodeDetail cdQF,
	QspCanadaCommon.dbo.CodeDetail cdOT
	
     WHERE	
	 batch.orderid  *=  rr.batchOrderId
	and batch.StatusInstance  *= cd.Instance
	and batch.OrderQualifierid  *= cdQF.Instance
	and batch.OrderTypeCode *= cdOT.Instance
    	and batch.Orderid  		= IsNull(@pOrderID, Orderid)
             and batch.CampaignID 		= IsNull(@pCampaignID, batch.CampaignID)
             and batch.AccountID 		= IsNull(@pAccountID, batch.AccountID)
             and batch.StatusInstance 	= IsNull(@pOrderStatus, batch.StatusInstance)
             and batch.OrderQualifierID 	= IsNull(@pOrderQualifier, batch.OrderQualifierID)
	and batch.DateReceived >= IsNull(@pFromDateReceived, batch.DateReceived)    
	and batch.DateReceived <= IsNull(@pToDateReceived, batch.DateReceived)
    and batch.OrderID in  (	  Select Distinct batch.OrderID
				  From QSPCanadaOrderManagement.dbo.BatchDistributionCenter bdc,
				           QSPCanadaOrderManagement.dbo.Batch batch,
  				           QSPCanadaCommon.dbo.Campaign CA
				 where bdc.batchid  = batch.id
			 	and bdc.batchdate  = batch.date
				and batch.CampaignID  = CA.ID
				and bdc.DistributionCenterID  = @pWareHouse 
				and CA.FMID 			=  @FMID
			    	and batch.Orderid  		= IsNull(@pOrderID, Orderid)
			             and batch.CampaignID 		= IsNull(@pCampaignID, batch.CampaignID)
			             and batch.AccountID 		= IsNull(@pAccountID, batch.AccountID)
			             and batch.StatusInstance 	= IsNull(@pOrderStatus, batch.StatusInstance)
			             and batch.OrderQualifierID 	= IsNull(@pOrderQualifier, batch.OrderQualifierID)
				and batch.DateReceived 	>= IsNull(@pFromDateReceived, batch.DateReceived)    
				and batch.DateReceived 	<= IsNull(@pToDateReceived, batch.DateReceived)
 
)
     Order by batch.AccountID, batch.CampaignID,batch.OrderiD

    END


 
END
GO
