select *
from Incident i
left join IncidentAction ia on ia.incidentinstance = i.IncidentInstance
where i.IncidentInstance in (
1714169,
1714088,
1714098,
1714058,
1714061,
1714083,
1714047,
1714113,
1714173,
1714054
)
order by i.IncidentInstance

select *
from CustomerOrderDetail
where CustomerOrderHeaderInstance in (
13053777,
13079767,
13079766,
13117341,
12990443,
12990411,
13118757,
12979049,
13108635,
12952365)
order by CustomerOrderHeaderInstance

select *
from Incident i
join IncidentAction ia on ia.incidentinstance = i.IncidentInstance
where CustomerOrderHeaderInstance in (
13053777,
13079767,
13079766,
13117341,
12990443,
12990411,
13118757,
12979049,
13108635,
12952365)
and ia.ActionInstance = 18
order by i.IncidentInstance