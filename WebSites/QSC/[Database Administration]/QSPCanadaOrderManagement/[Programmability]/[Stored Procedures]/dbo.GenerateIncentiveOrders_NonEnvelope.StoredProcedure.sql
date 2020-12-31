USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GenerateIncentiveOrders_NonEnvelope]    Script Date: 06/07/2017 09:19:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GenerateIncentiveOrders_NonEnvelope]


@BatchId int
,@BatchDate smalldatetime
,@SuccessTF bit output
,@ChangeUserId varchar(4)

AS

Declare @IsPlanetary bit
Declare @CampaignId int
Declare @IsGift bit
Declare @IsMagExpress bit
Declare @IsCombo bit
Declare @COH int
Declare @IncentivesBillToId int
Declare @IncentivesBillToAccountId int
Declare @FMId varchar(10)
Declare @CustomerBillToInstance int
Declare @AccountId int
Declare @CalcStatus int

set nocount on

SELECT
	@CampaignId = A.CampaignId,
	@CalcStatus = 	isnull( IncentiveCalculationStatus , 0)
FROM 
	QSPCanadaOrderManagement..Batch A 
WHERE
	A.Id = @BatchId
	AND A.Date = @BatchDate


if(@CalcStatus <> 0)
begin
	select 'done'
	return -1
	
end

--PRINT 'CampaignId = ' + Convert(varchar, IsNull(@CampaignId, ''))

SELECT
	@IncentivesBillToId = IncentivesBillToId,
	@FMId = FMID,
	@AccountId = BillToAccountId
FROM
	QSPCanadaCommon..Campaign
WHERE
	Id = @CampaignId

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
ELSE IF @IncentivesBillToId = 51004
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


-- First check to see if the campaign qualifies for automatic calculation
SELECT
	*
FROM 
	QSPCanadaCommon..CampaignProgram A
WHERE
	A.ProgramId  in (11,29)
	AND A.CampaignId = @CampaignId

IF @@RowCount > 0
	SET @IsPlanetary = 1
ELSE
	SET @IsPlanetary = 0

--Print 'IsPlanetary: ' + Convert(varchar, @IsPlanetary)

--OVERRIDE SINCE THERE IS NO PLANETARY REWARDS AT THIS TIME
--SET @IsPlanetary = 1

If @IsPlanetary = 1

	BEGIN

		--- Now Check To See If Regular Mag Program Exists
		--- If it does then you can't automatically calculate incentives
		SELECT
			*
		FROM 
			QSPCanadaCommon..CampaignProgram A
		WHERE
			A.ProgramId = 1
			AND A.CampaignId = @CampaignId
		
		IF @@RowCount > 0
			SET @SuccessTF = 0
		ELSE
			BEGIN		
--				PRINT 'REG MAG PROG DOES NOT EXIST'
				--- See If Atleast 1 program is MagExpress And/Or 1 Program is Gift

				SELECT
					@IsMagExpress = 1
				FROM 
					QSPCanadaCommon..CampaignProgram A
				WHERE
					A.ProgramId IN (2)
					AND A.CampaignId = @CampaignId

				SELECT
					@IsGift = 1
				FROM 
					QSPCanadaCommon..CampaignProgram A
				WHERE
					A.ProgramId IN (4)
					AND A.CampaignId = @CampaignId
				

				IF @IsMagExpress = 1  AND @IsGift = 1
					SET @IsCombo = 1


				IF @IsMagExpress = 0 AND @IsGift = 0
					SET @SuccessTF = 0
				ELSE
					 BEGIN

--						PRINT 'CHECKS OK'

						IF EXISTS(SELECT Name FROM TEMPDB..SYSOBJECTS WHERE Name = '#Temp1')
							DROP TABLE #Temp1
						
						-- Populate a temp table with Students who have sales for This Batch
				/*
						SELECT
							TOP 10000 
							--*
							A.CampaignId
							,D.StudentInstance
							,C.FirstName
							,C.LastName
							,E.ProductName
							, MagQuantity = Case
								WHEN E.ProductType = 46001 THEN 1
								ELSE 0
								END
							, OtherQuantity = Case
								WHEN E.ProductType <> 46001 THEN E.Quantity
								ELSE 0
								END
							, 0 As PreviouslyCalculatedItem
						INTO
							#Temp1
						FROM
							Batch A
							INNER JOIN Envelope B ON A.ID = B.OrderBatchId 
							INNER JOIN Student C ON B.TeacherInstance = C.TeacherInstance
							INNER JOIN CustomerOrderHeader D ON C.Instance =  D.StudentInstance
							INNER JOIN CustomerOrderDetail E ON D.Instance = E.CustomerOrderHeaderInstance
						WHERE
							IsNull(A.IncentiveCalculationStatus, 0) <> 1 
							AND A.Date = B.OrderBatchDate
							AND E.ProductType NOT IN (46008)      -- NOT AN INCENTIVE 
							AND A.Id = @BatchId
							AND A.Date = @BatchDate

				*/
					SELECT
							TOP 10000 
							--*
							A.CampaignId
							,D.StudentInstance
							,C.FirstName
							,C.LastName
							,E.ProductName
							, MagQuantity = Case
								WHEN E.ProductType = 46001 THEN 1
								ELSE 0
								END
							, OtherQuantity = Case
								WHEN E.ProductType <> 46001 THEN E.Quantity
								ELSE 0
								END
							, 0 As PreviouslyCalculatedItem
						INTO
							#Temp1
						FROM
							Batch A
							INNER JOIN Teacher B ON A.AccountID = B.AccountID 
							INNER JOIN Student C ON B.Instance = C.TeacherInstance
							INNER JOIN CustomerOrderHeader D ON 								
								 C.Instance =  D.StudentInstance
							INNER JOIN CustomerOrderDetail E ON D.Instance = E.CustomerOrderHeaderInstance
						WHERE
							IsNull(A.IncentiveCalculationStatus, 0) <> 1 
							
							AND E.ProductType NOT IN (46008)      -- NOT AN INCENTIVE 
							AND A.Id = @BatchId
							AND A.Date = @BatchDate							
							and OrderBatchDate=date and OrderbatchId = id
						
					INSERT INTO 
							#Temp1
						SELECT
							TOP 1000 
							--*
							A.CampaignId
							,D.StudentInstance
							,C.FirstName
							,C.LastName
							,E.ProductName
							, MagQuantity = Case
								WHEN E.ProductType = 46001 THEN 1
								ELSE 0
								END
							, OtherQuantity = Case
								WHEN E.ProductType <> 46001 THEN E.Quantity
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
							AND E.ProductType NOT IN (46008)      -- NOT AN INCENTIVE 						
						--- Grab any previously calculated items for cumulative calculations
/*
						INSERT INTO 
							#Temp1
						SELECT
							TOP 1000 
							--*
							A.CampaignId
							,D.StudentInstance
							,C.FirstName
							,C.LastName
							,E.ProductName
							, MagQuantity = Case
								WHEN E.ProductType = 46001 THEN 1
								ELSE 0
								END
							, OtherQuantity = Case
								WHEN E.ProductType <> 46001 THEN E.Quantity
								ELSE 0
								END
							, 1 As PreviouslyCalculatedItem
						FROM
							Batch A
							INNER JOIN Envelope B ON A.ID = B.OrderBatchId 
							INNER JOIN Student C ON B.TeacherInstance = C.TeacherInstance
							INNER JOIN CustomerOrderHeader D ON C.Instance =  D.StudentInstance
							INNER JOIN CustomerOrderDetail E ON D.Instance = E.CustomerOrderHeaderInstance
							INNER JOIN #Temp1 F ON F.CampaignId = A.CampaignId
						WHERE
							IsNull(A.IncentiveCalculationStatus, 0) = 1 
							AND A.Date = B.OrderBatchDate
							AND E.ProductType NOT IN (46008)      -- NOT AN INCENTIVE 
*/						
						
						/*
						SELECT * FROM #Temp1
						ORDER BY 
							FirstName
							, LastName
							, StudentInstance
						*/
						--- Calculate Incentives And Insert Order
						
						DECLARE @StudentInstance int,
						@FirstName varchar(50),
						@LastName varchar(50),
						@MagQuantity int,
						@OtherQuantity int,
						@TotalQuantity int,
						@MagPrice_Instance int,
						@PrizePrice money,
						@PrizeProductCode varchar(20),
						@PrizeProgramSectionId int

						DECLARE Detail CURSOR FOR
							SELECT
								A.StudentInstance
								, A.FirstName
								, A.LastName
								, Sum(A.MagQuantity) As MagQuantity
								, Sum(A.OtherQuantity) As OtherQuantity	
								, Sum(A.MagQuantity + A.OtherQuantity) As TotalQuantity
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

--								PRINT 'ABOUT TO Calc Inc'

								--- Get the Item
								exec CalculateIncentivePrize @BatchDate, @TotalQuantity, @IsGift, @IsMagExpress, @IsCombo, @MagPrice_Instance output
							
--								PRINT 'Item x: ' + Convert(varchar, @MagPrice_Instance)

								IF @MagPrice_Instance > 0 
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
										select @t=Program_type from QSPCanadaProduct..Pricing_Details where MagPrice_Instance=@MagPrice_Instance

										EXEC CreateDetailItem @BatchDate, @coh, @PrizeProductCode, @FirstName,
											@LastName,1, @PrizePrice, @PrizeProgramSectionId,  @PrizePrice, 1, @t, @MagPrice_Instance, 500

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


Update
	QSPCanadaOrderManagement..Batch  Set IncentiveCalculationStatus = 1
WHERE
	Id = @BatchId
	AND Date = @BatchDate

-- OUTPUT
SELECT @SuccessTF
GO
