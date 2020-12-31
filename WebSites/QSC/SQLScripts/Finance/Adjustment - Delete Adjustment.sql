USE [QSPCanadaFinance]

select top 10 *
from adjustment
order by adjustment_id desc

SELECT		adj.Adjustment_ID, e.GL_Entry_ID, t.GL_Transaction_ID, e.IS_Posted AS ISPostedToGL
INTO		#AdjustmentsToDelete
FROM		Adjustment adj
LEFT JOIN	GL_Entry e
				ON	e.Adjustment_ID = adj.Adjustment_ID
LEFT JOIN	GL_Transaction t
				ON	t.GL_Entry_ID = e.GL_Entry_ID
WHERE		adj.Adjustment_ID IN (88020)
ORDER BY adj.Account_ID

SELECT		*
FROM		#AdjustmentsToDelete atd
JOIN		Adjustment adj
				ON	adj.Adjustment_ID = atd.Adjustment_ID
LEFT JOIN	GL_Entry e
				ON	e.Adjustment_ID = adj.Adjustment_ID
LEFT JOIN	GL_Transaction t
				ON	t.GL_Entry_ID = e.GL_Entry_ID

BEGIN TRAN

DELETE  GL_Transaction
FROM	GL_Transaction t
JOIN	#AdjustmentsToDelete atd
			ON	atd.GL_Entry_ID = t.GL_Entry_ID

DELETE  GL_Entry
FROM	GL_Entry e
JOIN	#AdjustmentsToDelete atd
			ON	atd.Adjustment_ID = e.Adjustment_ID

DELETE  Adjustment
FROM	Adjustment adj
JOIN	#AdjustmentsToDelete atd
			ON	atd.Adjustment_ID = adj.Adjustment_ID

COMMIT TRAN

DROP TABLE #AdjustmentsToDelete