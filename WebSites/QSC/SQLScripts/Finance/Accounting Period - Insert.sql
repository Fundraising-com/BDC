USE QSPCanadaFinance
GO

select *
from accounting_period
where accounting_year = 2016
order by ACCOUNTING_YEAR, ACCOUNTING_PERIOD

select ACCOUNTING_YEAR + 1 ACCOUNTING_YEAR, ACCOUNTING_PERIOD, DATEADD(yy,1,Start_Date) Start_Date, DATEADD(yy,1,End_Date) End_Date, Country_code, 'N' Is_Closed
from accounting_period
where accounting_year = 2015
order by ACCOUNTING_YEAR, ACCOUNTING_PERIOD

begin tran
insert ACCOUNTING_PERIOD
select ACCOUNTING_YEAR + 1 ACCOUNTING_YEAR, ACCOUNTING_PERIOD, DATEADD(yy,1,Start_Date) Start_Date, DATEADD(yy,1,End_Date) End_Date, Country_code, 'N' Is_Closed
from accounting_period
where accounting_year = 2015
order by ACCOUNTING_YEAR, ACCOUNTING_PERIOD
--commit tran