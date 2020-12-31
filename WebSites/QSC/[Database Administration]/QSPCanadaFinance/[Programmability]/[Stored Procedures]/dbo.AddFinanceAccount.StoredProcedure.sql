USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AddFinanceAccount]    Script Date: 06/07/2017 09:17:00 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[AddFinanceAccount]
	@AccountID 		int,	
	@AccountType		int,
	@ChangedBy		int
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 4/30/2004 
--   Insert a new Finance Account Record For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

INSERT QSPCanadaFinance..Account (
			ACCOUNT_ID,
			ACCOUNT_TYPE_ID,
			EMPLOYEE_NAME,
			LAST_STATEMENT_DATE, 
			LAST_AGING_DATE,
			AGING_CURRENT,
			AGING_30, 
			AGING_60, 
			AGING_90, 
			AGING_120_OVER, 
			DATE_CREATED, 
			DATE_MODIFIED, 
			LAST_UPDATED_BY, 
			COUNTRY_CODE)
VALUES(@AccountID, 
	@AccountType, 
	@ChangedBy,
	null,
	null,
	0, 
	0, 
	0, 
	0, 
	0, 
	GETDATE(), 
	null, 
	null, 
	'CA')

SET NOCOUNT OFF
GO
