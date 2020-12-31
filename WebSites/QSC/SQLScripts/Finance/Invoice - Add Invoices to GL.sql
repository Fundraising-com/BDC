--Adding 1 invoice to GL
BEGIN TRAN t1
DECLARE	@RetVal INT
EXEC	GL_Entry_InsertInvoice @InvoiceID = 699278 --@Generate_GL_For_Invoice	268453, @RetVal OUTPUT
SELECT	@RetVal
COMMIT TRAN t1

--Adding many invoices to GL
DECLARE	@StartDate AS DateTime
DECLARE	@EndDate AS DateTime
SELECT	@StartDate = Start_Date,
		@EndDate = End_Date
FROM	Accounting_Period
WHERE	GETDATE() BETWEEN Start_Date AND End_Date
AND		Is_Closed = 'N'

SELECT		i.*
INTO		#InvoicesNoGL
FROM		Invoice i
LEFT JOIN	GL_Entry e
				ON	e.Invoice_ID = i.Invoice_ID
WHERE		e.Invoice_ID IS NULL
AND			i.Invoice_Date BETWEEN @StartDate AND @EndDate

SELECT	*
FROM	#InvoicesNoGL

BEGIN TRAN t1
DECLARE @InvoiceId		INT
DECLARE	@RetVal			INT 

DECLARE Invoices CURSOR FOR
SELECT	Invoice_ID
FROM	#InvoicesNoGL

OPEN	Invoices
FETCH	Next FROM Invoices INTO @InvoiceId
	
WHILE @@Fetch_Status = 0
BEGIN	
	EXEC	--Generate_GL_For_Invoice	@InvoiceId, @RetVal OUTPUT
	SELECT	@RetVal
	FETCH Next FROM Invoices INTO @InvoiceId
END
CLOSE Invoices
DEALLOCATE Invoices
COMMIT TRAN t1

DROP TABLE #InvoicesNoGL
