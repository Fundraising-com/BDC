USE QSPCanadaFinance

DECLARE @Accounting_Year	INT,
		@Accounting_Period	INT

SET	@Accounting_Year = 2009
SET	@Accounting_Period = 8

UPDATE	Accounting_Period
SET		Is_Closed = 'N'
WHERE	Accounting_Year = @Accounting_Year
AND		Accounting_Period = @Accounting_Period

UPDATE	GL_Entry
SET		Is_Posted = 'N',
		GL_Posting_Date = NULL
WHERE	Accounting_Year = @Accounting_Year
AND		Accounting_Period = @Accounting_Period

DELETE	GLAccountBalance
WHERE	AccountingYear = @Accounting_Year
AND		AccountingPeriod = @Accounting_Period

SELECT	*
FROM	Accounting_Period

SELECT	*
FROM	GL_Entry
WHERE	Accounting_Year = @Accounting_Year
AND		Accounting_Period = @Accounting_Period