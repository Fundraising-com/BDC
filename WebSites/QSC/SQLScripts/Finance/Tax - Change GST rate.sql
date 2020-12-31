UPDATE	tax
SET		tax_rate = 5.00
WHERE	Tax_ID = 1

UPDATE	tax
SET		tax_rate = 13.00
WHERE	Tax_ID IN (2, 4, 5)

UPDATE	TaxRegion
SET		ConsolidatedRate = 5.00
WHERE	ID = 1

UPDATE	TaxRegion
SET		ConsolidatedRate = 13.00
WHERE	ID = 2

--SELECT	pd.QSP_Price AS QSP_Price_Original_GST, CONVERT(Numeric(10,2), CEILING(ROUND(pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END * 1.05, 2))) AS QSP_Price_New_GST, pd.Product_Code, p.Product_Sort_Name, ps.CatalogCode
UPDATE	pd
SET		pd.QSP_Price = CONVERT(Numeric(10,2), CEILING(ROUND(pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END * 1.05, 2)))
FROM	Pricing_Details pd
JOIN	Product p
			ON	p.Product_Instance = pd.Product_Instance
JOIN	ProgramSection ps
			ON	pd.ProgramSectionID = ps.ID
WHERE	pd.Pricing_Year = 2008
AND		pd.TaxRegionID = 1 --GST
AND		p.Type = 46001 --Magazine only
AND		pd.QSP_Price <> CONVERT(Numeric(10,2), CEILING(ROUND(pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END * 1.05, 2)))


--SELECT	pd.QSP_Price AS QSP_Price_Original_HST, CONVERT(Numeric(10,2), CEILING(ROUND(pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END * 1.13, 2))) AS QSP_Price_New_HST, pd.Product_Code, p.Product_Sort_Name, ps.CatalogCode
UPDATE	pd
SET		pd.QSP_Price = CONVERT(Numeric(10,2), CEILING(ROUND(pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END * 1.13, 2)))
FROM	Pricing_Details pd
JOIN	Product p
			ON	p.Product_Instance = pd.Product_Instance
JOIN	ProgramSection ps
			ON	pd.ProgramSectionID = ps.ID
WHERE	pd.Pricing_Year = 2008
AND		pd.TaxRegionID = 2 --HST
AND		p.Type = 46001 --Magazine only
AND		pd.QSP_Price <> CONVERT(Numeric(10,2), CEILING(ROUND(pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END * 1.13, 2)))