USE [QSPCanadaOrderManagement]

SELECT		cod.CustomerOrderHeaderInstance,
			cod.TransID,
			ref.AP_Cheque_ID,
			crh.Instance AS CustomerRemitHistoryInstance,
			incAct.Instance AS IncidentActionInstance,
			inc.IncidentInstance,
			ref.Amount,
			ref.Refund_ID,
			ref.CreateDate,
			ref.Address1,
			ref.Address2,
			ref.City,
			ref.Province,
			ref.PostalCode,
			ref.Country,
			ref.FirstName,
			ref.LastName
INTO		#SubsToChange
FROM		CustomerOrderDetail cod
LEFT JOIN	QSPCanadaFinance..Refund ref
				ON	ref.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	ref.TransID = cod.TransID
LEFT JOIN	(CustomerOrderDetailRemitHistory codrh
JOIN			CustomerRemitHistory crh
					ON	crh.Instance = codrh.CustomerRemitHistoryInstance)
				ON	codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	codrh.TransID = cod.TransID
				AND	codrh.Status = 42002
LEFT JOIN	(Incident inc
JOIN			IncidentAction incAct
					ON	incAct.IncidentInstance = inc.IncidentInstance
					AND	incAct.ActionInstance IN (1, 3)) --1: Cancel, 3: Refund
				ON	inc.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	inc.TransID = cod.TransID
WHERE		cod.CustomerOrderHeaderInstance = 10484178
AND			cod.TransID = 1

SELECT	*
FROM	#SubsToChange

SELECT	*
FROM	QSPCanadaFinance..Refund ref
JOIN	#SubsToChange stc
			ON	stc.CustomerOrderHeaderInstance = ref.CustomerOrderHeaderInstance
			AND	stc.TransID = ref.TransID

--Modify/Delete the refund
BEGIN TRAN t1
UPDATE	ref
SET		Postal_Code = 'M1E1W6'
FROM	QSPCanadaFinance..Refund ref
JOIN	#SubsToChange stc
			ON	stc.CustomerOrderHeaderInstance = ref.CustomerOrderHeaderInstance
			AND	stc.TransID = ref.TransID
COMMIT TRAN t1

BEGIN TRAN t2
DELETE	ref
FROM	QSPCanadaFinance..Refund ref
JOIN	#SubsToChange stc
			ON	stc.CustomerOrderHeaderInstance = ref.CustomerOrderHeaderInstance
			AND	stc.TransID = ref.TransID
COMMIT TRAN t2

--If needed, remove canceled sub and incidents
BEGIN TRAN t3
DELETE	codrh
FROM	CustomerOrderDetailRemitHistory codrh
JOIN	#SubsToChange stc
			ON	stc.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
			AND	stc.TransID = codrh.TransID
WHERE	codrh.Status IN (42002)
COMMIT TRAN t3

BEGIN TRAN t4
DELETE	incAct
FROM	IncidentAction incAct
JOIN	#SubsToChange stc
			ON	stc.IncidentActionInstance = incAct.Instance

DELETE	QSPCanadaOrderManagement..Incident
FROM	Incident inc
JOIN	#SubsToChange stc
			ON	stc.IncidentInstance = inc.IncidentInstance
COMMIT TRAN t4

--If needed add reversing GL entries  (or delete gl_entry and gl_transactions)
BEGIN TRAN t5

DECLARE @GL_Entry_ID				INT,
		@LiabilityGLAccountNumber	VARCHAR(50),
		@DistGLAccountNumber		VARCHAR(50),
		@CashGLAccountNumber		VARCHAR(50),
		@RunDate					DATETIME
		@Amount						NUMERIC(10, 2)

SET @RunDate = GETDATE()

SET @Amount = 65.00

SELECT	@GLAccountingYear = Accounting_Year,
		@GLAccountingPeriod = Accounting_Period
FROM	QSPCanadaFinance..Accounting_Period 
WHERE	@RunDate BETWEEN Start_Date AND End_Date
AND		Is_Closed = 'N'

SET @LiabilityGLAccountNumber = '062.2001.0000.1007.00.00.000'
SET @CashGLAccountNumber = '062.1000.0000.0000.00.00.000'
SET @DistGLAccountNumber = '062.3406.0000.1007.09.70.000'
SET @RunDate = GETDATE()

INSERT INTO GL_ENTRY
(
	ACCOUNTING_YEAR, 
	ACCOUNTING_PERIOD, 
	GL_ENTRY_DATE, 
	GL_POSTING_DATE, 
	[DESCRIPTION], 
	IS_POSTED, 
	COUNTRY_CODE
)
SELECT	@GLAccountingYear,
		@GLAccountingPeriod,
		@RunDate,
		NULL,
		'Delete Customer Refund',
		'N',
		'CA'
		
SET @GL_Entry_ID = SCOPE_IDENTITY()

INSERT INTO GL_TRANSACTION
SELECT	@GL_Entry_ID,
		@DistGLAccountNumber,
		'C',
		@Amount,
		2 --Status 1 = new, 2 = approved

INSERT INTO GL_TRANSACTION
SELECT	@GL_Entry_ID,
		@LiabilityGLAccountNumber,
		'D',
		@Amount,
		2 --Status 1 = new, 2 = approved

INSERT INTO GL_TRANSACTION
SELECT	@GL_Entry_ID,
		@LiabilityGLAccountNumber,
		'C',
		@Amount,
		2 --Status 1 = new, 2 = approved

INSERT INTO GL_TRANSACTION
SELECT	@GL_Entry_ID,
		@CashGLAccountNumber,
		'D',
		@Amount,
		2 --Status 1 = new, 2 = approved
COMMIT TRAN t5