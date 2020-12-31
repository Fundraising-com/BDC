USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetGLEntriesByPayment]    Script Date: 06/07/2017 09:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetGLEntriesByPayment]

	@InvoiceID 	INT,
	@PaymentID 	INT

AS

SELECT		gle.GL_Entry_ID,
			gle.Invoice_ID,
			gle.Payment_ID,
			gle.GL_Entry_Date,
			gle.GL_Posting_Date,
			gle.Description,
			gle.Is_Posted,
			dbo.UDF_GLAccount_GetAccountNumber(gla.GLAccountID) AS GL_Account_Number,
			gla.Description AS GL_Account_Description,
			glt.Debit_Credit,
			glt.Amount
FROM		GL_Entry gle
JOIN		GL_Transaction glt
				ON	glt.GL_Entry_ID = gle.GL_Entry_ID
JOIN		GLAccount gla
				ON	gla.GLAccountID = glt.GLAccountID
WHERE		gle.Payment_ID = @PaymentID
ORDER BY	gle.GL_Posting_Date DESC
GO
