USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spDoCreditCardGL_MS]    Script Date: 06/07/2017 09:20:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE      PROCEDURE [dbo].[spDoCreditCardGL_MS] 
 	 @CCBatchid int


 AS

Begin	
	declare @paymentAmount Numeric(10,2)
	declare	@RetVal int 
	declare @paymentid int

	declare a cursor for
	select   payment_id, payment_amount  from   qspcanadafinance..payment 
	where cast(convert(varchar(10),datetime_created,101) as datetime) = '04/28/2005'
	and cast(convert(varchar(10),payment_effective_date,101) as datetime) = '04/30/2005'
	and payment_id  < 0

OPEN a
	FETCH NEXT FROM a    INTO
		 @paymentId,
		 @paymentAmount
				
	while(@@fetch_status = 0)
	begin

		Select cast(@paymentAmount  as varchar) + '  '+ cast( @paymentid as varchar)
	
		If @paymentAmount > 0 
		Begin

			exec QSPCanadaFinance..GL_Function Null 		,	--Invoice_Id
				 @PaymentId 					,	--PaymentId
				 Null						,	--AdjustmentId
				 2   						,	-- Trans Id 2 for PaymentRecording
				 62	      					,    	-- @Entity QSP
				 @paymentAmount 				,	--PaymentAmount
				 @RetVal                				-- OutPutVariable returns 0 or 1 O --> Success
				
		End
	     
		


		FETCH NEXT FROM a INTO
		 @paymentId,
		 @paymentAmount

	end
	
	close a
	deallocate a

end
GO
