USE [QSPCanadaProduct]
GO
/****** Object:  View [dbo].[vw_Kanata_Catalog_SelectAll]    Script Date: 06/07/2017 09:17:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_Kanata_Catalog_SelectAll]

AS
--Issue # 2980 MS Jul 30, 2007
--All Kanata / WFC catalog for current season where there is (atleast) one item 
--Issue#3314 MS Sept 7, 07 Added Kanata Bulk
--Assume naming convention for catalog 

SELECT	DISTINCT	pm.*
FROM 	QSPCanadaProduct.dbo.Pricing_Details pd
JOIN	QSPCanadaCommon.dbo.Season s
			ON	s.Season = pd.Pricing_Season
			AND	s.FiscalYear = pd.Pricing_Year
JOIN	QSPCanadaProduct.dbo.ProgramSection ps
			ON	ps.ID = pd.ProgramSectionID
JOIN	QSPCanadaProduct.dbo.Program_Master pm
			ON	pm.Program_ID = ps.Program_ID
WHERE	GetDate() BETWEEN DateAdd(Month, -4, s.StartDate) AND DateAdd(Year, 1, s.EndDate) --Last year and this year and next year when next year is within 2 months
AND		pm.Status in (30403, 30404) --Approved, In Use
AND		
(
		pm.Program_Type LIKE '%DRAW%'
OR		pm.Program_Type LIKE '%Prize Zone%'
OR		pm.Program_Type LIKE '%Book%'
OR		pm.Program_Type LIKE '%WFC%'
OR		pm.Program_Type LIKE 'Prize Safari%'
OR		pm.Program_Type LIKE '%Kanata%BULK%'
OR		pm.Program_Type LIKE '%Kanata Pick%'
OR		pm.Program_Type LIKE 'Prize Dimension%'
OR		pm.Program_Type LIKE 'Treasure Quest%'
OR		pm.Program_Type Like 'Bear%'
OR		pm.Program_Type Like '%Field Supply%'
OR		pm.Program_Type LIKE 'Go For Gold%'
OR		pm.Program_Type LIKE 'Game On%'
OR		pm.Program_Type LIKE 'Prize Time%'
OR		pm.Program_Type LIKE '%time to be amazing%'
OR		pm.Program_Type LIKE '%Prize Blast%'
OR		pm.Program_Type LIKE '%Prize Factor%'
OR		pm.Program_Type LIKE 'KB%'
OR		pm.Program_Type LIKE '%Tasty%'
OR		pm.Program_Type LIKE '%Gift%'
OR		pm.Program_Type LIKE 'Prize%'
OR		pm.Program_Type LIKE '%Goin Ape%'
OR		pm.Program_Type LIKE '%Prize Workz%'
OR		pm.Program_Type LIKE '%Totes%'
OR		pm.Program_Type LIKE '%Candle%'
OR		pm.Program_Type LIKE '%To Remember This%'
OR		pm.Program_Type LIKE '%Entertainment%'
OR		pm.Program_Type LIKE '%Embrace%'
OR		pm.Program_Type LIKE '%Festival%'
OR		pm.Program_Type LIKE '%Organic Edibles%'
OR		pm.Program_Type LIKE '%Bloom%'
OR		pm.Program_Type LIKE '%Kitchen%'
OR		pm.Program_Type LIKE '%Donation%'
OR		pm.Program_Type LIKE '%Nature%'
OR		pm.Program_Type LIKE '%Chocolate%'
OR		pm.Program_Type LIKE '%Popcorn%'
OR		pm.Program_Type LIKE '%Travel Cup%'
OR		pm.Program_Type LIKE '%Dream Big%'
OR		pm.Program_Type LIKE '%Enjoy Something%'
OR		pm.Program_Type LIKE '%Jewelry%'
OR		pm.Program_Type LIKE '%Tervis%'
OR		pm.Program_Type LIKE '%Discount%'
OR		pm.Program_Type LIKE '%Pretzel%'
OR		pm.Program_Type LIKE '%Leap%'
OR		pm.Program_Type LIKE '%Cool%'
OR		pm.Program_Type LIKE '%Rally%'
)
AND		pm.Program_Type NOT LIKE '%Magazine%'
AND		pm.Program_Type NOT LIKE '%Brochure%'

GO
