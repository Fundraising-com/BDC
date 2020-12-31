USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AccountingPeriod_GetCurrent]    Script Date: 06/07/2017 09:16:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AccountingPeriod_GetCurrent]

	@AccountingYear		INT OUTPUT,
	@AccountingPeriod	INT OUTPUT

AS

DECLARE @Date	DATETIME
SET @Date = GETDATE()

SELECT	@AccountingYear = Accounting_Year, 
		@AccountingPeriod = Accounting_Period
FROM	Accounting_Period
WHERE	@Date BETWEEN [Start_Date] AND End_Date
AND		Is_Closed = 'N'
GO
