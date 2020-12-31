USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[VerifyZeroTaxAndFix]    Script Date: 06/07/2017 09:17:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[VerifyZeroTaxAndFix]  @OrderId Int, @OutPut Int OutPut, @OutPutMessage Varchar(200) OutPut
 AS

Declare @AccountId 		Int,
	@State			Varchar(10),
	@AddressListId		Int,
	@StateProvince 	Varchar(10),
	@tax			Numeric(10,2),
	@PricingDetailsId	Int,
	@ProgramSectionId 	Int,
	@EmailSubject		Varchar(100),
	@EmailFrom		Varchar(100),
	@ErrorMessage		Varchar(1000)


Declare
	@TaxError TABLE (
		Id				Int	Identity,
		AccountId			Int	,
		State				Varchar(2),	
		AddressListId			Int,
		Tax				Numeric(10,2),
		PricingDetailsId			Int,
		ProgramSectionId		Int

		)



	Set @EmailSubject = 'Invalid pricing details / program section for Order Id '+Cast(@OrderId as Varchar)
	Set @EmailFrom = 'qspfulfillment@qsp.com'               		


	Insert into @TaxError
	Select    b.AccountId ,  IsNull(a.state,' '), AddressListId,Tax, d.PricingDetailsId,d.ProgramSectionId
	From      QSPCanadaOrderManagement..Batch b,
		QSPCanadaOrderManagement..CustomerOrderHeader h,
		QSPCanadaOrderManagement..CustomerOrderDetail d,
		QSPCanadaCommon..CAccount a,
 		QSPCanadaOrderManagement..CustomerPaymentHeader cph
	 	left outer join QSPCanadaOrderManagement..CreditCardPayment ccp on 
	 	cph.Instance=ccp.CustomerPaymentHeaderInstance
	Where   b.id=h.orderbatchid
		And b.date=h.orderbatchdate
		And h.Instance = d.CustomerOrderHeaderInstance
		And h.instance=cph.CustomerOrderHeaderInstance
		And a.ID = b.AccountID
		And orderid= @OrderId 
		And d.producttype not in (46013,46014,46015)	
		And d.statusInstance in (502,507,508,512,513) 	--Remit, ship,un-remittable,un-shipable
		And ( (h.paymentMethodInstance <> 50002 And ccp.StatusInstance = 19000) Or paymentMethodInstance=50002) 
		And d.delflag<> 1				--Not deleted
		And (d.tax=0  Or IsNull(a.state,'')='')


		Select Top 1 	@AccountId = AccountId,
				@State=State,
				@AddressListId = AddressListId,
				@Tax =Tax, 
				@PricingDetailsId = PricingDetailsId,
				@ProgramSectionId=ProgramSectionId
				
		From @TaxError Where  Tax = 0

		If @@Rowcount > 0  
		Begin
			If (IsNull(@ProgramSectionId ,0) >0 And IsNull(@PricingDetailsId,0) >0)
			Begin

				Select @StateProvince = StateProvince from QSPCanadacommon..address where addresslistid=@AddressListId And address_Type = 54001 --Shipto

				If @@Rowcount > 0
				Begin
					
					If  IsNull(@StateProvince,'') <> ''
					Begin
						--If State code exists in address but not exists in CAccount update Caccount record with state code from Address
						If  IsNull(@State,'') = ''  
						Begin
							Set @OutPutMessage = 'Province code update to '+@StateProvince+ ' for AddressListId ='+cast(@AddressListId as varchar)
							Update QSPCanadaCommon..CAccount Set State = @StateProvince
							Where Id = @AccountId
						End

						-- Account address record exists get state
						EXEC QSPCanadaCommon.dbo.PR_TEMP_FIX_TAXES  @OrderId
				
						Set @OutPut = 0
					End
					Else
						Begin
						   Set @OutPutMessage = 'Blank state / province code in QSPCanadacommon..address for account '+Cast(@AccountId as Varchar)
						   Set @OutPut =1
						End
				End
				Else
				Begin
					--Need to create record in QSPCanadacommon..address for account
					Set @OutPutMessage = 'Address record not exists for account '+Cast(@AccountId as Varchar)
					Set @OutPut =1
				End

			End
			Else
			Begin
				Set @OutPutMessage = 'Tax can not be calculated, bad program section / pricing details'
				Set @ErrorMessage  = @OutPutMessage
				Set @OutPut =1
				Exec QSPCanadaCommon.dbo.Send_EMAIL @EmailFrom,'qsp-IT-canada@qsp.com',@EmailSubject,@ErrorMessage
			End

		End	--Tax=0
		Else  	-- No record with zero tax check if the province code is blank
		Begin
			Select Top 1 	@AccountId = AccountId,
					@State=State,
					@AddressListId = AddressListId,
					@Tax =Tax, 
					@PricingDetailsId = PricingDetailsId,
					@ProgramSectionId=ProgramSectionId

			From @TaxError Where  IsNull(State,'') = ''

			If @@Rowcount > 0  
			Begin
				Select @StateProvince = StateProvince from QSPCanadacommon..address where addresslistid=@AddressListId And address_Type = 54001 --Shipto

				If @@Rowcount > 0
				Begin
					
					If  IsNull(@StateProvince,'') <> ''
					Begin
						--If State code exists in address but not exists in CAccount update Caccount record with state code from Address
						If  IsNull(@State,'') = ''  
						Begin
							Set @OutPutMessage = 'Province code update to '+@StateProvince+ ' for AddressListId ='+cast(@AddressListId as varchar) 

							Update QSPCanadaCommon..CAccount Set State = @StateProvince
							Where Id = @AccountId
							
							Set @OutPut =0
						End
					End
					Else
						Begin
						  Set @OutPutMessage = 'Blank state / province code in QSPCanadacommon..address for account '+Cast(@AccountId as Varchar)
						  Set @OutPut =1
						End
					
				End
				Else
				Begin
					--Need to create record in QSPCanadacommon..address for account
					Set @OutPutMessage = 'Address record not exists for account '+Cast(@AccountId as Varchar)
					Set @OutPut =1
				End
			End
			Else
				Begin
				    Set @OutPutMessage = 'No billable item without tax found'
				    Set @OutPut =0
				End 
		End
GO
