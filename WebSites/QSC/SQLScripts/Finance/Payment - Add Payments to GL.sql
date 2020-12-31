--Adding 1 payment to GL
EXEC	QSPCanadaFinance..GL_Entry_InsertPayment 1622117 -- @PaymentId 		

--Adding many payments to GL
DECLARE	@StartDate AS DateTime
DECLARE	@EndDate AS DateTime
SELECT	@StartDate = Start_Date,
		@EndDate = End_Date
FROM	Accounting_Period
WHERE	GETDATE() BETWEEN Start_Date AND End_Date
AND		Is_Closed = 'N'

SELECT		p.*
INTO		#PaymentsNoGL
FROM		Payment p
LEFT JOIN	GL_Entry e
				ON	e.Payment_ID = p.Payment_ID
WHERE		e.Payment_ID IS NULL
AND			p.Payment_Effective_Date BETWEEN @StartDate AND @EndDate

SELECT	*
FROM	#PaymentsNoGL

BEGIN TRAN t1
DECLARE @PaymentId		INT
DECLARE @PaymentAmount	NUMERIC(10,2)
DECLARE	@RetVal			INT 

DECLARE Payments CURSOR FOR
SELECT	Payment_ID,
		Payment_Amount
FROM	#PaymentsNoGL

OPEN	Payments
FETCH	Next FROM Payments INTO @PaymentId, @PaymentAmount
	
WHILE @@Fetch_Status = 0
BEGIN	
	IF @PaymentAmount > 0 
	BEGIN
		EXEC QSPCanadaFinance..GL_Function	Null,			--Invoice_Id
											@PaymentID,		--PaymentId
											Null,			--AdjustmentId
											2,				-- Trans Id 2 for PaymentRecording
											62,    			-- @Entity QSP
											@PaymentAmount,	--PaymentAmount
											@RetVal			-- Output Variable returns 0 (success) or 1 (Error)
	END
	FETCH Next FROM Payments INTO @PaymentID, @PaymentAmount
END
CLOSE Payments
DEALLOCATE Payments
COMMIT TRAN t1