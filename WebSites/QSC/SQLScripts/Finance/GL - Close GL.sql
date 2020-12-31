--Mark entries as posted
DECLARE	@Accounting_Year AS INT
DECLARE	@Accounting_Period AS INT
SELECT	@Accounting_Year = Accounting_Year,
		@Accounting_Period = Accounting_Period
FROM	Accounting_Period
WHERE	Start_Date = (SELECT MIN(Start_Date) FROM Accounting_Period WHERE Is_Closed = 'N')

BEGIN TRAN t1
UPDATE	GL_entry
SET		Is_Posted = 'Y',
		GL_Posting_Date = GETDATE()
WHERE	Is_Posted = 'N'
AND		Accounting_Year = @Accounting_Year
AND		Accounting_Period = @Accounting_Period
COMMIT TRAN t1

--Run QSPCanadaFinance.dbo.ValidateCurrentPeriod and check for error message
DECLARE	@ErrorMessage AS varchar(200)
EXEC	QSPCanadaFinance..ValidateCurrentPeriod 'CA', @ErrorMessage OUTPUT 
SELECT	@ErrorMessage

--Run QSPCanadaFinance.dbo.CloseCurrentPeriod
DECLARE	@ErrorMessage AS varchar(200)
EXEC	QSPCanadaFinance..CloseCurrentPeriod 'CA', @ErrorMessage OUTPUT 
SELECT	@ErrorMessage

SELECT	*
FROM	Accounting_Period

--GL Summary by Account
DECLARE	@Accounting_Year AS INT
DECLARE	@Accounting_Period AS INT
SELECT	@Accounting_Year = Accounting_Year,
		@Accounting_Period = Accounting_Period
FROM	Accounting_Period
WHERE	Start_Date = (SELECT MAX(Start_Date) FROM Accounting_Period WHERE Is_Closed = 'Y')

SELECT		t.GL_Account_Number,
			acc.GL_Account_Description,
			SUM(t.Amount) AS Amount,
			t.Debit_Credit
FROM		GL_Entry e
JOIN		GL_Transaction t
				ON	t.GL_Entry_ID = e.GL_Entry_ID
LEFT JOIN	GL_Account acc
				ON	acc.GL_Account_Number = t.GL_Account_Number
WHERE		Accounting_Period = @Accounting_Period
AND			Accounting_Year = @Accounting_Year
GROUP BY	t.GL_Account_Number,
			acc.GL_Account_Description,
			t.Debit_Credit
ORDER BY	t.GL_Account_Number

/*If asked for GL Detail by Account
DECLARE	@Accounting_Year AS INT
DECLARE	@Accounting_Period AS INT
SELECT	@Accounting_Year = Accounting_Year,
		@Accounting_Period = Accounting_Period
FROM	Accounting_Period
WHERE	Start_Date = (SELECT MAX(Start_Date) FROM Accounting_Period WHERE Is_Closed = 'Y')

SELECT	t.GL_Account_Number,
		t.Amount,
		t.Debit_Credit,
		e.Invoice_ID,
		e.Payment_ID,
		e.Adjustment_ID,
		e.GL_Entry_Date,
		e.GL_Posting_Date,
		e.Description
FROM	GL_Entry e
JOIN	GL_Transaction t
			ON	t.GL_Entry_ID = e.GL_Entry_ID
WHERE	Accounting_Period = @Accounting_Period
AND		Accounting_Year = @Accounting_Year
ORDER BY t.GL_Account_Number, Debit_Credit, GL_Entry_Date
*/