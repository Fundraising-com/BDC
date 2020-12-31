USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spSearchByNameAndProductType]    Script Date: 06/07/2017 09:20:56 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[spSearchByNameAndProductType] 
	@name varchar(50),
	@producttype int
 AS
	select distinct B.Product_Code,B.Product_Sort_Name,MagPrice_instance,
		PD.nbr_of_issues as Term,
		Pd.qsp_price as Price,
		Pd.programsectionid as ProgramSection,
		pd.pricing_year,
		pd.pricing_season,
		T.Description +' '+d.program_type from 
	QSPCanadaCommon..CampaignToContentCatalog A
	INNER JOIN QSPCanadaProduct..Program_Master D ON D.Code LIKE A.Content_Catalog_Code
	INNER JOIN QSPCanadaProduct..ProgramSection C On C.CatalogCode= D.Code
	inner Join QSPCanadaProduct..Pricing_Details PD on C.Id = PD.ProgramSectionId
	INNER JOIN QSPCanadaProduct..Product B ON PD.Product_Code = B.Product_Code
	inner join   QSPCanadaCommon..TaxRegion  T on T.ID=PD.TaxregionId
	where D.Status IN ('30403', '30404')
	and b.Product_Sort_Name like @name
	and PD.Pricing_year=b.product_year
	and pd.pricing_season = b.product_season
	and productline=@producttype-46000
	order by pd.pricing_year,
		pd.pricing_season
GO
