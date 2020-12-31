USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[preDDS_HomeRoomSummaryReportDetail]    Script Date: 06/07/2017 09:20:43 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE   PROCEDURE [dbo].[preDDS_HomeRoomSummaryReportDetail] @OrderId 	Int,
						    @BatchId 	Int,
						    @BatchDate	DateTime
						
 AS
	Set NoCount On

	Declare	@MaxRowCnt 		Int,
		@MaxRowCnt1		Int,
		@StudentInstance	Int,
		@StudentInstanceWeb   Int,
		@MagQuantity 		Int, 
		@MagTotalPrice	Numeric (10,2) , 
		@StudentLastName	Varchar(30), 
		@StudentFirstName	Varchar(30),  
		@TeacherLastName	Varchar(30), 
		@TeacherFirstName	Varchar(30),  
		@Classroom		Varchar(10),
		@CampaignId		Int,
		@Seq			Int,
		@OldClassRoom      	Varchar(10),
		@TotalGiftForClass	Int,
		@TotalMagForClass 	Int,
		@TotalGiftAmtForClass	Numeric(10,2),
		@TotalMagAmtForClass	Numeric(10,2),
		@TotalRDForClass	Int,
		@TotalRogerForClass	Int,
		@TotalLvlAForClass	Int,
		@TotalLvlBForClass	Int,
		@TotalLvlCForClass	Int,
		@TotalLvlDForClass	Int,
		@TotalLvlEForClass	Int,
		@TotalLvlFForClass	Int,
		@TotalLvlGForClass	Int,
		@TIndex		Int,
		@GiftQuantity		Int,
		@GiftPrice		Numeric(10,2),
		--@MagQuantity		Int,
		@MagPrice		Numeric(10,2),
		@RDPremium		Int,
		@RogerPremium	Int,
		@LevelA		Int,
		@LevelB		Int,
		@LevelC		Int,
		@LevelD		Int,
		@LevelE		Int,
		@LevelF		Int,
		@LevelG		Int,
		@IncentiveProgramId	Int,
		@InterNet		Varchar(1),
		@OrderQualifierId		Int

	Declare	@TabClass Table(
			Tindex			Int Identity,			
			Classroom		Varchar(10)
				          ) 

	Declare @TabStudent Table(
			 TIndex			Int Identity,
			 StudentInstance   	Int,
			 Internet		    	Varchar(1)		
			 )

	Declare @TabGroupSummaryReportDetail Table(
			TIndex		   	Int Identity,
			Quantity 		Int,
			TotalPrice 		Numeric(10,2), 
			ProductType 		Int,
			ProductCode 		Varchar(20),
			TeacherFirstName  	Varchar(50), 
			TeacherLastName 	Varchar(50), 
			Instance 		Int,
			Classroom 		Varchar(10), 
			CampaignID 		Int, 
			OrderID 		Int, 
			BatchId 		Int,
			BatchDate 		dateTime, 
			Premium_Code 		Varchar(50) ,
			PremiumCount 		Int, 
			IncentiveCount 		Int,
			CustomerHeaderInstance Int,
			LastName 		Varchar(50) ,
			FirstName 		Varchar(50) ,
			StudentInstance 	Int,
			OrderQualifierId 		Int

			 )

	Declare @TabStudentInternet Table(
			 TIndex		   	Int Identity,
			 StudentInstance	Int
			 )

	Declare @TabProductTotals Table(
		      Tindex			Int Identity,
		      StudentInstance		Int, 
		      Quantity 			Int, 
		      TotalPrice			Numeric(10,2) , 
		      TeacherLastName		Varchar(30), 
		      TeacherFirstName		Varchar(30), 
		      StudentLastName		Varchar(30), 
		      StudentFirstName		Varchar(30), 
		      Classroom			Varchar(10),
		      Producttype			Varchar(10),
		      ProductCode		Varchar(10),
		      CampaignId			Int,
		      OrderQualifierId		Int,
		      OrderId			Int,
		      BatchId			Int,
		      BatchDate			DateTime
		    )	

	Declare @TabOutPut Table (
 			TIndex			Int,
			CampaignId		Int,
			OrderQualifierId		Int,
			OrderId			Int,
			StudentInstance		Int, 
			StudentLastName	Varchar(30), 
		     	StudentFirstName	Varchar(30), 
			ClassRoom		Varchar(10),
			TeacherLastName 	Varchar(30),
			TeacherFirstName  	Varchar(30),
			GiftQuantity		Int,
			GiftPrice		Numeric(10,2),
			MagQuantity		Int,
			MagPrice		Numeric(10,2),
			RDPremium		Int,
			RogerPremium		Int,
			LevelA			Int,
			LevelB			Int,
			LevelC			Int,
			LevelD			Int,
			LevelE			Int,
			LevelF			Int,
			LevelG			Int
				                  )

	Declare @TabOutPutSorted Table (
 			TIndex			Int Identity,  
			CampaignId		Int,
			 OrderQualifierId		Int,
			OrderId			Int,
			StudentInstance		Int, 
			StudentLastName	Varchar(30), 
		     	StudentFirstName	Varchar(30), 
			ClassRoom		Varchar(10),
			TeacherLastName 	Varchar(30),
			TeacherFirstName  	Varchar(30),
			GiftQuantity		Int,
			GiftPrice		Numeric(10,2),
			MagQuantity		Int,
			MagPrice		Numeric(10,2),
			RDPremium		Int,
			RogerPremium		Int,
			LevelA			Int,
			LevelB			Int,
			LevelC			Int,
			LevelD			Int,
			LevelE			Int,
			LevelF			Int,
			LevelG			Int
				                  )


	Declare @TabOutPut1 Table (
 			TIndex			Int,
			CampaignId		Int,
			OrderQualifierId		Int,
			OrderId			Int,
			StudentInstance		Int, 
			StudentLastName	Varchar(30), 
		     	StudentFirstName	Varchar(30), 
			ClassRoom		Varchar(10),
			TeacherLastName 	Varchar(30),
			TeacherFirstName  	Varchar(30),
			GiftQuantity		Int,
			GiftPrice		Numeric(10,2),
			MagQuantity		Int,
			MagPrice		Numeric(10,2),
			RDPremium		Int,
			RogerPremium		Int,
			LevelA			Int,
			LevelB			Int,
			LevelC			Int,
			LevelD			Int,
			LevelE			Int,
			LevelF			Int,
			LevelG			Int,
			TotalGiftForClass	Int,
			TotalMagForClass 	Int,
			TotalGiftAmtForClass	Numeric(10,2),
			TotalMagAmtForClass	Numeric(10,2),
			TotalRDForClass	Int,
			TotalRogerForClass	Int,
			TotalLvlAForClass	Int,
			TotalLvlBForClass	Int,
			TotalLvlCForClass	Int,
			TotalLvlDForClass	Int,
			TotalLvlEForClass	Int,
			TotalLvlFForClass	Int,
			TotalLvlGForClass	Int
				                  )


	Declare @TabOutPut1Sorted Table (
 			TIndex			Int Identity, 
			CampaignId		Int,
			OrderQualifierId		Int,
			OrderId			Int,
			StudentInstance		Int, 
			StudentLastName	Varchar(30), 
		     	StudentFirstName	Varchar(30), 
			ClassRoom		Varchar(10),
			TeacherLastName 	Varchar(30),
			TeacherFirstName  	Varchar(30),
			GiftQuantity		Int,
			GiftPrice		Numeric(10,2),
			MagQuantity		Int,
			MagPrice		Numeric(10,2),
			RDPremium		Int,
			RogerPremium		Int,
			LevelA			Int,
			LevelB			Int,
			LevelC			Int,
			LevelD			Int,
			LevelE			Int,
			LevelF			Int,
			LevelG			Int,
			TotalGiftForClass	Int,
			TotalMagForClass 	Int,
			TotalGiftAmtForClass	Numeric(10,2),
			TotalMagAmtForClass	Numeric(10,2),
			TotalRDForClass	Int,
			TotalRogerForClass	Int,
			TotalLvlAForClass	Int,
			TotalLvlBForClass	Int,
			TotalLvlCForClass	Int,
			TotalLvlDForClass	Int,
			TotalLvlEForClass	Int,
			TotalLvlFForClass	Int,
			TotalLvlGForClass	Int
				                  )
	--Populate the TabGroupSummaryDetail with Order Info
	Insert into @TabGroupSummaryReportDetail
	Select * from dbo.Udf_GroupSummaryReportDetail(@OrderId,@BatchId,@BatchDate)

	-- Insert All teachers
	Insert Into @TabStudent
	Select Distinct StudentInstance, Null
	From @TabGroupSummaryReportDetail
	

	Insert Into @TabStudent
	Select Distinct StudentInstance, 'Y'
	From    dbo.OnLineOrderMappingTable Where StudentInstance Not In (Select StudentInstance From @TabStudent)
	And      LandedOrderID=  @OrderId 	


	-- All student who sold on QSP.ca	
	Insert Into @TabStudentInternet
	Select Distinct StudentInstance
	From   dbo.OnLineOrderMappingTable 
	Where LandedOrderID=    @OrderId

	Select @MaxRowCnt = Count(*) From  @TabStudentInternet

	-- Identify those students who have internet sale
	While @MaxRowCnt > 0 
	Begin
		Select @StudentInstanceWeb = StudentInstance from @TabStudentInternet Where Tindex = @MaxRowCnt 

		Select @StudentInstance = StudentInstance from @TabStudent  Where StudentInstance = @StudentInstanceWeb
 		
		If @StudentInstanceWeb = @StudentInstance
		Begin
			Update @TabStudent Set Internet ='Y' Where StudentInstance = @StudentInstanceWeb
		End
		
	Set @MaxRowCnt = @MaxRowCnt -1
	End


	-- Product Totals except Incentives and Premiums 
	Insert Into @TabProductTotals
	Select        StudentInstance,
		    Sum(Quantity) , 
		    Sum(TotalPrice) , 
		    TeacherLastName, 
		    TeacherFirstName, 
		    Lastname,
		    FirstName,
		    Classroom,
		    ProductType,
		    Null,			--ProductCode for Incentives used
		    CampaignId,
		     OrderQualifierId,
		    @OrderId,
		    @BatchID,
		    @BatchDate	
	From @TabGroupSummaryReportDetail
 	--From    dbo.GroupSummaryDetail 
	--Where	OrderId  =     @OrderId
	--And	BatchId =     @BatchId
	--And 	BatchDate = @BatchDate	
	--And ProductType Not In ('46008','46013','46014','46015' )    -- Incentive, Magazine Incentive, Music Incentive, Food Incentive
	Where ProductType Not In ('46008','46013','46014','46015' )    -- Incentive, Magazine Incentive, Music Incentive, Food Incentive
	Group By CampaignId,TeacherLastName,TeacherFirstName,  Classroom, StudentInstance, Lastname, FirstName,ProductType, OrderQualifierId	


	Insert Into @TabProductTotals
	Select	StudentInstance,
		Sum(Quantity) , 
		Sum(TotalPrice) , 
		TeacherLastName,
		TeacherFirstName, 
		Lastname,
		FirstName,
		Classroom,
		ProductType,
		Null,			--ProductCode for Incentives used
		CampaignId,
		OrderQualifierId,	
		@OrderId,
		@BatchID,
		@BatchDate	
 	From	dbo.GroupSummaryDetailOnline
	Where  OnLineOrderId In 
			(Select Distinct OnLineOrderId From dbo.OnLineOrderMappingTable Where LandedOrderId=@OrderId)
	 And ProductType Not In ('46008','46013','46014','46015' )    -- Incentive, Magazine Incentive, Music Incentive, Food Incentive
	 Group By CampaignId,TeacherLastName,TeacherFirstName,  Classroom, StudentInstance, Lastname, FirstName,ProductType, OrderQualifierId	


	-- Incentives
	Insert into @TabProductTotals
	Select       StudentInstance,
		    Sum(Quantity) , 
		    Sum(TotalPrice) , 
		    TeacherLastName, 
		    TeacherFirstName, 
		    Lastname,
		    FirstName,
		    Classroom,
		    ProductType,
		    ProductCode,			--ProductCode for Incentives used
		    CampaignId,
		     OrderQualifierId,
		    OrderId,
		    BatchId,
		    BatchDate	
	From @TabGroupSummaryReportDetail
	--From    dbo.GroupSummaryDetail
 	--Where   OrderId=     @OrderId
	--And	BatchId=      @BatchId
	--And 	BatchDate= @BatchDate	
	--And 	ProductType in ('46008','46013','46014','46015' )    -- Incentive, Magazine Incentive, Music Incentive, Food Incentive
	Where ProductType in ('46008','46013','46014','46015' )    -- Incentive, Magazine Incentive, Music Incentive, Food Incentive
	Group By CampaignId,BatchDate,BatchId, OrderId,TeacherLastName,TeacherFirstName,  Classroom, StudentInstance, Lastname,FirstName,ProductType,ProductCode, OrderQualifierId


	--Prizes	
	Insert into @TabProductTotals
	Select	StudentInstance,
		Sum(Quantity) , 
		Sum(TotalPrice) , 
		TeacherLastName, 
		TeacherFirstName, 
		Lastname,
		FirstName,
		Classroom,
	 	'PRIZE',
		Premium_Code,			
		CampaignId,
		 OrderQualifierId,
	 	OrderId,
		BatchId,
		BatchDate	
	From @TabGroupSummaryReportDetail
	--From    dbo.GroupSummaryDetail
 	--Where  OrderId=     @OrderId
	--And	BatchId=      @BatchId
	--And 	BatchDate= @BatchDate	
	--And	Premium_Code in ('1','2','3','4','5','6' )        -- RD and Rogers 
	Where Premium_Code in ('1','2','3','4','5','6' )        -- RD and Rogers 
	And 	 IsNull(PremiumCount,0) > 0   
	Group By CampaignId,BatchDate,BatchId, OrderId,TeacherLastName,TeacherFirstName,  Classroom, StudentInstance, Lastname, FirstName,Premium_Code, OrderQualifierId	

	--prizes have already been found using the landedorder id hence not required
	/*Insert into @TabProductTotals
	Select        StudentInstance,
		    Sum(Quantity) , 
		    Sum(TotalPrice) , 
		    TeacherLastName,
		    TeacherFirstName, 
		    Lastname,
		    FirstName,
		    Classroom,
		    'PRIZE',
		    Null,			--ProductCode for Incentives used
		    CampaignId,
		     @OrderId,
		    @BatchID,
		    @BatchDate	
 	FROM    shahordermanagement.dbo.GroupSummaryDetailOnline
	 WHERE       onlineorderid in 
			(select distinct onlineOrderid from shahordermanagement.dbo.OnLineOrderMappingTable where landedorderid=@OrderId)
	AND Premium_Code in ('1','2','3','4' )        -- RD and Rogers 
	-- AND	 Instance = @TeacherInstance
	  GROUP BY CampaignId,TeacherLastName,TeacherFirstName,  Classroom, StudentInstance, Lastname,FirstName,ProductType*/


	--For Each  Student get the totals for each productline prize and premium and insert output record
	Select @MaxRowCnt = Count(*) From  @TabStudent
	Set @Seq = 0

	While @MaxRowCnt > 0 
	Begin
		Select @StudentInstance=  StudentInstance, @InterNet = Internet From @TabStudent Where Tindex = @MaxRowCnt
		Set @Seq = @Seq+1
		
		Select  	@CampaignId = CampaignId		,
			@OrderId       =  OrderId			,
			@StudentFirstName =(Case @InterNet
					          When  'Y' Then StudentFirstName+' *'
					          Else StudentFirstName
					          End),
			@StudentLastName = StudentLastName ,
			--@StudentFirstName = StudentFirstName ,
			@TeacherLastName=TeacherLastName,
			@TeacherFirstName= TeacherFirstName,
			@Classroom = ClassRoom,	
			@OrderQualifierId =  OrderQualifierId		
		From @TabProductTotals 
		Where StudentInstance = @StudentInstance

		If @@Rowcount > 0 
		Begin
		Insert Into @TabOutPut 
			Values ( @Seq,
				@CampaignId		,
				@OrderQualifierId	,
				@OrderId		,
				@StudentInstance 	,
				@StudentLastName 	,
				@StudentFirstName  	,
				@Classroom		,
				@TeacherLastName,
				@TeacherFirstName,
				0			,--GiftQuantity
				0			,--GiftPrice
				0 			,--MagQuantity		
				0 			,--MagTotalPrice	
				0			,--RDrPremium
				0			,--RogerPremium
				0			,--LevelA
				0			,--LevelB
				0			,--LevelC
				0			,--LevelD
				0			,--LevelE
				0			,--LevelF
				0			--LevelG
			            )
		End


		--Update Magazine quantity and price for the teacher
		
		Select   @MagQuantity = IsNull(Sum(Quantity),0),
		             @MagTotalPrice = IsNull(Sum(TotalPrice),0)
		From	@TabProductTotals
		Where (ProductType = Cast(46001 as Varchar) Or ProductType = Cast(46006 as Varchar))   --Magazine and Book
		And StudentInstance = @StudentInstance
	
		If @@Rowcount > 0
		Begin
		Update @TabOutPut 
		Set MagQuantity = @MagQuantity,
		      MagPrice 	= @MagTotalPrice
		Where StudentInstance = @StudentInstance
		End

	
		--Update Gift quantity and price for the teacher
		Select @MagQuantity = IsNull(Sum(Quantity),0),
		           @MagTotalPrice = IsNull(Sum(TotalPrice),0)
		From    @TabProductTotals
		Where (ProductType = Cast(46002 as Varchar) Or ProductType = Cast(46003 as Varchar) or ProductType = Cast(46005 as Varchar)) --Gift Chocolate Food
		And StudentInstance = @StudentInstance
	
		If @@Rowcount > 0
		Begin
		Update @TabOutPut 
		Set GiftQuantity = @MagQuantity,
		      GiftPrice 	= @MagTotalPrice
		Where StudentInstance = @StudentInstance
		End

	
		--Update Incentives quantity and price for the teacher
		Select  @MagQuantity = IsNull(Sum(Quantity),0)
		From    @TabProductTotals
		Where (ProductType = Cast(46008 as Varchar) Or ProductType = Cast(46013 as Varchar) Or 	-- Incentives
			ProductType = Cast(46014 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
		And StudentInstance = @StudentInstance
		And productCode = 'PRA'
	
		If @@Rowcount > 0 and @MagQuantity > 0
		Begin
		Update @TabOutPut 
		Set LevelA = @MagQuantity
		Where StudentInstance = @StudentInstance
		End
	

		--Level B Incentive
		Select  @MagQuantity = IsNull(Sum(Quantity),0)
		From    @TabProductTotals
		Where (ProductType = Cast(46008 as Varchar) Or ProductType = Cast(46013 as Varchar) Or 	-- Incentives
			ProductType = Cast(46014 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
		And StudentInstance = @StudentInstance
		And productCode = 'PRB'
	
		If @@Rowcount > 0 and @MagQuantity > 0
		Begin
		Update @TabOutPut 
		Set LevelB = @MagQuantity
		Where StudentInstance = @StudentInstance
		End
	
	
		--Level C Incentive
		Select  @MagQuantity = IsNull(Sum(Quantity),0)
		From    @TabProductTotals
		Where (ProductType = Cast(46008 as Varchar) Or ProductType = Cast(46013 as Varchar) Or 	-- Incentives
			ProductType = Cast(46014 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
		And StudentInstance = @StudentInstance
		And productCode = 'PRC'
	
		If @@Rowcount > 0 and @MagQuantity > 0
		Begin
		Update @TabOutPut 
		Set LevelC = @MagQuantity
		Where StudentInstance = @StudentInstance
		End

		--Level D Incentive
		Select  @MagQuantity = IsNull(Sum(Quantity),0)
		From    @TabProductTotals
		Where (ProductType = Cast(46008 as Varchar) Or ProductType = Cast(46013 as Varchar) Or 	-- Incentives
			ProductType = Cast(46014 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
		And StudentInstance = @StudentInstance
		And productCode = 'PRD'

		If @@Rowcount > 0 and @MagQuantity > 0
		Begin
		Update @TabOutPut 
		Set LevelD = @MagQuantity
		Where StudentInstance = @StudentInstance
		End


		--Level E Incentive
		Select  @MagQuantity = IsNull(Sum(Quantity),0)
		From    @TabProductTotals
		Where (ProductType = Cast(46008 as Varchar) Or ProductType = Cast(46013 as Varchar) Or 	-- Incentives
			ProductType = Cast(46014 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
		And StudentInstance = @StudentInstance
		And productCode = 'PRE'
	
		If @@Rowcount > 0 and @MagQuantity > 0
		Begin
		Update @TabOutPut 
		Set LevelE = @MagQuantity
		Where StudentInstance = @StudentInstance
		End
	
	
		--Level F Incentive
		Select  @MagQuantity = IsNull(Sum(Quantity),0)
		From    @TabProductTotals
		Where (ProductType = Cast(46008 as Varchar) Or ProductType = Cast(46013 as Varchar) Or 	-- Incentives
			ProductType = Cast(46014 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
		And StudentInstance = @StudentInstance
		And productCode = 'PRF'
	
		If @@Rowcount > 0 and @MagQuantity > 0
		Begin
		Update @TabOutPut 
		Set LevelF = @MagQuantity
		Where StudentInstance = @StudentInstance
		End
	
	
		--Level G Incentive
		Select  @MagQuantity = IsNull(Sum(Quantity),0)
		From    @TabProductTotals
		Where (ProductType = Cast(46008 as Varchar) Or ProductType = Cast(46013 as Varchar) Or 	-- Incentives
			ProductType = Cast(46014 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
		And StudentInstance = @StudentInstance
		And productCode = 'PRG'
	
		If @@Rowcount > 0 and @MagQuantity > 0
		Begin
		Update @TabOutPut 
		Set LevelG = @MagQuantity
		Where StudentInstance = @StudentInstance
		End

	
		--Prizes Rogers
		Select  @MagQuantity = IsNull(Sum(Quantity),0)
		From    @TabProductTotals
		Where ProductType = 'PRIZE' 
		And StudentInstance = @StudentInstance
		And productCode  in ( '1', '3','6') 	--RD (1,3,6)	Roger (2,4,5)
		
		If @@Rowcount > 0 and @MagQuantity > 0
		Begin
		Update @TabOutPut 
		Set RDPremium = @MagQuantity
		Where StudentInstance = @StudentInstance
		End

		--Prizes RD
		Select  @MagQuantity = IsNull(Sum(Quantity),0)
		From    @TabProductTotals
		Where ProductType = 'PRIZE' 
		And StudentInstance = @StudentInstance
		And productCode  in ( '2', '4','5') 	--RD (1,3,6)	Roger (2,4,5)
		
		If @@Rowcount > 0 and @MagQuantity > 0
		Begin
		Update @TabOutPut 
		Set RogerPremium = @MagQuantity
		Where StudentInstance = @StudentInstance
		End

		 Set @MaxRowCnt = @MaxRowCnt -1 
	End	


	-- Sort on Teacher Name Student name and store result
	Insert Into @TabOutPutSorted
	Select  	CampaignId,
		OrderQualifierId,
		OrderId		,
		StudentInstance		,
		StudentLastName	,
		StudentFirstName	,
		ClassRoom		,
		TeacherLastName 	,
		TeacherFirstName  	,
		GiftQuantity		,
		GiftPrice		,
		MagQuantity		,
		MagPrice		,
		RDPremium		,
		RogerPremium		,
		LevelA			,
		LevelB			,
		LevelC			,
		LevelD			,
		LevelE			,
		LevelF			,
		LevelG			
	 From 	@TabOutPut Order By TeacherLastName Desc,Classroom Desc,StudentLastname Desc,StudentFirstName Desc

	-- Output table result has been inserted into OutputSorted table delete all record now
	Delete From @TabOutPut

	--Put Sorted rssult back into output
	Insert Into @TabOutPut 
	Select * From @TabOutPutSorted  

	Insert Into @TabClass
	Select Distinct  Classroom From @TabOutPut
	
	
	Select @MaxRowCnt = Count(*) From @TabClass
	While @MaxRowCnt>0
	Begin
		Select  @OldClassroom = Classroom From @TabClass Where Tindex =@MaxRowCnt
		Set @Seq = 0
		Set  @TotalMagForClass = 0

		Select @MaxRowCnt1 = Count(*) From @TabOutput

		While @MaxRowCnt1>0
		Begin
		Select   @TIndex			=TIndex,
			@CampaignId			=CampaignId,
			@OrderQualifierId		= OrderQualifierId,
			@OrderId			=OrderId,
			@StudentInstance		=StudentInstance, 
			@StudentLastName		=StudentLastName, 
		     	@StudentFirstName		=StudentFirstName, 
			@ClassRoom			=ClassRoom,
			@TeacherLastName 		=TeacherLastName,
			@TeacherFirstName  		=TeacherFirstName,
			@GiftQuantity			=GiftQuantity,
			@GiftPrice			=GiftPrice,
			@MagQuantity			=MagQuantity,
			@MagPrice			=MagPrice,
			@RDPremium			=RDPremium,
			@RogerPremium		=RogerPremium,
			@LevelA			=LevelA,
			@LevelB			=LevelB,
			@LevelC			=LevelC,
			@LevelD			=LevelD,
			@LevelE			=LevelE,
			@LevelF			=LevelF,
			@LevelG			=LevelG
		From @TabOutPut Where Tindex =@MaxRowCnt1

		If @OldClassroom = @Classroom
		Begin

		Set  @Seq = @Seq+1
			
		Insert Into @TabOutPut1 
		Values ( @Seq,
			@CampaignId		,
			@OrderQualifierId	,
			@OrderId		,
			@StudentInstance 	,			@StudentLastName 	,
			@StudentFirstName  	,
			@Classroom		,
			@TeacherLastName	,
			@TeacherFirstName	,
			@GiftQuantity		,	--GiftQuantity
			@GiftPrice		,	--GiftPrice
			@MagQuantity		,	--MagQuantity		
			@MagPrice 			,--MagTotalPrice	
			@RDPremium			,--RDrPremium
			@RogerPremium		,--RogerPremium
			@LevelA			,--LevelA
			@LevelB			,--LevelB
			@LevelC			,--LevelC
			@LevelD			,--LevelD
			@LevelE			,--LevelE
			@LevelF			,--LevelF
			@LevelG			,--LevelG
			0				,--@TotalGiftForClass	
			0				,--@TotalMagForClass 	
			0				,--@TotalGiftAmtForClass	
			0				,--@TotalMagAmtForClass	
			0				,--@TotalRDForClass
			0				,--@TotalRogerForClass
			0				,--@TotalLvlAForClass	
			0				,--@TotalLvlBForClass	
			0				,--@TotalLvlCForClass	
			0				,--@TotalLvlDForClass	
			0				,--@TotalLvlEForClass	
			0				,--@TotalLvlFForClass	
			0				--@TotalLvlGForClass	
		            )
			End


		Set @MaxRowCnt1 = @MaxRowCnt1 - 1
		End
		Select  @TotalMagForClass 	= Sum(MagQuantity),
			@TotalMagAmtForClass	= Sum(MagPrice),
			@TotalGiftForClass	= Sum(GiftQuantity),
			@TotalGiftAmtForClass	= Sum(GiftPrice),
			@TotalRDForClass	= Sum(RDPremium),
			@TotalRogerForClass	= Sum(RogerPremium),
			@TotalLvlAForClass	= Sum(LevelA),
			@TotalLvlBForClass	= Sum(LevelB),
			@TotalLvlCForClass	= Sum(LevelC),
			@TotalLvlDForClass	= Sum(LevelD),
			@TotalLvlEForClass	= Sum(LevelE),
			@TotalLvlFForClass	= Sum(LevelF),
			@TotalLvlGForClass	= Sum(LevelG)
		From @TabOutPut1 Where classroom =@oldClassroom


		Update @TabOutPut1 
		Set TotalMagForClass 		=@TotalMagForClass,
			TotalMagAmtForClass 	=@TotalMagAmtForClass,
			TotalGiftForClass 	=@TotalGiftForClass,
			TotalGiftAmtForClass 	=@TotalGiftAmtForClass,
			TotalRDForClass 	=@TotalRDForClass,
			TotalRogerForClass 	=@TotalRogerForClass,
			TotalLvlAForClass	=@TotalLvlAForClass,
			TotalLvlBForClass	=@TotalLvlBForClass,
			TotalLvlCForClass	=@TotalLvlCForClass,
			TotalLvlDForClass	=@TotalLvlDForClass,
			TotalLvlEForClass	=@TotalLvlEForClass,
			TotalLvlFForClass	=@TotalLvlFForClass,
			TotalLvlGForClass	=@TotalLvlGForClass
		Where  classroom =@oldClassroom
	Set @MaxRowCnt = @MaxRowCnt - 1
	End

	Select * From @TabOutPut1 Order By classroom,TeacherLastName,TeacherFirstName,Tindex
GO
