USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Kanata_GetItemsByCatalogAndAccountType]    Script Date: 06/07/2017 09:20:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Kanata_GetItemsByCatalogAndAccountType]

	@ProgramType	varchar(50) = '',	-- CATALOG NAME
	@ProductCode	varchar(20) = '',	-- PRODUCT CODE
	@CampaignId		int = 0,			-- CAMPAIGN ID
	@IsFmAccount	int = 0,
	@ProductType	int = 46008			-- PRODUCT TYPE

AS

IF (@ProgramType <> '')
BEGIN
	SET	@ProgramType = '%' + @ProgramType + '%'
END

SELECT		DISTINCT	--to filter Duplicate items from program GIFT and GIFT ONLY
			p.Product_Code,
			p.Product_Sort_Name,
			0 as Term,
			pd.MagPrice_Instance,
			1 AS Quantity,
			pd.QSP_Price AS Catalog_Price,
			ps.ID AS ProgramSection,
			pd.Pricing_Season AS Product_Season,
			pd.Pricing_Year AS Product_Year,
			pd.Language_Code AS Lang,
			p.Type AS ProductType,
			pd.QSP_Price AS EnterredPrice,
			master.Program_Type AS Catalog_Name,
			Count(pd.MagPrice_Instance) TotalCount,
			Sum(pd.QSP_Price) TotalPrice,
			ps.CatalogCode,
			0 as TransID,
			0 AS IsDeleted,
			45004 AS PriceOverrideReason
FROM		QSPcanadaProduct..Program_Master master,
			QSPCanadaProduct..ProgramSection ps,
			QSPCanadaProduct..Pricing_Details pd,
			QSPCanadaProduct..Product p
WHERE		master.Program_ID = ps.Program_ID
AND			ps.ID = pd.ProgramSectionID
AND			pd.Product_Instance = p.Product_Instance
AND			p.Status = 30600		--Active product
AND			pd.Status = 30600
AND			ps.Program_ID in (SELECT	Program_ID
								FROM	QSPcanadaProduct..vw_Kanata_Catalog_SelectAll)
AND			p.Product_Code LIKE '%'+@ProductCode+'%'
AND			master.Program_Type LIKE @ProgramType
GROUP BY	p.Product_Code,
			p.Product_Sort_Name,
			pd.MagPrice_Instance,
			pd.QSP_Price,
			ps.ID,
			pd.Pricing_Season,
			pd.Pricing_Year,
			pd.Language_Code,
			p.Type,
			master.Program_Type,
			ps.CatalogCode
ORDER BY	p.Product_Code
GO
