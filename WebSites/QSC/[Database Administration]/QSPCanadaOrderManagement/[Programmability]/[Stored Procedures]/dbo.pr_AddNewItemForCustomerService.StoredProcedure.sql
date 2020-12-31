USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_AddNewItemForCustomerService]    Script Date: 06/07/2017 09:19:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE[dbo].[pr_AddNewItemForCustomerService]
	@lCampaignID 				int,
	@lProductpriceInstance 			int,
	@zNewRenewal 			varchar(1),
	@dPrice 				float,
	@lOverrideCode 			int,
	@lCustomerInstance 			int,
	@zProductType 			varchar(10),
	@lUserProfileID 				int,
	@sFirstName 				nvarchar(50)	= '',
	@sLastName 				nvarchar(50)	= '',
	@sAddress1				nvarchar(50)	= '',
	@sAddress2				nvarchar(50)	= '',
	@sCity					nvarchar(50)	= '',
	@sStateCode				nvarchar(5)	= '',
	@sZip					nvarchar(20)	= '',
	@iOrderQualifierID 			int		= 39008,
	@iOldCustomerOrderHeaderInstance	int		= 0,
	@iOldTransID				int		= 0
as
	set nocount on
	set xact_abort on

	declare @lAccountID int
	declare @lOrderID int
	declare @batchID int
	declare @teacherinstance int
	declare @studentinstance int
	declare @coh int
	declare @batchdate datetime
	declare @billtoacctid int
	declare @productcode varchar(20)
	declare @programsectionid int
	declare @quantity int
	declare @catalogprice float
	declare @count int
	declare @orderbatchid int
	declare @tmpDate datetime
	declare @iCreateNewCustomer int
	declare @iCustomerType int
	declare @OrderID int
	declare @IsStaffOrder int
	declare @ShippingFee money

	set @tmpDate = getDate()
	select @batchdate = cast(cast(datepart(YYYY,@tmpDate)as varchar) + '-' + right('0' + cast(datepart(MM,@tmpDate)as varchar),2) + '-' + right('0' + cast(datepart(DD,@tmpDate)as varchar),2) as datetime)

	-- first grab the account from the campaign
	--Added  IsstaffOrder according to Campaign.IsStaffOrder MS Nov 28, 2006
	select @lAccountID=BillToAccountID ,
	           @IsStaffOrder = IsStaffOrder	
	from qspcanadacommon..campaign where ID=@lCampaignID

	select @count=count(*) from teacher
		where accountid = @lAccountID
			and lastname='ZZ' and classroom='ZZ'

	BEGIN TRAN tAddNewItem

	if(@count <> 0)
	begin
		select @teacherinstance=instance from teacher
		where accountid = @lAccountID
			and lastname='ZZ' and classroom='ZZ'
	end
	else
	begin		
		exec CreateTeacher
			@lAccountID ,
			'ZZ',
			'ZZ',
			'ZZ',
			'ZZ',
			'ZZ',
			@teacherinstance  OUTPUT
	end

	exec CreateStudent
		@teacherinstance ,
		'ZZ',
		'ZZ',
		@studentinstance  OUTPUT

	exec CreateBatch @batchdate,
			@lAccountID ,
			@lAccountID ,
			@lCampaignID,
			40002 ,  -- in process
			41008 ,   -- group
			@iOrderQualifierID,  --customer service batch
			@lOrderID OUTPUT

print @lOrderID
	select @orderbatchid = id from batch where orderid=@lOrderID

	if(@iOrderQualifierID = 39012)
	begin
		set @iCreateNewCustomer = 0
	end
	else if(@lCustomerInstance <> 0)
	begin
		select	@iCustomerType = Type
		from	Customer
		where	Instance = @lCustomerInstance

		if(@iCustomerType = 50605 or @iCustomerType = 50606 or @iCustomerType = 50607 or @iCustomerType = 50608) -- Gift Certificate
		begin
			set @iCreateNewCustomer = 0
		end
		else
		begin
			set @iCreateNewCustomer = 1
		end
	end
	else
	begin
		set @iCreateNewCustomer = 1
	end


	if(@iCreateNewCustomer = 1)
	begin

		declare @maxinstance int
		create table #temp
		(
			 NextInstance int
		)
		
		insert into #temp exec qspcanadaordermanagement..InsertNextInstance 3
		select @maxinstance=nextinstance from #temp
		truncate table #temp
		
		drop table #temp

		insert into Customer
			(Instance,
				StatusInstance,
				LastName,
				FirstName,
				Address1,
				Address2,
				City,
				County,
				State,
				Zip,
				ZipPlusFour,
				OverrideAddress,
				ChangeUserID,
				ChangeDate,
				Email,
				Phone,
				Type)
			values(@maxinstance,
				300, -- make it good
				@sLastName,
				@sFirstName,
				@sAddress1,
				@sAddress2,
				@sCity,
				'',
				@sStateCode,
				@sZip,
				'',
				'', 
				@lUserProfileID,
				getDate(),
				'',
				'',
				50601)
		set @lCustomerInstance = @maxinstance
	end

	exec CreateOrderHeader
		@batchdate ,
		@orderbatchid ,
		@lAccountID ,
		@lCampaignID ,
		@lCustomerInstance ,
		@coh  OUTPUT,
		@lUserProfileID 

print @coh
	--if(@iOrderQualifierID = 39002)
	if(@iOrderQualifierID = 39020 OR @iOrderQualifierID = 39008)  -- Ben 01/25/2006: New Customer Service to invoice replaces supplement --Jeff: 04/27/07-payment should be 50002 for Cust Service orders too
	BEGIN
		update CustomerOrderHeader 
			set	StudentInstance=@studentinstance,
				PaymentMethodInstance = 50002,
				orderbatchid=@orderbatchid where instance=@coh
	END
	else
	BEGIN
		update CustomerOrderHeader 
			set	StudentInstance=@studentinstance,
				orderbatchid=@orderbatchid where instance=@coh
	END

--If pid = 0, attempt to find the correct pid
IF ISNULL(@lProductPriceInstance, 0) = 0
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

	SELECT	@lProductPriceInstance = pd.MagPrice_Instance
	FROM	CustomerOrderDetail cod
	JOIN	QSPCanadaProduct..Pricing_Details pd
				ON	pd.Product_Code = cod.ProductCode
				AND	pd.Pricing_Year = @ProductYear
				--AND	pd.Pricing_Season = @ProductSeason
				AND	pd.TaxRegionID = @TaxRegionID
	WHERE	cod.CustomerOrderHeaderInstance = @iOldCustomerOrderHeaderInstance
	AND		cod.TransID = @iOldTransID
END


	SELECT	@productcode = pd.Product_code,
			@quantity = CASE @zProductType WHEN 46001 THEN pd.Nbr_of_Issues ELSE 1 END,
			@programsectionid = pd.ProgramSectionID,
			@catalogprice = pd.QSP_Price,
			@ShippingFee = pd.AddlHandlingFee
	FROM	QSPCanadaProduct..PRICING_DETAILS pd
	WHERE	pd.MagPrice_Instance = @lProductpriceInstance

	IF (ISNULL(@dPrice, 0.00) = 0.00)
	BEGIN
		select @dPrice = qsp_price
		from qspcanadaproduct..pricing_details
		where magprice_instance=@lProductpriceInstance		
	END

	declare @today smalldatetime
	select @today = GetDate()	
	exec CreateDetailItem
		 @today,
		@coh ,
		@productcode ,
		@sFirstName,
		@sLastName,
		@quantity ,
		@dPrice,
		@programsectionid ,
		@catalogprice ,
		0 ,
		@zProductType ,
		@lProductpriceInstance ,
		502,
		0

	IF (@ShippingFee > 0.00)
	BEGIN
		DECLARE @ShippingProductCode varchar(20),
				@ShippingProgramSectionID int,
				@ShippingProductType int,
				@ShippingProductpriceInstance int
				
		SET @ShippingProductType = 46021
		
		SELECT	@ShippingProductCode = p.Product_Code,
				@ShippingProgramSectionID = pd.ProgramSectionID,
				@ShippingProductpriceInstance = pd.MagPrice_Instance
		FROM	QSPCanadaProduct..Product p
		JOIN	QSPCanadaProduct..PRICING_DETAILS pd ON pd.Product_Instance = p.Product_Instance
		WHERE	p.Type = @ShippingProductType
		AND		pd.QSP_Price = @ShippingFee
	
		exec CreateDetailItem
				@today,
				@coh ,
				@ShippingProductCode,
				@sFirstName,
				@sLastName,
				@quantity,
				@ShippingFee,
				@ShippingProgramSectionID,
				@ShippingFee,
				0,
				@ShippingProductType,
				@ShippingProductpriceInstance,
				502,
				0
	END
	
--sp_columns 'Batch'
	update Batch 
		set EnterredCount=1,
			EnterredAmount=@dPrice,
			CalculatedAmount=@dPrice,
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
			IsStaffOrder= @IsStaffOrder 
			--IsStaffOrder=0  --MS Nov 28, 2006 should be consistant with ca IsStaffOrder Flag
		where orderid=@lOrderID

	update CustomerOrderDetail Set Renewal = @zNewRenewal
		where CustomerOrderHeaderInstance=@coh

	update	Customer
	set	StatusInstance = 300
	where	Instance = @lCustomerInstance
	--exec pr_ForceCloseOrder @lOrderID

	--saqib - 28 march 06 - re-generate the fresh OE for the main order so it elminates the current sub from it  
	IF  @iOrderQualifierID = 39020
	  Begin
		--get the Main Order #
		Select top 1 @OrderID = batch.OrderID
		 From 	QSPCanadaOrderManagement.dbo.customerorderheader coh,
			QSPCanadaOrderManagement.dbo.batch batch
		 Where batch.id = coh.orderbatchid
		     and batch.date = coh.orderbatchdate
		     and coh.Instance = @iOldCustomerOrderHeaderInstance 

		 Update  QspCanadaOrderManagement.dbo.[ReportRequestBatch_OrderEntryFollowupReport] 
		 Set createdate = getdate(), QUEUEDATE = NULL,RUNDATESTART= NULL,FILENAME =NULL 
		 where reportrequestbatchid  IN  ( select id from QspCanadaOrderManagement.dbo.[ReportRequestBatch]
		 where batchorderid  = @OrderID     )  
	  End


	COMMIT TRAN tAddNewItem

	set xact_abort off
	set nocount off
GO
