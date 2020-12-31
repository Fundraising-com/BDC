select cd.Description, DATEPART(dd, e.GL_ENTRY_DATE) 'Date in January', SUM(Payment_Amount) Amount
from payment p
join qspcanadacommon..codedetail cd on cd.instance = p.payment_method_id
join GL_ENTRY e on e.PAYMENT_ID = p.PAYMENT_ID
where e.GL_ENTRY_DATE between '2017-01-01' and '2017-02-01'
group by cd.Description, DATEPART(dd, e.GL_ENTRY_DATE)
order by DATEPART(dd, e.GL_ENTRY_DATE), cd.Description

----------
select cd.Description, DATEPART(dd, p.payment_effective_date) 'Date in September', SUM(Payment_Amount) Amount
from payment p
join qspcanadacommon..codedetail cd on cd.instance = p.payment_method_id
where payment_effective_date between '2015-09-01' and '2015-10-01'
group by cd.Description, DATEPART(dd, p.payment_effective_date)
order by DATEPART(dd, p.payment_effective_date), cd.Description

select cd.Description, DATEPART(dd, p.payment_effective_date) 'Date in September', SUM(Payment_Amount) Amount
from payment p
join qspcanadacommon..codedetail cd on cd.instance = p.payment_method_id
join GL_ENTRY e on e.PAYMENT_ID = p.PAYMENT_ID
join GL_TRANSACTION t on t.gl_entry_id = e.GL_ENTRY_ID and t.DEBIT_CREDIT = 'C'
where payment_effective_date between '2015-09-01' and '2015-10-01'
and e.GL_ENTRY_DATE not between '2015-09-01' and '2015-10-01'
group by cd.Description, DATEPART(dd, p.payment_effective_date)
order by DATEPART(dd, p.payment_effective_date), cd.Description


select *
from payment p
join GL_ENTRY e on e.PAYMENT_ID = p.PAYMENT_ID
join GL_TRANSACTION t on t.gl_entry_id = e.GL_ENTRY_ID and t.DEBIT_CREDIT = 'C'
where p.PAYMENT_EFFECTIVE_DATE between '2015-09-01' and '2015-10-01'
and e.GL_ENTRY_DATE not between '2015-09-01' and '2015-10-01'
order by PAYMENT_EFFECTIVE_DATE

select *
from GL_ENTRY e
left join GL_TRANSACTION t on t.gl_entry_id = e.GL_ENTRY_ID and t.DEBIT_CREDIT = 'D'
where e.PAYMENT_ID > 0
and e.GL_ENTRY_DATE between '2015-09-01' and '2015-10-01'