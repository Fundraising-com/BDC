USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GenerateResolveProductList]    Script Date: 06/07/2017 09:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_GenerateResolveProductList]
as
select distinct
D.Code,
B.Product_Code,
	B.Product_Sort_Name,
PD.Nbr_Of_Issues,
pd.qsp_price as Price, pricing_year, pricing_season, 
productline+46000 as productline, pd.taxregionid
	  from 	
 QSPCanadaProduct..Program_Master D ,
QSPCanadaProduct..ProgramSection C,
QSPCanadaProduct..Pricing_Details PD ,
QSPCanadaProduct..Product B ,
QSPCanadaCommon..TaxRegion  T 
where 
 pd.product_code=b.product_code
and PD.Pricing_year=b.product_year
and pd.pricing_season = b.product_season
and C.CatalogCode= D.Code         -- catalog code
and  C.Id = PD.ProgramSectionId   -- program section
and  PD.Product_Code = B.Product_Code
and  T.ID=PD.TaxregionId
and productline not in (4,8)
--and pricing_season='F'
and pricing_year>=2005
order by D.Code, b.product_code, pricing_year,pricing_season, taxregionid
GO
