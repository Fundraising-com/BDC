SELECT	coh.Instance AS CustomerOrderHeaderInstance,
		coh.CampaignID
INTO	#OrdersToTransfer
FROM	CustomerOrderHeader coh
left JOIN	InternetOrderID ioi
			ON	ioi.CustomerOrderHeaderInstance = coh.Instance
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN	Batch b 
			ON	b.ID = coh.OrderBatchID
			AND	b.Date = coh.OrderBatchDate
WHERE	b.orderid = 915650

SELECT	DISTINCT *
FROM	#OrdersToTransfer

--Only can do if order hasn't been closed
SELECT		inv.Is_Printed, ap.Is_Closed, *
FROM		QSPCanadaFinance..Invoice inv
JOIN		Batch b
				ON	b.OrderID = inv.Order_ID
JOIN		CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.[Date]
JOIN		#OrdersToTransfer o
				ON	o.CustomerOrderHeaderInstance = coh.Instance
LEFT JOIN	QSPCanadaFinance..GL_Entry ge
				ON	ge.Invoice_ID = inv.Invoice_ID
LEFT JOIN	QSPCanadaFinance..Accounting_Period ap
				ON	ap.Accounting_Year = ge.Accounting_Year
				AND	ap.Accounting_Period = ge.Accounting_Period

BEGIN TRAN t1
UPDATE	B
SET		CampaignID = 85788,
		AccountID = 33413,
		ShipToAccountID = 33413
FROM	CustomerOrderHeader coh
JOIN	Batch b
			ON	b.ID = coh.OrderBatchID 
			AND	b.Date = coh.OrderBatchDate
JOIN	#OrdersToTransfer o
			ON	o.CustomerOrderHeaderInstance = coh.Instance
COMMIT TRAN t1

BEGIN TRAN t2
UPDATE	coh
SET		CampaignID = 58384
FROM	CustomerOrderHeader coh
JOIN	#OrdersToTransfer o 
			ON	o.CustomerOrderHeaderInstance = coh.Instance
COMMIT TRAN t2

SELECT	*
FROM	QSPCanadaFinance..Adjustment adj
JOIN	Batch b
			ON	b.OrderID = adj.Order_ID
JOIN	CustomerOrderHeader coh
			ON	b.ID = coh.OrderBatchID 
			AND	b.Date = coh.OrderBatchDate
JOIN	#OrdersToTransfer o 
			ON	o.CustomerOrderHeaderInstance = coh.Instance

BEGIN TRAN t3
UPDATE	adj
SET		Campaign_ID = 58384, ACCOUNT_ID = 
FROM	QSPCanadaFinance..Adjustment adj
JOIN	Batch b
			ON	b.OrderID = adj.Order_ID
JOIN	CustomerOrderHeader coh
			ON	b.ID = coh.OrderBatchID 
			AND	b.Date = coh.OrderBatchDate
JOIN	#OrdersToTransfer o 
			ON	o.CustomerOrderHeaderInstance = coh.Instance
COMMIT TRAN t3

SELECT	*
FROM	QSPCanadaFinance..Payment pmt
JOIN	Batch b
			ON	b.OrderID = pmt.Order_ID
JOIN	CustomerOrderHeader coh
			ON	b.ID = coh.OrderBatchID 
			AND	b.Date = coh.OrderBatchDate
JOIN	#OrdersToTransfer o 
			ON	o.CustomerOrderHeaderInstance = coh.Instance

BEGIN TRAN t4
UPDATE	pmt
SET		Campaign_ID = 58384, ACCOUNT_ID = 
FROM	QSPCanadaFinance..Payment pmt
JOIN	Batch b
			ON	b.OrderID = pmt.Order_ID
JOIN	CustomerOrderHeader coh
			ON	b.ID = coh.OrderBatchID 
			AND	b.Date = coh.OrderBatchDate
JOIN	#OrdersToTransfer o 
			ON	o.CustomerOrderHeaderInstance = coh.Instance
COMMIT TRAN t4

--Check Teacher.AccountID