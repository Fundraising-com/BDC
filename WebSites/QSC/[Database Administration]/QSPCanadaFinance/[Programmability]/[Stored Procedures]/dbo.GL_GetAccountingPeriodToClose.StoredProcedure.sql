USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GL_GetAccountingPeriodToClose]    Script Date: 06/07/2017 09:17:22 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GL_GetAccountingPeriodToClose]

	@Accounting_Year	INT OUTPUT,
	@Accounting_Period	INT OUTPUT

AS

--Get oldest open GL Accounting Period that needs to be closed
SELECT	@Accounting_Year = Accounting_Year,
		@Accounting_Period = Accounting_Period
FROM	Accounting_Period
WHERE	[Start_Date] = (SELECT	MIN([Start_Date])
						FROM	Accounting_Period
						WHERE	Is_Closed = 'N'
						AND		GETDATE() > [End_Date])

IF ISNULL(@Accounting_Year, 0) = 0
BEGIN
	SET @Accounting_Year = 0
END

IF ISNULL(@Accounting_Period, 0) = 0
BEGIN
	SET @Accounting_Period = 0
END
GO
