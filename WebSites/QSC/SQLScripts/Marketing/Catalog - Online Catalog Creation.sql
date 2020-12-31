select
	DISTINCT
	CASE B.RemitCode WHEN '' THEN B.Product_Code ELSE B.RemitCode END AS RemitCode
	, B.Product_Sort_Name
	, basepriceoriginalcurrency
	, nbr_of_issues
	, Pd.qsp_price as Price
	, CASE pd.QSPCAListingCopyText WHEN '' THEN E.AdCopyText WHEN null THEN E.AdCopyText ELSE pd.QSPCAListingCopyText END AS AdCopyText
	, E.Premium_Copy
	, Replace(F.Description, 'Category ', '') As 'Category'
	, C.CatalogCode
	, D.Code
	, CONVERT(Numeric(10,2), CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * 1.05 * 100) / 100) AS NewsStand_Price_Yr_GST_Price
	, CONVERT(Numeric(10,2), CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * 1.13 * 100) / 100) AS NewsStand_Price_Yr_HST_Price
	,	CASE (CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * 1.05 * 100) / 100)
		WHEN 0 THEN 0
		ELSE CASE
				WHEN ISNULL(FLOOR((((CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * 1.05 * 100) / 100) - (CEILING(ROUND(pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END * 1.05, 2)))) / (CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * 1.05 * 100) / 100)) * 100) / 100, 0) < 0.15 THEN 0
				ELSE ISNULL(CONVERT(Numeric(10), FLOOR((((CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * 1.05 * 100) / 100) - (CEILING(ROUND(pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END * 1.05, 2)))) / (CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * 1.05 * 100) / 100)) * 100)), 0) END END AS Cat_GST_Percent
	,	CASE (CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * 1.13 * 100) / 100)
		WHEN 0 THEN 0
		ELSE CASE
				WHEN ISNULL(FLOOR((((CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * 1.13 * 100) / 100) - (CEILING(ROUND(pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END * 1.13, 2)))) / (CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * 1.13 * 100) / 100)) * 100) / 100, 0) < 0.15 THEN 0
				ELSE ISNULL(CONVERT(Numeric(10), FLOOR((((CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * 1.13 * 100) / 100) - (CEILING(ROUND(pd.Basic_Price_Yr * CASE p.Currency WHEN 802 THEN pd.ConversionRate ELSE 1 END * 1.13, 2)))) / (CEILING(pd.Nbr_of_Issues * pd.NewsStand_Price_Yr * 1.13 * 100) / 100)) * 100)), 0) END END AS Cat_HST_Percent
from 
	ca_oltp1.QSPCanadaProduct.dbo.Program_Master D ,
	ca_oltp1.QSPCanadaProduct.dbo.ProgramSection C,
	ca_oltp1.QSPCanadaProduct.dbo.Pricing_Details PD,
	ca_oltp1.QSPCanadaProduct.dbo.Product B,
	ca_oltp1.QSPCanadaCommon.dbo.TaxRegion  T
	, ca_oltp1.QSPCanadaProduct.dbo.Program_Details E
	, ca_oltp1.QSPCanadaCommon.dbo.CodeDetail F
	, ca_oltp1.QSPCanadaProduct.dbo.Product P
where 
	d.program_id= @ProgramId
	and pd.product_code=b.product_code
	and PD.Pricing_year=b.product_year
	and pd.pricing_season = b.product_season
	and C.CatalogCode= D.Code
	and  C.Id = PD.ProgramSectionId
	and  PD.Product_Code = B.Product_Code
	and  T.ID=PD.TaxregionId
	and t.id = @TaxRegionId
	--and b.type=46001
	and internetapproval=1
	AND e.Program_Year = b.Product_Year
	AND e.Program_Season = B.Product_Season
	AND e.Product_Code = B.Product_Code
	AND e.TaxRegionId = PD.TaxRegionId
	AND e.ProgramSectionId = PD.ProgramSectionId
	AND e.Offer_Id = PD.Offer_COde
	AND f.Instance = b.Category_Code
	AND B.Status = 30600 
	AND Pd.qsp_price > 0
	AND	p.Product_Instance = pd.Product_Instance
order by 
	B.RemitCode