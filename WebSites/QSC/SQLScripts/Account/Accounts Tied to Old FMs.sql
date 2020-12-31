USE [QSPCanadaOrderManagement]
GO

/*
SELECT *
FROM QSPCanadaCommon..FieldManager
WHERE Firstname = 'claire'
*/

DECLARE
	@FromDate		DATETIME,
	@ToDate			DATETIME
	
SET @FromDate = '2016-01-01'
SET @ToDate = '2018-12-31'

SELECT		fm.FirstName FMFirstName,
			fm.LastName FMLastName,
			dm.FirstName DMFirstName,
			dm.LastName DMLastName,
			fm.SAPAcctNo SAPAcctNo,
			fm.FMID FMID,
			acc.ID AccountID,
			acc.Name AccountName,
			camp.ID CampaignID,
			camp.StartDate,
			camp.EndDate,
			SUM(CASE WHEN pst.ID = 1 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) GiftNetSales,
			SUM(CASE WHEN pst.ID = 1 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) ELSE 0 END) GiftUnits,
			SUM(CASE WHEN pst.ID = 2 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) MagazineNetSales,
			SUM(CASE WHEN pst.ID = 2 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) ELSE 0 END) MagazineUnits,
			SUM(CASE WHEN pst.ID = 9 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) CookieDoughNetSales,
			SUM(CASE WHEN pst.ID = 9 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) ELSE 0 END) CookieDoughUnits,
			SUM(CASE WHEN pst.ID = 10 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) PopcornNetSales,
			SUM(CASE WHEN pst.ID = 10 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) ELSE 0 END) PopcornUnits,
			SUM(CASE WHEN pst.ID = 11 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) JewelryNetSales,
			SUM(CASE WHEN pst.ID = 11 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) ELSE 0 END) JewelryUnits,
			SUM(CASE WHEN pst.ID = 13 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) CandleNetSales,
			SUM(CASE WHEN pst.ID = 13 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) ELSE 0 END) CandleUnits,
			SUM(CASE WHEN pst.ID = 14 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) TRTNetSales,
			SUM(CASE WHEN pst.ID = 14 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) ELSE 0 END) TRTUnits,
			SUM(CASE WHEN pst.ID = 15 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) EntertainmentNetSales,
			SUM(CASE WHEN pst.ID = 15 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) ELSE 0 END) EntertainmentUnits,
			SUM(CASE WHEN v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) TotalNetSales,
			SUM(CASE WHEN v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) ELSE 0 END) TotalUnits,
			CASE cup.Locked WHEN 0 THEN 'Active' ELSE 'Inactive' END AS IsFMActive
FROM		QSPCanadaFinance..vw_GetNetForReporting v
JOIN		QSPCanadaCommon..Campaign camp WITH (NOLOCK) ON camp.ID = v.CampaignID
JOIN		QSPCanadaCommon..CAccount acc WITH (NOLOCK) ON acc.ID = camp.BillToAccountID
JOIN		QSPCanadaCommon..Season seas WITH (NOLOCK) ON camp.StartDate BETWEEN seas.StartDate AND seas.EndDate AND seas.Season IN ('F','S')
JOIN		QSPCanadaProduct..ProgramSectionType pst WITH (NOLOCK) ON pst.ID = v.section_type_id
--JOIN		QSPCanadaCommon..FieldManager fm WITH (NOLOCK) ON fm.FMID = camp.FMID
--JOIN		QSPCanadaCommon..FieldManager fm WITH (NOLOCK) ON fm.FMID = CASE WHEN v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN QSPCanadaCommon.dbo.UDF_Account_GetFMID(camp.BillToAccountID, DATEADD(dd, -1, @ToDate)) ELSE camp.FMID END
JOIN		QSPCanadaCommon..FieldManager fm WITH (NOLOCK) ON fm.FMID =  QSPCanadaCommon.dbo.UDF_Account_GetFMID(camp.BillToAccountID, '2018-12-30')
--JOIN		QSPCanadaCommon..FieldManager fm WITH (NOLOCK) ON fm.FMID = camp.SalesBaseFMID 
JOIN		QSPCanadaCommon..FieldManager dm WITH (NOLOCK) ON dm.FMID = CASE fm.DMIndicator WHEN 'N' THEN fm.DMID ELSE fm.FMID END
JOIN		QSPCanadaCommon..CUserProfile cup WITH (NOLOCK) ON cup.FMNumber = fm.FMID
WHERE		pst.ID IN (1,2,9,10,11,13,14,15) -- Gift, Mag, CD, Jewelry, Candle, Trt, Entertainment
AND			( v.invoice_date BETWEEN @FromDate AND @ToDate)
AND			cup.Locked <> 0 
AND			acc.caccountcodeclass <> 'FM'
AND			camp.StartDate IN (select top 1 max(StartDate) from qspcanadacommon..Campaign c where c.BillToAccountID = camp.BillToAccountID)--group by billtoaccountid order by startdate desc)
GROUP BY	fm.FirstName,
			fm.LastName,
			fm.SAPAcctNo,
			fm.FMID,
			dm.FirstName,
			dm.LastName,
			cup.Locked,
			acc.ID,
			acc.Name,
			camp.ID,
			camp.StartDate,
			camp.EndDate
ORDER BY	dm.FirstName,
			dm.LastName,
			fm.FirstName,
			fm.LastName,
			camp.StartDate
