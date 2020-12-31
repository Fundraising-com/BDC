USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GL_Validate]    Script Date: 06/07/2017 09:17:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GL_Validate]

	@Accounting_Year	INT,
	@Accounting_Period	INT,
	@IsValidPeriod BIT OUTPUT

AS

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

DECLARE	@StartDate			DATETIME,
		@EndDate			DATETIME
SELECT	@StartDate = [Start_Date],
		@EndDate = End_Date
FROM	Accounting_Period
WHERE	Accounting_Year = @Accounting_Year
AND		Accounting_Period = @Accounting_Period

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
			ISNULL(cup.FirstName, pmt.Last_Updated_By),
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
AND			t.GLAccountID NOT IN (244, 245, 246, 247, 250, 251, 252, 253) -- TRT Liability Relief
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
AND			t.GLAccountID NOT IN (244, 245, 246, 247, 250, 251, 252, 253) -- TRT Liability Relief
GROUP BY	inv.Invoice_ID

INSERT INTO	#ErrorList
SELECT		'Invoice',
			'Incorrect GL Entry',
			inv.Invoice_ID,
			inv.Account_ID,
			inv.Order_ID,
			inv.Invoice_Date,
			(SELECT ABS(SUM(invSec.Net_Before_Tax + invSec.Total_Tax_Amount + invSec.Group_Profit_Amount + ISNULL(invSec.ThirdParty_Profit_Amount, 0.00))) FROM Invoice_Section invSec WHERE invSec.Invoice_ID = inv.Invoice_ID),
			invDeb.Amount,
			invCred.Amount,
			ISNULL(cup.FirstName, inv.Last_Updated_By),
			cup.LastName
FROM		Invoice inv
LEFT JOIN	#InvoiceDebit invDeb
				ON	invDeb.Invoice_ID = inv.Invoice_ID
LEFT JOIN	#InvoiceCredit invCred
				ON	invCred.Invoice_ID = inv.Invoice_ID
LEFT JOIN	QSPCanadaCommon..CUserProfile cup
				ON	CONVERT(VARCHAR, cup.Instance) = inv.Last_Updated_By
WHERE		inv.Invoice_Date BETWEEN @StartDate AND @EndDate
AND			(ABS((SELECT ABS(SUM(invSec.Net_Before_Tax + invSec.Total_Tax_Amount + invSec.Group_Profit_Amount + ISNULL(invSec.ThirdParty_Profit_Amount, 0.00))) FROM Invoice_Section invSec WHERE invSec.Invoice_ID = inv.Invoice_ID) - ISNULL(invDeb.Amount, 0)) > 0.02
OR			ISNULL(invDeb.Amount, 0) <> ISNULL(invCred.Amount, 0))

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
			ISNULL(cup.FirstName, adj.Last_Updated_By),
			cup.LastName
FROM		Adjustment adj
JOIN		Adjustment_Type adjType
				ON	adjType.Adjustment_Type_ID = adj.Adjustment_Type_ID
LEFT JOIN	#AdjustmentDebit adjDeb
				ON	adjDeb.Adjustment_ID = adj.Adjustment_ID
LEFT JOIN	#AdjustmentCredit adjCred
				ON	adjCred.Adjustment_ID = adj.Adjustment_ID
LEFT JOIN	QSPCanadaCommon..CUserProfile cup
				ON	CONVERT(VARCHAR, cup.Instance) = adj.Last_Updated_By
WHERE		adj.Date_Created BETWEEN @StartDate AND @EndDate
AND			adj.Adjustment_Type_ID NOT IN (49028, 49030) --49028: Online Profit, 49030: Customer Service
AND			(ISNULL(adjDeb.Amount, 0) <> ISNULL(adjCred.Amount, 0)
OR			ISNULL(adj.Adjustment_Amount * CASE adjType.Debit_Credit WHEN 'C' THEN 1 ELSE -1 END, 0) < 0
OR			ISNULL(adjDeb.Amount, 0) <= 0
OR			ISNULL(adjCred.Amount, 0) <= 0)
AND			adjType.ExcludeFromInvoicing = 0

--Remit AP Cheques with incorrect GL
SELECT		apcr.AP_Cheque_Remit_ID,
			SUM(t.Amount) AS Amount
INTO		#APChequeRemitCredit
FROM		AP_Cheque_Remit apcr
LEFT JOIN	(GL_Entry e
JOIN			GL_Transaction t
					ON	t.GL_Entry_ID = e.GL_Entry_ID
					AND	t.Debit_Credit = 'C')
				ON	e.AP_Cheque_Remit_ID = apcr.AP_Cheque_Remit_ID
				AND	e.Description NOT LIKE 'Cancel%'
WHERE		apcr.CreationDate BETWEEN @StartDate AND @EndDate
GROUP BY	apcr.AP_Cheque_Remit_ID

SELECT		apcr.AP_Cheque_Remit_ID,
			SUM(t.Amount) AS Amount
INTO		#APChequeRemitDebit
FROM		AP_Cheque_Remit apcr
LEFT JOIN	(GL_Entry e
JOIN			GL_Transaction t
					ON	t.GL_Entry_ID = e.GL_Entry_ID
					AND	t.Debit_Credit = 'D')
				ON	e.AP_Cheque_Remit_ID = apcr.AP_Cheque_Remit_ID
				AND	e.Description NOT LIKE 'Cancel%'
WHERE		apcr.CreationDate BETWEEN @StartDate AND @EndDate
GROUP BY	apcr.AP_Cheque_Remit_ID

INSERT INTO	#ErrorList
SELECT		'APChequeRemit',
			'Incorrect GL Entry',
			apcr.AP_Cheque_Remit_ID,
			NULL,
			NULL,
			apcr.CreationDate,
			ISNULL(apcr.NetAmount, 0) + ISNULL(apcr.GSTAmount, 0.00) + ISNULL(apcr.HSTAmount, 0.00) + ISNULL(apcr.PSTAmount, 0.00),
			apcrDeb.Amount,
			apcrCred.Amount,
			NULL,
			NULL
FROM		AP_Cheque_Remit apcr
LEFT JOIN	#APChequeRemitDebit apcrDeb
				ON	apcrDeb.AP_Cheque_Remit_ID = apcr.AP_Cheque_Remit_ID
LEFT JOIN	#APChequeRemitCredit apcrCred
				ON	apcrCred.AP_Cheque_Remit_ID = apcr.AP_Cheque_Remit_ID
WHERE		apcr.CreationDate BETWEEN @StartDate AND @EndDate
AND			(ISNULL(apcr.NetAmount, 0) + ISNULL(apcr.GSTAmount, 0.00) + ISNULL(apcr.HSTAmount, 0.00) + ISNULL(apcr.PSTAmount, 0.00) <> ISNULL(apcrCred.Amount, 0)
OR			ISNULL(apcr.NetAmount, 0) + ISNULL(apcr.GSTAmount, 0.00) + ISNULL(apcr.HSTAmount, 0.00) + ISNULL(apcr.PSTAmount, 0.00) <> ISNULL(apcrDeb.Amount, 0))

--Refund Cheques with incorrect GL
SELECT		ref.Refund_ID,
			SUM(t.Amount) AS Amount
INTO		#RefundCredit
FROM		Refund ref
LEFT JOIN	(GL_Entry e
JOIN			GL_Transaction t
					ON	t.GL_Entry_ID = e.GL_Entry_ID
					AND	t.Debit_Credit = 'C')
				ON	e.Refund_ID = ref.Refund_ID
WHERE		ref.CreateDate BETWEEN @StartDate AND @EndDate
GROUP BY	ref.Refund_ID

SELECT		ref.Refund_ID,
			SUM(t.Amount) AS Amount
INTO		#RefundDebit
FROM		Refund ref
LEFT JOIN	(GL_Entry e
JOIN			GL_Transaction t
					ON	t.GL_Entry_ID = e.GL_Entry_ID
					AND	t.Debit_Credit = 'D')
				ON	e.Refund_ID = ref.Refund_ID
WHERE		ref.CreateDate BETWEEN @StartDate AND @EndDate
GROUP BY	ref.Refund_ID

INSERT INTO	#ErrorList
SELECT		'Refund',
			'Incorrect GL Entry',
			ref.Refund_ID,
			NULL,
			NULL,
			ref.CreateDate,
			ref.Amount,
			refDeb.Amount,
			refCred.Amount,
			NULL,
			NULL
FROM		Refund ref
LEFT JOIN	#RefundDebit refDeb
				ON	refDeb.Refund_ID = ref.Refund_ID
LEFT JOIN	#RefundCredit refCred
				ON	refCred.Refund_ID = ref.Refund_ID
WHERE		ref.Refund_Type_ID IN (1) --1: Customer Refund
AND			ref.CreateDate BETWEEN @StartDate AND @EndDate
AND			(ISNULL(refDeb.Amount, 0) <> ISNULL(refCred.Amount, 0)
OR			ISNULL(ref.Amount, 0) <= 0
OR			ISNULL(refDeb.Amount, 0) <= 0
OR			ISNULL(refCred.Amount, 0) <= 0)

--Payments in GL who don't have Payment records
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

--Remit AP Cheque in GL who don't have Remit AP Cheque records
INSERT INTO	#ErrorList
SELECT		'APRemitCheque',
			'GL Entry missing AP Cheque Record',
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
LEFT JOIN	AP_Cheque_Remit apcr
				ON	apcr.AP_Cheque_Remit_ID = e.AP_Cheque_Remit_ID
WHERE		apcr.AP_Cheque_Remit_ID IS NULL
AND			e.AP_Cheque_Remit_ID > 0
AND			e.Accounting_Year = @Accounting_Year
AND			e.Accounting_Period = @Accounting_Period

--Refund in GL who don't have Refund records
INSERT INTO	#ErrorList
SELECT		'Refund',
			'GL Entry missing Refund Record',
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
LEFT JOIN	Refund ref
				ON	ref.Refund_ID = e.Refund_ID
WHERE		ref.Refund_ID IS NULL
AND			e.Refund_ID > 0
AND			e.Accounting_Year = @Accounting_Year
AND			e.Accounting_Period = @Accounting_Period

--Unapproved Payments
INSERT INTO	#ErrorList
SELECT		'Payment',
			'Unapproved',
			pmt.Payment_ID,
			pmt.Account_ID,
			pmt.Order_ID,
			pmt.Payment_Effective_Date,
			pmt.Payment_Amount,
			NULL,
			NULL,
			ISNULL(cup.FirstName, pmt.Last_Updated_By),
			cup.LastName
FROM		Payment pmt
LEFT JOIN	QSPCanadaCommon..CUserProfile cup
				ON	CONVERT(VARCHAR, cup.Instance) = pmt.Last_Updated_By
WHERE		pmt.Payment_Effective_Date BETWEEN @StartDate AND @EndDate
AND			pmt.DateTime_Created IS NULL

--Unapproved Invoices
INSERT INTO	#ErrorList
SELECT		'Invoice',
			'Unapproved',
			inv.Invoice_ID,
			inv.Account_ID,
			inv.Order_ID,
			inv.Invoice_Date,
			NULL,
			NULL,
			NULL,
			ISNULL(cup.FirstName, inv.Last_Updated_By),
			cup.LastName
FROM		Invoice inv
LEFT JOIN	QSPCanadaCommon..CUserProfile cup
				ON	CONVERT(VARCHAR, cup.Instance) = inv.Last_Updated_By
WHERE		inv.Invoice_Date BETWEEN @StartDate AND @EndDate
AND			inv.DateTime_Approved IS NULL

--Unapproved Adjustments
INSERT INTO	#ErrorList
SELECT		'Adjustment',
			'Incorrect GL Entry',
			adj.Adjustment_ID,
			adj.Account_ID,
			adj.Order_ID,
			adj.Date_Created,
			adj.Adjustment_Amount,
			NULL,
			NULL,
			ISNULL(cup.FirstName, adj.Last_Updated_By),
			cup.LastName
FROM		Adjustment adj
LEFT JOIN	QSPCanadaCommon..CUserProfile cup
				ON	CONVERT(VARCHAR, cup.Instance) = adj.Last_Updated_By
WHERE		adj.Date_Created BETWEEN @StartDate AND @EndDate
AND			Date_Created IS NULL

--Payments tied to Orders that don't show on the Statement
INSERT INTO	#ErrorList
SELECT		'Payment',
			'Tied to an Order that doesn''t show on the Statement',
			pmt.Payment_ID,
			pmt.Account_ID,
			pmt.Order_ID,
			pmt.Payment_Effective_Date,
			pmt.Payment_Amount,
			NULL,
			NULL,
			ISNULL(cup.FirstName, pmt.Last_Updated_By),
			cup.LastName
FROM		Payment pmt
JOIN		QSPCanadaOrderManagement..Batch b ON b.OrderID = pmt.ORDER_ID
LEFT JOIN	QSPCanadaCommon..CUserProfile cup
				ON	CONVERT(VARCHAR, cup.Instance) = pmt.Last_Updated_By
WHERE		pmt.Payment_Effective_Date BETWEEN @StartDate AND @EndDate
AND			b.OrderQualifierID NOT IN (39001, 39002, 39003, 39005, 39006, 39007, 39016, 39017, 39018, 39019, 39020, 39021, 39022, 39023)
AND			pmt.LAST_UPDATED_BY NOT IN ('cc_pack_execution')


--Select all Errors
SELECT		*
FROM		#ErrorList
ORDER BY	Item,
			Error,
			TransactionID

DECLARE @NumErrors INT
SELECT	@NumErrors = COUNT(*)
FROM	#ErrorList

IF @NumErrors > 0
BEGIN
	SET @IsValidPeriod = 0
END
ELSE
BEGIN
	SET @IsValidPeriod = 1
END
GO
