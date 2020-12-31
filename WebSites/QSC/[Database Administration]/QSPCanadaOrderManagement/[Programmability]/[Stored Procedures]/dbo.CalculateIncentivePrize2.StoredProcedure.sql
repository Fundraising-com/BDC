USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[CalculateIncentivePrize2]    Script Date: 06/07/2017 09:19:22 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[CalculateIncentivePrize2]

@BatchDate smalldatetime,
@Quantity int,
@IsGift bit,
@IsMagExpress bit,
@IsCombo bit,
@Pricing_Detail_Id int = null output

AS

DECLARE @FiscalYear int

IF Month(@BatchDate) < 7
	SET @FiscalYear = Year(@BatchDate) 
Else
	SET @FiscalYear = Year(@BatchDate) + 1

Set @Pricing_Detail_Id = 0


PRINT '-------------------------------------------- TS'
PRINT 'BatchDate: ' + Convert(varchar, @BatchDate)
PRINT 'Quantity: ' + Convert(varchar, @Quantity)
PRINT 'FiscalYear: ' + Convert(varchar, @FiscalYear)

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


IF @IsCombo = 0
	BEGIN
		IF @IsMagExpress  = 1
			BEGIN
				PRINT 'MAGExpress NonCombo'
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
			END
		
		
		IF @IsGift  = 1
			BEGIN
				PRINT 'GIFT NON COMBO'
				SELECT 
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
			END
		
	END

ELSE 
	BEGIN
		PRINT 'COMBO'
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
	END


IF @Pricing_Detail_Id <> 0 
	SELECT @Pricing_Detail_Id
Else
	SELECT 0
GO
