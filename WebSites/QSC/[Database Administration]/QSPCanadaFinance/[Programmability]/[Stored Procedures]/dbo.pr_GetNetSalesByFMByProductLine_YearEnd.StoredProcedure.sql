USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[pr_GetNetSalesByFMByProductLine_YearEnd]    Script Date: 06/07/2017 09:17:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_GetNetSalesByFMByProductLine_YearEnd]

	@FallFromDate	DATETIME,
	@FallToDate		DATETIME,
	@SpringFromDate	DATETIME,
	@SpringToDate	DATETIME

AS

SET @FallToDate = DATEADD(dd, 1, @FallToDate)
SET @SpringToDate = DATEADD(dd, 1, @SpringToDate)

SELECT	   fm.FirstName FMFirstName,
			   fm.LastName FMLastName,
			   fm.FMID FMID,
			   SUM(ISNULL(v.NetSale, 0)) FallNetSales
INTO		   #Fall
FROM		   QSPCanadaFinance..vw_GetNetForReporting v
JOIN		   QSPCanadaCommon..Campaign camp WITH (NOLOCK) ON camp.ID = v.CampaignID
JOIN		   QSPCanadaProduct..ProgramSectionType pst WITH (NOLOCK) ON pst.ID = v.section_type_id
JOIN		   QSPCanadaCommon..FieldManager fm WITH (NOLOCK) ON fm.FMID = camp.FMID
WHERE		   pst.ID IN (1,2,9,10,11,13,14,15) -- Gift, Mag, CD, Jewelry, Candle, Trt, Entertainment
AND			fm.FMID NOT IN ('0508')
AND			v.invoice_date BETWEEN @FallFromDate AND @FallToDate
AND			v.IsInvoiced = 1
GROUP BY	   fm.FirstName,
			   fm.LastName,
   			fm.FMID

SELECT	   fm.FirstName FMFirstName,
			   fm.LastName FMLastName,
			   fm.FMID FMID,
			   SUM(ISNULL(v.NetSale, 0)) SpringNetSales
INTO		   #Spring
FROM		   QSPCanadaFinance..vw_GetNetForReporting v
JOIN		   QSPCanadaCommon..Campaign camp WITH (NOLOCK) ON camp.ID = v.CampaignID
JOIN		   QSPCanadaProduct..ProgramSectionType pst WITH (NOLOCK) ON pst.ID = v.section_type_id
JOIN		   QSPCanadaCommon..FieldManager fm WITH (NOLOCK) ON fm.FMID = camp.FMID
WHERE		   pst.ID IN (1,2,9,10,11,13,14,15) -- Gift, Mag, CD, Jewelry, Candle, Trt, Entertainment
AND			fm.FMID NOT IN ('0508')
AND			v.invoice_date BETWEEN @SpringFromDate AND @SpringToDate
AND			v.IsInvoiced = 1
GROUP BY	   fm.FirstName,
			   fm.LastName,
   			fm.FMID

SELECT	   fm.FirstName FMFirstName,
			   fm.LastName FMLastName,
			   fm.FMID FMID,
			   SUM(ISNULL(v.NetSale, 0)) TotalNetSales
INTO		   #Year
FROM		   QSPCanadaFinance..vw_GetNetForReporting v
JOIN		   QSPCanadaCommon..Campaign camp WITH (NOLOCK) ON camp.ID = v.CampaignID
JOIN		   QSPCanadaProduct..ProgramSectionType pst WITH (NOLOCK) ON pst.ID = v.section_type_id
JOIN		   QSPCanadaCommon..FieldManager fm WITH (NOLOCK) ON fm.FMID = camp.FMID
WHERE		   pst.ID IN (1,2,9,10,11,13,14,15) -- Gift, Mag, CD, Jewelry, Candle, Trt, Entertainment
AND			fm.FMID NOT IN ('0508')
AND			v.invoice_date BETWEEN @SpringFromDate AND @FallToDate
AND			v.IsInvoiced = 1
GROUP BY	   fm.FirstName,
			   fm.LastName,
   			fm.FMID

SELECT	   fm.FirstName + ' ' + fm.LastName RepName,
			   fm.SAPAcctNo,
			   ISNULL(cup.Locked, 0) Terminated,
			   SUM(ISNULL(s.SpringNetSales, 0.00)) SpringNetSales,
			   SUM(ISNULL(f.FallNetSales, 0.00)) FallNetSales,
			   SUM(ISNULL(y.TotalNetSales, 0.00)) TotalNetSales
FROM		   #Year y
JOIN		   QSPCanadaCommon..FieldManager fm WITH (NOLOCK) ON fm.FMID = y.FMID
LEFT JOIN	#Fall f ON f.FMID = fm.FMID
LEFT JOIN	#Spring s ON s.FMID = fm.FMID
LEFT JOIN	QSPCanadaCommon..CUserProfile cup ON cup.FMNumber = fm.FMID
GROUP BY	   fm.FirstName,
			   fm.LastName,
			   fm.SAPAcctNo,
			   fm.FMID,
			   cup.Locked
ORDER BY	   fm.FirstName,
			   fm.LastName

DROP TABLE #Fall
DROP TABLE #Spring
DROP TABLE #Year
GO
