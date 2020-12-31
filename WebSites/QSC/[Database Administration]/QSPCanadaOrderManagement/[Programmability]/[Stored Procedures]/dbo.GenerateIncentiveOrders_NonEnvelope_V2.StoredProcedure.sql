USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GenerateIncentiveOrders_NonEnvelope_V2]    Script Date: 06/07/2017 09:19:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[GenerateIncentiveOrders_NonEnvelope_V2]
  @BatchId int
, @BatchDate smalldatetime
, @Supplemental int
, @SuccessTF int output
, @ChangeUserId varchar(4)

AS
set nocount on
Declare @IsCumulative int
Declare @CampaignId int
--Declare @IsGift int
--Declare @IsGiftOnly int
--Declare @IsMagExpress int
Declare @IsCombo int
--Declare @IsCookieDough int
Declare @COH int
Declare @IncentivesBillToId int
Declare @IncentivesBillToAccountId int
Declare @FMId varchar(4)
Declare @CustomerBillToInstance int
Declare @CustomerShipToInstance int
Declare @AccountId int
declare @StudentIncentiveReceivedCount int
Declare @IsHybrid int
Declare @IsMSCumulative int
Declare @IsOverLevelDHybrid int
Declare @FiscalYearOnly int

select @IsCombo=0
select @IsCumulative=0
--select @IsGift=0
--select @IsGiftOnly=0
--select @IsMagExpress=0
select @CampaignId=0
select @COH = 0
select @AccountId=0
select @IncentivesBillToId=0
select @IncentivesBillToAccountId=0
select @IsHybrid=0
select @IsMSCumulative=0
select @IsOverLevelDHybrid = 0
Select @FiscalYearOnly = 0

Declare @FiscalYear smalldatetime

SELECT
	@CampaignId = A.CampaignId
FROM 
	QSPCanadaOrderManagement..Batch A 
WHERE
	A.Id = @BatchId
	AND A.Date = @BatchDate

PRINT 'CampaignId = ' + Convert(varchar, IsNull(@CampaignId, ''))

SELECT
	@IncentivesBillToId = IncentivesBillToId,
	@FMId = FMID,
	@AccountId = BillToAccountId,
	@FiscalYear = StartDate
FROM
	QSPCanadaCommon..Campaign
WHERE
	Id = @CampaignId



IF Month(@FiscalYear) < 7
	SET @FiscalYearOnly = Year(@FiscalYear) 
Else
	SET @FiscalYearOnly = Year(@FiscalYear) + 1

--- Determine the accountid to charge this to

IF @IncentivesBillToId = 51001
	BEGIN
		--- SPLIT BILLING FIGURE OUT LATER
		SELECT 1
	END
ELSE IF @IncentivesBillToId = 51002
	BEGIN
		--- BILL TO FM
		SELECT @IncentivesBillToAccountId = 1    -- per KT
		EXEC CreateCustomerFM @FMID,	@ChangeUserId, 2, @CustomerBillToInstance OUTPUT
	END
ELSE IF @IncentivesBillToId = 51003
	BEGIN
		--- BILL TO GROUP
		SELECT @IncentivesBillToAccountId = @AccountId
		EXEC	CreateCustomerAccount @AccountId, @ChangeUserId, @CustomerBillToInstance OUTPUT
	END
ELSE IF @IncentivesBillToId = 51004 OR @IncentivesBillToId IS NULL
	BEGIN
		--- BILL TO QSP
		SELECT @IncentivesBillToAccountId = 1
		EXEC	CreateCustomerAccount 1, @ChangeUserId, @CustomerBillToInstance OUTPUT
	END
ELSE
	BEGIN
		--- FIGURE OUT WHAT TO DO OTHERWISE
		SELECT 1
	END

EXEC	CreateCustomerAccount @AccountId, @ChangeUserId, @CustomerShipToInstance OUTPUT


declare @ff int
-- First check to see if the campaign qualifies for automatic calculation
SELECT 1
FROM 
	QSPCanadaCommon..CampaignProgram A
WHERE
	A.ProgramId in ( 11, 18,22,29,33,40,42) --Prize Safari added Aug 15, 07 MS -- Removed BEAR and Prize Dimesion added Oct 7 MS
	AND A.CampaignId = @CampaignId
	AND A.DeletedTF = 0

IF @@RowCount > 0
	SET @IsCumulative = 1
ELSE
	SET @IsCumulative = 0
	
SELECT 1
FROM 
	QSPCanadaCommon..CampaignProgram A
WHERE
	A.ProgramId in ( 43 ) 
	AND A.CampaignId = @CampaignId
	AND A.DeletedTF = 0

IF @@RowCount > 0
	SET @IsHybrid = 1
ELSE
	SET @IsHybrid = 0

SELECT 1
FROM 
	QSPCanadaCommon..CampaignProgram A
WHERE
	A.ProgramId in ( 48 ) 
	AND A.CampaignId = @CampaignId
	AND A.DeletedTF = 0

IF @@RowCount > 0
	SET @IsMSCumulative = 1
ELSE
	SET @IsMSCumulative = 0

If @IsCumulative = 1 or @IsHybrid = 1 or @IsMSCumulative = 1

	BEGIN

		--- Now Check To See If Regular Mag Program Exists
		--- If it does then you can't automatically calculate incentives
		SELECT
			1
		FROM 
			QSPCanadaCommon..CampaignProgram A
		WHERE
			A.ProgramId = 1
			AND A.CampaignId = @CampaignId
			AND A.DeletedTF = 0

		IF @@RowCount > 0
			SET @SuccessTF = 0
		ELSE
			BEGIN		
				--PRINT 'REG MAG PROG DOES NOT EXIST'
				--- See If Atleast 1 program is MagExpress And/Or 1 Program is Gift

				/*SELECT
					@IsMagExpress = 1
				FROM 
					QSPCanadaCommon..CampaignProgram A
				WHERE
					A.ProgramId IN (2, 47) --Treat Mag FS like Mag Express
					AND A.CampaignId = @CampaignId
					AND A.DeletedTF = 0*/

				/*SELECT
					@IsGift = 1
				FROM 
					QSPCanadaCommon..CampaignProgram A
				WHERE
					A.ProgramId IN (4,19,32,49,52,53,54,55,56,58,59,62)
					AND A.CampaignId = @CampaignId
					AND A.DeletedTF = 0		
					AND A.OnlineOnly = 0*/

				/*SELECT
					@IsCookieDough = 1
				FROM 
					QSPCanadaCommon..CampaignProgram A
				WHERE
					A.ProgramId IN (44, 70)
					AND A.CampaignId = @CampaignId
					AND A.DeletedTF = 0*/	

				/*SELECT
					@IsGiftOnly = 1
				FROM 
					QSPCanadaCommon..CampaignProgram A
				WHERE
					A.ProgramId IN (19)
					AND A.CampaignId = @CampaignId
					AND A.DeletedTF = 0				*/

				/*IF @IsMagExpress = 1  AND (@IsGift = 1 or @IsCookieDough = 1)
					SET @IsCombo = 1
				
				IF (@IsGift = 1 And @IsCookieDough = 1)				
					SET @IsCombo = 1
					
				if @IsCookieDough = 1 and ( @IsMagExpress = 0 and @IsGift = 0 and @IsGiftOnly = 0)
					SET @IsCombo = 1
				
				if @IsGiftOnly = 1
					Set @IsCombo = 1*/
					
				--IF (@IsGift = 1 or @IsCookieDough = 1)
				Set @IsCombo = 1

			PRINT 'IsCombo: ' + Convert(varchar, @IsCombo)
			PRINT 'IsHybrid: ' + Convert(varchar, @IsHybrid)
			PRINT 'IsMiddleSchoolCumulative: ' + Convert(varchar, @IsMSCumulative)
			--Print 'IsMagExpress: ' + Convert(varchar, @IsMagExpress)
			--Print '@IsGift: ' + Convert(varchar, @IsGift)
--
				--IF @IsMagExpress = 0 AND @IsGift = 0
				--	SET @SuccessTF = 0
			--	ELSE
					 BEGIN

						PRINT 'CHECKS OK'


						IF EXISTS(SELECT Name FROM TEMPDB..SYSOBJECTS WHERE Name = '#Temp1')
							DROP TABLE #Temp1


						CREATE TABLE [dbo].[#Temp1] (
							[CampaignId] [int] NULL ,
							[StudentInstance] [int] NULL ,
							[FirstName] [varchar] (100) NULL ,
							[LastName] [varchar] (100) NULL ,							
							[ProductName] [varchar] (100) NULL ,							
							[MagQuantity] numeric(10,2) NULL ,								
							[OtherQuantity] numeric(10,2) NULL ,						
							[PreviouslyCalculatedItem] [int] NULL 
							
						) ON [PRIMARY]


						IF @IsCombo = 1
							BEGIN

						
								INSERT INTO 
									#Temp1								
								SELECT
									--TOP 10000 
									--*
									A.CampaignId
									,D.StudentInstance
									,C.FirstName
									,C.LastName
									,E.ProductName
									, MagQuantity = Case
										WHEN E.ProductType in ( 46001,46006,46007, 46023, 46024 ) THEN 2
										ELSE 0
										END
									, OtherQuantity = Case
										WHEN E.ProductType not in ( 46001,46006,46007, 46023, 46024 ) THEN E.Quantity
										ELSE 0
										END
									, 0 As PreviouslyCalculatedItem
								FROM
									Batch A
									INNER JOIN Teacher B ON A.AccountID = B.AccountID 
									INNER JOIN Student C ON B.Instance = C.TeacherInstance
									INNER JOIN CustomerOrderHeader D ON 								
										 C.Instance =  D.StudentInstance
									INNER JOIN CustomerOrderDetail E ON D.Instance = E.CustomerOrderHeaderInstance
								WHERE
									IsNull(A.IncentiveCalculationStatus, 0) <> 1 
									
									AND E.ProductType NOT IN (46008,46013,46014,46015, 46017, 46021)       -- NOT AN INCENTIVE 
									AND A.Id = @BatchId
									AND A.Date = @BatchDate							
									and OrderBatchDate=date and OrderbatchId = id
									and E.DelFlag=0
								
		
								if(@Supplemental = 0)
								begin
									INSERT INTO 
										#Temp1
									SELECT
										--TOP 1000 
										--*
										A.CampaignId
										,D.StudentInstance
										,C.FirstName
										,C.LastName
										,E.ProductName
										, MagQuantity = Case
											WHEN E.ProductType in ( 46001,46006,46007, 46023, 46024 ) THEN 2
											ELSE 0
											END
										, OtherQuantity = Case
											WHEN E.ProductType not in ( 46001,46006,46007, 46023, 46024 ) THEN E.Quantity
											ELSE 0
											END
										, 1 As PreviouslyCalculatedItem
									FROM
										Batch A
										INNER JOIN Teacher B ON A.AccountID = B.AccountID 
										INNER JOIN Student C ON B.Instance = C.TeacherInstance
										INNER JOIN CustomerOrderHeader D ON C.Instance =  D.StudentInstance
										INNER JOIN CustomerOrderDetail E ON D.Instance = E.CustomerOrderHeaderInstance
										--INNER JOIN #Temp1 F ON F.CampaignId = A.CampaignId
									WHERE
			--							IsNull(A.IncentiveCalculationStatus, 0) = 1 
			--							AND A.Date = B.OrderBatchDate
										A.CampaignID = @CampaignId
										And OrderBatchDate=date and OrderbatchId = id
										AND E.ProductType NOT IN (46008,46013,46014,46015, 46017, 46021)      -- NOT AN INCENTIVE 	
										and OrderQualifierID=39009 -- internet
										and OrderTypeCode <> 41012 -- Free Sub order
										AND E.DelFlag=0
								end	

								/*INSERT INTO 
									#Temp1
								SELECT
									--TOP 1000 
									--*
									A.CampaignId
									,D.StudentInstance
									,C.FirstName
									,C.LastName
									,E.ProductName
									, MagQuantity = Case
										WHEN E.ProductType in ( 46001,46006,46007 ) THEN 2
										ELSE 0
										END
									, OtherQuantity = Case
										WHEN E.ProductType not in ( 46001,46006,46007 ) THEN E.Quantity
										ELSE 0
										END
									, 1 As PreviouslyCalculatedItem
								FROM
									Batch A
									INNER JOIN Teacher B ON A.AccountID = B.AccountID 
									INNER JOIN Student C ON B.Instance = C.TeacherInstance
									INNER JOIN CustomerOrderHeader D ON C.Instance =  D.StudentInstance
									INNER JOIN CustomerOrderDetail E ON D.Instance = E.CustomerOrderHeaderInstance
									INNER JOIN #Temp1 F ON F.CampaignId = A.CampaignId
								WHERE
									IsNull(A.IncentiveCalculationStatus, 0) = 1 
		--							AND A.Date = B.OrderBatchDate
									and OrderBatchDate=date and OrderbatchId = id
									AND E.ProductType NOT IN (46008,46013,46014,46015, 46017, 46021)      -- NOT AN INCENTIVE 						
									AND E.DelFlag=0
									option(maxdop 1)*/ -- ONLY USE ONE CPU no parallel processing
							END
						ELSE
							BEGIN

								INSERT INTO 
									#Temp1								
								SELECT
									--TOP 10000 
									--*
									A.CampaignId
									,D.StudentInstance
									,C.FirstName
									,C.LastName
									,E.ProductName
									, MagQuantity = Case

										WHEN E.ProductType in  ( 46001,46006,46007, 46023, 46024 )THEN 1
										ELSE 0
										END
									, OtherQuantity = Case
										WHEN E.ProductType not in ( 46001,46006,46007, 46023, 46024 ) THEN E.Quantity * 0.5
										ELSE 0
										END
									, 0 As PreviouslyCalculatedItem
								FROM
									Batch A
									INNER JOIN Teacher B ON A.AccountID = B.AccountID 
									INNER JOIN Student C ON B.Instance = C.TeacherInstance
									INNER JOIN CustomerOrderHeader D ON C.Instance =  D.StudentInstance
									INNER JOIN CustomerOrderDetail E ON D.Instance = E.CustomerOrderHeaderInstance
								WHERE
									IsNull(A.IncentiveCalculationStatus, 0) <> 1 
									AND E.ProductType NOT IN (46008,46013,46014,46015, 46017, 46021)      -- NOT AN INCENTIVE 
									AND A.Id = @BatchId
									AND A.Date = @BatchDate							
									and OrderBatchDate=date and OrderbatchId = id
									AND E.DelFlag=0
		
								
								if(@Supplemental = 0)
								begin
		
									INSERT INTO 
										#Temp1
									SELECT
										--TOP 1000 
										--*
										A.CampaignId
										,D.StudentInstance
										,C.FirstName
										,C.LastName
										,E.ProductName
										, MagQuantity = Case
											WHEN E.ProductType in ( 46001,46006,46007, 46023, 46024 ) THEN 1
											ELSE 0
											END
										, OtherQuantity = Case
											WHEN E.ProductType not in ( 46001,46006,46007, 46023, 46024 ) THEN E.Quantity * 0.5
											ELSE 0
											END
										, 1 As PreviouslyCalculatedItem
									FROM
										Batch A
										INNER JOIN Teacher B ON A.AccountID = B.AccountID 
										INNER JOIN Student C ON B.Instance = C.TeacherInstance
										INNER JOIN CustomerOrderHeader D ON C.Instance =  D.StudentInstance
										INNER JOIN CustomerOrderDetail E ON D.Instance = E.CustomerOrderHeaderInstance
										--INNER JOIN #Temp1 F ON F.CampaignId = A.CampaignId
									WHERE
			--							IsNull(A.IncentiveCalculationStatus, 0) = 1 
			--							AND A.Date = B.OrderBatchDate
										A.CampaignID = @CampaignId
										And OrderBatchDate=date and OrderbatchId = id
										AND E.ProductType NOT IN (46008,46013,46014,46015, 46017, 46021)       -- NOT AN INCENTIVE 	
										and OrderTypeCode <> 41012 -- Free Sub order
										and OrderQualifierID=39009 -- internet
										AND E.DelFlag=0
								end	

		
								/*INSERT INTO 
									#Temp1
								SELECT
									--TOP 1000 
									--*
									A.CampaignId
									,D.StudentInstance
									,C.FirstName
									,C.LastName
									,E.ProductName
									, MagQuantity = Case
										WHEN E.ProductType in ( 46001,46006,46007 ) THEN 1
										ELSE 0
										END
									, OtherQuantity = Case
										WHEN E.ProductType not in ( 46001,46006,46007 ) THEN E.Quantity
										ELSE 0
										END
									, 1 As PreviouslyCalculatedItem
								FROM
									Batch A
									INNER JOIN Teacher B ON A.AccountID = B.AccountID 
									INNER JOIN Student C ON B.Instance = C.TeacherInstance
									INNER JOIN CustomerOrderHeader D ON C.Instance =  D.StudentInstance
									INNER JOIN CustomerOrderDetail E ON D.Instance = E.CustomerOrderHeaderInstance
									INNER JOIN #Temp1 F ON F.CampaignId = A.CampaignId
								WHERE
									IsNull(A.IncentiveCalculationStatus, 0) = 1 
		--							AND A.Date = B.OrderBatchDate
									and OrderBatchDate=date and OrderbatchId = id
									AND E.ProductType NOT IN (46008,46013,46014,46015, 46017, 46021)      -- NOT AN INCENTIVE 				
									AND E.DelFlag=0
									AND C.Instance In
									(
										SELECT
											distinct D.StudentInstance
											
										FROM
											Batch A
											INNER JOIN Teacher B ON A.AccountID = B.AccountID 
											INNER JOIN Student C ON B.Instance = C.TeacherInstance
											INNER JOIN CustomerOrderHeader D ON 								
												 C.Instance =  D.StudentInstance
											INNER JOIN CustomerOrderDetail E ON D.Instance = E.CustomerOrderHeaderInstance
										WHERE
											E.ProductType NOT IN (46008,46013,46014,46015, 46017, 46021)      -- NOT AN INCENTIVE 
											AND E.DelFlag=0
											AND A.Id = @BatchId
											AND A.Date = @BatchDate							
											and OrderBatchDate=date and OrderbatchId = id

									)*/

							END



						UPDATE  #Temp1 SET OtherQuantity = 1 WHERE OtherQuantity > 100
						
-- comment ou

/*						SELECT * FROM #Temp1
						ORDER BY 
							
							 LastName
							,FirstName
							, StudentInstance
*/						
						--- Calculate Incentives And Insert Order
						
						DECLARE @StudentInstance int,
						@FirstName varchar(50),
						@LastName varchar(50),
						@MagQuantity numeric(10,2),
						@OtherQuantity numeric(10,2),
						@TotalQuantity numeric(10,2),
						@MagPrice_Instance int,
						@PrizePrice money,
						@PrizeProductCode varchar(20),
						@PrizeProgramSectionId int

						DECLARE Detail CURSOR FOR
								SELECT
								A.StudentInstance
								, A.FirstName
								, A.LastName
								, ROUND(Sum(A.MagQuantity), 0) As MagQuantity
								, ROUND(Sum(A.OtherQuantity), 0) As OtherQuantity	
								, ROUND(Sum(A.MagQuantity + A.OtherQuantity), 0) As TotalQuantity
							FROM
								#Temp1 A
							GROUP BY
								A.FirstName
								, A.LastName
								, A.StudentInstance


							OPEN Detail
								FETCH NEXT FROM Detail INTO
									@StudentInstance,
									@FirstName,
									@LastName,
									@MagQuantity,
									@OtherQuantity,
									@TotalQuantity
							
							WHILE @@Fetch_Status = 0
								BEGIN

								--PRINT 'ABOUT TO Calc Inc'

								PRINT 'ABOUT TO Calc Inc'
								PRINT 'First: ' + @FirstName
								PRINT 'Last: ' + @LastName
								PRINT 'Totlal Quantity: ' + Convert(varchar, @TotalQuantity)

								-- Change to send in if hybrid program 2012
								--- Get the Item
								exec CalculateIncentivePrize @FiscalYear, @TotalQuantity, 1, 0, @IsCombo, @IsHybrid, @IsMSCumulative, @MagPrice_Instance output
							
							PRINT 'Item x: ' + Convert(varchar, @MagPrice_Instance)

								IF @MagPrice_Instance > 0 
									BEGIN

										exec dbo.pr_StudentIncentiveCheck @StudentInstance, @MagPrice_Instance, @CampaignId, @StudentIncentiveReceivedCount out

										PRINT 'StudentInstance: ' + Convert(varchar, @StudentInstance)
									PRINT 'StudentIncentiveReceivedCount: ' + Convert(varchar, @StudentIncentiveReceivedCount)

										IF @StudentIncentiveReceivedCount = 0
											BEGIN
												--- Grab the information For the Prize
												SELECT
													@PrizePrice = QSP_Price,
													@PrizeProductCode = Product_Code,
													@PrizeProgramSectionId = ProgramSectionId
												FROM
													QSPCanadaProduct..Pricing_Details
												WHERE
													MagPrice_Instance = @MagPrice_Instance									
								
														
												--- INSERT AN ORDER HEADER
												EXEC CreateOrderHeader @BatchDate, @BatchId, @IncentivesBillToAccountId, @CampaignId, @CustomerBillToInstance, @coh  OUTPUT, @ChangeUserId
				
												UPDATE CustomerOrderHeader SET StudentInstance = @StudentInstance WHERE Instance = @coh
				
												--- INSERT AN ORDER DETAIL
												declare @t int -- set roduct type
--												select @t=Program_type from QSPCanadaProduct..Pricing_Details where MagPrice_Instance=@MagPrice_Instance
												select @t=type from QSPCanadaProduct..Pricing_Details pd ,QSPCanadaProduct..Product p where MagPrice_Instance=@MagPrice_Instance
												and p.product_Instance= pd.product_instance

												-- If hybrid and higher than D set the high level one to shipped and create a D otherwise 												
												if @ishybrid = 0
												begin								
												
													EXEC CreateDetailItem @BatchDate, @coh, @PrizeProductCode, @FirstName,
														@LastName,1, @PrizePrice, @PrizeProgramSectionId,  @PrizePrice, 1, @t, @MagPrice_Instance, 500, @CustomerShipToInstance
												end
												else
												begin
													
													declare @threshold numeric(10,2)
													declare @lowlevel int
													Declare @shipstatus int
													
													Select @threshold = 0
													Select @lowlevel = 0
													Select @shipstatus = 500
													
													if @IsHybrid <> 0
													begin
														if @IsCombo <> 0
															Select @threshold = Long1Value
																	,@lowlevel = DoubleValue from systemoptions where 
																	Long2Value = @FiscalYearOnly and 
																			KeyValue = 'hybrid combo threshold'
																		
														else	
														begin
																										
															Select @threshold = Long1Value
																	,@lowlevel = DoubleValue from systemoptions where 
																	Long2Value = @FiscalYearOnly and 
																			KeyValue = 'hybrid mag threshold'
														end
														PRINT 'Low level: ' + Convert(varchar, @lowlevel)
														PRINT '@FiscalYear: ' + Convert(varchar, @FiscalYear)
														PRINT '@@threshold: ' + Convert(varchar, @threshold)
																			
														-- if total is less than the threshold then fine else we'll need a low threshold level
														if @TotalQuantity >= @lowlevel
														begin
															Select @shipstatus = 508
															
															EXEC CreateDetailItem @BatchDate, @coh, '1051392', @FirstName,
																@LastName,1, @PrizePrice, @PrizeProgramSectionId,  @PrizePrice, 1, @t, @threshold, 500, @CustomerShipToInstance

														
														end
														EXEC CreateDetailItem @BatchDate, @coh, @PrizeProductCode, @FirstName,
															@LastName,1, @PrizePrice, @PrizeProgramSectionId,  @PrizePrice, 1, @t, @MagPrice_Instance, @shipstatus, @CustomerShipToInstance	
															
																																
																
													end
													else
													begin												
																											
														EXEC CreateDetailItem @BatchDate, @coh, @PrizeProductCode, @FirstName,
															@LastName,1, @PrizePrice, @PrizeProgramSectionId,  @PrizePrice, 1, @t, @MagPrice_Instance, 500, @CustomerShipToInstance
													end
												end

													
													

												--update CustomerOrderDetail set CustomerShipToInstance = @CustomerShipToInstance where CustomerOrderHeaderInstance = @coh
											END

									END

								FETCH NEXT FROM Detail INTO
									@StudentInstance,
									@FirstName,
									@LastName,
									@MagQuantity,
									@OtherQuantity,
									@TotalQuantity


							END

						CLOSE Detail
						DEALLOCATE Detail

							
					END
			END
	END
ELSE
	SET @SuccessTF = 0

-- OUTPUT
SELECT @SuccessTF
GO
