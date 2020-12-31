USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spDoCreditCardGL]    Script Date: 06/07/2017 09:20:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE       PROCEDURE [dbo].[spDoCreditCardGL] 
 	 @CCBatchid int, @PaymentProcessed Int OutPut


 AS

	
	--**    Get all the accounts that participated in this run
	--**    push in payments and then do a GL
	

  Begin Tran t1

	
	declare @Error int
	select @Error = 0
	declare @id int
	declare @orderid int
	declare @campaign int
	declare @accountid int
	declare @amnt numeric(10,2)
	declare @authcode varchar(6)
	declare @authdate datetime
	declare @payment int
	declare @last varchar(40)
	declare @first varchar(40)
	declare @paymentid int
	declare @CustomerType int
	declare @Cnt		int

	--declare @CCBatchid int
	--select @CCBatchid=75
	
	-- Below comment no longer applies per Spring 2007
	-- For Staff order CC amount charged is half so reduced amount to 1/2 of full amount 
	declare a cursor for
	select CreditCardBatch.id,
	           orderid,
	           Batch.CampaignID,
	           COH.accountid, 
	          (Case ca.IsStaffOrder 
		When 0	 Then Round(TotalAmount,2)
		When 1	 Then Round(TotalAmount,2) -- just took out the 1/2 off in case come back KT
	           End) TotalAmount,		
	         AuthorizationCode,
	         authorizationdate,
	         (Case Substring(CreditCardNumber,1,1)
	 	When  4 Then 50003
		When 5	Then 50004
		Else 0
	        End) PaymentMethodInstance,			
	        lastname, 
	        firstname,
	        ISNULL(c.Type, 0)
	from creditcardbatch,
	       creditcardpayment,
   	       customerpaymentheader cph,
   	       customerorderheader coh,
                    batch,
                    customer c,
	      QSPCanadaCommon..campaign ca	 
	where     (isGLDone = 0 or isgldone is null)
	and CreditCardBatch.ID = Batchid
	and customerpaymentheaderinstance=cph.instance
	and coh.instance = cph.customerorderheaderinstance
	and cph.iscreditcard=1
	and orderbatchdate=date
	and orderbatchid=batch.id
	and batch.campaignid=ca.id
	and c.instance = customerbilltoinstance
	and creditcardpayment.statusinstance=19000 -- good
	and CreditcardBatch.id = @CCBatchid

	OPEN a
	FETCH NEXT FROM a INTO
		 @id ,
		 @orderid ,
		 @campaign ,
		 @accountid ,
		 @amnt,
		 @authcode,
		 @authdate,
		 @payment,
		 @last,
		 @first, 
		@CustomerType
							
	Set @Cnt = 0	-- No of record processed
	while(@@fetch_status = 0)
	begin

		Set @Cnt = @Cnt +1
		if(@@Error <> 0)
		begin
			select @Error = 1	
		end
		
		select * from QSPCanadaFinance..ACCOUNT where account_id=@accountid
		if(@@rowcount = 0)
		begin
			insert QSPCanadaFinance..ACCOUNT
			(
				ACCOUNT_ID,
				ACCOUNT_TYPE_ID,
				EMPLOYEE_NAME,
				LAST_STATEMENT_DATE,
				LAST_AGING_DATE,
				AGING_CURRENT,
				AGING_30,
				AGING_60,
				AGING_90,
				AGING_120_OVER,
				DATE_CREATED,
				DATE_MODIFIED,
				LAST_UPDATED_BY,
				COUNTRY_CODE
			)
			select @accountid,
				--1,
				@CustomerType,-- good payer 'caseu new account
				NULL,
				NULL,
				NULL,
				0,
				0,
				0,
				0,
				0,
				GetDate(),
				GetDate(),
				'cc_pack_execution',
				'CA'
				 
		end	
		if(@@Error <> 0)
		begin
			 exec QSPCanadaCommon..Send_EMail  'spDoCreditCardGL@qsp.com','qsp-qspfulfillment-dev@qsp.com',
						'spDoCreditCardGL', 'Error inserting into ACCOUNT'
			 Select @Error = 1
		end

		Insert QSPCanadaFinance..Payment
		(
			ACCOUNT_ID,
			ACCOUNT_TYPE_ID,
			PAYMENT_METHOD_ID,
			PAYMENT_EFFECTIVE_DATE,
			CHEQUE_NUMBER,
			CHEQUE_DATE,
			CHEQUE_PAYER,
			CREDIT_CARD_OWNER,
			CREDIT_CARD_AUTHORIZATION,
			PAYMENT_AMOUNT,
			NOTE_TO_PRINT,
			DATETIME_CREATED,
			DATETIME_MODIFIED,
			LAST_UPDATED_BY,
			ORDER_ID,
			COUNTRY_CODE,
			CAMPAIGN_ID
		)
		select @accountid,
			@CustomerType, 
			@payment,
	--		2,  - approved
			@authdate,
			NULL,
			NULL,
			NULL,
			@first+' '+ @last,
			@authcode,
			@amnt,
			NULL,
	--		NULL,
			GetDate(),
			GetDate(),
			'cc_pack_execution',
			@orderid,
			'CA',	
			@campaign

		select @paymentid = Scope_Identity()
			

		-- Scope_Identity() to get new payment id
	
		if(@@Error <> 0)
		begin
			 exec QSPCanadaCommon..Send_EMail  'spDoCreditCardGL@qsp.com','qsp-qspfulfillment-dev@qsp.com',
						'spDoCreditCardGL', 'Error inserting into ACCOUNT'
			 Select @Error = 1
		end
	
	
	
		-- Call the GL_Function
	
		declare @paymentAmount Numeric(10,2)
	
		Select @paymentAmount = Payment_Amount from QSPCanadaFinance..Payment Where Payment_Id = @paymentid	
		If @paymentAmount > 0 
		Begin
			EXEC QSPCanadaFinance..GL_Entry_InsertPayment @PaymentID	
		End

		if(@@Error <> 0)
		begin
		        exec QSPCanadaCommon..Send_EMail  'spDoCreditCardGL@qsp.com','qsp-qspfulfillment-dev@qsp.com',
						'spDoCreditCardGL', 'Error in GL Function'
			Select @Error = 1
		end
	
--		drop table #spDoCreditCardGL

		FETCH NEXT FROM a INTO
			 @id ,
			 @orderid ,
			 @campaign ,
			 @accountid ,
			 @amnt,
			 @authcode,
			 @authdate,
			 @payment,
			 @last,
			 @first,
			@CustomerType 

	end
	
	close a
	deallocate a

	if( @Error = 0 )
	begin
		COMMIT TRAN T1
	end
	else
	begin

		--
		--**   Rollback the whole thing and set shipment to -1
		--
		ROLLBACK tran T1
		declare @str varchar(500)
		select @str = 'Rollback occurred in GL Transaction for cc'
		exec QSPCanadaCommon..Send_EMail  'DoCreditcardGL@qsp.com',
					'qsp-qspfulfillment-dev@qsp.com',
					'Rollback', @str
	
	
	end

	Update CreditCardBatch set IsGLDone = 1 
		 where  IsNull(IsGLDone,0) = 0 and id = @CCBatchid

	Set @PaymentProcessed = @Cnt
GO
