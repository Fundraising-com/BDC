
declare
	@FMIDTo			VARCHAR(4),
	@FMIDFrom		VARCHAR(4),
	@DMID			VARCHAR(4),
	@PriorFromDate	DATETIME,
	@PriorToDate	DATETIME

set @FMIDFrom = null
set @FMIDTo = null
set @DMID = null
set @PriorFromDate = '2016-07-01'
set @PriorToDate = '2017-06-30'

SET @PriorToDate = DATEADD(dd, 1, @PriorToDate)

--Summary
SELECT		fm.FirstName TransferredToFMFirstName,
			fm.LastName TransferredToFMLastName,
			fmOrig.FirstName TransferredFromFMFirstName,
			fmOrig.LastName TransferredFromFMLastName,
			dm.FirstName DMFirstName,
			dm.LastName DMLastName,
			--acc.ID AccountID,
			--acc.Name AccountName,
			SUM(CASE WHEN v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) TotalNetSales,
			SUM(CASE WHEN v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0 END) TotalUnits,
			SUM(CASE WHEN pst.ID IN (1,16) AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) GiftNetSales,
			SUM(CASE WHEN pst.ID IN (1,16) AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0 END) GiftUnits,
			SUM(CASE WHEN pst.ID = 2 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) MagazineNetSales,
			SUM(CASE WHEN pst.ID = 2 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0 END) MagazineUnits,
			SUM(CASE WHEN pst.ID = 9 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) CookieDoughNetSales,
			SUM(CASE WHEN pst.ID = 9 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0 END) CookieDoughUnits,
			SUM(CASE WHEN pst.ID = 10 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) PopcornNetSales,
			SUM(CASE WHEN pst.ID = 10 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0 END) PopcornUnits,
			SUM(CASE WHEN pst.ID = 11 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) JewelryNetSales,
			SUM(CASE WHEN pst.ID = 11 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0 END) JewelryUnits,
			SUM(CASE WHEN pst.ID = 13 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) CandleNetSales,
			SUM(CASE WHEN pst.ID = 13 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0 END) CandleUnits,
			SUM(CASE WHEN pst.ID = 14 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) TRTNetSales,
			SUM(CASE WHEN pst.ID = 14 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0 END) TRTUnits,
			SUM(CASE WHEN pst.ID = 15 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) EntertainmentNetSales,
			SUM(CASE WHEN pst.ID = 15 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0 END) EntertainmentUnits
FROM		QSPCanadaFinance..vw_GetNetForReporting v
JOIN		QSPCanadaCommon..Campaign camp WITH (NOLOCK) ON camp.ID = v.CampaignID
JOIN		QSPCanadaCommon..Season seas WITH (NOLOCK) ON camp.StartDate BETWEEN seas.StartDate AND seas.EndDate AND seas.Season IN ('F','S')
JOIN		QSPCanadaCommon..CAccount acc WITH (NOLOCK) ON acc.ID = camp.BillToAccountID
JOIN		QSPCanadaProduct..ProgramSectionType pst WITH (NOLOCK) ON pst.ID = v.section_type_id
JOIN		QSPCanadaCommon..FieldManager fm WITH (NOLOCK) ON fm.FMID = camp.SalesBaseFMID
															AND camp.FMID <> fm.FMID
															AND (v.orderqualifierid NOT IN (39009) OR v.invoice_date < DATEADD(dd, 1, seas.EndDate))
JOIN		QSPCanadaCommon..FieldManager dm WITH (NOLOCK) ON dm.FMID = CASE fm.DMIndicator WHEN 'N' THEN fm.DMID ELSE fm.FMID END
JOIN		QSPCanadaCommon..FieldManager fmOrig WITH (NOLOCK) ON fmOrig.FMID = camp.FMID
WHERE		pst.ID IN (1,2,9,10,11,13,14,15,16) -- Gift, Mag, CD, Popcorn, Jewelry, Candle, Trt, Entertainment, Gift Card
AND			fm.FMID NOT IN ('0508')
AND			fm.FMID = ISNULL(@FMIDTo, fm.FMID)
AND			fmOrig.FMID = ISNULL(@FMIDFrom, fmOrig.FMID)
AND			dm.FMID = ISNULL(@DMID, dm.FMID)
AND			(v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate)
AND			camp.ExcludeFromSalesBase = 0
GROUP BY	fm.FirstName,
			fm.LastName,
			fm.SAPAcctNo,
			fm.FMID,
			fmOrig.FirstName,
			fmOrig.LastName,
			dm.FirstName,
			dm.LastName--,
			--acc.ID,
			--acc.Name
ORDER BY	fm.FirstName,
			fm.LastName--,
			--acc.ID
			
--Details
SELECT		fm.FirstName TransferredToFMFirstName,
			fm.LastName TransferredToFMLastName,
			fmOrig.FirstName TransferredFromFMFirstName,
			fmOrig.LastName TransferredFromFMLastName,
			dm.FirstName DMFirstName,
			dm.LastName DMLastName,
			acc.ID AccountID,
			acc.Name AccountName,
			camp.ID CampaignID,
			camp.DateModified TransferDate,
			--CASE camp.ExcludeFromSalesBase WHEN 1 THEN 'Yes' ELSE 'No' END ExludedFromSalesBase,
			SUM(CASE WHEN v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) TotalNetSales,
			SUM(CASE WHEN v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0 END) TotalUnits,
			SUM(CASE WHEN pst.ID = 1 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) GiftNetSales,
			SUM(CASE WHEN pst.ID = 1 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0 END) GiftUnits,
			SUM(CASE WHEN pst.ID = 2 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) MagazineNetSales,
			SUM(CASE WHEN pst.ID = 2 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0 END) MagazineUnits,
			SUM(CASE WHEN pst.ID = 9 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) CookieDoughNetSales,
			SUM(CASE WHEN pst.ID = 9 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0 END) CookieDoughUnits,
			SUM(CASE WHEN pst.ID = 10 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) PopcornNetSales,
			SUM(CASE WHEN pst.ID = 10 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0 END) PopcornUnits,
			SUM(CASE WHEN pst.ID = 11 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) JewelryNetSales,
			SUM(CASE WHEN pst.ID = 11 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0 END) JewelryUnits,
			SUM(CASE WHEN pst.ID = 13 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) CandleNetSales,
			SUM(CASE WHEN pst.ID = 13 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0 END) CandleUnits,
			SUM(CASE WHEN pst.ID = 14 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) TRTNetSales,
			SUM(CASE WHEN pst.ID = 14 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0 END) TRTUnits,
			SUM(CASE WHEN pst.ID = 15 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) EntertainmentNetSales,
			SUM(CASE WHEN pst.ID = 15 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0 END) EntertainmentUnits
FROM		QSPCanadaFinance..vw_GetNetForReporting v
JOIN		QSPCanadaCommon..Campaign camp WITH (NOLOCK) ON camp.ID = v.CampaignID
JOIN		QSPCanadaCommon..Season seas WITH (NOLOCK) ON camp.StartDate BETWEEN seas.StartDate AND seas.EndDate AND seas.Season IN ('F','S')
JOIN		QSPCanadaCommon..CAccount acc WITH (NOLOCK) ON acc.ID = camp.BillToAccountID
JOIN		QSPCanadaProduct..ProgramSectionType pst WITH (NOLOCK) ON pst.ID = v.section_type_id
JOIN		QSPCanadaCommon..FieldManager fm WITH (NOLOCK) ON fm.FMID = camp.SalesBaseFMID
															AND camp.FMID <> fm.FMID
															AND (v.orderqualifierid NOT IN (39009) OR v.invoice_date < DATEADD(dd, 1, seas.EndDate))
JOIN		QSPCanadaCommon..FieldManager dm WITH (NOLOCK) ON dm.FMID = CASE fm.DMIndicator WHEN 'N' THEN fm.DMID ELSE fm.FMID END
JOIN		QSPCanadaCommon..FieldManager fmOrig WITH (NOLOCK) ON fmOrig.FMID = camp.FMID
WHERE		pst.ID IN (1,2,9,10,11,13,14,15) -- Gift, Mag, CD, Popcorn, Jewelry, Candle, Trt, Entertainment
AND			fm.FMID NOT IN ('0508')
AND			fm.FMID = ISNULL(@FMIDTo, fm.FMID)
AND			fmOrig.FMID = ISNULL(@FMIDFrom, fmOrig.FMID)
AND			dm.FMID = ISNULL(@DMID, dm.FMID)
AND			(v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate)
AND			camp.ExcludeFromSalesBase = 0
GROUP BY	fm.FirstName,
			fm.LastName,
			fm.SAPAcctNo,
			fm.FMID,
			fmOrig.FirstName,
			fmOrig.LastName,
			dm.FirstName,
			dm.LastName,
			acc.ID,
			acc.Name,
			camp.ID,
			camp.DateModified,
			camp.ExcludeFromSalesBase
ORDER BY	fm.FirstName,
			fm.LastName,
			acc.ID