select fm.fmid, fm.firstname, fm.lastname, camp.lang AccountLanguage, SUM(cod.Quantity) NumberOfCatalogs
from CustomerOrderDetail cod
join CustomerOrderHeader coh on coh.Instance = cod.CustomerOrderHeaderInstance
join Batch b on b.ID = coh.OrderBatchID and b.Date = coh.OrderBatchDate
join QSPCanadaCommon..Campaign camp on camp.ID = b.CampaignID
join QSPCanadaCommon..FieldManager fm on fm.FMID = camp.FMID
where cod.CreationDate between '2013-07-01' and '2013-12-31'
and b.StatusInstance <> 40005
and cod.DelFlag <> 1
and cod.ProductCode = '1054003' --Staff catalog
GROUP BY fm.fmid, fm.firstname, fm.lastname, camp.Lang
ORDER BY fm.fmid


/*
select *
from qspcanadaproduct..Product
where Product_Code = '1054003'
*/