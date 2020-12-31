USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_GetNetSalesBySeasonOverallSummary]    Script Date: 06/07/2017 09:18:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_GetNetSalesBySeasonOverallSummary]

AS

SELECT		s.FMID,
			s.RepName,
			fm.SAPAcctNo,
			SUM(CASE Season WHEN 'Spring 2015' THEN MagNetSales + GiftNetSales + JewelryNetSales + CookieDoughNetSales + CandleNetSales + TrtNetSales + EntertainmentNetSales ELSE 0 END) AS Spring2015NetSales,
			SUM(CASE Season WHEN 'Spring 2015' THEN MagNetUnits + GiftNetUnits + JewelryNetUnits + CookieDoughNetUnits + CandleNetUnits + TrtNetUnits + EntertainmentNetUnits ELSE 0 END) AS Spring2015Units,
			SUM(CASE Season WHEN 'Fall 2015' THEN MagNetSales + GiftNetSales + JewelryNetSales + CookieDoughNetSales + CandleNetSales + TrtNetSales + EntertainmentNetSales ELSE 0 END) AS Fall2015NetSales,
			SUM(CASE Season WHEN 'Fall 2015' THEN MagNetUnits + GiftNetUnits + JewelryNetUnits + CookieDoughNetUnits + CandleNetUnits + TrtNetUnits + EntertainmentNetUnits ELSE 0 END) AS Fall2015Units,
			SUM(MagNetSales + GiftNetSales + JewelryNetSales + CookieDoughNetSales + CandleNetSales + TrtNetSales + EntertainmentNetSales) AS Total2015NetSales,
			SUM(MagNetUnits + GiftNetUnits + JewelryNetUnits + CookieDoughNetUnits + CandleNetUnits + TrtNetUnits + EntertainmentNetUnits) AS Total2015Units
FROM		[dbo].[vw_GetNetSalesBySeasonSummary] s
JOIN		QSPCanadaCommon..FieldManager fm ON fm.FMID = s.FMID
WHERE		Season IN ('Fall 2015', 'Spring 2015')
GROUP BY	s.FMID,
			s.RepName,
			fm.SAPAcctNo
--ORDER BY	RepName
GO
