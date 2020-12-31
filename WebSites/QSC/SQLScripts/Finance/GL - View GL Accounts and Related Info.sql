SELECT		*
FROM		GL_Account gla
LEFT JOIN	GL_Transaction_PL gltpl
				ON	gltpl.GL_Account_Number = gla.GL_Account_Number
LEFT JOIN	GL_Entry_Product_Line glepl
				ON	gltpl.GL_Entry_Product_Line_ID = gltpl.GL_Entry_Product_Line_ID
LEFT JOIN	GL_Entry_Model glem
				ON	glem.GL_Entry_Model_ID = glepl.GL_Entry_Model_ID
LEFT JOIN	GL_Entry_Type glet
				ON	glet.GL_Entry_Type_ID = glem.GL_Entry_Type_ID