USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[CorrectInvoiceGenerationError]    Script Date: 06/07/2017 09:17:06 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[CorrectInvoiceGenerationError]  @OrderId Int, @FromDate DateTime, @ToDate DateTime, @ErrorCode Int
AS

	Declare @Order 	Int,
		@Cnt 		Int,
		@PricingMissing Varchar(1),
		@OutPut	Int,
		@OutPutMessage	Varchar(200)

	Declare @AllBadOrderItem Table (
					Id 	Int	Identity,
					OrderId	Int,
					ErrorCode Int,
					BadPricing Int,
					Fixed	Int,
					ReturnErrorMessage Varchar(200))		

	Declare @DistinctOrder	Table (	Id 	Int	Identity,
					OrderId	Int)

	Declare @DistinctError Table (
					Id 	Int	Identity,
					OrderId	Int,
					ErrorCode Int)
					
					
					
	
	Insert into @AllBadOrderItem
		Select Orderid ,InvoiceGenErrorCode ,0,0,Null
		From QSPCanadaFinance..InvoiceGenerationLog
		Where OrderId  = IsNull(@OrderId,OrderId)
		And Convert(Varchar(10),DateTimeCreated,101)  >= IsNull(@FromDate, '01/01/1995')
		And Convert(Varchar(10),DateTimeCreated,101)  <= IsNull(@ToDate, GetDate())
		And InvoiceGenErrorCode= IsNull(@ErrorCode,InvoiceGenErrorCode)
		And IsFixed=0	--Still in Error
		Order By 1,2

	Insert into @DistinctOrder
	Select Distinct OrderId from @AllBadOrderItem Order By 1


	Declare  OrderInError Cursor For 	
	Select OrderId from @DistinctOrder

	Open OrderInError
	Fetch Next From  OrderInError  Into @Order
		
		
	While(@@Fetch_Status = 0)
	Begin


		Insert into @DistinctError
		Select  Distinct Orderid ,ErrorCode 
		From @AllBadOrderItem Where OrderId = @Order Order By 2

		Set @PricingMissing	='N'		

		/************************ Pricing or program section is bad ***********************************************************/
		Select @Cnt = Count(*) From @DistinctError where ErrorCode = 61001 or ErrorCode = 61002	
		If @Cnt > 0
		-- If there is missing pricing details or program section donot fix errors for that order
		Begin
			
			Set @PricingMissing	='Y'
			
			Update @AllBadOrderItem
			Set BadPricing =1 , ReturnErrorMessage = 'PricingDetail or Program Section bad'
			Where OrderId=@Order

		End




		Set @OutPutMessage=Null
		Set @OutPut =Null

		If @PricingMissing <> 'Y'
		-- Process all other errors if pricing detail or program Section is ok
		Begin

			/************************ Pricing and program section is ok but tax is zero fix all tax error ***********************************************************/
			Select @Cnt = Count(*) From @DistinctError where ErrorCode  in (61003,61004)	--Zero Tax or Null province code for Account
			If @Cnt > 0
			Begin
				Exec QSPCanadaFinance.dbo.VerifyZeroTaxAndFix  @Order , @OutPut  OutPut, @OutPutMessage  OutPut

				
				If @OutPut = 0 --Success
				Begin
			
					Update @AllBadOrderItem
					Set Fixed =1 ,ReturnErrorMessage = @OutPutMessage
					Where OrderId=@Order and ErrorCode in (61003,61004)	

					Select 'Tax fixed for order ' +cast(@Order as varchar)

					Update QSPCanadaFinance..InvoiceGenerationLog
					Set IsFixed=1, DateFixed=GetDate()
					Where OrderId = @Order and InvoiceGenErrorCode in  (61003,61004)

				End
				Else
					Update @AllBadOrderItem
					Set ReturnErrorMessage = @OutPutMessage
					Where OrderId=@Order and ErrorCode in (61003,61004)	
			End 
			
			
			Set @OutPutMessage=Null
			Set @OutPut =Null

			/***********************************-Batch Status ***************************************************************************************************************/
			Select @Cnt = Count(*) From @DistinctError where ErrorCode  in (61008)	-- Update Batch Status if all  billable items shipped/remitted
			If @Cnt > 0
			Begin
			
				Exec QSPCanadaFinance.dbo.VerifyBatchStatusAndFix  @Order , @OutPut  OutPut, @OutPutMessage  OutPut


				If @OutPut = 0 --Success
				Begin
			
					Update @AllBadOrderItem
					Set Fixed =1 , ReturnErrorMessage = @OutPutMessage
					Where OrderId=@Order and ErrorCode in (61008)	


					Select 'Batch Status  fixed for order ' +cast(@Order as varchar)

					Update QSPCanadaFinance..InvoiceGenerationLog
					Set IsFixed=1, DateFixed=GetDate()
					Where OrderId = @Order and InvoiceGenErrorCode in  (61008)
					
				End
				Else
					Update @AllBadOrderItem
					Set ReturnErrorMessage = @OutPutMessage
					Where OrderId=@Order and ErrorCode in (61008)	
			End 


			Set @OutPutMessage=Null
			Set @OutPut =Null

			/**************************************** Paid Item Not shipped  **********************************************/
			
			Select @Cnt = Count(*) From @DistinctError where ErrorCode  in (61009)	-- Paid item not remitted
			If @Cnt > 0
			Begin
			
				Exec QSPCanadaFinance.dbo.VerifyAndRemitPaidItem  @Order , @OutPut  OutPut, @OutPutMessage  OutPut


				If @OutPut = 0 --Success
				Begin
			
					Update @AllBadOrderItem
					Set Fixed =1 , ReturnErrorMessage = @OutPutMessage
					Where OrderId=@Order and ErrorCode in (61009)	


					Select 'All items sent to remit for order  ' +cast(@Order as varchar)

					Update QSPCanadaFinance..InvoiceGenerationLog
					Set IsFixed=1, DateFixed=GetDate()
					Where OrderId = @Order and InvoiceGenErrorCode in  (61009)
					
				End
				Else
					Update @AllBadOrderItem
					Set ReturnErrorMessage = @OutPutMessage
					Where OrderId=@Order and ErrorCode in (61009)	
			End 
		
		End
		

		--Process and delete from  temp table @DistinctError
		Delete from @DistinctError

	Fetch Next From OrderInError Into @Order

	End
	
	Close OrderInError
	Deallocate OrderInError
	
	--Item not fixed
	select Distinct OrderId,ErrorCode,ReturnErrorMessage from @AllBadOrderItem Where Fixed = 0
GO
