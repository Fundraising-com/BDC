CREATE TABLE #ErrorList
(
	Item				VARCHAR(30),
	Error				VARCHAR(100),
	TransactionID		INT,
	AccountID			INT,
	OrderID				INT,
	CreationDate		DATETIME,
	ItemAmount			DECIMAL(16,2),
	GLDebitAmount		DECIMAL(16,2),
	GLCreditAmount		DECIMAL(16,2),
	EntryClerkFirstName	VARCHAR(100),
	EntryClerkLastName	VARCHAR(100)
)

--Get most recent open GL Accounting Period
DECLARE	@StartDate AS DateTime
DECLARE	@EndDate AS DateTime
SELECT	@StartDate = Start_Date,
		@EndDate = End_Date
FROM	Accounting_Period
WHERE	Start_Date = (SELECT MIN(Start_Date) FROM Accounting_Period WHERE Is_Closed = 'N')

--Payments with incorrect GL
SELECT		pmt.Payment_ID,
			SUM(t.Amount) AS Amount
INTO		#PaymentCredit
FROM		Payment pmt
LEFT JOIN	(GL_Entry e
JOIN			GL_Transaction t
					ON	t.GL_Entry_ID = e.GL_Entry_ID
					AND	t.Debit_Credit = 'C')
				ON	e.Payment_ID = pmt.Payment_ID
WHERE		pmt.Payment_Effective_Date BETWEEN @StartDate AND @EndDate
GROUP BY	pmt.Payment_ID

SELECT		pmt.Payment_ID,
			SUM(t.Amount) AS Amount
INTO		#PaymentDebit
FROM		Payment pmt
LEFT JOIN	(GL_Entry e
JOIN			GL_Transaction t
					ON	t.GL_Entry_ID = e.GL_Entry_ID
					AND	t.Debit_Credit = 'D')
				ON	e.Payment_ID = pmt.Payment_ID
WHERE		pmt.Payment_Effective_Date BETWEEN @StartDate AND @EndDate
GROUP BY	pmt.Payment_ID

INSERT INTO	#ErrorList
SELECT		'Payment',
			'Incorrect GL Entry',
			pmt.Payment_ID,
			pmt.Account_ID,
			pmt.Order_ID,
			pmt.Payment_Effective_Date,
			pmt.Payment_Amount,
			pmtDeb.Amount,
			pmtCred.Amount,
			cup.FirstName,
			cup.LastName
FROM		Payment pmt
LEFT JOIN	#PaymentDebit pmtDeb
				ON	pmtDeb.Payment_ID = pmt.Payment_ID
LEFT JOIN	#PaymentCredit pmtCred
				ON	pmtCred.Payment_ID = pmt.Payment_ID
LEFT JOIN	QSPCanadaCommon..CUserProfile cup
				ON	CONVERT(VARCHAR, cup.Instance) = pmt.Last_Updated_By
WHERE		pmt.Payment_Effective_Date BETWEEN @StartDate AND @EndDate
AND			(pmt.Payment_Amount <> ISNULL(pmtCred.Amount, 0)
OR			pmt.Payment_Amount <> ISNULL(pmtDeb.Amount, 0))

--Invoices with incorrect GL
SELECT		inv.Invoice_ID,
			SUM(t.Amount) AS Amount
INTO		#InvoiceCredit
FROM		Invoice inv
LEFT JOIN	(GL_Entry e
JOIN			GL_Transaction t
					ON	t.GL_Entry_ID = e.GL_Entry_ID
					AND	t.Debit_Credit = 'C')
				ON	e.Invoice_ID = inv.Invoice_ID
WHERE		inv.Invoice_Date BETWEEN @StartDate AND @EndDate
GROUP BY	inv.Invoice_ID

SELECT		inv.Invoice_ID,
			SUM(t.Amount) AS Amount
INTO		#InvoiceDebit
FROM		Invoice inv
LEFT JOIN	(GL_Entry e
JOIN			GL_Transaction t
					ON	t.GL_Entry_ID = e.GL_Entry_ID
					AND	t.Debit_Credit = 'D')
				ON	e.Invoice_ID = inv.Invoice_ID
WHERE		inv.Invoice_Date BETWEEN @StartDate AND @EndDate
GROUP BY	inv.Invoice_ID

INSERT INTO	#ErrorList
SELECT		'Invoice',
			'Incorrect GL Entry',
			inv.Invoice_ID,
			inv.Account_ID,
			inv.Order_ID,
			inv.Invoice_Date,
			invSec.Net_Before_Tax + invSec.Total_Tax_Amount + invSec.Group_Profit_Amount,
			invDeb.Amount,
			invCred.Amount,
			cup.FirstName,
			cup.LastName
FROM		Invoice inv
LEFT JOIN	#InvoiceDebit invDeb
				ON	invDeb.Invoice_ID = inv.Invoice_ID
LEFT JOIN	#InvoiceCredit invCred
				ON	invCred.Invoice_ID = inv.Invoice_ID
JOIN		Invoice_Section invSec
				ON	invSec.Invoice_ID = inv.Invoice_ID
LEFT JOIN	QSPCanadaCommon..CUserProfile cup
				ON	CONVERT(VARCHAR, cup.Instance) = inv.Last_Updated_By
WHERE		inv.Invoice_Date BETWEEN @StartDate AND @EndDate
AND			(ABS(invSec.Net_Before_Tax + invSec.Total_Tax_Amount + invSec.Group_Profit_Amount - ISNULL(invDeb.Amount, 0)) > 0.05
OR			ABS(invSec.Net_Before_Tax + invSec.Total_Tax_Amount + invSec.Group_Profit_Amount - ISNULL(invCred.Amount, 0)) > 0.05)

--Adjustments with incorrect GL
SELECT		adj.Adjustment_ID,
			SUM(t.Amount) AS Amount
INTO		#AdjustmentCredit
FROM		Adjustment adj
LEFT JOIN	(GL_Entry e
JOIN			GL_Transaction t
					ON	t.GL_Entry_ID = e.GL_Entry_ID
					AND	t.Debit_Credit = 'C')
				ON	e.Adjustment_ID = adj.Adjustment_ID
WHERE		adj.Date_Created BETWEEN @StartDate AND @EndDate
GROUP BY	adj.Adjustment_ID

SELECT		adj.Adjustment_ID,
			SUM(t.Amount) AS Amount
INTO		#AdjustmentDebit
FROM		Adjustment adj
LEFT JOIN	(GL_Entry e
JOIN			GL_Transaction t
					ON	t.GL_Entry_ID = e.GL_Entry_ID
					AND	t.Debit_Credit = 'D')
				ON	e.Adjustment_ID = adj.Adjustment_ID
WHERE		adj.Date_Created BETWEEN @StartDate AND @EndDate
GROUP BY	adj.Adjustment_ID

INSERT INTO	#ErrorList
SELECT		'Adjustment',
			'Incorrect GL Entry',
			adj.Adjustment_ID,
			adj.Account_ID,
			adj.Order_ID,
			adj.Date_Created,
			adj.Adjustment_Amount,
			adjDeb.Amount,
			adjCred.Amount,
			cup.FirstName,
			cup.LastName
FROM		Adjustment adj
LEFT JOIN	#AdjustmentDebit adjDeb
				ON	adjDeb.Adjustment_ID = adj.Adjustment_ID
LEFT JOIN	#AdjustmentCredit adjCred
				ON	adjCred.Adjustment_ID = adj.Adjustment_ID
LEFT JOIN	QSPCanadaCommon..CUserProfile cup
				ON	CONVERT(VARCHAR, cup.Instance) = adj.Last_Updated_By
WHERE		adj.Date_Created BETWEEN @StartDate AND @EndDate
AND			(ISNULL(adjDeb.Amount, 0) <> ISNULL(adjCred.Amount, 0)
OR			ISNULL(adjDeb.Amount, 0) <= 0
OR			ISNULL(adjCred.Amount, 0) <= 0)

--Payments in GL who don't have Payment records
DECLARE	@Accounting_Year AS INT
DECLARE	@Accounting_Period AS INT
SELECT	@Accounting_Year = Accounting_Year,
		@Accounting_Period = Accounting_Period
FROM	Accounting_Period
WHERE	Start_Date = (SELECT MIN(Start_Date) FROM Accounting_Period WHERE Is_Closed = 'N')

INSERT INTO	#ErrorList
SELECT		'Payment',
			'GL Entry missing Payment Record',
			e.GL_Entry_ID,
			NULL,
			NULL,
			e.GL_Entry_Date,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL
FROM		GL_Entry e
LEFT JOIN	Payment p
				ON	p.Payment_ID = e.Payment_ID
WHERE		p.Payment_ID IS NULL
AND			e.Payment_ID > 0
AND			e.Accounting_Year = @Accounting_Year
AND			e.Accounting_Period = @Accounting_Period

--Invoices in GL who don't have Invoice records
INSERT INTO	#ErrorList
SELECT		'Invoice',
			'GL Entry missing Invoice Record',
			e.GL_Entry_ID,
			NULL,
			NULL,
			e.GL_Entry_Date,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL
FROM		GL_Entry e
LEFT JOIN	Invoice i
				ON	i.Invoice_ID = e.Invoice_ID
WHERE		i.Invoice_ID IS NULL
AND			e.Invoice_ID > 0
AND			e.Accounting_Year = @Accounting_Year
AND			e.Accounting_Period = @Accounting_Period

--Adjustments in GL who don't have Adjustment records
INSERT INTO	#ErrorList
SELECT		'Adjustment',
			'GL Entry missing Adjustment Record',
			e.GL_Entry_ID,
			NULL,
			NULL,
			e.GL_Entry_Date,
			NULL,
			NULL,
			NULL,
			NULL,
			NULL
FROM		GL_Entry e
LEFT JOIN	Adjustment a
				ON	a.Adjustment_ID = e.Adjustment_ID
WHERE		a.Adjustment_ID IS NULL
AND			e.Adjustment_ID > 0
AND			e.Accounting_Year = @Accounting_Year
AND			e.Accounting_Period = @Accounting_Period

--Select all Errors
SELECT	*
FROM	#ErrorList