USE [QSPCanadaOrderManagement]
GO

/****** Object:  View [dbo].[vw_InventoryReportingNetSales]    Script Date: 08/21/2017 14:59:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP VIEW [dbo].[vw_InventoryReportingNetSales]
GO

CREATE View [dbo].[vw_InventoryReportingNetSales] AS 
SELECT		fm.FirstName FMFirstName,
			fm.LastName FMLastName,
			dm.FirstName DMFirstName,
			dm.LastName DMLastName,
			fm.SAPAcctNo SAPAcctNo,
			fm.FMID FMID,
			dm.FMID DFMID,
			v.invoice_date, 
			SUM(CASE WHEN pst.ID = 1  THEN ISNULL(v.NetSale, 0) ELSE 0 END) GiftNetSales,
			SUM(CASE WHEN pst.ID = 1  THEN ISNULL(v.Units, 0) ELSE 0 END) GiftUnits,
			SUM(CASE WHEN pst.ID = 1   AND v.ProgramType IN (30328) THEN ISNULL(v.NetSale, 0) ELSE 0 END) NaturallyGoodNetSales,
			SUM(CASE WHEN pst.ID = 1   AND v.ProgramType IN (30328) THEN ISNULL(v.Units, 0) ELSE 0 END) NaturallyGoodUnits,
			SUM(CASE WHEN pst.ID = 1   AND v.ProgramType IN (30324) THEN ISNULL(v.NetSale, 0) ELSE 0 END) FestivalNetSales,
			SUM(CASE WHEN pst.ID = 1   AND v.ProgramType IN (30324) THEN ISNULL(v.Units, 0) ELSE 0 END) FestivalUnits,
			SUM(CASE WHEN pst.ID = 1   AND v.ProgramType IN (30323) THEN ISNULL(v.NetSale, 0) ELSE 0 END) GiftsWeLoveNetSales,
			SUM(CASE WHEN pst.ID = 1   AND v.ProgramType IN (30323) THEN ISNULL(v.Units, 0) ELSE 0 END) GiftsWeLoveUnits,
			SUM(CASE WHEN pst.ID = 1   AND v.ProgramType IN (30329) THEN ISNULL(v.NetSale, 0) ELSE 0 END) LifeIsSweetNetSales,
			SUM(CASE WHEN pst.ID = 1   AND v.ProgramType IN (30329) THEN ISNULL(v.Units, 0) ELSE 0 END) LifeIsSweetUnits,
			SUM(CASE WHEN pst.ID = 1   AND v.ProgramType IN (30326) THEN ISNULL(v.NetSale, 0) ELSE 0 END) KitchenCollectionNetSales,
			SUM(CASE WHEN pst.ID = 1   AND v.ProgramType IN (30326) THEN ISNULL(v.Units, 0) ELSE 0 END) KitchenCollectionUnits,
			SUM(CASE WHEN pst.ID = 1  AND v.ProgramType IN (30327) THEN ISNULL(v.NetSale, 0) ELSE 0 END) DonationSales,
			SUM(CASE WHEN pst.ID = 1  AND v.ProgramType IN (30327) THEN ISNULL(v.Units, 0) ELSE 0 END) DonationUnits,
			SUM(CASE WHEN pst.ID = 1  AND v.ProgramType IN (30332) THEN ISNULL(v.NetSale, 0) ELSE 0 END) TumblerSales,
			SUM(CASE WHEN pst.ID = 1  AND v.ProgramType IN (30332) THEN ISNULL(v.Units, 0) ELSE 0 END) TumblerUnits,
			SUM(CASE WHEN pst.ID = 11  AND v.ProgramType IN (30325) THEN ISNULL(v.NetSale, 0) ELSE 0 END) OrganicEdiblesNetSales,
			SUM(CASE WHEN pst.ID = 11  AND v.ProgramType IN (30325) THEN ISNULL(v.Units, 0) ELSE 0 END) OrganicEdiblesUnits,
			SUM(CASE WHEN pst.ID = 2  THEN ISNULL(v.NetSale, 0) ELSE 0 END) MagazineNetSales,
			SUM(CASE WHEN pst.ID = 2  THEN ISNULL(v.Units, 0) ELSE 0 END) MagazineUnits,
			SUM(CASE WHEN pst.ID = 9  THEN ISNULL(v.NetSale, 0) ELSE 0 END) CookieDoughNetSales,
			SUM(CASE WHEN pst.ID = 9  THEN ISNULL(v.Units, 0) ELSE 0 END) CookieDoughUnits,
			SUM(CASE WHEN pst.ID = 10 THEN ISNULL(v.NetSale, 0) ELSE 0 END) PopcornNetSales,
			SUM(CASE WHEN pst.ID = 10 THEN ISNULL(v.Units, 0) ELSE 0 END) PopcornUnits,
			SUM(CASE WHEN pst.ID = 11 AND v.ProgramType != (30325) THEN ISNULL(v.NetSale, 0) ELSE 0 END) JewelryNetSales,
			SUM(CASE WHEN pst.ID = 11 AND v.ProgramType != (30325) THEN ISNULL(v.Units, 0) ELSE 0 END) JewelryUnits,
			SUM(CASE WHEN pst.ID = 14 THEN ISNULL(v.NetSale, 0) ELSE 0 END) TRTNetSales,
			SUM(CASE WHEN pst.ID = 14 THEN ISNULL(v.Units, 0) ELSE 0 END) TRTUnits,
			SUM(ISNULL(v.NetSale, 0)) TotalNetSales,
			SUM(ISNULL(v.Units, 0)) TotalUnits,
			CASE cup.Locked WHEN 0 THEN 'Active' ELSE 'Inactive' END AS IsFMActive
FROM		QSPCanadaFinance..vw_GetNetForReporting v
JOIN		QSPCanadaCommon..Campaign camp WITH (NOLOCK) ON camp.ID = v.CampaignID
JOIN		QSPCanadaCommon..Season seas WITH (NOLOCK) ON camp.StartDate BETWEEN seas.StartDate AND seas.EndDate AND seas.Season IN ('F','S')
JOIN		QSPCanadaProduct..ProgramSectionType pst WITH (NOLOCK) ON pst.ID = v.section_type_id
JOIN		QSPCanadaCommon..FieldManager fm WITH (NOLOCK) ON fm.FMID = camp.FMID
JOIN		QSPCanadaCommon..FieldManager dm WITH (NOLOCK) ON dm.FMID = CASE fm.DMIndicator WHEN 'N' THEN fm.DMID ELSE fm.FMID END
LEFT JOIN	QSPCanadaCommon..CUserProfile cup WITH (NOLOCK) ON cup.FMNumber = fm.FMID
WHERE		pst.ID IN (1,2,9,10,11,13,14,15) -- Gift, Mag, CD, Jewelry, Candle, Trt, Entertainment
AND			fm.FMID <> '0508'
GROUP BY	fm.FirstName,
			fm.LastName,
			fm.SAPAcctNo,
			fm.FMID,
			dm.FMID,
			v.invoice_date,
			dm.FirstName,
			dm.LastName,
			cup.Locked


GO

