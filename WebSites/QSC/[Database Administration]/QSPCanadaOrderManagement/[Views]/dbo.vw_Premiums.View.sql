USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_Premiums]    Script Date: 06/07/2017 09:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_Premiums] AS
SELECT DISTINCT p.product_code,
	 PD.qsppremiumid,
	CASE WHEN PD.QSppremiumId IN ('1','3','6','9','12') THEN 1 ELSE 2 END AS PremiumType	
FROM  	QSPCanadaProduct..Product as P,
	QSPCanadaProduct..Pricing_Details as PD
	--QSPCanadaProduct..Program_Details as PGD
WHERE	p.type=46001
	and p.product_code=pd.product_code
	and p.product_year = pd.pricing_year
	and p.product_season = pd.pricing_season
	--and PD.QSppremiumId in ('1','2','3','4','5','6','7','8','9','10','11','12') 		MS March 8, 2006
	and PD.QSppremiumId >0
	and product_year =CASE WHEN DATEPART(MM, getDate()) >6 THEN DATEPART(YYYY,getDate()) + 1 ELSE DATEPART(YYYY,getDate()) END

--Disabled MS Sept 8, 2005
/*
SELECT 	DISTINCT p.product_code,
	PGD.qsppremiumid,
	CASE WHEN PGD.QSppremiumId IN ('1','3','6') THEN 1 ELSE 2 END AS PremiumType
FROM  	QSPCanadaProduct..Product as P,
	QSPCanadaProduct..Pricing_Details as PD,
	QSPCanadaProduct..Program_Details as PGD
WHERE	p.type=46001
	and p.product_code=pgd.product_code
	and p.product_year = pd.pricing_year
	and p.product_season = pd.pricing_season
	and pd.product_code=pgd.product_code
	and pgd.program_year = pd.pricing_year
	and pgd.program_season = pd.pricing_season
	and pgd.taxregionid = pd.taxregionid
	and pd.programsectionid= pgd.programsectionid
	and PGD.QSppremiumId in ('1','2','3','4','5','6') 
	and premium_code in ('1','2','3','4','5','6')   
	and product_year = CASE WHEN DATEPART(MM, getDate()) >6 THEN DATEPART(YYYY,getDate()) + 1 ELSE DATEPART(YYYY,getDate()) END

*/
GO
