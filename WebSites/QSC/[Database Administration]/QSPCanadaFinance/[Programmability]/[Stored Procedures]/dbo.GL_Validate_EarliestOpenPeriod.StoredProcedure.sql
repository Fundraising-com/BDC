USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GL_Validate_EarliestOpenPeriod]    Script Date: 06/07/2017 09:17:23 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GL_Validate_EarliestOpenPeriod]

AS

--Get most recent open GL Accounting Period
DECLARE	@Accounting_Year	INT,
		@Accounting_Period	INT
SELECT	@Accounting_Year = Accounting_Year,
		@Accounting_Period = Accounting_Period
FROM	Accounting_Period
WHERE	[Start_Date] = (SELECT MIN([Start_Date]) FROM Accounting_Period WHERE Is_Closed = 'N')

DECLARE	@IsValidPeriod BIT

EXEC GL_Validate @Accounting_Year = @Accounting_Year, @Accounting_Period = @Accounting_Period, @IsValidPeriod = @IsValidPeriod OUTPUT
GO
