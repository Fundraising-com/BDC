USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdateCreditCardInformation]    Script Date: 06/07/2017 09:20:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[pr_UpdateCreditCardInformation]
	@CustomerOrderHeaderInstance		int,
	@iTransID				int,
	@iNewCustomerOrderHeaderInstance	int,
	@iCloseOrder				int,
	@CreditCardNumber			varchar(50),
	@ExpDate				datetime,
	@iPaymentMethodInstance		int,
	@authorization_number			varchar(50) = '',
	@iProblemCode				int = 0,
	@iCommunicationChannelInstance	int = 0,
	@iCommunicationSourceInstance	int = 0,
	@lUserProfileID				int,
	@priceToCharge numeric(10,2)
as
	set nocount on
	declare @lAccountID int
	declare @lOrderID int
	declare @batchID int
	declare @coh int
	declare @batchdate datetime
	declare @count int
	declare @orderbatchid int
	declare @tmpDate datetime
	declare @newDate datetime
	declare @studentinstance int
	declare @iNextDetailTransID int
	declare @count_cod int
	declare @sum_cod float
	declare @TotalAmount numeric(10,2)
	declare @lCampaignID int
	declare @lCustomerInstance int
	declare @iOnlineCreditCardBatchCount int
	declare @maxinstance int
	declare @iCreditCardBatchID int
	declare @iPaymentBatchID int
	declare @FileName varchar(200)
	declare @OrderID int

	-- GET THE BATCH DATE
	set @tmpDate = getDate()

	if(@iNewCustomerOrderHeaderInstance = 0)
	BEGIN
		select @batchdate = cast(cast(datepart(YYYY,@tmpDate)as varchar) + '-' + right('0' + cast(datepart(MM,@tmpDate)as varchar),2) + '-' + right('0' + cast(datepart(DD,@tmpDate)as varchar),2) as datetime)
	
		-- GET THE CAMPAIGN ID, CUSTOMER INSTANCE, STUDENT INSTANCE
		select @lCampaignID = CampaignID,@lCustomerInstance=CustomerBillToInstance, @studentinstance=StudentInstance from customerorderheader where instance = @CustomerOrderHeaderInstance
		set @iNextDetailTransID = 2
	
		--GET THE COD TOTALS
		set @count_cod = 1
		/*select	@sum_cod = cod.Price
		from	CustomerOrderDetail cod
		where	CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
		and	TransID = @iTransID*/
	
		-- first grab the account from the campaign
		select @lAccountID=BillToAccountID from qspcanadacommon..campaign where ID=@lCampaignID
	
		--Create a new batch for this CA
		exec CreateBatch @batchdate,
				@lAccountID ,
				@lAccountID ,
				@lCampaignID,
				40002,  -- in process
				41008,  -- group
				39015,  --customer service batch
				@lOrderID OUTPUT
	
		print @lOrderID
		
		select @orderbatchid = id from batch where orderid=@lOrderID
	
		update b 
				set EnterredCount=@count_cod,
					--EnterredAmount=@sum_cod,
					--CalculatedAmount=@sum_cod,
					TeacherCount=1,
					StudentCount=1,
					CustomerCount=1,
					OrderCount=1,
					OrderCountAccept=1,
					OrderDetailCount=0,
					OrderDetailCountError=0,
					ReportedEnvelopes=0,
					MagnetBookletCount=0,
					MagnetCardCount=0,
					MagnetGoodCardCount=0,
					MagnetCardsMailed=0,
					IsStaffOrder=c.IsStaffOrder
				from	Batch b,
					QSPCanadaCommon..Campaign c
				where	c.ID = b.CampaignID
				and	b.orderid=@lOrderID
	
		-- create the order header
		exec CreateOrderHeader
			@batchdate ,
			@orderbatchid ,
			@lAccountID ,
			@lCampaignID ,
			@lCustomerInstance ,
			@coh  OUTPUT,
			@lUserProfileID 
	
		print @coh
		
		update CustomerOrderHeader 
			set	StatusInstance=402,
				FirstStatusInstance=402,
				StudentInstance = @StudentInstance,
				NextDetailTransID = @iNextDetailTransID,
				orderbatchid=@orderbatchid ,
				PaymentMethodInstance = @iPaymentMethodInstance  --added nhamel 2005/4/6
			where instance=@coh
	END
	else
	BEGIN
		-- GET THE CAMPAIGN ID, CUSTOMER INSTANCE, STUDENT INSTANCE
		select @lCampaignID = CampaignID,@lCustomerInstance=CustomerBillToInstance, @studentinstance=StudentInstance, @iNextDetailTransID = NextDetailTransID + 1, @batchdate = OrderBatchDate, @orderbatchid = OrderBatchID from customerorderheader where instance = @iNewCustomerOrderHeaderInstance
	
		--GET THE COD TOTALS
		select	@count_cod = count(codnew.Price) + 1, @sum_cod = cod.Price + sum(codnew.Price)
		from	CustomerOrderDetail cod,
			CustomerOrderDetail codnew
		where	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
		and	cod.TransID = @iTransID
		and	codnew.CustomerOrderHeaderInstance = @iNewCustomerOrderHeaderInstance
		group by cod.Price
	
		-- first grab the account from the campaign
		select @lAccountID=BillToAccountID from qspcanadacommon..campaign where ID=@lCampaignID
	
		select	@lOrderID = OrderId from batch where ID = @orderbatchid and Date = @batchdate
	
		update	Batch 
		set	EnterredCount=@count_cod
			--EnterredAmount=@sum_cod,
			--CalculatedAmount=@sum_cod
		where	id = @orderbatchid
		and	date = @batchdate

		set @coh = @iNewCustomerOrderHeaderInstance
		
		update	CustomerOrderHeader 
		set	NextDetailTransID = @iNextDetailTransID
		where	instance=@coh
	END
	
	-- create the order details 
	select * into #temp_cod from customerorderdetail where customerorderheaderinstance=@CustomerOrderHeaderInstance and TransID = @iTransID
	update #temp_cod set statusinstance=502, customerorderheaderinstance=@coh, TransID = @iNextDetailTransID - 1, CreationDate = getdate()

	DECLARE @OriginalPrice NUMERIC(10,2)
	SELECT @OriginalPrice = ISNULL(Price, 0.00) FROM #temp_cod
	IF (ISNULL(@PriceToCharge, 0.00) <> @OriginalPrice)
	BEGIN
		CREATE TABLE #taxes
		(
			Tax1				NUMERIC(14, 6),
			Tax2				NUMERIC(14, 6),
			Gross				NUMERIC(14, 6),
			Net					NUMERIC(14, 6),
			GroupProfitAmount	NUMERIC(14, 6)
		)

		DECLARE @ProgramSectionID	INT,
				@PricingDetailsID	INT,
				@ProductCode		VARCHAR(20)

		SELECT	@ProgramSectionID = pd.ProgramSectionID,
				@PricingDetailsID = pd.MagPrice_Instance,
				@ProductCode = prod.Product_Code
		FROM	QSPCanadaProduct..Pricing_Details pd
		JOIN	QSPCanadaProduct..Product prod
					ON	prod.Product_Instance = pd.Product_Instance
		JOIN	CustomerOrderDetail cod
					ON	cod.PricingDetailsID = pd.MagPrice_Instance
		WHERE	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
		AND		cod.TransID = @iTransID

		DECLARE	@OrderDateStr			VARCHAR(20),
				@Tax					NUMERIC(14, 6),
				@TaxA					NUMERIC(14, 6),
				@Net					NUMERIC(14, 6),
				@Gross					NUMERIC(14, 6),
				@GroupProfitAmount		NUMERIC(14, 6),
				@CampaignID				INT,
				@Province				VARCHAR(2),
				@customershiptoinstance INT,
				@PaidPrice				NUMERIC(10, 2)
				

		SELECT	@OrderDateStr			= CONVERT(VARCHAR(20), coh.OrderBatchDate, 101),
				@CampaignID				= camp.ID,
				@customershiptoinstance = cod.CustomerShipToInstance,
				@PaidPrice = @PriceToCharge
		FROM	CustomerOrderDetail cod
		JOIN	CustomerOrderHeader coh
					ON	coh.Instance = cod.CustomerOrderHeaderInstance
		JOIN	Batch b
					ON	b.ID = coh.OrderBatchID
					AND	b.Date = coh.OrderBatchDate
		JOIN	QSPCanadaCommon..Campaign camp
					ON	camp.ID = b.CampaignID
		WHERE	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
		AND		cod.TransID = @iTransID

		IF (@customershiptoinstance > 0)
		BEGIN
			SELECT	@Province = State
			FROM	Customer
			WHERE	Instance = @customershiptoinstance
		END

		IF (ISNULL(@Province, '') = '')
		BEGIN
			SELECT	@Province = State
			FROM	CustomerOrderHeader coh
			JOIN	Customer cust
						ON	cust.Instance = coh.CustomerBillToInstance
			WHERE	coh.Instance = @CustomerOrderHeaderInstance
		END
			
		INSERT INTO #Taxes
		EXEC QSPCanadaCommon..pr_Calc_Order_Item_Amounts
				@OrderDateStr, @PaidPrice, @ProgramSectionID, 
				@ProductCode, 'N', @CampaignID, @PricingDetailsID, @Province, @CustomerOrderHeaderInstance

		SELECT	@Tax = Tax1,
				@TaxA = Tax2,
				@Net = Net,
				@Gross = Gross,
				@GroupProfitAmount = GroupProfitAmount
		FROM	#Taxes
		
		DROP TABLE #Taxes

		UPDATE	#temp_cod
		SET		Tax = @Tax,
				TaxA = @Tax,
				Tax2 = @TaxA,
				Tax2A = @TaxA,
				Net = @Net,
				Gross = @Gross,
				Price = @PriceToCharge,
				PriceA = @priceToCharge,
				GroupProfitAmount = @GroupProfitAmount
	END

	--If pid = 0, attempt to find the correct pid
	DECLARE @PricingDetailsID2 INT
	SELECT	@PricingDetailsID2 = PricingDetailsID FROM #temp_cod
	IF ISNULL(@PricingDetailsID2, 0) = 0
	BEGIN
		/*********************** get current season ******************************/
		DECLARE 	@ProductSeason 	char(1)
		DECLARE		@ProductYear	int

		EXEC		pr_RemitTest_GetCurrentSeason @ProductSeason output, @ProductYear output
		/*************************************************************************/

		DECLARE	@Lang			VARCHAR(10),
				@Prov			VARCHAR(10),
				@TaxRegionID	INT

		SELECT	@Lang = camp.Lang,
				@Prov = [add].StateProvince,
				@TaxRegionID = trp.TaxRegionID
		FROM	QSPCanadaCommon..Campaign camp
		JOIN	QSPCanadaCommon..CAccount acc
					ON	acc.ID = camp.ShipToAccountID
		JOIN	QSPCanadaCommon..AddressList al
					ON	al.ID = acc.AddressListID
		JOIN	QSPCanadaCommon..Address [add]
					ON	[add].AddressListID = al.ID
					AND	[add].Address_Type = 54001 -- SHIP TO Address Type
		JOIN	QSPCanadaCommon..TaxRegionProvince trp
					ON	trp.Province = [add].StateProvince
		WHERE	camp.ID = @lCampaignID

		UPDATE	tempcod
		SET		PricingDetailsID = pd.MagPrice_Instance
		FROM	#temp_cod tempcod
		JOIN	QSPCanadaProduct..Pricing_Details pd
					ON	pd.Product_Code = tempcod.ProductCode
					AND	pd.Pricing_Year = @ProductYear
					AND	pd.Pricing_Season = @ProductSeason
					AND	pd.TaxRegionID = @TaxRegionID
	END

	insert into customerorderdetail select * from #temp_cod

	-- Insert incident and action - Added by Ben 2005/04/25
	if(@iProblemCode <> 0)
	begin
		DECLARE	@iInstancePC int,
		    		@iIncidentInstance int
	
		exec pr_Incident_Insert @iIncidentInstance out, @iProblemCode, @CustomerOrderHeaderInstance, @iTransID, @iCommunicationChannelInstance, @iCommunicationSourceInstance,1,0,'Automated Update CC', @lUserProfileID
		exec pr_IncidentAction_Insert @iInstancePC out, @iIncidentInstance, 18, 'Automated Update CC', @lUserProfileID
	end

	-- set the customer to valid
	update Customer set statusinstance=300 where instance = @lCustomerInstance

	--Update Payment Method, Payment Header, etc.
	if(@iCloseOrder = 1)
	BEGIN
		select	@TotalAmount = CASE c.IsStaffOrder WHEN 0 THEN sum(cod.Price) ELSE sum(cod.Price)/* / 2*/ END
		from	CustomerOrderDetail cod,
			CustomerOrderHeader coh,
			QSPCanadaCommon..Campaign c
		where	cod.CustomerOrderHeaderInstance = @coh
		and	coh.Instance = @coh
		and	c.ID = coh.CampaignID
		group by
			c.IsStaffOrder

		select	@iOnlineCreditCardBatchCount = count(*)
		from	CreditCardBatch
		where	Type = 58002
		and	IsGLDone != 1
	
		if (@iOnlineCreditCardBatchCount = 0)
		BEGIN
			/*create table #temp
			(
				 NextInstance int
			)
		
		
			insert into #temp exec qspcanadaordermanagement..InsertNextInstance 8
			select @maxinstance=nextinstance from #temp
			truncate table #temp
	
			drop table #temp*/
			
			SELECT	@maxinstance = MAX(ID) + 1
			FROM	CreditCardBatch
	
			set @FileName = 'Online_' + cast(datepart(yyyy, @tmpDate) as varchar) + '_' + cast(datepart(mm, @tmpDate) as varchar) + '_' + cast(datepart(dd, @tmpDate) as varchar) + '_' + cast(datepart(hh, @tmpDate) as varchar) + '_' + cast(datepart(mi, @tmpDate) as varchar) + '_' + cast(datepart(ss, @tmpDate) as varchar)
	
			insert into CreditCardBatch
			(InputFileName,
			OutputFileName,
			StartImportTime,
			EndImportTime,
			Status,
			TotalRecordCount,
			TotalDollarAmount,
			ID,
			DateCreated,
			UserIDCreated,
	
			ChangeDate,
			ChangeUserID,
			Type,
			IsGLDone)
			values
			(@FileName,
			@FileName,
			@tmpDate,
			@tmpDate,
			40001,
			0,
			0,
			@maxinstance,
			@tmpDate,
			'ADMI',
			@tmpDate,
			'ADMI',
			58002,
			0)
	
			set @iCreditCardBatchID = @maxinstance
		END
		else
		BEGIN
			select top 1 @iCreditCardBatchID = ID
			from	CreditCardBatch
			where	Type = 58002
			and	IsGLDone != 1
		END
	
		
		create table #temp2
		(
			Date datetime,
			NextInstance int
		)
		
		
		insert into #temp2 exec qspcanadaordermanagement..sp_GetMaxPaymentID @tmpDate
		select @newDate=Date from #temp2
		select @maxinstance=nextinstance from #temp2
		truncate table #temp2
	
		drop table #temp2
	
		insert into PaymentBatch
		(PaymentDate,
		PaymentID,
		DepositDate,
		DepositID,
		EnterredAmount,
		EnterredCount,
		CalculatedAmount,
		CalculatedCount,
		StatusInstance,
		Clerk,
		FileName,
		StartImportTime,
		EndImportTime,
		DateCreated,
		UserIDCreated,
		DateChanged,
		UserIDChanged,
		IsDirty,
		DirtyStatus,
		IsCreditCard)
		values
		(@newDate,
		@maxinstance,
		'01/01/1995',
		0,
		0,
		0,
		0,
		0,
		40001,
		null,
		null,
		'01/01/1995',
		'01/01/1995',
		getdate(),
		'ADMI',
		getdate(),
		'ADMI',
		0,
		0,
		1)
		set @iPaymentBatchID = @maxinstance
	
		/*create table #temp3
		(
			 NextInstance int
		)
	
		insert into #temp3 exec qspcanadaordermanagement..InsertNextInstance 6
		select @maxinstance=nextinstance + 100000000 from #temp3
		truncate table #temp3

		drop table #temp3*/

		SELECT	@maxinstance = MAX(Instance) + 1
		FROM	CustomerPaymentHeader

		insert into CustomerPaymentHeader
		(Instance,
		CustomerOrderHeaderInstance,
		InvoiceNumber,
		PaymentBatchDate,
		PaymentBatchID,
		PaymentBatchSequence,
		NextDetailTransID,
		TotalAmount,
		DateCreated,
		UserIDCreated,
		DateChanged,
		UserIDChanged,
		StatusInstance,
		IsCreditCard)
		values
		(@maxinstance,
		@coh,
		0,
		@newDate,
		@iPaymentBatchID,
		1,
		1,
		@TotalAmount,
		@tmpDate,

		'ADMI',
		@tmpDate,
		'ADMI',

		600,
		1)

		insert CustomerPaymentDetail
			(CustomerPaymentHeaderInstance,
			TransID,
			CustomerOrderDetailTransID,
			Amount)
		select	@maxinstance,
			cod.TransID - 1,
			cod.TransID,
			CASE c.IsStaffOrder WHEN 0 THEN cod.Price ELSE cod.Price/* / 2*/ END
		from	CustomerOrderDetail cod,
			CustomerOrderHeader coh,
			QSPCanadaCommon..Campaign c
		where	cod.CustomerOrderHeaderInstance = @coh
		and	coh.Instance = @coh
		and	c.ID = coh.CampaignID
	
		insert into CreditCardPayment
		(CustomerPaymentHeaderInstance,
		CreditCardNumber,
		ExpirationDate,
		ReasonCode,
		AuthorizationSource,
		AuthorizationCode,
		AuthorizationDate,
		AVSResponseCode,
		StatusInstance,
		DateCreated,
		UserIDCreated,
		DateChanged,
		UserIDChanged,
		BatchID,
		VeriSignID)
		 values
		(@maxinstance,
		@CreditCardNumber,
		CASE WHEN len(cast(datepart(MM, @ExpDate) as varchar)) = 2 THEN cast(datepart(MM, @ExpDate) as varchar) ELSE '0' + cast(datepart(MM, @ExpDate) as varchar) END + substring(cast(datepart(YY, @ExpDate) as varchar), 3, 2),
		20100,
		null,
		@authorization_number,
		@tmpDate,
		null,
		19000,
		@tmpDate,
		'ADMI',
		@tmpDate,
		'ADMI',
		@iCreditCardBatchID,
		null)
	
	
		update	CustomerOrderHeader
		set	PaymentMethodInstance = @iPaymentMethodInstance
		where	Instance = @coh
		
	/*
	If no creditcardbatch for online available then (Note: we will; add a status to qualifer a credit card batch ....see if its online or not)
	Create a record in CreditCardBatch - Filename should be Online_YYYYMMDDMMSSMM
	else
	 select cc bartch id
	end
	
	Add records in the following tables:
	CreditCardPayment
	CustomerPaymentHeader
	
	update CustomerOrderHeader with CPH Instance
	
	*/
	
		--sp_columns 'Batch'

		select @coh
		--exec pr_ForceCloseOrder @lOrderID


		--get the Main Order #
		Select top 1 @OrderID = batch.OrderID
		 From 	QSPCanadaOrderManagement.dbo.customerorderheader coh,
			QSPCanadaOrderManagement.dbo.batch batch
		 Where batch.id = coh.orderbatchid
		     and batch.date = coh.orderbatchdate
		     and coh.Instance = @CustomerOrderHeaderInstance 

		--saqib - 16 march 06 - re-generate the fresh OE for the main order so it elminates the current sub from it  
		 Update  QspCanadaOrderManagement.dbo.[ReportRequestBatch_OrderEntryFollowupReport] 
		 Set createdate = getdate(), QUEUEDATE = NULL,RUNDATESTART= NULL,FILENAME =NULL 
		 where reportrequestbatchid  IN  ( select id from QspCanadaOrderManagement.dbo.[ReportRequestBatch]
		 where batchorderid  = @OrderID     )  
	END

	select @coh
GO
