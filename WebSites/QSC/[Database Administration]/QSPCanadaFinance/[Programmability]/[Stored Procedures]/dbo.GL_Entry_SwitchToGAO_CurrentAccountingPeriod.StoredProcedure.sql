USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GL_Entry_SwitchToGAO_CurrentAccountingPeriod]    Script Date: 06/07/2017 09:17:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GL_Entry_SwitchToGAO_CurrentAccountingPeriod]

AS

DECLARE     @Accounting_Year  INT,
            @Accounting_Period	INT

SELECT      @Accounting_Year = Accounting_Year,
            @Accounting_Period = Accounting_Period
FROM		Accounting_Period
WHERE		[Start_Date] = (SELECT MIN([Start_Date]) FROM Accounting_Period WHERE Is_Closed = 'N')

EXEC  [dbo].[GL_Entry_SwitchToGAO]
            @AccountingYear = @Accounting_Year,
            @AccountingPeriod = @Accounting_Period
GO
