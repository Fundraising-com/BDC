USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[HomeRoomSummaryReportDetail_Internet]    Script Date: 06/07/2017 09:19:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE    PROCEDURE [dbo].[HomeRoomSummaryReportDetail_Internet] @OrderId 	Int,
						    @BatchId 	Int,
						    @BatchDate	DateTime
						
 AS
set nocount on
	Declare	 @MaxRowCnt 		Int,
		@MaxRowCnt1	Int,
		 @StudentInstance	Int,
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
		@TIndex			Int,
			@GiftQuantity		Int,
			@GiftPrice		Numeric(10,2),
			--@MagQuantity		Int,
			@MagPrice		Numeric(10,2),
			@RDPremium		Int,
			@RogerPremium		Int,
			@LevelA			Int,
			@LevelB			Int,
			@LevelC			Int,
			@LevelD			Int,
			@LevelE			Int,
			@LevelF			Int,
			@LevelG			Int,
			@IncentiveProgramId		Int

	Declare	@TabClass	Table(
			Tindex		Int Identity,			
			Classroom	Varchar(10)
				          ) 

	Declare @TabStudent Table(
			 TIndex		   		Int Identity,
			 StudentInstance		Int
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
		      OrderId			Int,
		      BatchId			Int,
		      BatchDate			DateTime
		    )	

	Declare @TabOutPut Table (
 			--TIndex			Int Identity,  need to reset seq number after each teacher
			TIndex			Int,
			CampaignId		Int,
			OrderId			Int,
			StudentInstance		Int, 
			StudentLastName		Varchar(30), 
		     	StudentFirstName		Varchar(30), 
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
 			TIndex			Int Identity,  --need to reset seq number after each teacher
			CampaignId		Int,
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
 			TIndex			Int Identity, -- need to reset seq number after each teacher
			CampaignId		Int,
			OrderId			Int,
			StudentInstance		Int, 
			StudentLastName		Varchar(30), 
		     	StudentFirstName		Varchar(30), 
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
			TotalRDForClass		Int,
			TotalRogerForClass		Int,
			TotalLvlAForClass	Int,
			TotalLvlBForClass	Int,
			TotalLvlCForClass	Int,
			TotalLvlDForClass	Int,
			TotalLvlEForClass	Int,
			TotalLvlFForClass	Int,
			TotalLvlGForClass	Int
				                  )



	-- Insert All teachers

	Insert into @TabStudent
	SELECT DISTINCT StudentInstance
	FROM         dbo.GroupSummaryDetail
	WHERE      OrderId=         @OrderId 	
	And	      BatchId=        @BatchId
	And 	      BatchDate=   @BatchDate	

	Insert into @TabStudent
	SELECT DISTINCT StudentInstance
	FROM         OnLineOrderMappingTable where studentinstance not in (select studentinstance from @TabStudent)
	and      LandedOrderID=         @OrderId 	
	

	

--select * from OnLineOrderMappingTable	
	  -- Product Totals except Incentives and Premiums 
	Insert into @TabProductTotals
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
		    @OrderId,
		    @BatchID,
		    @BatchDate	
 	FROM    dbo.GroupSummaryDetail
	 WHERE      OrderId=     @OrderId
	And	      BatchId=      @BatchId
	And 	      BatchDate= @BatchDate	
	 And ProductType not in ('46008','46013','46014','46015' )    -- Incentive, Magazine Incentive, Music Incentive, Food Incentive
	-- AND	 Instance = @TeacherInstance
	  GROUP BY CampaignId,TeacherLastName,TeacherFirstName,  Classroom, StudentInstance, Lastname,
		    FirstName,ProductType

	Insert into @TabProductTotals
	Select        StudentInstance,
		    Sum(Quantity) , 
		    Sum(TotalPrice) , 
		    'QSP.CA' as TeacherLastName, 
		    TeacherFirstName, 
		    Lastname,
		    FirstName,
		    Classroom,
		    ProductType,
		    Null,			--ProductCode for Incentives used
		    CampaignId,
		    @OrderId,
		    @BatchID,
		    @BatchDate	
 	FROM    dbo.GroupSummaryDetailOnline
	 WHERE       onlineorderid in 
			(select distinct onlineOrderid from OnLineOrderMappingTable where landedorderid=@OrderId)
	 And ProductType not in ('46008','46013','46014','46015' )    -- Incentive, Magazine Incentive, Music Incentive, Food Incentive
	-- AND	 Instance = @TeacherInstance
	  GROUP BY CampaignId,TeacherLastName,TeacherFirstName,  Classroom, StudentInstance, Lastname,
		    FirstName,ProductType

	-- Incentives
	Insert into @TabProductTotals
	Select        StudentInstance,
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
		    OrderId,
		    BatchId,
		    BatchDate	
 	FROM    dbo.GroupSummaryDetail
	 WHERE      OrderId=     @OrderId
	And	      BatchId=      @BatchId
	And 	      BatchDate= @BatchDate	
	And ProductType in ('46008','46013','46014','46015' )    -- Incentive, Magazine Incentive, Music Incentive, Food Incentive
	GROUP BY CampaignId,BatchDate,BatchId, OrderId,TeacherLastName,TeacherFirstName,  Classroom, StudentInstance, Lastname,
		    FirstName,ProductType,ProductCode


/*
	update @TabProductTotals set Lastname = '* '+lastname
		from @TabProductTotals, OnLineOrderMappingTable where @TabProductTotals.StudentInstance=OnLineOrderMappingTable.StudentInstance
		and LandedOrderID = @orderid
*/	

	--Prizes	
	Insert into @TabProductTotals
	Select        StudentInstance,
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
	 	    OrderId,
		    BatchId,
		    BatchDate	
 	FROM    dbo.GroupSummaryDetail
 	WHERE   OrderId=     @OrderId
	And	 BatchId=      @BatchId
	And 	 BatchDate= @BatchDate	
	 AND Premium_Code in ('1','2','3','4' )        -- RD and Rogers 
	-- And 	 IsNull(PremiumCount,0) > 0   -- AllQsp Premium Ids are zero	
	GROUP BY CampaignId,BatchDate,BatchId, OrderId,TeacherLastName,TeacherFirstName,  Classroom, StudentInstance, Lastname,
		    FirstName,Premium_Code
	Insert into @TabProductTotals
	Select        StudentInstance,
		    Sum(Quantity) , 
		    Sum(TotalPrice) , 
		    		    'QSP.CA' as TeacherLastName,  
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
 	FROM    dbo.GroupSummaryDetailOnline
	 WHERE       onlineorderid in 
			(select distinct onlineOrderid from OnLineOrderMappingTable where landedorderid=@OrderId)
		AND Premium_Code in ('1','2','3','4' )        -- RD and Rogers 
	-- AND	 Instance = @TeacherInstance
	  GROUP BY CampaignId,TeacherLastName,TeacherFirstName,  Classroom, StudentInstance, Lastname,
		    FirstName,ProductType
--select * from @TabProductTotals  where studentlastname='Apps' order by studentlastname,studentfirstname

--select * from @TabStudent where studentinstance=793743

	--For Each  Student get the totals for each productline prize and premium and insert output record
	Select @MaxRowCnt = Count(*) From  @TabStudent
	Set @Seq = 0

	While @MaxRowCnt > 0 

	Begin
		Select @StudentInstance=  StudentInstance From @TabStudent Where Tindex = @MaxRowCnt
		Set @Seq = @Seq+1
		Select  	@CampaignId = CampaignId		,
			@OrderId       =  OrderId			,
			@StudentLastName = StudentLastName ,
			@StudentFirstName = StudentFirstName ,
			@TeacherLastName=TeacherLastName,
			@TeacherFirstName= TeacherFirstName,
			@Classroom = ClassRoom		
		From @TabProductTotals 
		Where StudentInstance = @StudentInstance
	
		If @@Rowcount > 0 
		Begin
		Insert into @TabOutPut 
			Values ( @Seq,
				@CampaignId		,
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
		
		Select @MagQuantity = IsNull(Sum(Quantity),0),
		            @MagTotalPrice = IsNull(Sum(TotalPrice),0)
		From 	@TabProductTotals
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
			ProductType = Cast(46015 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
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
			ProductType = Cast(46015 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
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
			ProductType = Cast(46015 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
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
			ProductType = Cast(46015 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
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
			ProductType = Cast(46015 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
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
			ProductType = Cast(46015 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
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
			ProductType = Cast(46015 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
		And StudentInstance = @StudentInstance
		And productCode = 'PRG'
	
		If @@Rowcount > 0 and @MagQuantity > 0
		Begin
			Update @TabOutPut 
			Set LevelG = @MagQuantity
			Where StudentInstance = @StudentInstance
		End
	
		--Prizes
		Select  @MagQuantity = IsNull(Sum(Quantity),0)
		From    @TabProductTotals
		Where ProductType = 'PRIZE' 
		And StudentInstance = @StudentInstance
		And productCode  in ( '1', '3') 	--Roger (1,3)	RD (2,4)
		
		If @@Rowcount > 0 and @MagQuantity > 0
		Begin
			Update @TabOutPut 
			Set RogerPremium = @MagQuantity
			Where StudentInstance = @StudentInstance
		End
	
		Select  @MagQuantity = IsNull(Sum(Quantity),0)
		From    @TabProductTotals
		Where ProductType = 'PRIZE' 
		And StudentInstance = @StudentInstance
		And productCode  in ( '2', '4') 	--Roger (1,3)	RD (2,4)
		
		If @@Rowcount > 0 and @MagQuantity > 0
		Begin
			Update @TabOutPut 
			Set RDPremium = @MagQuantity
			Where StudentInstance = @StudentInstance
		End
	
	
		 Set @MaxRowCnt = @MaxRowCnt -1 
	End	
--select * from @TabOutPut where studentlastname = 'Apps' order by StudentLastName,                StudentFirstName        
	-- Sort on Teacher Name Student name and store result
	Insert into @TabOutPutSorted
	Select  	CampaignId, OrderId		,
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
	 From 	@TabOutPut Order By TeacherLastName,Classroom,StudentLastname,studentFirstName 

	-- Output table result has been inserted into OutputSorted table delete all record now
	Delete From @TabOutPut

	--Put Sorted rssult back into output
	Insert into @TabOutPut 
	Select * From @TabOutPutSorted  

	Insert into @TabClass
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
			
			Insert into @TabOutPut1 
		Values ( @Seq,
			@CampaignId		,
			@OrderId		,
			@StudentInstance 	,
			@StudentLastName 	,
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
		From @TabOutPut1 where classroom =@oldClassroom


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

	Select * from @TabOutPut1 Order By Classroom,TeacherLastName,TeacherFirstName,Tindex
GO
