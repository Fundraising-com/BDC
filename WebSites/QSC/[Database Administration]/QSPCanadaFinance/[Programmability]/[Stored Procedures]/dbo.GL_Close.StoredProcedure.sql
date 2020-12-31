USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GL_Close]    Script Date: 06/07/2017 09:17:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GL_Close]

	@Accounting_Year	INT,
	@Accounting_Period	INT

AS

DECLARE	@Last_Accounting_Year	INT,
		@Last_Accounting_Period	INT

SELECT	@Last_Accounting_Year = Accounting_Year,
		@Last_Accounting_Period = Accounting_Period
FROM	Accounting_Period
WHERE	[Start_Date] = (SELECT MAX(Start_Date) FROM Accounting_Period WHERE Is_Closed = 'Y')


DECLARE	@GLAccountID		VARCHAR(200),
		@OpeningBalance		DECIMAL(18,2),
		@Amount				DECIMAL(18,2)

BEGIN TRANSACTION

EXEC	[dbo].[GL_Entry_SwitchToGAO]
		@AccountingYear = @Accounting_Year,
		@AccountingPeriod = @Accounting_Period

UPDATE	GL_entry
SET		Is_Posted = 'Y',
		GL_Posting_Date = GETDATE()
WHERE	Is_Posted = 'N'
AND		Accounting_Year = @Accounting_Year
AND		Accounting_Period = @Accounting_Period

UPDATE	Accounting_Period
SET		Is_Closed = 'Y'
WHERE	Accounting_Year = @Accounting_Year
AND		Accounting_Period = @Accounting_Period

DECLARE		GLBalance CURSOR FOR
SELECT		GLAccountID
FROM		GLAccount
ORDER BY	GLAccountID

OPEN GLBalance
FETCH NEXT FROM GLBalance INTO @GLAccountID

WHILE @@FETCH_STATUS = 0
BEGIN

	SELECT	@Amount = ISNULL(SUM(	CASE glt.Debit_Credit
										WHEN 'C' THEN	glt.Amount * -1
										ELSE			glt.Amount
									END), 0)
	FROM	GL_Entry gle
	JOIN	GL_Transaction glt
				ON	glt.GL_Entry_ID = gle.GL_Entry_ID
	WHERE	glt.GLAccountID = @GLAccountID
	AND		gle.Accounting_Year = @Accounting_Year
	AND		gle.Accounting_Period = @Accounting_Period

	SELECT	@OpeningBalance = ClosingBalance
	FROM	GLAccountBalance
	WHERE	GLAccountID = @GLAccountID
	AND		AccountingYear = @Last_Accounting_Year
	AND		AccountingPeriod = @Last_Accounting_Period

	INSERT INTO	GLAccountBalance
	(
		GLAccountID,
		AccountingYear,
		AccountingPeriod,
		OpeningBalance,
		ClosingBalance
	)
	Values
	(
		@GLAccountID,
		@Accounting_Year,
		@Accounting_Period,
		ISNULL(@OpeningBalance, 0),
		ISNULL(@OpeningBalance + @Amount, 0)
	)

	SET @Amount = 0
	SET	@OpeningBalance = 0

	FETCH NEXT FROM GLBalance INTO @GLAccountID

END
CLOSE GLBalance
DEALLOCATE GLBalance

COMMIT TRANSACTION
GO
