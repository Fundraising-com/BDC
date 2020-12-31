USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[VerifyAndRemitPaidItem]    Script Date: 06/07/2017 09:17:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[VerifyAndRemitPaidItem] @OrderId Int , @ReturnCode Int OutPut, @ReturnMessage Varchar(200) OutPut

As
	Declare
		@coh		Int,
		@TransId	Int,
		@TaxAmount	Numeric(10,2),
		@ItemStatus	Int,
		@ProvinceCode	Varchar(10),
		@ReturnErrorCode	Int,
		@ReturnErrorMessage Varchar(400),
		@RemitStatus  	Int,
		@SentToRemit    Varchar(1),
		@ErrorInRemiting Varchar(1)
	

	Declare AllItemNotRemited Cursor For
	Select    h.Instance,
		d.TransId,
		d.statusInstance ItemStatus, 
		IsNull(a.state,' ') State, 
		d.Tax
	From      QSPCanadaOrderManagement.dbo.CreditCardPayment ccp RIGHT OUTER JOIN
              	QSPCanadaOrderManagement.dbo.Batch b INNER JOIN
             		QSPCanadaOrderManagement.dbo.CustomerOrderHeader h ON b.ID = h.OrderBatchID AND b.[Date] = h.OrderBatchDate INNER JOIN
              	QSPCanadaOrderManagement.dbo.CustomerOrderDetail d ON h.Instance = d.CustomerOrderHeaderInstance INNER JOIN
              	QSPCanadaCommon.dbo.CAccount a ON b.AccountID = a.Id LEFT OUTER JOIN
              	QSPCanadaOrderManagement.dbo.CustomerPaymentHeader cph ON h.Instance = cph.CustomerOrderHeaderInstance ON 
                      	ccp.CustomerPaymentHeaderInstance = cph.Instance
	Where   orderid= @OrderId 
		And d.producttype  in (46001)	
		And d.statusInstance in (502) 		--Paid
		And ( (h.paymentMethodInstance <> 50002 And ccp.StatusInstance = 19000) 
			Or paymentMethodInstance=50002) 
		And d.delflag<> 1			--Not deleted
	
	Open AllItemNotRemited
	Fetch Next From  AllItemNotRemited  Into
		 @coh ,
		@TransId,
		@ItemStatus,
		@ProvinceCode,
		@TaxAmount

	If  @@Fetch_Status = -1
	Begin
		Set @RemitStatus = 1
	End	
		
	

	While(@@Fetch_Status = 0)
	begin
		
		Set @SentToRemit = 'N'

		If IsNull(@TaxAmount,0) = 0 Or IsNull(@ProvinceCode,'')=''
		Begin
			
			Exec QSPCanadaFinance.dbo.VerifyZeroTaxAndFix @OrderId , @ReturnErrorCode Output, @ReturnErrorMessage Output

			
			If IsNull(@ReturnErrorCode,1)  <> 1
			Begin
				-- Tax calculated now sent to remit in case of error, email will be sent 
				Exec QSPCanadaOrderManagement.dbo.spRemitIndividualItem_MS @coh, @TransId, @RemitStatus  Output

				If IsNull(@RemitStatus,1) = 0
				Begin
					Set @SentToRemit = 'Y'
					
				End
				Else	-- Tax Calculated but error in remitting
				Begin
					Set @ErrorInRemiting = 'Y'
				End
				
			End
			
		End

		-- Tax and Provice code ok
		If  @SentToRemit = 'N'  
		Begin

			Exec QSPCanadaOrderManagement.dbo.spRemitIndividualItem_MS @coh, @TransId, @RemitStatus  OutPut

		End

		--Sent to remit All or None 	
		If IsNull(@RemitStatus,1) <> 0
		Begin
			Break
		End
		
		
		Fetch Next From AllItemNotRemited Into
			@coh ,
			@TransId,
			@ItemStatus,
			@ProvinceCode,
			@TaxAmount

	End

	If IsNull(@RemitStatus,1) <> 0
	Begin
		Set @ReturnMessage = 'Error sending item to remit'
		Set @ReturnCode = 1
	End
	Else
	Begin
		Set @ReturnMessage = 'No error sending item to remit'
		 Set @ReturnCode =0
	End

	Close AllItemNotRemited
	Deallocate AllItemNotRemited
GO
