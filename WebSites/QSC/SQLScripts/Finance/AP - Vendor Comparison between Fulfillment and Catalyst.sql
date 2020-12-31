--Active Vendors that exist in the Fulfillment system but not in Catalyst
SELECT		p.VendorNumber,
			p.product_sort_name AS FulfillmentName
FROM		[QSPCanadaProduct]..Product p
LEFT JOIN	[TempDataWithoutBackup]..Vendor c
				ON	CAST(c.[Vendor Number] AS varchar) = p.VendorNumber
WHERE		c.[Vendor Number] is null
AND			ISNULL(p.VendorNumber,'') <> ''
AND			p.Product_Year = 2009
AND			p.Product_Season = 'F'
AND			p.Status = 30600
AND			p.Type = 46001
GROUP BY	p.VendorNumber,
			p.product_sort_name
select * from [TempDataWithoutBackup]..Vendor
--Active Vendors whose names match, but other info does not match (excluding Country code)
SELECT DISTINCT	p.product_sort_name AS Name,
				p.VendorNumber AS FulfillmentVendorNumber,
				c.[Vendor Number] AS CatalystVendorNumber,
				CASE WHEN p.VendorNumber <> CONVERT(varchar,c.[Vendor Number]) THEN CONVERT(varchar, p.VendorNumber) ELSE '' END AS FulfillmentVendorNumber,
				CASE WHEN p.VendorNumber <> CONVERT(varchar,c.[Vendor Number]) THEN CONVERT(varchar, c.[Vendor Number]) ELSE '' END AS CatalystVendorNumber,
				CASE WHEN p.PayGroupLookUpCode <> c.[Pay Group] THEN CONVERT(varchar, p.PayGroupLookUpCode) ELSE '' END AS FulfillmentPayGroupLookUpCode,
				CASE WHEN p.PayGroupLookUpCode <> c.[Pay Group] THEN CONVERT(varchar, c.[Pay Group]) ELSE '' END AS CatalystPayGroupLookUpCode,
				CASE WHEN p.VendorSiteName <> c.[Vendor Site] THEN CONVERT(varchar, p.VendorSiteName) ELSE '' END AS FulfillmentVendorSiteName,
				CASE WHEN p.VendorSiteName <> c.[Vendor Site] THEN CONVERT(varchar, c.[Vendor Site]) ELSE '' END AS CatalystVendorSiteName,
				CASE WHEN (CASE p.Currency WHEN 801 THEN 'CAD' WHEN 802 THEN 'USD' ELSE 'unknown' END) <> c.[Payment Currency] THEN CONVERT(varchar, CASE p.Currency WHEN 801 THEN 'CAD' WHEN 802 THEN 'USD' ELSE 'unknown' END) ELSE '' END AS FulfillmentCurrency,
				CASE WHEN (CASE p.Currency WHEN 801 THEN 'CAD' WHEN 802 THEN 'USD' ELSE 'unknown' END) <> c.[Payment Currency] THEN CONVERT(varchar, c.[Payment Currency]) ELSE '' END AS CatalystPaymentCurrency,
				CASE WHEN (CASE p.Currency WHEN 801 THEN 'CAD' WHEN 802 THEN 'USD' ELSE 'unknown' END) <> c.[Invoice Currency] THEN CONVERT(varchar, CASE p.Currency WHEN 801 THEN 'CAD' WHEN 802 THEN 'USD' ELSE 'unknown' END) ELSE '' END AS FulfillmentCurrency,
				CASE WHEN (CASE p.Currency WHEN 801 THEN 'CAD' WHEN 802 THEN 'USD' ELSE 'unknown' END) <> c.[Invoice Currency] THEN CONVERT(varchar, c.[Invoice Currency]) ELSE '' END AS CatalystInvoiceCurrency,
				CASE WHEN fh.CountryCode <> c.Country THEN CONVERT(varchar, fh.CountryCode) ELSE '' END AS FulfillmentCountry,
				CASE WHEN fh.CountryCode <> c.Country THEN CONVERT(varchar, c.Country) ELSE '' END AS CatalystCountry
FROM			[QSPCanadaProduct]..Product p
JOIN			[TempDataWithoutBackup]..Vendor c
					ON	p.product_sort_name = c.[Vendor Name]
JOIN			[QSPCanadaProduct]..Fulfillment_House fh
					ON	fh.Ful_Nbr = p.Fulfill_House_Nbr
WHERE			p.product_sort_name is not null
AND				P.Product_Year = 2009
AND				P.Product_Season = 'F'
AND				c.[Inactive Date] is null
AND				p.Status = 30600
AND				p.Type = 46001
AND				(CONVERT(varchar, p.VendorNumber) <> CONVERT(varchar,c.[Vendor Number])
OR					p.PayGroupLookUpCode <> c.[Pay Group]
OR					p.VendorSiteName <> c.[Vendor Site]
OR					c.[Payment Currency] <> CASE p.Currency WHEN 801 THEN 'CAD' WHEN 802 THEN 'USD' ELSE 'unknown' END
OR					c.[Invoice Currency] <> CASE p.Currency WHEN 801 THEN 'CAD' WHEN 802 THEN 'USD' ELSE 'unknown' END
--OR					p.CountryCode <> c.Country)
)

--Active Vendors whose Vendor Numbers match, but whose names do not match
SELECT DISTINCT p.VendorNumber,
				p.product_sort_name AS FulfillmentName,
				c.[Vendor Name] AS CatalystName
FROM			[QSPCanadaProduct]..Product p
JOIN			[TempDataWithoutBackup]..Vendor c
					ON CONVERT(varchar, p.VendorNumber) = CONVERT(varchar,c.[Vendor Number])
WHERE			p.VendorNumber is not null
AND				p.Product_Year = 2009
AND				p.Product_Season = 'F'
AND				p.Status = 30600
AND				p.Type = 46001
AND				p.Product_Sort_Name collate SQL_Latin1_General_CP1_CI_AI <> c.[Vendor Name] collate SQL_Latin1_General_CP1_CI_AI
AND				c.[Inactive Date] is null
