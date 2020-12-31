USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_GetNetSalesBySeasonSummary]    Script Date: 06/07/2017 09:18:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_GetNetSalesBySeasonSummary] AS
SELECT  FMID
, RepName
, Season
, SUM(MagNetSales) AS MagNetSales
, SUM(GiftNetSales) AS GiftNetSales
, SUM(JewelryNetSales) AS JewelryNetSales
, SUM(CookieDoughNetSales) AS CookieDoughNetSales
, SUM(CandleNetSales) AS CandleNetSales
, SUM(TrtNetSales) AS TrtNetSales
, SUM(EntertainmentNetSales) EntertainmentNetSales
, SUM(MagNetUnits) AS MagNetUnits
, SUM(GiftNetUnits) AS GiftNetUnits
, SUM(JewelryNetUnits) AS JewelryNetUnits
, SUM(CookieDoughNetUnits) AS CookieDoughNetUnits
, SUM(CandleNetUnits) AS CandleNetUnits
, SUM(TrtNetUnits) AS TrtNetUnits
, SUM(EntertainmentNetUnits) AS EntertainmentNetUnits
FROM [dbo].[vw_GetNetSalesBySeasonRollup] 
GROUP BY FMID
, RepName
, Season
GO
