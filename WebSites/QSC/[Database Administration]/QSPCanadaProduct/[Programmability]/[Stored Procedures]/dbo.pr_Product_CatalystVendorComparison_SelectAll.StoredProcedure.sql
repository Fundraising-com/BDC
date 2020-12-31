USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_Product_CatalystVendorComparison_SelectAll]    Script Date: 06/07/2017 09:17:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Product_CatalystVendorComparison_SelectAll]

AS

--Active Vendors that exist in the Fulfillment system but not in Catalyst
SELECT		p.VendorNumber
FROM		[QSPCanadaProduct]..Product p
LEFT JOIN	[CATALYST1]..NOETIX_ADMIN.[CAQPO_VENDOR_SITES] c
				ON	CAST(c.[Vendor_Number] AS varchar) = p.VendorNumber
JOIN		QSPCanadaCommon.dbo.Season s
				ON	s.Season = p.Product_Season
				AND	s.FiscalYear = p.Product_Year
				AND	GetDate() BETWEEN s.StartDate AND S.EndDate
				AND s.Season <> 'Y' 	
WHERE		c.[Vendor_Number] is null
AND			ISNULL(p.VendorNumber,'') <> ''
AND			p.Product_Year = 2008
AND			p.Status = 30600
AND			p.Type = 46001
GROUP BY	p.VendorNumber

--Active Vendors whose names match, but other info does not match (excluding Country code)
SELECT DISTINCT	p.product_sort_name AS Name,
				p.VendorNumber AS FulfillmentVendorNumber,
				c.[Vendor_Number] AS CatalystVendorNumber,
				CASE WHEN p.VendorNumber <> CONVERT(varchar,c.[Vendor_Number]) THEN CONVERT(varchar, p.VendorNumber) ELSE '' END AS FulfillmentVendorNumber,
				CASE WHEN p.VendorNumber <> CONVERT(varchar,c.[Vendor_Number]) THEN CONVERT(varchar, c.[Vendor_Number]) ELSE '' END AS CatalystVendorNumber,
				CASE WHEN p.PayGroupLookUpCode <> c.[Pay_Group_Lookup_Code] THEN CONVERT(varchar, p.PayGroupLookUpCode) ELSE '' END AS FulfillmentPayGroupLookUpCode,
				CASE WHEN p.PayGroupLookUpCode <> c.[Pay_Group_Lookup_Code] THEN CONVERT(varchar, c.[Pay_Group_Lookup_Code]) ELSE '' END AS CatalystPayGroupLookUpCode,
				CASE WHEN p.VendorSiteName <> c.[Vendor_Site_Code] THEN CONVERT(varchar, p.VendorSiteName) ELSE '' END AS FulfillmentVendorSiteName,
				CASE WHEN p.VendorSiteName <> c.[Vendor_Site_Code] THEN CONVERT(varchar, c.[Vendor_Site_Code]) ELSE '' END AS CatalystVendorSiteName,
				CASE WHEN (CASE p.Currency WHEN 801 THEN 'CAD' WHEN 802 THEN 'USD' ELSE 'unknown' END) <> c.[Payment_Currency_Code] THEN CONVERT(varchar, CASE p.Currency WHEN 801 THEN 'CAD' WHEN 802 THEN 'USD' ELSE 'unknown' END) ELSE '' END AS FulfillmentCurrency,
				CASE WHEN (CASE p.Currency WHEN 801 THEN 'CAD' WHEN 802 THEN 'USD' ELSE 'unknown' END) <> c.[Payment_Currency_Code] THEN CONVERT(varchar, c.[Payment_Currency_Code]) ELSE '' END AS CatalystPaymentCurrency,
				CASE WHEN (CASE p.Currency WHEN 801 THEN 'CAD' WHEN 802 THEN 'USD' ELSE 'unknown' END) <> c.[Invoice_Currency_Code] THEN CONVERT(varchar, CASE p.Currency WHEN 801 THEN 'CAD' WHEN 802 THEN 'USD' ELSE 'unknown' END) ELSE '' END AS FulfillmentCurrency,
				CASE WHEN (CASE p.Currency WHEN 801 THEN 'CAD' WHEN 802 THEN 'USD' ELSE 'unknown' END) <> c.[Invoice_Currency_Code] THEN CONVERT(varchar, c.[Invoice_Currency_Code]) ELSE '' END AS CatalystInvoiceCurrency,
				CASE WHEN fh.CountryCode <> c.Country THEN CONVERT(varchar, fh.CountryCode) ELSE '' END AS FulfillmentCountry,
				CASE WHEN fh.CountryCode <> c.Country THEN CONVERT(varchar, c.Country) ELSE '' END AS CatalystCountry
FROM			[QSPCanadaProduct]..Product p
JOIN			[CATALYST1]..NOETIX_ADMIN.[CAQPO_VENDOR_SITES] c
					ON	p.product_sort_name = c.[Vendor_Name]
JOIN			[QSPCanadaProduct]..Fulfillment_House fh
					ON	fh.Ful_Nbr = p.Fulfill_House_Nbr
JOIN			[QSPCanadaCommon]..Season s
				ON	s.Season = p.Product_Season
				AND	s.FiscalYear = p.Product_Year
				AND	GetDate() BETWEEN s.StartDate AND S.EndDate
				AND s.Season <> 'Y' 	
WHERE			p.Product_sort_name is not null
AND				c.[Inactive_Date] is null
AND				p.Status = 30600
AND				p.Type = 46001
AND				(CONVERT(varchar, p.VendorNumber) <> CONVERT(varchar,c.[Vendor_Number])
OR					p.PayGroupLookUpCode <> c.[Pay_Group_Lookup_Code]
OR					p.VendorSiteName <> c.[Vendor_Site_Code]
OR					c.[Payment_Currency_Code] <> CASE p.Currency WHEN 801 THEN 'CAD' WHEN 802 THEN 'USD' ELSE 'unknown' END
OR					c.[Invoice_Currency_Code] <> CASE p.Currency WHEN 801 THEN 'CAD' WHEN 802 THEN 'USD' ELSE 'unknown' END
--OR					p.CountryCode <> c.Country)
)

--Active Vendors whose Vendor Numbers match, but whose names do not match
SELECT DISTINCT p.VendorNumber,
				p.product_sort_name AS FulfillmentName,
				c.[Vendor_Name] AS CatalystName
FROM			[QSPCanadaProduct]..Product p
JOIN			[CATALYST1]..NOETIX_ADMIN.[CAQPO_VENDOR_SITES] c
					ON CONVERT(varchar, p.VendorNumber) = CONVERT(varchar,c.[Vendor_Number])
JOIN			[QSPCanadaCommon]..Season s
				ON	s.Season = p.Product_Season
				AND	s.FiscalYear = p.Product_Year
				AND	GetDate() BETWEEN s.StartDate AND S.EndDate
				AND s.Season <> 'Y' 	
WHERE			p.VendorNumber is not null
AND				p.Status = 30600
AND				p.Type = 46001
AND				p.Product_Sort_Name collate SQL_Latin1_General_CP1_CI_AI <> c.[Vendor_Name] collate SQL_Latin1_General_CP1_CI_AI
AND				c.[Inactive_Date] is null
GO
