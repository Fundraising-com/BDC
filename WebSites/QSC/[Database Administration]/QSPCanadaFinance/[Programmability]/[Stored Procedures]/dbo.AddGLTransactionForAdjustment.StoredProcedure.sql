USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AddGLTransactionForAdjustment]    Script Date: 06/07/2017 09:17:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[AddGLTransactionForAdjustment]

	@GLEntryID		INT,
	@GLAccountID	VARCHAR(50),
	@DebitCredit	VARCHAR(1),
	@Amount			NUMERIC(10,2)

AS

INSERT GL_Transaction
(
	GL_Entry_ID,
	GLAccountID,
	Debit_Credit,
	Amount,
	GL_Transaction_Status_ID
)
SELECT	@GLEntryID,
		@GLAccountID,
		@DebitCredit,
		@Amount,
		2
GO
