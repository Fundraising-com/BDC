USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[LoadPaymentXMLFile]    Script Date: 06/07/2017 09:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LoadPaymentXMLFile]  @RetVal Int OutPut 
AS


		Declare @TextLine 		Varchar(8000),
			@command 		Varchar(255),
			@i			Int,
			@PtrVal			Varbinary(16),
			@xmlDoc 		Int,
			@sqlStatement 		NVarchar(4000),
			@AccountID		Int,
			@AccountType  	Int,
			@Message		Varchar(500),
			@Cnt			Int,
			@TotalPayments 	Numeric(14,2),
			@LastPaymentId	Int,
			@CurrentYear		Varchar(10),
			@ChequeFilePath  	Varchar(100),
			@FiletoprocessRaw 	varchar(100),
			@Filetoprocess 		Varchar(100),
			@FileWithPath		Varchar(200),
			@ProcessedFileDir  	Varchar(200) ,
			@ProcessedFileDirWithFile Varchar(200),
			@BadFileDir  		Varchar(200) ,
			@BadFileDirWithFile 	Varchar(200),
			@BadOrderId		Int,
			@BadAccountId		Int,
			@BadcampaignId	Int,
			@MoveToProcessFolder Varchar(1)
			

	Declare
			@OrderID		Int,
			@CampaignID		Int,
			@PaymentMethod	Int,
			@PaymentEffectiveDate DateTime,
			@CheckNumber		Varchar(50),
			@CheckDate		DateTime ,
			@CheckPayer		Varchar(50) ,
			@CreditCardOwner	Varchar(50) ,
			@CreditCardAuthNumber Varchar(50) ,
			@Amount		Numeric(12,2),
			@ChangedBy		Varchar(50),
			@Value1 		Int,
			@GLRetVal		Int,

			@DepositDate	        	DateTime,
			@ItemCount	       	Int,
        			@DepositAmount        	Numeric(14,2),
        			@DepositStatusID      	Int,
        			@DepositAccountId    	Varchar(50),
       			@BankDepositID      	Int,
			@DepositItemId 		Int				

	Declare  @PaymentRecord	Table(
			Order_Id 		Int ,
			Account_Id 		Int ,
			Campaign_Id  		Int,
			Cheque_Number 	Varchar(50),
			Cheque_Date 		Datetime,
			Cheque_Payer		Varchar(50),
			Payment_Amount 	Numeric(10, 2),
			Deposit_Date		Datetime )
	
	
	Declare  @DataToValidate	Table(
			BatchOrderId		Int,
			BatchAccountId		Int,
			BatchCampaignId	Int,
			Order_Id 		Int ,
			Account_Id 		Int ,
			Campaign_Id  		Int,
			Deposit_Date		Datetime )

	Declare  @DBPaymentRecord	Table(
			Order_Id 		Int ,
			Account_Id 		Int ,
			Campaign_Id  		Int,
			Cheque_Number 	Varchar(50),
			Cheque_Date 		Datetime Null ,
			Cheque_Payer		Varchar(50),
			Payment_Amount 	Numeric(10, 2) ,
			Account_Type_Id  	Int ,
			Payment_Method_Id 	Int ,
			Payment_Effective_Date Datetime Null ,
			Credit_Card_Owner 	Varchar(50),
			Credit_Card_Authorization Varchar(20),
			Note_To_Print 		Varchar(100),
			Datetime_Created  	Datetime,
			Datetime_Modified 	Datetime,
			Last_Updated_By 	Varchar(30),
			Country_Code 		Varchar(10),
			PaymentId		Int,
			DepositId		Int,
			DepositDate		Datetime
			 )
	Declare  @ErrorRecord	Table(
			Order_Id 		Int ,
			Account_Id 		Int ,
			Campaign_Id  		Int,
			ErrorMessage		Varchar(250)
				      )

	Create Table #tResults  (Line_Id Int identity, Line_Text Varchar(8000) )

	Create Table #X (ID 	Int Primary Key,
			 F 	Text)


	--Get current Year
	Select @CurrentYear = Datepart(Year,Getdate())
	Set @CurrentYear = @CurrentYear+'_'

	--Get name of file to process
	Set @ProcessedFileDir = '\\PCI-FNP01.pci.swgao.int\SWCorpFTP$\QSPCanada\QSPCAFinance\ResolveChequePayments\ProcessedFiles\'
	Set @BadFileDir           = '\\PCI-FNP01.pci.swgao.int\SWCorpFTP$\QSPCanada\QSPCAFinance\ResolveChequePayments\Badfiles\'
	Set @ChequeFilePath   = '\\PCI-FNP01.pci.swgao.int\SWCorpFTP$\QSPCanada\QSPCAFinance\ResolveChequePayments\'  		
				
	--Move XML files failed to load due to validation from previous run from BAD folder  			MS Dec4, 06
	Select @command = 'MOVE '+@BadFileDir+'*.xml' +' '+ @ChequeFilePath
	
	EXECUTE AS LOGIN='qspcafulfillment';
	
	Exec master..xp_cmdshell @command

	--start processing files	
	Select @command        = 'DIR /b ' + @ChequeFilePath

	Create Table #tFileName  (Name Varchar(100) )
	Insert #tFileName Exec master..xp_cmdshell @command
	

	Select Name into #a From #tFileName Where Name Like '%.xml'

	Drop Table #tFileName

	Declare cPayFiles Cursor For
	Select Rtrim(Substring(Name,(Charindex(@CurrentYear ,Name)),100))from #a 
	
	Open cPayFiles

	Fetch Next From cPayFiles Into @Filetoprocess
	While @@Fetch_Status = 0 --------------------------------------------> Begin processing each file
	Begin

	Set @RetVal=0

	--Blank Filename return error
	If IsNull(@Filetoprocess,'')=''	Begin
	    Set @Message = 'No Cheque payment file to Process'
	    Exec QSPCanadaCommon..Send_EMail  'LoadChequeXML@qsp.com','jmiles@gafundraising.com,dpettit@gafundraising.com', 'Error in Cheque XML Import',@Message
	    Set @RetVal =1  --Error
	End

	--Check Against Files already processed
	Select Top 1 @Cnt = Count(*) From QSPCanadaFinance.dbo.ChequePaymentLog 
	Where Upper(FIleName)=Upper(Ltrim(Rtrim(@Filetoprocess))) 
	And RecordCount > 0 And TotalAmount > 0 

	--If file not already processed 
	If @Cnt =0  And @RetVal =0
	Begin
	
		Set @FileWithPath = @ChequeFilePath+@Filetoprocess
	
		Select @command = 'TYPE ' + @FileWithPath

		--Clear  tables for each file, initialize
		Delete from #tResults
		Delete from #X

		--Insert File content in table
		Insert Into #tResults
       		Exec Master..xp_cmdshell @command 

	    	Insert Into #X Values (1, '')

	    	Select @PtrVal = TextPtr(F)
      		From #X
	     	Where  ID = 1
	
    		Declare c Cursor For
    		Select Ltrim(Rtrim(Replace(Replace(Line_Text,'&',' '),'''',''''''))) From #tResults Order By line_id

	    	Open c
    		Fetch  Next From c Into @TextLine

	    	While @@fetch_status = 0
    		Begin
	                Set @TextLine = @TextLine
	        	   Select @i = i From (Select i = DataLength(F) From #X) xyx
	        	   UpdateText #X.F @PtrVal @i 0 @TextLine
        	        	Fetch  Next From c Into @TextLine
    		End
 
    		Close c
		Deallocate c
		--Format file content
		Declare @hdl int
		Exec QSPCanadaOrderManagement..sp_xml_concat_V2 @xmlDoc Out, '(SELECT F FROM #X)a', 'F'

		--Clear payment table for each file
		Delete from @PaymentRecord
	
		Insert Into  @PaymentRecord 
		Select * From OpenXML (@xmlDoc, '/PAYMENTS/PAYMENT',2)
		With (	ORDERID 		Int,
			ACCOUNTID 		Int,
			CAMPAIGNID 		Int,
			CHEQUENUMBER 	Varchar(50), 
			CHEQUEDATE 		DateTime, 
			PAYER 			Varchar(50) ,
			AMOUNT 		Numeric(10,2) ,
			DEPOSITDATE		DateTime
		 	)


		--Resolve enter blank in ChequeNumber if CASH
		Update @PaymentRecord
		Set Cheque_Number = 'CASH'
		Where IsNull(Cheque_Number,'')=''

		--If no data to process return error, a file must have record
		-- move file to badFile folder
		Select @Cnt=Count(*) From @PaymentRecord
		If IsNull(@Cnt,0)=0
		Begin
		   Set @Message = 'No payment record in Cheque payment file '+@Filetoprocess
		   Exec QSPCanadaCommon..Send_EMail  'LoadChequeXML@qsp.com','jmiles@gafundraising.com,dpettit@gafundraising.com', 'Error in Cheque XML Import',@Message
		   Set @RetVal =1  --Error

		   Exec sp_xml_removedocument @xmlDoc

		   Delete From #tResults
		   Delete From #x

		   Set @BadFileDirWithFile = @BadFileDir+@Filetoprocess
		   Select @command = 'MOVE ' + @FileWithPath + '  '+ @BadFileDirWithFile
		   Exec master..xp_cmdshell @command

	             End	

		--If file has records
		If @RetVal = 0 
		Begin
		    --Validate orders group and account
		    --Check blank data if exists send error email and move file to bad folder
		    Select @cnt= Count(*) From @PaymentRecord
		    Where (IsNull(ORDER_ID,0)=0 OR IsNull(ACCOUNT_ID,0)=0 OR 
	      	                IsNull(CAMPAIGN_ID,0)=0 OR IsNull(CHEQUE_NUMBER,'')='' OR
	    	                IsNull(PAYMENT_AMOUNT,-1)<= 0 OR IsNull(CHEQUE_DATE,'')=''  OR 
	     	                IsNull(DEPOSIT_DATE,'')='' OR (DATEDIFF(day, DEPOSIT_DATE, GetDate())>=180) 
	   	               )	
		
		   If @Cnt>0  
		   Begin
		     Set @Message = 'Missing or invalid order ifo in Cheque payment file '+@Filetoprocess
		     Exec QSPCanadaCommon..Send_EMail  'LoadChequeXML@qsp.com','jmiles@gafundraising.com,dpettit@gafundraising.com', 'Error in Cheque XML Import',@Message
		     Insert Into QSPCanadaFinance.dbo.ChequePaymentLog Values( Ltrim(Rtrim(@Filetoprocess)),GetDate(),0,0, 0,@Message,Null)
		     Set @RetVal =1 --Error
			
		     Exec sp_xml_removedocument @xmlDoc
	
		      Delete From #tResults
		      Delete From #x

		     Set @BadFileDirWithFile = @BadFileDir+@Filetoprocess
		     Select @command = 'MOVE ' + @FileWithPath + '  '+ @BadFileDirWithFile
   		     Exec master..xp_cmdshell @command
		   End
		End --If file has records


		--if no blank data 
		If @RetVal = 0 
		Begin
		   --Check if mutiple deposit date exists in file
		    Select Distinct Deposit_date From @PaymentRecord
		    If @@Rowcount >1
		    Begin
			Set @Message = 'Multiple Deposit dates in Cheque payment file '+@Filetoprocess
			Exec QSPCanadaCommon..Send_EMail  'LoadChequeXML@qsp.com','jmiles@gafundraising.com,dpettit@gafundraising.com', 'Error in Cheque XML Import',@Message
			Insert Into QSPCanadaFinance.dbo.ChequePaymentLog Values( Ltrim(Rtrim(@Filetoprocess)),GetDate(),0,0, 0,@Message,Null)
			Set @RetVal =1 --Error

			Exec sp_xml_removedocument @xmlDoc
			
			 Delete From #tResults
		              Delete From #x

			Set @BadFileDirWithFile = @BadFileDir+@Filetoprocess			Select @command = 'MOVE ' + @FileWithPath + '  '+ @BadFileDirWithFile
			Exec master..xp_cmdshell @command
		     End
	                End  --if no blank data 

		--if no mutiple deposit date
		If @RetVal = 0 
		Begin
		    -- Check no existant orderId, AccountId or CampaignId	
		    Insert @DataToValidate
		    Select b.Orderid,b.accountid,b.campaignid,p.Order_Id,p.Account_Id,p.Campaign_id,p.Deposit_date
		    From @PaymentRecord p, QSPCanadaOrdermanagement..batch b
		    Where p.order_Id *=b.orderid

		    Insert into @ErrorRecord
		    Select Order_Id,Account_Id,Campaign_Id,'Non Existant or Invalid OrderId,AccountId or CampaignId'
		    From @DataToValidate
		    Where ( ORDER_ID     <> IsNull(BatchOrderId,0)   OR
			    Account_Id   <> IsNull(BatchAccountId,0) OR
	           	                 Campaign_Id  <> IsNull(BatchCampaignId,0)
	      		   )	

		   --Check if there are orders which not exists in DB  but payment has been received
		   Select @cnt=Count(*) From @ErrorRecord
		   If @Cnt>0
		   Begin
			Declare @ErrorMessageText	Varchar(8000)
			Declare @Leng			Int
			--For Error email
			Set @ErrorMessageText =  'Non Existant or Invalid OrderId,AccountId or CampaignId'+CHAR(13)
			--For Cheque Payment log
			Set @Message =  'Non Existant or Invalid OrderId,AccountId or CampaignId'

			Declare ErrorIds Cursor For
    			Select Order_Id,Account_Id,Campaign_id From @ErrorRecord

    			Open ErrorIds
	    		Fetch  Next From ErrorIds Into @BadOrderId,@BadAccountId,@BadCampaignId
		
			While @@fetch_status = 0 And IsNull(@Leng,0) < 7500 --Max is 8000
    			Begin
				Set @ErrorMessageText= IsNull(@ErrorMessageText,'')+ Str(@BadOrderId)+','+Str(@BadAccountId)+','+Str(@BadCampaignId)+ CHAR(13)
				Set @Leng = IsNull(@Leng,0)+Len(@ErrorMessageText)
			
				--Donot Insert multiple record each Order, CA or Account MS May 26, 06
				--Insert Into QSPCanadaFinance.dbo.ChequePaymentLog Values( Ltrim(Rtrim(@Filetoprocess)),GetDate(),0,0, 0,@Message,Null)

			Fetch  Next From ErrorIds Into @BadOrderId,@BadAccountId,@BadCampaignId
			End

			Insert Into QSPCanadaFinance.dbo.ChequePaymentLog Values( Ltrim(Rtrim(@Filetoprocess)),GetDate(),0,0, 0,@Message,Null)

			Close ErrorIds
			Deallocate ErrorIds
		
			Exec QSPCanadaCommon..Send_EMail  'LoadChequeXML@qsp.com','jmiles@gafundraising.com,dpettit@gafundraising.com', 'Error in Cheque XML Import', @ErrorMessageText

			Set @RetVal =1 --Error
			Exec sp_xml_removedocument @xmlDoc
		
			 Delete From #tResults
		              Delete From #x

			Set @BadFileDirWithFile = @BadFileDir+@Filetoprocess
			Select @command = 'MOVE ' + @FileWithPath + '  '+ @BadFileDirWithFile
			Exec master..xp_cmdshell @command
			
		     End
		End --if no mutiple deposit date

		--If all orders exists in DB for which payment have been received
		If @RetVal = 0 
		Begin

		   --Data is validated now Prepare record for DB insert, initialize @DBPaymentRecord
		   Delete from @DBPaymentRecord
		   
		   Insert Into @DBPaymentRecord( Order_Id ,Account_Id, Campaign_Id, Cheque_Number, Cheque_Date, Cheque_Payer, Payment_Amount,DepositDate )
		   Select Order_Id ,Account_Id, Campaign_Id, Cheque_Number, Cheque_Date, Cheque_Payer, Payment_Amount,Deposit_Date From @PaymentRecord

		   Declare Acc Cursor For
    		   Select Account_id From @DBPaymentRecord 

    		   Open Acc
    		   Fetch  Next From Acc Into @AccountID
    		   While @@Fetch_Status = 0
    		   Begin
			Exec QSPCanadafinance..GetAccountType @AccountID, @AccountType Output
	     	
	        		Update @DBPaymentRecord Set Account_type_id  = IsNull(@AccountType,0) Where Account_Id=@AccountID
       		   Fetch  Next From Acc Into @AccountID
    		   End
 
    		   Close Acc
		   Deallocate Acc
	
		   Update @DBPaymentRecord
		   Set 	
			Payment_Method_Id 	=50002,
			Payment_Effective_Date  =GetDate() ,
			Datetime_Created  	=GetDate(),
			Datetime_Modified 	=Null,
			Last_Updated_By 	='ChequeLoad',
			Country_Code 		='CA'

	
		   -- starting payment id for these payment record being inserted
		   Select @LastPaymentId = Max(Payment_Id)  From QSPCanadaFinance..Payment

		  --Insert Payment record and Create GL entry
		  Declare @PaymentSuccess Int
		  Set @PaymentSuccess = 1

		  Begin Transaction		
		  Declare Payments Cursor For
    		  Select  Order_Id,
			  Account_Id,
			  Account_Type_Id,
			  Campaign_id,
			  Payment_Method_Id, 
			  Payment_Effective_Date,
			  Cheque_Number,  
			  Cheque_Date,  
			  Cheque_Payer,
			  Credit_Card_Owner,	
	 		  Credit_Card_Authorization,
			  Payment_Amount,
			  Last_Updated_By		
		  From    @DBPaymentRecord 
		
    		  Open Payments
		  Fetch  Next From Payments Into @OrderID,
					@AccountId,
					@AccountType,
					@CampaignID		,
					@PaymentMethod		,
					@PaymentEffectiveDate   ,
					@CheckNumber		,
					@CheckDate		,
					@CheckPayer		,
					@CreditCardOwner	,
					@CreditCardAuthNumber 	,
					@Amount			,
					@ChangedBy		
			
		  While @@fetch_status = 0 And @PaymentSuccess =1
    		  Begin
	
		  Insert Payment 
		  Select  @AccountID, 
			  ISNULL(@AccountType,0),
			  @PaymentMethod,
			  @PaymentEffectiveDate, 
			  @CheckNumber,
			  @CheckDate,
			  @CheckPayer,
			  @CreditCardOwner,
			  @CreditCardAuthNumber,
			  @Amount, 
			  Null, 		--NoteToPrint, 
			  GetDate(),  	--date created
			  Null, 		-- date modified
			  @ChangedBy,
			  @OrderID, 
			  'CA',  		--Country Code
			  @CampaignID
	
		  SET @Value1 = Scope_Identity()	
		

		  --Create GL for payment When payment inserted successfully
		  If @@Error =0
		  Begin
		
		  Exec GL_Entry_InsertPayment @Value1 -- @PaymentId
		
		   End
		   Else  -- Failed to insert payment record in DB
		   Begin
			Set @PaymentSuccess = 0
		   End

		   Update @DBPaymentRecord set Paymentid=@value1
		   Where Current Of Payments

		
	                Fetch  Next From Payments Into  @OrderID,
					          @AccountId,
					          @AccountType,
					          @CampaignID		,
					          @PaymentMethod		,
					          @PaymentEffectiveDate,
					          @CheckNumber		,
					          @CheckDate		,
					          @CheckPayer		,
					          @CreditCardOwner	,
					          @CreditCardAuthNumber 	,
					          @Amount			,
					          @ChangedBy	
	          End
	          Close Payments
	          Deallocate Payments

	          If @PaymentSuccess = 1
	          Begin
		Select @ItemCount=count(*) , @DepositAmount=sum(Payment_Amount), @DepositDate = Isnull(DepositDate, convert(DateTime,getdate(),101))
		From @DBPaymentRecord Group By DepositDate

		Select @ItemCount,@DepositAmount,@DepositDate

		--Create Deposit record
		Exec dbo.AddBankDeposit	@DepositDate ,	@ItemCount ,   @DepositAmount  ,    55002,    1   ,   @BankDepositID   output
		
		--Procedure use the current date as deposit date change it to actual date
		Update QSPCanadaFinance..Bank_Deposit 
		Set  Deposit_Date= @DepositDate
		Where  Bank_Deposit_ID  =@BankDepositID


		Update @DBPaymentRecord set Depositid= @BankDepositID 

	
		--Create DepositItem record
		Declare DepositItem Cursor For
    		Select  DepositId,PaymentId From @Dbpaymentrecord

		Open DepositItem
		Fetch  Next From DepositItem Into @BankDepositID,@Value1
		While @@fetch_status = 0 
    		Begin
	
		Exec dbo.AddBankDepositItem	 @BankDepositID ,   @Value1   ,  @DepositItemId    output

		Fetch  Next From DepositItem Into @BankDepositID,@Value1
		End
		Close DepositItem
		Deallocate DepositItem
		
		--Successful processing
		Insert Into QSPCanadaFinance.dbo.ChequePaymentLog Values( Ltrim(Rtrim(@Filetoprocess)),GetDate(),IsNull(@ItemCount,0),IsNull(@DepositAmount,0), @LastPaymentId+1,Null,Null)
		If @@Error =0 
		Begin
			Commit
			Set @MoveToProcessFolder = 'Y'
			Set @RetVal =0
		End
		ELSE
		Begin
			Rollback
			Set @RetVal =1 --Error
			Set @Message = 'Cannot create payment log record, rollback for Cheque payment file '+@Filetoprocess
			Exec QSPCanadaCommon..Send_EMail  'LoadChequeXML@qsp.com','jmiles@gafundraising.com,dpettit@gafundraising.com', 'Error in Cheque XML Import', @Message
			Set @MoveToProcessFolder = 'N'
		End

	End
	Else --If Payment insert is not successfull
	Begin
		
		Rollback
		Set @RetVal =1 --Error
		Set @Message = 'Cannot create payment record, rollback for Cheque payment file '+@Filetoprocess
		Exec QSPCanadaCommon..Send_EMail  'LoadChequeXML@qsp.com','jmiles@gafundraising.com,dpettit@gafundraising.com', 'Error in Cheque XML Import', @Message
		Set @MoveToProcessFolder = 'N'
	End

	Exec sp_xml_removedocument @xmlDoc

	End
	End
	Else	--File Name found in Payment log with record count > 0
	Begin

		Set @Message = 'File already exists cannot process cheque payment file '+@Filetoprocess
		Exec QSPCanadaCommon..Send_EMail  'LoadChequeXML@qsp.com','jmiles@gafundraising.com,dpettit@gafundraising.com', 'Error in Cheque XML Import',@Message
		Set @RetVal =1 --error
		Set @MoveToProcessFolder = 'N'
	End

	--Move file to corresponding folder based on success
	If IsNull(@MoveToProcessFolder,'N') = 'Y'
	Begin
		Set @ProcessedFileDirWithFile = @ProcessedFileDir+@Filetoprocess
		Select @command = 'MOVE ' + @FileWithPath + '  '+ @ProcessedFileDirWithFile
		EXEC master..xp_cmdshell @command
	End
	Else
	Begin
		Set @BadFileDirWithFile = @BadFileDir+@Filetoprocess
		Select @command = 'MOVE ' + @FileWithPath + '  '+ @BadFileDirWithFile
		EXEC master..xp_cmdshell @command
	End

	Fetch Next From cPayFiles Into @Filetoprocess
	End

	Close cPayFiles
	Deallocate cPayFiles	

	Drop Table #a
	Drop Table #tResults
	Drop Table #X
GO
