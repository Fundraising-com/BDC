DECLARE @zProcessingFeeProductCode varchar(20);
DECLARE @iProductType int;
DECLARE @iProgramSectionID int;
DECLARE @iPricingDetailsID int;
DECLARE @iProgramSectionTypeID int;

SET @iProductType = (SELECT ID FROM QSPCanadaCommon..QSPProductLine WHERE DESCRIPTION LIKE '%Processing fees%');
SET @zProcessingFeeProductCode = 'PFEE';;
SET @iPricingDetailsID = (SELECT TOP 1 MagPrice_Instance from QSPCanadaProduct..PRICING_DETAILS WHERE Product_Code = @zProcessingFeeProductCode);
SET @iProgramSectionID = (SELECT TOP 1 ProgramSectionID from QSPCanadaProduct..Pricing_Details WHERE Product_Code = @zProcessingFeeProductCode);
SET @iProgramSectionTypeID = (SELECT PS.Type FROM QSPCanadaProduct..ProgramSection PS WHERE ID = @iProgramSectionID);

SELECT  @zProcessingFeeProductCode, @iProductType, @iProgramSectionID, @iPricingDetailsID, @iProgramSectionTypeID