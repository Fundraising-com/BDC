USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[DoGlForPayments]    Script Date: 06/07/2017 09:19:26 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[DoGlForPayments]
AS

begin

declare  @paymentId		Int,
	@paymentAmount	Numeric(10,2),
	@RetVal		Int,
	@MaxrowCount		Int

declare @Tabpay Table (Tindex 	Int Identity,
			PaymentId	Int,
			Amount		Numeric(10,2)
			)

	Insert into @Tabpay
		select payment_id, payment_amount 
		from QspCanadaFinance..payment 
		Where Payment_Id   > 452393 and Payment_Id <= 452777 
		--where order_id in  (SELECT orderid FROM QSPCanadaOrderManagement..Batch B WHERE OrderQualifierID = 39003)
		--and payment_method_id <> 50002  
		order by payment_id     --1094 

	Select @MaxrowCount = Count(*) from @Tabpay
		
	While @MaxrowCount > 0
	begin

	Select @paymentId = PaymentId , @paymentAmount= Amount From @Tabpay where Tindex =    @MaxrowCount

			exec QSPCanadaFinance..GL_Function Null 		,	--Invoice_Id
				 @PaymentId 					,	--PaymentId
				 Null						,	--AdjustmentId
				 2   						,	-- Trans Id 2 for PaymentRecording
				 62	        					,    	-- @Entity QSP
				 @paymentAmount  				,	--PaymentAmount
				 @RetVal         				-- OutPutVariable returns 0 or 1 O --> Success
				
	
	Set @MaxrowCount = @MaxrowCount -1
	End

	
End
GO
