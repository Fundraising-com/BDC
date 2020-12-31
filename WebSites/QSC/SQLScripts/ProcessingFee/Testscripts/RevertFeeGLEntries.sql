USE QSPCanadaFinance

DECLARE @iTransactionTypeID int
DECLARE @zDescription varchar(100)
Declare @zAccount varchar(50)
DECLARE @iProductLineID int

SET @zAccount = '99999'	
SET @iTransactionTypeID = 1			--invoice type
SET @zDescription = 'Processing fees'
SET @iProductLineID = 46017

DELETE FROM [QSPCanadaFinance].[dbo].[GLAccount]
WHERE Account = @zAccount

DELETE FROM GLEntryTYPE
WHERE TransactionTypeID = @iTransactionTypeID AND Description = @zDescription

DELETE FROM [QSPCanadaFinance].[dbo].[GLAccountMap]
WHERE ProductLineID = @iProductLineID;