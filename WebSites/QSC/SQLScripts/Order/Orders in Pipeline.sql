select *
from core.CustomerOrder co
join core.tote t on t.toteid = co.toteidcontract
join core.contract c on c.contractid = t.contractid
join core.contractaddress ca on ca.contractid = c.contractid and ca.isorganization = 1
where FormCode in ('0737')--, '0745')
and CustomerOrderStateID NOT IN (23, 39)
order by co.toteidcontract

select *
from form

select *
from core.customerorderstate

select top 99 *
from batch
where orderqualifierid in (39001,39002)
order by Date desc