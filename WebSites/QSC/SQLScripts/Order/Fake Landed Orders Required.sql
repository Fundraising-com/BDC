select distinct c.id CampaignID, cstatus.description CampaignStatus, c.startdate, c.enddate, c.cookiedoughdeliverydate, fm.firstname + ' ' + fm.lastname fm, QSPCanadaOrderManagement.dbo.UDF_ProgramsbyCampaign(c.ID) Programs
from campaign c
join codedetail cstatus on cstatus.instance = c.status
join campaignprogram cp on cp.campaignid = c.id and cp.deletedtf = 0
join fieldmanager fm on fm.fmid = c.fmid
left join QSPCanadaOrderManagement..Batch b on b.CampaignID = c.id and b.OrderQualifierID = 39001
LEFT JOIN (QSPCanadaOrderManagement..Batch b2 
			JOIN	QSPCanadaOrderManagement..CustomerOrderHeader coh ON coh.OrderBatchID = b2.ID AND coh.OrderBatchDate = b2.Date
			JOIN	QSPCanadaOrderManagement..CustomerOrderDetail cod ON cod.CustomerOrderHeaderInstance = coh.Instance
																	 AND cod.DistributionCenterID = 1 AND b2.orderqualifierid = 39009 AND cod.IsShippedToAccount = 1
																	 AND cod.StatusInstance NOT IN (501, 506, 508)
		  )	on b2.campaignid = c.id 
where c.startdate BETWEEN '2016-07-01' AND '2016-12-31'
and c.onlineonlyprograms = 1
--and cp.programid in (42, 44, 51, 54, 55, 56, 58, 59)
and b.orderID is null
and (cod.CustomerOrderHeaderInstance IS NOT NULL OR (cp.programid in (42) AND c.Status IN (37002)))
order by c.enddate
