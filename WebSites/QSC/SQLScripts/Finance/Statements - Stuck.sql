select top 99 *
from Statement s
left join statementprintrequest p on p.statementid = s.statementid
where campaignid = 83950
order by s.StatementID

select *
from StatementPrintRequestError e
join Statement s on s.StatementID = e.StatementID
where s.CampaignID = 83950

select orderqualifierid, *
from QSPCanadaOrderManagement..Batch
where campaignid = 83950
and orderqualifierid not in (39009)

select *
from qspcanadacommon..campaignaudit
where id = 83950

select *
from statementprintrequesterrortype