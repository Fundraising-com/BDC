USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[CalculateIncentivePrize]    Script Date: 06/07/2017 09:19:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CalculateIncentivePrize]

@BatchDate smalldatetime,
@Quantity numeric(10,2),
@IsGift int,
@IsMagExpress int,
@IsCombo int,
@IsHybrid int,
@IsMSCumulative int,
@Pricing_Detail_Id int = null output

AS

DECLARE @FiscalYear int

Declare @PrizeSection int

IF Month(@BatchDate) < 7
	SET @FiscalYear = Year(@BatchDate) 
Else
	SET @FiscalYear = Year(@BatchDate) + 1

Set @Pricing_Detail_Id = 0
Set @PrizeSection =0

if @IsHybrid <> 0
begin
	if @IsCombo <> 0
	begin	
		Select @PrizeSection = Long1Value from systemoptions where Long2Value = @FiscalYear and 
				KeyValue = 'hybrid combo'
			

	end				
	else
		Select @PrizeSection = Long1Value from systemoptions where Long2Value = @FiscalYear and 
				KeyValue = 'hybrid mag'
	

end
else if @IsMSCumulative <> 0
begin
	if @IsCombo <> 0
	begin	
		Select @PrizeSection = Long1Value from systemoptions where Long2Value = @FiscalYear and 
				KeyValue = 'middleschool cumulative combo'
			

	end				
	else
		Select @PrizeSection = Long1Value from systemoptions where Long2Value = @FiscalYear and 
				KeyValue = 'middleschool cumulative mag'
	

end
else
begin
	if @IsCombo <> 0
	begin
		Select @PrizeSection = Long1Value from systemoptions where Long2Value = @FiscalYear and 
				KeyValue = 'cumulative combo'
				
				
	end			
	else
		Select @PrizeSection = Long1Value from systemoptions where Long2Value = @FiscalYear and 
				KeyValue = 'cumulative mag'

end



PRINT '-------------------------------------------- TS'
PRINT 'BatchDate: ' + Convert(varchar, @BatchDate)
PRINT 'Quantity: ' + Convert(varchar, @Quantity)
PRINT 'FiscalYear: ' + Convert(varchar, @FiscalYear)
PRINT '@PrizeSection: ' + Convert(varchar, @PrizeSection)
PRINT '@@IsCombo: ' + Convert(varchar, @IsCombo)
PRINT '@@@IsHybrid: ' + Convert(varchar, @IsHybrid)
PRINT '@@@IsMSCumulative: ' + Convert(varchar, @IsMSCumulative)
/*
IF @IsGift = 1
	PRINT '@IsGift=1'
ELSE
	PRINT '@IsGift=0'

IF @IsMagExpress = 1
	PRINT '@IsMagExpress=1'
ELSE
	PRINT '@IsMagExpress=0'

IF @IsCombo = 1
	PRINT '@IsCombo=1'
ELSE
	PRINT '@IsCombo=0'
*/

IF @IsCombo = 0
	BEGIN
		IF @IsMagExpress  = 1
			BEGIN
			PRINT 'MAGExpress NonCombo'
/*
--				PRINT 'MAGExpress NonCombo'
				SELECT 
					TOP 1
					@Pricing_Detail_Id = B.MagPrice_Instance
				FROM
					QSPCanadaProduct..Product A
					INNER JOIN QSPCanadaProduct..Pricing_Details B ON B.OracleCode = A.OracleCode AND B.Program_Type = A.ProductLine
				WHERE
					A.Product_Year = @FiscalYear
					AND A.ProductLine = 46013
					AND A.Prize_Level_Qty_Required <= @Quantity
				ORDER BY
					A.Prize_Level_Qty_Required DESC
*/
				SELECT top 1 @Pricing_Detail_Id = B.MagPrice_Instance
								FROM
									QSPCanadaProduct..Product A
									,QSPCanadaProduct..Pricing_Details B 
				
								WHERE
									A.Product_Year = @FiscalYear
									AND A.Type = 46013
									and  A.product_instance=b.Product_instance
--									and B.Program_Type = A.Type
									AND A.Prize_Level_Qty_Required <= @Quantity
									and ProgramSectionID = @PrizeSection
													ORDER BY
														A.Prize_Level_Qty_Required DESC


			END
		
		
		IF @IsGift  = 1
			BEGIN

--				PRINT 'GIFT NON COMBO'
/*				SELECT 
					TOP 1
					@Pricing_Detail_Id = B.MagPrice_Instance
				FROM
					QSPCanadaProduct..Product A
					INNER JOIN QSPCanadaProduct..Pricing_Details B ON B.OracleCode = A.OracleCode AND B.Program_Type = A.ProductLine
				WHERE
					A.Product_Year = @FiscalYear
					AND A.ProductLine = 46014
					AND A.Prize_Level_Qty_Required <= @Quantity
				ORDER BY
					A.Prize_Level_Qty_Required DESC
*/

				SELECT top 1 @Pricing_Detail_Id = B.MagPrice_Instance
								FROM
									QSPCanadaProduct..Product A
									,QSPCanadaProduct..Pricing_Details B 
				
								WHERE
									A.Product_Year = @FiscalYear
									AND A.Type = 46014
									and  A.product_instance=b.Product_instance
--									and B.Program_Type = A.Type
									AND A.Prize_Level_Qty_Required <= @Quantity
									and ProgramSectionID = @PrizeSection
													ORDER BY
														A.Prize_Level_Qty_Required DESC

			END
		
	END

ELSE 
	BEGIN
		PRINT '[CalculateIncentivePrize] COMBO'
		PRINT @FiscalYear

		SELECT top 1 @Pricing_Detail_Id = B.MagPrice_Instance
								FROM
									QSPCanadaProduct..Product A
									,QSPCanadaProduct..Pricing_Details B 
				
								WHERE
									A.Product_Year = @FiscalYear
									AND A.Type = 46014
									and  A.product_instance=b.Product_instance
--									and B.Program_Type = A.Type
									AND A.Prize_Level_Qty_Required <= @Quantity
										and ProgramSectionID = @PrizeSection
													ORDER BY
														A.Prize_Level_Qty_Required DESC
	END

/*
IF @Pricing_Detail_Id <> 0 
	SELECT @Pricing_Detail_Id
Else
	SELECT 0

*/
GO
