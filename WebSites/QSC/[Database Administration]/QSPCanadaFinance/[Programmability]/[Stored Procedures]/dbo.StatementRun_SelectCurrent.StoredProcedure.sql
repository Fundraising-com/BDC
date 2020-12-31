USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[StatementRun_SelectCurrent]    Script Date: 06/07/2017 09:17:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[StatementRun_SelectCurrent]

	@StatementRunID		INT OUTPUT,
	@StatementRunDate	DATETIME OUTPUT,
	@FiscalYearEnd		BIT OUTPUT,
	@StatementRunClosed	BIT OUTPUT

AS

SELECT		TOP 1
			@StatementRunID = StatementRunID,
			@StatementRunDate = StatementRunDate,
			@FiscalYearEnd = FiscalYearEnd,
			@StatementRunClosed = StatementRunClosed
FROM		StatementRun
WHERE		StatementRunDate <= GETDATE()
ORDER BY	StatementRunDate DESC
GO
