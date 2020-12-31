USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_VerifyOrder]    Script Date: 06/07/2017 09:20:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE  [dbo].[pr_VerifyOrder]
 @OrderId int,
 @ErrorMsg varchar(1000) output ,
 @HasError int output 

AS
--print 'starting pr_VerifyOrder ' + cast(getdate() as varchar(50))

--- Saqib - Feb 06
-- verify order data prior to run xml for tpl...


Begin

Declare @IsDataMissing int , @OrderExist int,   @ErrorText varchar (1000) 
Declare @CustomerBillToInstance int, @StudentInstance int, @PricingDetailsID int
Declare @IsCumulative int , @IsPrizeCalc int, @Students int, @Prizes int , @Msg varchar(1000) ,@Error int, @RecExist int

Set @ErrorMsg = ''
Set @ErrorText  = ''
Set  @Msg = ''
Set @IsDataMissing  = 0 
Set @OrderExist        = 0 


--check if key data is not missing
     --print 'check if key data is not missing ' + cast(getdate() as varchar(50))
     --print 'select top 1 ... from orderdetail,header,batch ' + cast(getdate() as varchar(50))
	 Select   top 1  @IsDataMissing = 1 ,
		 @CustomerBillToInstance  =  coh.CustomerBillToInstance, 
		 @StudentInstance = StudentInstance ,
		 @PricingDetailsID = PricingDetailsID
	 From 	QspCanadaOrdermanagement.dbo.customerorderdetail cod,
		QspCanadaOrdermanagement.dbo.customerorderheader coh,
		QspCanadaOrdermanagement.dbo.Batch as batch
	 Where batch.id = coh.orderbatchid
	 	and batch.date = coh.orderbatchdate
	 	and coh.instance = cod.customerorderheaderinstance
	 	and batch.statusinstance <> 40005 --not cancelled
	 	and isnull(cod.delflag,0) <> 1 
		AND cod.ProductType <> 46001
		and batch.orderid  = @OrderId
		AND cod.StatusInstance <> 501
		and ( coh.CustomerBillToInstance is null OR
		      coh.StudentInstance is null OR
		      cod.PricingDetailsID is null  ) 


IF @IsDataMissing  = 1

  Begin
	IF  @CustomerBillToInstance is NULL
	   begin
		Set @ErrorText = 'CustomerBillToInstance is NULL'
	   end 	

	IF  @StudentInstance is NULL
	   begin
		Set @ErrorText = @ErrorText + char(13)+'StudentInstance is NULL'
	   end 	

	IF  @PricingDetailsID is NULL
	   begin
		Set @ErrorText = @ErrorText + char(13)+'PricingDetailsID is NULL'
	   end 	

  End


--check if order exist in batch table but no coh or cod....
     --print 'check if order exist in batch table but no coh or cod ' + cast(getdate() as varchar(50))
     --print 'select top 1... from cod,coh,batch ' + cast(getdate() as varchar(50))
	 Select  top 1 @OrderExist = 1 
	 From 	QspCanadaOrdermanagement.dbo.customerorderdetail cod,
		QspCanadaOrdermanagement.dbo.customerorderheader coh,
		QspCanadaOrdermanagement.dbo.Batch as batch
	 Where batch.id = coh.orderbatchid
	 	and batch.date = coh.orderbatchdate
	 	and coh.instance = cod.customerorderheaderinstance
	 	and batch.statusinstance <> 40005 --not cancelled
	 	and isnull(cod.delflag,0) <> 1 
		AND isnull(cod.ProductType, 0) <> 46001
		and batch.orderid  = @OrderId 


	 IF  @OrderExist is NULL or @OrderExist = '' or @OrderExist = 0
	   begin
		Set @ErrorText = @ErrorText + char(13)+'Order does not exist' 
	   end 	

---now insert entry into error log table if it doesnt exist exist already
   --print 'now insert entry into error log table if it doesnt exist exist already ' + cast(getdate() as varchar(50))
   --print 'select top 1 ... from systemerrorlog ' + cast(getdate() as varchar(50))
           	Select top 1 @RecExist = 1  
	From  QspCanadaCommon.dbo.SystemErrorLog 
	Where OrderID = @OrderID
	 and ProcName = 'pr_VerifyOrder' 
	 and isFixed <> 1 

		IF @RecExist <> 1 and @ErrorText <> ''
		  Begin
		         Insert into QspCanadaCommon.dbo.SystemErrorLog 
			   ( ErrorDate,OrderID,CampaignID,ProcName,Desc1,Desc2,IsReviewed,IsFixed) 
		         values ( getdate(),@OrderID,Null, 'pr_VerifyOrder',@ErrorText,Null,0,0 ) 
		   End 



 --Verify if prizes are correct
 --print 'verify if prizes are correct ' + cast(getdate() as varchar(50))
 --print 'pr_verifyprizes ' + cast(getdate() as varchar(50))
 Exec QSPCanadaOrderManagement.dbo.pr_VerifyPrizes	@OrderId , @Msg  output , @Error output
 --print 'back in verify order' 

 --print '@errortext contains ' + @ErrorText + ' ' +cast(getdate() as varchar(50))
 IF  @Error = 1 
   Begin
       Set @ErrorText = @ErrorText+ char(13) + @Msg
   End 

--verify if there is any un-fixed problem for that order in error log table

 Set @Error = 0 
 --print 'verify if there is any un-fixed problem for that order in error log table ' + cast(getdate() as varchar(50))
 --print 'select @error ... from systemerrorlog ' + cast(getdate() as varchar(50))
 Select @Error = 1
 from QspCanadaCommon.dbo.SystemErrorLog   
 where Orderid = @OrderId
            and isFixed = 0 

 IF  @Error = 1 
   Begin
       --print 'if @Error = 1 ' + cast(getdate() as varchar(50))
       Set @HasError = 1 
       Set @ErrorText = '****Following Problems are found**** '+char(13)+ +char(13)+@ErrorText+ char(13) + 'Un-Fixed issue in SystemErrorLog table' + char(13) + + char(13) +'****************************'
       --print @ErrorText + cast(getdate() as varchar(50))
   End 

/*
 IF @HasError = 1 -- if order has an error then put the batch into under review state
   Begin
	 UPDATE QSPCanadaOrderManagement.dbo.Batch
	 SET	   StatusInstance = 40003 -- under review 
	 WHERE  OrderId = @OrderId    
   End
*/
  
  Set @ErrorMsg = @ErrorText 



End
GO
