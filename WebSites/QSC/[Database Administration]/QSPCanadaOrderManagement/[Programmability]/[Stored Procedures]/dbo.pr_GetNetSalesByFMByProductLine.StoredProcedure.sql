USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GetNetSalesByFMByProductLine]    Script Date: 06/07/2017 09:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_GetNetSalesByFMByProductLine]

	@FMID						VARCHAR(4) = NULL,
	@DMID						VARCHAR(4) = NULL,
	@FromDate					DATETIME,
	@ToDate						DATETIME,
	@PriorFromDate				DATETIME,
	@PriorToDate				DATETIME,
	@IncludeExcludedCampaigns	BIT = 0,
	@IncludeInactiveFMs			BIT = 1,
	@IncludeWIP					BIT = 0,
	@IncludeBDCReferredAccounts	BIT = 1,
	@IncludeAdjustments			BIT = 1,
	@GroupByDM					BIT = 1

AS

SET @ToDate = DATEADD(dd, 1, @ToDate)
SET @PriorToDate = DATEADD(dd, 1, @PriorToDate)

SELECT		fm.FirstName FMFirstName,
			fm.LastName FMLastName,
			dm.FirstName DMFirstName,
			dm.LastName DMLastName,
			fm.SAPAcctNo SAPAcctNo,
			fm.FMID FMID,
			SUM(CASE WHEN pst.ID IN (1,16,17,18) AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) CurrentGiftNetSales,
			SUM(CASE WHEN pst.ID IN (1,16,17,18) AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) CurrentGiftUnits,
			SUM(CASE WHEN pst.ID = 2 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) CurrentMagazineNetSales,
			SUM(CASE WHEN pst.ID = 2 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) CurrentMagazineUnits,
			SUM(CASE WHEN pst.ID = 9 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) CurrentCookieDoughNetSales,
			SUM(CASE WHEN pst.ID = 9 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) CurrentCookieDoughUnits,
			SUM(CASE WHEN pst.ID = 10 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) CurrentPopcornNetSales,
			SUM(CASE WHEN pst.ID = 10 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) CurrentPopcornUnits,
			SUM(CASE WHEN pst.ID = 11 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) CurrentJewelryNetSales,
			SUM(CASE WHEN pst.ID = 11 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) CurrentJewelryUnits,
			SUM(CASE WHEN pst.ID = 15 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) CurrentDiscountCardNetSales,
			SUM(CASE WHEN pst.ID = 15 AND v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) CurrentDiscountCardUnits,
			SUM(CASE WHEN v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) CurrentTotalNetSales,
			SUM(CASE WHEN v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) CurrentTotalUnits,
			SUM(CASE WHEN pst.ID IN (1,16,17,18) AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) PriorGiftNetSales,
			SUM(CASE WHEN pst.ID IN (1,16,17,18) AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) PriorGiftUnits,
			SUM(CASE WHEN pst.ID = 2 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) PriorMagazineNetSales,
			SUM(CASE WHEN pst.ID = 2 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) PriorMagazineUnits,
			SUM(CASE WHEN pst.ID = 9 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) PriorCookieDoughNetSales,
			SUM(CASE WHEN pst.ID = 9 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) PriorCookieDoughUnits,
			SUM(CASE WHEN pst.ID = 10 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) PriorPopcornNetSales,
			SUM(CASE WHEN pst.ID = 10 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) PriorPopcornUnits,
			SUM(CASE WHEN pst.ID = 11 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) PriorJewelryNetSales,
			SUM(CASE WHEN pst.ID = 11 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) PriorJewelryUnits,
			SUM(CASE WHEN pst.ID = 14 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) PriorTRTNetSales,
			SUM(CASE WHEN pst.ID = 14 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) PriorTRTUnits,
			SUM(CASE WHEN pst.ID = 15 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) PriorDiscountCardNetSales,
			SUM(CASE WHEN pst.ID = 15 AND v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) PriorDiscountCardUnits,
			SUM(CASE WHEN v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) PriorTotalNetSales,
			SUM(CASE WHEN v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) PriorTotalUnits,
			SUM(CASE WHEN pst.ID IN (1,16,17,18) AND v.IsInvoiced = 0 THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) UninvoicedGiftNetSales,
			SUM(CASE WHEN pst.ID IN (1,16,17,18) AND v.IsInvoiced = 0 THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) UninvoicedGiftUnits,
			SUM(CASE WHEN pst.ID = 2 AND v.IsInvoiced = 0 THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) UninvoicedMagazineNetSales,
			SUM(CASE WHEN pst.ID = 2 AND v.IsInvoiced = 0 THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) UninvoicedMagazineUnits,
			SUM(CASE WHEN pst.ID = 9 AND v.IsInvoiced = 0 THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) UninvoicedCookieDoughNetSales,
			SUM(CASE WHEN pst.ID = 9 AND v.IsInvoiced = 0 THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) UninvoicedCookieDoughUnits,
			SUM(CASE WHEN pst.ID = 10 AND v.IsInvoiced = 0 THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) UninvoicedPopcornNetSales,
			SUM(CASE WHEN pst.ID = 10 AND v.IsInvoiced = 0 THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) UninvoicedPopcornUnits,
			SUM(CASE WHEN pst.ID = 11 AND v.IsInvoiced = 0 THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) UninvoicedJewelryNetSales,
			SUM(CASE WHEN pst.ID = 11 AND v.IsInvoiced = 0 THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) UninvoicedJewelryUnits,
			SUM(CASE WHEN pst.ID = 15 AND v.IsInvoiced = 0 THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) UninvoicedDiscountCardNetSales,
			SUM(CASE WHEN pst.ID = 15 AND v.IsInvoiced = 0 THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) UninvoicedDiscountCardUnits,
			SUM(CASE WHEN v.IsInvoiced = 0 THEN ISNULL(v.NetSale, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) UninvoicedTotalNetSales,
			SUM(CASE WHEN v.IsInvoiced = 0 THEN ISNULL(v.Units, 0) * ISNULL(fma.Percentage/100.00, 1) ELSE 0 END) UninvoicedTotalUnits,
			--CASE SUM(CASE WHEN v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) WHEN 0 THEN NULL ELSE (SUM(CASE WHEN v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.NetSale, 0) ELSE 0.00 END) - SUM(CASE WHEN v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END)) / SUM(CASE WHEN v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.NetSale, 0) ELSE 0 END) * 100.00 END NetSalesGrowthPercentage,
			--CASE SUM(CASE WHEN v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0 END) WHEN 0 THEN NULL ELSE (SUM(CASE WHEN v.invoice_date BETWEEN @FromDate AND @ToDate THEN ISNULL(v.Units, 0) ELSE 0.00 END) - SUM(CASE WHEN v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0 END)) / SUM(CASE WHEN v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN ISNULL(v.Units, 0) ELSE 0.00 END) * 100.00 END UnitsGrowthPercentage
			CASE cup.Locked WHEN 0 THEN 'Active' ELSE 'Inactive' END AS IsFMActive
FROM		QSPCanadaFinance..vw_GetNetForReporting v
JOIN		QSPCanadaCommon..Campaign camp WITH (NOLOCK) ON camp.ID = v.CampaignID
JOIN		QSPCanadaCommon..CAccount acc WITH (NOLOCK) ON acc.ID = camp.BillToAccountID
JOIN		QSPCanadaCommon..Season seas WITH (NOLOCK) ON camp.StartDate BETWEEN seas.StartDate AND seas.EndDate AND seas.Season IN ('F','S')
LEFT JOIN	QSPCanadaProduct..ProgramSectionType pst WITH (NOLOCK) ON pst.ID = v.section_type_id
--JOIN		QSPCanadaCommon..FieldManager fm WITH (NOLOCK) ON fm.FMID = camp.FMID
--JOIN		QSPCanadaCommon..FieldManager fm WITH (NOLOCK) ON fm.FMID = CASE WHEN v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate THEN QSPCanadaCommon.dbo.UDF_Account_GetFMID(camp.BillToAccountID, DATEADD(dd, -1, @ToDate)) ELSE camp.FMID END
--JOIN		QSPCanadaCommon..FieldManager fm WITH (NOLOCK) ON fm.FMID = CASE WHEN (v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate) AND (v.orderqualifierid NOT IN (39009) OR v.invoice_date < DATEADD(dd, 1, seas.EndDate)) THEN QSPCanadaCommon.dbo.UDF_Account_GetFMID(camp.BillToAccountID, DATEADD(yy, 1, v.invoice_date)) ELSE camp.FMID END
JOIN		QSPCanadaCommon..FieldManager fmI WITH (NOLOCK) ON fmI.FMID = CASE WHEN (v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate) AND (v.orderqualifierid NOT IN (39009) OR v.invoice_date < DATEADD(dd, 1, seas.EndDate)) THEN camp.SalesBaseFMID ELSE camp.FMID END
LEFT JOIN	QSPCanadaCommon..FieldManagerAssociate fma ON fma.AssociateFMID = fmI.FMID AND v.Invoice_Date <= fma.EffectiveToDate
JOIN		QSPCanadaCommon..FieldManager fm WITH (NOLOCK) ON fm.FMID = ISNULL(fma.FMID, fmI.FMID)
JOIN		QSPCanadaCommon..FieldManager dm WITH (NOLOCK) ON dm.FMID = CASE @GroupByDM WHEN 0 THEN '1552' ELSE CASE fm.DMIndicator WHEN 'N' THEN fm.DMID ELSE fm.FMID END END
LEFT JOIN	QSPCanadaCommon..CUserProfile cup WITH (NOLOCK) ON cup.FMNumber = fm.FMID
WHERE		(pst.ID IN (1,2,9,10,11,14,15,16,17,18) OR pst.ID IS NULL)
AND			fm.FMID NOT IN ('0508')
AND			(@IncludeInactiveFMs = 1 OR (cup.Locked = 0 AND dm.FMID NOT IN ('0506') AND fm.FMID NOT IN ('1552','1538')))
AND			fm.FMID = ISNULL(@FMID, fm.FMID)
AND			dm.FMID = ISNULL(@DMID, dm.FMID)
AND			(v.invoice_date BETWEEN @FromDate AND @ToDate OR v.invoice_date BETWEEN @PriorFromDate AND @PriorToDate OR v.IsInvoiced = 0)
AND			(ISNULL(@IncludeExcludedCampaigns, 0) = 1 OR camp.ExcludeFromSalesBase = 0)
AND			(@IncludeWIP = 1 OR v.IsInvoiced = 1)
AND			(ISNULL(@IncludeBDCReferredAccounts, 0) = 1 OR ISNULL(acc.ParentID, 0) NOT IN (34838))
AND			(@IncludeAdjustments = 1 OR v.section_type_id IS NOT NULL)
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

GO
