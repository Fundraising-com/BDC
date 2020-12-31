--Must delete refund cheques tied to the statement also

SELECT	StatementID
INTO	#StatementToDelete
FROM	[Statement]
WHERE	StatementID BETWEEN 110264 AND 110291
AND		StatementID NOT IN (110271, 110272)

BEGIN TRAN

DELETE	statInv
FROM	StatementInvoice statInv
JOIN	#StatementToDelete statDel
			ON	statDel.StatementID = statInv.StatementID

DELETE	statInvOnl
FROM	StatementInvoiceOnline statInvOnl
JOIN	#StatementToDelete statDel
			ON	statDel.StatementID = statInvOnl.StatementID

DELETE	statInvCustSvc
FROM	StatementInvoiceCustSvc statInvCustSvc
JOIN	#StatementToDelete statDel
			ON	statDel.StatementID = statInvCustSvc.StatementID

DELETE	statPmt
FROM	StatementPayment statPmt
JOIN	#StatementToDelete statDel
			ON	statDel.StatementID = statPmt.StatementID

DELETE	statAdj
FROM	StatementAdjustment statAdj
JOIN	#StatementToDelete statDel
			ON	statDel.StatementID = statAdj.StatementID

DELETE	statPrintReq
FROM	StatementPrintRequest statPrintReq
JOIN	#StatementToDelete statDel
			ON	statDel.StatementID = statPrintReq.StatementID

DELETE	stat
FROM	[Statement] stat
JOIN	#StatementToDelete statDel
			ON	statDel.StatementID = stat.StatementID

COMMIT TRAN