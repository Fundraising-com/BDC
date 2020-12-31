USE QSPCanadaFinance

DECLARE @iTransactionTypeID int
DECLARE @zDescription varchar(100)
DECLARE @iGlEntryTypeId int

/* The next block are all information related to GL account, and have been filled out with information provided by Janet Welch */
DECLARE @iGLAccountID int
DECLARE @iGLAccountStatusID int
DECLARE @iGLAccountSystemID int
Declare @zAccount varchar(50)
DECLARE @zDivision varchar(50)
DECLARE @zProduct varchar(50)
DECLARE @zDepartment varchar(50)
DECLARE @zProject varchar(50)
DECLARE @zSource varchar(50)
DECLARE @zGeographic  varchar(50)
DECLARE @zOther varchar(50)
DECLARE @zAffiliate varchar(50)
DECLARE @zEntity varchar(50)
DECLARE @zLangMarket varchar(50)
DECLARE @zDistChannel varchar(50)
DECLARE @zSegment varchar(50)
DECLARE @dCreateDate datetime
DECLARE @iBusinessUnitID int

/* GlAccountMap variables */
DECLARE @iProductLineID int
DECLARE @iTaxId int
DECLARE @bDebit	bit
Declare @iCurrencyID int
DECLARE @iPaymentMethodID int

SET @iGLAccountStatusID = 1				--Active
SET @iGLAccountSystemID = 2				--Provided by Janet Welch
SET @zDescription = 'Canada processing fees'
SET @zAccount = '320603'				--Provided by Janet Welch
SET @zDivision = '471'					--Provided by Janet Welch
SET @zProduct = '480'					--Provided by Janet Welch
SET @zDepartment = '1511'				--Provided by Janet Welch
SET @zProject = NULL					--Provided by Janet Welch
SET @zSource = '0806'					--Provided by Janet Welch
SET @zGeographic  = NULL				--Provided by Janet Welch
SET @zOther = NULL					--All GLAccountSystemID 2 values are null for Other
SET @zAffiliate = NULL					--All GLAccountSystemID 2 values are null for Affiliate
SET @zEntity = NULL					--All GLAccountSystemID 2 values are null for entity
SET @zLangMarket = NULL					--All GLAccountSystemID 2 values are null for LangMarket
SET @zDistChannel = NULL				--All GLAccountSystemID 2 values are null for DistChannel
SET @zSegment = '000'					--All GLAccountSystemID 2 values are null for Segment
SET @dCreateDate = GetDate()				
SET @iBusinessUnitID = 1				--Provided by Janet Welch

/* CREATE GL Account for processing fees */
INSERT INTO [QSPCanadaFinance].[dbo].[GLAccount]
           ([GLAccountStatusID]
           ,[GLAccountSystemID]
           ,[Description]
           ,[Account]
           ,[Division]
           ,[Product]
           ,[Department]
           ,[Project]
           ,[Source]
           ,[Geographic]
           ,[Other]
           ,[Affiliate]
           ,[Entity]
           ,[LangMarket]
           ,[DistChannel]
           ,[Segment]
           ,[CreateDate]
           ,[BusinessUnitID])
     VALUES
           (@iGLAccountStatusID,
			@iGLAccountSystemID,
			@zDescription,
			@zAccount,
			@zDivision,
			@zProduct,
			@zDepartment,
			@zProject,
			@zSource,
			@zGeographic,
			@zOther,
			@zAffiliate,
			@zEntity,
			@zLangMarket,
			@zDistChannel,
			@zSegment,
			@dCreateDate,
			@iBusinessUnitID)

SET @iGLAccountID = SCOPE_IDENTITY()

/* Create GlEntryType */
SET @iTransactionTypeID = 1			--invoice type
SET @zDescription = 'Processing fees'

INSERT INTO GLEntryTYPE
	(TransactionTypeID,
	Description)
VALUES
	(@iTransactionTypeID,
	@zDescription)

SET @iGlEntryTypeId = SCOPE_IDENTITY()

-- TODO: Change if QSPCanadaProduct..QSPProductLine is set to another value during CreateFeeProduct.sql
SET @iProductLineID = 46017
SET @iTaxId = NULL
SET @bDebit	= 0
SET @iCurrencyID= NULL
SET @iPaymentMethodID= NULL
/* Create GlAccountMap to link GL to QSPProductLine */

INSERT INTO [QSPCanadaFinance].[dbo].[GLAccountMap]
           ([GLEntryTypeID]
           ,[ProductLineID]
           ,[TaxID]
           ,[Debit]
           ,[GLAccountID]
           ,[CurrencyID]
           ,[PaymentMethodID]
           ,[BusinessUnitID])
     VALUES
           (@iGlEntryTypeId,
           @iProductLineID,
           @iTaxId,
           @bDebit,
           @iGLAccountID,
           @iCurrencyID,
           @iPaymentMethodID,
           @iBusinessUnitID)

SELECT @iGLAccountID as GLAccountID, @iGlEntryTypeId as GlEntryTypeId





