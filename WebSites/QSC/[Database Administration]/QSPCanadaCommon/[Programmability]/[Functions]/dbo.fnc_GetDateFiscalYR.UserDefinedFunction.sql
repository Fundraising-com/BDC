USE [QSPCanadaCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[fnc_GetDateFiscalYR]    Script Date: 06/07/2017 09:33:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fnc_GetDateFiscalYR]
(
	@FYDate datetime
)
RETURNS int
AS
/*
BEGIN
	DECLARE @FirstOfFiscalYR datetime
	DECLARE @FiscalYR int
	
	SET @FirstOfFiscalYR = '7/1/'+ STR(YEAR(@FYDate))
	SET @FiscalYR = 0
		
	IF (@FYDate < @FirstOfFiscalYR)
		BEGIN
			SET @FiscalYR = YEAR(@FYDate)
		END
	ELSE
		BEGIN
			SET @FiscalYR = YEAR(@FYDate) + 1
		END
	
	RETURN @FiscalYR
END
*/
BEGIN
   DECLARE @FiscalYR int
	Select @FiscalYR = Case
	When Month(@FYDate) in (7,8,9,10, 11,12) then
		Year(@FYDate) + 1
	When Month(@FYDate) in (1,2,3,4,5,6) then
		Year(@FYDate)
	End
   RETURN(@FiscalYR)
End
GO
