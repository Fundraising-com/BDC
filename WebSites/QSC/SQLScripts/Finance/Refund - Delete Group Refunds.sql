SELECT	adj.Adjustment_ID, e.GL_Entry_ID, t.GL_Transaction_ID, adj.Adjustment_Amount, e.Is_Posted
INTO	#AdjustmentsToDelete
FROM	Adjustment adj
JOIN	GL_Entry e
			ON	e.Adjustment_ID = adj.Adjustment_ID
JOIN	GL_Transaction t
			ON	t.GL_Entry_ID = e.GL_Entry_ID
WHERE	adj.Adjustment_ID BETWEEN 90581 AND 90587

SELECT	*
FROM	#AdjustmentsToDelete

SELECT		ref.Refund_ID, ref.amount, apc.AP_Cheque_ID, apc.ChequeNumber
INTO		#GroupRefundsToDelete
FROM		Refund ref
LEFT JOIN	AP_Cheque apc
				ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
WHERE		ref.Refund_ID BETWEEN 1045533 and 1045534

SELECT	*
FROM	#GroupRefundsToDelete

BEGIN TRAN t1
DELETE  GL_Transaction
FROM	GL_Transaction t
JOIN	#AdjustmentsToDelete atd
			ON	atd.GL_Entry_ID = t.GL_Entry_ID
COMMIT TRAN t1

BEGIN TRAN t2
DELETE  GL_Entry
FROM	GL_Entry e
JOIN	#AdjustmentsToDelete atd
			ON	atd.Adjustment_ID = e.Adjustment_ID
COMMIT TRAN t2

BEGIN TRAN t3
DELETE  Adjustment
FROM	Adjustment adj
JOIN	#AdjustmentsToDelete atd
			ON	atd.Adjustment_ID = adj.Adjustment_ID
COMMIT TRAN t3

update adjustment
set refund_id = null
where refund_id is not null

BEGIN TRAN t4
DELETE  Refund
FROM	Refund ref
JOIN	#GroupRefundsToDelete grtd
			ON	grtd.Refund_ID = ref.Refund_ID
COMMIT TRAN t4

BEGIN TRAN t5
UPDATE	stat
SET		AP_Cheque_ID = NULL
FROM	[Statement] stat
JOIN	#GroupRefundsToDelete grtd
			ON	grtd.AP_Cheque_ID = stat.AP_Cheque_ID
COMMIT TRAN t5

BEGIN TRAN t6
DELETE  AP_Cheque
FROM	AP_Cheque apc
JOIN	#GroupRefundsToDelete grtd
			ON	grtd.AP_Cheque_ID = apc.AP_Cheque_ID
COMMIT TRAN t6

select * from ap_cheque where ap_cheque_batch_id = 144

delete ap_cheque_batch
where ap_cheque_batch_id = 144