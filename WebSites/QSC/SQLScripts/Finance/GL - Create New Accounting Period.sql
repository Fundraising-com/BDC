SELECT	*
FROM	Accounting_Period

DECLARE	@AccountingYear AS Int
SET		@AccountingYear = 2009
DECLARE	@AccountingPeriod AS Int
SET		@AccountingPeriod = 7
DECLARE	@StartDate AS DateTime
SET		@StartDate = '2008-12-20'
DECLARE	@EndDate AS DateTime
SET		@EndDate = '2009-01-20'

INSERT INTO QSPCanadaFinance..Accounting_Period 
VALUES(@AccountingYear,	@AccountingPeriod,	@StartDate,	@EndDate, 'CA', 'N')

SELECT	*
FROM	Accounting_Period
