USE [QSPCanadaProduct]

DECLARE @Season	VARCHAR
DECLARE @Year	INT

SET	@Season = 'S'
SET	@Year = 2014

--Remember in Excel to Replace ' for '. Remember to prepend a ' in each header name
SELECT	DISTINCT
		'''' + pm.Code AS Code,
		'''' + prod.Product_Code AS Product_Code,
		'''' + prod.Product_Sort_Name AS Product_Sort_Name,
		pd.Nbr_Of_Issues,
		pd.QSP_Price AS Price,
		pd.Pricing_Year,
		'''' + pd.Pricing_Season AS Pricing_Season,
		prod.ProductLine + 46000 AS ProductLine,
		pd.TaxRegionID
FROM	Program_Master pm
JOIN	ProgramSection ps
			ON	ps.CatalogCode = pm.Code
JOIN	Pricing_Details pd
			ON	pd.ProgramSectionID = ps.ID
JOIN	Product prod
			ON	prod.Product_Instance = pd.Product_Instance
WHERE	pd.TaxRegionID IN (0, 1, 2)
AND		prod.ProductLine NOT IN (4, 8, 13, 14, 19, 21)
AND		pm.Code NOT LIKE 'CLOSEOUT%'
AND		pd.Pricing_Season = @Season
AND		pd.Pricing_Year = @Year
ORDER BY '''' + pm.Code,
		'''' + prod.Product_Code,
		pd.Pricing_Year,
		'''' + pd.Pricing_Season,
		pd.TaxRegionID