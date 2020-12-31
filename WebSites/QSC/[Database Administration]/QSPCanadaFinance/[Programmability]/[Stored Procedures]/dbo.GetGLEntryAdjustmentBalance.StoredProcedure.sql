USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetGLEntryAdjustmentBalance]    Script Date: 06/07/2017 09:17:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetGLEntryAdjustmentBalance]

	@AdjustmentID 	INT,
	@Value 			INT OUTPUT

AS

DECLARE @Amount	NUMERIC(10, 2)

SELECT		CASE glt.Debit_Credit
				WHEN 'C' THEN SUM(Amount) 
				WHEN 'D' THEN SUM(Amount) * -1 
			END AS Amount
INTO		#Temp
FROM		GL_Entry gle
JOIN		GL_Transaction glt
				ON	gle.GL_Entry_ID = glt.GL_Entry_ID
JOIN		GLAccount gla
				ON	gla.GLAccountID = glt.GLAccountID
WHERE		gle.Adjustment_ID = @AdjustmentID  
GROUP BY	glt.Debit_Credit

SELECT	@Amount = SUM(Amount)
FROM	#Temp

IF ISNULL(@Amount,0) <> 0
	BEGIN
		SET @Value = 1 -- Not Equal
	END
ELSE
	BEGIN
		SET @Value = 0 -- Equal
	END

DROP TABLE #Temp
GO
