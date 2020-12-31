USE [QSPCanadaOrderManagement]
GO

declare
	@FMID			VARCHAR(4),
	@DMID			VARCHAR(4),
	@FromDate		DATETIME,
	@ToDate			DATETIME,
	@PriorFromDate	DATETIME,
	@PriorToDate	DATETIME,
	@IncludeExcludedCampaigns BIT

set @fmid = null
set @dmid = null
set @FromDate = '2017-07-01'
set @ToDate = '2017-12-31'
set @PriorFromDate = '1995-01-01'
set @PriorToDate = '1995-01-01'
set @IncludeExcludedCampaigns = 0

SET @ToDate = DATEADD(dd, 1, @ToDate)
SET @PriorToDate = DATEADD(dd, 1, @PriorToDate)

SELECT		fm.FirstName FMFirstName,
			fm.LastName FMLastName,
			dm.FirstName DMFirstName,
			dm.LastName DMLastName,
			fm.SAPAcctNo SAPAcctNo,
			fm.FMID FMID,
			--SUM(CASE WHEN pst.ID = 1 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) CurrentGiftNetSales,
			--SUM(CASE WHEN pst.ID = 1 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) ELSE 0 END) CurrentGiftUnits,

SUM(CASE WHEN pst.ID = 1 AND v.invoice_date BETWEEN @FromDate AND @ToDate AND v.ProgramType IN (30328) THEN ISNULL(v.NetSale, 0) ELSE 0 END) CurrentNaturallyGoodNetSales,
SUM(CASE WHEN pst.ID = 1 AND v.invoice_date BETWEEN @FromDate AND @ToDate AND v.ProgramType IN (30328) THEN ISNULL(v.Units, 0) ELSE 0 END) CurrentNaturallyGoodUnits,

SUM(CASE WHEN pst.ID = 1 AND v.invoice_date BETWEEN @FromDate AND @ToDate AND v.ProgramType IN (30324) THEN ISNULL(v.NetSale, 0) ELSE 0 END) CurrentFestivalNetSales,
SUM(CASE WHEN pst.ID = 1 AND v.invoice_date BETWEEN @FromDate AND @ToDate AND v.ProgramType IN (30324) THEN ISNULL(v.Units, 0) ELSE 0 END) CurrentFestivalUnits,

SUM(CASE WHEN pst.ID IN (1,11) AND v.invoice_date BETWEEN @FromDate AND @ToDate AND v.ProgramType IN (30323) THEN ISNULL(v.NetSale, 0) ELSE 0 END) CurrentGiftsWeLoveNetSales,
SUM(CASE WHEN pst.ID IN (1,11) AND v.invoice_date BETWEEN @FromDate AND @ToDate AND v.ProgramType IN (30323) THEN ISNULL(v.Units, 0) ELSE 0 END) CurrentGiftsWeLoveUnits,

SUM(CASE WHEN pst.ID = 1 AND v.invoice_date BETWEEN @FromDate AND @ToDate AND v.ProgramType IN (30329) THEN ISNULL(v.NetSale, 0) ELSE 0 END) CurrentLifeIsSweetNetSales,
SUM(CASE WHEN pst.ID = 1 AND v.invoice_date BETWEEN @FromDate AND @ToDate AND v.ProgramType IN (30329) THEN ISNULL(v.Units, 0) ELSE 0 END) CurrentLifeIsSweetUnits,

SUM(CASE WHEN pst.ID = 1 AND v.invoice_date BETWEEN @FromDate AND @ToDate AND v.ProgramType IN (30326) THEN ISNULL(v.NetSale, 0) ELSE 0 END) CurrentKitchenCollectionNetSales,
SUM(CASE WHEN pst.ID = 1 AND v.invoice_date BETWEEN @FromDate AND @ToDate AND v.ProgramType IN (30326) THEN ISNULL(v.Units, 0) ELSE 0 END) CurrentKitchenCollectionUnits,

SUM(CASE WHEN pst.ID = 11 AND v.invoice_date BETWEEN @FromDate AND @ToDate AND v.ProgramType IN (30325) THEN ISNULL(v.NetSale, 0) ELSE 0 END) CurrentOrganicEdiblesNetSales,
SUM(CASE WHEN pst.ID = 11 AND v.invoice_date BETWEEN @FromDate AND @ToDate AND v.ProgramType IN (30325) THEN ISNULL(v.Units, 0) ELSE 0 END) CurrentOrganicEdiblesUnits,

			SUM(CASE WHEN pst.ID = 2 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) CurrentMagazineNetSales,
			SUM(CASE WHEN pst.ID = 2 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) ELSE 0 END) CurrentMagazineUnits,
			SUM(CASE WHEN pst.ID = 9 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) CurrentCookieDoughNetSales,
			SUM(CASE WHEN pst.ID = 9 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) ELSE 0 END) CurrentCookieDoughUnits,
			SUM(CASE WHEN pst.ID = 10 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) CurrentPopcornNetSales,
			SUM(CASE WHEN pst.ID = 10 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) ELSE 0 END) CurrentPopcornUnits,
			--SUM(CASE WHEN pst.ID = 11 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) CurrentJewelryNetSales,
			--SUM(CASE WHEN pst.ID = 11 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) ELSE 0 END) CurrentJewelryUnits,
			SUM(CASE WHEN pst.ID = 14 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) CurrentTRTNetSales,
			SUM(CASE WHEN pst.ID = 14 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) ELSE 0 END) CurrentTRTUnits,
			SUM(CASE WHEN v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) CurrentTotalNetSales,
			CASE cup.Locked WHEN 0 THEN 'Active' ELSE 'Inactive' END AS IsFMActive
FROM		QSPCanadaFinance..vw_GetNetForReporting v
JOIN		QSPCanadaCommon..Campaign camp WITH (NOLOCK) ON camp.ID = v.CampaignID
JOIN		QSPCanadaCommon..Season seas WITH (NOLOCK) ON camp.StartDate BETWEEN seas.StartDate AND seas.EndDate AND seas.Season IN ('F','S')
JOIN		QSPCanadaProduct..ProgramSectionType pst WITH (NOLOCK) ON pst.ID = v.section_type_id
--JOIN		QSPCanadaCommon..FieldManager fm WITH (NOLOCK) ON fm.FMID = camp.FMID
--JOIN		QSPCanadaCommon..FieldManager fm WITH (NOLOCK) ON fm.FMID = CASE WHEN v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN QSPCanadaCommon.dbo.UDF_Account_GetFMID(camp.BillToAccountID, DATEADD(dd, -1, @ToDate)) ELSE camp.FMID END
JOIN		QSPCanadaCommon..FieldManager fm WITH (NOLOCK) ON fm.FMID = CASE WHEN (v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate) AND (v.orderqualifierid NOT IN (39009) OR v.invoice_date < DATEADD(dd, 1, seas.EndDate)) THEN QSPCanadaCommon.dbo.UDF_Account_GetFMID(camp.BillToAccountID, DATEADD(yy, 1, v.invoice_date)) ELSE camp.FMID END
JOIN		QSPCanadaCommon..FieldManager dm WITH (NOLOCK) ON dm.FMID = CASE fm.DMIndicator WHEN 'N' THEN fm.DMID ELSE fm.FMID END
LEFT JOIN	QSPCanadaCommon..CUserProfile cup WITH (NOLOCK) ON cup.FMNumber = fm.FMID
WHERE		pst.ID IN (1,2,9,10,11,13,14,15) -- Gift, Mag, CD, Jewelry, Candle, Trt, Entertainment
AND			fm.FMID NOT IN ('0508')
AND			fm.FMID = ISNULL(@FMID, fm.FMID)
AND			dm.FMID = ISNULL(@DMID, dm.FMID)
AND			(v.invoice_date BETWEEN @FromDate AND @ToDate OR v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate)
AND			(ISNULL(@IncludeExcludedCampaigns, 0) = 1 OR camp.ExcludeFromSalesBase = 0)
GROUP BY	fm.FirstName,
			fm.LastName,
			fm.SAPAcctNo,
			fm.FMID,
			dm.FirstName,
			dm.LastName,
			cup.Locked
ORDER BY	dm.FirstName,
			dm.LastName,
			fm.FirstName,
			fm.LastName
