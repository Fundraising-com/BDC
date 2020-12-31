USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spRemitAPVerification]    Script Date: 06/07/2017 09:20:55 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[spRemitAPVerification] 	@FromBatchID 	Int, 
							@ToBatchID 		Int,
							@RetValAP		Int 	OutPut,
							@ErrorMessageAP	Varchar(100) OutPut
AS
	Declare 
	@Cnt			Int,
	@CurCnt 		Int,
	@TitleCount 		Int,
	@MaxId 		Int,
	@MaxRecNo 		Int,
	@RecCounter 		Int,
	@Currency 		Varchar(5),
	@TotalAp 		Numeric(12,6),
	@TotalFromInterface	Numeric(12,6),
	@TitleCode		Varchar(10),
	@MissingTable		Varchar(20),
	@PayGroup		Varchar(50),

	@APTitleCode		Varchar(10),
	@TotalPaymentAP 	Numeric(14,2),
	@CurrencyCodeAP	Varchar(10),
	@PayGroupAP		Varchar(50),

	@RemitTitle		Varchar(10),
	@TotalPaymentRemit	Numeric(14,2),
	@CurrencyCodeRemit 	Varchar(10),
	@PayGroupRemit	Varchar(50),

	@TotalAmountDiscrepancyUSD Numeric(14,2),
	@TotalAmountDiscrepancyCAD Numeric(14,2),

	@ExcessAmount  Numeric(14,2)


	/**************** Table variables Remit data ***********************/
	Declare
	@AllItem TABLE (
				Id				Int	Identity,
				FulfHouseId			Int	,
				PublisherId			Int,	
				TitleCode			Varchar(10),	
				Province			Varchar(20),
				Currency			Varchar(10),
				PayGroupLookupCode		Varchar(50),
				TotalBasePrice			Numeric(10,2),
				TotalRemitAmount		Numeric(12,6),
				TotalTax1Amount		Numeric(12,6),
				TotalTax2Amount		Numeric(12,6)
				)

	Declare
	@MagazineTotalByTitle TABLE (
				Id				Int	Identity,
				FulfHouseId			Int	,
				PublisherId			Int,	
				TitleCode			Varchar(10),	
				Currency			Varchar(10),
				PayGroupLookupCode		Varchar(50),
				TotalBasePrice			Numeric(10,2),
				TotalRemitAmount		Numeric(12,2),
				GST				Numeric(12,2),
				HST				Numeric(12,2),
				PST				Numeric(12,2),
				TotalTax			Numeric(12,2),
				TotalPayment			Numeric(14,2)

				)
	Declare
	@DistinctTitleFromRemit TABLE (
				Id			Int	Identity,
				TitleCode		Varchar(10)
				)

	Declare
	@DistinctPayGroupFromRemit TABLE (
				Id			Int	Identity,
				PayGroupLookupCode	Varchar(50)
				)


	Declare
	@TotalAPbyCurrency TABLE (
				Id			Int	Identity,
				TotalPayment		Numeric(14,2),
				Currency		Varchar(10),
				ExcessAmount		Numeric(14,2)
					)

	Declare
	@TotalAPbyPayGroup TABLE (
				Id			Int	Identity,
				TotalPayment		Numeric(14,2),
				PayGroupLookupCode  	Varchar(50),
				ExcessAmount		Numeric(14,2)
					)


	/**************** Table variables Interface data ***********************/

	Declare
	@TotalFromApInterface TABLE (
				Id			Int	Identity,
				TitleCode		Varchar(10),	
				TotalPayment		Numeric(14,2),
				Currency		Varchar(10),
				PayGroupLookupCode 	Varchar(50)
					)


	Declare
	@DistinctTitleFromAPinterface TABLE (
				Id			Int	Identity,
				TitleCode		Varchar(10)
					)
	Declare
	@DistinctPayGroupFromAPinterface TABLE (
				Id			Int	Identity,
				PayGroupLookupCode	Varchar(50)
				)
	
	Declare
	@TotalFromApInterfaceByCurrency TABLE (
				Id			Int	Identity,
				TotalPayment		Numeric(14,2),
				Currency		Varchar(10),
				ExcessAmount		Numeric(14,2)
				
				
					)
	Declare
	@TotalFromApInterfaceByPayGroup TABLE (
				Id		Int	Identity,
				TotalPayment	Numeric(14,2),
				PayGroupLookupCode  Varchar(50),
				ExcessAmount	Numeric(14,2)
					)


	Declare 
	@MissingTitle TABLE(
				Id		Int	Identity,
				TitleCode	Varchar(10),
				MissingFrom	Varchar(30)
			        )

	Declare 
	@MissingPaygroup TABLE(
				Id			Int	Identity,
				PayGroupLookupCode 	Varchar(50),
				MissingFrom		Varchar(30)
			        )


	Declare
	@AllException TABLE (
				Id			Int	Identity,
				FromBatchId		Int,
				ToBatchId		Int,
				TitleCode		Varchar(20),
				PayGroupLookupCode	Varchar(50),
				Description		Varchar(250)
			         )	

	--Insert All remit items in temp table
	Insert into @AllItem
	Select    Product.Fulfill_House_Nbr,
		 Product.Pub_Nbr,
		 codrh.TitleCode,
		 crh.State ,
		 Case codrh.CurrencyID 
			When 801 Then 'CAD' 
			When 802 Then 'USD'
			Else   'UNKNON' 
		end as CurrencyID,  
 	 	product.paygrouplookupcode,
	 	Sum(codrh.BasePrice)  as TotalBasePrice, 
	 	Round(codrh.RemitRate * Sum(codrh.BasePrice),6) as TotalRemit, 
	 	Round(Sum(IsNull(codrh.Tax,0)),6) Tax1,
	 	Round(Sum(IsNull(codrh.Tax2,0)),6) Tax2
	From     QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrh,
		QSPCanadaOrderManagement..RemitBatch rb,
	 	QSPCanadaOrderManagement..CustomerOrderHeader coh,
	 	QSPCanadaOrderManagement..CustomerOrderDetail cod,
 	 	QSPCanadaOrderManagement..CustomerRemitHistory crh,
	 	QSPCanadaProduct..Pricing_Details pd,
	 	QSPCanadaProduct..Product Product,
	 	QSPCanadaProduct..Fulfillment_House fh,
	 	QSPCanadaProduct..Publishers pub
	Where codrh.RemitBatchID = rb.id
	 And cod.customerorderheaderinstance = codrh.customerorderheaderinstance
	 And cod.transid = codrh.transid
	 And coh.Instance = cod.CustomerOrderHeaderInstance
	 And codrh.CustomerRemitHistoryInstance = crh.Instance
	 And cod.PricingDetailsID = pd.MagPrice_Instance
	 And pd.Pricing_Year = Product.Product_Year
	 And pd.Pricing_Season = Product.Product_Season
	 And codrh.TitleCode = Product.Product_Code 
	 And Product.Fulfill_House_Nbr = fh.Ful_Nbr   
	 And Product.Pub_Nbr = pub.Pub_Nbr
	 And rb.RunID between IsNull(@FromBatchID, rb.RunID) and IsNull(@ToBatchID, rb.RunID) 
	 And CODRH.STATUS IN (42000, 42001) 	--needs to be sent, sent
	Group by Product.Fulfill_House_Nbr,Product.Pub_Nbr, crh.State,codrh.CurrencyID,codrh.TitleCode, codrh.BasePrice,codrh.RemitRate,product.paygrouplookupcode  

	
	--Sum up prices, base prices and Tax and Remit amount 
	Insert into @MagazineTotalByTitle
	 Select FulfHouseId,
		 PublisherId,
	 	TitleCode,	 	Currency,
	 	PayGroupLookupCode,
   	 	Round(Sum(TotalBasePrice),2) 	as TotalBasePrice,
	 	Round(Sum(TotalRemitAmount),2) 	as TotalRemit,
	 	IsNull((Select Round(Sum(IsNull(TotalTax1Amount,0)),2) From @AllItem Where TitleCode = a.TitleCode and Currency = a.Currency and Province  Not In ( 'NB','NS','NL')  ),0) 	as GST,
	 	IsNull((Select Round(Sum(IsNull(TotalTax1Amount,0)),2) From @AllItem Where TitleCode = a.TitleCode and Currency = a.Currency and Province  In ( 'NB','NS','NL')  ),0) 	as HST,
	 	IsNull(Round(Sum(TotalTax2Amount),2),0) 																as PST, 

		 IsNull((Select Round(Sum(IsNull(TotalTax1Amount,0)),2) From @AllItem Where TitleCode = a.TitleCode and Currency = a.Currency and Province  Not In ( 'NB','NS','NL')  ),0) +
		 IsNull((Select Round(Sum(IsNull(TotalTax1Amount,0)),2) From @AllItem Where TitleCode = a.TitleCode and Currency = a.Currency and Province  In ( 'NB','NS','NL')  ),0) 	+
		 IsNull(round(sum(TotalTax2Amount),2),0) as Tax,

           		 IsNull( Round(Sum(TotalRemitAmount),2),0) +
 	 	 IsNull((Select Round(Sum(IsNull(TotalTax1Amount,0)),2) From @AllItem Where TitleCode = a.TitleCode and Currency = a.Currency and Province  Not In ( 'NB','NS','NL')  ),0) +
		 IsNull((Select Round(Sum(IsNull(TotalTax1Amount,0)),2) From @AllItem Where TitleCode = a.TitleCode and Currency = a.Currency and Province  In ( 'NB','NS','NL')  ) ,0)	+
		 IsNull(Round(Sum(TotalTax2Amount),2),0) as TotalPayment
	 From     @AllItem a
	 Group by FulfHouseId, PublisherId,TitleCode,Currency , PayGroupLookupCode 


	--Get distinct magazine title from Remit data
	Insert into @DistinctTitleFromRemit
	Select Distinct TitleCode From @MagazineTotalByTitle


	--Total Amount by CAD and USD from remit history 
	Insert into @TotalAPbyCurrency
	Select Sum(TotalPayment),Currency,0 	--Excess Amount
	From @MagazineTotalByTitle Group By Currency 

	--Total Amount by Paygroup lookup code  
	Insert into @TotalAPbyPayGroup
	Select Sum(TotalPayment),PayGroupLookupCode,0 	--Excess Amount
	From @MagazineTotalByTitle Group By PayGroupLookupCode


	/****************************************************** Interface Data extraction ***********************************************/	
	--Get totals from Ap interface table
	Insert into @TotalFromApInterface
	Select  Substring(invoice_num,1,4), Sum(invoice_amount),invoice_currency_code,pay_group_lookup_code 
	--From qspOracleinterface..om_tbl_ap_invoices_interface
	From [QSPOracleInterface].[dbo].[OM_TBL_AP_INVOICES_INTF_BKUP]
	 Where pay_group_lookup_code Not In ( 'CA QSP CUSTOMER CAD REFUN', 'CA QSP GROUP CAD REFUNDS')  -- Exclude CS refund and Group refunds
	And Cast(Substring(INVOICE_NUM,6,4 )as Int )=  @FromBatchID
	Group By  Substring(invoice_num,1,4),pay_group_lookup_code,invoice_currency_code 
	Order by 1
	
	/*
	--If more than one remit batch is entered get the remaining data from Oracleinterface backup table an dinsert
	If @FromBatchID <> @ToBatchID
	Begin
		Insert into @TotalFromApInterface
	 	Select  Substring(invoice_num,1,4), sum(invoice_amount),invoice_currency_code,pay_group_lookup_code 
		From [QSPOracleInterface].[dbo].[OM_TBL_AP_INVOICES_INTF_BKUP]
		Where pay_group_lookup_code Not In ( 'CA QSP CUSTOMER CAD REFUN', 'CA QSP GROUP CAD REFUNDS')  -- Exclude CS refund and Group refunds
		And Cast(Substring(INVOICE_NUM,6,4) As Int )  > @FromBatchID 
		And (Cast(Substring(INVOICE_NUM,6,4) As Int) <=@ToBatchID)
		Group By  Substring(invoice_num,1,4),pay_group_lookup_code,invoice_currency_code 
		Order by 1

	End
	*/


	--Get distinct magazine title from AP data
	Insert into @DistinctTitleFromAPinterface
	Select Distinct TitleCode From @TotalFromApInterface

	--Get Totals by Currency code from all interface data
	Insert into @TotalFromApInterfaceByCurrency
	Select Sum(TotalPayment),Currency,0 	--Excess Amount
	From @TotalFromApInterface Group By Currency


	--Get Totals by PayGroup Lookup Code  from all interface data
	Insert into @TotalFromApInterfaceByPayGroup
	Select Sum(TotalPayment),PayGroupLookupCode,0 	--Excess Amount
	From @TotalFromApInterface Group By PayGroupLookupCode


	-- Check all titles which exists in Remit data also exist in  AP data
	Select @TitleCount = Count(*)  From @DistinctTitleFromRemit

	Select @Cnt = Count(*) From @DistinctTitleFromAPinterface

	If IsNull(@TitleCount,0 ) <> IsNull(@Cnt,0 ) 
	Begin
		Set @MissingTable = ''
		If IsNull(@TitleCount,0 )  > IsNull(@Cnt,0 ) 
		Begin
			Set @MissingTable = 'AP Interface'
			Insert into @MissingTitle
			Select  TitleCode, @MissingTable From @DistinctTitleFromRemit Where TitleCode Not in (Select TitleCode From @DistinctTitleFromAPinterface)
		End
		Else
		Begin
			Set @MissingTable = 'Remit History'
			Insert into @MissingTitle 
			Select  TitleCode, @MissingTable From @DistinctTitleFromAPinterface Where TitleCode Not in (Select TitleCode From @DistinctTitleFromRemit)
		End

		Select @MaxId=Max(id), @Cnt = Count(*) From @MissingTitle
		Set @RecCounter = 0
		Set @MissingTable=''
		While  	@Cnt  > 0
		Begin
			Select @TitleCode = TitleCode, @MissingTable=MissingFrom From @MissingTitle Where Id = @MaxId - @RecCounter
			 Insert into @AllException Values (@FromBatchID,@ToBatchId,@TitleCode,Null,'No matching Title found in '+@MissingTable +' for '+@TitleCode)

		Set @RecCounter = @RecCounter + 1
		Set @Cnt = @Cnt -1
		End	

	End
	--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	--For each currency total from remit history compare  with AP interface totals
	Select @CurCnt = Count(*) , @MaxId = Max(Id)  From @TotalAPbyCurrency  
	Set  @Cnt = 0
	While  @CurCnt > 0
	Begin
		--Total from remit 
		Select @Currency = Currency, @TotalAp = TotalPayment  From @TotalAPbyCurrency Where id = @MaxId - @Cnt
		
		--total from interface
		Select @TotalFromInterface = TotalPayment From @TotalFromApInterfaceByCurrency Where Currency = @Currency

		If IsNull(@TotalFromInterface,0) <> IsNull(@TotalAp,0)
		Begin

			If IsNull(@TotalFromInterface,0) > IsNull(@TotalAp,0)
			Begin

 
				--Total From Interface is greater than Remit Totals take amount from AP interface and check against remit totals for each magazine
				-- so that we magazine total with discrepancy can be found
				Update @TotalFromApInterfaceByCurrency Set  ExcessAmount= ( IsNull(@TotalFromInterface,0) - IsNull(@TotalAp,0))
				Where  Currency = @Currency


				--Since interface amount is greater than remit, for each magazine take total from interface and compare it with remit total and insert the exception with error detail
				Select  @TitleCount=Count(*), @MaxRecNo = Max(Id) from @TotalFromApInterface

				Set @RecCounter = 0
				Set @TotalAmountDiscrepancyUSD = 0
				Set @TotalAmountDiscrepancyCAD = 0
				While @TitleCount > 0
				Begin
					Select  @APTitleCode=TitleCode, @TotalPaymentAP = TotalPayment , @CurrencyCodeAP = Currency 
					from @TotalFromApInterface Where id = (@MaxRecNo - @RecCounter)

					--Get the title total amount from remit data using the title from AP
					Select @RemitTitle= TitleCode , @CurrencyCodeRemit = Currency	, @TotalPaymentRemit =TotalPayment
					From @MagazineTotalByTitle Where Currency = @CurrencyCodeAP And TitleCode = @APTitleCode

					-- If Exists
					If @@Rowcount > 0 
					Begin
						
						If @TotalPaymentAP <> @TotalPaymentRemit
						Begin
							If IsNull(@TotalPaymentAP,0) > IsNull(@TotalPaymentRemit,0)
							Begin
							  Insert into @AllException values (@FromBatchID,@ToBatchId,@APTitleCode,Null,
											'Payment ('+@CurrencyCodeAP+ ') excess amount ' +cast(Abs(@TotalPaymentAP - @TotalPaymentRemit) as varchar) +' for title '+@APTitleCode+' in AP interface'
											 )
							End
							Else
							Begin
							  Insert into @AllException values (@FromBatchID,@ToBatchId,@APTitleCode,Null,
											'Payment ('+@CurrencyCodeAP+ ') excess amount ' +cast(Abs(@TotalPaymentAP - @TotalPaymentRemit) as varchar) +' for title '+@APTitleCode+' in remit history'
											 )
							End

							If @CurrencyCodeAP = 'CAD'
							Begin
								Set @TotalAmountDiscrepancyCAD = @TotalAmountDiscrepancyCAD +Abs(@TotalPaymentAP - @TotalPaymentRemit)
							End
							If @CurrencyCodeAP = 'USD'
							Begin
								Set @TotalAmountDiscrepancyUSD = @TotalAmountDiscrepancyUSD +Abs(@TotalPaymentAP - @TotalPaymentRemit)
							End
						End 
						
					End
					
				Set @RecCounter = @RecCounter+1
				Set @TitleCount =@TitleCount -1
				End


			End
			----------------------------------- Total From Remit data by Currency is greater than AP total by Currency  ------------------------
			Else
			Begin
				--Total From Remit  is greater than Interface Totals
				Update @TotalAPbyCurrency Set  ExcessAmount= ( IsNull(@TotalFromInterface,0) - IsNull(@TotalAp,0))
				Where  Currency = @Currency

				--Check total of each title and compare it with AP and insert the exception with error detail
				Select  @TitleCount=Count(*), @MaxRecNo = Max(Id) from @MagazineTotalByTitle
				Set @RecCounter = 0
				Set @TotalAmountDiscrepancyUSD = 0
				Set @TotalAmountDiscrepancyCAD = 0
				While @TitleCount > 0
				Begin
					Select  @RemitTitle=TitleCode, @TotalPaymentRemit = TotalPayment , @CurrencyCodeRemit = Currency 
					From @MagazineTotalByTitle Where id = (@MaxRecNo - @RecCounter)

					--Get the title total amount from AP interface data using the title from remit
					Select @APTitleCode= TitleCode	 , @CurrencyCodeAP   = Currency, @TotalPaymentAP =TotalPayment
					From @TotalFromApInterface Where Currency = @CurrencyCodeRemit And TitleCode = @RemitTitle

					If @@Rowcount > 0 
					Begin
						
						If @TotalPaymentAP <> @TotalPaymentRemit
						Begin
							If IsNull(@TotalPaymentAP,0) > IsNull(@TotalPaymentRemit,0)
							Begin
							  Insert into @AllException values (@FromBatchID,@ToBatchId,@RemitTitle,Null,
											'Payment ('+@CurrencyCodeRemit+ ') excess amount ' +cast(Abs(@TotalPaymentAP - @TotalPaymentRemit) as varchar) +' for title '+@RemitTitle+' in AP interface'
											 )
							End
							Else
							Begin
							  Insert into @AllException values (@FromBatchID,@ToBatchId,@RemitTitle,Null,
											'Payment ('+@CurrencyCodeAP+ ') excess amount ' +cast(Abs(@TotalPaymentAP - @TotalPaymentRemit) as varchar) +' for title '+@RemitTitle+' in remit history'
											 )
							End

							If @CurrencyCodeRemit = 'CAD'
							Begin
								Set @TotalAmountDiscrepancyCAD = @TotalAmountDiscrepancyCAD +Abs(@TotalPaymentAP - @TotalPaymentRemit)
							End
							If @CurrencyCodeRemit = 'USD'
							Begin
								Set @TotalAmountDiscrepancyUSD = @TotalAmountDiscrepancyUSD +Abs(@TotalPaymentAP - @TotalPaymentRemit)
							End
						End 
						
					End
					
				Set @RecCounter = @RecCounter+1
				Set @TitleCount =@TitleCount -1
				End

			End
			
		End
		
	Set @Cnt = @Cnt +1
	Set @CurCnt = @CurCnt -1
	End --For Each currency total in remit

	/************************************************Paygroup check********************************************************************************************/

		--Ensure all paygroup from remit exists in AP interface
		Set @TitleCount = 0
		Set @Cnt =0 

		Insert into @DistinctPayGroupFromRemit
		Select Distinct  PayGroupLookupCode from @MagazineTotalByTitle


		Insert into @DistinctPayGroupFromAPinterface
		Select Distinct  PayGroupLookupCode from @TotalFromApInterface


		-- Compare paygroup count from remit and AP
		Select @TitleCount = Count(*)  from @DistinctPayGroupFromRemit

		Select @Cnt = Count(*) from @DistinctPayGroupFromAPinterface

		If IsNull(@TitleCount,0 ) <> IsNull(@Cnt,0 ) 
		Begin
			Set @MissingTable = ''
			If IsNull(@TitleCount,0 )  > IsNull(@Cnt,0 ) 
			Begin
				Set @MissingTable = 'AP Interface'
				Insert into @MissingPaygroup
				Select  PayGroupLookupCode, @MissingTable From @DistinctPayGroupFromRemit Where PayGroupLookupCode Not in (Select PayGroupLookupCode From @DistinctPayGroupFromAPinterface)
			End
			Else
			Begin
				Set @MissingTable = 'Remit History'
				Insert into @MissingPaygroup 
				Select  PayGroupLookupCode, @MissingTable From @DistinctPayGroupFromAPinterface Where PayGroupLookupCode Not in (Select PayGroupLookupCode From @DistinctPayGroupFromRemit)
			End

			Select @MaxId=Max(id), @Cnt = Count(*) From @MissingPaygroup
			Set @RecCounter = 0
			Set @MissingTable=''
			While  	@Cnt  > 0
			Begin
				Select @PayGroup = PayGroupLookupCode, @MissingTable=MissingFrom From @MissingPaygroup Where Id = @MaxId - @RecCounter
				 Insert into @AllException values (@FromBatchID,@ToBatchId,Null,@PayGroup,'No matching Paygroup found in '+@MissingTable +' for '+@PayGroup)

			Set @RecCounter = @RecCounter + 1
			Set @Cnt = @Cnt -1
			End	
		End

	--If AP totals (USD and CAD)are matching with remit, ensure they match by paygroup as well  
	If (Isnull(@TotalAmountDiscrepancyCAD,0) =  0 And Isnull(@TotalAmountDiscrepancyUSD,0) =0)
	--If (IsNull(@TotalAmountDiscrepancyCAD,0) <> 0 And IsNull(@TotalAmountDiscrepancyUSD,0) <>0)
	Begin
		
		--If Paygroup totals from remit is greater than total from interface
		If (Select Sum(TotalPayment) from @TotalAPbyPayGroup) > (Select Sum(TotalPayment) from @TotalFromApInterfaceByPayGroup) 
		Begin

		Select @CurCnt = Count(*) , @MaxId = Max(Id)  From @TotalAPbyPayGroup  
		Set  @Cnt = 0
		Set @TotalAp=0
		Set @TotalFromInterface=0
		--Loop through each paygroup taking paygroup from remit
		While  @CurCnt > 0
		Begin
			Set @TotalFromInterface = 0
			Set @TotalAp = 0
			--Total from remit by paygroup
			Select @PayGroupAP = PayGroupLookupCode, @TotalAp = TotalPayment  From @TotalAPbyPayGroup Where id = @MaxId - @Cnt
		
			--total from interface by paygroup
			Select @TotalFromInterface = TotalPayment From @TotalFromApInterfaceByPayGroup Where PayGroupLookupCode = @PayGroupAP
			
			--If matching paygroup found in interface data and amount is not equal to paygroup total from remit
			If @@RowCount > 0 And (IsNull(@TotalFromInterface,0) <> IsNull(@TotalAp,0))
			Begin
				If IsNull(@TotalFromInterface,0) > IsNull(@TotalAp,0)
				Begin
					--Total From Interface is greater than Remit 
					Update @TotalFromApInterfaceByPayGroup set  ExcessAmount= ( IsNull(@TotalFromInterface,0) - IsNull(@TotalAp,0))
					Where  PayGroupLookupCode = @PayGroupAP

					 Insert into @AllException Values (@FromBatchID,@ToBatchId,Null,@PayGroupAP,
											'Payment  amount excess in AP by ' +Cast(Abs(@TotalFromInterface - @TotalAp) As Varchar) +' for PayGroupLookupCode '+@PayGroupAP
											 )
				End
				Else
				Begin
					--Total from remit is greater
					Update @TotalApByPayGroup Set  ExcessAmount= ( IsNull(@TotalFromInterface,0) - IsNull(@TotalAp,0))
					Where  PayGroupLookupCode = @PayGroupAP

					 Insert into @AllException Values (@FromBatchID,@ToBatchId,Null,@PayGroupAP,
											'Payment  amount excess in remit by ' +Cast(Abs(@TotalFromInterface - @TotalAp) As Varchar) +' for PayGroupLookupCode '+@PayGroupAP
											 )

				End
			End
	
		Set @Cnt = @Cnt +1
		Set @CurCnt = @CurCnt -1
		End --For Each paygroup total in remit	
		End
		--------------------------------------   If Paygroup totals from remit is less than total from interface   ---------------- 
		If (Select Sum(TotalPayment) From @TotalAPbyPayGroup) < (Select Sum(TotalPayment) From @TotalFromApInterfaceByPayGroup) 
		Begin

		Select @CurCnt = Count(*) , @MaxId = Max(Id)  From @TotalFromApInterfaceByPayGroup
		Set  @Cnt = 0
		Set @TotalAp=0
		Set @TotalFromInterface=0
		While  @CurCnt > 0
		Begin
			Set @TotalFromInterface = 0
			Set @TotalAp = 0
			--Total from interface by paygroup
			Select @PayGroupAP = PayGroupLookupCode, @TotalFromInterface = TotalPayment  From @TotalFromApInterfaceByPayGroup Where id = @MaxId - @Cnt

			--total from remit by paygroup
			Select @TotalAp = TotalPayment From @TotalAPbyPayGroup   Where PayGroupLookupCode = @PayGroupAP

			If @@RowCount > 0 And (IsNull(@TotalFromInterface,0) <> IsNull(@TotalAp,0))
			Begin
				If IsNull(@TotalFromInterface,0) > IsNull(@TotalAp,0)
				Begin
					--Total From Interface is greater than Remit 
					Update @TotalFromApInterfaceByPayGroup Set  ExcessAmount= ( IsNull(@TotalFromInterface,0) - IsNull(@TotalAp,0))
					Where  PayGroupLookupCode = @PayGroupAP

					 Insert into @AllException Values (@FromBatchID,@ToBatchId,Null,@PayGroupAP,
											'Payment  amount excess in AP  by ' +Cast(Abs(@TotalFromInterface - @TotalAp) As Varchar) +' for PayGroupLookupCode '+@PayGroupAP
											 )
				End
				Else
				Begin
					--Total from remit is greater
					Update @TotalApByPayGroup Set  ExcessAmount= ( IsNull(@TotalFromInterface,0) - IsNull(@TotalAp,0))
					Where  PayGroupLookupCode = @PayGroupAP

					 Insert into @AllException Values (@FromBatchID,@ToBatchId,Null,@PayGroupAP,
											'Payment  amount excess in remit by ' +Cast(Abs(@TotalFromInterface - @TotalAp) As Varchar) +' for PayGroupLookupCode '+@PayGroupAP
											 )

				End
			End
	
		Set @Cnt = @Cnt +1
		Set @CurCnt = @CurCnt -1
		End --For Each paygroup total in remit	
		End
	End	

	Set @Cnt = 0
	Select @Cnt = Count(*) from @AllException
	If IsNull(@Cnt,0)=0
	Begin
		Set @RetValAP = 0
		Set @ErrorMessageAP = 'No problem found in AP data comparision with remit'
	End
	Else
	Begin
		Set @RetValAP = 1
		Set @ErrorMessageAP = 'Problem found in AP data comparision with remit'
		Select * From @AllException
	End
GO
