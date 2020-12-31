USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_AddNewSubForCustomerService_CourtesyBatch]    Script Date: 06/07/2017 09:19:43 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  procedure [dbo].[pr_AddNewSubForCustomerService_CourtesyBatch]
	@lCampaignID int,
	@lMagpriceInstance int,
	@zNewRenewal varchar(1),
	@dPrice float,
	@lOverrideCode int,
	@lCustomerInstance int,
	@lUserProfileID int,
	@sFirstName 				nvarchar(50)	= '',
	@sLastName 				nvarchar(50)	= '',
	@sAddress1				nvarchar(50)	= '',
	@sAddress2				nvarchar(50)	= '',
	@sCity					nvarchar(50)	= '',
	@sStateCode				nvarchar(5)	= '',
	@sZip					nvarchar(20)	= ''
as
	set nocount on
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
	declare @customertype int
	declare @batchtype varchar(15)
	declare @IsStaffOrder int

	select @customertype = type from customer where instance = @lCustomerInstance

	
	set @batchtype='39014'

	set @tmpDate = getDate()
	select @batchdate = cast(cast(datepart(YYYY,@tmpDate)as varchar) + '-' + right('0' + cast(datepart(MM,@tmpDate)as varchar),2) + '-' + right('0' + cast(datepart(DD,@tmpDate)as varchar),2) as datetime)

	-- first grab the account from the campaign
	select @lAccountID=BillToAccountID, 
		@IsStaffOrder=IsStaffOrder
	from qspcanadacommon..campaign where ID=@lCampaignID

	select @count=count(*) from teacher
		where accountid = @lAccountID
			and lastname='ZZ' and classroom='ZZ'

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
			@batchtype,  --customer service batch
			@lOrderID OUTPUT
print @lOrderID
	select @orderbatchid = id from batch where orderid=@lOrderID
	
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
		set StudentInstance=@studentinstance,
			orderbatchid=@orderbatchid where instance=@coh

--sp_columns 'CustomerOrderDetail'	

	select @productcode=Product_code,
		@quantity=Nbr_of_Issues,
		@programsectionid=ProgramSectionID,
		@catalogprice=qsp_price
		from qspcanadaproduct..pricing_details
		where magprice_instance=@lMagpriceInstance
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
		46001 ,
		@lMagpriceInstance ,
		502 
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
			IsStaffOrder=@IsStaffOrder
		where orderid=@lOrderID

	update CustomerOrderDetail Set Renewal = @zNewRenewal
		where CustomerOrderHeaderInstance=@coh

	exec DoCloseOrder @lOrderID,1
GO
