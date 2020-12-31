USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_GetNetSalesBySeason]    Script Date: 06/07/2017 09:18:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_GetNetSalesBySeason] AS
SELECT fm.fmid, fm.LastName + ',' + fm.FirstName AS RepName, 
ps.ID AS section_type_id, 
ps.Description AS ProductType,
seas.Name AS Season,
seas.ID AS SeasonID,
v.invoice_date,
ISNULL(v.NetSale,0)/* * ISNULL(ccs.CommissionPercentage/100.00, 1)*/ AS NetSales,
ISNULL(v.Units,0)/* * ISNULL(ccs.CommissionPercentage/100.00, 1)*/ AS NetUnits 
FROM QSpCanadaFinance.dbo.vw_GetNetForReporting v
JOIN QSPCanadaCommon.dbo.Campaign AS c WITH(NOLOCK) ON c.ID = v.campaignid
JOIN QSPCanadaProduct.dbo.programsectiontype AS ps WITH(NOLOCK) ON ps.ID = v.section_type_id
/*LEFT JOIN	(QSPCanadaCommon..CampaignCommissionSplit ccs
JOIN		QSPCanadaCommon..FieldManager fmSplit
				ON	fmSplit.FMID = ccs.FMID)
				ON	ccs.CampaignID = c.ID*/
--JOIN QSPCanadaCommon.dbo.FieldManager AS fm WITH(NOLOCK) ON fm.FMID = c.FMID --ISNULL(fmSplit.FMID, c.FMID)
JOIN QSPCanadaCommon..FieldManager fm WITH(NOLOCK) ON fm.FMID = QSPCanadaCommon.dbo.UDF_Account_GetFMID(c.BillToAccountID, '2015-12-31')--DATEADD(mm, -1, GETDATE()))
JOIN QSPCanadaCommon..Season seas WITH(NOLOCK) ON v.invoice_date BETWEEN seas.StartDate AND DATEADD(dd, 1, seas.EndDate) AND seas.Season IN ('F', 'S')
WHERE ps.ID IN  (1,2,9,11,13,14,15) -- Gift, Mag, CD, Jewelry, Candle, Trt, Entertainment
AND fm.FMID NOT IN (1538, 508,510, 97,503)
GO
