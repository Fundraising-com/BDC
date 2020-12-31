USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spGetCodeAndCampaignForCustomerService]    Script Date: 06/07/2017 09:20:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetCodeAndCampaignForCustomerService]  
  
 @Code varchar(20)='%',  -- PRODUCT CODE  
 @CampaignId int = 0,  -- CAMPAIGN ID  
 @ProductType int = 46001, -- PRODUCT TYPE  
 @CustomerInstance int = 0, -- TO GET THE TAX REGION ID  
 @CouponSetID int = 0  -- TO KNOW IF SUBS FOR GIFT CERTIFICATE ONLY  
  
AS  
  
Declare  @TaxRegionId Int, @Year int, @Season varchar(1)  
Declare  @now smalldatetime  
DECLARE @iLastYear int  
DECLARE @zLastSeason varchar(1)  
  
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
   
  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
--  This section will determine the Address To Send To and find TaxRegionId  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
  
DECLARE @StateProvince varchar(30)  
  
SELECT @StateProvince = C.State FROM Customer C WHERE C.Instance = @CustomerInstance AND C.StatusInstance = 300  
  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
--  This allows to add new subs for customers that were invalid  
--  Ben - 2005-09-27  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
IF(@StateProvince is null)  
BEGIN  
 SELECT TOP 1  
   @StateProvince = a.StateProvince  
 FROM  QSPCanadaCommon..Address a,  
   QSPCanadaCommon..CAccount ca,  
   QSPCanadaCommon..Campaign c  
 WHERE ca.AddressListID = a.AddressListID  
 AND  c.BillToAccountID = ca.ID  
 AND  a.address_type = 54002  
 AND  c.ID = @CampaignId  
END  
  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
--  This section will determine the TaxRegionId   
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
SELECT @TaxRegionId = TaxRegionId FROM QSPCanadaCommon..TaxRegionProvince WHERE Province LIKE @StateProvince  
  
--PRINT '@TaxRegionId: ' + Convert(varchar, IsNull(@TaxRegionId, 0))  
print 'dd' + coalesce(@StateProvince, 'null')  
  
  
if @CouponSetID = 0 and @ProductType = 46001  
begin  
  
print @CampaignId  
print @Season  
print @Year  
print @TaxRegionId  
  
  
 if @CampaignId <> 30016  
 begin  
 select   distinct B.Product_Code,  
  B.Product_Sort_Name,  
  MagPrice_instance,  
  PD.nbr_of_issues as Term,  
  Pd.qsp_price as Price,  
  Pd.programsectionid as ProgramSection,  
  pd.pricing_season,  
  pd.Language_code as Lang,  
  D.Program_Type as catalog_name,  
  pd.Language_code,  
  B.Type as ProductType,  
  C.CatalogCode from   
   
  
 QSPCanadaProduct..Program_Master D  
 INNER JOIN QSPCanadaProduct..ProgramSection C On C.CatalogCode= D.Code  
 inner Join QSPCanadaProduct..Pricing_Details PD on C.Id = PD.ProgramSectionId  
  INNER JOIN QSPCanadaProduct..Product B ON PD.Product_Code = B.Product_Code  
 where   
 PD.Pricing_year = @Year  
 and B.product_year=@Year  
 --and PD.Pricing_season =@Season  
 --and b.product_season=@Season  
  --AND D.Status IN ('30403', '30404')  
 and b.product_code like @code  
 and b.Status IN (30600, 30603) --30600:Active, 30603: Unremittable
 and productline=@producttype-46000   
 and taxregionid=@TaxRegionId   
 order by B.Product_Sort_Name  
 end  
 else  
 begin  
  
  select  distinct  B.Product_Code,  
  B.Product_Sort_Name,  
  MagPrice_instance,  
  PD.nbr_of_issues as Term,  
  Pd.qsp_price as Price,  
  Pd.programsectionid as ProgramSection,  
  pd.pricing_season,  
  pd.Language_code as Lang,  
  B.Type as ProductType,  
  D.Program_Type as catalog_name,  
  C.CatalogCode from   
  QSPCanadaProduct..Program_Master D  
  INNER JOIN QSPCanadaProduct..ProgramSection C On C.CatalogCode= D.Code  
  inner Join QSPCanadaProduct..Pricing_Details PD on C.Id = PD.ProgramSectionId  
   INNER JOIN QSPCanadaProduct..Product B ON PD.Product_Code = B.Product_Code   
  where   
  PD.Pricing_year = @Year  
  and B.product_year=@Year  
  --and PD.Pricing_season =@Season  
  --and b.product_season=@Season  
 --  AND D.Status IN ('30403', '30404')  
  and b.product_code like @code  
  and b.Status IN (30600, 30603) --30600:Active, 30603: Unremittable
  and productline=@producttype-46000  
   
  order by b.Product_Sort_Name--b.product_code  
  
 end  
end  
else if   @ProductType = 46001  
begin  
 select distinct  
  B.Product_Code,  
  pd.Language_code,  
  B.Product_Sort_Name,  
  PD.nbr_of_issues as Term,  
  pd.qsp_price as Price,   
  pricing_year,   
  pricing_season,   
  productline+46000 as productline,   
  MagPrice_instance,  
  pd.Language_code as Lang,  
  B.Type as ProductType,  
  pd.taxregionid,  
  b.lang,  
  case d.Lang when 'EN' then 'English Catalog' when 'FR' then 'French Catalog' end AS Catalog_Name  
 from   
  QSPCanadaProduct..Program_Master D ,  
 QSPCanadaProduct..ProgramSection C,  
 QSPCanadaProduct..Pricing_Details PD ,  
 QSPCanadaProduct..Product B ,  
 QSPCanadaCommon..TaxRegion  T,  
 QSPCanadaCommon..Season s  
 where   
  pd.product_code=b.product_code  
 and PD.Pricing_year=b.product_year  
 and pd.pricing_season = b.product_season  
 and C.CatalogCode= D.Code         -- catalog code  
 and C.Id = PD.ProgramSectionId   -- program section  
 and PD.Product_Code = B.Product_Code  
 and T.ID=PD.TaxregionId  
 and s.ID = d.Season  
 and d.SubType = 30305  
 --and t.id=1  
 and taxregionid=@TaxRegionId  
 and b.product_code like @code  
 and b.status IN (30600, 30603) --30600:Active, 30603: Unremittable
 and s.FiscalYear = @Year  
 and s.Season IN (@Season, 'Y')  
 order by b.Product_Sort_Name--b.product_code, pricing_year,pricing_season, taxregionid  
end  
else if @ProductType = 0  
begin  
  select  distinct  B.Product_Code,  
  B.Product_Sort_Name,  
  MagPrice_instance,  
  PD.nbr_of_issues as Term,  
  Pd.qsp_price as Price,  
  Pd.programsectionid as ProgramSection,  
  pd.pricing_season,  
  pd.Language_code as Lang,  
  B.Type as ProductType,  
  D.Program_Type as catalog_name,  
  C.CatalogCode from   
 QSPCanadaProduct..Program_Master D  
 INNER JOIN QSPCanadaProduct..ProgramSection C On C.Program_ID = D.Program_ID
 inner Join QSPCanadaProduct..Pricing_Details PD on C.Id = PD.ProgramSectionId  
  INNER JOIN QSPCanadaProduct..Product B ON PD.Product_Instance = B.Product_Instance
 where B.product_year=@Year  
 --and b.product_season=@Season  
 and b.Status = 30600  
 and b.product_code like @code  
 and (productline=6
 or productline = 7  
 or productline = 12
  or productline = 2  
 or productline = 20
 or ProductLine = 22
 or ProductLine = 23)  
  
 order by b.Product_Sort_Name--b.product_code  
end  
  
-- Next two sections modified by Ben for Product Replacement.  
-- It gets only one contract per ProgramSection  
else if @ProductType = 1 -- Kanata  
begin  
 -- Ben - 2006-03-09 : TEMP - Changed to keep Kanata products active for one more season  
 SELECT @iLastYear = CASE @Season WHEN 'F' THEN @Year - 1 WHEN 'S' THEN @Year ELSE 0 END,  
   @zLastSeason = CASE @Season WHEN 'F' THEN 'S' WHEN 'S' THEN 'F' ELSE '' END  
  
 SELECT p.Product_Code,  
   p.Product_Sort_Name,  
   pd.MagPrice_Instance,  
   pd.Nbr_Of_Issues AS Term,  
   pd.QSP_Price AS Price,  
   ps.ID AS ProgramSection,  
   pd.Pricing_Season,  
   pd.Language_Code AS Lang,  
   p.Type AS ProductType,  
   pm.Program_Type AS Catalog_Name,  
   ps.CatalogCode  
 INTO  #KanataTemp  
 FROM  QSPCanadaProduct..Product p  
 INNER JOIN QSPCanadaProduct..Pricing_Details pd  
    ON pd.Product_Instance = p.Product_Instance  
 INNER JOIN QSPCanadaProduct..ProgramSection ps  
    ON ps.ID = pd.ProgramSectionID  
 INNER JOIN QSPCanadaProduct..Program_Master pm  
    ON pm.Program_ID = ps.Program_ID
 WHERE /*((p.Product_Year = @Year  
 AND  p.Product_Season = @Season)  
 OR  (p.Product_Year = @iLastYear  
 AND  p.Product_Season = @zLastSeason))*/  
   p.Product_Code LIKE @code  
 AND  p.Status = 30600  
 AND  p.ProductLine IN (13, 14, 15, 8)  
   
   
 SELECT t.Product_Code,  
   t.CatalogCode,  
   MIN(t.ProgramSection) AS ProgramSection  
 INTO  #KanataMinSection  
 FROM  #KanataTemp t  
 GROUP BY t.Product_Code,  
   t.CatalogCode  
   
   
 DELETE t  
 FROM  #KanataTemp t  
 LEFT JOIN #KanataMinSection ms  
    ON ms.Product_Code = t.Product_Code  
    AND ms.CatalogCode = t.CatalogCode  
    AND ms.ProgramSection = t.ProgramSection  
 WHERE ms.Product_Code IS NULL  
   
   
 SELECT t.Product_Code,  
   t.Product_Sort_Name,  
   t.MagPrice_instance,  
   t.Term,  
   1 AS Quantity,  
   t.Price,  
   t.ProgramSection,  
   t.Pricing_Season,  
   t.Lang,  
   t.ProductType,  
   t.Price AS EnterredPrice,  
   t.Catalog_Name,  
   t.CatalogCode  
 FROM  #KanataTemp t  
 ORDER BY t.Product_Sort_Name  
   
   
 DROP TABLE #KanataTemp  
 DROP TABLE #KanataMinSection  
end  
else if @ProductType = 2 -- Gift  
begin  
 -- Ben - 2006-02-09 : TEMP - Changed to keep gifts active for one more season  
 SELECT @iLastYear = CASE @Season WHEN 'F' THEN @Year - 1 WHEN 'S' THEN @Year ELSE 0 END,  
   @zLastSeason = CASE @Season WHEN 'F' THEN 'S' WHEN 'S' THEN 'F' ELSE '' END  
  
 SELECT p.Product_Code,  
   p.Product_Sort_Name,  
   pd.MagPrice_Instance,  
   pd.Nbr_Of_Issues AS Term,  
   pd.QSP_Price AS Price,  
   ps.ID AS ProgramSection,  
   pd.Pricing_Season,  
   pd.Language_Code AS Lang,  
   p.Type AS ProductType,  
   pm.Program_Type AS Catalog_Name,  
   ps.CatalogCode  
 INTO  #GiftTemp  
 FROM  QSPCanadaProduct..Product p  
 INNER JOIN QSPCanadaProduct..Pricing_Details pd  
    ON pd.Product_Instance = p.Product_Instance  
 INNER JOIN QSPCanadaProduct..ProgramSection ps  
    ON ps.ID = pd.ProgramSectionID  
 INNER JOIN QSPCanadaProduct..Program_Master pm  
    ON pm.Program_ID = ps.Program_ID
 WHERE /*((p.Product_Year = @Year  
 AND  p.Product_Season = @Season)  
 OR  (p.Product_Year = @iLastYear  
 AND  p.Product_Season = @zLastSeason))*/  
   p.Product_Code LIKE @code  
 AND  p.Status = 30600  
 AND  p.ProductLine IN (2, 3, 20, 22)  
   
   
 SELECT t.Product_Code,  
   t.CatalogCode,  
   MIN(t.ProgramSection) AS ProgramSection  
 INTO  #GiftMinSection  
 FROM  #GiftTemp t  
 GROUP BY t.Product_Code,  
   t.CatalogCode  
   
   
 DELETE t  
 FROM  #GiftTemp t  
 LEFT JOIN #GiftMinSection ms  
    ON ms.Product_Code = t.Product_Code  
    AND ms.CatalogCode = t.CatalogCode  
    AND ms.ProgramSection = t.ProgramSection  
 WHERE ms.Product_Code IS NULL  
   
   
 SELECT t.Product_Code,  
   t.Product_Sort_Name,  
   t.MagPrice_instance,  
   t.Term,  
   1 AS Quantity,  
   t.Price,  
   t.ProgramSection,  
   t.Pricing_Season,  
   t.Lang,  
   t.ProductType,  
   t.Price AS EnterredPrice,  
   t.Catalog_Name,  
   t.CatalogCode  
 FROM  #GiftTemp t  
 ORDER BY t.Product_Sort_Name  
   
   
 DROP TABLE #GiftTemp  
 DROP TABLE #GiftMinSection  
end  

else if @ProductType = 3 -- Cookie Dough  
begin  
 -- Ben - 2006-02-09 : TEMP - Changed to keep gifts active for one more season  
 SELECT @iLastYear = CASE @Season WHEN 'F' THEN @Year - 1 WHEN 'S' THEN @Year ELSE 0 END,  
   @zLastSeason = CASE @Season WHEN 'F' THEN 'S' WHEN 'S' THEN 'F' ELSE '' END  
  
 SELECT p.Product_Code,  
   p.Product_Sort_Name,  
   pd.MagPrice_Instance,  
   pd.Nbr_Of_Issues AS Term,  
   pd.QSP_Price AS Price,  
   ps.ID AS ProgramSection,  
   pd.Pricing_Season,  
   pd.Language_Code AS Lang,  
   p.Type AS ProductType,  
   pm.Program_Type AS Catalog_Name,  
   ps.CatalogCode  
 INTO  #CookieDoughTemp  
 FROM  QSPCanadaProduct..Product p  
 INNER JOIN QSPCanadaProduct..Pricing_Details pd  
    ON pd.Product_Instance = p.Product_Instance  
 INNER JOIN QSPCanadaProduct..ProgramSection ps  
    ON ps.ID = pd.ProgramSectionID  
 INNER JOIN QSPCanadaProduct..Program_Master pm  
    ON pm.Program_ID = ps.Program_ID
 WHERE 
   p.Product_Code LIKE @code  
 AND  p.Status = 30600  
 AND  p.ProductLine IN (18)  
   
   
 SELECT t.Product_Code,  
   t.CatalogCode,  
   MIN(t.ProgramSection) AS ProgramSection  
 INTO  #CookieDoughMinSection  
 FROM  #CookieDoughTemp t  
 GROUP BY t.Product_Code,  
   t.CatalogCode  
   
   
 DELETE t  
 FROM  #CookieDoughTemp t  
 LEFT JOIN #CookieDoughMinSection ms  
    ON ms.Product_Code = t.Product_Code  
    AND ms.CatalogCode = t.CatalogCode  
    AND ms.ProgramSection = t.ProgramSection  
 WHERE ms.Product_Code IS NULL  
   
   
 SELECT t.Product_Code,  
   t.Product_Sort_Name,  
   t.MagPrice_instance,  
   t.Term,  
   1 AS Quantity,  
   t.Price,  
   t.ProgramSection,  
   t.Pricing_Season,  
   t.Lang,  
   t.ProductType,  
   t.Price AS EnterredPrice,  
   t.Catalog_Name,  
   t.CatalogCode  
 FROM  #CookieDoughTemp t  
 ORDER BY t.Product_Sort_Name  
   
   
 DROP TABLE #CookieDoughTemp  
 DROP TABLE #CookieMinSection  
end  

else if @ProductType = -1  
begin  
 select  distinct  B.Product_Code,  
   B.Product_Sort_Name,  
   MagPrice_instance,  
   PD.nbr_of_issues as Term,  
   Pd.qsp_price as Price,  
   Pd.programsectionid as ProgramSection,  
   B.Product_Year,  
   B.Product_Season as Pricing_Season,  
   pd.Language_code as Lang,  
   B.Type as ProductType,  
   D.Program_Type as catalog_name,  
   C.CatalogCode from   
 QSPCanadaProduct..Program_Master D  
 INNER JOIN QSPCanadaProduct..ProgramSection C On C.Program_ID = D.Program_ID
 inner Join QSPCanadaProduct..Pricing_Details PD on C.Id = PD.ProgramSectionId  
  INNER JOIN QSPCanadaProduct..Product B ON PD.Product_Code = B.Product_Code  
 where   
-- PD.Pricing_year = @Year  
-- and B.product_year=@Year  
 --and PD.Pricing_season in(@Season, 'A')  
 --and b.product_season=@Season  
--  AND D.Status IN ('30403', '30404')  
 pd.pricing_year = b.product_year  
 and pd.pricing_season = b.product_season  
 and b.product_code like @code  
 --and pd.taxregionid=@TaxRegionId  
 --and b.Status = 30600  
  
 order by B.Product_Year DESC, B.Product_Season DESC, b.Product_Sort_Name--b.product_code  
end  
else  
begin  
  select  distinct  B.Product_Code,  
  B.Product_Sort_Name,  
  MagPrice_instance,  
  PD.nbr_of_issues as Term,  
  Pd.qsp_price as Price,  
  Pd.programsectionid as ProgramSection,  
  pd.pricing_season,  
  pd.Language_code as Lang,  
  B.Type as ProductType,  
  D.Program_Type as catalog_name,  
  C.CatalogCode from   
 QSPCanadaProduct..Program_Master D  
 INNER JOIN QSPCanadaProduct..ProgramSection C On C.Program_ID = D.Program_ID  
 inner Join QSPCanadaProduct..Pricing_Details PD on C.Id = PD.ProgramSectionId  
  INNER JOIN QSPCanadaProduct..Product B ON PD.Product_Code = B.Product_Code   
 where   
 PD.Pricing_year = @Year  
 and B.product_year=@Year  
 --and PD.Pricing_season =@Season  
 --and b.product_season=@Season  
--  AND D.Status IN ('30403', '30404')  
 and b.product_code like @code  
 and b.Status = 30600  
 and productline=@producttype-46000  
  
 order by b.Product_Sort_Name--b.product_code  
end
GO
