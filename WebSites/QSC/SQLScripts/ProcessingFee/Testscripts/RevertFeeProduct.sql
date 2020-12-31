DECLARE @iProductType int
SET @iProductType = 46017

DELETE FROM [QSPCanadaCommon].[dbo].[QSPProductLine]
WHERE ID = @iProductType;

DELETE FROM [QSPCanadaCommon].[dbo].[CodeDetail]
WHERE Instance = @iProductType;

GO

DECLARE @zCatalogCode varchar(50)
DECLARE @iCatalogType int

SET @zCatalogCode = 'PFEE01'
SET @iCatalogType = 8

DELETE FROM QSPCanadaProduct..PROGRAM_MASTER
WHERE CODE = @zCatalogCode;

DELETE FROM QSPCanadaProduct..ProgramSectionType 
WHERE ID = @iCatalogType

DELETE FROM QSPCanadaCommon..TaxApplicableTax
WHERE SECTION_TYPE_ID = @iCatalogType

DELETE FROM QSPCanadaProduct..ProgramSection
WHERE CatalogCode = @zCatalogCode;

GO

DECLARE @zProductCode varchar(20)
SET @zProductCode = 'PFEE'

DELETE FROM QSPCanadaProduct..ProductDescription
WHERE PRODUCT_CODE = @zProductCode;

DELETE FROM QSPCanadaProduct..PRICING_DETAILS
WHERE Product_Code = @zProductCode;

DELETE FROM QSPCanadaProduct..Product
WHERE Product_Code = @zProductCode;


GO