DECLARE @iProductType int;

DECLARE @iCatalogId int
DECLARE @zCatalogCode varchar(50)
DECLARE @zCatalogName varchar(50)
DECLARE @iCatalogType int
DECLARE @zLanguage varchar(10)
DECLARE @iYear int
DECLARE @zSeason varchar(1)
DECLARE @iCatalogStatus int
DECLARE @zIsReplacement varchar(1)
DECLARE @zUserID varchar(30)

DECLARE @iCatalogSectionType int
DECLARE @iFulfillmentId int

DECLARE @iCatalogSectionId int
DECLARE @iFSProgramID int

DECLARE @iproductId int
DECLARE @zProductCode varchar(20)
DECLARE @zProductName varchar(55)
DECLARE @iCategoryID int
DECLARE @iStatus int
DECLARE @iDaysLeadTime int
DECLARE @iNbrOfIssuesPerYear int
DECLARE @iPublisherID int
DECLARE @iFulfillmentHouseID int
DECLARE @zComment varchar(200)
DECLARE @zVendorNumber varchar(30)
DECLARE @zVendorSiteName varchar(15)
DECLARE @zPayGroupLookupCode varchar(25)
DECLARE @iCurrency int
DECLARE @zGSTRegistrationNumber varchar(20)
DECLARE @zHSTRegistrationNumber varchar(20)
DECLARE @zPSTRegistrationNumber varchar(20)
DECLARE @zOracleCode varchar(50)
DECLARE @zPrizeLevel varchar(10)
DECLARE @iPrizeLevelQtyRequired int
DECLARE @zRemitCode varchar(20)
DECLARE @bIsQSPExclusive bit
DECLARE @zEnglishDescription varchar(50)
DECLARE @zFrenchDescription varchar(50)

DECLARE @iNewMagPriceInstance int
DECLARE @iMagPriceInstance int

DECLARE @dEffectiveDate datetime
DECLARE @dEndDate datetime
DECLARE @dDateSubmitted datetime
DECLARE @fRemitRate numeric(10,2)
DECLARE @fConversionRate numeric(10,2)
DECLARE @fNewsStandPrice numeric(10,2)
DECLARE @iListingLevelID int
DECLARE @zListingCopyText varchar(500)
DECLARE @iAdInCatalog int
DECLARE @iAdPageSizeID int
DECLARE @iAdPaymentCurrency int
DECLARE @fAdCost numeric(10,2)
DECLARE @zPricingComment varchar(200)
DECLARE @iEffortKeyRequired int
DECLARE @zEffortKey varchar(40)
DECLARE @iNumberOfIssues int
DECLARE @fBasePrice numeric(10,2)
DECLARE @fQSPPrice numeric(10,2)
DECLARE @iInternetApproval int
DECLARE @zABCCode varchar(20)
DECLARE @iQSPPremiumID int
DECLARE @iFSApplicabilityId int
DECLARE @iFSDistributionLevelID int
DECLARE @iFSExtraLimitRate int
DECLARE @bFSIsBrochure bit
DECLARE @iTaxRegionID int
DECLARE @zPremiumInd varchar(1)
DECLARE @zPremiumCode varchar(50)
DECLARE @zPremiumCopy varchar(500)
DECLARE @zFSProvinceCode varchar(10)

DECLARE @iPricingDetailsID int

/* 	Start by inserting a QSP Product Line item, whose id will match the Product type that is to be created in CodeDetail 
	If this value is altered for the production system, it needs to be changed in many scripts. Most ids are identity, but 
	this one isn't, so this value is frequently used to retrieve new inserts, and will cause severe errors in the scripts 
	if not properly updated.
	Scripts to modify if iProductType <> 46017:
		SQLScripts\ProcessingFee\Deployment scripts\CreateFeeGL.sql
		SQLScripts\ProcessingFee\Deployment scripts\Process sp changes\UpdateBillableOrdersUDF.sql
		SQLScripts\ProcessingFee\Deployment scripts\Report sp changes\CreateGetProcessingFeesGLCounts.sql
		SQLScripts\ProcessingFee\Deployment scripts\Report sp changes\CreateInvoiceRegisterReport2.sql
		SQLScripts\ProcessingFee\Testscripts\CreateTestFees.sql
		SQLScripts\ProcessingFee\Testscripts\DeleteTestFeeInvoices.sql
		SQLScripts\ProcessingFee\Testscripts\GenerateTestFeeInvoices.sql
		SQLScripts\ProcessingFee\Testscripts\RevertFeeGLEntries.sql
		SQLScripts\ProcessingFee\Testscripts\RevertFeeProduct.sql
		*/
SET @iProductType = 46017

INSERT INTO [QSPCanadaCommon].[dbo].[QSPProductLine]
           ([ID]
           ,[EntityID]
           ,[ProductLineNumber]
           ,[Description]
           ,[DistributionCenterID])
     VALUES
           (@iProductType,			--(<ID, int,>
           62,						--<EntityID, int,> (62 is value all other entries have)
           NULL,					--<ProductLineNumber, int,> (According to Jeff, not used, and not known what value would be appropriate)
           'Processing fees', --<Description, varchar(50),>
           NULL)						--<DistributionCenterID, int,>)

--/* Insert a product Type into the code detail table, under 46000 (Product Type) code header */
INSERT INTO [QSPCanadaCommon].[dbo].[CodeDetail]
           ([Instance]
           ,[CodeHeaderInstance]
           ,[Description]
           ,[Gross]
           ,[ADPCode])
     VALUES
           (@iProductType,	--[Instance]
           46000,			--[CodeHeaderInstance]
           'Processing fees', --[Description]
           0,				--<Gross>
           '')				--<ADPCode, varchar(6),>)

/* Create a program Master */
SET @zCatalogCode = 'PFEE01'			--Change RevertFeeGL.sql if another value is selected
SET @zCatalogName = 'Processing fees'
SET @iCatalogType = 30301				--Fundraising
SET @zLanguage = 'EN/FR'
SET @iYear = 2011
set @zSeason = 'F'
SET @iCatalogStatus = 30404				--In Use
SET @zIsReplacement = 'N'
SET @zUserID = NULL


EXECUTE [QSPCanadaProduct].[dbo].[pr_InsertCatalog] 
   @zCatalogCode
  ,@zCatalogName 
  ,@iCatalogType
  ,@zLanguage
  ,@iYear
  ,@zSeason
  ,@iCatalogStatus
  ,@zIsReplacement
  ,@zUserID

/* Create a program section type */
/* If this value is altered for the production system, it needs to be changed in many scripts. Most ids are identity, but 
	this one isn't, so this value is frequently used to retrieve new inserts, and will cause severe errors in the scripts 
	if not properly updated.
	Scripts to modify if iCatalogSectionType <> 8:
		SQLScripts\ProcessingFee\Deployment scripts\Process sp changes\UpdateGenerateInvoices.sql
		SQLScripts\ProcessingFee\Deployment scripts\Report sp changes\CreateGetInvoiceTotalsInfo_WithProcessingFee.sql
		SQLScripts\ProcessingFee\Deployment scripts\Report sp changes\CreateGetProcessingFeesInvoiceCounts.sql
		SQLScripts\ProcessingFee\Deployment scripts\Report sp changes\CreateInvoiceRegisterReport2.sql
		SQLScripts\ProcessingFee\Deployment scripts\Report sp changes\UpdateUDFListInvoiceSection.sql
		SQLScripts\ProcessingFee\Testscripts\DeleteTestFeeInvoices.sql
		SQLScripts\ProcessingFee\Testscripts\GenerateTestFeeInvoices.sql 
		SQLScripts\ProcessingFee\Testscripts\RevertFeeProduct.sql
		SQLScripts\ProcessingFee\Testscripts\ValidateTotals.sql
	*/
SET @iCatalogSectionType = 8		--New type for processing fee
SET @iFulfillmentId = 31005			--Misc.

INSERT INTO [QSPCanadaProduct]..ProgramSectionType
	(ID,
	FulfillmentID,
	Description,
	IsTaxIncluded,
	IsPriceWithTax
	) Values
	(@iCatalogSectionType,
	@iFulfillmentId,
	@zCatalogName,
	0,
	'Y')

/* 	Set applicable taxes for new section type (copy from type 5 - misc.) 
	TODO: Confirm that 5 is a correct reference entry for processing fee taxes - may want to use magazine (2) entries instead */
INSERT INTO QSPCanadaCommon..TaxApplicableTax
	(
		TAX_ID,
		SECTION_TYPE_ID,
		COUNTRY_CODE,
		PROVINCE_CODE
	) 
	SELECT 
		TAX_ID,
		@iCatalogSectionType,
		COUNTRY_CODE,
		PROVINCE_CODE
	FROM QSPCanadaCommon..TaxApplicableTax
	WHERE SECTION_TYPE_ID = 5
	
SET @iFSProgramID = 0

SELECT @iCatalogId = Program_ID FROM [QSPCanadaProduct]..Program_Master WHERE Code = @zCatalogCode

EXECUTE [QSPCanadaProduct]..[pr_InsertCatalogSection] 
   @iCatalogID
  ,@zCatalogCode
  ,@iCatalogSectionType
  ,@zCatalogName
  ,@iFSProgramID
  ,@zUserID

/* Create a product */
/* If this value is altered for the production system, it needs to be changed in many scripts. Most ids are identity, but 
	this one isn't, so this value is frequently used to retrieve new inserts, and will cause severe errors in the scripts 
	if not properly updated.
	Scripts to modify if zProductCode <> 'PFEE':
		SQLScripts\ProcessingFee\Deployment scripts\Report sp changes\CreateGetProcessingFeesCODCounts.sql
		SQLScripts\ProcessingFee\Deployment scripts\Report sp changes\UpdateGetBatchDetails.sql
		SQLScripts\ProcessingFee\Testscripts\CreateTestFees.sql
		SQLScripts\ProcessingFee\Testscripts\DeleteTestFees.sql
		SQLScripts\ProcessingFee\Testscripts\GenerateTestFeeInvoices.sql
		SQLScripts\ProcessingFee\Testscripts\GetGeneratedValues.sql
		SQLScripts\ProcessingFee\Testscripts\RevertFeeProduct.sql
		SQLScripts\ProcessingFee\Testscripts\ValidateTotals.sql
	*/
SET @zProductCode = 'PFEE'
SET @zProductName = 'Processing fees'
SET @iCategoryID = NULL
SET @iStatus = 30600								--Active
SET @iDaysLeadTime = NULL
SET @iNbrOfIssuesPerYear = NULL
SET @iPublisherID = 0
SET @iFulfillmentHouseID = NULL
SET @zComment = 'Processing fee on landed orders'
SET @zVendorNumber = NULL
SET @zVendorSiteName = NULL
SET @zPayGroupLookupCode = NULL
SET @iCurrency = 801 								--CAD
SET @zGSTRegistrationNumber = NULL
SET @zHSTRegistrationNumber = NULL
Set @zPSTRegistrationNumber = NULL
SET @zOracleCode = @zProductCode
SET @zPrizeLevel = ''
SET @iPrizeLevelQtyRequired = 0
SET @zRemitCode = NULL
SET @bIsQSPExclusive = 0
set @zEnglishDescription = 'Processing fees'
SET @zFrenchDescription = 'Frais de traitement'

SELECT @iCatalogSectionId = ID FROM QSPCanadaProduct..ProgramSection WHERE CatalogCode = @zCatalogCode

EXECUTE [QSPCanadaProduct].[dbo].[pr_InsertProduct] 
   @zProductCode
  ,@iYear
  ,@zSeason
  ,@zProductName
  ,@zProductName
  ,@zLanguage
  ,@iCategoryID
  ,@iStatus
  ,@iProductType
  ,@iDaysLeadTime
  ,@iNbrOfIssuesPerYear
  ,@iPublisherID
  ,@iFulfillmentHouseID
  ,@zComment
  ,@zVendorNumber
  ,@zVendorSiteName
  ,@zPayGroupLookupCode
  ,@iCurrency
  ,@zGSTRegistrationNumber
  ,@zHSTRegistrationNumber
  ,@zPSTRegistrationNumber
  ,@zOracleCode
  ,@zPrizeLevel
  ,@iPrizeLevelQtyRequired
  ,@zRemitCode
  ,@bIsQSPExclusive
  ,@zEnglishDescription
  ,@zFrenchDescription
  ,@zUserID

SELECT  @iproductId = Product_Instance from QSPCanadaProduct..Product WHERE Product_Code = @zProductCode

SET @dEffectiveDate = '2011-07-01'		--Default dates for Fall of fiscal year 2012
SET @dEndDate = '2011-12-31'			--Default dates for Fall of fiscal year 2012
SET @fRemitRate = NULL					--Not applicable to processing fee
SET @fConversionRate = NULL				--Not applicable to processing fee
SET @fNewsStandPrice = NULL				--Not applicable to processing fee
SET @iListingLevelID = NULL				--Legacy parameter, not used in sp...
SET @zListingCopyText = NULL			--Legacy parameter, not used in sp...
SET @iAdInCatalog = NULL				--Legacy parameter, not used in sp...
SET @iAdPageSizeID = NULL				--Legacy parameter, not used in sp...
SET @iAdPaymentCurrency = NULL			--Legacy parameter, not used in sp...
SET @fAdCost = NULL						--Legacy parameter, not used in sp...
SET @zComment = ''						
SET @iEffortKeyRequired = NULL			--Legacy parameter, not used in sp...
SET @zEffortKey = NULL					--Legacy parameter, not used in sp...
SET @iNumberOfIssues = 0				--Not applicable to processing fee
SET @fBasePrice = NULL					--Legacy parameter, not used in sp...
SET @fQSPPrice = 1						--1$ processing fee
SET @iInternetApproval = NULL			--Legacy parameter, not used in sp...
SET @zABCCode = NULL					--Legacy parameter, not used in sp...
SET @iQSPPremiumID = NULL				--Legacy parameter, not used in sp...
SET @iFSApplicabilityId = 0				--Will be set to NULL by SP when product type <> 46004 
SET @iFSDistributionLevelID = 0			--Will be set to NULL by SP when product type <> 46004 
SET @iFSExtraLimitRate = 0				--Will be set to NULL by SP when product type <> 46004 
SET @bFSIsBrochure = 0					--Will be set to NULL by SP when product type <> 46004 
SET @iTaxRegionID = 0					--Undetermined, determined by customer
SET @zPremiumInd = NULL					--Legacy parameter, not used in sp...
SET @zPremiumCode = NULL				--Legacy parameter, not used in sp...
SET @zPremiumCopy = NULL				--Legacy parameter, not used in sp...
SET @zFSProvinceCode = NULL				--Will be set to NULL by SP when product type <> 46001


EXECUTE [QSPCanadaProduct].[dbo].[pr_InsertPricingDetails_Single] 
   @iproductId
  ,@zProductCode
  ,@iYear
  ,@zSeason
  ,@iCatalogSectionId
  ,@iStatus
  ,@dEffectiveDate
  ,@dEndDate
  ,@dEffectiveDate
  ,@fRemitRate
  ,@fConversionRate
  ,@fNewsStandPrice
  ,@iListingLevelID
  ,@zListingCopyText
  ,@iAdInCatalog
  ,@iAdPageSizeID
  ,@iAdPaymentCurrency
  ,@fAdCost
  ,@zPricingComment
  ,@iEffortKeyRequired
  ,@zEffortKey
  ,@iNumberOfIssues
  ,@fBasePrice
  ,@fQSPPrice
  ,@iInternetApproval
  ,@zABCCode
  ,@iQSPPremiumID
  ,@zOracleCode
  ,@iFSApplicabilityId
  ,@iFSDistributionLevelID
  ,@iFSExtraLimitRate
  ,@bFSIsBrochure
  ,@zOracleCode
  ,@zOracleCode
  ,@iFSProgramID
  ,@iTaxRegionID
  ,@zPremiumInd
  ,@zPremiumCode
  ,@zPremiumCopy
  ,@zFSProvinceCode
  ,@zUserID
  
  SELECT @iPricingDetailsID = MagPrice_Instance FROM QSPCanadaProduct..Pricing_Details WHERE Product_Instance = @iproductId
  
  --Select and return to user all values created by script for use in COD insertions
  SELECT @iProductType as QSPProductLine, @iCatalogSectionType as ProgramSectionType,  @iCatalogId as ProgramMasterID, @iCatalogSectionId as ProgramSectionID, @zProductCode as ProductCode, @iproductId as ProductID, @iPricingDetailsID as PricingDetailsId