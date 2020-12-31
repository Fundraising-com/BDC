USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ProductReplacement]    Script Date: 06/07/2017 09:20:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[pr_ProductReplacement]
	@lCampaignID int,
	@lProductpriceInstance int,
	@IQuantity int,
	@dPrice float = 0,
	@lOverrideCode int = '45005',
	@zProductType varchar(10),
	@lUserProfileID int,
	@zStudentFirstName varchar(50) = 'ZZ',
	@zStudentLastName varchar(50) = 'ZZ',
	@zComment varchar(300) = '',
	@lOrderID int OUT
as
	set nocount on
	declare @lAccountID int
	declare @batchID int
	declare @teacherinstance int
	declare @studentinstance int
	declare @coh int
	declare @batchdate datetime
	declare @billtoacctid int
	declare @productcode varchar(20)
	declare @programsectionid int
	declare @catalogprice float
	declare @count int
	declare @orderbatchid int
	declare @tmpDate datetime
	declare @lCustomerInstance int
	declare 	@sFirstName nvarchar(50)
	declare @sLastName nvarchar(50)

	declare @iOrderQualifierID int

	set @sFirstName = 'Item'
	set @sLastName = 'Replacement'

	set @tmpDate = getDate()
	select @batchdate = cast(cast(datepart(YYYY,@tmpDate)as varchar) + '-' + right('0' + cast(datepart(MM,@tmpDate)as varchar),2) + '-' + right('0' + cast(datepart(DD,@tmpDate)as varchar),2) as datetime)

	select @iOrderQualifierID =	CASE WHEN @zProductType IN ('46002', '46003')
					THEN	39019	-- gift problem solver
					ELSE	39018	-- kanata problem solver
					END

	-- first grab the account from the campaign
	select @lAccountID=BillToAccountID from qspcanadacommon..campaign where ID=@lCampaignID

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
		@zStudentFirstName,
		@zStudentLastName,
		@studentinstance  OUTPUT

	exec CreateBatch @batchdate,
			@lAccountID ,
			@lAccountID ,
			@lCampaignID,
			40002 ,  -- in process
			41008 ,   -- group
			@iOrderQualifierID,
			@lOrderID OUTPUT

	print @lOrderID

	select @orderbatchid = id from batch where orderid=@lOrderID

	--Create dummy customer

	exec CreateCustomerAccount 	@lAccountID,
					@lUserProfileID,
					@lCustomerInstance  OUTPUT
	
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


---------------------------------TO CHANGE----------------------------------------------------------------------------------

	select @productcode=Product_code,
		@programsectionid=ProgramSectionID,
		@catalogprice=qsp_price
		from qspcanadaproduct..pricing_details
		where magprice_instance=@lProductpriceInstance
-------------------------------------------------------------------------------------------------------------------

	declare @today smalldatetime
	select @today = GetDate()	
	exec CreateDetailItem
		 @today,
		@coh ,
		@productcode ,
		@sFirstName,
		@sLastName,
		@IQuantity ,
		@dPrice,
		@programsectionid ,
		@catalogprice ,
		0 ,
		@zProductType ,
		@lProductpriceInstance ,
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
			--IsStaffOrder=0,  MS March 13, 2008
			Comment = @zComment
		where orderid=@lOrderID

	update CustomerOrderDetail Set Renewal = 'N', PriceOverRideID = @lOverrideCode
		where CustomerOrderHeaderInstance=@coh

	--exec pr_ForceCloseOrder @lOrderID
GO
