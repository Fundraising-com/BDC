select MONTH(cod.CreationDate) [Month], YEAR(cod.CreationDate) [Year], cd.Description ProductLine, COUNT(*) NumberOfLineItems
from CustomerOrderDetail cod
join CustomerOrderHeader coh on coh.Instance = cod.CustomerOrderHeaderInstance
join Batch b on b.ID = coh.OrderBatchID and b.Date = coh.OrderBatchDate
join QSPCanadaCommon..CodeDetail cd on cd.Instance = cod.ProductType
where YEAR(cod.CreationDate) >= 2010
and b.StatusInstance <> 40005
and b.OrderQualifierID in (39001, 39002)
GROUP BY MONTH(cod.CreationDate), YEAR(cod.CreationDate), cd.Description
ORDER BY YEAR(cod.CreationDate), MONTH(cod.CreationDate)