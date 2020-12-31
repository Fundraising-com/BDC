USE QSPCanadaFinance
GO

select *
from StatementRun
where StatementRunID >= 60
order by statementrunid

select DATEADD(yy, 1, statementrundate) StatementRunDate, FiscalYearEnd, 0 StatementRunClosed, StatementsInOwingOnly
from StatementRun
where StatementRunID >= 60
order by StatementRunID

begin tran
insert StatementRun
select DATEADD(yy, 1, statementrundate) StatementRunDate, FiscalYearEnd, 0 StatementRunClosed, StatementsInOwingOnly
from StatementRun
where StatementRunID >= 60
order by StatementRunID
--commit tran
