USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_VerifyPrizes]    Script Date: 06/07/2017 09:20:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE  [dbo].[pr_VerifyPrizes]  

 @OrderId int,
 @ErrorMsg varchar(1000) output ,
 @HasError int output 

AS
--print 'starting pr_VerifyPrizes ' + cast(getdate() as varchar(50))

 Declare @IsCumulative int , @IsPrizeCalc int, @Students int, @OnlineStudents int,  @Prizes int ,@RecExist int

-- Declare @HasError int
 
 Set @IsCumulative = 0 
 Set @IsPrizeCalc = 0 
 Set @Students = 0 
 Set @OnlineStudents = 0 
 Set @Prizes  = 0 
 Set @HasError = 0 
 Set @RecExist = 0 

         --print 'first select statement... select top 1 @isculative = 1... from ' + cast(getdate() as varchar(50))
         Select top 1 @IsCumulative = 1
         From 	QSPCANADACOMMON.dbo.CAMPAIGN as ca,
		QspCanadaCommon.dbo.campaignprogram cp,
		QspCanadaOrdermanagement.dbo.Batch as batch
 	    Where 	 ca.id  = cp.CampaignID
		and batch.campaignid = ca.id
		and batch.orderid  = @OrderId  
		and cp.programid in (11,18,22,29,40) --Prize Safari/Zone added Aug 15, 07 MS
		and cp.deletedtf <>1
		and batch.OrderQualifierID in( 39001,39002)  
		and not exists ( Select  1
        			 From 	QspCanadaCommon.dbo.campaignprogram cp2
			 	 Where 	 cp.CampaignID = cp2.CampaignID
				 and cp2.programid in (1) 
				 and cp2.deletedtf <>1    )  


   IF  @IsCumulative = 1  
     Begin
	
     --print 'if @iscumulative = 1 ' + cast(getdate() as varchar(50))
     --print 'select top 1 @isprizecalc = 1 from ...cod,coh,batch... ' + cast(getdate() as varchar(50))
	 Select top 1 @IsPrizeCalc = 1
	 From 	QspCanadaOrdermanagement.dbo.customerorderdetail cod,
		QspCanadaOrdermanagement.dbo.customerorderheader coh,
		QspCanadaOrdermanagement.dbo.Batch as batch
	 Where batch.id = coh.orderbatchid
	 	and batch.date = coh.orderbatchdate
	 	and coh.instance = cod.customerorderheaderinstance
	 	and batch.statusinstance <> 40005 --not cancelled
	 	and cod.producttype in (46008,46013,46014,46015) 
	 	and isnull(cod.delflag,0) <> 1 
	 	and batch.orderid  = @OrderId   

    --print 'if @isprizecalc <>1 set @errormsg = order not picked... ' + cast(getdate() as varchar(50))
	If  @IsPrizeCalc <> 1
	 Begin  
	    Set @ErrorMsg =  'Order not picked due to missing prizes, Order#' +str(@orderid) + char(13)
	 End 	

     --print 'select @Students = count(distinct coh.studentinstance) from cod,coh,batch ' + cast(getdate() as varchar(50))
	 Select @Students = count(distinct coh.studentinstance) 
	 from 	QspCanadaOrdermanagement.dbo.customerorderdetail cod,
		QspCanadaOrdermanagement.dbo.customerorderheader coh,
		QspCanadaOrdermanagement.dbo.Batch as batch
	 where batch.id = coh.orderbatchid
		 and batch.date = coh.orderbatchdate
		 and coh.instance = cod.customerorderheaderinstance
		 and batch.statusinstance <> 40005 --not cancelled
		 and cod.producttype NOT IN (46008,46013,46014,46015) 
		--and cod.productcode <> 'NNNN'   --MS Nov 1, 2007 Exclude those student where item not good
	 	and isnull(cod.delflag,0) <> 1 
		 and batch.orderid  = @OrderId   

     --print 'select @onlinestudents = count(distinct map.studentinstance) from cod,coh,batch,map ' + cast(getdate() as varchar(50))
	 Select @OnlineStudents = count(distinct map.studentinstance)   
     from 	QspCanadaOrdermanagement.dbo.customerorderdetail cod, -- with(index(ix_customerorderdetail_jfp)),
		QspCanadaOrdermanagement.dbo.customerorderheader coh,
		QspCanadaOrdermanagement.dbo.Batch as batch,
		QSPCanadaOrderManagement.dbo.OnlineOrderMappingTable map
        left join
        ( Select distinct coh.StudentInstance
		 FROM  QSPCanadaOrderManagement.dbo.customerorderdetail cod,
		       QSPCanadaOrderManagement.dbo.customerorderheader coh,
		       QSPCanadaOrderManagement.dbo.batch as batch
		 where batch.id = coh.orderbatchid
		 and batch.date = coh.orderbatchdate
		 and coh.instance = cod.customerorderheaderinstance
		 and batch.OrderId = @OrderId
		 and cod.ProductType NOT IN (46008,46013,46014,46015)) exclude
      on map.studentinstance = exclude.studentinstance
      where batch.id = coh.orderbatchid
        and batch.date = coh.orderbatchdate
	    and coh.instance = cod.customerorderheaderinstance
	    and batch.statusinstance <> 40005 --not cancelled
	    and cod.producttype NOT IN (46008,46013,46014,46015) 
	    --and cod.productcode <> 'NNNN'   --MS Nov 1, 2007 Exclude those student where item not good
	    and isnull(cod.delflag,0) <> 1 
	    and  map.LandedOrderID = batch.OrderID 
	    and map.LandedOrderID  = @OrderId
	    --and  map.studentinstance NOT IN  -- exclude those students who have sold items in ground sale
        and exclude.studentinstance is null

               Set @Students =  @Students + @OnlineStudents 
     
     --print 'select @Prizes = count(*) from cod,coh,batch ' + cast(getdate() as varchar(50))
	 Select @Prizes = count(*) 
	 from 	QspCanadaOrdermanagement.dbo.customerorderdetail cod,
		QspCanadaOrdermanagement.dbo.customerorderheader coh,
		QspCanadaOrdermanagement.dbo.Batch as batch
	 where batch.id = coh.orderbatchid
		 and batch.date = coh.orderbatchdate
		 and coh.instance = cod.customerorderheaderinstance
		 and batch.statusinstance <> 40005 --not cancelled
		 and cod.producttype in (46008,46013,46014,46015) 
	 	and isnull(cod.delflag,0) <> 1 
		 and batch.orderid  = @OrderId   
print @Students
print @Prizes
	IF  @IsPrizeCalc = 1
	 Begin  
	  IF  @Students <>  @Prizes
	    Begin  
	       Set @ErrorMsg =  'Number of students and prizes mismatches, Order not picked, Order# ' +str(@orderid) + char(13)
	    End	
             End   

  --print 'select top 1 @RecExist = 1 from systemerrorlog '  + cast(getdate() as varchar(50))
  Select top 1 @RecExist = 1  
  From  QspCanadaCommon.dbo.SystemErrorLog 
   Where OrderID = @OrderID
    and ProcName = 'pr_VerifyPrizes'
    and isFixed <> 1 


	 IF   @IsCumulative =  1  and (@IsPrizeCalc <> 1 OR (@Students <>  @Prizes))
	   Begin
	        Set @HasError = 1

		IF @RecExist <> 1
		  Begin
                 --print 'insert into systemerrorlog '  + cast(getdate() as varchar(50))
		         Insert into QspCanadaCommon.dbo.SystemErrorLog 
			   ( ErrorDate,OrderID,CampaignID,ProcName,Desc1,Desc2,IsReviewed,IsFixed) 
		         values ( getdate(),@OrderID,Null, 'pr_VerifyPrizes','Error in prizes',@ErrorMsg,0,0 ) 
		  End

	   End 

 

End
GO
