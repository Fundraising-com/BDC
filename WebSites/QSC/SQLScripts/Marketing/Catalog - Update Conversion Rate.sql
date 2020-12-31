USE QSPCanadaProduct

BEGIN TRAN

DECLARE @ConversionRate NUMERIC(12, 2),
		@Year			INT,
		@Season			VARCHAR(1),
		@GSTRate		NUMERIC(12, 2),
		@HSTRate		NUMERIC(12, 2)

SET @Year = 2010
SET @Season = 'S'
SET @GSTRate = 1.05
SET @HSTRate = 1.13

SELECT	@ConversionRate = DefaultConversionRate
FROM	QSPCanadaCommon..Season
WHERE	FiscalYear = @Year
AND		Season = @Season

PRINT	@ConversionRate

SELECT	*
/*UPDATE	pd
SET		ConversionRate = @ConversionRate,
		QSP_Price = CEILING(ROUND(pd.Basic_Price_Yr * @ConversionRate * CASE pd.TaxRegionID WHEN 1 THEN @GSTRate ELSE @HSTRate END, 2)),
		BasePriceOriginalCurrency = ROUND(pd.Basic_Price_Yr * @ConversionRate, 2),
		NewsStandPriceOriginalCurrency = ROUND(pd.NewsStand_Price_Yr / @ConversionRate, 2)*/
FROM	Pricing_Details pd
JOIN	Product p
			ON	p.Product_Instance = pd.Product_Instance
WHERE	pd.Pricing_Year = @Year
AND		pd.Pricing_Season = @Season
AND		p.Currency = 802 --802: US

COMMIT TRAN