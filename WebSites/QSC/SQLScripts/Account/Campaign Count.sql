select *
from Campaign c
join CAccount acc on acc.Id = c.BillToAccountID
where Status = 37002
and StartDate between '2016-01-01' and '2016-06-30'
and c.IsStaffOrder = 0
and c.FMID not in ('0508')
and acc.CAccountCodeClass not in ('FM')

select *
from Campaign c
join CAccount acc on acc.Id = c.BillToAccountID
where Status = 37002
and StartDate between '2015-01-01' and '2015-06-30'
and ApprovedStatusDate < '2015-03-02'
and c.IsStaffOrder = 0
and c.FMID not in ('0508')
and acc.CAccountCodeClass not in ('FM')

select *
from Campaign c
join CAccount acc on acc.Id = c.BillToAccountID
where Status = 37002
and StartDate between '2015-01-01' and '2015-06-30'
and c.IsStaffOrder = 0
and c.FMID not in ('0508')
and acc.CAccountCodeClass not in ('FM')