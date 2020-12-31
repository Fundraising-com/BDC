USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GetItemsByCatalogAndAccountType]    Script Date: 06/07/2017 09:20:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_GetItemsByCatalogAndAccountType]
	@CatalogCode varchar(50)='%',		-- PRODUCT CODE
	@Code varchar(20)='%',		-- PRODUCT CODE
	@CampaignId int = 0,		-- CAMPAIGN ID
	@IsFmAccount int =0,
	@ProductType int = 46008	-- PRODUCT TYPE

AS

DECLARE @Prov 	Varchar(10)						
DECLARE @Season 	Char(1)
DECLARE @Year 	Int
DECLARE @Lang 	Varchar(10)
DECLARE @TaxRegionId Int
--Set @CampaignID =34632

Declare @now smalldatetime

SET @now = getDate()

SELECT 

	-- Ben - 2006-01-02 : Changed so that it keeps the products for one month after a season change
	@Year = CASE
		WHEN MONTH(CONVERT(smalldatetime,@now)) > 7 THEN YEAR(CONVERT(smalldatetime,@now)) + 1
		WHEN MONTH(CONVERT(smalldatetime,@now)) <= 7 THEN YEAR(CONVERT(smalldatetime,@now))
		ELSE
			0
		END
	,@Season = CASE
		WHEN MONTH(CONVERT(smalldatetime,@now)) > 7 OR MONTH(CONVERT(smalldatetime,@now)) = 1 THEN 'F'
		WHEN MONTH(CONVERT(smalldatetime,@now)) BETWEEN 2 AND 7 THEN 'S'
		ELSE ''
		END

SELECT 
	@Lang = ca.Lang
	,@Prov= c.StateProvince
FROM
	QSPCanadaCommon..Campaign CA,
	QSPCanadaCommon..CAccount A
	INNER JOIN QSPCanadaCommon..AddressList B ON A.AddressListId = B.Id
	INNER JOIN QSPCanadaCommon..Address C ON C.AddressListId = B.Id
WHERE
	C.Address_Type = 54001  --- SHIP TO Address Type
	AND CA.ShipToAccountID=A.id
	AND CA.Id = @CampaignID


SELECT @TaxRegionId = TaxRegionId FROM QSPCanadaCommon..TaxRegionProvince WHERE Province=@Prov

--Only those items running under CA
IF @IsFMAccount=0
Begin
SELECT DISTINCT           --to filter Duplicate items from program GIFT and GIFT ONLY
	p.Product_Code,
	p.Product_Sort_Name,
	0 as Term,
	pd.MagPrice_Instance,
	1 AS Quantity,
	pd.QSP_Price AS Price,
	ps.ID AS ProgramSection,
	pd.Pricing_Season,
	pd.Language_Code AS Lang,
	p.Type AS ProductType,
	pd.QSP_Price AS EnterredPrice,
	master.Program_Type AS Catalog_Name,
	Count(pd.MagPrice_Instance) TotalCount,
	Sum(pd.QSP_Price) TotalPrice,
	ps.CatalogCode,
	0 as TransID
FROM 	
	QSPCanadaProduct.dbo.Program_Master master,
	QSPCanadaProduct.dbo.ProgramSection ps,
	QSPCanadaProduct.dbo.Pricing_Details pd,
	QSPCanadaProduct.dbo.Product p
WHERE 
p.Status = 30600		--Active product
AND pd.QSP_Price > 0		--Must have a price
AND master.Code = ps.CatalogCode
AND ps.ID = pd.ProgramSectionID
AND pd.Product_Instance = p.Product_Instance
AND master.Program_Type LIKE @CatalogCode
AND p.Product_Code LIKE '%'
--and  pd.TaxregionID=@TaxRegionId
Group By p.Product_Code, p.Product_Sort_Name, pd.MagPrice_Instance,
	pd.QSP_Price,ps.ID ,pd.Pricing_Season,	pd.Language_Code ,p.Type ,master.Program_Type ,ps.CatalogCode
End


--All items irrespective of CA program(s)
IF @IsFMAccount=1
Begin

declare @start datetime
declare @end datetime

Select @Start=Startdate,@end=Enddate
From [QSPCanadaCommon].[dbo].[Season]
Where (
	--Convert(varchar(10),Getdate(),101) between StartDate and EndDate 
	Convert(varchar(10),DATEADD(Month,-12,Getdate()),101) between StartDate and EndDate
or 	Convert(varchar(10),DATEADD(Month,-24,Getdate()),101) between StartDate and EndDate
)
and Season <>'Y'

SELECT  DISTINCT          --to filter Duplicate items from program GIFT and GIFT OONLY
	p.Product_Code,
	p.Product_Sort_Name,
	0 as Term,
	pd.MagPrice_Instance,
	1 AS Quantity,
	pd.QSP_Price AS Price,
	ps.ID AS ProgramSection,
	pd.Pricing_Season,
	pd.Language_Code AS Lang,
	p.Type AS ProductType,
	pd.QSP_Price AS EnterredPrice,
	master.Program_Type AS Catalog_Name,
	Count(pd.MagPrice_Instance) TotalCount,
	Sum(pd.QSP_Price) TotalPrice,
	ps.CatalogCode
FROM	QSPcanadaProduct..Program_Master master ,
	QSPCanadaProduct.dbo.ProgramSection ps,
	QSPCanadaProduct.dbo.Pricing_Details pd,
	QSPCanadaProduct.dbo.Product p,
	QSPCanadaCommon..Season s
WHERE s.ID = master.Season
AND master.Code = ps.CatalogCode
AND ps.ID = pd.ProgramSectionID
AND pd.Product_Instance = p.Product_Instance
AND p.Status = 30600		 --Active product
AND pd.QSP_Price > 0		 --Must have a price
AND ps.type <> 2		-- Not Magazine
AND p.Type Not In (46001,46006,46007)
AND s.startdate between @Start and @end
AND p.Product_Code LIKE @code
--AND  pd.TaxregionID=@TaxRegionId
Group By p.Product_Code, p.Product_Sort_Name, pd.MagPrice_Instance,
	pd.QSP_Price,ps.ID ,pd.Pricing_Season,	pd.Language_Code ,p.Type ,master.Program_Type ,ps.CatalogCode
End

   



/*
--select top 10 * from qspcanadaproduct..product
SELECT p.Product_Code,
	p.Product_Sort_Name,
	pd.MagPrice_Instance,
	pd.QSP_Price AS Price,
	ps.ID AS ProgramSection,
	pd.Pricing_Season,
	pd.Language_Code AS Lang,
	p.Type AS ProductType,
	pm.Program_Type AS Catalog_Name,
	ps.CatalogCode
INTO	#KanataTemp
FROM	QSPCanadaProduct..Product p
	INNER JOIN	QSPCanadaProduct..Pricing_Details pd ON pd.Product_Instance = p.Product_Instance
	INNER JOIN	QSPCanadaProduct..ProgramSection ps ON ps.ID = pd.ProgramSectionID
	INNER JOIN	QSPCanadaProduct..Program_Master pm 	ON pm.Code = ps.CatalogCode
WHERE	p.Product_Year = 2006 --@Year
AND		p.Product_Season = 'F' -- @Season
AND		p.Product_Code LIKE @code
AND		p.Status = 30600
AND		p.ProductLine IN (13, 14, 15, 8)
	
	
SELECT	t.Product_Code,
		t.CatalogCode,
		MIN(t.ProgramSection) AS ProgramSection
INTO	#KanataMinSection
FROM	#KanataTemp t
GROUP BY  t.Product_Code, t.CatalogCode
	
	
DELETE	t
FROM		#KanataTemp t
LEFT JOIN	#KanataMinSection ms	ON	ms.Product_Code = t.Product_Code AND	ms.CatalogCode = t.CatalogCode 	AND	ms.ProgramSection = t.ProgramSection
WHERE	ms.Product_Code IS NULL
	

SELECT	t.Product_Code,
		t.Product_Sort_Name,
		0 as Term,
		t.MagPrice_instance,
		1 AS Quantity,
		t.Price,
		t.ProgramSection,
		t.Pricing_Season,
		t.Lang,
		t.ProductType,
		t.Price AS EnterredPrice,
		t.Catalog_Name,
		t.CatalogCode
FROM		#KanataTemp t
ORDER BY	t.Product_Sort_Name
	
	
DROP TABLE #KanataTemp
DROP TABLE #KanataMinSection
*/
GO
