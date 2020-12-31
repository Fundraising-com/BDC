USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spPreCloseVerification]    Script Date: 06/07/2017 09:20:55 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[spPreCloseVerification] @OrderId Int ,@RetVal  Int OutPut 
/********************************************************************************************************
MS Oct 02, 2006 Excluded Gift Sample and P Solver Qualifier from "No Magazine and Gift" check
*********************************************************************************************************/

As
	Declare @ItemCount	Int,
		@MajorProdLine	Int,
		@ProvinceCode	Varchar(10),
		@MaxId	Int,
		@Cnt		Int,
		@ProgramSectionType Int,
		@ProgramSectionTypeCompare Int, 
		@TaxCount	Int,
		@Tax		Numeric(12,4),
		@TaxA		Numeric(12,4),
		@Tax2		Numeric(12,4),
		@Tax2A	Numeric(12,4),
		@Gross		Numeric(10,2),
		@Net		Numeric(10,2),
		@Coh		Int,
		@TransId	Int,
		@ProductCode	Varchar(20),
		@ProductName	Varchar(50),
		@CampaignId    Int,
		@SecTypeCnt 	Int,
		@Cnt1 		Int,
		@SecMaxId	Int,
		@OrderQualifier	Int,
		@AccountId	Int,
		@AccountName	Varchar(100)
	
	Declare
	@AllOrderItem TABLE (
			Id				Int	Identity,
			OrderId				Int	,
			OrderTypeCode			Int,
			OrderQualifierId			Int,
			BatchAccountId			Int	,
			AccountName			Varchar(100),
			AccountProvince		Varchar(10),
			CampaignId 			Int	,
			CampaignAccProvince		Varchar(10),
			BatchStaffOrderFlag		Int,
			CAStaffOrderFlag		Int,
			CustomerStatus			Int,
			CustomerFirstName		Varchar(50),
			CustomerLastName		Varchar(50),
			Address1			Varchar(50),
			Address2			Varchar(50),
			City				Varchar(50),
			State				Varchar(30),
			Zip				Varchar(10),
			Recipient			Varchar(50),
			Price				Numeric(10,2),
			CatalogPrice			Numeric(10,2),
			PriceOverrideId			Int,
			Tax				Numeric(12,4),
			TaxA				Numeric(12,4),
			Tax2				Numeric(12,4),
			Tax2A				Numeric(12,4),
			Gross				Numeric(10,2),
			Net				Numeric(10,2),
			PricingDetailId			Int,
			ProgramSectionId		Int,
			ProgramSectionType		Int,
			ProductType			Int,
			ProductCode			Varchar(20),
			ProductName			Varchar(50),
			CustomerOrderHeaderInstance	Int,	
			TransId				Int,	
			PaymentMethod			Int
			)

		
		Declare 
		@AllProgram TABLE(
					MajorProductLine Int,
					ProgramName	Varchar(50)
				      )	

		Declare 
		@AllSectionType TABLE(
					Id		Int	Identity,
					SectionTypeId   Int
				      )	


		Declare
		@AllException TABLE (
					Id				Int	Identity,
					CustomerOrderHeaderInstance	Int,
					TransId				Int,
					ProductCode			Varchar(20),
					ProductName			Varchar(50),
					Description			Varchar(250)
				         )	
	--Get All items for the order
	Insert Into @AllOrderItem
	Select   Orderid,
		OrderTypeCode,
		OrderQualifierId,
		b.AccountId,
		CAcc.Name,
		ac.State,
		b.Campaignid,
		Adr.StateProvince, --ShiptoAccount's Province code from Address table
		b.IsStaffOrder    BatchStaffOrderFlag,
		ca.IsStaffOrder,
		c.StatusInstance,
		c.FirstName,
		c.LastName,
		C.Address1,
		C.Address2,
		c.City,
		C.State,
		c.Zip,
		--MS Oct 2, 2006 If Problem Solver (Kanata/Gift) Ignore Null recipient
		Case OrderQualifierId 
		When 39018 Then Case IsNull(Recipient,'')
				    When '' Then 'ZZZZZZ'
				    Else Recipient
				    End
		When 39019 Then Case IsNull(Recipient,'')
				    When '' Then 'ZZZZZZ'
				    Else Recipient
				    End
		When 39005 Then Case IsNull(Recipient,'')
				    When '' Then 'ZZZZZZ'
				    Else Recipient
				    End
		Else  Recipient
		End Recipient,
		d.Price,
		CatalogPrice,
		priceOverrideId, -- 1Coupon,2Invalid Price,3Closest Matching Offer,4None,5Replacement
		d.Tax,
		d.TaxA,
		Tax2,
		d.Tax2A,
		d.Gross,
		d.Net	,
		d.pricingdetailsId,
		d.ProgramSectionId,
		progSec.Type,
		d.productType,
		d.ProductCode,
		d.ProductName,
		h.Instance,
		d.TransId,
		h.paymentmethodInstance
	From QSPCanadaCommon.dbo.Address Adr RIGHT OUTER JOIN
                      QSPCanadaCommon.dbo.CAccount CAcc ON Adr.AddressListID = CAcc.AddressListID AND Adr.address_type = 54001 RIGHT OUTER JOIN
                      QSPCanadaOrderManagement.dbo.Batch b INNER JOIN
                      QSPCanadaOrderManagement.dbo.CustomerOrderHeader h ON b.ID = h.OrderBatchID AND b.[Date] = h.OrderBatchDate INNER JOIN
                      QSPCanadaOrderManagement.dbo.CustomerOrderDetail d ON h.Instance = d.CustomerOrderHeaderInstance INNER JOIN
                      QSPCanadaOrderManagement.dbo.Customer c ON h.CustomerBillToInstance = c.Instance INNER JOIN
                      QSPCanadaCommon.dbo.Campaign ca ON b.CampaignID = ca.ID ON CAcc.Id = b.ShipToAccountID LEFT OUTER JOIN
                      QSPCanadaOrderManagement.dbo.Account ac ON b.AccountID = ac.ID LEFT OUTER JOIN
                      QSPCanadaProduct.dbo.ProgramSection progSec ON d.ProgramSectionID = progSec.ID
	Where  b.OrderID = @OrderId 
	And d.Producttype Not in (46013,46014,46015,46017) 	--Incentives 
	And OrderTypeCode Not in (41002) 		--CAFS
	And D.DelFlag=0
	And D.Productcode <> 'NNNN'
	And D.statusInstance Not in (501) 	

	Select top 1 1 from @AllOrderItem

	If @@RowCount > 0 
	Begin

	Insert into @AllException
	Select 	CustomerOrderHeaderInstance,
		TransId,
		ProductCode,
		ProductName,
		(Case	When ProductCode = '9999'				Then 'Invalid Product code - Illegible Item'
	          		When IsNull(Recipient,'')   = ''         			Then 'Missing Recipient Name'
			When IsNull(PricingDetailId,0) = 0 			Then 'Zero Pricing Detail'
			When Isnull(ProgramSectionId,0)=0			Then 'Zero Program Section'
			When IsNull(AccountProvince,'@@') = '@@'		Then 'Missing Account Province'
			When PaymentMethod not  in (50004,50003,50002,50005) Then 'Invalid Payment Method' 
--			When (PaymentMethod in (50004,50003) And 
--			           (IsNull(CustomerFirstName,'')='' Or  IsNull(CustomerLastName,'')='' ))	Then 'Credit card owner name missing'
			--When (PaymentMethod in (50002) And 
			         --  (IsNull(CustomerFirstName,'')='' Or  IsNull(CustomerLastName,'')='' ))	Then 'Customer last/first name missing'

			When IsNull(BatchStaffOrderFlag,'99') <> IsNull(CAStaffOrderFlag,'9')	Then 'CA StaffOrder flag does not match with order'
--			When IsNull(AccountProvince,'@') <> IsNull(CampaignAccProvince,'@@')	Then 'Province code in CAccount does not match with code in Account'
			When IsNull(CampaignAccProvince,'@') ='@'				Then 'Shipto Account''s Address contain blank province'
--			When (IsNull(CatalogPrice,-2) <> IsNull(Price,-1) And PriceOverrideId = 45004 ) Then 'Price does not match with catalog price'
--			When IsNull(CustomerStatus,0)= 301 	Then 'Customer Status is Invalid'
--			When IsNull(Address1,'') = ''  		Then 'Missing Street Address for customer'
--	      		When Isnull(City,'') = ''      		Then 'Missing City for Customer'
--	      		When Isnull(State,'')= ''      		Then 'Missing province for customer'
 --             		When Isnull(Zip,'')= ''        		Then 'Missing Postal Code for Customer'


		End) Error
	
	From @AllOrderItem
	Where  ProductCode = '9999'		
	          		OR IsNull(Recipient,'')   = ''         			
			OR IsNull(PricingDetailId,0) = 0 			
			OR Isnull(ProgramSectionId,0)=0			
			OR IsnUll(AccountProvince,'@@') = '@@'		
--			OR (PaymentMethod in (50004,50003) And  (IsNull(CustomerFirstName,'')='' Or  IsNull(CustomerLastName,'')='' ))	
			OR IsNull(BatchStaffOrderFlag,'99') <> IsNull(CAStaffOrderFlag,'9')	
--			OR IsNull(AccountProvince,'@') <> IsNull(CampaignAccProvince,'@@')	
--			OR IsNull(CustomerStatus,0)= 301 	
--			OR IsNull(Address1,'') = ''  		
--	      		OR Isnull(City,'') = ''      		
--	      		OR Isnull(State,'')= ''      		
 --             		OR Isnull(Zip,'')= ''        		
			OR  IsNull(CampaignAccProvince,'@') ='@'	


	
	--Get the CampaignId and check programs running under CA
	Select Top 1 @CampaignId = CampaignId , @OrderQualifier = OrderQualifierId from @AllOrderItem

	declare @Iskanata int
	declare @KanataCA int

	set @KanataCA =0
	Set @Iskanata =0

	 If IsNull(@CampaignId,0)=0
	Begin 
		  Select  @Iskanata=1, @KanataCA = CampaignId  from QSPCanadaOrderManagement..Batch b Where orderid=@OrderId and orderqualifierid=39018
	End	

	
	If IsNull(@CampaignId,0) > 0
	Begin
		Insert into @AllProgram
		Select 	MajorProductlineId , -- 1 Magazine	 2 GIFT
			Name 
		From 	QSPCanadacommon..CampaignProgram cp, 
			QSPCanadacommon..Program p
		Where p.Id = cp.ProgramId
		And ProgramTypeId In(36001)  --Exclude Incentive and Rewards
		And CampaignId= @CampaignId
		And DeletedTF = 0
	End
	Else	--Modified Nov 5, 2006 KanataOrder can have only Incentives No record for CA in @AllOrderItem
	If @Iskanata = 0
	Begin
		
		       Insert into @AllException Values (Null,Null,Null,Null,'Order has blank CA in Batch or Incorrect order qualifier')
	End
	Else
	Begin
		If @Iskanata =1 and @KanataCA = 0
		Begin
			  Insert into @AllException Values (Null,Null,Null,Null,'Order has blank CA in Batch')
		End
	End
	--Check number of items for each program/productline  under CA
	/*
	If (Select Count(*) from @AllProgram) >0  
	Begin
		If @OrderQualifier In (39001,39009)
		Begin
			Select @ItemCount =  Count(*) From @AllOrderItem Where ProductType  In (46001,46006)
			Select @MajorProdLine= MajorProductline From @AllProgram Where MajorProductline = 1 -- Magazine

			If @MajorProdLine >0 And (@ItemCount =0 )
			Begin
				Insert into @AllException Values (Null,Null,Null,Null,'Magazine item count zero for Magazine Program')
			End
		
			Set @MajorProdLine = 0 -- Reset variable before checking the next 
			
			If @OrderQualifier Not In (39009)
			Begin
				Select @ItemCount= Count(*) From @AllOrderItem Where ProductType  In(46002,46003,46005,46007,46010,46011,46012)
				Select @MajorProdLine = MajorProductline From @AllProgram Where MajorProductline = 2 -- Gift
				If  ( IsNull(@MajorProdLine,0) >0 And IsNull(@ItemCount,0) =0 )
				Begin
					Insert into @AllException Values (Null,Null,Null,Null,'Gift item count zero for CA running Gift Program')
				End
			End
		End
	End	
	*/
	Select Top 1  @ProvinceCode= AccountProvince From @AllOrderItem

	-- If provice code is not null check if the all applicable taxes have been calculated
	If IsNull(@ProvinceCode,'@@') <>  '@@'  
	Begin
		Insert into @AllSectionType
		Select Distinct ProgramSectionType From @AllOrderItem Where   IsNull(ProgramSectionId,0)  >0   
		-- Null program Section already caught and SectionType for Incentives excluded

		Select @SecTypeCnt= Count(*) , @SecMaxId = Max(Id)  from @AllSectionType  
		Set  @Cnt1 = 0
		While  @SecTypeCnt > 0
		Begin
			Select @ProgramSectionType= SectionTypeId From @AllSectionType Where  id = (@SecMaxId - @Cnt1)
	
			
			--Find Applicable Taxes for province and section Type
			Select @taxCount = Count(tax.tax_id)  From QSPCanadaCommon..TAXAPPLICABLETAX tax  Where tax.province_code = @ProvinceCode
			And  tax.section_type_id = IsNull(@ProgramSectionType,2) -- 2 = magazine


			-- Count all magazine item and loop thriugh to check if all applicable taxes have been calculated
			Select @ItemCount= Count(*) , @MaxId = Max(Id)  from @AllOrderItem
			 Where   IsNull(ProgramSectionId,0)  >0 
			Set @Cnt = 0
			While @ItemCount > 0 
			Begin
				-- Get the program section type  and find applicable taxes
				Select 	@ProgramSectionTypeCompare = ProgramSectionType , 
					@Tax=Tax,@TaxA=TaxA,@Tax2=Tax2, @Tax2A=Tax2A,@Gross =Gross,@Net = Net,
					@Coh= CustomerOrderHeaderInstance, 
					@TransId= TransId, 
					@ProductCode= ProductCode,
					@ProductName=ProductName,
					@OrderQualifier = OrderQualifierId, 
					@AccountId= BatchAccountId,
					@AccountName= AccountName
				From @AllOrderItem 
				Where  IsNull(ProgramSectionId,0)  >0  
				And OrderQualifierId Not In(39019,39018,39005) --problem Solver Zero price MS Oct 24, 2006
				And Id = @MaxId - @Cnt
				
				--Select 	@ProgramSectionTypeCompare,@Tax,@TaxA,@Tax2,@Tax2A,@Gross,@Net,@Coh,@OrderQualifier,@AccountId,@AccountName

				If ( @taxCount =1) And  
				   (@ProgramSectionType = @ProgramSectionTypeCompare) And
				     ( (IsNull(@Tax,0) =0  Or IsNull(@Gross,0)=0 Or IsNull(@Net,0)=0) )--Tax2 is not Applicable only GST
				    
				    --Disabled  Sept 15, 2005 MS only TAX and TAX2 to check 
    				   /*( (IsNull(@Tax,0) =0 Or IsNull(@TaxA,0) =0 Or IsNull(@Gross,0)=0 Or IsNull(@Net,0)=0) OR
					  (IsNull(@Tax2,0) <> 0 Or IsNull(@Tax2A,0) <> 0 ) --Tax2 is not Applicable only GST
				   )*/
				   --Free Accounts (QSP Conferance etc) or Cust Serv orders have zero price  - Added Sept 14, 2005 MS
				   And @OrderQualifier Not In (39008,39017,39023) 			--Cust Service, WFC Bonus
				   And @AccountId Not In(18626,18627,18721,18752)		--Free Accounts\
				   And @ProductCode Not Like 'DO%' --Donation Items
				   And @ProductCode Not Like 'D1%' --Variable Donation Items

				Begin
					Insert into @AllException Values (@Coh,@TransId,@ProductCode,@ProductName,'Zero Tax or  Zero Gross/Net or wrong Tax Calculation for the item (GST/HST) ')
		   
				End
			
				If (@taxCount > 1) And 
				   (@ProgramSectionType = @ProgramSectionTypeCompare) And
				    (IsNull(@Tax,0) =0 Or IsNull(@Gross,0)=0 Or IsNull(@Net,0)=0)
				   --Disable Sept 15, 2005 MS only TAX and TAX2 to check 
				  -- (IsNull(@Tax,0) =0 Or IsNull(@TaxA,0) =0 or IsNull(@Tax2,0) =0 Or IsNull(@Tax2A,0) =0 Or IsNull(@Gross,0)=0 Or IsNull(@Net,0)=0)
				   --Free Accounts (QSP Conferance etc) or Cust Serv orders have zero price  - Added Sept 14, 2005 MS
				   And @OrderQualifier Not In (39008,39017,39023) 			--Cust Service, WFC Bonus
				  And @AccountId Not In(18626,18627,18721,18752)		--Free Accounts
				  And @ProductCode Not Like 'DO%' --Donation Items
				  And @ProductCode Not Like 'D1%' --Variable Donation Items
		 		  
				Begin
					Insert into @AllException Values (@Coh,@TransId,@ProductCode,@ProductName,'Zero Tax or  Zero Gross/Net or wrong tax calculation for the item (GST and PST) ')
		   
				End

			Set @Cnt = @Cnt+1
			Set @ItemCount = @ItemCount - 1
			End

		Set @Cnt1 = @Cnt1+1
		Set @SecTypeCnt = @SecTypeCnt - 1
		End  --SecType
	End
	End -------------------> if there are records to validate
	Select * From @AllException	
	If @@RowCount  > 0
	Begin
		Set @RetVal = 0  --Fail
		Insert into QSPCanadaOrderManagement.dbo.OrderClosingLog
		Select @OrderId,  CustomerOrderHeaderInstance,	TransId	, ProductCode,ProductName, Description,'PRE',GetDate(),0,Null
		From @AllException
	End
	Else
		Set @RetVal =1	 -- Success
GO
