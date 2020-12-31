USE QSPCanadaFinance
GO

SELECT		*
FROM		ACCOUNTING_PERIOD
ORDER BY	ACCOUNTING_YEAR DESC, ACCOUNTING_PERIOD DESC

DECLARE @AccountingYear INT
DECLARE @AccountingPeriod INT

SET @AccountingYear = 2016
SET @AccountingPeriod = 1

SELECT		e.Accounting_year,
			e.Accounting_period,
			ap.start_date,
			ap.end_date,
			CASE	WHEN a.GLAccountID IN (146, 193) THEN 'CAN'
					WHEN a.GLAccountID IN (147, 194) THEN 'NB'
					WHEN a.GLAccountID IN (155, 201) THEN 'QC'
					WHEN a.GLAccountID IN (148, 195) THEN 'NS'
					WHEN a.GLAccountID IN (149, 207) THEN 'NL'
					WHEN a.GLAccountID IN (216, 218) THEN 'ON'
					WHEN a.GLAccountID IN (153, 199) THEN 'PE'
					WHEN a.GLAccountID IN (151, 197) THEN 'MB'
					WHEN a.GLAccountID IN (154, 200) THEN 'SK'
					WHEN a.GLAccountID IN (215, 217) THEN 'BC'
			END AS Province,		
			a.glaccountid,
			CASE e.Country_Code WHEN 'CA' THEN 'CAD' ELSE 'USD' END Currency,
			a.description,
			SUM(t.amount) TransactionAmount
from		gl_entry e
join		accounting_period ap ON ap.accounting_period = e.accounting_period and ap.Accounting_year = e.Accounting_year
join		gl_transaction t on t.gl_entry_id = e.gl_entry_id
join		glaccount a on a.glaccountid = t.glaccountid
join		invoice i on i.invoice_id = e.invoice_id
join		qspcanadaordermanagement..batch b on b.orderid = i.order_id
where		ap.Accounting_Year = @AccountingYear
and			ap.Accounting_Period = @AccountingPeriod
group by	e.accounting_year, e.accounting_period,ap.start_date, ap.end_date, a.glaccountid, e.Country_Code, a.description 
--order by	e.accounting_year, e.accounting_period, ap.start_date, ap.end_date, Province, a.glaccountid, a.description

UNION ALL

select e.Accounting_year, e.Accounting_period, ap.start_date, ap.end_date, CASE a.Account WHEN '223100' THEN 'QC' ELSE 'CAN' END AS Province, a.glaccountid, CASE e.Country_Code WHEN 'CA' THEN 'CAD' ELSE 'USD' END Currency, a.description, SUM(CASE t.Debit_Credit WHEN 'C' THEN t.Amount * -1 ELSE t.Amount END) SalesRevenue
from gl_entry e
join accounting_period ap ON ap.accounting_period = e.accounting_period and ap.Accounting_year = e.Accounting_year
join gl_transaction t on t.gl_entry_id = e.gl_entry_id
join glaccount a on a.glaccountid = t.glaccountid
where a.Account in ('223100', '223200')
and	ap.Accounting_Year = @AccountingYear
and ap.Accounting_Period = @AccountingPeriod
group by e.accounting_year, e.accounting_period, ap.start_date, ap.end_date, a.Account, a.glaccountid, e.Country_Code, a.description

order by e.accounting_year, e.accounting_period, ap.start_date, ap.end_date, a.glaccountid, a.description
