sp_columns 'CreditCardBatch'
/*
** Change primary key on CreditCardbatch
*/

ALTER TABLE [dbo].[CustomerOrderHeader]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerOrderHeaderBatch] FOREIGN KEY([OrderBatchDate],[OrderBatchID])
REFERENCES [dbo].[Batch] ([Date],[ID])
Go
ALTER TABLE [dbo].[CustomerOrderHeader]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerOrderHeaderStudent] FOREIGN KEY([StudentInstance])
REFERENCES [dbo].[Student] ([Instance])

GO
ALTER TABLE [dbo].[CustomerOrderHeader]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerOrderHeaderBillToCustomer] FOREIGN KEY([CustomerBillToInstance])
REFERENCES [dbo].[Customer] ([Instance])
GO
ALTER TABLE [dbo].[CustomerOrderDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerOrderDetail] FOREIGN KEY([CustomerOrderHeaderInstance])
REFERENCES [dbo].[CustomerOrderHeader] ([Instance])
GO
ALTER TABLE [dbo].[CustomerOrderDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerOrderDetailShipTo] FOREIGN KEY([CustomerShipToInstance])
REFERENCES [dbo].[Customer] ([Instance])
GO

ALTER TABLE [dbo].[Student]  WITH NOCHECK ADD  CONSTRAINT [FK_StudentTeacher] FOREIGN KEY([TeacherInstance])
REFERENCES [dbo].[Teacher] ([Instance])
GO
ALTER TABLE [dbo].[CustomerPaymentHeader]  WITH NOCHECK ADD  CONSTRAINT [FK_PaymentOrderHeader] FOREIGN KEY([CustomerOrderHeaderInstance])
REFERENCES [dbo].[CustomerOrderHeader] ([Instance])
GO
ALTER TABLE [dbo].[creditcardpayment]  WITH NOCHECK ADD  CONSTRAINT [FK_CCPaymentHeader] FOREIGN KEY([CustomerPaymentHeaderInstance])
REFERENCES [dbo].[CustomerPaymentHeader] ([Instance])
GO
ALTER TABLE [dbo].[creditcardpayment]  WITH NOCHECK ADD  CONSTRAINT [FK_CCCreditCardBatch] FOREIGN KEY([BatchID])
REFERENCES [dbo].[CreditCardBatch] ([ID])
GO
ALTER TABLE [dbo].[InternetOrderID]  WITH NOCHECK ADD  CONSTRAINT [FK_InternetOrderHeader] FOREIGN KEY([CustomerOrderHeaderInstance])
REFERENCES [dbo].[CustomerOrderHeader] ([Instance])
GO

Select Description, Instance from codedetail where codeheaderinstance= 40000
Select *from batch 

 where date = '10/28/12' order by date desc
Select * from Customerorderheader where orderbatchdate = '10/28/12'
Select * from teacher where instance = 291298

Select * from Customerorderheader coh
	join customerorderdetail on Customerorderheaderinstance= coh.instance 
	join student s on coh.studentinstance = s.instance
	--join teacher t on s.teacherinstance = t.instance
	join customer c on c.instance = customerbilltoinstance
	where  orderbatchdate = '10/23/12' and orderbatchid=1000
Select * from NextTableID
Insert Batch(date, id, orderid) values('10/20/12', 1000,10054905)

Delete customerorderdetail where customerorderheaderinstance in  (Select instance from Customerorderheader where orderbatchdate = '10/23/12' and orderbatchid=1000)
delete Customerorderheader where orderbatchdate = '10/23/12' and orderbatchid=1000
delete batch where orderid = 10054905

Select * from teacher where name like 'DEREPENTIGNY'

Select Max(orderid) from batch
exec InsertBatch  10054905
Select * from codedetail where instance = 39015

alter Procedure dbo.InsertBatch  

	@OrderBatchDate datetime
	,@AccountID int
	,@EnterredCount int
	,@EnterredAmount numeric(10,2)
	,@CalculatedAmount numeric(10,2)
	,@StatusInstance int
	,@KE3FileName varchar(200)
	,@ChangeUserID varchar(4)
	,@ChangeDate datetime
	,@TeacherCount int
	,@StudentCount int
	,@CustomerCount int
	,@OrderCount int
	,@OrderCountAccept int
	,@OrderDetailCount int
	,@OrderDetailCountError int
	,@StartImportTime datetime
	,@EndImportTime datetime
	,@ImportTimeSeconds int
	,@Clerk varchar(4)
	,@DateCreated datetime
	,@UserIDCreated varchar(4)
	,@DateKeyed datetime
	,@DateBatchCompleted datetime
	,@OverridePctState bit
	,@PctState numeric(10,2)
	,@OriginalStatusInstance int
	,@OrderTypeCode int
	,@CampaignID int 
	,@BillToAddressID int
	,@ShipToAddressID int
	,@ShipToAccountID int
	,@BillToFMID varchar(4)
	,@ShipToFMID varchar(4)
	,@ReportedEnvelopes int
	,@PaymentSend numeric(10,2)
	,@SalesBeforeTax numeric(10,2)
	,@DateSent datetime
	,@DateReceived datetime
	,@ContactFirstName varchar(50)
	,@ContactLastName varchar(50)
	,@ContactEmail varchar(50)
	,@ContactPhone varchar(50)
	,@Comment varchar(300)
	,@IncentiveCalculationStatus int
	,@MagnetBookletCount int
	,@MagnetCardCount int
	,@MagnetGoodCardCount int
	,@MagnetCardsMailed int
	,@MagnetMailDate datetime
	,@PickDate datetime
	,@IsDMApproved bit
	,@CountryCode varchar(10)
	,@PickLine int
	,@OrderQualifierID int = 0 
	,@CheckPayableToQSPAmount numeric(10,2) = 0.00
	,@IsIncentive bit = 0
	,@OrderDeliveryDate datetime ='1/1/95'
	,@RefNumber int = 0
	,@PaymentBatchDate datetime ='1/1/95'
	,@PaymentBatchID int = 0 
	,@IsStaffOrder bit =0 
	,@InquireUponComplete bit =0
	,@GroupProfit numeric(10,2) = 0.00
	,@OrderID int = 0
	,@OrderAmntDue numeric(10,2) = 0.00
	,@MagnetPostage numeric(10,2) =0.00
	,@OrderIDIncentive int = 0
	,@IsInvoiced bit = 0
	,@CampaignNetTotal numeric(10,2) = 0.00
	,@DistributionCenterID int = 0
	,@IsMagQueueDone int = 0
	,@ProblemID int = 0
	,@CheckDate datetime ='1/1/95'
	,@CheckNumber int  = 0
	,@MagnetUnitPostage  numeric(10,2) = 0.00
	,@PostageAdjustmentID int = 0
as
		
	Declare @id int
	Declare @batchdate datetime
	Select @batchdate = Convert(varchar(10),GetDate(),101)

    if(@orderqualifierid = 39009)
    begin
		Select @batchdate=@OrderBatchDate
		exec [dbo].[CreateBatch]
					@batchdate ,
					@AccountID,
					@ShipToAccountID ,
					@campaignid ,
					@statusinstance ,
					@ordertypecode , 
					@orderqualifierid ,
					@orderid  OUTPUT
       	Update Batch set 
				EnterredCount=@EnterredCount
				,EnterredAmount=@EnterredAmount
				,CalculatedAmount=@CalculatedAmount
				,KE3FileName=@KE3FileName
				,ChangeUserID=@ChangeUserID
				,ChangeDate=@ChangeDate
				,TeacherCount=@TeacherCount
				,StudentCount=@StudentCount
				,CustomerCount=@CustomerCount
				,OrderCount=@OrderCount
				,OrderCountAccept=@OrderCountAccept
				,OrderDetailCount=@OrderDetailCount
				,OrderDetailCountError=@OrderDetailCountError
				,StartImportTime=@StartImportTime
				,EndImportTime=@EndImportTime
				,ImportTimeSeconds=@ImportTimeSeconds
				,Clerk=@Clerk
				,DateCreated=@DateCreated
				,UserIDCreated=@UserIDCreated
				,DateKeyed=@DateKeyed
			where orderid = @orderid
    end
	else
	begin

		SELECT @id = (isnull(max(id),1)) from Batch where date=@batchdate and id between 1000 and 19999 group by date

		If (@id is NULL)
		Begin
			Select  @id = 1000
		End
		ELSE
		Begin
			SELECT @id=isnull(max(id),1000)+1 from Batch where date=@batchdate and id between 1000 and 19999 group by date
		End
		
		Insert Batch(date, id, orderid) values(@batchdate, @id, @orderid)

	End

	
	Select @Id = id from batch where orderid = @orderid
	Select @batchdate as Date, @id as ID, @orderid as OrderID
	go
/****************/	
	
	
alter procedure dbo.InsertCustomerOrderHeader
			@NextDetailTransID int
			,@AccountID int
			,@CustomerBillToInstance int
			,@StudentInstance int
			,@StatusInstance int
			,@FirstStatusInstance int
			,@TotalProcessingFee float
			,@TotalProcessingFeeA float
			,@ProcessingFeeTax float
			,@ProcessingFeeTaxA float
			,@ProcessingFeeTransID int
			,@orderbatchdate datetime
			,@orderbatchid int
			,@OrderBatchSequence int
			,@CreationDate datetime
			,@LastSentInvoiceDate datetime
			,@NumberInvoicesSent int
			,@ForceInvoice bit
			,@DelFlag bit
			,@ChangeUserID varchar(4)
			,@ChangeDate datetime
			,@Type int
			,@PaymentMethodInstance int
			,@CampaignID int
				
as
	declare @ins table (instance int)
	Insert into	@ins exec dbo.InsertNextInstance 4
	Insert CustomerOrderHeader(Instance
								,NextDetailTransID
								,AccountID
								,CustomerBillToInstance
								,StudentInstance
								,StatusInstance
								,FirstStatusInstance
								,TotalProcessingFee
								,TotalProcessingFeeA
								,ProcessingFeeTax
								,ProcessingFeeTaxA
								,ProcessingFeeTransID
								,OrderBatchDate
								,OrderBatchID
								,OrderBatchSequence
								,CreationDate
								,LastSentInvoiceDate
								,NumberInvoicesSent
								,ForceInvoice
								,DelFlag
								,ChangeUserID
								,ChangeDate
								,Type
								,PaymentMethodInstance
								,CampaignID) 
	
		select instance,@NextDetailTransID 
			,@AccountID 
			,@CustomerBillToInstance 
			,@StudentInstance 
			,@StatusInstance 
			,@FirstStatusInstance 
			,@TotalProcessingFee 
			,@TotalProcessingFeeA 
			,@ProcessingFeeTax 
			,@ProcessingFeeTaxA 
			,@ProcessingFeeTransID 
			,@orderbatchdate 
			,@orderbatchid 
			,@OrderBatchSequence 
			,@CreationDate 
			,@LastSentInvoiceDate 
			,@NumberInvoicesSent 
			,@ForceInvoice 
			,@DelFlag 
			,@ChangeUserID 
			,@ChangeDate 
			,@Type 
			,@PaymentMethodInstance 
			,@CampaignID  from @ins
	select instance from @ins
	
alter Procedure dbo.InsertTeacher
		@AccountID int
		,@Name varchar(101)
		,@Classroom varchar(50)
		,@DateCreated datetime
		,@UserIDCreated varchar(4)
		,@DateChanged datetime
		,@UserIDChanged varchar(4)
		,@Title varchar(10)
		,@FirstName varchar(50)
		,@MiddleInitial varchar(10)
		,@LastName varchar(50)
as
	declare @ins table (instance int)
	Insert into	@ins exec dbo.InsertNextInstance 1
	Insert Teacher(Instance
					,AccountID
					,Name
					,Classroom
					,DateCreated
					,UserIDCreated
					,DateChanged
					,UserIDChanged
					,Title
					,FirstName
					,MiddleInitial
					,LastName
	) select instance
		,@AccountID
		,@Name
		,@Classroom
		,@DateCreated
		,@UserIDCreated
		,@DateChanged
		,@UserIDChanged
		,@Title
		,@FirstName
		,@MiddleInitial
		,@LastName
	 from @ins
	select instance from @ins
go

Create Procedure dbo.UpdateTeacher
		@Instance int
		,@AccountID int
		,@Name varchar(101)
		,@Classroom varchar(50)
		,@DateCreated datetime
		,@UserIDCreated varchar(4)
		,@DateChanged datetime
		,@UserIDChanged varchar(4)
		,@Title varchar(10)
		,@FirstName varchar(50)
		,@MiddleInitial varchar(10)
		,@LastName varchar(50)
as
	
	Update Teacher Set 
					AccountID = @AccountID
					,Name=@Name
					,Classroom=@Classroom
					,DateCreated=@DateCreated
					,UserIDCreated=@UserIDCreated
					,DateChanged=@DateChanged
					,UserIDChanged=@UserIDChanged
					,Title=@Title
					,FirstName=@FirstName
					,MiddleInitial=@MiddleInitial
					,LastName=@LastName
	where Instance = @Instance					
	
go

alter Procedure dbo.InsertStudent 
			@TeacherInstance int
			,@LastName varchar(50)			
			,@FirstName varchar(101)
			,@DateCreated datetime
			,@UserIDCreated varchar(4)
			,@DateChanged datetime
			,@UserIDChanged varchar(4)
as
	declare @ins table (instance int)
	Insert into	@ins exec dbo.InsertNextInstance 2
	Insert Student(Instance,TeacherInstance
			,LastName
			,FirstName
			,DateCreated
			,UserIDCreated
			,DateChanged
			,UserIDChanged) 
			select instance, @TeacherInstance 
			,@LastName 			
			,@FirstName 
			,@DateCreated 
			,@UserIDCreated 
			,@DateChanged 
			,@UserIDChanged  from @ins
	select instance from @ins	
go	

Create Procedure dbo.UpdateStudent 
			@StudentInstance int
			,@TeacherInstance int
			,@LastName varchar(50)			
			,@FirstName varchar(101)
			,@DateCreated datetime
			,@UserIDCreated varchar(4)
			,@DateChanged datetime
			,@UserIDChanged varchar(4)
as
	update Student set
		TeacherInstance= @TeacherInstance
		,LastName=@LastName
		,FirstName=@FirstName
		,DateChanged = @DateChanged
		,UserIDChanged = @UserIDChanged
	where Instance = @StudentInstance
go	


alter Procedure dbo.InsertCustomer
	@StatusInstance int
	,@LastName varchar(50)
	,@FirstName varchar(50)
	,@Address1 varchar(50)
	,@Address2 varchar(50)
	,@City varchar(50)
	,@County varchar(31)
	,@State varchar(10)
	,@Zip varchar(10)
	,@ZipPlusFour varchar(4)
	,@OverrideAddress bit
	,@ChangeUserID varchar(4)
	,@ChangeDate datetime
	,@Email varchar(50)
	,@Phone varchar(25)
	,@Type int
as
	declare @ins table (instance int)
	Insert into	@ins exec dbo.InsertNextInstance 3
	Insert Customer(Instance
					,StatusInstance
					,LastName
					,FirstName
					,Address1
					,Address2
					,City
					,County
					,State
					,Zip
					,ZipPlusFour
					,OverrideAddress
					,ChangeUserID
					,ChangeDate
					,Email
					,Phone
					,Type	) 
	select instance
			,@StatusInstance 
			,Isnull(@LastName,'')
			,Isnull(@FirstName ,'')
			,@Address1 
			,@Address2 
			,@City 
			,@County 
			,@State 
			,@Zip 
			,@ZipPlusFour 
			,@OverrideAddress 
			,@ChangeUserID 
			,@ChangeDate 
			,@Email 
			,@Phone 
			,@Type 	
	 from @ins
	select instance from @ins		
Go	


create Procedure dbo.UpdateCustomer
	@Instance int
	,@StatusInstance int
	,@LastName varchar(50)
	,@FirstName varchar(50)
	,@Address1 varchar(50)
	,@Address2 varchar(50)
	,@City varchar(50)
	,@County varchar(31)
	,@State varchar(10)
	,@Zip varchar(10)
	,@ZipPlusFour varchar(4)
	,@OverrideAddress bit
	,@ChangeUserID varchar(4)
	,@ChangeDate datetime
	,@Email varchar(50)
	,@Phone varchar(25)
	,@Type int
as
	
	Update Customer set
					StatusInstance=@StatusInstance
					,LastName=isnull(@LastName,'')
					,FirstName=Isnull(@FirstName ,'')
					,Address1=isnull(@Address1,'')
					,Address2=isnull(@Address2,'')
					,City=isnull(@City,'')
					,County=isnull(@County,'')
					,State=isnull(@State,'')
					,Zip=isnull(@Zip,'')
					,ZipPlusFour=isnull(@ZipPlusFour,'')
					,OverrideAddress=@OverrideAddress
					,ChangeUserID=@ChangeUserID
					,ChangeDate=@ChangeDate
					,Email=@Email
					,Phone=@Phone
					,Type=@Type	
	
	  where Instance = @Instance
Go	

alter Procedure dbo.InsertCreditCardBatch
			@InputFileName varchar(200)
			,@OutputFileName varchar(200)
			,@StartImportTime datetime
			,@EndImportTime datetime
			,@Status int
			,@TotalRecordCount int
			,@TotalDollarAmount int 
			,@DateCreated datetime
			,@UserIDCreated  varchar(4)
			,@ChangeDate datetime
			,@ChangeUserID  varchar(4)
			,@Type int
			,@IsGLDone int
as
	declare @ins table (instance int)
	Insert into	@ins exec dbo.InsertNextInstance 8

	insert CreditCardBatch		
		(InputFileName
		,OutputFileName
		,StartImportTime
		,EndImportTime
		,Status
		,TotalRecordCount
		,TotalDollarAmount
		,ID
		,DateCreated
		,UserIDCreated
		,ChangeDate
		,ChangeUserID
		,Type
		,IsGLDone)
	Select 
			@InputFileName 
			,@OutputFileName 
			,@StartImportTime 
			,@EndImportTime 
			,@Status 
			,@TotalRecordCount 
			,@TotalDollarAmount  
			,instance
			,@DateCreated 
			,@UserIDCreated  
			,@ChangeDate 
			,@ChangeUserID  
			,@Type 
			,@IsGLDone 	from @ins

go


create Procedure dbo.InsertCustomerPaymentHeader
	@CustomerOrderHeaderInstance int
	,@InvoiceNumber int
	,@PaymentBatchDate datetime
	,@PaymentBatchID int
	,@PaymentBatchSequence int
	,@NextDetailTransID int
	,@TotalAmount float
	,@DateCreated datetime
	,@UserIDCreated varchar(4) 
	,@DateChanged datetime = '1/1/95'
	,@UserIDChanged varchar(4) = ''
	,@StatusInstance int = 0
	,@IsCreditCard bit = 0
	,@Signed	varchar(1) = 0

as
	declare @ins table (instance int)
	Insert into	@ins exec dbo.InsertNextInstance 6
	Insert CustomerPaymentHeader(Instance
					,CustomerOrderHeaderInstance
					,InvoiceNumber
					,PaymentBatchDate
					,PaymentBatchID
					,PaymentBatchSequence
					,NextDetailTransID
					,TotalAmount
					,DateCreated
					,UserIDCreated
					,DateChanged
					,UserIDChanged
					,StatusInstance
					,IsCreditCard
					,Signed	) 
	select instance
			,@CustomerOrderHeaderInstance 
			,@InvoiceNumber 
			,@PaymentBatchDate 
			,@PaymentBatchID 
			,@PaymentBatchSequence 
			,@NextDetailTransID 
			,@TotalAmount 
			,@DateCreated 
			,@UserIDCreated 
			,@DateChanged 
			,@UserIDChanged 
			,@StatusInstance 
			,@IsCreditCard 
			,@Signed	
	 from @ins
	select instance from @ins		
Go	


create Procedure dbo.InsertCreditCardPayment
	@CustomerPaymentHeaderInstance int
	,@CreditCardNumber varchar(25)
	,@ExpirationDate varchar(10)
	,@ReasonCode int
	,@AuthorizationSource varchar(2)	
	,@AuthorizationCode varchar(6)
	,@AuthorizationDate datetime
	,@AVSResponseCode varchar(2)
	,@StatusInstance int
	,@DateCreated datetime
	,@UserIDCreated varchar(4)
	,@DateChanged datetime
	,@UserIDChanged varchar(4)
	,@BatchID int
	,@VeriSignID varchar(13)

as
	Insert CreditCardPayment(CustomerPaymentHeaderInstance
				,CreditCardNumber
				,ExpirationDate
				,ReasonCode
				,AuthorizationSource
				,AuthorizationCode
				,AuthorizationDate
				,AVSResponseCode
				,StatusInstance
				,DateCreated
				,UserIDCreated
				,DateChanged
				,UserIDChanged
				,BatchID
				,VeriSignID	) 
	select @CustomerPaymentHeaderInstance 
				,@CreditCardNumber 
				,@ExpirationDate 
				,@ReasonCode 
				,@AuthorizationSource 	
				,@AuthorizationCode 
				,@AuthorizationDate 
				,@AVSResponseCode 
				,@StatusInstance 
				,@DateCreated 
				,@UserIDCreated 
				,@DateChanged 
				,@UserIDChanged 
				,@BatchID 
				,@VeriSignID 
	 
	select @CustomerPaymentHeaderInstance	
Go	

/*  
** other procs
*/
USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spGetProductCodeFromRemitCodeAndTerm]    Script Date: 11/19/2012 11:41:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spGetProductCodeFromRemitCodeAndTerm]    Script Date: 11/27/2012 09:56:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[spGetProductCodeFromRemitCodeAndTerm]
	
	@zRemitCode		varchar(20),
	@iTerm			int,
	@iCampaignID	int

 AS

/*********************** get current season ******************************/
DECLARE 	@zProductSeason 	char(1)
DECLARE		@iProductYear		int

EXEC		pr_RemitTest_GetCurrentSeason @zProductSeason output, @iProductYear output
/*************************************************************************/

SET NOCOUNT ON

DECLARE @zProductCode					varchar(20),
		@iMainENProgramSectionID		int,
		@iMainFRProgramSectionID		int,
		@iStaffProgramSectionID			int,
		@bIsStaffCampaign				bit,
		@iAccountID						int

-- Get catalog sections
SELECT	@iMainENProgramSectionID = ps.ID
FROM	QSPCanadaProduct..ProgramSection ps
JOIN	QSPCanadaProduct..Program_Master pm on pm.Program_ID = ps.Program_ID
JOIN	QSPCanadaCommon..Season s on s.ID = pm.Season
WHERE	pm.Code LIKE 'MAG%'
AND		pm.Lang = 'EN'
AND		s.FiscalYear = @iProductYear
AND		s.Season = @zProductSeason

/*SELECT	@iMainENProgramSectionID = ps.ID
FROM	QSPCanadaProduct..ProgramSection ps,
		QSPCanadaProduct..Program_Master pm,
		QSPCanadaProduct..Pricing_Details pdFS,
		QSPCanadaProduct..ProgramSection psFS,
		QSPCanadaProduct..Program_Master pmFS,
		QSPCanadaProduct..ProgFSSectionMap pfssmFS
WHERE	pm.Program_ID = ps.Program_ID
AND		pdFS.FSContent_Catalog_Code = pm.Code
AND		psFS.ID = pdFS.ProgramSectionID
AND		pmFS.Program_ID = psFS.Program_ID
AND		pfssmFS.Catalog_Section_ID = psFS.ID
AND		psFS.Type = 3
AND		pfssmFS.Program_ID = 1
AND		pdFS.FSIsBrochure = 1
AND		pdFS.TaxRegionID = 1
AND		COALESCE(pdFS.FSProvinceCode, '') = ''
AND		pm.SubType <> 30305
AND		COALESCE(ps.Name, '') NOT LIKE 'Book%'
AND		pdFS.Language_Code = 'EN'
AND		pdFS.Product_Code LIKE '%GST%'
AND		pm.Program_Type NOT LIKE 'Staff%'
AND		pdFS.Pricing_Year = @iProductYear
AND		pdFS.Pricing_Season = @zProductSeason*/

SELECT	@iMainFRProgramSectionID = ps.ID
FROM	QSPCanadaProduct..ProgramSection ps
JOIN	QSPCanadaProduct..Program_Master pm on pm.Program_ID = ps.Program_ID
JOIN	QSPCanadaCommon..Season s on s.ID = pm.Season
WHERE	pm.Code LIKE 'MAG%'
AND		pm.Lang = 'FR'
AND		s.FiscalYear = @iProductYear
AND		s.Season = @zProductSeason

/*SELECT	@iMainFRProgramSectionID =  ps.ID
FROM	QSPCanadaProduct..ProgramSection ps,
		QSPCanadaProduct..Program_Master pm,
		QSPCanadaProduct..Pricing_Details pdFS,
		QSPCanadaProduct..ProgramSection psFS,
		QSPCanadaProduct..Program_Master pmFS,
		QSPCanadaProduct..ProgFSSectionMap pfssmFS
WHERE	pm.Program_ID = ps.Program_ID
AND		pdFS.FSContent_Catalog_Code = pm.Code
AND		psFS.ID = pdFS.ProgramSectionID
AND		pmFS.Program_ID = psFS.Program_ID
AND		pfssmFS.Catalog_Section_ID = psFS.ID
AND		psFS.Type = 3
AND		pfssmFS.Program_ID = 1
AND		pdFS.FSIsBrochure = 1
AND		pdFS.TaxRegionID = 1
AND		COALESCE(pdFS.FSProvinceCode, '') = ''
AND		pm.SubType <> 30305
AND		COALESCE(ps.Name, '') NOT LIKE 'Book%'
AND		pdFS.Language_Code = 'FR'
AND		pdFS.Product_Code LIKE '%GST%'
AND		pm.Program_Type NOT LIKE 'Staff%'
AND		pdFS.Pricing_Year = @iProductYear
AND		pdFS.Pricing_Season = @zProductSeason*/

SELECT	@iStaffProgramSectionID = ps.ID
FROM	QSPCanadaProduct..ProgramSection ps,
		QSPCanadaProduct..Program_Master pm,
		QSPCanadaCommon..Season s
WHERE	pm.Program_ID = ps.Program_ID
AND		s.ID = pm.Season
AND		pm.Program_Type like 'Staff%'
AND		s.FiscalYear = @iProductYear
AND		s.Season = @zProductSeason

SELECT	@bIsStaffCampaign = IsStaffOrder,
		@iAccountID = ShipToAccountID
FROM	QSPCanadaCommon..Campaign
WHERE	ID = @iCampaignID

--If Staff campaign, get Staff product code
IF (@bIsStaffCampaign = 1)
BEGIN
	SELECT DISTINCT	@zProductCode = ISNULL(p.Product_code,'')
	FROM			QSPCanadaProduct..Product p
	LEFT JOIN		QSPCanadaproduct..Pricing_Details pd
						ON	pd.Product_Instance = p.Product_Instance
	LEFT JOIN		QSPCanadaproduct..ProgramSection ps
						ON	ps.ID = pd.ProgramSectionID
	LEFT JOIN		QSPCanadaproduct..Program_Master pm
						ON	pm.Program_ID = ps.Program_ID
	WHERE			p.RemitCode = @zRemitCode
	AND				pd.Nbr_of_Issues = @iTerm
	AND				p.Status IN (30600, 30603) --Active or Unremittable
	AND				ps.ID IN (@iStaffProgramSectionID)

	-- if none found then disregard terms chosen
	IF (@zProductCode IS NULL)
	BEGIN	
		SELECT DISTINCT TOP 1	@zProductCode = ISNULL(p.Product_code,'')
		FROM					QSPCanadaProduct..Product p
		LEFT JOIN				QSPCanadaproduct..Pricing_Details pd
									ON	pd.Product_Instance = p.Product_Instance
		LEFT JOIN				QSPCanadaproduct..ProgramSection ps
									ON	ps.ID = pd.ProgramSectionID
		LEFT JOIN				QSPCanadaproduct..Program_Master pm
									ON	pm.Program_ID = ps.Program_ID
		WHERE					p.Product_Year = @iProductYear
		AND						p.Product_Season = @zProductSeason
		AND						p.RemitCode = @zRemitCode
		AND						p.Status IN (30600, 30603) --Active or Unremittable
		AND						ps.ID IN (@iStaffProgramSectionID)
	END

	-- if none found then assume the remit code matches the product code (necessary for Books Online)
	IF (@zProductCode IS NULL)
	BEGIN	
		SELECT	@zProductCode = ISNULL(@zRemitCode, '')
	END
END
ELSE
BEGIN
	SELECT DISTINCT	@zProductCode = ISNULL(p.Product_code,'')
	FROM			QSPCanadaProduct..Product p
	LEFT JOIN		QSPCanadaproduct..Pricing_Details pd
						ON	pd.Product_Instance = p.Product_Instance
	LEFT JOIN		QSPCanadaproduct..ProgramSection ps
						ON	ps.ID = pd.ProgramSectionID
	LEFT JOIN		QSPCanadaproduct..Program_Master pm
						ON	pm.Program_ID = ps.Program_ID
	WHERE			p.RemitCode = @zRemitCode
	AND				pd.Nbr_of_Issues = @iTerm
	AND				p.Status IN (30600, 30603) --Active or Unremittable
	AND				ps.ID IN (@iMainENProgramSectionID, @iMainFRProgramSectionID)
	AND				p.Product_Code <> '8212' --Sports Illustrated Kids has 2 products for 1 remit code
	AND				LEFT(p.Product_code, 1) <> 'K'
	AND				((LEFT(p.Product_code, 1) = 'G' AND RIGHT(p.Product_code, 1) = 'G' AND @iAccountID = 30045)
	OR				(LEFT(p.Product_code, 1) <> 'G' AND @iAccountID <> 30045))


	-- if none found then disregard terms chosen
	IF (@zProductCode IS NULL)
	BEGIN	
		SELECT DISTINCT TOP 1	@zProductCode = ISNULL(p.Product_code,'')
		FROM					QSPCanadaProduct..Product p
		LEFT JOIN				QSPCanadaproduct..Pricing_Details pd
									ON	pd.Product_Instance = p.Product_Instance
		LEFT JOIN				QSPCanadaproduct..ProgramSection ps
									ON	ps.ID = pd.ProgramSectionID
		LEFT JOIN				QSPCanadaproduct..Program_Master pm
									ON	pm.Program_ID = ps.Program_ID
		WHERE					p.Product_Year = @iProductYear
		AND						p.Product_Season = @zProductSeason
		AND						p.RemitCode = @zRemitCode
		AND						p.Status IN (30600, 30603) --Active or Unremittable
		AND						ps.ID IN (@iMainENProgramSectionID, @iMainFRProgramSectionID)
		AND						p.Product_Code <> '8212' --Sports Illustrated Kids has 2 products for 1 remit code
		AND						LEFT(p.Product_code, 1) <> 'K'
		AND						((LEFT(p.Product_code, 1) = 'G' AND RIGHT(p.Product_code, 1) = 'G' AND @iAccountID = 30045)
		OR						(LEFT(p.Product_code, 1) <> 'G' AND @iAccountID <> 30045))	
	END

	-- if none found then assume the remit code matches the product code (necessary for Books Online)
	IF (@zProductCode IS NULL)
	BEGIN	
		SELECT	@zProductCode = ISNULL(@zRemitCode, '')
	END
END

SELECT @zProductCode as ProductCode
go


alter procedure CalculateTax
	@p_date                     	VARCHAR(10),
    @p_amount 		 numeric(10,2),--float,
    @p_section_id		INT,
    @p_product_code           VARCHAR(10)  , 
	@p_is_straight_order	VARCHAR(1),
	@p_campaign_id	INT,
	@MagPrice_Instance      INT,
	@p_ProvinceCode	VARCHAR(2) 
AS

	declare @ins table (V_TAX_1 numeric(14,6)
						,V_TAX_2 numeric(14,6)
						,V_GROSS numeric(14,6)
						,V_NET numeric(14,6))
	Insert into	@ins exec QSPCanadaCommon.[dbo].[PR_CALC_ORDER_ITEM_AMOUNTS]
					@p_date,
  					@p_amount,
  					@p_section_id,
					@p_product_code,
					@p_is_straight_order,
					@p_campaign_id,
					@MagPrice_Instance,
					@p_ProvinceCode

	Select V_TAX_1 as Tax1, V_TAX_2 as Tax2, V_GROSS as Gross, V_NET as Net from @ins
go
