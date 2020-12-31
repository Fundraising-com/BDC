USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spRemitIntegrityVerification]    Script Date: 06/07/2017 09:20:56 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[spRemitIntegrityVerification] @OrderId Int, @VerifyRetVal Int Output ,@ReturnMessage Varchar(200) Output
As




Declare @RemitBatchDate		DateTime,
	@PricingYear			Int,
	@PricingSeason	 		Varchar(2),
	@RemitDistinctTitleCount	Int,
	@CodDistinctTitleCount		Int,
	@TitleCode			Varchar(10),
	@TitleCodeToCompare		Varchar(10),
	@TitleName			Varchar(50),
	@Cnt				Int,
	@CustomerLastName		Varchar(50),
	@CustomerFirstName		Varchar(50),
	@Coh				Int,
	@TransId			Int,
	@RemitBatchId			Int,
	@RemitBatchIdToCompare 	Int,
	@MaxRowCounter		Int,
	@Increment			Int,
	@NumberofIssues		Int,
	@EffortKey			Varchar(10) ,
	@EffortKeytoCompare		Varchar(10) ,
	@BasePrice			Numeric(10,2),
	@RemitRate  			Numeric(10,2),
	@BasePriceToCompare		Numeric(10,2),
	@RemitRateToCompare		Numeric(10,2),
	@NumberofIssuesToCompare	Int,
	@FulfillmentHouseNumber	Int
	
	
	-- All Item sent to remit
	Declare
	@AllRemitItem TABLE (
			Id				Int	Identity,
			OrderId				Int,
			OrderQualifier			Int,
			DetailProductCode		Varchar(20),
			DetailProductName 		Varchar(50),
			DetailQuantity			Int, 
			PaymentMethod			Int,
			Coh				Int,
			DetailTransId			Int,	
			CCPayStatus			Int,
			TotalCCAmount			Numeric(10,2), 
			CCAuthorization			Varchar(20),
			CPHInstance			Int,
			IsCreditCard			Int,
			CPHStatus			Int,
			RemitTitleCode			Varchar(20),
			RemitMagazineTitle		Varchar(50),
			ProductStatus			Int,
			RemitNumberofIssues		Int,
			PricindDetailNoOfIssues		Int,
			RemitEffortKey			Varchar(10),
			PricingDetailEffortKey		Varchar(10),
			PricingYear			Int,
			PricingSeason			Varchar(2),
			HasTaxRegNumber		Varchar(1),
			RemitBatchId			Int,
			RunID				Int, 
			RemitBatchDate			DateTime,
			RemitStatus			Int, 
			FulfillmentHouseNumber		Int, 
			FulfillmentHouseName		Varchar(50), 
			PublisherNumber		Int, 
			PublisherName			Varchar(50), 
			BasePrice			Numeric(10,2), 
			RemitRate			Numeric(10,2), 
			RemitAmount			Numeric(10,2),
			RemitTax1			Numeric(10,2), 
			RemitTax2			Numeric(10,2), 
			RemitCurrency			Varchar(10),
			--RemitState			Varchar(10),
			--RemitZip			Varchar(10),
			CustomerLastName		Varchar(50), 
			CustomerFirstName		Varchar(50), 
			CustomerAddress1		Varchar(50), 
			CustomerCity			Varchar(50), 
			CustomerState			Varchar(30), 
			CustomerZip			Varchar(10)
			
			)

		Declare
		@DuplicateCustWithSameMag TABLE (
						Id			Int	Identity,
						Title			Varchar(10),
						CustomerLastName	Varchar(50), 
						CustomerFirstName	Varchar(50), 
						OrderQualifier		Int,
						Cnt			Int
				       		  )	

		Declare
		@CCPaidItemWithoutPaymentHeader TABLE (
						Id		Int	Identity,
						Title		Varchar(10),
						coh		Int, 
						TransId		Int, 
						TitleDes 	Varchar(50)
						
				       		  )	

		Declare
		@TitleWithTaxReg TABLE (
					Id		Int	Identity,
					Title		Varchar(10)
				         )	

		Declare
		@DistinctRemitTitle TABLE (
					Id		Int	Identity,
					Title		Varchar(10),
					TitleDes 	Varchar(50),
					RemitBatchId	Int,
					EffortKey	Varchar(50)
				         )	
		Declare
		@DistinctCODTitle TABLE (
					Id		Int	Identity,
					Title		Varchar(10),
					TitleDes 	Varchar(50),
					BasePrice	Numeric(10,2),
					Issues		Int,
					RemitRate	Numeric(10,2)
				         )	


		Declare
		@TitleWithMultiRemitBatchId TABLE (
					Id		Int	Identity,
					Title		Varchar(10),
					RemitBatchId	Int
				         )	
		Declare
		@TitleWithMultiEffortKey TABLE (
					Id		Int	Identity,
					Title		Varchar(10),
					Issues		Int,
					EffortKey	Varchar(20)
				         )	

		Declare
		@TitleWithMultiBasePriceOrRemitRate TABLE (
					Id		Int	Identity,
					Title		Varchar(10),
					Issues		Int,
					BasePrice	Numeric(10,2),
					RemitRate	Numeric(10,2)
				         )	

		
		Declare
		@AllException TABLE (
					Id				Int	Identity,
					OrderId				Int,
					CustomerOrderHeaderInstance	Int,
					TransId				Int,
					ProductCode			Varchar(20),
					ProductName			Varchar(50),
					Description			Varchar(250)
				         )	

Insert into @AllRemitItem
select	b.orderid,
	b.OrderQualifierId,
	cod.productCode,
	cod.productName,
	cod.Quantity,
	coh.paymentmethodinstance,
	coh.Instance,
	cod.TransId,
	ccp.statusinstance,
	cph.TotalAmount,
	ccp.AuthorizationCode,
	cph.Instance,
	cph.IsCreditCard,
	cph.statusinstance,
	codrh.TitleCode,
	codrh.MagazineTitle,
	Prod.Status,
	codrh.NumberOfIssues,
	pd.nbr_of_issues,
	codrh.EffortKey,
	pd.Effort_Key,
	pd.Pricing_Year,
	pd.pricing_Season,
	'N',		--TaxRegistrationNumber
	codrh.RemitBatchId,
	rb.RunID,
	CONVERT(varchar(10), rb.Date, 101) AS RemitBatchDate,
	CODRH.STATUS,
	Prod.Fulfill_House_Nbr,
	fh.Ful_Name, 
	Prod.Pub_Nbr,
	pub.Pub_Name,
	codrh.BasePrice,
	codrh.RemitRate,
	Round(codrh.RemitRate * codrh.BasePrice,2) as RemitAmount, 
	isnull(codrh.Tax,0) Tax1,
	isnull(codrh.Tax2,0)Tax2,
	Case codrh.CurrencyID when 801 then 'CAD' when 802 then 'USD' Else 'UNKNOWN' end as CurrencyID,  
	--crh.State,
	--crh.Zip,
	crh.LastName,
	crh.firstname,
	crh.Address1,
	crh.city,
	crh.state,
	crh.zip
From     QSPCanadaOrderManagement..Batch b,
	QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrh,
	QSPCanadaOrderManagement..RemitBatch rb,
	--QSPCanadaOrderManagement..Customer cust,
	QSPCanadaOrderManagement..CustomerOrderHeader coh,
	QSPCanadaOrderManagement..CustomerOrderDetail cod,
 	QSPCanadaOrderManagement..CustomerRemitHistory crh,
	QSPCanadaProduct..Pricing_Details pd,
	QSPCanadaProduct..Product Prod,
	QSPCanadaProduct..Fulfillment_House fh,
	QSPCanadaProduct..Publishers pub,
	QSPCanadaOrderManagement..CustomerPaymentHeader cph
	 left outer join QSPCanadaOrderManagement..CreditCardPayment ccp on 
	 cph.Instance=ccp.CustomerPaymentHeaderInstance
Where   b.id=coh.orderbatchid
	And b.date=coh.orderbatchdate
	And codrh.RemitBatchID = rb.id
	AND cod.customerorderheaderinstance = codrh.customerorderheaderinstance
	AND cod.transid = codrh.transid
	AND coh.Instance = cod.CustomerOrderHeaderInstance
	AND coh.instance=cph.CustomerOrderHeaderInstance
	--AND coh.CustomerBilltoInstance=Cust.Instance
	AND codrh.CustomerRemitHistoryInstance = crh.Instance
	AND cod.PricingDetailsID = pd.MagPrice_Instance
	AND pd.Pricing_Year = Prod.Product_Year
	AND pd.Pricing_Season = Prod.Product_Season
	AND codrh.TitleCode = Prod.Product_Code 
	AND Prod.Fulfill_House_Nbr = fh.Ful_Nbr   
	AND Prod.Pub_Nbr = pub.Pub_Nbr
	AND Prod.Pub_Nbr = Prod.Pub_Nbr
	AND Prod.Fulfill_House_Nbr = Prod.Fulfill_House_Nbr
	AND codrh.CurrencyID = codrh.CurrencyID
	AND CODRH.STATUS IN (42000, 42001,42010) --needs to be sent, sent and no longer offred
	and orderid= @OrderId 



	-- Get pricing year and pricing season
	Select Top 1 @RemitBatchDate = RemitBatchDate
	From @AllRemitItem

	Select  @PricingYear = FiscalYear  , @PricingSeason = Season
	from QSPCanadaCommon..season 
	where @RemitBatchDate between startDate and EndDate
	And IsNUll(Season,'Y') <> 'Y'

	If @@Rowcount <>1 
	Begin
		If @@Rowcount =0
		Begin
			Insert into @AllException values  (@OrderId,Null,Null,Null,Null,'Cannot Find pricing year and season info')
		End
		Else
			Insert into @AllException values  (@OrderId,Null,Null,Null,Null,'Multiple pricing year and season found')
		Return 
	End 



	-- Distinct Title Code from Remit data
	Insert into @DistinctRemitTitle
	Select Distinct RemitTitleCode, RemitMagazineTitle,Null,Null From @AllRemitItem Where RemitStatus in (42001,42002)

	-- Distinct Title Code from COD data
	Insert into @DistinctCODTitle
	Select Distinct DetailProductCode, DetailProductName,Null,Null,Null 
	From @AllRemitItem Where RemitStatus in (42001,42002)


	--Check if the distinct title count same in remit and order detail
	Select @RemitDistinctTitleCount  = Count(*) From @DistinctRemitTitle

	Select @CodDistinctTitleCount  = Count(*) From @DistinctCODTitle

	
	If @RemitDistinctTitleCount <> @CodDistinctTitleCount
	Begin
		Select @TitleCode = Title , @TitleName =TitleDes  From @DistinctRemitTitle Where Title Not in (Select  Title From @DistinctCODTitle)
		If @@Rowcount > 0
		Begin
			Insert into @AllException values  (@OrderId,Null,Null,@TitleCode,@TitleName,'No matching title found in Orderdetail for title '+@TitleCode)
		End 

		Set @TitleCode = Null
		Set @TitleName = Null

		Select @TitleCode = Title , @TitleName =TitleDes  From @DistinctCODTitle Where Title Not in (Select  Title From @DistinctRemitTitle)
		If @@Rowcount > 0
		Begin
			Insert into @AllException values  (@OrderId,Null,Null,@TitleCode,@TitleName,'No matching title found in CODRemit for title '+@TitleCode)
		End 
		
	End

	--Get all titles with Tax registration and Update TaxReg Flag
	Insert into @TitleWithTaxReg
	Select Title_Code  From qspcanadacommon..TaxMagRegistration 
	Where TAx_id=1 
	And IsNull(Tax_Registration_Number,'') <> ''

	Set @TitleCode = Null
	Set @RemitDistinctTitleCount = 0

	Select @RemitDistinctTitleCount = Count(*) From @DistinctRemitTitle
	While @RemitDistinctTitleCount > 0
	Begin
		Select @TitleCode = Title From @DistinctRemitTitle Where Id=@RemitDistinctTitleCount

		Select @Cnt = 1  From @TitleWithTaxReg Where Title=@TitleCode
		If @@Rowcount > 0
		Begin
			Update @AllRemitItem Set HasTaxRegNumber = 'Y'  Where RemitTitleCode = @TitleCode
		End

	Set @RemitDistinctTitleCount = @RemitDistinctTitleCount -1
	End


	-- Findout all exception within data for the order
	Insert into @AllException
	Select 	@OrderId,
		Coh,
		DetailTransId,
		RemitTitleCode,
		RemitMagazineTitle,
		(Case	When RemitTitleCode = '9999'									Then 'Invalid title code'
			--When RemitTitleCode <> DetailProductCode							Then 'Magazine title code does not match with order detail'
			When IsNull(Ltrim(Rtrim(RemitMagazineTitle)),'@') <> IsNull(Ltrim(Rtrim(DetailProductName)),'@@' )	Then ' Blank title or magazine title does not match with order detail'
		
			--When IsNull(DetailQuantity,0)  = 0 			Then 'Zero number of issues in Order detail for corresponding remit number of issues' 
			--When  IsNull(RemitNumberofIssues,0)=0 			Then 'Zero number of issues in remit for magazine'  
			When (IsNull(DetailQuantity,-1) <> IsNull(RemitNumberofIssues,0))	Then 'Zero or mismatch number of Issues for magazine'

			When  (Isnull(ProductStatus,0) = 0 ) Then 'Magazine status not known'
			When  (Isnull(ProductStatus,0) <> 30600 ) Then 'Inactive magazine being remitted'

			When  (HasTaxRegNumber = 'Y' AND IsNull(RemitTax1,0) = 0)	Then 'Magazine with tax registration has zero tax'
			When  RemitCurrency = 'UNKNOWN' 				Then 'Invalid currency'

	          		When PaymentMethod not  in (50004,50003,50002) 			Then 'Invalid Payment Method' 
			When (PaymentMethod in (50004,50003) And 
			           (IsNull(CustomerFirstName,'')='' Or  IsNull(CustomerLastName,'')='' ))	Then 'Credit card owner name missing'

			When IsNull(CustomerAddress1,'') = ''  		Then 'Missing Street Address for customer'
	      		When Isnull(CustomerCity,'') = ''      		Then 'Missing City for Customer'
	      		When Isnull(CustomerState,'')= ''      		Then 'Missing province for customer'
              		When Isnull(CustomerZip,'')= ''        		Then 'Missing Postal Code for Customer'


		End) Error
	From @AllRemitItem
	Where 	RemitStatus in (42001,42002)   AND
		(	
			( RemitTitleCode = '9999'	OR  IsNull(RemitTitleCode,'') = '')	
	      		OR (PaymentMethod in (50004,50003) And  (IsNull(CustomerFirstName,'')='' Or  IsNull(CustomerLastName,'')='' ))	
			OR IsNull(CustomerAddress1,'') = ''  		
	      		OR Isnull(CustomerCity,'') = ''      		
	      		OR Isnull(CustomerState,'')= ''      		
              		OR Isnull(CustomerZip,'')= ''   
			OR ( 	(PricindDetailNoOfIssues <>  RemitNumberofIssues)  OR	--Number of Issues should match from COD, Remit and PricingDetail
				(RemitNumberofIssues    <>      DetailQuantity)	   OR
				(RemitNumberofIssues = 0)
			       )
			OR IsNull(PricingDetailEffortKey,'') <>  IsNull(RemitEffortKey,'')  	-- Effor key is exists should match from Remit and PricingDetail
			OR (PaymentMethod in (50004,50003) And (OrderQualifier <> 39014 )AND  (IsNull(CCPayStatus,19001) <> 19000 ) )	 -- If Credit Card must have good payment except CC courtesy	
			OR (IsNull(PricingSeason,'') <> @PricingSeason	OR  IsNull(PricingYear,0) <> @PricingYear    OR  	 (IsNull(PricingSeason,'') <> @PricingSeason AND  IsNull(PricingYear,0) <> @PricingYear ))
			OR ((RemitTitleCode <> DetailProductCode ) OR (IsNull(RemitTitleCode,'')=''	) OR  (IsNull(DetailProductCode,'')='') )  --COD magazine title code should be same as remit titlecode
			OR (Ltrim(Rtrim(RemitMagazineTitle)) <> Ltrim(Rtrim(DetailProductName)) OR IsNull(RemitMagazineTitle,'')='' OR IsNull(DetailProductName,'')='' )			--COD magazine title should be same as remit title
			OR ( (IsNull(DetailQuantity,0) <> IsNull(RemitNumberofIssues,0)) OR	 (IsNull(DetailQuantity,0)  = 0) OR  (IsNull(RemitNumberofIssues,0)=0)  )  -- COD # fo Issues same as Remit
			OR (IsNull(ProductStatus,0) = 0 OR IsNull(ProductStatus,0) <> 30600 )-- should be Active magazine
			OR (HasTaxRegNumber = 'Y' AND IsNull(RemitTax1,0) = 0)	    --Magazine with tax registration should have tax
			OR RemitCurrency = 'UNKNOWN'

		   )

	--Duplicate Customer with same title
	Insert into @DuplicateCustWithSameMag
	Select RemitTitleCode, CustomerLastName, CustomerFirstName, OrderQualifier,Count(*)
	From @AllRemitItem Where RemitStatus in (42001,42002)
	Group By CustomerLastName, CustomerFirstName, RemitTitleCode,OrderQualifier
	Having Count(*) > 1
	
	Set @RemitDistinctTitleCount =0
	Select @RemitDistinctTitleCount = Count(*) From @DuplicateCustWithSameMag
	While @RemitDistinctTitleCount>0
	Begin
		Select @TitleCode = Title , @CustomerLastName =CustomerLastName, @CustomerFirstName= CustomerFirstName 
		From @DuplicateCustWithSameMag Where Id= @RemitDistinctTitleCount

		Insert into @AllException values 
		 (@OrderId,Null,Null,@TitleCode,Null, 'Multiple record found for title '+@TitleCode +'for customer '+IsNull(@CustomerFirstName,'N/A')+' '+ IsNull(@CustomerLastName,'') )
	
	Set @RemitDistinctTitleCount = @RemitDistinctTitleCount -1
	End


	--Item paid by CC has no payment record in payment header
	Insert into @CCPaidItemWithoutPaymentHeader
	Select Distinct DetailProductCode,coh,DetailTransId,  DetailProductName
	From @AllRemitItem 
	Where RemitStatus in (42001,42002) 
	And IsCreditCard=1
	And CPHStatus =600	-- Good
	And Coh not in (Select Distinct CustomerorderHeaderInstance From QSPCanadaOrderManagement..CustomerorderDetailRemitHistory)
	And CPHInstance not in (Select CustomerPaymentHeaderInstance From QSPCanadaOrderManagement..CreditCardPayment)
	
	Set @RemitDistinctTitleCount =0
	Set @TitleCode =''
	Set @TitleName =''
	Select @RemitDistinctTitleCount = Count(*) From @CCPaidItemWithoutPaymentHeader
	While @RemitDistinctTitleCount>0
	Begin
		Select @TitleCode = Title , @Coh=coh  ,@TransId=TransId, @TitleName =TitleDes
		From @CCPaidItemWithoutPaymentHeader Where Id= @RemitDistinctTitleCount

		Insert into @AllException values 
		 (@OrderId,@Coh,@TransId,@TitleCode,@TitleName, 'CC paid Item found without payment header') 
	
	Set @RemitDistinctTitleCount = @RemitDistinctTitleCount -1
	End

	-- Titles with multiple batch ids
	Delete From @DistinctRemitTitle

	--Using table variable for storing the all titles 
	Insert into @DistinctRemitTitle
	Select RemitTitleCode, RemitMagazineTitle,RemitBatchID,Null 
	From @AllRemitItem Where RemitStatus in (42001,42002) 
	And  RemitTitleCode in (Select Distinct RemitTitleCode From  @AllRemitItem  Group by RemitTitleCode Having Count(*) > 1)

	-- Get distinct title with multiple batch id but store only titlecode, next step
	--Update the batch id by selecting one batchid from @DistinctRemitTitle which has more than one remit batch id for the title
	Insert into @TitleWithMultiRemitBatchId
	Select Distinct Title,Null From @DistinctRemitTitle 

	--Loop through and update remit batch id value for each distict title that has multiple value
	Select @RemitDistinctTitleCount = Count(*) From @TitleWithMultiRemitBatchId
	While @RemitDistinctTitleCount >0
	Begin
		Set @RemitBatchId=0
		Select @TitleCode=Title From  @TitleWithMultiRemitBatchId Where Id=@RemitDistinctTitleCount
		
		Select Top 1 @RemitBatchId= RemitBatchId From @DistinctRemitTitle Where Title = @TitleCode
		
		Update @TitleWithMultiRemitBatchId Set RemitBatchId = @RemitBatchId Where Title = @TitleCode

	Set @RemitDistinctTitleCount = @RemitDistinctTitleCount -1 
	End


	--Now all titles have been updated with remit batch id now check each record if it has a different remit batch id
	Select @RemitDistinctTitleCount = Count(*) From @TitleWithMultiRemitBatchId
	While @RemitDistinctTitleCount >0
	Begin
		Set @RemitBatchId = 0
		Select @TitleCode=Title,  @RemitBatchId = RemitBatchId From  @TitleWithMultiRemitBatchId Where Id=@RemitDistinctTitleCount
	

		--Now search among the titles with multiple remit batch id		
		Select @Cnt=Count(*), @MaxRowCounter=Max(Id)  From @DistinctRemitTitle
		Set @Increment = 0
		While @Cnt >0
		Begin

			Select @TitleCodeToCompare=Title , @TitleName=TitleDes, @RemitBatchIdToCompare=RemitBatchId From @DistinctRemitTitle Where id=(@MaxRowCounter-@Increment)

			If ( @TitleCodeToCompare = @TitleCode And @RemitBatchIdToCompare <> @RemitBatchId)
			Begin
				Insert into @AllException values 	 (@OrderId,Null,Null,@TitleCode,@TitleName, 'Multiple remit batch ids having different values (other than '+Cast( @RemitBatchId as Varchar) +')')
				break
			End
		Set @Increment = @Increment+1
		Set @Cnt=@Cnt-1
		End

	Set @RemitDistinctTitleCount=@RemitDistinctTitleCount-1
	End


	-- Titles with different effort keys having same title and same # of Issues
	Delete From @DistinctRemitTitle

	--Using table variable for storing the all titles 
	Insert into @DistinctRemitTitle
	Select RemitTitleCode, RemitMagazineTitle,RemitNumberofIssues,RemitEffortKey 
	From @AllRemitItem Where RemitStatus in (42001,42002) 
	And  RemitTitleCode in (Select Distinct RemitTitleCode From  @AllRemitItem Where IsNull(RemitEffortKey,'') <> ''  Group by RemitTitleCode Having Count(*) > 1)

	-- table variable used in remit batch check  is used for effort key check, column RemitbatchId now hold number of issues
	Insert into @TitleWithMultiEffortKey
	Select Distinct Title,RemitBatchId,EffortKey From @DistinctRemitTitle 

	--Now check if with same no of issues effort key is different
	Select @RemitDistinctTitleCount = Count(*) From @TitleWithMultiEffortKey
	While @RemitDistinctTitleCount >0
	Begin
		
		Select @TitleCode=Title,  @NumberOfIssues =  Issues   ,  @EffortKey = EffortKey From  @TitleWithMultiEffortKey Where Id=@RemitDistinctTitleCount

		--Now search among the titles with multiple remit batch id		
		Select @Cnt=Count(*), @MaxRowCounter=Max(Id)  From @DistinctRemitTitle
		Set @Increment = 0
		While @Cnt >0
		Begin

			Select @TitleCodeToCompare=Title , @TitleName=TitleDes, @RemitBatchIdToCompare=RemitBatchId, @EffortKeytoCompare = EffortKey
			From @DistinctRemitTitle Where id=(@MaxRowCounter-@Increment)

			-- If Title code in list of tiltle with multiple effort key matches with title being checked from distinct title list and # of issues are also same 
			-- check effort key if not same record exception.( Variable @r@RemitBatchIdToCompareemit  containnumber of issues here)
			If (@TitleCodeToCompare = @TitleCode And   @RemitBatchIdToCompare =  @NumberOfIssues And @EffortKeytoCompare <> @EffortKey)
			Begin
				Insert into @AllException values 	 (@OrderId,Null,Null,@TitleCode,@TitleName, 'Multiple effort keys  having different values (other than '+Cast( @EffortKey as Varchar) +')')
				break
			End
		Set @Increment = @Increment+1
		Set @Cnt=@Cnt-1
		End

	Set @RemitDistinctTitleCount=@RemitDistinctTitleCount-1
	End

	
	--Check Duplicate base price or remit rate for magazine with multiple number of issues
	-- use table @DistinctCODTitle to store all titles. It was  used to store distinct titleas from COD so delete everything
	Delete From @DistinctCODTitle

	--Using table variable for storing the all titles 
	Insert into @DistinctCODTitle
	Select RemitTitleCode, RemitMagazineTitle,BasePrice,RemitNumberofIssues,RemitRate
	From @AllRemitItem Where RemitStatus in (42001,42002) 
	And  RemitTitleCode in (Select Distinct RemitTitleCode From  @AllRemitItem  Group by RemitTitleCode, RemitNumberofIssues  
				Having (Count(BasePrice) > 1 OR Count(RemitRate)> 1) )

	Insert into @TitleWithMultiBasePriceOrRemitRate
	Select Distinct Title,Issues,BasePrice,RemitRate From @DistinctCODTitle 

	--Now check if with same no of issues title has different base price or remit rate
	Select @RemitDistinctTitleCount = Count(*) From @TitleWithMultiBasePriceOrRemitRate
	While @RemitDistinctTitleCount >0
	Begin
		
		Select @TitleCode=Title,  @NumberOfIssues =  Issues   ,  @BasePrice = BasePrice,  @RemitRate=RemitRate From  @TitleWithMultiBasePriceOrRemitRate Where Id=@RemitDistinctTitleCount

		--Now search among the titles with multiple issues 	
		Select @Cnt=Count(*), @MaxRowCounter=Max(Id)  From @DistinctCODTitle
		Set @Increment = 0
		While @Cnt >0
		Begin

			Select @TitleCodeToCompare=Title , @TitleName=TitleDes, @NumberofIssuesToCompare=Issues, @BasePriceToCompare = BasePrice,  @RemitRateToCompare=RemitRate 
			From @DistinctCODTitle Where id=(@MaxRowCounter-@Increment)

			-- check title code in list of tiltles with multiple issues
			If ((@TitleCodeToCompare = @TitleCode And   @NumberofIssuesToCompare =  @NumberOfIssues) And 
				(@BasePriceToCompare <> @BasePrice Or @RemitRateToCompare <> @RemitRate ))
			Begin
				If (@BasePriceToCompare <> @BasePrice)
				Begin
					Insert into @AllException values 	 (@OrderId,Null,Null,@TitleCode,@TitleName, 'Multiple base price found other than ('+Cast(@BasePrice as varchar)+')')
					break
				End
				Else	Insert into @AllException values 	 (@OrderId,Null,Null,@TitleCode,@TitleName, 'Multiple remit rate found other than ('+Cast(@RemitRate as varchar)+')')
					
			End
		Set @Increment = @Increment+1
		Set @Cnt=@Cnt-1
		End

	Set @RemitDistinctTitleCount=@RemitDistinctTitleCount-1
	End

	-- Check fulfillment house that does not exists (CHAD)
	Delete From @DistinctCODTitle

	--Using table variable (earlier used for distinct remitTitle) for storing the all titles (BasePrice, Issues, and remitrate column now contain Remit Batch Id, Fulfillment house Number, 0)
	Insert into @DistinctCODTitle
	Select Distinct RemitTitleCode, RemitMagazineTitle,RemitBatchID,FulfillmentHouseNumber,0
	From @AllRemitItem Where RemitStatus not in (42001,42002) 
	
	-- Store Title code and fulfillment house number into table variable used earlier for  title with multiple RemitBatchId
	Delete from @TitleWithMultiRemitBatchId

	Insert into @TitleWithMultiRemitBatchId
	Select Title,Issues --Issues contain Fulf House Number	
	From @DistinctCODTitle t , QSPCanadaProduct..Product p  
	Where t.Title = p.Product_Code 
	And t.Issues <> p.Fulfill_House_Nbr ---- remitrate has fulfillment house number
	And Product_Season = @PricingSeason
	And Product_Year = @PricingYear

	--Now check if with same no of issues title has different base price or remit rate
	Select @Cnt=Count(*), @MaxRowCounter=Max(Id)  From @TitleWithMultiRemitBatchId
	Set @Increment = 0
	While @Cnt >0
	Begin
		--Get the title for which no fulfillment house was found 
		Select @TitleCode=Title 	From @TitleWithMultiRemitBatchId Where id=(@MaxRowCounter-@Increment)
		
		Set @FulfillmentHouseNumber = 0 
		Set @TitleName =''

		Select Top 1 @TitleName =TitleDes,  @FulfillmentHouseNumber= Issues  From @DistinctCODTitle Where Title = @TitleCode

		Insert into @AllException values 	 (@OrderId,Null,Null,@TitleCode,@TitleName, 'Magazine does not have fulfillemt house '+Cast ( @FulfillmentHouseNumber as Varchar)+' number in product database for current year ans season')
					
	Set @Increment = @Increment+1
	Set @Cnt=@Cnt-1
	End

	--Paid Item not remitted
	--In Table variable remitbatchid will contain coh and effort key (varchar) will contain TransId

	Delete From @DistinctRemitTitle

	Insert into @DistinctRemitTitle
	Select	cod.productCode,
		cod.productName,
		coh.Instance,
		cod.TransId
		--cod.StatusInstance,
		--ccp.statusInstance
	From   	QSPCanadaOrderManagement..Batch b,
		QSPCanadaOrderManagement..CustomerOrderHeader coh,
		QSPCanadaOrderManagement..CustomerOrderDetail cod,
		QSPCanadaOrderManagement..CustomerPaymentHeader cph
	 	Left outer join QSPCanadaOrderManagement..CreditCardPayment ccp on 
	 	cph.Instance=ccp.CustomerPaymentHeaderInstance
	Where 	b.id=coh.orderbatchid
	And 	b.date=coh.orderbatchdate
	And 	coh.Instance = cod.CustomerOrderHeaderInstance
	And 	coh.instance=cph.CustomerOrderHeaderInstance
	And 	orderid= @OrderId
	And 	cod.StatusInstance Not in (507)  --Not remitted
	And 	producttype in (46001)
	And 	( 	(coh.PaymentMethodInstance <> 50002 	And ccp.StatusInstance = 19000 )
			OR
			(coh.PaymentMethodInstance = 50002 	)
             		)
	And b.OrderQualifierId <> 39014  -- CCReprocess Courtesy


	--If record exists insert into exception
	Select @Cnt=Count(*), @MaxRowCounter=Max(Id)  From @DistinctRemitTitle
	Set @Increment = 0
	While @Cnt >0
	Begin
		--Get the title for which no fulfillment house was found 
		Select @TitleCode=Title , @TitleName =TitleDes, @coh=RemitBatchid , @TransId = Cast(EffortKey as Int) From @DistinctRemitTitle Where id=(@MaxRowCounter-@Increment)


		Insert into @AllException values 	 (@OrderId,@coh,@TransId,@TitleCode,@TitleName, 'Paid magazine not remitted')
					
	Set @Increment = @Increment+1
	Set @Cnt=@Cnt-1
	End


	--OutPut
	Set @Cnt = 0
	Select @Cnt = Count(*) from @AllException
	If IsNull(@Cnt,0)=0
	Begin
		Set @VerifyRetVal = 0
		Set @ReturnMessage = 'No problem found in remit data'
	End
	Else
	Begin
		Set @VerifyRetVal = 1
		Set @ReturnMessage = 'Problem found in remit data '
		Select * From @AllException
	End
GO
