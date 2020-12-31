USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AddGLEntriesForAdjustment]    Script Date: 06/07/2017 09:17:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddGLEntriesForAdjustment]

	@AccountID		INT,
	@OrderID		INT,
	@AdjustmentID 	INT,
	@AdjustmentType INT,
	@Amount			NUMERIC(10, 2),
	@ChangedBy		VARCHAR(20),
	@RefundID		INT = NULL

AS

DECLARE @FiscalYear 		SMALLINT,
		@Output				INT,
		@Description		VARCHAR(100),
		@ErrorMessage		VARCHAR(150)

DECLARE @AccountType INT
EXEC GetAccountType @AccountID, @AccountType OUTPUT

DECLARE @BusinessUnitID INT
SELECT	@BusinessUnitID = acc.BusinessUnitID
FROM	QSPCanadaCommon..CAccount acc
WHERE	acc.ID = @AccountID

DECLARE @AdjustmentDescription	VARCHAR(100)

SELECT		@AdjustmentDescription = adjType.Name
FROM		Adjustment_Type adjType
WHERE		adjType.Adjustment_Type_ID = @AdjustmentType

DECLARE	@GLEntryID	INT

EXEC	GL_Entry_Insert
		@AdjustmentID = @AdjustmentID,
		@Description = @AdjustmentDescription,
		@CountryCode = 'CA',
		@BusinessUnitID = @BusinessUnitID,
		@GLEntryID = @GLEntryID OUTPUT


DECLARE		GLEntryCursor CURSOR FOR
SELECT		glAcc.GLAccountID,
			glAccAdjType.Debit
FROM		Adjustment_Type adjType
JOIN		GLAccountAdjustmentType glAccAdjType
				ON	adjType.Adjustment_Type_ID = glAccAdjType.AdjustmentTypeID
JOIN		GLAccount glAcc
				ON	glAcc.GLAccountID = glAccAdjType.GLAccountID
WHERE		adjType.Adjustment_Type_ID = @AdjustmentType
ORDER BY	glAcc.GLAccountID,
			glAccAdjType.Debit

DECLARE @GLAccountID			INT,
		@Debit					BIT

OPEN GLEntryCursor
FETCH NEXT FROM GLEntryCursor INTO @GLAccountID, @Debit

WHILE @@FETCH_STATUS = 0
BEGIN

	INSERT GL_Transaction
	(
		GL_Entry_ID,
		GLAccountID,
		Debit_Credit,
		Amount,
		GL_Transaction_Status_ID
	)
	SELECT	@GLEntryID,
			@GLAccountID,
			CASE @Debit
				WHEN 0 THEN	'C'
				ELSE		'D'
			END,
			@Amount,
			2

	FETCH NEXT FROM GLEntryCursor INTO @GLAccountID, @Debit

END
CLOSE GLEntryCursor
DEALLOCATE GLEntryCursor
GO
