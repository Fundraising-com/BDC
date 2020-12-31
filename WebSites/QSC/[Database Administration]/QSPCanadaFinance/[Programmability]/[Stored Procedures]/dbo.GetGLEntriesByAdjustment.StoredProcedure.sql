USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetGLEntriesByAdjustment]    Script Date: 06/07/2017 09:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetGLEntriesByAdjustment]

	@InvoiceID		INT,
	@AdjustmentID	INT

AS

SELECT		gle.GL_Entry_ID,
			gle.Invoice_ID,
			gle.Adjustment_ID,
			gle.GL_Entry_Date,
			gle.GL_Posting_Date,
			gle.Description,
			gle.Is_Posted,
			dbo.UDF_GLAccount_GetAccountNumber(gla.GLAccountID) AS GL_Account_Number,
			gla.Description AS GL_Account_Description,
			glt.Debit_Credit,
			glt.Amount,
			gla.GLAccountID
FROM		GL_Entry gle
JOIN		GL_Transaction glt
				ON	glt.GL_Entry_ID = gle.GL_Entry_ID
JOIN		GLAccount gla
				ON	gla.GLAccountID = glt.GLAccountID
WHERE		gle.Adjustment_ID = @AdjustmentID
ORDER BY	gle.GL_Posting_Date DESC
GO
