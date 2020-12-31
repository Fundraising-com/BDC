select (numberofparticipants)
from campaign c
join CAccount a on a.ID = c.billtoaccountid
where c.status = 37002
and c.startdate between '2012-07-01' and '2013-06-30'
and c.isstafforder = 0
and a.businessunitid in (1,3)

3214, 928234
2180, 693120
