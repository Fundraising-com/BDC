USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetCurrentAccountingPeriod]    Script Date: 06/07/2017 09:17:15 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetCurrentAccountingPeriod]
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 5/3/2004 
--   Current Accounting Period for Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT MIN(Start_Date) FROM ACCOUNTING_PERIOD
WHERE Is_Closed = 'N'

SET NOCOUNT OFF
GO
