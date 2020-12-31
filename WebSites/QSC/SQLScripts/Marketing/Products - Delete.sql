     SELECT Distinct pd.*                                         
      FROM                                     
       QSPCanadaProduct.dbo.Program_Master pm                                 
       JOIN QSPCanadaProduct.dbo.ProgramSection ps ON ps.Program_ID = pm.Program_ID                               
      JOIN QSPCanadaProduct.dbo.Pricing_Details pd ON ps.Id = pd.ProgramSectionId                                
      JOIN QSPCanadaProduct.dbo.Product p ON pd.product_instance = p.product_instance                                  
      JOIN QSPCanadaCommon.dbo.TaxRegion  t ON  t.ID = pd.TaxregionId                                
      JOIN QSPCanadaProduct.dbo.vw_Season s ON pm.Season = s.ID                                
       AND s.StartDate <= GETDATE()                                     
       AND s.EndDate >= GETDATE()                                 
       LEFT JOIN QSPCanadaProduct.dbo.ProductDescription pdes ON p.OracleCode = pdes.Product_Code                               
       WHERE  p.Status IN (30600, 30601, 30603) --Active, Inactive, Unremittable                                  
       AND pd.TaxRegionID IN (0,1)                                
      AND  p.type IN (46001,46002, 46018, 46020, 46022, 46023, 46024) --qsp pl- magazine, qsp pl- gift, Cookie Dough, Jewelry, Candles, TRT, Entertainment                                
      AND pm.Status IN (30403, 30404) -- Approved, Inuse                                 
       AND pm.Lang <> 'FR'                                  
      AND (PDES.Language_Code IS NULL OR PDES.Language_Code = 'EN')                                  
      AND pm.Program_ID = 325
      ORDER BY pd.MagPrice_Instance

select *
from PRICING_DETAILS
where Pricing_Season = 'Y'

select *
from PRICING_DETAILS
where Product_Code like 'DG%'
and ProgramSectionID = 1165

select *
from Product p
join ProductDescription pd on pd.PRODUCT_CODE = p.OracleCode
order by Product_Instance desc

select *
from ProductDescription
order by PRODUCT_CODE desc

begin tran
delete PRICING_DETAILS
where Product_Code not like 'DG%'
and ProgramSectionID = 1165

update PRICING_DETAILS
set Pricing_Season = 'S'
where Pricing_Season = 'Y' --Should never have pricing_details with 'Y'
and ProgramSectionID = 1165

delete Product
where Product_Instance >= 55531
and Product_Code not like 'DG%'

update Product
set Product_Season = 'S'
where Product_Season = 'Y' --Should never have pricing_details with 'Y'
and Product_Instance >= 55531

