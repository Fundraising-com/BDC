SELECT	e.GL_Entry_ID, t.GL_Transaction_ID, e.IS_Posted AS ISPostedToGL
INTO	#EntriesToDelete
FROM	GL_Entry e
JOIN	GL_Transaction t
			ON	t.GL_Entry_ID = e.GL_Entry_ID
WHERE	e.GL_Entry_ID BETWEEN 1361953 AND 1362240

SELECT	*
FROM	#EntriesToDelete

BEGIN TRAN t1

DELETE  GL_Transaction
FROM	GL_Transaction t
JOIN	#EntriesToDelete etd
			ON	etd.GL_Entry_ID = t.GL_Entry_ID

DELETE  GL_Entry
FROM	GL_Entry e
JOIN	#EntriesToDelete etd
			ON	etd.GL_Entry_ID = e.GL_Entry_ID

COMMIT TRAN t1

