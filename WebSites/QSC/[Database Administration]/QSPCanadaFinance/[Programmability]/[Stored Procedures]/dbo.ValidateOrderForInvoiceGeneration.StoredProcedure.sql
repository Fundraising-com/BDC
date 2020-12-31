USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[ValidateOrderForInvoiceGeneration]    Script Date: 06/07/2017 09:17:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[ValidateOrderForInvoiceGeneration] 	@OrderId			Int,
								@RetVal			Int Output
As
Begin	
	Set NoCount on
	Set @RetVal = 0	-- Success
	

	Declare @Cnt 			Int,
		@BHEItemCnt		Int,
		@BatchStatus    	Int,
		@PrizesShipped		Int,
		@ProgramGroupProfit 	Int,
		@CampaignId		Int,
		@IsStaff		Int,
		--MS June 21, 2006
		@ChequeAmountFromPayment Numeric(10,2),
		@ChequeAmountFromBatch     Numeric(10,2),
		@ToLog	bit


	-- temp table to keep filtered data from from Billable item
	Create Table	
	#AllOrderItem  (Id				Int	Identity,
			OrderId				Int	Not Null,
			OrderDate			DateTime,
			AccountId			Int	Not Null,
			State				Varchar(20),
			CampaignId 			Int	Not Null,
			BatchStatus			Int,
			CustomerOrderHeaderInstance	Int	Not Null,
			TransId				Int	Not Null,
			ProductType			Int,
			ItemStatus			Int,
			PricingDetailsId			Int,
			ProgramSectionId		Int,
			Price				Numeric(12,5),
			TaxAmount			Numeric(12,5),
			PaymentMethodId		Int,
			CreditCardStatus			Int,
			CCPaymentBatchId		Int,
			CPHInstance			Int,
			PaymentHeaderStatus		Int,
			CPHTotalAmount		Numeric(12,5),
			TotalPaidByTrans		Numeric(12,5),
			PrizesShipped			Int,
			ChequeAmount			Numeric(10,2),	----MS June 21, 2006 Issue #521
			ProgramSectionTypeID		Int Not Null,
			OrderQualifierID		Int Not Null,
			ProductCode varchar(20) NULL
			)

	-- temp table InvGenLog	with same structure as invoiceGeneration Log
	Create Table
 	#InvGenLog  (	Id				Int	Identity,
			OrderId				Int	Not Null,
			AccountId			Int	Not Null,
			CampaignId 			Int	Not Null,
			BatchStatus			Int,
			CustomerOrderHeaderInstance	Int	Not Null,
			TransId				Int	Not Null,
			PricingDetailsId			Int,
			ProgramSectionId		Int,
			TaxAmount			Numeric(12,5),
			PaymentMethodId		Int,
			PaymentStatusId		Int,
			PaymentHeaderStatusId		Int,
			ProvinceCode			Varchar(10),
			PaymentBatchId			Int,
			CPHInstance			Int,
			CPHTotal			Numeric(12,5),
			TotalPaidByCC			Numeric(12,5),
			PrizesShipped			Int,
			InvoiceGenErrorCode		Int	       ,      --Not Null,
			InvoiceGenErrorMessage	Varchar(200), 	 --Not Null
			ProductType Int,
			ProgramSectionTypeID Int Null,
			ProductCode varchar(20) NULL
		    )

	
	-- temp table to store customer payment info
	Create Table
	#PaymentHeaderTotal(
			Id				Int	Identity,
			OrderId				Int,	
			AccountId			Int,	
			CampaignId 			Int,	
			CustomerOrderHeaderInstance	Int	,
			PaymentMethodId		Int,
			PriceTotal			Numeric(12,5),
			CPHInstance			Int,
			CPHTotal			Numeric(12,5),
			CCPaymentBatchId		Int,
			IsGLDone			Varchar(1)
			)

	
	-- Get all items for order being validated
	Insert #AllOrderItem 
	Select OrderId,
	       Date,
	       AccountId,
	       State,
	       CampaignId,
	       BatchStatusId,		
	       Instance, 
	       TransId,
	       ProductType,
	       ItemStatus,
	       PricingDetailsId,
	       ProgramSectionId,
	       Price,
	       Tax,
	       PaymentMethodInstance,
	       CreditCardStatus,
	       CCPaymentBatchId,
	       CPHInstance,	
	       PaymentHeaderStatus,
	       CPHTotalAmount,
	       Sum(Case PaymentMethodInstance
	       When 50002 Then 0
			Else Price
	       End) TotalPaidByTrans,
	       1, --PrizesShipped	
	       ChequepaymentAmount,	----MS June 21, 2006 Issue #521
	       SectionType ProgramSectionTypeID,
	       OrderQualifierID,
	       ProductCode
	From  #TempBillableOrdersFromBatch 
 	Where OrderId= @OrderId
	Group BY OrderId,
	       Date,
	       AccountId,
	       State,
	       CampaignId,
	       BatchStatusId,		
	       Instance, 
	       TransId,
	       ProductType,
	       ItemStatus,
	       PricingDetailsId,
	       ProgramSectionId,
	       Price,
	       Tax,
	       PaymentMethodInstance,
	       CreditCardStatus,
	       CCPaymentBatchId,
	       CPHInstance,	
	       PaymentHeaderStatus,
	       CPHTotalAmount,
	       ChequepaymentAmount,
	       SectionType,
	       OrderQualifierID,
	       ProductCode

	--Check if The all prizes have been shipped
	Select Top 1 b.orderid
	From 	QSPCanadaOrderManagement..Batch b,
     		QSPCanadaOrderManagement..CustomerorderHeader h,
     		QSPCanadaOrderManagement..CustomeroRderDetail d
	Where  	b.id=OrderBatchId
	And	b.date=orderBatchdate
	And	h.instance=d.customerorderheaderinstance
	And 	d.statusinstance not in (508)
	And 	d.Delflag =0
	And 	d.producttype  in (46008, 46013,46014,46015) --Check Only Prizes
	And 	OrderId= @OrderId

	If @@Rowcount > 0
	Begin
		Update #AllOrderItem Set PrizesShipped=0
	End

	-- Select All record with problem from Billable items
	Insert into #InvGenLog	(OrderId, 
				AccountId,
				CampaignId, 
				BatchStatus,
				CustomerOrderHeaderInstance, 
				TransId, 
				PricingDetailsId,
				ProgramSectionId,
				TaxAmount,
				PaymentMethodId,
				PaymentStatusId ,
				PaymentHeaderStatusId,
				ProvinceCode,
				PaymentBatchId,
				CPHInstance,
				CPHTotal,
				PrizesShipped,
				ProductType,
				ProgramSectionTypeID,
				ProductCode)
	Select OrderId,
	       AccountId,
	       CampaignId,
	       BatchStatus,		
	       CustomerOrderHeaderInstance, 
	       TransId,
	       PricingDetailsId,
	       ProgramSectionId,
	       TaxAmount,
	       PaymentMethodId,
	       CreditCardStatus,
	       PaymentHeaderStatus,
	       State,   	--ProvinceCode	
	       CCPaymentBatchId,
	       CPHInstance, 
	       CPHTotalAmount,
	       PrizesShipped,
		   ProductType,
		   ProgramSectionTypeID,
		   ProductCode
	From #AllOrderItem 
	Where 	OrderId= @OrderId		
	And (  
	       (PricingDetailsId 	 = 0		OR
	        programSectionId	 = 0 		OR
	        (IsNull(State,'@')  ='@' OR State='')   OR
	         (IsNull(TaxAmount,0) = 0 And IsNull(ProductType, 0) <> 46018 And ProgramSectionTypeId NOT IN (3, 6, 9, 10) And (ProductType <> 46002 OR ProductCode NOT LIKE 'DO%')) OR
	        --IsNull(PaymentBatchId,0) = 0          OR 	--For Cheques Payment batch can be Null MS
	         IsNull(PaymentMethodId,0) Not In(50002,50003,50004,50006,50007)
	        )      OR		
	       ( PaymentMethodId <> 50002  And  (IsNull(PaymentHeaderStatus,0) = 600 And  IsNull(CreditCardStatus,0) <> 19000))
		       OR		
	       ( PaymentMethodId <> 50002  And  (IsNull(PaymentHeaderStatus,0) = 601 And  IsNull(CreditCardStatus,0) = 19000)) 	
		       OR
	       ( PaymentMethodId In(50003,50004,50006,50007)  And   IsNull(CCpaymentBatchId,0) = 0 And OrderQualifierID NOT IN (39009) )
	       )   

------------------ June 21, 2006 Issue #521
	--Check if Cheque Payment got applied
	Select Top 1 @ChequeAmountFromBatch=ChequeAmount From #AllOrderItem 
	Where OrderId= @OrderId	
	And ChequeAmount >  0
	
	If @@RowCount > 0
	Begin
		Select @ChequeAmountFromPayment = Sum(Payment_Amount )
		From QSPCanadaFinance..Payment 
		Where Order_Id=@OrderId
		And Payment_Method_Id=50002
		
		Set @ToLog = 0
		Set @ChequeAmountFromPayment = IsNull(@ChequeAmountFromPayment,0)
		
		-- Validate the the 2 amounts are different
		IF ( @ChequeAmountFromPayment <> @ChequeAmountFromBatch)
		BEGIN
			-- If they are different, we log it.. unless, the difference is less than 25
			Set @ToLog = 1	

			-- Validate that they are both different of 0
			IF (@ChequeAmountFromPayment > 0 AND @ChequeAmountFromBatch > 0)
			BEGIN
				-- If the difference is less than 25, we don't log it
				IF (ABS(@ChequeAmountFromPayment - @ChequeAmountFromBatch) <= 25) 
					Set @ToLog = 0
			END
		END
				
		If (@ToLog = 1)
		Begin
			Insert into #InvGenLog(	
				OrderId, 
				AccountId, 
				CampaignId, 
				CustomerOrderHeaderInstance, 
				TransId,
				PaymentMethodId,
				PaymentBatchId ,
				CPHInstance,
				CPHTotal,
				TotalPaidByCC,
				InvoiceGenErrorCode,
				InvoiceGenErrorMessage)	
			Select Top 1  OrderId,
	       			AccountId,
	       			CampaignId,
	       			0, 
				0,
	       			0,
	      			0  ,
	       			0 ,
	       			0 ,
	       			0,	
	       			61014,	-- Un-applied Cheque
	       			'Payments donot match with attached cheque or Un-applied Cheque payment $'+Convert(varchar,@ChequeAmountFromBatch) 	       	
		From #AllOrderItem 
		End
	End

-------------------
/*
	--Check if prizes shipped		
	Select Top 1 PrizesShipped From #AllOrderItem 
	Where OrderId= @OrderId	
	And PrizesShipped = 0

	If @@RowCount > 0
	Begin
		Insert into #InvGenLog(	
				OrderId, 
				AccountId, 
				CampaignId, 
				CustomerOrderHeaderInstance, 
				TransId,
				PaymentMethodId,
				PaymentBatchId ,
				CPHInstance,
				CPHTotal,
				TotalPaidByCC,
				InvoiceGenErrorCode,
				InvoiceGenErrorMessage)	
		Select Top 1 	OrderId,
	       			AccountId,
	       			CampaignId,
	       			0, 
				0,
	       			0,
	      			0  ,
	       			0 ,
	       			0 ,
	       			0,	
	       			61013,	--Paid item not remited/shipped	
	       			'Prizes not shipped'	       	
		From #AllOrderItem 
	End
*/
	--CA program group profit is zero for Non Staff CAs
	Select  Top 1 @CampaignId = CampaignId From #AllOrderItem

	Select @IsStaff = IsStaffOrder From QSPcanadacommon..campaign
	Where  Id = @CampaignId

	If  IsNull(@IsStaff, 0) = 0 AND @CampaignId <> 108508 --Gift Card Redemption CA
	Begin		
		Select Top 1 @ProgramGroupProfit= GroupProfit From qspcanadacommon..campaignprogram cp
		Where campaignid=@CampaignId
		And ProgramId in ( Select Id From qspcanadacommon..program
		   		Where programTypeid=36001
		   		And defaultProfit > 0 )
		And DeletedTF=0
	
		If IsNull(@ProgramGroupProfit,0) = 0
		Begin
		Insert into #InvGenLog(	
				OrderId, 
				AccountId, 
				CampaignId, 
				CustomerOrderHeaderInstance, 
				TransId,
				PaymentMethodId,
				PaymentBatchId ,
				CPHInstance,
				CPHTotal,
				TotalPaidByCC,
				InvoiceGenErrorCode,
				InvoiceGenErrorMessage)	
		Select Top 1 	OrderId,
	       			AccountId,
	       			CampaignId,
	       			0, 
				0,
	       			0,
	      			0  ,
	       			0 ,
	       			0 ,
	       			0,	
	       			61011,	--Paid item not remited/shipped	
	       			'No Group Profit for CA program'	       	
		From #AllOrderItem 
		End
	End

	-- Payments made by CC must be posted to be reflected on invoices
	Insert into #PaymentHeaderTotal
	Select OrderId,AccountId,campaignid,CustomerorderHeaderInstance,PaymentMethodId,Sum(price), CPHInstance,CPHTotalAmount,CCpaymentbatchId, 'N' 
	From #AllOrderItem
	Where paymentmethodId in(50003,50004,50006,50007)
	Group by   OrderId,AccountId,campaignid,CustomerorderHeaderInstance,PaymentMethodId,CPHInstance,CCpaymentbatchId,CPHTotalAmount

	
	Declare @CCpaymentbatchId int
	Declare @CPHInstance int
	Declare @IsGLDone int

	Declare AllCCPayBatch Cursor For
	Select Distinct CCpaymentbatchId From #PaymentHeaderTotal 

	Open AllCCPayBatch
	Fetch Next From AllCCPayBatch Into @CCpaymentbatchId
		
	While @@Fetch_Status = 0
	Begin

		Select @IsGLDone= ISGLDone From QSPcanadaOrderManagement..CreditCardBatch
		Where ID=@CCpaymentbatchId
		
		If IsNull(@IsGLDone,0) =1
		Begin
			Update 	#PaymentHeaderTotal
			Set IsGLDone= 'Y'
			Where CCPaymentBatchId= @CCpaymentbatchId
		End

	Fetch Next From AllCCPayBatch Into @CCpaymentbatchId
	End
	Close AllCCPayBatch
	Deallocate AllCCPayBatch


	--Non batch CC payments
	Declare AllCCNoPayBatch Cursor For
	Select Distinct CPHInstance From #PaymentHeaderTotal Where IsNull(CCpaymentbatchId,0) = 0

	Open AllCCNoPayBatch
	Fetch Next From AllCCNoPayBatch Into @CPHInstance
		
	While @@Fetch_Status = 0
	Begin
		
		Select @IsGLDone= ISGLDone from QSPcanadaOrderManagement..NonBatchCreditCardPayment
		Where CustomerpaymentHeaderInstance=@CPHInstance
		
		If IsNull(@IsGLDone,0) =1
		Begin
			Update 	#PaymentHeaderTotal
			Set IsGLDone= 'Y'
			Where CPHInstance= @CPHInstance
		End

	Fetch Next From AllCCNoPayBatch Into @CPHInstance
	End
	Close AllCCNoPayBatch
	Deallocate AllCCNoPayBatch


	Insert into #InvGenLog	(OrderId, 
				AccountId, 
				CampaignId, 
				CustomerOrderHeaderInstance, 
				TransId,
				PaymentMethodId,
				PaymentBatchId ,
				CPHInstance,
				CPHTotal,
				TotalPaidByCC,
				InvoiceGenErrorCode,
				InvoiceGenErrorMessage)	
	Select OrderId,
	       AccountId,
	       CampaignId,
	       CustomerOrderHeaderInstance, 
	       0, 				--Trans Id not applicable
	       PaymentMethodId,
	       CCPaymentBatchId  ,
	       CPHInstance ,
	       CPHTotal ,
	       priceTotal,	
	       61012,	--Unposted CC payments 	
	       'CC payments have not been posted'	       	
	From #PaymentHeaderTotal 
	Where IsGLDone In('N') 

	-- Check if there is one paid item which has not been sent to Remit or Shipped
	Insert into #InvGenLog	(OrderId, 
				AccountId, 
				CampaignId, 
				BatchStatus,
				CustomerOrderHeaderInstance, 
				TransId, 
				PricingDetailsId,
				ProgramSectionId,
				TaxAmount,
				PaymentMethodId,
				PaymentStatusId ,
				PaymentHeaderStatusId,
				ProvinceCode,
				PaymentBatchId, 
				InvoiceGenErrorCode,
				InvoiceGenErrorMessage)	
	Select OrderId,
	       AccountId,
	       CampaignId,
	       BatchStatus,		
	       CustomerOrderHeaderInstance, 
	       TransId,
	       PricingDetailsId,
	       ProgramSectionId,
	       TaxAmount,
	       PaymentMethodId,
	       Null as PaymentStatusId ,
	       Null as PaymentHeaderStatusId,
	       Null as ProvinceCode,
	       Null as PaymentBatchId,
	       61009,	--Paid item not remited/shipped	
	       'Paid item not remited/shipped'	       	
	From #AllOrderItem a
	Where ItemStatus In(502)
	And (Cast(CustomerOrderHeaderInstance as Varchar)+' '+Cast(TransId as Varchar)) not in(
											--  Inactive items will be invoiced and switch letter will be issued 
											Select Cast(CustomerOrderHeaderInstance as Varchar)+' '+Cast(TransId as Varchar)
											From QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory
											Where CustomerOrderHeaderInstance =a.CustomerOrderHeaderInstance
											And TransId=a.TransId
											And Status = 42010)  --Magazine Inactive
	And ProductType NOT IN (46017, 46021)

	-- Check if there are no shipable items and batch status is not fulfilled
	Select  Count(*)
	From #AllOrderItem 
	Where producttype in (46002,46003,46004,46005,46006,46007,46008,46010,46011,46012,46020,46022,46024)
        	And  ItemStatus  in (509,510,511)
	
	if @@Rowcount  > 0
	Begin
	     Insert into #InvGenLog ( OrderId,
				AccountId, 
				CampaignId, 
				BatchStatus,
				CustomerOrderHeaderInstance, 
				TransId, 
				PricingDetailsId,
				ProgramSectionId,
				TaxAmount,
				PaymentMethodId,
				PaymentStatusId ,
				PaymentHeaderStatusId,
				ProvinceCode,
				PaymentBatchId, 
				InvoiceGenErrorCode,
				InvoiceGenErrorMessage)	
	     Select Top 1  OrderId,
	      		AccountId,
	     		CampaignId,
	       		BatchStatus,		
	      		CustomerOrderHeaderInstance, 
	       		TransId,
	       		Null,
	       		Null,
	       		Null,
	       		Null,
	       		Null,
	       		Null,
	       		Null,
	       		Null,
	       		61010,	
	       		'Shippable Item not Fulfilled'	
	     From #AllOrderItem 
	     Where  ItemStatus In(509,510,511)  
	End

	--Update All error messages and code
	Update 	#InvGenLog	
	Set InvoiceGenErrorCode	= 61001,
	    InvoiceGenErrorMessage = 'Zero Pricing Detail'
	Where IsNull(PricingDetailsId,0) = 0
	And IsNull(InvoiceGenErrorCode,0) = 0
	And IsNull(InvoiceGenErrorMessage,'@')='@' -- Update Only if no previous error

	Update 	#InvGenLog	
	Set InvoiceGenErrorCode	= 61002,
	    InvoiceGenErrorMessage = 'Zero Program Section'
	Where IsNull(programSectionId,0) = 0
	And IsNull(InvoiceGenErrorCode,0) = 0
	And IsNull(InvoiceGenErrorMessage,'@')='@' --If not updated because of Null PricingDetail


	Update 	#InvGenLog	
	Set InvoiceGenErrorCode	= 61003,
	    InvoiceGenErrorMessage = 'Zero Tax'
	Where (IsNull(TaxAmount,0) = 0 And IsNull(ProductType, 0) <> 46018 And ProgramSectionTypeId NOT IN (3, 6, 9, 15, 16) And (ProductType <> 46002 OR (ProductCode NOT LIKE 'DO%' AND ProductCode NOT LIKE 'D1%')))
	And IsNull(InvoiceGenErrorCode,0) = 0
	And IsNull(InvoiceGenErrorMessage,'@')='@' --If not updated because of Null Program Section
				
	/*
	Update 	#InvGenLog	
	Set InvoiceGenErrorCode	= 61004,
	    InvoiceGenErrorMessage = 'Null Province Code'
	Where (IsNull(ProvinceCode,'@') = '@' OR ProvinceCode='')
	And IsNull(InvoiceGenErrorCode,0) = 0
	And IsNull(InvoiceGenErrorMessage,'@')='@' --If not updated because of Zero Tax
	*/
	
	Update 	#InvGenLog	
	Set InvoiceGenErrorCode	= 61005,
	    InvoiceGenErrorMessage = 'Invalid Payment Method'
	Where IsNull(PaymentMethodId,0) Not In(50002,50003,50004,50006,50007)
	And IsNull(InvoiceGenErrorCode,0) = 0
	And IsNull(InvoiceGenErrorMessage,'@')='@' --If not updated because of Province COde

	Update 	#InvGenLog	
	Set InvoiceGenErrorCode	= 61006,
	    InvoiceGenErrorMessage = 'PaymentHeader Status/ Payment Status mismatch'
	Where ( (IsNull(PaymentHeaderStatusId,0) =  600 And  IsNull(PaymentStatusId,0) <> 19000 AND PaymentMethodId <> 50002)
	         OR		
	        (IsNull(PaymentHeaderStatusId,0) = 601  And  IsNull(PaymentStatusId,0) = 19000 AND PaymentMethodId <> 50002)
	      ) 	
	And IsNull(InvoiceGenErrorCode,0) = 0
	And IsNull(InvoiceGenErrorMessage,'@')='@' --If not updated because of Payment Method	Update 	#InvGenLog	
	
	Update 	#InvGenLog	
	Set InvoiceGenErrorCode	= 61007,
	    InvoiceGenErrorMessage = 'Zero Payment Batch, Invoice Generated'
	Where IsNull(PaymentBatchId,0) = 0
	And IsNull(InvoiceGenErrorCode,0) = 0
	And IsNull(InvoiceGenErrorMessage,'@')='@' --If not updated because of Payment Status mismatch	
	--If there are no errors then check for batch status
	Select Distinct InvoiceGenErrorCode from #InvGenLog Where InvoiceGenErrorCode <>  61007
	If @@RowCount = 0
	Begin
		--check if any item in not invoicable (excluding) prize
		Select  TOP 1 orderId	From #AllOrderItem 
		Where  ItemStatus  not in (502,507,508,512,513,514,515) 
		
		/*If @@Rowcount  =  0
		Begin
	     		--All items invoicable check if the prizes are also shipped
	     		Select Top 1 @BatchStatus=Isnull(BatchStatus,0),@PrizesShipped=PrizesShipped from #AllOrderItem 	
	     		If  @BatchStatus <>  40013 AND @PrizesShipped=1 
	     		Begin
	     			Insert into #InvGenLog ( OrderId,
						AccountId, 
						CampaignId, 
						BatchStatus,
						CustomerOrderHeaderInstance, 
						TransId, 
						PricingDetailsId,
						ProgramSectionId,
						TaxAmount,
						PaymentMethodId,
						PaymentStatusId ,
						PaymentHeaderStatusId,
						ProvinceCode,
						PaymentBatchId, 
						InvoiceGenErrorCode,
						InvoiceGenErrorMessage)	
	     			Select Top 1  	OrderId,
	      					AccountId,
	     					CampaignId,
	       					BatchStatus,		
	      					CustomerOrderHeaderInstance, 
	       					TransId,
	       					Null,
	       					Null,
	       					Null,
	       					Null,
	       					Null,
	       					Null,
	       					Null,
	       					Null,
	       					61008,	--Batch Staus should be updated to fulfilled
	       					'Batch Status Error'	
	     			From #AllOrderItem 
		
	     		End -- If batch status not fulfilled and all prizes shipped
	   	End	*/-- There are no item with status other than billable
	End	-- If no other errors
	
	--All errors entered in temp Error 
	Select @Cnt =	count(*) from #InvGenLog 

	If Isnull(@Cnt,0) > 0 
	Begin

	    Insert into InvoiceGenerationLog 
	    Select OrderId, 
		AccountId, 
		CampaignId, 
		BatchStatus,
		CustomerOrderHeaderInstance, 
		TransId, 
		PricingDetailsId,
		ProgramSectionId,
		TaxAmount,
		PaymentMethodId,
		PaymentStatusId ,
		PaymentHeaderStatusId,
		ProvinceCode,
		PaymentBatchId, 
		paymentBatchId	,
		CPHInstance		,
		CPHTotal		,
		TotalPaidByCC			,
		InvoiceGenErrorCode,
		InvoiceGenErrorMessage,
		GetDate(),
		0,		-- IsFixed
		Null		--Date Fixed
	   from #InvGenLog	
	   Where InvoiceGenErrorCode 	<>  61007		
	    
	End
	
	--Return error exclude Zero Payment Batch Id
	Select @Cnt =	Count(*) from #InvGenLog Where InvoiceGenErrorCode <>  61007
	If Isnull(@Cnt,0) > 0 
	Begin
		 Set @RetVal	=1
	End

Drop table #AllOrderItem
Drop table #InvGenLog
Drop table #PaymentHeaderTotal
End

/* Old Version with Table variables and without prizes check
Begin	
	Set NoCount on
	Set @RetVal = 0	-- Success

	Declare @Cnt 		Int,
		@BHEItemCnt	Int,
		@BatchStatus    Int,
		@ProgramGroupProfit Int,
		@CampaignId	Int,
		@IsStaff	Int

	Declare
	-- temp table InvGenLog	with same structure as invoiceGeneration Log
 	@InvGenLog TABLE (
			Id				Int	Identity,
			OrderId				Int	Not Null,
			AccountId			Int	Not Null,
			CampaignId 			Int	Not Null,
			BatchStatus			Int,
			CustomerOrderHeaderInstance	Int	Not Null,
			TransId				Int	Not Null,
			PricingDetailId			Int,
			ProgramSectionId		Int,
			TaxAmount			Numeric(10,2),
			PaymentMethodId		Int,
			PaymentStatusId		Int,
			PaymentHeaderStatusId		Int,
			ProvinceCode			Varchar(10),
			PaymentBatchId			Int,
			CCpaymentBatchId		Int,
			CPHInstance			Int,
			CPHTotal			Numeric(10,2),
			TotalPaidByCC			Numeric(10,2),
			InvoiceGenErrorCode		Int	       , --Not Null,
			InvoiceGenErrorMessage	Varchar(200) --Not Null
			
			)
	-- temp table to keep filtered data from from Billable item
	Declare	@AllOrderItem TABLE (
			Id				Int	Identity,
			OrderId				Int	Not Null,
			AccountId			Int	Not Null,
			CampaignId 			Int	Not Null,
			BatchStatus			Int,
			CustomerOrderHeaderInstance	Int	Not Null,
			TransId				Int	Not Null,
			ProductType			Int,
			ItemStatus			Int,
			PricingDetailId			Int,
			ProgramSectionId		Int,
			Price				Numeric(10,2),
			TaxAmount			Numeric(10,2),
			PaymentMethodId		Int,
			CCPaymentBatchId		Int,
			CPHInstance			Int,
			CPHTotalAmount		Numeric(10,2)
			
			)

	Declare
	-- temp table InvGenLog	with same structure as invoiceGeneration Log
 	@PaymentHeaderTotal TABLE (
			Id				Int	Identity,
			OrderId				Int,	
			AccountId			Int,	
			CampaignId 			Int,	
			CustomerOrderHeaderInstance	Int	,
			PaymentMethodId		Int,
			PriceTotal			Numeric(10,2),
			CPHInstance			Int,
			CPHTotal			Numeric(10,2),
			CCPaymentBatchId		Int,
			IsGLDone			Varchar(1)
			)

	-- Only for  order being validated
	Insert into @AllOrderItem 
	Select OrderId,
	       AccountId,
	       CampaignId,
	       BatchStatusId,		
	       Instance, 
	       TransId,
	       ProductType,
	       ItemStatus,
	       PricingDetailsId,
	       ProgramSectionId,
	       Price,
	       Tax,
	       PaymentMethodInstance,
	       CCPaymentBatchId,
	       CPHInstance,	
	       CPHTotalAmount
	From  #TempBillableOrdersFromBatch
 	Where OrderId= @OrderId

	-- Select All record with problem from Billable items
	Insert into @InvGenLog	(OrderId, 
				AccountId, 
				CampaignId, 
				BatchStatus,
				CustomerOrderHeaderInstance, 
				TransId, 
				PricingDetailId,
				ProgramSectionId,
				TaxAmount,
				PaymentMethodId,
				PaymentStatusId ,
				PaymentHeaderStatusId,
				ProvinceCode,
				PaymentBatchId)
	Select OrderId,
	       AccountId,
	       CampaignId,
	       BatchStatusId,		
	       Instance, 
	       TransId,
	       PricingDetailsId,
	       ProgramSectionId,
	       Tax,
	       PaymentMethodInstance,
	       CreditCardStatus,
	       PaymentHeaderStatus,
	       State,   --ProvinceCode	
	       PaymentBatchId
	from #TempBillableOrdersFromBatch 
	Where 	OrderId= @OrderId		
	And (  
	       (pricingDetailsId 	 = 0	OR
	        programSectionId	 = 0 	OR
	        (IsNull(State,'@')  ='@' OR State='')    OR
	        IsNull(Tax,0)       = 0      OR
	        --IsNull(PaymentBatchId,0) = 0     OR 		--For Cheques Payment batch can be Null MS
	        IsNull(PaymentMethodInstance,0) Not In(50002,50003,50004)
	        )
	      OR		
	      ( PaymentMethodInstance <> 50002  And    (IsNull(PaymentHeaderStatus,0) =  600 And  IsNull(CreditCardStatus,0) <> 19000) )
	
	      OR		
	       (  PaymentMethodInstance <> 50002  And  ( IsNull(PaymentHeaderStatus,0) = 601  And  IsNull(CreditCardStatus,0) = 19000)) 	

	       OR
	      ( PaymentMethodInstance In(50003,50004)  And   IsNull(PaymentBatchId,0) = 0  )
	      )    	
		
	--CA program group profit is zero for Non Staff CAs
	Select  Top 1 @CampaignId = CampaignId From @AllOrderItem

	Select @IsStaff = IsStaffOrder From QSPcanadacommon..campaign
	Where  Id = @CampaignId

	If  IsNull(@IsStaff, 0) = 0
	Begin		
		Select Top 1 @ProgramGroupProfit= GroupProfit From qspcanadacommon..campaignprogram cp
		Where campaignid=@CampaignId
		And ProgramId in ( Select Id From qspcanadacommon..program
		   		Where programTypeid=36001
		   		And defaultProfit > 0 )
		And DeletedTF=0
	
		If IsNull(@ProgramGroupProfit,0) = 0
		Begin
		Insert into @InvGenLog	(OrderId, 
				AccountId, 
				CampaignId, 
				CustomerOrderHeaderInstance, 
				TransId,
				PaymentMethodId,
				CCPaymentBatchId ,
				CPHInstance,
				CPHTotal,
				TotalPaidByCC,
				InvoiceGenErrorCode,
				InvoiceGenErrorMessage)	
		Select Top 1 	OrderId,
	       			AccountId,
	       			CampaignId,
	       			0, 
				0,
	       			0,
	      			0  ,
	       			0 ,
	       			0 ,
	       			0,	
	       			61011,	--Paid item not remited/shipped	
	       			'No Group Profit for CA program'	       	
		From @AllOrderItem 
		End
	End

	-- Payments made by CC must be posted to be reflected on invoices
	Insert into @PaymentHeaderTotal
	Select OrderId,AccountId,campaignid,CustomerorderHeaderInstance,PaymentMethodId,sum(price), CPHInstance,CPHTotalAmount,CCpaymentbatchId, 'N' 
	from @AllOrderItem
	Where paymentmethodId in(50003,50004)
	Group by   OrderId,AccountId,campaignid,CustomerorderHeaderInstance,PaymentMethodId,CPHInstance,CCpaymentbatchId,CPHTotalAmount

	
	Declare @CCpaymentbatchId int
	Declare @CPHInstance int
	Declare @IsGLDone int

	Declare AllCCPayBatch Cursor For
	Select Distinct CCpaymentbatchId From @PaymentHeaderTotal 

	Open AllCCPayBatch
	Fetch Next From AllCCPayBatch Into @CCpaymentbatchId
		
	While @@Fetch_Status = 0
	Begin

		Select @IsGLDone= ISGLDone from QSPcanadaOrderManagement..CreditCardBatch
		Where ID=@CCpaymentbatchId
		
		if IsNull(@IsGLDone,0) =1
		begin
			Update 	@PaymentHeaderTotal
			Set IsGLDone= 'Y'
			Where CCPaymentBatchId= @CCpaymentbatchId
		end

	Fetch Next From AllCCPayBatch Into @CCpaymentbatchId
	End
	Close AllCCPayBatch
	Deallocate AllCCPayBatch


	--Non batch CC payments
	Declare AllCCNoPayBatch Cursor For
	Select Distinct CPHInstance From @PaymentHeaderTotal Where IsNull(CCpaymentbatchId,0) = 0

	Open AllCCNoPayBatch
	Fetch Next From AllCCNoPayBatch Into @CPHInstance
		
	While @@Fetch_Status = 0
	Begin
		
		Select @IsGLDone= ISGLDone from QSPcanadaOrderManagement..NonBatchCreditCardPayment
		Where CustomerpaymentHeaderInstance=@CPHInstance
		
		If IsNull(@IsGLDone,0) =1
		Begin
			Update 	@PaymentHeaderTotal
			Set IsGLDone= 'Y'
			Where CPHInstance= @CPHInstance
		End

	Fetch Next From AllCCNoPayBatch Into @CPHInstance
	End
	Close AllCCNoPayBatch
	Deallocate AllCCNoPayBatch


	Insert into @InvGenLog	(OrderId, 
				AccountId, 
				CampaignId, 
				CustomerOrderHeaderInstance, 
				TransId,
				PaymentMethodId,
				CCPaymentBatchId ,
				CPHInstance,
				CPHTotal,
				TotalPaidByCC,
				InvoiceGenErrorCode,
				InvoiceGenErrorMessage)	
	Select OrderId,
	       AccountId,
	       CampaignId,
	       CustomerOrderHeaderInstance, 
	       0, 				--Trans Id not applicable
	       PaymentMethodId,
	       CCPaymentBatchId  ,
	       CPHInstance ,
	       CPHTotal ,
	       priceTotal,	
	       61012,	--Unposted CC payments 	
	       'CC payments have not been posted'	       	
	From @PaymentHeaderTotal 
	Where IsGLDone In('N') 
	
	--Disbaled MS
	--Insert into @InvGenLog	(OrderId, 
	--			AccountId, 
	--			CampaignId, 
	--			CustomerOrderHeaderInstance, 
	--			TransId,
	---			PaymentMethodId,
	--			CCPaymentBatchId ,
	--			CPHInstance,
	--			CPHTotal,
	--			TotalPaidByCC,
	--			InvoiceGenErrorCode,
	--			InvoiceGenErrorMessage)	
	--Select OrderId,
	 --      AccountId,
	 --      CampaignId,
	 --      CustomerOrderHeaderInstance, 
	 --      0,    --Trans Id not applicable
	 --      PaymentMethodId,
	 --      CCPaymentBatchId  ,
	 --      CPHInstance ,
	 --      CPHTotal ,
	 --      priceTotal,	
	 --      61013,	--CC Payment not Equal price total	
	 --      'CC payments not equal to price total'	       	
	--From @PaymentHeaderTotal 
	--Where IsNull(CPHTotal,0)  <>  IsNull(priceTotal,0)


	-- Check if there is one paid item which has not been sent to Remit or Shipped
	Insert into @InvGenLog	(OrderId, 
				AccountId, 
				CampaignId, 
				BatchStatus,
				CustomerOrderHeaderInstance, 
				TransId, 
				PricingDetailId,
				ProgramSectionId,
				TaxAmount,
				PaymentMethodId,
				PaymentStatusId ,
				PaymentHeaderStatusId,
				ProvinceCode,
				PaymentBatchId, 
				InvoiceGenErrorCode,
				InvoiceGenErrorMessage)	
	Select OrderId,
	       AccountId,
	       CampaignId,
	       BatchStatus,		
	       CustomerOrderHeaderInstance, 
	       TransId,
	       PricingDetailId,
	       ProgramSectionId,
	       TaxAmount,
	       PaymentMethodId,
	       Null as PaymentStatusId ,
	       Null as PaymentHeaderStatusId,
	       Null as ProvinceCode,
	       Null as PaymentBatchId,
	       61009,	--Paid item not remited/shipped	
	       'Paid item not remited/shipped'	       	
	From @AllOrderItem a
	Where ItemStatus In(502)
	And (Cast(CustomerOrderHeaderInstance as Varchar)+' '+Cast(TransId as Varchar)) not in(
											--  Inactive items will be invoiced and switch letter will be issued 
											Select Cast(CustomerOrderHeaderInstance as Varchar)+' '+Cast(TransId as Varchar)
											From QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory
											Where CustomerOrderHeaderInstance =a.CustomerOrderHeaderInstance
											And TransId=a.TransId
											And Status = 42010)  --Magazine Inactive


	-- Check if there are no shipable items and batch status is not fulfilled
	Select  Count(*)
	From @AllOrderItem 
	Where producttype in (46002,46003,46005,46006,46007,46010,46011,46012)
             And  ItemStatus  in (509,510,511)
	
	if @@Rowcount  > 0
	Begin
	     Insert into @InvGenLog ( OrderId,
				AccountId, 
				CampaignId, 
				BatchStatus,
				CustomerOrderHeaderInstance, 
				TransId, 
				PricingDetailId,
				ProgramSectionId,
				TaxAmount,
				PaymentMethodId,
				PaymentStatusId ,
				PaymentHeaderStatusId,
				ProvinceCode,
				PaymentBatchId, 
				InvoiceGenErrorCode,
				InvoiceGenErrorMessage)	
	     Select Top 1  OrderId,
	      		AccountId,
	     		CampaignId,
	       		BatchStatus,		
	      		CustomerOrderHeaderInstance, 
	       		TransId,
	       		Null,
	       		Null,
	       		Null,
	       		Null,
	       		Null,
	       		Null,
	       		Null,
	       		Null,
	       		61010,	
	       		'BHE/GIFT Item Not Shipped'	
	     From @AllOrderItem 
	     Where  ItemStatus In(509,510,511)  
	End


	-- If All item Shipped/remitted and batch status not fulfilled
	Select   *
	From @AllOrderItem 
	Where producttype in (46001,46002,46003,46005,46006,46007,46010,46011,46012)
       	 And (producttype in     (46001)And  ItemStatus  not in (502,507,512,514) OR  
	         producttype not in (46001)And  ItemStatus  not in (502,508,513)
	     )
	
	
	If @@Rowcount  =  0
	Begin
	     If (Select Top 1 Isnull(BatchStatus,0) from @AllOrderItem ) <>  40013
	     Begin
	     Insert into @InvGenLog ( OrderId,
				AccountId, 
				CampaignId, 
				BatchStatus,
				CustomerOrderHeaderInstance, 
				TransId, 
				PricingDetailId,
				ProgramSectionId,
				TaxAmount,
				PaymentMethodId,
				PaymentStatusId ,
				PaymentHeaderStatusId,
				ProvinceCode,
				PaymentBatchId, 
				InvoiceGenErrorCode,
				InvoiceGenErrorMessage)	
	     Select Top 1  OrderId,
	      		AccountId,
	     		CampaignId,
	       		BatchStatus,		
	      		CustomerOrderHeaderInstance, 
	       		TransId,
	       		Null,
	       		Null,
	       		Null,
	       		Null,
	       		Null,
	       		Null,
	       		Null,
	       		Null,
	       		61008,	--Batch Staus should be updated to fulfilled
	       		'Batch Status Error'	
	     From @AllOrderItem 
	     End
	    -- Where  ItemStatus In(510,511)  
	End


	--Update errorCode and message and insert in Invoice Gen Log

	Update 	@InvGenLog	
	Set InvoiceGenErrorCode	= 61001,
	    InvoiceGenErrorMessage = 'Zero Pricing Detail'
	Where IsNull(PricingDetailId,0) = 0
	And IsNull(InvoiceGenErrorCode,0) = 0
	And IsNull(InvoiceGenErrorMessage,'@')='@' -- Update Only if no previous error

	Update 	@InvGenLog	
	Set InvoiceGenErrorCode	= 61002,
	    InvoiceGenErrorMessage = 'Zero Program Section'
	Where IsNull(programSectionId,0) = 0
	And IsNull(InvoiceGenErrorCode,0) = 0
	And IsNull(InvoiceGenErrorMessage,'@')='@' --If not updated because of Null PricingDetail


	Update 	@InvGenLog	
	Set InvoiceGenErrorCode	= 61003,
	    InvoiceGenErrorMessage = 'Zero Tax'
	Where IsNull(TAXAmount,0) = 0
	And IsNull(InvoiceGenErrorCode,0) = 0
	And IsNull(InvoiceGenErrorMessage,'@')='@' --If not updated because of Null Program Section

				
	Update 	@InvGenLog	
	Set InvoiceGenErrorCode	= 61004,
	    InvoiceGenErrorMessage = 'Null Province Code'
	Where (IsNull(ProvinceCode,'@') = '@' OR ProvinceCode='')
	And IsNull(InvoiceGenErrorCode,0) = 0
	And IsNull(InvoiceGenErrorMessage,'@')='@' --If not updated because of Zero Tax
	
	Update 	@InvGenLog	
	Set InvoiceGenErrorCode	= 61005,
	    InvoiceGenErrorMessage = 'Invalid Payment Method'
	Where IsNull(PaymentMethodId,0) Not In(50002,50003,50004)
	And IsNull(InvoiceGenErrorCode,0) = 0
	And IsNull(InvoiceGenErrorMessage,'@')='@' --If not updated because of Province COde

	Update 	@InvGenLog	
	Set InvoiceGenErrorCode	= 61006,
	    InvoiceGenErrorMessage = 'PaymentHeader Status/ Payment Status mismatch'
	Where ( (IsNull(PaymentHeaderStatusId,0) =  600 And  IsNull(PaymentStatusId,0) <> 19000)
	         OR		
	        (IsNull(PaymentHeaderStatusId,0) = 601  And  IsNull(PaymentStatusId,0) = 19000)
	      ) 	
	And IsNull(InvoiceGenErrorCode,0) = 0
	And IsNull(InvoiceGenErrorMessage,'@')='@' --If not updated because of Payment Method

	Update 	@InvGenLog	
	Set InvoiceGenErrorCode	= 61007,
	    InvoiceGenErrorMessage = 'Zero Payment Batch, Invoice Generated'
	Where IsNull(PaymentBatchId,0) = 0
	And IsNull(InvoiceGenErrorCode,0) = 0
	And IsNull(InvoiceGenErrorMessage,'@')='@' --If not updated because of Payment Status mismatch			
	-- Set return value if there is an error found, Zero Payment batch only indicate that it is non batch payment
	Select @Cnt =	count(*) from @InvGenLog 

	If Isnull(@Cnt,0) > 0 
	Begin

	    Insert into InvoiceGenerationLog 
	    Select OrderId, 
		AccountId, 
		CampaignId, 
		BatchStatus,
		CustomerOrderHeaderInstance, 
		TransId, 
		PricingDetailId,
		ProgramSectionId,
		TaxAmount,
		PaymentMethodId,
		PaymentStatusId ,
		PaymentHeaderStatusId,
		ProvinceCode,
		PaymentBatchId, 
		CCpaymentBatchId	,
		CPHInstance		,
		CPHTotal		,
		TotalPaidByCC			,
		InvoiceGenErrorCode,
		InvoiceGenErrorMessage,
		GetDate(),
		0,		-- IsFixed
		Null		--Date Fixed
	   from @InvGenLog	
	   		
	    
	End
	--Return error exclude Zero Payment Batch Id
	Select @Cnt =	Count(*) from @InvGenLog Where InvoiceGenErrorCode	<>  61007
	If Isnull(@Cnt,0) > 0 
	Begin
		 Set @RetVal	=1
	End
	
End
*/
GO
