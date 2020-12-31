SELECT	pmt.Payment_ID, e.GL_Entry_ID, t.GL_Transaction_ID
INTO	#PaymentsToUpdate
FROM	Payment pmt
JOIN	GL_Entry e
			ON	e.Payment_ID = pmt.Payment_ID
JOIN	GL_Transaction t
			ON	t.GL_Entry_ID = e.GL_Entry_ID
WHERE	pmt.Payment_ID IN (
1052453
)
ORDER BY pmt.Account_ID

--Ensure GL hasn't been posted
SELECT	e.Is_Posted, *
FROM	#PaymentsToUpdate pmtupd
JOIN	GL_Entry e
			ON	e.Payment_ID = pmtupd.Payment_ID

BEGIN TRAN t1
UPDATE  pmt
SET		pmt.Order_ID = 802804,
		pmt.Account_ID = 4765,
		pmt.Campaign_ID = 54391
FROM	Payment pmt
JOIN	#PaymentsToUpdate pmtupd
			ON	pmtupd.Payment_ID = pmt.Payment_ID
COMMIT TRAN t1
