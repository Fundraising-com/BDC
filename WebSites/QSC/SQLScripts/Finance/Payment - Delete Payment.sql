USE QSPCanadaFinance
GO

/*select top 9 * from payment
order by payment_id desc*/

SELECT	pmt.Payment_ID, e.GL_Entry_ID, t.GL_Transaction_ID
INTO	#PaymentsToDelete
FROM	Payment pmt
JOIN	GL_Entry e
			ON	e.Payment_ID = pmt.Payment_ID
JOIN	GL_Transaction t
			ON	t.GL_Entry_ID = e.GL_Entry_ID
WHERE	pmt.Payment_ID IN (
3026703

)

ORDER BY pmt.Account_ID

SELECT	*
FROM	#PaymentsToDelete d
JOIN	Payment p ON p.payment_id = d.payment_id

BEGIN TRAN
DELETE  GL_Transaction
FROM	GL_Transaction t
JOIN	#PaymentsToDelete atd
			ON	atd.GL_Entry_ID = t.GL_Entry_ID

DELETE  GL_Entry
FROM	GL_Entry e
JOIN	#PaymentsToDelete atd
			ON	atd.Payment_ID = e.Payment_ID

DELETE  Payment
FROM	Payment pmt
JOIN	#PaymentsToDelete atd
			ON	atd.Payment_ID = pmt.Payment_ID
COMMIT TRAN
