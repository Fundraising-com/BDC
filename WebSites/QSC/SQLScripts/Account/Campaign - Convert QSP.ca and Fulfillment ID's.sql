select qf.campaign_id
from ca_oltp1.QSPCanadaCommon.dbo.campaign qc, qspfulfillment.dbo.campaign qf
where qc.ID = qf.fulf_campaign_id
AND qc.ID = 49343 

select qc.ID
from ca_oltp1.QSPCanadaCommon.dbo.campaign qc, qspfulfillment.dbo.campaign qf
where qc.ID = qf.fulf_campaign_id
AND qf.campaign_ID = 495628