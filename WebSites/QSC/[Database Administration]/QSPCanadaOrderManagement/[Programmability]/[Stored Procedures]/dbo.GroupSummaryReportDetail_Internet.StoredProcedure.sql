USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GroupSummaryReportDetail_Internet]    Script Date: 06/07/2017 09:19:37 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  Procedure [dbo].[GroupSummaryReportDetail_Internet]   @OrderId 	Int,
						  @BatchId 	Int,
						  @BatchDate	DateTime
						
 As
	set nocount on
	Declare	 @MaxRowCnt 		Int,
		 @TeacherInstance	Int,
		 @MagQuantity 		Int, 
		 @MagTotalPrice	Numeric (10,2) , 
		 @TeacherLastName	Varchar(30), 
		 @TeacherFirstName	Varchar(30),  
		 @Classroom		Varchar(10),
		@CampaignId		Int

	Declare @TabTeacher Table(
			 TIndex		   		Int Identity,
			 TeacherInsatnce		Int
			 )

	Declare @TabProductTotals Table(
		      Tindex			Int Identity,
		      TeacherInstance		Int, 
		      Quantity 			Int, 
		      TotalPrice			Numeric(10,2) , 
		      TeacherLastName		Varchar(30), 
		      TeacherFirstName		Varchar(30), 
		      Classroom			Varchar(10),
		      Producttype			Varchar(10),
		      ProductCode		Varchar(10),
		      CampaignId			Int,
		      OrderId			Int,
		      BatchId			Int,
		      BatchDate			DateTime
		    )	

	Declare @TabOutPut Table (
 			TIndex			Int Identity,
			CampaignId		Int,
			OrderId			Int,
			TeacherInstance 	Int,
			TeacherLastName 	Varchar(30),
			TeacherFirstName  	Varchar(30),
			ClassRoom		Varchar(10),
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


	-- Insert All teachers

	Insert into @TabTeacher
	SELECT DISTINCT Instance
	FROM         dbo.GroupSummaryDetail
	WHERE      OrderId=         @OrderId 	
	And	      BatchId=        @BatchId
	And 	      BatchDate=   @BatchDate	

	
	  -- Product Totals except Incentives and Premiums 
	Insert into @TabProductTotals
	SELECT     Instance,
		    Sum(Quantity) , 
		    Sum(TotalPrice) , 
		    TeacherLastName, 
		     TeacherFirstName, 
		     Classroom,
		     ProductType,
		     Null,			--ProductCode for Incentives used
		     CampaignId,
		     OrderId,
		     BatchId,
		     BatchDate	
 	FROM    dbo.GroupSummaryDetail
	 WHERE      OrderId=     @OrderId 	
	And	      BatchId=      @BatchId
	And 	      BatchDate= @BatchDate	
	 And ProductType not in ('46008','46013','46014','46015' )    -- Incentive, Magazine Incentive, Music Incentive, Food Incentive
	-- AND	 Instance = @TeacherInstance
	  GROUP BY CampaignId,TeacherLastName, TeacherFirstName,  Classroom, Instance,ProductType,OrderId, BatchId, BatchDate

	Insert into @TabProductTotals
	Select        Instance,
		    Sum(Quantity) , 
		    Sum(TotalPrice) , 
		    'QSP.CA' as TeacherLastName, 
		    TeacherFirstName, 
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
	  GROUP BY CampaignId,TeacherLastName,TeacherFirstName,  Classroom, Instance,ProductType



	-- Incentives
	Insert into @TabProductTotals
	Select       Instance,
		    Sum(Quantity) , 
		    Sum(TotalPrice) , 
		    TeacherLastName, 
		     TeacherFirstName, 
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
	-- AND	 Instance = @TeacherInstance
	 GROUP BY ProductCode,CampaignId,TeacherLastName, TeacherFirstName,  
	Classroom, Instance,ProductType,OrderId, BatchId, BatchDate


	Insert into @TabProductTotals
	Select        Instance,
		    Sum(Quantity) , 
		    Sum(TotalPrice) , 
		    'QSP.CA' as TeacherLastName, 
		    TeacherFirstName, 
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
	 And ProductType  in ('46008','46013','46014','46015' )    -- Incentive, Magazine Incentive, Music Incentive, Food Incentive
	-- AND	 Instance = @TeacherInstance
	  GROUP BY CampaignId,TeacherLastName,TeacherFirstName,  Classroom, Instance,ProductType


	--Prizes	

	Insert into @TabProductTotals
	SELECT     Instance,
		   Count(PremiumCount), 
	   	0 , 
	   	TeacherLastName, 
	   	TeacherFirstName, 
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
	 GROUP BY Premium_Code,CampaignId,TeacherLastName, TeacherFirstName,  
	Classroom, Instance,ProductType,OrderId, BatchId, BatchDate

	Insert into @TabProductTotals
	Select        Instance,
		    Sum(Quantity) , 
		    Sum(TotalPrice) , 
		    'QSP.CA' as TeacherLastName, 
		    TeacherFirstName, 
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
	 AND Premium_Code in ('1','2','3','4' )        -- RD and Rogers 
	-- AND	 Instance = @TeacherInstance
	  GROUP BY CampaignId,TeacherLastName,TeacherFirstName,  Classroom, Instance,ProductType



	--For Each  Teacher get the totals for each productline prize and premium and insert output record
	Select @MaxRowCnt = Count(*) From  @TabTeacher

	While @MaxRowCnt > 0 

	Begin
	Select @TeacherInstance=  TeacherInsatnce From @TabTeacher Where Tindex = @MaxRowCnt

	Select  	@CampaignId = CampaignId		,
		@OrderId       =  OrderId			,
		@TeacherLastName = TeacherLastName ,
		@TeacherFirstName = TeacherFirstName ,
		@Classroom = ClassRoom		
	From @TabProductTotals 
	Where TeacherInstance = @TeacherInstance

	If @@Rowcount > 0 
	Begin
	Insert into @TabOutPut 
		Values (
			@CampaignId		,
			@OrderId		,
			@TeacherInstance 	,
			@TeacherLastName 	,
			@TeacherFirstName  	,
			@Classroom		,
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
	And TeacherInstance = @TeacherInstance

	If @@Rowcount > 0
	Begin
	Update @TabOutPut 
	Set MagQuantity = @MagQuantity,
	      MagPrice 	= @MagTotalPrice
	Where TeacherInstance = @TeacherInstance
	End

	--Update Gift quantity and price for the teacher
	
	Select @MagQuantity = IsNull(Sum(Quantity),0),
	           @MagTotalPrice = IsNull(Sum(TotalPrice),0)
	From    @TabProductTotals
	Where (ProductType = Cast(46002 as Varchar) Or ProductType = Cast(46003 as Varchar) or ProductType = Cast(46005 as Varchar)) --Gift Chocolate Food
	And TeacherInstance = @TeacherInstance

	If @@Rowcount > 0
	Begin
	Update @TabOutPut 
	Set GiftQuantity = @MagQuantity,
	      GiftPrice 	= @MagTotalPrice
	Where TeacherInstance = @TeacherInstance
	End

	--Update Incentives quantity and price for the teacher
	
	Select  @MagQuantity = IsNull(Sum(Quantity),0)
	From    @TabProductTotals
	Where (ProductType = Cast(46008 as Varchar) Or ProductType = Cast(46013 as Varchar) Or 	-- Incentives
		ProductType = Cast(46015 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
	And TeacherInstance = @TeacherInstance
	And productCode = 'PRA'

	If @@Rowcount > 0 and @MagQuantity > 0
	Begin
		Update @TabOutPut 
		Set LevelA = @MagQuantity
		Where TeacherInstance = @TeacherInstance
	End

	--Level B Incentive
	Select  @MagQuantity = IsNull(Sum(Quantity),0)
	From    @TabProductTotals
	Where (ProductType = Cast(46008 as Varchar) Or ProductType = Cast(46013 as Varchar) Or 
		ProductType = Cast(46015 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
	And TeacherInstance = @TeacherInstance
	And productCode = 'PRB'

	If @@Rowcount > 0 and @MagQuantity > 0
	Begin
		Update @TabOutPut 
		Set LevelB = @MagQuantity
		Where TeacherInstance = @TeacherInstance
	End


	--Level C Incentive
	Select  @MagQuantity = IsNull(Sum(Quantity),0)
	From    @TabProductTotals
	Where (ProductType = Cast(46008 as Varchar) Or ProductType = Cast(46013 as Varchar) Or 
		ProductType = Cast(46015 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
	And TeacherInstance = @TeacherInstance
	And productCode = 'PRC'

	If @@Rowcount > 0 and @MagQuantity > 0
	Begin
		Update @TabOutPut 
		Set LevelC = @MagQuantity
		Where TeacherInstance = @TeacherInstance
	End


	--Level D Incentive
	Select  @MagQuantity = IsNull(Sum(Quantity),0)
	From    @TabProductTotals
	Where (ProductType = Cast(46008 as Varchar) Or ProductType = Cast(46013 as Varchar) Or 
		ProductType = Cast(46015 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
	And TeacherInstance = @TeacherInstance
	And productCode = 'PRD'

	If @@Rowcount > 0 and @MagQuantity > 0
	Begin
		Update @TabOutPut 
		Set LevelD = @MagQuantity
		Where TeacherInstance = @TeacherInstance
	End

	--Level E Incentive
	Select  @MagQuantity = IsNull(Sum(Quantity),0)
	From    @TabProductTotals
	Where (ProductType = Cast(46008 as Varchar) Or ProductType = Cast(46013 as Varchar) Or 
		ProductType = Cast(46015 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
	And TeacherInstance = @TeacherInstance
	And productCode = 'PRE'

	If @@Rowcount > 0 and @MagQuantity > 0
	Begin
		Update @TabOutPut 
		Set LevelE = @MagQuantity
		Where TeacherInstance = @TeacherInstance
	End


	--Level F Incentive
	Select  @MagQuantity = IsNull(Sum(Quantity),0)
	From    @TabProductTotals
	Where (ProductType = Cast(46008 as Varchar) Or ProductType = Cast(46013 as Varchar) Or 
		ProductType = Cast(46015 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
	And TeacherInstance = @TeacherInstance
	And productCode = 'PRF'

	If @@Rowcount > 0 and @MagQuantity > 0
	Begin
		Update @TabOutPut 
		Set LevelF = @MagQuantity
		Where TeacherInstance = @TeacherInstance
	End


	--Level G Incentive
	Select  @MagQuantity = IsNull(Sum(Quantity),0)
	From    @TabProductTotals
	Where (ProductType = Cast(46008 as Varchar) Or ProductType = Cast(46013 as Varchar) Or 
		ProductType = Cast(46015 as Varchar) Or	ProductType = Cast(46015 as Varchar) 	)
	And TeacherInstance = @TeacherInstance
	And productCode = 'PRG'

	If @@Rowcount > 0 and @MagQuantity > 0
	Begin
		Update @TabOutPut 
		Set LevelG = @MagQuantity
		Where TeacherInstance = @TeacherInstance
	End

	--Prizes
	Select  @MagQuantity = IsNull(Sum(Quantity),0)
	From    @TabProductTotals
	Where ProductType = 'PRIZE' 
	And TeacherInstance = @TeacherInstance
	And productCode  in ( '1', '3') 	--Roger (1,3)	RD (2,4)
	
	If @@Rowcount > 0 and @MagQuantity > 0
	Begin
		Update @TabOutPut 
		Set RogerPremium = @MagQuantity
		Where TeacherInstance = @TeacherInstance
	End

	Select  @MagQuantity = IsNull(Sum(Quantity),0)
	From    @TabProductTotals
	Where ProductType = 'PRIZE' 
	And TeacherInstance = @TeacherInstance
	And productCode  in ( '2', '4') 	--Roger (1,3)	RD (2,4)
	
	If @@Rowcount > 0 and @MagQuantity > 0
	Begin
		Update @TabOutPut 
		Set RDPremium = @MagQuantity
		Where TeacherInstance = @TeacherInstance
	End


	 Set @MaxRowCnt = @MaxRowCnt -1 
	End	
	
	Select * From @TabOutPut Order By ClassRoom,TeacherLastName ,TeacherFirstName
GO
