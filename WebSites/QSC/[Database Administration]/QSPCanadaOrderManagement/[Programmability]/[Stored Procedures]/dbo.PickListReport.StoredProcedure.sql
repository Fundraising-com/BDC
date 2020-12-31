USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[PickListReport]    Script Date: 06/07/2017 09:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[PickListReport]
	@ReportRequestID 	int, 
	@OrderID		Int,
	@BatchId		Int,
	@BatchDate		DateTime,
	@ShipDateFrom		DateTime ,
	@ShipDateTo		DateTime,
	@ReportType		Int,  -- PackingSlip(2) or PickList(1) Used By GetLabelsForPickList Proc
	@ShipmentGroupID Int = null
As


Declare @OrderType	Int,
	@CALanguage	Varchar(2),
	@CampaignId	Int,
	@OrderQualifierID int


/* Select @ItemsCount = count(*)
 From 	QSPCanadaOrderManagement.dbo.customerorderdetail cod,
	QSPCanadaOrderManagement.dbo.customerorderheader coh,
	QSPCanadaOrderManagement.dbo.batch batch
 Where batch.id = coh.orderbatchid
 and batch.date = coh.orderbatchdate
 and coh.instance = cod.customerorderheaderinstance
 and batch.statusinstance <> 40005 --not cancelled
 and cod.producttype <> 46001 --non mag items
 and cod.delflag <> 1 
 and batch.orderid  = @OrderID 
*/

Select  @OrderType= b.orderTypeCode ,@CampaignId=campaignid, @OrderQualifierID=OrderQualifierID
From  	QSPCanadaOrderManagement.dbo.batch b
Where  b.orderId = @OrderID

IF(@OrderQualifierID = 39008)

Begin
             Exec dbo.GetDataforPickListForCustomerService  @OrderID,@BatchId,@BatchDate,@ShipDateFrom,@ShipDateTo,@ReportType,@ShipmentGroupID   -- PackingSlip(2) or PickList(1)

	-- remarked by saqib on 18 apr 2006
	---- IF @@RowCount = 0  and @ItemsCount > 0 
	 ----        Insert into QspCanadaCommon.dbo.SystemErrorLog 
		----   ( ErrorDate,OrderID,CampaignID,ProcName,Desc1,Desc2,IsReviewed,IsFixed) 
	       ----  values ( getdate(),@OrderID,Null, 'PickListReport','Zero RowCount',null,0,0 )    

End

Else

Begin

	If  ( @OrderType in (41001,41009) Or
	      (	@OrderType = 41008 And @CampaignId <> 0))
	Begin
		
	             Exec dbo.GetDataforPickList  @OrderID,@BatchId,@BatchDate,@ShipDateFrom,@ShipDateTo,@ReportType,@ShipmentGroupID   -- PackingSlip(2) or PickList(1)
	-- remarked by saqib on 18 apr 2006
	-- IF @@RowCount = 0 and @ItemsCount > 0 
	  --       Insert into QspCanadaCommon.dbo.SystemErrorLog 
		--   ( ErrorDate,OrderID,CampaignID,ProcName,Desc1,Desc2,IsReviewed,IsFixed) 
--	         values ( getdate(),@OrderID,Null, 'PickListReport','Zero RowCount',null,0,0 )    
	End

	If  ( @OrderType in (41002,41005,41006, 41007, 41011, 41010) Or
	     (	@OrderType = 41008 And IsNull(@CampaignId,0)= 0))
	Begin
	
	             Exec dbo.GetDataForPickListNonEnvelope  @OrderID,@BatchId,@BatchDate, @ShipDateFrom  ,@ShipDateTo,@ReportType,@ShipmentGroupID   -- PackingSlip(2) or PickList(1)

	-- remarked by saqib on 18 apr 2006
--	 IF @@RowCount = 0 and @ItemsCount > 0 
	--         Insert into QspCanadaCommon.dbo.SystemErrorLog 
		--   ( ErrorDate,OrderID,CampaignID,ProcName,Desc1,Desc2,IsReviewed,IsFixed) 
--	         values ( getdate(),@OrderID,Null, 'PickListReport','Zero RowCount',null,0,0 )    
	End

	--End

	

end

--following lines are written by saqib on 13-Apr-2005 to update data driven subscription support tables

IF @ReportRequestID <> 0  -- if the value is not zero it means the report is called from a data driven subscription
BEGIN
     
   UPDATE Qspcanadaordermanagement.dbo.ReportRequestBatch_PrintPickList
   Set  RunDateStart = getdate()
   Where [id]  = @ReportRequestID

END
GO
