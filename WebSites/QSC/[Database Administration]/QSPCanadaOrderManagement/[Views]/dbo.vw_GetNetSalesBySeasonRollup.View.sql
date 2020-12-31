USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_GetNetSalesBySeasonRollup]    Script Date: 06/07/2017 09:18:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_GetNetSalesBySeasonRollup] AS
SELECT FMID, RepName, Season, SeasonID, SUM(NetSales) AS MagNetSales, 0 AS GiftNetSales, 0 AS JewelryNetSales, 0 AS CookieDoughNetSales, 0 AS CandleNetSales, 0 AS TrtNetSales, 0 AS EntertainmentNetSales
, SUM(NetUnits) AS MagNetUnits, 0 AS GiftNetUnits, 0 AS JewelryNetUnits, 0 AS CookieDoughNetUnits, 0 AS CandleNetUnits, 0 AS TrtNetUnits, 0 AS EntertainmentNetUnits
FROM [dbo].[vw_GetNetSalesBySeason] 
WHERE section_type_id = 2 -- Magazine
GROUP BY FMID, RepName, Season, SeasonID
UNION
SELECT FMID, RepName, Season, SeasonID, 0,SUM(NetSales),0,0,0,0,0,0,SUM(NetUnits),0,0,0,0,0 
FROM [dbo].[vw_GetNetSalesBySeason] 
WHERE section_type_id = 1 -- Gift
GROUP BY FMID, RepName, Season, SeasonID
UNION
SELECT FMID, RepName, Season, SeasonID, 0,0,SUM(NetSales),0,0,0,0,0,0,SUM(NetUnits),0,0,0,0  
FROM [dbo].[vw_GetNetSalesBySeason] 
WHERE section_type_id = 11 -- Jewelry
GROUP BY FMID, RepName, Season, SeasonID
UNION
SELECT FMID, RepName, Season, SeasonID, 0,0,0,SUM(NetSales),0,0,0,0,0,0,SUM(NetUnits),0,0,0
FROM [dbo].[vw_GetNetSalesBySeason] 
WHERE section_type_id = 9 -- Cookie Dough
GROUP BY FMID, RepName, Season, SeasonID
UNION
SELECT FMID, RepName, Season, SeasonID, 0,0,0,0,SUM(NetSales),0,0,0,0,0,0,SUM(NetUnits),0,0 
FROM [dbo].[vw_GetNetSalesBySeason] 
WHERE section_type_id = 13 -- Candle
GROUP BY FMID, RepName, Season, SeasonID
UNION
SELECT FMID, RepName, Season, SeasonID, 0,0,0,0,0,SUM(NetSales),0,0,0,0,0,0,SUM(NetUnits),0
FROM [dbo].[vw_GetNetSalesBySeason] 
WHERE section_type_id = 14 -- Trt
GROUP BY FMID, RepName, Season, SeasonID
UNION
SELECT FMID, RepName, Season, SeasonID, 0,0,0,0,0,0,SUM(NetSales),0,0,0,0,0,0,SUM(NetUnits)
FROM [dbo].[vw_GetNetSalesBySeason] 
WHERE section_type_id = 15 -- Entertainment
GROUP BY FMID, RepName, Season, SeasonID
GO
