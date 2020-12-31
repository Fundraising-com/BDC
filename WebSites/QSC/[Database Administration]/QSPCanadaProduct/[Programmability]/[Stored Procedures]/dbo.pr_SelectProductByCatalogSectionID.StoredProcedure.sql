USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectProductByCatalogSectionID]    Script Date: 06/07/2017 09:18:04 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectProductByCatalogSectionID]

	@iProgramSectionID	int

AS

	SELECT	DISTINCT
			p.Product_Code,
			p.Product_Year,
			p.Product_Season,
			pdGST.Status,
			coalesce(cdStatus.Description, '') AS StatusDescription,
			p.Product_Sort_Name,
			QSPCanadaOrderManagement.dbo.UDF_ReplaceAccents(p.Product_Sort_Name) AS Product_Sort_Name_WithoutAccents,
			p.Lang,
			p.Type AS ProductTypeInstance,
			cd.Description AS ProductType,
			pdGST.MagPrice_Instance,
			pdHST.MagPrice_Instance AS MagPrice_InstanceHST,
			pdGST.QSP_Price AS GSTPrice,
			pdHST.QSP_Price AS HSTPrice,
			pdGST.Nbr_of_Issues AS Term,
			--pd.TaxRegionID,
			--CASE pd.TaxRegionID WHEN 1 THEN 'GST' WHEN 2 THEN 'HST' END AS TaxName,
			p.Pub_Nbr AS PublisherID,
			p.Fulfill_House_Nbr AS FulfillmentHouseID
FROM			Product p
JOIN			Pricing_Details pdGST
				ON	pdGST.ProgramSectionID = @iProgramSectionID
				AND 	pdGST.Product_Code = p.Product_Code
				AND	pdGST.Pricing_Season = p.Product_Season
				AND	pdGST.Pricing_Year = p.Product_Year
				AND	pdGST.TaxRegionID = 1
JOIN			Pricing_Details pdHST
				ON	pdHST.ProgramSectionID = @iProgramSectionID
				AND 	pdHST.Product_Code = p.Product_Code
				AND	pdHST.Pricing_Season = p.Product_Season
				AND	pdHST.Pricing_Year = p.Product_Year
				AND	pdHST.Nbr_of_Issues = pdGST.Nbr_of_Issues
				AND	pdHST.TaxRegionID = 2
JOIN			QSPCanadaCommon..CodeDetail cd
				ON	cd.Instance = p.Type
LEFT OUTER JOIN	QSPCanadaCommon..CodeDetail cdStatus
				ON	cdStatus.Instance = pdGST.Status
GO
